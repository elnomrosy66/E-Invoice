using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmAccounts : Master
{
	public Account account;

	public AccountType _AccountType;

	private IContainer components = null;

	private TextBoxEx txtTaxReg;

	private Button BtnSearchAccount;

	private TextBoxEx txtCommercialRegNo;

	private TextBoxEx txtCountry;

	private TextBoxEx txtGovernate;

	private TextBoxEx txtRegionCity;

	private TextBoxEx txtStreet;

	private TextBoxEx txtBuildingNumber;

	private TextBoxEx txtName;

	private LabelEx lbTaxReg;

	private LabelEx lbCommercialRegNo;

	private LabelEx lbCountry;

	private LabelEx lbGovernate;

	private LabelEx lbRegionCity;

	private LabelEx lbStreet;

	private LabelEx lbBuildingNumber;

	private LabelEx lbName;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TextBoxEx txtEmail;

	private LabelEx labelEx4;

	private TextBoxEx txtPhone;

	private LabelEx labelEx3;

	private TextBoxEx txtAddress;

	private LabelEx labelEx2;

	private TabPage tabPage2;

	private LabelEx labelAccount;

	private ComboBoxEx combCanonicalType;

	public frmAccounts(AccountType accountType)
	{
		_AccountType = accountType;
		InitializeComponent();
		New();
		Refresh();
	}

	public override void Refresh()
	{
		FillMaster(_unitOfWork.Accounts.GetAllBy((Account x) => (int)x.AccountType == (int)_AccountType));
		Fill(combCanonicalType, Info.CanonicalTypesList);
		base.Refresh();
	}

	public override void New()
	{
		account = new Account
		{
			Country = "EG",
			CanonicalType = CanonicalType.B,
			RegionCity = " ",
			Governate = " ",
			BuildingNumber = " ",
			Street = " "
		};
		base.New();
	}

	public override void GetData()
	{
		txtName.Text = account.Name;
		txtCountry.Text = account.Country;
		txtGovernate.Text = account.Governate;
		txtRegionCity.Text = account.RegionCity;
		txtStreet.Text = account.Street;
		txtBuildingNumber.Text = account.BuildingNumber;
		txtCommercialRegNo.Text = account.CommercialRegNo;
		txtTaxReg.Text = account.TaxReg;
		txtAddress.Text = account.Address;
		txtEmail.Text = account.Email;
		txtPhone.Text = account.Phone;
		combCanonicalType.SelectedValue = (int)account.CanonicalType;
		base.GetData();
	}

	public override void SetData()
	{
		account.Name = txtName.Text;
		account.Country = txtCountry.Text;
		account.Governate = txtGovernate.Text;
		account.RegionCity = txtRegionCity.Text;
		account.Street = txtStreet.Text;
		account.BuildingNumber = txtBuildingNumber.Text;
		account.CommercialRegNo = txtCommercialRegNo.Text;
		account.TaxReg = txtTaxReg.Text;
		account.AccountType = _AccountType;
		account.Address = txtAddress.Text;
		account.Email = txtEmail.Text;
		account.Phone = txtPhone.Text;
		account.CanonicalType = (CanonicalType)combCanonicalType.SelectedValue;
		base.SetData();
	}

	public override void Save()
	{
		if (txtTaxReg.Text == string.Empty)
		{
			Mess.Warning("يجب ادخال رقم التسجيل الضريبي و في حالة ان العميل فرد وليس شركة يتم ادخال الرقم القومي او نترك مسافة  فقط");
			return;
		}
		SetData();
		if (account.Id == 0)
		{
			_unitOfWork.Accounts.Add(account);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Accounts.Update(account);
			Mess.Update();
		}
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (account != null && Mess.AskDelete() == DialogResult.Yes)
		{
			account.IsDelete = IsDelete.Deleted;
			_unitOfWork.Accounts.Update(account);
			Mess.Delete();
		}
		base.Delete();
	}

	private void frmAccounts_Load(object sender, EventArgs e)
	{
		if (_AccountType == AccountType.Customer)
		{
			Text = "العملاء";
		}
		else
		{
			Text = "الموردون";
		}
	}

	private void BtnSearchAccount_Click(object sender, EventArgs e)
	{
		Info._SelectedAccount = 0;
		frmAccountSearch frmAccountSearch2 = new frmAccountSearch(_AccountType);
		if (frmAccountSearch2.ShowDialog() == DialogResult.OK && Info._SelectedAccount != 0)
		{
			account = _unitOfWork.Accounts.GetTById(Info._SelectedAccount);
			if (account != null)
			{
				GetData();
			}
		}
	}

	public override void GetSelectedItem()
	{
		Account account = (Account)MasterCompo.ComboBox.SelectedItem;
		if (account != null)
		{
			this.account = _unitOfWork.Accounts.GetTById(account.Id);
			GetData();
		}
	}

	private void txtEmail_TextChanged(object sender, EventArgs e)
	{
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
		this.txtTaxReg = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.BtnSearchAccount = new System.Windows.Forms.Button();
		this.txtCommercialRegNo = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtCountry = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtGovernate = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtRegionCity = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtStreet = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtBuildingNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTaxReg = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCommercialRegNo = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCountry = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbGovernate = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbRegionCity = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbStreet = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbBuildingNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.txtEmail = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx4 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPhone = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtAddress = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx2 = new E_Invoice.Desktop.Controls.LabelEx();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.labelAccount = new E_Invoice.Desktop.Controls.LabelEx();
		this.combCanonicalType = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		base.SuspendLayout();
		this.txtTaxReg.IsNumber = false;
		this.txtTaxReg.Location = new System.Drawing.Point(168, 44);
		this.txtTaxReg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtTaxReg.Name = "txtTaxReg";
		this.txtTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtTaxReg.Size = new System.Drawing.Size(357, 25);
		this.txtTaxReg.TabIndex = 22;
		this.txtCommercialRegNo.IsNumber = false;
		this.txtCommercialRegNo.Location = new System.Drawing.Point(168, 71);
		this.txtCommercialRegNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCommercialRegNo.Name = "txtCommercialRegNo";
		this.txtCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCommercialRegNo.Size = new System.Drawing.Size(357, 25);
		this.txtCommercialRegNo.TabIndex = 23;
		this.txtCountry.IsNumber = false;
		this.txtCountry.Location = new System.Drawing.Point(15, 11);
		this.txtCountry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCountry.Name = "txtCountry";
		this.txtCountry.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCountry.Size = new System.Drawing.Size(417, 25);
		this.txtCountry.TabIndex = 24;
		this.txtGovernate.IsNumber = false;
		this.txtGovernate.Location = new System.Drawing.Point(15, 40);
		this.txtGovernate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtGovernate.Name = "txtGovernate";
		this.txtGovernate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtGovernate.Size = new System.Drawing.Size(417, 25);
		this.txtGovernate.TabIndex = 25;
		this.txtRegionCity.IsNumber = false;
		this.txtRegionCity.Location = new System.Drawing.Point(15, 67);
		this.txtRegionCity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtRegionCity.Name = "txtRegionCity";
		this.txtRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtRegionCity.Size = new System.Drawing.Size(417, 25);
		this.txtRegionCity.TabIndex = 26;
		this.txtStreet.IsNumber = false;
		this.txtStreet.Location = new System.Drawing.Point(15, 95);
		this.txtStreet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtStreet.Name = "txtStreet";
		this.txtStreet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtStreet.Size = new System.Drawing.Size(417, 25);
		this.txtStreet.TabIndex = 27;
		this.txtBuildingNumber.IsNumber = false;
		this.txtBuildingNumber.Location = new System.Drawing.Point(15, 124);
		this.txtBuildingNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtBuildingNumber.Name = "txtBuildingNumber";
		this.txtBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtBuildingNumber.Size = new System.Drawing.Size(417, 25);
		this.txtBuildingNumber.TabIndex = 28;
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(168, 15);
		this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtName.Size = new System.Drawing.Size(357, 25);
		this.txtName.TabIndex = 29;
		this.BtnSearchAccount.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.BtnSearchAccount.Location = new System.Drawing.Point(138, 15);
		this.BtnSearchAccount.Size = new System.Drawing.Size(25, 25);
		this.BtnSearchAccount.TabIndex = 40;
		this.BtnSearchAccount.UseVisualStyleBackColor = true;
		this.BtnSearchAccount.Click += new System.EventHandler(this.BtnSearchAccount_Click);
		this.lbTaxReg.Location = new System.Drawing.Point(531, 44);
		this.lbTaxReg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbTaxReg.Name = "lbTaxReg";
		this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbTaxReg.Size = new System.Drawing.Size(109, 24);
		this.lbTaxReg.TabIndex = 30;
		this.lbTaxReg.Text = "سجل ضريبي";
		this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCommercialRegNo.Location = new System.Drawing.Point(534, 71);
		this.lbCommercialRegNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbCommercialRegNo.Name = "lbCommercialRegNo";
		this.lbCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCommercialRegNo.Size = new System.Drawing.Size(106, 24);
		this.lbCommercialRegNo.TabIndex = 31;
		this.lbCommercialRegNo.Text = "سجل تجاري";
		this.lbCommercialRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCountry.Location = new System.Drawing.Point(436, 10);
		this.lbCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbCountry.Name = "lbCountry";
		this.lbCountry.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCountry.Size = new System.Drawing.Size(71, 24);
		this.lbCountry.TabIndex = 32;
		this.lbCountry.Text = "كود الدولة";
		this.lbCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbGovernate.Location = new System.Drawing.Point(436, 41);
		this.lbGovernate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbGovernate.Name = "lbGovernate";
		this.lbGovernate.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbGovernate.Size = new System.Drawing.Size(62, 24);
		this.lbGovernate.TabIndex = 33;
		this.lbGovernate.Text = "المحافظة";
		this.lbGovernate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbRegionCity.Location = new System.Drawing.Point(436, 67);
		this.lbRegionCity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbRegionCity.Name = "lbRegionCity";
		this.lbRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbRegionCity.Size = new System.Drawing.Size(62, 24);
		this.lbRegionCity.TabIndex = 34;
		this.lbRegionCity.Text = "المدينة";
		this.lbRegionCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbStreet.Location = new System.Drawing.Point(436, 95);
		this.lbStreet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbStreet.Name = "lbStreet";
		this.lbStreet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbStreet.Size = new System.Drawing.Size(62, 24);
		this.lbStreet.TabIndex = 35;
		this.lbStreet.Text = "الشارع";
		this.lbStreet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbBuildingNumber.Location = new System.Drawing.Point(436, 124);
		this.lbBuildingNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbBuildingNumber.Name = "lbBuildingNumber";
		this.lbBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbBuildingNumber.Size = new System.Drawing.Size(62, 24);
		this.lbBuildingNumber.TabIndex = 36;
		this.lbBuildingNumber.Text = "المبني";
		this.lbBuildingNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbName.Location = new System.Drawing.Point(536, 19);
		this.lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(105, 24);
		this.lbName.TabIndex = 37;
		this.lbName.Text = "اسم الجهة/الشخص";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 27);
		this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.RightToLeftLayout = true;
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(657, 274);
		this.tabControl1.TabIndex = 39;
		this.tabPage1.Controls.Add(this.txtEmail);
		this.tabPage1.Controls.Add(this.txtTaxReg);
		this.tabPage1.Controls.Add(this.labelEx4);
		this.tabPage1.Controls.Add(this.txtCommercialRegNo);
		this.tabPage1.Controls.Add(this.txtPhone);
		this.tabPage1.Controls.Add(this.labelEx3);
		this.tabPage1.Controls.Add(this.txtAddress);
		this.tabPage1.Controls.Add(this.labelEx2);
		this.tabPage1.Controls.Add(this.lbName);
		this.tabPage1.Controls.Add(this.txtName);
		this.tabPage1.Controls.Add(this.BtnSearchAccount);
		this.tabPage1.Controls.Add(this.lbCommercialRegNo);
		this.tabPage1.Controls.Add(this.lbTaxReg);
		this.tabPage1.Location = new System.Drawing.Point(4, 26);
		this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.tabPage1.Size = new System.Drawing.Size(649, 244);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "بيانات الشركة";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.txtEmail.IsNumber = false;
		this.txtEmail.Location = new System.Drawing.Point(168, 155);
		this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtEmail.Size = new System.Drawing.Size(357, 25);
		this.txtEmail.TabIndex = 23;
		this.txtEmail.TextChanged += new System.EventHandler(txtEmail_TextChanged);
		this.labelEx4.Location = new System.Drawing.Point(531, 153);
		this.labelEx4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx4.Name = "labelEx4";
		this.labelEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx4.Size = new System.Drawing.Size(108, 24);
		this.labelEx4.TabIndex = 31;
		this.labelEx4.Text = "البريد الالكتروني";
		this.labelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtPhone.IsNumber = false;
		this.txtPhone.Location = new System.Drawing.Point(168, 128);
		this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtPhone.Name = "txtPhone";
		this.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtPhone.Size = new System.Drawing.Size(357, 25);
		this.txtPhone.TabIndex = 23;
		this.labelEx3.Location = new System.Drawing.Point(531, 125);
		this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(108, 24);
		this.labelEx3.TabIndex = 31;
		this.labelEx3.Text = "رقم التليفون";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtAddress.IsNumber = false;
		this.txtAddress.Location = new System.Drawing.Point(168, 99);
		this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtAddress.Name = "txtAddress";
		this.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtAddress.Size = new System.Drawing.Size(357, 25);
		this.txtAddress.TabIndex = 23;
		this.labelEx2.Location = new System.Drawing.Point(529, 97);
		this.labelEx2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx2.Name = "labelEx2";
		this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx2.Size = new System.Drawing.Size(108, 24);
		this.labelEx2.TabIndex = 31;
		this.labelEx2.Text = "العنوان";
		this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabPage2.Controls.Add(this.labelAccount);
		this.tabPage2.Controls.Add(this.combCanonicalType);
		this.tabPage2.Controls.Add(this.txtRegionCity);
		this.tabPage2.Controls.Add(this.txtCountry);
		this.tabPage2.Controls.Add(this.lbBuildingNumber);
		this.tabPage2.Controls.Add(this.txtGovernate);
		this.tabPage2.Controls.Add(this.lbStreet);
		this.tabPage2.Controls.Add(this.lbRegionCity);
		this.tabPage2.Controls.Add(this.txtStreet);
		this.tabPage2.Controls.Add(this.lbGovernate);
		this.tabPage2.Controls.Add(this.txtBuildingNumber);
		this.tabPage2.Controls.Add(this.lbCountry);
		this.tabPage2.Location = new System.Drawing.Point(4, 26);
		this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.tabPage2.Size = new System.Drawing.Size(649, 247);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "الفاتورة الالكترونية";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.labelAccount.Location = new System.Drawing.Point(440, 151);
		this.labelAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelAccount.Name = "labelAccount";
		this.labelAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelAccount.Size = new System.Drawing.Size(59, 23);
		this.labelAccount.TabIndex = 38;
		this.labelAccount.Text = "النوع";
		this.labelAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.combCanonicalType.Location = new System.Drawing.Point(15, 151);
		this.combCanonicalType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.combCanonicalType.Name = "combCanonicalType";
		this.combCanonicalType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.combCanonicalType.Size = new System.Drawing.Size(417, 25);
		this.combCanonicalType.TabIndex = 37;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(657, 301);
		base.Controls.Add(this.tabControl1);
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.Name = "frmAccounts";
		this.Text = "العملاء";
		base.Load += new System.EventHandler(frmAccounts_Load);
		base.Controls.SetChildIndex(this.tabControl1, 0);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
