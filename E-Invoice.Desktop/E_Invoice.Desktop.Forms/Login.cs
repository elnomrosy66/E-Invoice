using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;

namespace E_Invoice.Desktop.Forms;

public class Login : A
{
	private IContainer components = null;

	private TextBoxEx txtUser;

	private TextBoxEx txtPass;

	private LabelEx labelEx9;

	private LabelEx labelEx1;

	private btnEx btnEx2;

	public Login()
	{
		InitializeComponent();
	}

	private void btnEx2_Click(object sender, EventArgs e)
	{
		if (txtUser.Text == string.Empty || txtPass.Text == string.Empty)
		{
			Mess.Warning("قم بادخال اسم المستخدم وكلمة المرور");
		}
		else if (txtUser.Text == "admin" || txtPass.Text == "123456789")
		{
			Hide();
			frmMain frmMain2 = new frmMain();
			frmMain2.ShowDialog();
		}
		else
		{
			Mess.Warning("خطاء في بيانات الدخول");
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
		this.txtUser = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtPass = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx9 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		this.btnEx2 = new E_Invoice.Desktop.Controls.btnEx();
		base.SuspendLayout();
		this.txtUser.BackColor = System.Drawing.Color.White;
		this.txtUser.IsNumber = false;
		this.txtUser.Location = new System.Drawing.Point(252, 91);
		this.txtUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtUser.Name = "txtUser";
		this.txtUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtUser.Size = new System.Drawing.Size(317, 25);
		this.txtUser.TabIndex = 0;
		this.txtUser.Text = "admin";
		this.txtPass.BackColor = System.Drawing.Color.White;
		this.txtPass.IsNumber = false;
		this.txtPass.Location = new System.Drawing.Point(252, 140);
		this.txtPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtPass.Name = "txtPass";
		this.txtPass.PasswordChar = '*';
		this.txtPass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtPass.Size = new System.Drawing.Size(317, 25);
		this.txtPass.TabIndex = 1;
		this.txtPass.Text = "123456789";
		this.labelEx9.Location = new System.Drawing.Point(49, 97);
		this.labelEx9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx9.Name = "labelEx9";
		this.labelEx9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.labelEx9.Size = new System.Drawing.Size(126, 21);
		this.labelEx9.TabIndex = 46;
		this.labelEx9.Text = "اسم المستخدم :";
		this.labelEx9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx1.Location = new System.Drawing.Point(59, 146);
		this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.labelEx1.Size = new System.Drawing.Size(126, 21);
		this.labelEx1.TabIndex = 46;
		this.labelEx1.Text = "كلمة المرور :";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEx2.BackColor = System.Drawing.Color.Maroon;
		this.btnEx2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.btnEx2.Location = new System.Drawing.Point(569, 210);
		this.btnEx2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.btnEx2.Name = "btnEx2";
		this.btnEx2.Size = new System.Drawing.Size(182, 71);
		this.btnEx2.TabIndex = 2;
		this.btnEx2.Text = "دخول";
		this.btnEx2.UseVisualStyleBackColor = false;
		this.btnEx2.Click += new System.EventHandler(btnEx2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(777, 314);
		base.Controls.Add(this.btnEx2);
		base.Controls.Add(this.labelEx1);
		base.Controls.Add(this.labelEx9);
		base.Controls.Add(this.txtPass);
		base.Controls.Add(this.txtUser);
		base.Name = "Login";
		this.Text = "دخول";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
