using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Helpers;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmPosDevices : Master
{
	public PosDevice posDevice;

	private IContainer components = null;

	private GroupBox grpGeneral;
	private TextBoxEx txtPosName;
	private TextBoxEx txtPosCode;
	private ComboBoxEx combBranch;
	private LabelEx lbBranch;
	private LabelEx lbPosName;
	private LabelEx lbPosCode;

	private GroupBox grpSequence;
	private TextBoxEx txtPrefix;
	private TextBoxEx txtSequence;
	private LabelEx lbPrefix;
	private LabelEx lbSequence;

	private GroupBox grpTax;
	private TextBoxEx txtSerialNumber;
	private TextBoxEx txtOSVersion;
	private TextBoxEx txtModel;
	private TextBoxEx txtActivityCode;
	private CheckBox chkIsProduction;
	private CheckBox chkIsActive;
	private LabelEx lbSerialNumber;
	private LabelEx lbOSVersion;
	private LabelEx lbModel;
	private LabelEx lbActivityCode;

	public frmPosDevices()
	{
		InitializeComponent();
		UITheme.ApplyTheme(this);
		FillBranches();
		New();
		Refresh();
	}

	private void FillBranches()
	{
		combBranch.DataSource = _unitOfWork.Branchs.GetAll();
		combBranch.DisplayMember = "Name";
		combBranch.ValueMember = "Id";
	}

	public override void Refresh()
	{
		FillMaster(_unitOfWork.PosDevices.GetAll());
		base.Refresh();
	}

	public override void New()
	{
		posDevice = new PosDevice();
		if (combBranch.Items.Count > 0)
			combBranch.SelectedIndex = 0;
		txtPosName.Text = "";
		txtPosCode.Text = "";
		txtSerialNumber.Text = "";
		txtOSVersion.Text = "Windows 10";
		txtModel.Text = "Desktop POS";
		txtActivityCode.Text = "";
		txtPrefix.Text = "REC-";
		txtSequence.Text = "1";
		chkIsProduction.Checked = false;
		chkIsActive.Checked = true;
		base.New();
	}

	public override void GetData()
	{
		if (posDevice != null)
		{
			txtPosName.Text = posDevice.PosName;
			txtPosCode.Text = posDevice.PosCode;
			combBranch.SelectedValue = posDevice.BranchId;
			txtSerialNumber.Text = posDevice.DeviceSerialNumber;
			txtOSVersion.Text = posDevice.DeviceOSVersion;
			txtModel.Text = posDevice.DeviceModel;
			txtActivityCode.Text = posDevice.ActivityCode;
			txtPrefix.Text = posDevice.ReceiptPrefix;
			txtSequence.Text = posDevice.CurrentSequence.ToString();
			chkIsProduction.Checked = posDevice.IsProduction;
			chkIsActive.Checked = posDevice.IsActive;
		}
		base.GetData();
	}

	public override void SetData()
	{
		posDevice.PosName = txtPosName.Text.Trim();
		posDevice.PosCode = txtPosCode.Text.Trim();
		if (combBranch.SelectedValue != null)
		{
			posDevice.BranchId = Convert.ToInt32(combBranch.SelectedValue);
		}
		posDevice.DeviceSerialNumber = txtSerialNumber.Text.Trim();
		posDevice.DeviceOSVersion = txtOSVersion.Text.Trim();
		posDevice.DeviceModel = txtModel.Text.Trim();
		posDevice.ActivityCode = txtActivityCode.Text.Trim();
		posDevice.ReceiptPrefix = string.IsNullOrEmpty(txtPrefix.Text.Trim()) ? "REC-" : txtPrefix.Text.Trim();
		if (long.TryParse(txtSequence.Text, out long seq))
		{
			posDevice.CurrentSequence = seq;
		}
		posDevice.IsProduction = chkIsProduction.Checked;
		posDevice.IsActive = chkIsActive.Checked;
		base.SetData();
	}

	public override void Save()
	{
		if (string.IsNullOrWhiteSpace(txtPosName.Text) || string.IsNullOrWhiteSpace(txtPosCode.Text))
		{
			MessageBox.Show("يرجى إدخال اسم وكود نقطة البيع", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		SetData();
		if (posDevice.Id == 0)
		{
			_unitOfWork.PosDevices.Add(posDevice);
			Mess.Save();
		}
		else
		{
			_unitOfWork.PosDevices.Update(posDevice);
			Mess.Update();
		}
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (posDevice != null && posDevice.Id > 0 && Mess.AskDelete() == DialogResult.Yes)
		{
			posDevice.IsDelete = IsDelete.Deleted;
			_unitOfWork.PosDevices.Update(posDevice);
			Mess.Delete();
			Refresh();
			New();
		}
		base.Delete();
	}

	public override void GetSelectedItem()
	{
		if (MasterCompo.SelectedItem is PosDevice selected)
		{
			posDevice = selected;
			GetData();
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
		this.grpGeneral = new System.Windows.Forms.GroupBox();
		this.lbBranch = new E_Invoice.Desktop.Controls.LabelEx();
		this.combBranch = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.lbPosName = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPosName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbPosCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPosCode = new E_Invoice.Desktop.Controls.TextBoxEx();

		this.grpSequence = new System.Windows.Forms.GroupBox();
		this.lbPrefix = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPrefix = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbSequence = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtSequence = new E_Invoice.Desktop.Controls.TextBoxEx();

		this.grpTax = new System.Windows.Forms.GroupBox();
		this.lbSerialNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtSerialNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbOSVersion = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtOSVersion = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbModel = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtModel = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbActivityCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtActivityCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.chkIsProduction = new System.Windows.Forms.CheckBox();
		this.chkIsActive = new System.Windows.Forms.CheckBox();

		this.grpGeneral.SuspendLayout();
		this.grpSequence.SuspendLayout();
		this.grpTax.SuspendLayout();
		base.SuspendLayout();

		// grpGeneral
		this.grpGeneral.BackColor = System.Drawing.Color.White;
		this.grpGeneral.Controls.Add(this.combBranch);
		this.grpGeneral.Controls.Add(this.lbBranch);
		this.grpGeneral.Controls.Add(this.txtPosName);
		this.grpGeneral.Controls.Add(this.lbPosName);
		this.grpGeneral.Controls.Add(this.txtPosCode);
		this.grpGeneral.Controls.Add(this.lbPosCode);
		this.grpGeneral.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpGeneral.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpGeneral.Location = new System.Drawing.Point(12, 40);
		this.grpGeneral.Name = "grpGeneral";
		this.grpGeneral.Size = new System.Drawing.Size(715, 140);
		this.grpGeneral.TabIndex = 1;
		this.grpGeneral.TabStop = false;
		this.grpGeneral.Text = "بيانات نقطة البيع الأساسية";

		// lbBranch
		this.lbBranch.AutoSize = true;
		this.lbBranch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBranch.Location = new System.Drawing.Point(620, 30);
		this.lbBranch.Name = "lbBranch";
		this.lbBranch.Size = new System.Drawing.Size(81, 21);
		this.lbBranch.Text = "الفرع التابع:";

		// combBranch
		this.combBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combBranch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.combBranch.FormattingEnabled = true;
		this.combBranch.Location = new System.Drawing.Point(20, 26);
		this.combBranch.Name = "combBranch";
		this.combBranch.Size = new System.Drawing.Size(590, 29);

		// lbPosName
		this.lbPosName.AutoSize = true;
		this.lbPosName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbPosName.Location = new System.Drawing.Point(620, 68);
		this.lbPosName.Name = "lbPosName";
		this.lbPosName.Size = new System.Drawing.Size(78, 21);
		this.lbPosName.Text = "اسم النقطة:";

		// txtPosName
		this.txtPosName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtPosName.Location = new System.Drawing.Point(20, 64);
		this.txtPosName.Name = "txtPosName";
		this.txtPosName.Size = new System.Drawing.Size(590, 29);

		// lbPosCode
		this.lbPosCode.AutoSize = true;
		this.lbPosCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbPosCode.Location = new System.Drawing.Point(620, 104);
		this.lbPosCode.Name = "lbPosCode";
		this.lbPosCode.Size = new System.Drawing.Size(75, 21);
		this.lbPosCode.Text = "كود النقطة:";

		// txtPosCode
		this.txtPosCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtPosCode.Location = new System.Drawing.Point(20, 100);
		this.txtPosCode.Name = "txtPosCode";
		this.txtPosCode.Size = new System.Drawing.Size(590, 29);

		// grpSequence
		this.grpSequence.BackColor = System.Drawing.Color.White;
		this.grpSequence.Controls.Add(this.txtPrefix);
		this.grpSequence.Controls.Add(this.lbPrefix);
		this.grpSequence.Controls.Add(this.txtSequence);
		this.grpSequence.Controls.Add(this.lbSequence);
		this.grpSequence.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpSequence.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpSequence.Location = new System.Drawing.Point(12, 188);
		this.grpSequence.Name = "grpSequence";
		this.grpSequence.Size = new System.Drawing.Size(715, 75);
		this.grpSequence.TabIndex = 2;
		this.grpSequence.TabStop = false;
		this.grpSequence.Text = "نظام الترقيم والتسلسل المنفصل للإيصالات";

		// lbPrefix
		this.lbPrefix.AutoSize = true;
		this.lbPrefix.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbPrefix.Location = new System.Drawing.Point(605, 32);
		this.lbPrefix.Name = "lbPrefix";
		this.lbPrefix.Size = new System.Drawing.Size(95, 21);
		this.lbPrefix.Text = "بادئة الإيصال:";

		// txtPrefix
		this.txtPrefix.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.txtPrefix.Location = new System.Drawing.Point(375, 28);
		this.txtPrefix.Name = "txtPrefix";
		this.txtPrefix.Size = new System.Drawing.Size(225, 29);

		// lbSequence
		this.lbSequence.AutoSize = true;
		this.lbSequence.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbSequence.Location = new System.Drawing.Point(235, 32);
		this.lbSequence.Name = "lbSequence";
		this.lbSequence.Size = new System.Drawing.Size(94, 21);
		this.lbSequence.Text = "الرقم التسلسلي:";

		// txtSequence
		this.txtSequence.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.txtSequence.IsNumber = true;
		this.txtSequence.Location = new System.Drawing.Point(20, 28);
		this.txtSequence.Name = "txtSequence";
		this.txtSequence.Size = new System.Drawing.Size(205, 29);

		// grpTax
		this.grpTax.BackColor = System.Drawing.Color.White;
		this.grpTax.Controls.Add(this.chkIsActive);
		this.grpTax.Controls.Add(this.chkIsProduction);
		this.grpTax.Controls.Add(this.txtSerialNumber);
		this.grpTax.Controls.Add(this.lbSerialNumber);
		this.grpTax.Controls.Add(this.txtOSVersion);
		this.grpTax.Controls.Add(this.lbOSVersion);
		this.grpTax.Controls.Add(this.txtModel);
		this.grpTax.Controls.Add(this.lbModel);
		this.grpTax.Controls.Add(this.txtActivityCode);
		this.grpTax.Controls.Add(this.lbActivityCode);
		this.grpTax.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpTax.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpTax.Location = new System.Drawing.Point(12, 270);
		this.grpTax.Name = "grpTax";
		this.grpTax.Size = new System.Drawing.Size(715, 185);
		this.grpTax.TabIndex = 3;
		this.grpTax.TabStop = false;
		this.grpTax.Text = "بيانات الربط مع مصلحة الضرائب والجهاز";

		// lbSerialNumber
		this.lbSerialNumber.AutoSize = true;
		this.lbSerialNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbSerialNumber.Location = new System.Drawing.Point(600, 30);
		this.lbSerialNumber.Name = "lbSerialNumber";
		this.lbSerialNumber.Size = new System.Drawing.Size(100, 21);
		this.lbSerialNumber.Text = "سيريال الجهاز:";

		// txtSerialNumber
		this.txtSerialNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtSerialNumber.Location = new System.Drawing.Point(20, 26);
		this.txtSerialNumber.Name = "txtSerialNumber";
		this.txtSerialNumber.Size = new System.Drawing.Size(570, 29);

		// lbOSVersion
		this.lbOSVersion.AutoSize = true;
		this.lbOSVersion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbOSVersion.Location = new System.Drawing.Point(600, 68);
		this.lbOSVersion.Name = "lbOSVersion";
		this.lbOSVersion.Size = new System.Drawing.Size(95, 21);
		this.lbOSVersion.Text = "نظام التشغيل:";

		// txtOSVersion
		this.txtOSVersion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtOSVersion.Location = new System.Drawing.Point(375, 64);
		this.txtOSVersion.Name = "txtOSVersion";
		this.txtOSVersion.Size = new System.Drawing.Size(215, 29);

		// lbModel
		this.lbModel.AutoSize = true;
		this.lbModel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbModel.Location = new System.Drawing.Point(240, 68);
		this.lbModel.Name = "lbModel";
		this.lbModel.Size = new System.Drawing.Size(89, 21);
		this.lbModel.Text = "طراز الجهاز:";

		// txtModel
		this.txtModel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtModel.Location = new System.Drawing.Point(20, 64);
		this.txtModel.Name = "txtModel";
		this.txtModel.Size = new System.Drawing.Size(205, 29);

		// lbActivityCode
		this.lbActivityCode.AutoSize = true;
		this.lbActivityCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbActivityCode.Location = new System.Drawing.Point(600, 104);
		this.lbActivityCode.Name = "lbActivityCode";
		this.lbActivityCode.Size = new System.Drawing.Size(87, 21);
		this.lbActivityCode.Text = "كود النشاط:";

		// txtActivityCode
		this.txtActivityCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtActivityCode.Location = new System.Drawing.Point(20, 100);
		this.txtActivityCode.Name = "txtActivityCode";
		this.txtActivityCode.Size = new System.Drawing.Size(570, 29);

		// chkIsProduction
		this.chkIsProduction.AutoSize = true;
		this.chkIsProduction.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.chkIsProduction.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
		this.chkIsProduction.Location = new System.Drawing.Point(400, 142);
		this.chkIsProduction.Name = "chkIsProduction";
		this.chkIsProduction.Size = new System.Drawing.Size(186, 25);
		this.chkIsProduction.TabIndex = 8;
		this.chkIsProduction.Text = "بيئة إنتاج فعلية (Live)";
		this.chkIsProduction.UseVisualStyleBackColor = true;

		// chkIsActive
		this.chkIsActive.AutoSize = true;
		this.chkIsActive.Checked = true;
		this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.chkIsActive.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
		this.chkIsActive.Location = new System.Drawing.Point(180, 142);
		this.chkIsActive.Name = "chkIsActive";
		this.chkIsActive.Size = new System.Drawing.Size(155, 25);
		this.chkIsActive.TabIndex = 9;
		this.chkIsActive.Text = "نقطة بيع نشطة ✔";
		this.chkIsActive.UseVisualStyleBackColor = true;

		// frmPosDevices
		base.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
		base.ClientSize = new System.Drawing.Size(740, 470);
		base.Controls.Add(this.grpTax);
		base.Controls.Add(this.grpSequence);
		base.Controls.Add(this.grpGeneral);
		base.Name = "frmPosDevices";
		this.Text = "إدارة نقاط البيع والكاشير (POS Devices)";
		this.grpGeneral.ResumeLayout(false);
		this.grpGeneral.PerformLayout();
		this.grpSequence.ResumeLayout(false);
		this.grpSequence.PerformLayout();
		this.grpTax.ResumeLayout(false);
		this.grpTax.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
