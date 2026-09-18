using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmReceiptSearch : Form
{
	private IContainer components = null;
	private UnitOfWork _unitOfWork;

	private Panel pnlHeader;
	private Label lbHeaderTitle;
	private Label lbSubtitle;

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
		UITheme.ApplyTheme(this);
		UITheme.ApplyGridTheme(dgvReceipts);
		ApplyStyles();
		dtFrom.Value = DateTime.Today.AddDays(-30);
		dtTo.Value = DateTime.Today.AddDays(1);
		LoadReceipts();
	}

	private void ApplyStyles()
	{
		UITheme.ApplySuccessButton(btnSelect);
		UITheme.ApplyDangerButton(btnCancel);
		UITheme.ApplyPrimaryButton(btnSearch);
		UITheme.ApplyNeutralButton(btnReset);

		if (dgvReceipts.Columns.Count >= 9)
		{
			dgvReceipts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvReceipts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvReceipts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvReceipts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvReceipts.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvReceipts.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvReceipts.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvReceipts.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

			dgvReceipts.Columns[1].DefaultCellStyle.Font = UITheme.BoldFont;
			dgvReceipts.Columns[6].DefaultCellStyle.Font = UITheme.BoldFont;
			dgvReceipts.Columns[6].DefaultCellStyle.ForeColor = Color.FromArgb(5, 150, 105);
		}
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
				string statusText = r.sent == OrderEinvSend.Sent ? "🟢 مرسل للضرائب" : "⚪ محلي (غير مرسل)";

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
		this.pnlHeader = new System.Windows.Forms.Panel();
		this.lbSubtitle = new System.Windows.Forms.Label();
		this.lbHeaderTitle = new System.Windows.Forms.Label();

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

		this.pnlHeader.SuspendLayout();
		this.grpSearch.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
		this.SuspendLayout();

		// pnlHeader
		this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.pnlHeader.Controls.Add(this.lbSubtitle);
		this.pnlHeader.Controls.Add(this.lbHeaderTitle);
		this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlHeader.Location = new System.Drawing.Point(0, 0);
		this.pnlHeader.Name = "pnlHeader";
		this.pnlHeader.Size = new System.Drawing.Size(920, 50);
		this.pnlHeader.TabIndex = 0;

		// lbHeaderTitle
		this.lbHeaderTitle.AutoSize = true;
		this.lbHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
		this.lbHeaderTitle.ForeColor = System.Drawing.Color.White;
		this.lbHeaderTitle.Location = new System.Drawing.Point(625, 11);
		this.lbHeaderTitle.Name = "lbHeaderTitle";
		this.lbHeaderTitle.Size = new System.Drawing.Size(280, 28);
		this.lbHeaderTitle.Text = "البحث في إيصالات البيع السابقة";

		// lbSubtitle
		this.lbSubtitle.AutoSize = true;
		this.lbSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
		this.lbSubtitle.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
		this.lbSubtitle.Location = new System.Drawing.Point(15, 15);
		this.lbSubtitle.Name = "lbSubtitle";
		this.lbSubtitle.Size = new System.Drawing.Size(340, 20);
		this.lbSubtitle.Text = "انقر نقراً مزدوجاً على أي إيصال لاسترجاعه أو تعديله";

		// grpSearch
		this.grpSearch.BackColor = System.Drawing.Color.White;
		this.grpSearch.Controls.Add(this.btnReset);
		this.grpSearch.Controls.Add(this.btnSearch);
		this.grpSearch.Controls.Add(this.dtTo);
		this.grpSearch.Controls.Add(this.lbTo);
		this.grpSearch.Controls.Add(this.dtFrom);
		this.grpSearch.Controls.Add(this.lbFrom);
		this.grpSearch.Controls.Add(this.txtSearchKeyword);
		this.grpSearch.Controls.Add(this.lbSearchKeyword);
		this.grpSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpSearch.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpSearch.Location = new System.Drawing.Point(12, 58);
		this.grpSearch.Name = "grpSearch";
		this.grpSearch.Size = new System.Drawing.Size(896, 75);
		this.grpSearch.TabIndex = 1;
		this.grpSearch.TabStop = false;
		this.grpSearch.Text = "خيارات البحث والتصفية";

		// lbSearchKeyword
		this.lbSearchKeyword.AutoSize = true;
		this.lbSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbSearchKeyword.Location = new System.Drawing.Point(815, 32);
		this.lbSearchKeyword.Name = "lbSearchKeyword";
		this.lbSearchKeyword.Size = new System.Drawing.Size(73, 21);
		this.lbSearchKeyword.Text = "بحث عام:";

		// txtSearchKeyword
		this.txtSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtSearchKeyword.Location = new System.Drawing.Point(565, 28);
		this.txtSearchKeyword.Name = "txtSearchKeyword";
		this.txtSearchKeyword.Size = new System.Drawing.Size(245, 29);
		this.txtSearchKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchKeyword_KeyDown);

		// lbFrom
		this.lbFrom.AutoSize = true;
		this.lbFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbFrom.Location = new System.Drawing.Point(520, 32);
		this.lbFrom.Name = "lbFrom";
		this.lbFrom.Size = new System.Drawing.Size(35, 21);
		this.lbFrom.Text = "من:";

		// dtFrom
		this.dtFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtFrom.Location = new System.Drawing.Point(395, 28);
		this.dtFrom.Name = "dtFrom";
		this.dtFrom.Size = new System.Drawing.Size(120, 29);

		// lbTo
		this.lbTo.AutoSize = true;
		this.lbTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbTo.Location = new System.Drawing.Point(350, 32);
		this.lbTo.Name = "lbTo";
		this.lbTo.Size = new System.Drawing.Size(39, 21);
		this.lbTo.Text = "إلى:";

		// dtTo
		this.dtTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtTo.Location = new System.Drawing.Point(225, 28);
		this.dtTo.Name = "dtTo";
		this.dtTo.Size = new System.Drawing.Size(120, 29);

		// btnSearch
		this.btnSearch.Location = new System.Drawing.Point(115, 25);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.Size = new System.Drawing.Size(95, 34);
		this.btnSearch.TabIndex = 6;
		this.btnSearch.Text = "بحث 🔍";
		this.btnSearch.UseVisualStyleBackColor = false;
		this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

		// btnReset
		this.btnReset.Location = new System.Drawing.Point(15, 25);
		this.btnReset.Name = "btnReset";
		this.btnReset.Size = new System.Drawing.Size(90, 34);
		this.btnReset.TabIndex = 7;
		this.btnReset.Text = "إعادة ضبط";
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
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "رقم الإيصال", Width = 115 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "النوع", Width = 70 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "التاريخ والوقت", Width = 135 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "نقطة البيع", Width = 115 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "اسم العميل", Width = 150 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الصافي (ج.م)", Width = 95 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الضريبة", Width = 85 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "حالة الإرسال", Width = 130 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "UUID", Visible = false }
		});
		this.dgvReceipts.Location = new System.Drawing.Point(12, 142);
		this.dgvReceipts.MultiSelect = false;
		this.dgvReceipts.Name = "dgvReceipts";
		this.dgvReceipts.ReadOnly = true;
		this.dgvReceipts.RowHeadersWidth = 25;
		this.dgvReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvReceipts.Size = new System.Drawing.Size(896, 325);
		this.dgvReceipts.TabIndex = 2;
		this.dgvReceipts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellDoubleClick);

		// btnSelect
		this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
		this.btnSelect.Location = new System.Drawing.Point(460, 480);
		this.btnSelect.Name = "btnSelect";
		this.btnSelect.Size = new System.Drawing.Size(220, 42);
		this.btnSelect.TabIndex = 3;
		this.btnSelect.Text = "اختيار الإيصال واسترجاعه ✔";
		this.btnSelect.UseVisualStyleBackColor = false;
		this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

		// btnCancel
		this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
		this.btnCancel.Location = new System.Drawing.Point(325, 480);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 42);
		this.btnCancel.TabIndex = 4;
		this.btnCancel.Text = "إلغاء [Esc]";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

		// frmReceiptSearch
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
		this.ClientSize = new System.Drawing.Size(920, 535);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnSelect);
		this.Controls.Add(this.dgvReceipts);
		this.Controls.Add(this.grpSearch);
		this.Controls.Add(this.pnlHeader);
		this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.Name = "frmReceiptSearch";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "البحث في إيصالات البيع السابقة";
		this.pnlHeader.ResumeLayout(false);
		this.pnlHeader.PerformLayout();
		this.grpSearch.ResumeLayout(false);
		this.grpSearch.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
		this.ResumeLayout(false);
	}
}
