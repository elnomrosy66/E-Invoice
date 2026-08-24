using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using MetroFramework.Controls;

namespace E_Invoice.Desktop.Forms;

public class frmSetting : Master
{
	public setting setting;

	private IContainer components = null;

	private TextBoxEx txtClientSecrit;

	private TextBoxEx txtSignerPass;

	private TextBoxEx txtClientId;

	private LabelEx lbTaxReg;

	private LabelEx lbCommercialRegNo;

	private LabelEx lbName;

	private MetroToggle togProdEnv;

	private MetroToggle togSigner;

	private LabelEx labelEx1;

	private LabelEx labelEx2;

	private MetroToggle togUseStoreBalanse;

	private LabelEx labelEx3;

	private MetroToggle togUseGpc;

	private LabelEx labelEx4;

	public frmSetting()
	{
		InitializeComponent();
		New();
	}

	private void frmSetting_Load(object sender, EventArgs e)
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

	public override void New()
	{
		setting = ((_unitOfWork.Settings.GetAll().FirstOrDefault() == null) ? new setting
		{
			ProdEnv = false,
			Signer = false
		} : _unitOfWork.Settings.GetAll().FirstOrDefault());
		base.New();
	}

	public override void GetData()
	{
		txtClientId.Text = setting.ClientId;
		txtClientSecrit.Text = setting.ClientSecret;
		txtSignerPass.Text = setting.TokenPass;
		if (setting.ProdEnv)
		{
			togProdEnv.CheckState = CheckState.Checked;
		}
		else
		{
			togSigner.CheckState = CheckState.Unchecked;
		}
		if (setting.Signer)
		{
			togSigner.CheckState = CheckState.Checked;
		}
		else
		{
			togSigner.CheckState = CheckState.Unchecked;
		}
		if (setting.UseStoreBalance)
		{
			togUseStoreBalanse.CheckState = CheckState.Checked;
		}
		else
		{
			togUseStoreBalanse.CheckState = CheckState.Unchecked;
		}
		if (setting.UseGpc)
		{
			togUseGpc.CheckState = CheckState.Checked;
		}
		else
		{
			togUseGpc.CheckState = CheckState.Unchecked;
		}
		base.GetData();
	}

	public override void SetData()
	{
		setting.ClientId = txtClientId.Text;
		setting.ClientSecret = txtClientSecrit.Text;
		setting.TokenPass = txtSignerPass.Text;
		if (togProdEnv.CheckState == CheckState.Checked)
		{
			setting.ProdEnv = true;
		}
		else
		{
			setting.ProdEnv = false;
		}
		if (togSigner.CheckState == CheckState.Checked)
		{
			setting.Signer = true;
		}
		else
		{
			setting.Signer = false;
		}
		if (togUseStoreBalanse.CheckState == CheckState.Checked)
		{
			setting.UseStoreBalance = true;
		}
		else
		{
			setting.UseStoreBalance = false;
		}
		if (togUseGpc.CheckState == CheckState.Checked)
		{
			setting.UseGpc = true;
		}
		else
		{
			setting.UseGpc = false;
		}
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (setting.Id == 0)
		{
			_unitOfWork.Settings.Add(setting);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Settings.Update(setting);
			Mess.Update();
		}
		New();
		Info._setting = _unitOfWork.Settings.GetAll().FirstOrDefault();
		base.Save();
	}

	public override void Delete()
	{
		base.Delete();
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
		this.txtClientSecrit = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtSignerPass = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtClientId = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTaxReg = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCommercialRegNo = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.togProdEnv = new MetroFramework.Controls.MetroToggle();
		this.togSigner = new MetroFramework.Controls.MetroToggle();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx2 = new E_Invoice.Desktop.Controls.LabelEx();
		this.togUseStoreBalanse = new MetroFramework.Controls.MetroToggle();
		this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
		this.togUseGpc = new MetroFramework.Controls.MetroToggle();
		this.labelEx4 = new E_Invoice.Desktop.Controls.LabelEx();
		base.SuspendLayout();
		this.txtClientSecrit.IsNumber = false;
		this.txtClientSecrit.Location = new System.Drawing.Point(125, 98);
		this.txtClientSecrit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtClientSecrit.Name = "txtClientSecrit";
		this.txtClientSecrit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtClientSecrit.Size = new System.Drawing.Size(422, 27);
		this.txtClientSecrit.TabIndex = 38;
		this.txtSignerPass.IsNumber = false;
		this.txtSignerPass.Location = new System.Drawing.Point(125, 139);
		this.txtSignerPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtSignerPass.Name = "txtSignerPass";
		this.txtSignerPass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtSignerPass.Size = new System.Drawing.Size(422, 27);
		this.txtSignerPass.TabIndex = 39;
		this.txtClientId.IsNumber = false;
		this.txtClientId.Location = new System.Drawing.Point(125, 57);
		this.txtClientId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtClientId.Name = "txtClientId";
		this.txtClientId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtClientId.Size = new System.Drawing.Size(422, 27);
		this.txtClientId.TabIndex = 40;
		this.lbTaxReg.Location = new System.Drawing.Point(10, 98);
		this.lbTaxReg.Name = "lbTaxReg";
		this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbTaxReg.Size = new System.Drawing.Size(115, 28);
		this.lbTaxReg.TabIndex = 41;
		this.lbTaxReg.Text = "Client_Secret";
		this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCommercialRegNo.Location = new System.Drawing.Point(10, 137);
		this.lbCommercialRegNo.Name = "lbCommercialRegNo";
		this.lbCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCommercialRegNo.Size = new System.Drawing.Size(126, 28);
		this.lbCommercialRegNo.TabIndex = 42;
		this.lbCommercialRegNo.Text = "Signer Password";
		this.lbCommercialRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbName.Location = new System.Drawing.Point(10, 57);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(115, 28);
		this.lbName.TabIndex = 43;
		this.lbName.Text = "ClientId";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.togProdEnv.AutoSize = true;
		this.togProdEnv.Location = new System.Drawing.Point(185, 178);
		this.togProdEnv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.togProdEnv.Name = "togProdEnv";
		this.togProdEnv.Size = new System.Drawing.Size(80, 24);
		this.togProdEnv.TabIndex = 44;
		this.togProdEnv.Text = "Off";
		this.togProdEnv.UseSelectable = true;
		this.togSigner.AutoSize = true;
		this.togSigner.Location = new System.Drawing.Point(185, 214);
		this.togSigner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.togSigner.Name = "togSigner";
		this.togSigner.Size = new System.Drawing.Size(80, 24);
		this.togSigner.TabIndex = 45;
		this.togSigner.Text = "Off";
		this.togSigner.UseSelectable = true;
		this.labelEx1.Location = new System.Drawing.Point(11, 172);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(144, 28);
		this.labelEx1.TabIndex = 42;
		this.labelEx1.Text = "Prod Enviroment";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx2.Location = new System.Drawing.Point(11, 208);
		this.labelEx2.Name = "labelEx2";
		this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx2.Size = new System.Drawing.Size(139, 28);
		this.labelEx2.TabIndex = 42;
		this.labelEx2.Text = "Sign Invoices";
		this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.togUseStoreBalanse.AutoSize = true;
		this.togUseStoreBalanse.Location = new System.Drawing.Point(185, 253);
		this.togUseStoreBalanse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.togUseStoreBalanse.Name = "togUseStoreBalanse";
		this.togUseStoreBalanse.Size = new System.Drawing.Size(80, 24);
		this.togUseStoreBalanse.TabIndex = 47;
		this.togUseStoreBalanse.Text = "Off";
		this.togUseStoreBalanse.UseSelectable = true;
		this.labelEx3.Location = new System.Drawing.Point(11, 247);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(164, 28);
		this.labelEx3.TabIndex = 46;
		this.labelEx3.Text = "البيع من رصيد المخزن فقط";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.togUseGpc.AutoSize = true;
		this.togUseGpc.Location = new System.Drawing.Point(186, 296);
		this.togUseGpc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.togUseGpc.Name = "togUseGpc";
		this.togUseGpc.Size = new System.Drawing.Size(80, 24);
		this.togUseGpc.TabIndex = 49;
		this.togUseGpc.Text = "Off";
		this.togUseGpc.UseSelectable = true;
		this.labelEx4.Location = new System.Drawing.Point(12, 294);
		this.labelEx4.Name = "labelEx4";
		this.labelEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx4.Size = new System.Drawing.Size(164, 28);
		this.labelEx4.TabIndex = 48;
		this.labelEx4.Text = "استخدام اكواد GS1 المؤقته";
		this.labelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(590, 348);
		base.Controls.Add(this.togUseGpc);
		base.Controls.Add(this.labelEx4);
		base.Controls.Add(this.togUseStoreBalanse);
		base.Controls.Add(this.labelEx3);
		base.Controls.Add(this.togSigner);
		base.Controls.Add(this.togProdEnv);
		base.Controls.Add(this.txtClientSecrit);
		base.Controls.Add(this.txtSignerPass);
		base.Controls.Add(this.txtClientId);
		base.Controls.Add(this.lbTaxReg);
		base.Controls.Add(this.labelEx2);
		base.Controls.Add(this.labelEx1);
		base.Controls.Add(this.lbCommercialRegNo);
		base.Controls.Add(this.lbName);
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "frmSetting";
		this.Text = "اعدادات";
		base.Load += new System.EventHandler(frmSetting_Load);
		base.Controls.SetChildIndex(this.lbName, 0);
		base.Controls.SetChildIndex(this.lbCommercialRegNo, 0);
		base.Controls.SetChildIndex(this.labelEx1, 0);
		base.Controls.SetChildIndex(this.labelEx2, 0);
		base.Controls.SetChildIndex(this.lbTaxReg, 0);
		base.Controls.SetChildIndex(this.txtClientId, 0);
		base.Controls.SetChildIndex(this.txtSignerPass, 0);
		base.Controls.SetChildIndex(this.txtClientSecrit, 0);
		base.Controls.SetChildIndex(this.togProdEnv, 0);
		base.Controls.SetChildIndex(this.togSigner, 0);
		base.Controls.SetChildIndex(this.labelEx3, 0);
		base.Controls.SetChildIndex(this.togUseStoreBalanse, 0);
		base.Controls.SetChildIndex(this.labelEx4, 0);
		base.Controls.SetChildIndex(this.togUseGpc, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
