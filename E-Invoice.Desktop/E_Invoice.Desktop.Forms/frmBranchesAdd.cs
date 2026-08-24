using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmBranchesAdd : Master
{
	public Branch branch;

	private IContainer components = null;

	private TextBoxEx txtName;

	private TextBoxEx txtCode;

	private LabelEx lbName;

	private LabelEx lbCode;

	private TextBoxEx txtCountry;

	private TextBoxEx txtGovernate;

	private TextBoxEx txtRegionCity;

	private LabelEx lbCountry;

	private TextBoxEx txtStreet;

	private TextBoxEx txtBuildingNumber;

	private LabelEx lbGovernate;

	private LabelEx lbRegionCity;

	private LabelEx lbBuildingNumber;

	private LabelEx lbStreet;

	public frmBranchesAdd()
	{
		InitializeComponent();
		New();
		Refresh();
	}

	public override void Refresh()
	{
		FillMaster(_unitOfWork.Branchs.GetAll());
		base.Refresh();
	}

	public override void New()
	{
		branch = new Branch();
		base.New();
	}

	public override void GetData()
	{
		txtName.Text = branch.Name;
		txtCode.Text = branch.Code;
		txtCountry.Text = branch.Country;
		txtGovernate.Text = branch.Governate;
		txtRegionCity.Text = branch.RegionCity;
		txtStreet.Text = branch.Street;
		txtBuildingNumber.Text = branch.BuildingNumber;
		base.GetData();
	}

	public override void SetData()
	{
		branch.Name = txtName.Text;
		branch.Code = txtCode.Text;
		branch.Country = txtCountry.Text;
		branch.Governate = txtGovernate.Text;
		branch.RegionCity = txtRegionCity.Text;
		branch.Street = txtStreet.Text;
		branch.BuildingNumber = txtBuildingNumber.Text;
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (branch.Id == 0)
		{
			_unitOfWork.Branchs.Add(branch);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Branchs.Update(branch);
			Mess.Update();
		}
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (branch != null && Mess.AskDelete() == DialogResult.Yes)
		{
			branch.IsDelete = IsDelete.Deleted;
			_unitOfWork.Branchs.Update(branch);
			Mess.Delete();
			base.Delete();
		}
	}

	public override void GetSelectedItem()
	{
		Branch branch = (Branch)MasterCompo.ComboBox.SelectedItem;
		if (branch != null)
		{
			this.branch = _unitOfWork.Branchs.GetTById(branch.Id);
			GetData();
		}
	}

	private void frmBranchesAdd_Load(object sender, EventArgs e)
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
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtCountry = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtGovernate = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtRegionCity = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbCountry = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtStreet = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtBuildingNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbGovernate = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbRegionCity = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbBuildingNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbStreet = new E_Invoice.Desktop.Controls.LabelEx();
		base.SuspendLayout();
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(106, 66);
		this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtName.Size = new System.Drawing.Size(564, 27);
		this.txtName.TabIndex = 10;
		this.txtCode.IsNumber = false;
		this.txtCode.Location = new System.Drawing.Point(106, 103);
		this.txtCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtCode.Name = "txtCode";
		this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCode.Size = new System.Drawing.Size(564, 27);
		this.txtCode.TabIndex = 11;
		this.lbName.Location = new System.Drawing.Point(14, 64);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(99, 28);
		this.lbName.TabIndex = 12;
		this.lbName.Text = "الاسم";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCode.Location = new System.Drawing.Point(14, 102);
		this.lbCode.Name = "lbCode";
		this.lbCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCode.Size = new System.Drawing.Size(99, 28);
		this.lbCode.TabIndex = 13;
		this.lbCode.Text = "كود الفرع";
		this.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCountry.IsNumber = false;
		this.txtCountry.Location = new System.Drawing.Point(106, 139);
		this.txtCountry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtCountry.Name = "txtCountry";
		this.txtCountry.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCountry.Size = new System.Drawing.Size(564, 27);
		this.txtCountry.TabIndex = 49;
		this.txtGovernate.IsNumber = false;
		this.txtGovernate.Location = new System.Drawing.Point(106, 172);
		this.txtGovernate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtGovernate.Name = "txtGovernate";
		this.txtGovernate.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtGovernate.Size = new System.Drawing.Size(564, 27);
		this.txtGovernate.TabIndex = 50;
		this.txtRegionCity.IsNumber = false;
		this.txtRegionCity.Location = new System.Drawing.Point(106, 205);
		this.txtRegionCity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtRegionCity.Name = "txtRegionCity";
		this.txtRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtRegionCity.Size = new System.Drawing.Size(564, 27);
		this.txtRegionCity.TabIndex = 51;
		this.lbCountry.Location = new System.Drawing.Point(14, 139);
		this.lbCountry.Name = "lbCountry";
		this.lbCountry.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCountry.Size = new System.Drawing.Size(135, 28);
		this.lbCountry.TabIndex = 54;
		this.lbCountry.Text = "كود الدولة";
		this.lbCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtStreet.IsNumber = false;
		this.txtStreet.Location = new System.Drawing.Point(106, 237);
		this.txtStreet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtStreet.Name = "txtStreet";
		this.txtStreet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtStreet.Size = new System.Drawing.Size(564, 27);
		this.txtStreet.TabIndex = 52;
		this.txtBuildingNumber.IsNumber = false;
		this.txtBuildingNumber.Location = new System.Drawing.Point(106, 271);
		this.txtBuildingNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtBuildingNumber.Name = "txtBuildingNumber";
		this.txtBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtBuildingNumber.Size = new System.Drawing.Size(564, 27);
		this.txtBuildingNumber.TabIndex = 53;
		this.lbGovernate.Location = new System.Drawing.Point(14, 172);
		this.lbGovernate.Name = "lbGovernate";
		this.lbGovernate.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbGovernate.Size = new System.Drawing.Size(131, 28);
		this.lbGovernate.TabIndex = 55;
		this.lbGovernate.Text = "المحافظة";
		this.lbGovernate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbRegionCity.Location = new System.Drawing.Point(14, 206);
		this.lbRegionCity.Name = "lbRegionCity";
		this.lbRegionCity.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbRegionCity.Size = new System.Drawing.Size(131, 28);
		this.lbRegionCity.TabIndex = 56;
		this.lbRegionCity.Text = "المدينة";
		this.lbRegionCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbBuildingNumber.Location = new System.Drawing.Point(10, 271);
		this.lbBuildingNumber.Name = "lbBuildingNumber";
		this.lbBuildingNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbBuildingNumber.Size = new System.Drawing.Size(131, 28);
		this.lbBuildingNumber.TabIndex = 58;
		this.lbBuildingNumber.Text = "المبني";
		this.lbBuildingNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbStreet.Location = new System.Drawing.Point(14, 237);
		this.lbStreet.Name = "lbStreet";
		this.lbStreet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbStreet.Size = new System.Drawing.Size(131, 28);
		this.lbStreet.TabIndex = 57;
		this.lbStreet.Text = "الشارع";
		this.lbStreet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(817, 377);
		base.Controls.Add(this.txtCountry);
		base.Controls.Add(this.txtGovernate);
		base.Controls.Add(this.txtRegionCity);
		base.Controls.Add(this.lbCountry);
		base.Controls.Add(this.txtStreet);
		base.Controls.Add(this.txtBuildingNumber);
		base.Controls.Add(this.lbGovernate);
		base.Controls.Add(this.lbRegionCity);
		base.Controls.Add(this.lbBuildingNumber);
		base.Controls.Add(this.lbStreet);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.txtCode);
		base.Controls.Add(this.lbName);
		base.Controls.Add(this.lbCode);
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "frmBranchesAdd";
		this.Text = "الفروع";
		base.Load += new System.EventHandler(frmBranchesAdd_Load);
		base.Controls.SetChildIndex(this.lbCode, 0);
		base.Controls.SetChildIndex(this.lbName, 0);
		base.Controls.SetChildIndex(this.txtCode, 0);
		base.Controls.SetChildIndex(this.txtName, 0);
		base.Controls.SetChildIndex(this.lbStreet, 0);
		base.Controls.SetChildIndex(this.lbBuildingNumber, 0);
		base.Controls.SetChildIndex(this.lbRegionCity, 0);
		base.Controls.SetChildIndex(this.lbGovernate, 0);
		base.Controls.SetChildIndex(this.txtBuildingNumber, 0);
		base.Controls.SetChildIndex(this.txtStreet, 0);
		base.Controls.SetChildIndex(this.lbCountry, 0);
		base.Controls.SetChildIndex(this.txtRegionCity, 0);
		base.Controls.SetChildIndex(this.txtGovernate, 0);
		base.Controls.SetChildIndex(this.txtCountry, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
