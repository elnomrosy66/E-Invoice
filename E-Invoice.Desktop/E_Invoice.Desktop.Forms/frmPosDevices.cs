using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmPosDevices : Master
{
	public PosDevice posDevice;

	private IContainer components = null;

	private TextBoxEx txtPosName;
	private TextBoxEx txtPosCode;
	private ComboBoxEx combBranch;
	private TextBoxEx txtSerialNumber;
	private TextBoxEx txtOSVersion;
	private TextBoxEx txtModel;
	private TextBoxEx txtActivityCode;
	private TextBoxEx txtPrefix;
	private TextBoxEx txtSequence;
	private CheckBox chkIsProduction;
	private CheckBox chkIsActive;

	private LabelEx lbBranch;
	private LabelEx lbPosName;
	private LabelEx lbPosCode;
	private LabelEx lbSerialNumber;
	private LabelEx lbOSVersion;
	private LabelEx lbModel;
	private LabelEx lbActivityCode;
	private LabelEx lbPrefix;
	private LabelEx lbSequence;

	public frmPosDevices()
	{
		InitializeComponent();
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
			base.Delete();
		}
	}

	public override void GetSelectedItem()
	{
		if (MasterCompo.ComboBox.SelectedItem is PosDevice selected)
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
		this.lbBranch = new E_Invoice.Desktop.Controls.LabelEx();
		this.combBranch = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.lbPosName = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPosName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbPosCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPosCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbSerialNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtSerialNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbOSVersion = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtOSVersion = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbModel = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtModel = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbActivityCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtActivityCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbPrefix = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtPrefix = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbSequence = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtSequence = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.chkIsProduction = new System.Windows.Forms.CheckBox();
		this.chkIsActive = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();

		// lbBranch
		this.lbBranch.AutoSize = true;
		this.lbBranch.Location = new System.Drawing.Point(520, 50);
		this.lbBranch.Name = "lbBranch";
		this.lbBranch.Size = new System.Drawing.Size(43, 18);
		this.lbBranch.Text = "الفرع:";

		// combBranch
		this.combBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combBranch.FormattingEnabled = true;
		this.combBranch.Location = new System.Drawing.Point(50, 46);
		this.combBranch.Name = "combBranch";
		this.combBranch.Size = new System.Drawing.Size(450, 26);

		// lbPosName
		this.lbPosName.AutoSize = true;
		this.lbPosName.Location = new System.Drawing.Point(520, 90);
		this.lbPosName.Name = "lbPosName";
		this.lbPosName.Size = new System.Drawing.Size(107, 18);
		this.lbPosName.Text = "اسم نقطة البيع:";

		// txtPosName
		this.txtPosName.Location = new System.Drawing.Point(50, 86);
		this.txtPosName.Name = "txtPosName";
		this.txtPosName.Size = new System.Drawing.Size(450, 26);

		// lbPosCode
		this.lbPosCode.AutoSize = true;
		this.lbPosCode.Location = new System.Drawing.Point(520, 130);
		this.lbPosCode.Name = "lbPosCode";
		this.lbPosCode.Size = new System.Drawing.Size(105, 18);
		this.lbPosCode.Text = "كود نقطة البيع:";

		// txtPosCode
		this.txtPosCode.Location = new System.Drawing.Point(50, 126);
		this.txtPosCode.Name = "txtPosCode";
		this.txtPosCode.Size = new System.Drawing.Size(450, 26);

		// lbSerialNumber
		this.lbSerialNumber.AutoSize = true;
		this.lbSerialNumber.Location = new System.Drawing.Point(520, 170);
		this.lbSerialNumber.Name = "lbSerialNumber";
		this.lbSerialNumber.Size = new System.Drawing.Size(148, 18);
		this.lbSerialNumber.Text = "سيريال جهاز الضرائب:";

		// txtSerialNumber
		this.txtSerialNumber.Location = new System.Drawing.Point(50, 166);
		this.txtSerialNumber.Name = "txtSerialNumber";
		this.txtSerialNumber.Size = new System.Drawing.Size(450, 26);

		// lbOSVersion
		this.lbOSVersion.AutoSize = true;
		this.lbOSVersion.Location = new System.Drawing.Point(520, 210);
		this.lbOSVersion.Name = "lbOSVersion";
		this.lbOSVersion.Size = new System.Drawing.Size(95, 18);
		this.lbOSVersion.Text = "نظام التشغيل:";

		// txtOSVersion
		this.txtOSVersion.Location = new System.Drawing.Point(50, 206);
		this.txtOSVersion.Name = "txtOSVersion";
		this.txtOSVersion.Size = new System.Drawing.Size(450, 26);

		// lbModel
		this.lbModel.AutoSize = true;
		this.lbModel.Location = new System.Drawing.Point(520, 250);
		this.lbModel.Name = "lbModel";
		this.lbModel.Size = new System.Drawing.Size(91, 18);
		this.lbModel.Text = "طراز الجهاز:";

		// txtModel
		this.txtModel.Location = new System.Drawing.Point(50, 246);
		this.txtModel.Name = "txtModel";
		this.txtModel.Size = new System.Drawing.Size(450, 26);

		// lbActivityCode
		this.lbActivityCode.AutoSize = true;
		this.lbActivityCode.Location = new System.Drawing.Point(520, 290);
		this.lbActivityCode.Name = "lbActivityCode";
		this.lbActivityCode.Size = new System.Drawing.Size(130, 18);
		this.lbActivityCode.Text = "كود النشاط الضريبي:";

		// txtActivityCode
		this.txtActivityCode.Location = new System.Drawing.Point(50, 286);
		this.txtActivityCode.Name = "txtActivityCode";
		this.txtActivityCode.Size = new System.Drawing.Size(450, 26);

		// lbPrefix
		this.lbPrefix.AutoSize = true;
		this.lbPrefix.Location = new System.Drawing.Point(520, 330);
		this.lbPrefix.Name = "lbPrefix";
		this.lbPrefix.Size = new System.Drawing.Size(94, 18);
		this.lbPrefix.Text = "بادئة الإيصال:";

		// txtPrefix
		this.txtPrefix.Location = new System.Drawing.Point(310, 326);
		this.txtPrefix.Name = "txtPrefix";
		this.txtPrefix.Size = new System.Drawing.Size(190, 26);

		// lbSequence
		this.lbSequence.AutoSize = true;
		this.lbSequence.Location = new System.Drawing.Point(210, 330);
		this.lbSequence.Name = "lbSequence";
		this.lbSequence.Size = new System.Drawing.Size(91, 18);
		this.lbSequence.Text = "الرقم الحالي:";

		// txtSequence
		this.txtSequence.IsNumber = true;
		this.txtSequence.Location = new System.Drawing.Point(50, 326);
		this.txtSequence.Name = "txtSequence";
		this.txtSequence.Size = new System.Drawing.Size(150, 26);

		// chkIsProduction
		this.chkIsProduction.AutoSize = true;
		this.chkIsProduction.Location = new System.Drawing.Point(350, 370);
		this.chkIsProduction.Name = "chkIsProduction";
		this.chkIsProduction.Size = new System.Drawing.Size(148, 22);
		this.chkIsProduction.Text = "بيئة إنتاج فعلية (Live)";
		this.chkIsProduction.UseVisualStyleBackColor = true;

		// chkIsActive
		this.chkIsActive.AutoSize = true;
		this.chkIsActive.Checked = true;
		this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkIsActive.Location = new System.Drawing.Point(200, 370);
		this.chkIsActive.Name = "chkIsActive";
		this.chkIsActive.Size = new System.Drawing.Size(130, 22);
		this.chkIsActive.Text = "نقطة بيع نشطة";
		this.chkIsActive.UseVisualStyleBackColor = true;

		// frmPosDevices
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(700, 420);
		base.Controls.Add(this.chkIsActive);
		base.Controls.Add(this.chkIsProduction);
		base.Controls.Add(this.txtSequence);
		base.Controls.Add(this.lbSequence);
		base.Controls.Add(this.txtPrefix);
		base.Controls.Add(this.lbPrefix);
		base.Controls.Add(this.txtActivityCode);
		base.Controls.Add(this.lbActivityCode);
		base.Controls.Add(this.txtModel);
		base.Controls.Add(this.lbModel);
		base.Controls.Add(this.txtOSVersion);
		base.Controls.Add(this.lbOSVersion);
		base.Controls.Add(this.txtSerialNumber);
		base.Controls.Add(this.lbSerialNumber);
		base.Controls.Add(this.txtPosCode);
		base.Controls.Add(this.lbPosCode);
		base.Controls.Add(this.txtPosName);
		base.Controls.Add(this.lbPosName);
		base.Controls.Add(this.combBranch);
		base.Controls.Add(this.lbBranch);
		base.Name = "frmPosDevices";
		this.Text = "إدارة نقاط البيع والكاشير (POS Devices)";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
