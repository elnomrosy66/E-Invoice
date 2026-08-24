using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmCompanies : Master
{
	public Company company;

	private IContainer components = null;

	private TextBoxEx txtTaxReg;

	private TextBoxEx txtCommercialRegNo;

	private TextBoxEx txtName;

	private LabelEx lbTaxReg;

	private LabelEx lbCommercialRegNo;

	private LabelEx lbName;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TextBoxEx txtEmail;

	private LabelEx labelEx4;

	private TextBoxEx txtPhone;

	private LabelEx labelEx3;

	private TextBoxEx txtAddress;

	private LabelEx labelEx2;

	private TextBoxEx txtCountry;

	private TextBoxEx txtGovernate;

	private TextBoxEx txtRegionCity;

	private LabelEx lbCountry;

	private TextBoxEx txtStreet;

	private TextBoxEx txtActCode;

	private TextBoxEx txtBuildingNumber;

	private LabelEx lbGovernate;

	private LabelEx lbRegionCity;

	private LabelEx labelEx1;

	private LabelEx lbBuildingNumber;

	private LabelEx lbStreet;

	public frmCompanies()
	{
		InitializeComponent();
		New();
	}

	public override void New()
	{
		company = ((_unitOfWork.Companies.GetAll().FirstOrDefault() == null) ? new Company() : _unitOfWork.Companies.GetAll().FirstOrDefault());
		base.New();
	}

	public override void GetData()
	{
		txtName.Text = company.Name;
		txtCountry.Text = company.Country;
		txtGovernate.Text = company.Governate;
		txtRegionCity.Text = company.RegionCity;
		txtStreet.Text = company.Street;
		txtBuildingNumber.Text = company.BuildingNumber;
		txtCommercialRegNo.Text = company.CommercialRegNo;
		txtTaxReg.Text = company.TaxReg;
		txtActCode.Text = company.ActivityCode;
		txtAddress.Text = company.Address;
		txtEmail.Text = company.Email;
		txtPhone.Text = company.Phone;
		base.GetData();
	}

	public override void SetData()
	{
		company.Name = txtName.Text;
		company.Country = txtCountry.Text;
		company.Governate = txtGovernate.Text;
		company.RegionCity = txtRegionCity.Text;
		company.Street = txtStreet.Text;
		company.BuildingNumber = txtBuildingNumber.Text;
		company.CommercialRegNo = txtCommercialRegNo.Text;
		company.TaxReg = txtTaxReg.Text;
		company.ActivityCode = txtActCode.Text;
		company.Address = txtAddress.Text;
		company.Email = txtEmail.Text;
		company.Phone = txtPhone.Text;
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (company.Id == 0)
		{
			_unitOfWork.Companies.Add(company);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Companies.Update(company);
			Mess.Update();
		}
		New();
		Info._Company = _unitOfWork.Companies.GetAll().FirstOrDefault();
		base.Save();
	}

	public override void Delete()
	{
		if (company != null)
		{
			company.IsDelete = IsDelete.Deleted;
			_unitOfWork.Companies.Update(company);
			Mess.Delete();
		}
		base.Delete();
	}

	public override void GetSelectedItem()
	{
		int id = (int)MasterCompo.ComboBox.SelectedValue;
		company = _unitOfWork.Companies.GetTById(id);
		GetData();
	}

	private void frmCompanies_Load(object sender, EventArgs e)
	{
		MasterCompo.Visible = false;
		toolStripSplitButton1.Visible = false;
		toolStripSplitButton2.Visible = false;
		toolStripButton5.Visible = false;
		toolStripButton4.Visible = false;
		toolStripTextBox1.Visible = false;
		toolStripLabel1.Visible = false;
		toolStripButton3.Visible = false;
		toolStripButton1.Visible = false;
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
		this.txtCommercialRegNo = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTaxReg = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCommercialRegNo = new E_Invoice.Desktop.Controls.LabelEx();
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
		this.txtCountry = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtGovernate = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtRegionCity = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbCountry = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtStreet = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtActCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtBuildingNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbGovernate = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbRegionCity = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbBuildingNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbStreet = new E_Invoice.Desktop.Controls.LabelEx();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		base.SuspendLayout();
		this.txtTaxReg.IsNumber = false;
		this.txtTaxReg.Location = new System.Drawing.Point(33, 56);
		this.txtTaxReg.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtTaxReg.Name = "txtTaxReg";
		this.txtTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtTaxReg.Size = new System.Drawing.Size(445, 27);
		this.txtTaxReg.TabIndex = 22;
		this.txtCommercialRegNo.IsNumber = false;
		this.txtCommercialRegNo.Location = new System.Drawing.Point(33, 94);
		this.txtCommercialRegNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtCommercialRegNo.Name = "txtCommercialRegNo";
		this.txtCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCommercialRegNo.Size = new System.Drawing.Size(445, 27);
		this.txtCommercialRegNo.TabIndex = 23;
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(33, 21);
		this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtName.Size = new System.Drawing.Size(445, 27);
		this.txtName.TabIndex = 29;
		this.lbTaxReg.Location = new System.Drawing.Point(487, 54);
		this.lbTaxReg.Name = "lbTaxReg";
		this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbTaxReg.Size = new System.Drawing.Size(135, 28);
		this.lbTaxReg.TabIndex = 30;
		this.lbTaxReg.Text = "سجل ضريبي";
		this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCommercialRegNo.Location = new System.Drawing.Point(487, 91);
		this.lbCommercialRegNo.Name = "lbCommercialRegNo";
		this.lbCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCommercialRegNo.Size = new System.Drawing.Size(135, 28);
		this.lbCommercialRegNo.TabIndex = 31;
		this.lbCommercialRegNo.Text = "سجل تجاري";
		this.lbCommercialRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbName.Location = new System.Drawing.Point(487, 18);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(135, 28);
		this.lbName.TabIndex = 37;
		this.lbName.Text = "اسم الشركة";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 29);
		this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.RightToLeftLayout = true;
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(651, 311);
		this.tabControl1.TabIndex = 38;
		this.tabPage1.Controls.Add(this.lbName);
		this.tabPage1.Controls.Add(this.txtTaxReg);
		this.tabPage1.Controls.Add(this.txtEmail);
		this.tabPage1.Controls.Add(this.labelEx4);
		this.tabPage1.Controls.Add(this.txtPhone);
		this.tabPage1.Controls.Add(this.labelEx3);
		this.tabPage1.Controls.Add(this.txtAddress);
		this.tabPage1.Controls.Add(this.labelEx2);
		this.tabPage1.Controls.Add(this.txtCommercialRegNo);
		this.tabPage1.Controls.Add(this.lbCommercialRegNo);
		this.tabPage1.Controls.Add(this.lbTaxReg);
		this.tabPage1.Controls.Add(this.txtName);
		this.tabPage1.Location = new System.Drawing.Point(4, 29);
		this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.tabPage1.Size = new System.Drawing.Size(643, 278);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "بيانات الشركة";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.txtEmail.IsNumber = false;
		this.txtEmail.Location = new System.Drawing.Point(33, 221);
		this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtEmail.Size = new System.Drawing.Size(445, 27);
		this.txtEmail.TabIndex = 23;
		this.labelEx4.Location = new System.Drawing.Point(487, 220);
		this.labelEx4.Name = "labelEx4";
		this.labelEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx4.Size = new System.Drawing.Size(135, 28);
		this.labelEx4.TabIndex = 31;
		this.labelEx4.Text = "البريد الالكتروني";
		this.labelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtPhone.IsNumber = false;
		this.txtPhone.Location = new System.Drawing.Point(33, 181);
		this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtPhone.Name = "txtPhone";
		this.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtPhone.Size = new System.Drawing.Size(445, 27);
		this.txtPhone.TabIndex = 23;
		this.labelEx3.Location = new System.Drawing.Point(487, 179);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(135, 28);
		this.labelEx3.TabIndex = 31;
		this.labelEx3.Text = "رقم التليفون";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtAddress.IsNumber = false;
		this.txtAddress.Location = new System.Drawing.Point(33, 136);
		this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtAddress.Name = "txtAddress";
		this.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtAddress.Size = new System.Drawing.Size(445, 27);
		this.txtAddress.TabIndex = 23;
		this.labelEx2.Location = new System.Drawing.Point(487, 134);
		this.labelEx2.Name = "labelEx2";
		this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx2.Size = new System.Drawing.Size(135, 28);
		this.labelEx2.TabIndex = 31;
		this.labelEx2.Text = "العنوان";
		this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabPage2.Controls.Add(this.txtCountry);
		this.tabPage2.Controls.Add(this.txtGovernate);
		this.tabPage2.Controls.Add(this.txtRegionCity);
		this.tabPage2.Controls.Add(this.lbCountry);
		this.tabPage2.Controls.Add(this.txtStreet);
		this.tabPage2.Controls.Add(this.txtActCode);
		this.tabPage2.Controls.Add(this.txtBuildingNumber);
		this.tabPage2.Controls.Add(this.lbGovernate);
		this.tabPage2.Controls.Add(this.lbRegionCity);
		this.tabPage2.Controls.Add(this.labelEx1);
		this.tabPage2.Controls.Add(this.lbBuildingNumber);
		this.tabPage2.Controls.Add(this.lbStreet);
		this.tabPage2.Location = new System.Drawing.Point(4, 29);
		this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.tabPage2.Size = new System.Drawing.Size(643, 278);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "الفاتورة الالكترونية";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.txtCountry.IsNumber = false;
		this.txtCountry.Location = new System.Drawing.Point(40, 56);
		this.txtCountry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtCountry.Name = "txtCountry";
		this.txtCountry.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCountry.Size = new System.Drawing.Size(445, 27);
		this.txtCountry.TabIndex = 37;
		this.txtGovernate.IsNumber = false;
		this.txtGovernate.Location = new System.Drawing.Point(40, 88);
		this.txtGovernate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtGovernate.Name = "txtGovernate";
		this.txtGovernate.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtGovernate.Size = new System.Drawing.Size(445, 27);
		this.txtGovernate.TabIndex = 38;
		this.txtRegionCity.IsNumber = false;
		this.txtRegionCity.Location = new System.Drawing.Point(40, 122);
		this.txtRegionCity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtRegionCity.Name = "txtRegionCity";
		this.txtRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtRegionCity.Size = new System.Drawing.Size(445, 27);
		this.txtRegionCity.TabIndex = 39;
		this.lbCountry.Location = new System.Drawing.Point(495, 52);
		this.lbCountry.Name = "lbCountry";
		this.lbCountry.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCountry.Size = new System.Drawing.Size(135, 28);
		this.lbCountry.TabIndex = 43;
		this.lbCountry.Text = "كود الدولة";
		this.lbCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtStreet.IsNumber = false;
		this.txtStreet.Location = new System.Drawing.Point(40, 154);
		this.txtStreet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtStreet.Name = "txtStreet";
		this.txtStreet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtStreet.Size = new System.Drawing.Size(445, 27);
		this.txtStreet.TabIndex = 40;
		this.txtActCode.IsNumber = false;
		this.txtActCode.Location = new System.Drawing.Point(40, 22);
		this.txtActCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtActCode.Name = "txtActCode";
		this.txtActCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtActCode.Size = new System.Drawing.Size(445, 27);
		this.txtActCode.TabIndex = 41;
		this.txtBuildingNumber.IsNumber = false;
		this.txtBuildingNumber.Location = new System.Drawing.Point(40, 187);
		this.txtBuildingNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtBuildingNumber.Name = "txtBuildingNumber";
		this.txtBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtBuildingNumber.Size = new System.Drawing.Size(445, 27);
		this.txtBuildingNumber.TabIndex = 42;
		this.lbGovernate.Location = new System.Drawing.Point(495, 86);
		this.lbGovernate.Name = "lbGovernate";
		this.lbGovernate.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbGovernate.Size = new System.Drawing.Size(131, 28);
		this.lbGovernate.TabIndex = 44;
		this.lbGovernate.Text = "المحافظة";
		this.lbGovernate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbRegionCity.Location = new System.Drawing.Point(495, 119);
		this.lbRegionCity.Name = "lbRegionCity";
		this.lbRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbRegionCity.Size = new System.Drawing.Size(131, 28);
		this.lbRegionCity.TabIndex = 45;
		this.lbRegionCity.Text = "المدينة";
		this.lbRegionCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx1.Location = new System.Drawing.Point(499, 20);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(131, 28);
		this.labelEx1.TabIndex = 47;
		this.labelEx1.Text = "كود النشاط الضريبي";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbBuildingNumber.Location = new System.Drawing.Point(491, 184);
		this.lbBuildingNumber.Name = "lbBuildingNumber";
		this.lbBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbBuildingNumber.Size = new System.Drawing.Size(131, 28);
		this.lbBuildingNumber.TabIndex = 48;
		this.lbBuildingNumber.Text = "المبني";
		this.lbBuildingNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbStreet.Location = new System.Drawing.Point(495, 152);
		this.lbStreet.Name = "lbStreet";
		this.lbStreet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbStreet.Size = new System.Drawing.Size(131, 28);
		this.lbStreet.TabIndex = 46;
		this.lbStreet.Text = "الشارع";
		this.lbStreet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(651, 340);
		base.Controls.Add(this.tabControl1);
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "frmCompanies";
		this.Text = "بيانات الشركة";
		base.Load += new System.EventHandler(frmCompanies_Load);
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
