using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmReceiptSearch : Form
{
	private IContainer components = null;
	private UnitOfWork _unitOfWork;

	private GroupBox grpSearch;
	private TextBoxEx txtSearchKeyword;
	private LabelEx lbSearchKeyword;
	private DateTimePickerEx dtFrom;
	private LabelEx lbFrom;
	private DateTimePickerEx dtTo;
	private LabelEx lbTo;
	private Button btnSearch;
	private Button btnReset;

	private DataGridView dgvReceipts;
	private Button btnSelect;
	private Button btnCancel;

	public Order SelectedReceipt { get; private set; }

	public frmReceiptSearch()
	{
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		dtFrom.Value = DateTime.Today.AddDays(-30);
		dtTo.Value = DateTime.Today.AddDays(1);
		LoadReceipts();
	}

	private void LoadReceipts()
	{
		try
		{
			string keyword = txtSearchKeyword.Text.Trim();
			DateTime fromDate = dtFrom.Value.Date;
			DateTime toDate = dtTo.Value.Date.AddDays(1).AddSeconds(-1);

			var query = _unitOfWork.Orders.GetAll()
				.Where(o => o.PosDeviceId != null || o.ReceiptNumber != null || !string.IsNullOrEmpty(o.FullReceiptNumber))
				.Where(o => o.Date >= fromDate && o.Date <= toDate);

			if (!string.IsNullOrEmpty(keyword))
			{
				query = query.Where(o =>
					(o.FullReceiptNumber != null && o.FullReceiptNumber.Contains(keyword)) ||
					(o.CustumerName != null && o.CustumerName.Contains(keyword)) ||
					(o.CustomerId != null && o.CustomerId.Contains(keyword)) ||
					(o.uuid != null && o.uuid.Contains(keyword))
				);
			}

			var list = query.OrderByDescending(o => o.Date).Take(100).ToList();

			dgvReceipts.Rows.Clear();
			foreach (var r in list)
			{
				string posName = r.PosDevice != null ? r.PosDevice.PosName : ("نقطة " + r.PosDeviceId);
				string typeName = r.OrderType == OrderType.SaleReturn ? "مرتجع" : "بيع";
				string statusText = r.sent == OrderEinvSend.Sent ? "مرسل للضرائب" : "محلي (غير مرسل)";

				int rowIndex = dgvReceipts.Rows.Add(
					r.Id,
					r.FullReceiptNumber ?? r.ReceiptNumber?.ToString() ?? r.Id.ToString(),
					typeName,
					r.Date.ToString("yyyy-MM-dd HH:mm"),
					posName,
					r.CustumerName ?? "مستهلك نهائي",
					r.NetInvoice.ToString("N2"),
					r.TotalVat.ToString("N2"),
					statusText,
					r.uuid ?? ""
				);
				dgvReceipts.Rows[rowIndex].Tag = r;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("خطأ أثناء تحميل الإيصالات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		LoadReceipts();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		txtSearchKeyword.Text = "";
		dtFrom.Value = DateTime.Today.AddDays(-30);
		dtTo.Value = DateTime.Today.AddDays(1);
		LoadReceipts();
	}

	private void txtSearchKeyword_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
			LoadReceipts();
		}
	}

	private void btnSelect_Click(object sender, EventArgs e)
	{
		SelectCurrentRow();
	}

	private void dgvReceipts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			SelectCurrentRow();
		}
	}

	private void SelectCurrentRow()
	{
		if (dgvReceipts.CurrentRow != null && dgvReceipts.CurrentRow.Tag is Order order)
		{
			var fullOrder = _unitOfWork.Orders.GetTById(order.Id);
			if (fullOrder != null)
			{
				var details = _unitOfWork.OrderDetails.GetAll().Where(d => d.OrderId == fullOrder.Id).ToList();
				foreach (var d in details)
				{
					if (d.Product == null && d.ProductId.HasValue && d.ProductId.Value > 0)
					{
						d.Product = _unitOfWork.Products.GetTById(d.ProductId.Value);
					}
				}
				fullOrder.OrderDetails = details;
				SelectedReceipt = fullOrder;
			}
			else
			{
				SelectedReceipt = order;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		else
		{
			MessageBox.Show("يرجى اختيار إيصال من الجدول أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		this.DialogResult = DialogResult.Cancel;
		this.Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.grpSearch = new System.Windows.Forms.GroupBox();
		this.lbSearchKeyword = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtSearchKeyword = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbFrom = new E_Invoice.Desktop.Controls.LabelEx();
		this.dtFrom = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.lbTo = new E_Invoice.Desktop.Controls.LabelEx();
		this.dtTo = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.btnSearch = new System.Windows.Forms.Button();
		this.btnReset = new System.Windows.Forms.Button();

		this.dgvReceipts = new System.Windows.Forms.DataGridView();
		this.btnSelect = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();

		this.grpSearch.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
		this.SuspendLayout();

		// grpSearch
		this.grpSearch.Controls.Add(this.btnReset);
		this.grpSearch.Controls.Add(this.btnSearch);
		this.grpSearch.Controls.Add(this.dtTo);
		this.grpSearch.Controls.Add(this.lbTo);
		this.grpSearch.Controls.Add(this.dtFrom);
		this.grpSearch.Controls.Add(this.lbFrom);
		this.grpSearch.Controls.Add(this.txtSearchKeyword);
		this.grpSearch.Controls.Add(this.lbSearchKeyword);
		this.grpSearch.Location = new System.Drawing.Point(12, 12);
		this.grpSearch.Name = "grpSearch";
		this.grpSearch.Size = new System.Drawing.Size(860, 75);
		this.grpSearch.TabIndex = 0;
		this.grpSearch.TabStop = false;
		this.grpSearch.Text = "بحث وتصفية الإيصالات";

		// lbSearchKeyword
		this.lbSearchKeyword.AutoSize = true;
		this.lbSearchKeyword.Location = new System.Drawing.Point(780, 32);
		this.lbSearchKeyword.Name = "lbSearchKeyword";
		this.lbSearchKeyword.Size = new System.Drawing.Size(71, 18);
		this.lbSearchKeyword.Text = "بحث عام:";

		// txtSearchKeyword
		this.txtSearchKeyword.Location = new System.Drawing.Point(540, 28);
		this.txtSearchKeyword.Name = "txtSearchKeyword";
		this.txtSearchKeyword.Size = new System.Drawing.Size(235, 26);
		this.txtSearchKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchKeyword_KeyDown);

		// lbFrom
		this.lbFrom.AutoSize = true;
		this.lbFrom.Location = new System.Drawing.Point(490, 32);
		this.lbFrom.Name = "lbFrom";
		this.lbFrom.Size = new System.Drawing.Size(34, 18);
		this.lbFrom.Text = "من:";

		// dtFrom
		this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtFrom.Location = new System.Drawing.Point(365, 28);
		this.dtFrom.Name = "dtFrom";
		this.dtFrom.Size = new System.Drawing.Size(120, 26);

		// lbTo
		this.lbTo.AutoSize = true;
		this.lbTo.Location = new System.Drawing.Point(320, 32);
		this.lbTo.Name = "lbTo";
		this.lbTo.Size = new System.Drawing.Size(37, 18);
		this.lbTo.Text = "إلى:";

		// dtTo
		this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtTo.Location = new System.Drawing.Point(195, 28);
		this.dtTo.Name = "dtTo";
		this.dtTo.Size = new System.Drawing.Size(120, 26);

		// btnSearch
		this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
		this.btnSearch.ForeColor = System.Drawing.Color.White;
		this.btnSearch.Location = new System.Drawing.Point(100, 25);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.Size = new System.Drawing.Size(85, 32);
		this.btnSearch.Text = "بحث";
		this.btnSearch.UseVisualStyleBackColor = false;
		this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

		// btnReset
		this.btnReset.Location = new System.Drawing.Point(15, 25);
		this.btnReset.Name = "btnReset";
		this.btnReset.Size = new System.Drawing.Size(80, 32);
		this.btnReset.Text = "تحديث";
		this.btnReset.UseVisualStyleBackColor = true;
		this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

		// dgvReceipts
		this.dgvReceipts.AllowUserToAddRows = false;
		this.dgvReceipts.AllowUserToDeleteRows = false;
		this.dgvReceipts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvReceipts.BackgroundColor = System.Drawing.Color.White;
		this.dgvReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvReceipts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "ID", Visible = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "رقم الإيصال", Width = 110 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "النوع", Width = 70 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "التاريخ والوقت", Width = 130 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "نقطة البيع", Width = 110 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "العميل", Width = 140 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الصافي", Width = 90 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الضريبة", Width = 80 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "حالة الإرسال", Width = 110 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "UUID", Visible = false }
		});
		this.dgvReceipts.Location = new System.Drawing.Point(12, 95);
		this.dgvReceipts.MultiSelect = false;
		this.dgvReceipts.Name = "dgvReceipts";
		this.dgvReceipts.ReadOnly = true;
		this.dgvReceipts.RowHeadersWidth = 25;
		this.dgvReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvReceipts.Size = new System.Drawing.Size(860, 320);
		this.dgvReceipts.TabIndex = 1;
		this.dgvReceipts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellDoubleClick);

		// btnSelect
		this.btnSelect.BackColor = System.Drawing.Color.ForestGreen;
		this.btnSelect.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold);
		this.btnSelect.ForeColor = System.Drawing.Color.White;
		this.btnSelect.Location = new System.Drawing.Point(460, 425);
		this.btnSelect.Name = "btnSelect";
		this.btnSelect.Size = new System.Drawing.Size(190, 38);
		this.btnSelect.Text = "اختيار الإيصال لاسترجاعه";
		this.btnSelect.UseVisualStyleBackColor = false;
		this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

		// btnCancel
		this.btnCancel.BackColor = System.Drawing.Color.Gray;
		this.btnCancel.ForeColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(340, 425);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(100, 38);
		this.btnCancel.Text = "إلغاء";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

		// frmReceiptSearch
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.ClientSize = new System.Drawing.Size(884, 475);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnSelect);
		this.Controls.Add(this.dgvReceipts);
		this.Controls.Add(this.grpSearch);
		this.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.Name = "frmReceiptSearch";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "البحث في إيصالات البيع السابقة";
		this.grpSearch.ResumeLayout(false);
		this.grpSearch.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
		this.ResumeLayout(false);
	}
}
