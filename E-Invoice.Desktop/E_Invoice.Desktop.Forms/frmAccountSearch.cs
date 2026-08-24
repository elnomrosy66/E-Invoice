using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmAccountSearch : A
{
	private IContainer components = null;

	public DataGridView DGVItems;

	private TextBox txtCustomerName;

	private TextBox txtCustomerRegisteration;

	private Label label1;

	private Label label2;

	private AccountType _accountType;

	public frmAccountSearch(AccountType accountType)
	{
		_accountType = accountType;
		InitializeComponent();
		txtCustomerName.TextChanged += txtCustomerName_TextChanged;
		txtCustomerRegisteration.TextChanged += txtCustomerRegisteration_TextChanged;
		DGVItems.CellDoubleClick += DGVItems_CellDoubleClick;
	}

	public frmAccountSearch()
	{
		_accountType = AccountType.Customer;
		InitializeComponent();
		txtCustomerName.TextChanged += txtCustomerName_TextChanged;
		txtCustomerRegisteration.TextChanged += txtCustomerRegisteration_TextChanged;
		DGVItems.CellDoubleClick += DGVItems_CellDoubleClick;
	}

	private void frmAccountSearch_Load(object sender, EventArgs e)
	{
		Filter();
	}

	private void txtCustomerName_TextChanged(object sender, EventArgs e)
	{
		Filter();
	}

	private void txtCustomerRegisteration_TextChanged(object sender, EventArgs e)
	{
		Filter();
	}

	private void Filter()
	{
		try
		{
			var query = _unitOfWork.Accounts.GetAllBy(x => (int)x.AccountType == (int)_accountType);
			if (!string.IsNullOrEmpty(txtCustomerName.Text))
			{
				query = query.Where(x => x.Name.Contains(txtCustomerName.Text));
			}
			if (!string.IsNullOrEmpty(txtCustomerRegisteration.Text))
			{
				query = query.Where(x => x.TaxReg.Contains(txtCustomerRegisteration.Text));
			}
			DGVItems.DataSource = (from a in query
								  select new
								  {
									  Id = a.Id,
									  Name = a.Name,
									  TaxReg = a.TaxReg,
									  RegionCity = a.RegionCity,
									  Governate = a.Governate
								  }).ToList();
		}
		catch (Exception)
		{
		}
	}

	private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			var idVal = DGVItems.Rows[e.RowIndex].Cells["Id"].Value;
			if (idVal != null && idVal is int idInt)
			{
				Info._SelectedAccount = idInt;
				DialogResult = DialogResult.OK;
				Close();
			}
		}
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVItems = new System.Windows.Forms.DataGridView();
		this.txtCustomerName = new System.Windows.Forms.TextBox();
		this.txtCustomerRegisteration = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		base.SuspendLayout();
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.AllowUserToDeleteRows = false;
		this.DGVItems.AllowUserToOrderColumns = true;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Location = new System.Drawing.Point(42, 170);
		this.DGVItems.Margin = new System.Windows.Forms.Padding(4);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(731, 230);
		this.DGVItems.TabIndex = 7;
		this.txtCustomerName.Location = new System.Drawing.Point(119, 12);
		this.txtCustomerName.Name = "txtCustomerName";
		this.txtCustomerName.Size = new System.Drawing.Size(151, 25);
		this.txtCustomerName.TabIndex = 8;
		this.txtCustomerRegisteration.Location = new System.Drawing.Point(473, 12);
		this.txtCustomerRegisteration.Name = "txtCustomerRegisteration";
		this.txtCustomerRegisteration.Size = new System.Drawing.Size(151, 25);
		this.txtCustomerRegisteration.TabIndex = 8;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(82, 18);
		this.label1.TabIndex = 9;
		this.label1.Text = "اسم العميل";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(301, 15);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(145, 18);
		this.label2.TabIndex = 9;
		this.label2.Text = "رقم التسجيل الضريبي";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtCustomerRegisteration);
		base.Controls.Add(this.txtCustomerName);
		base.Controls.Add(this.DGVItems);
		base.Name = "frmAccountSearch";
		base.Load += new System.EventHandler(frmAccountSearch_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
