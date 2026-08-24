using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmDateEdit : A
{
	private int _Id;

	private Order _Order;

	private IContainer components = null;

	private DateTimePicker dateTimePicker1;

	private Button button1;

	private Label label1;

	private Label lblInvNo;

	private Label label3;

	public frmDateEdit(int Id)
	{
		_Id = Id;
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		_Order.Date = dateTimePicker1.Value;
		_unitOfWork.Orders.Update(_Order);
		base.DialogResult = DialogResult.OK;
	}

	private void frmDateEdit_Load(object sender, EventArgs e)
	{
		_Order = _unitOfWork.Orders.GetTById(_Id);
		lblInvNo.Text = _Order.OrderBarcode;
		dateTimePicker1.Value = _Order.Date;
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
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.lblInvNo = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dateTimePicker1.Location = new System.Drawing.Point(138, 79);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(175, 25);
		this.dateTimePicker1.TabIndex = 0;
		this.button1.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.Location = new System.Drawing.Point(305, 172);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(172, 46);
		this.button1.TabIndex = 1;
		this.button1.Text = "حفظ التاريخ الجديد";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 18);
		this.label1.TabIndex = 2;
		this.label1.Text = "فاتورة رقم :";
		this.lblInvNo.AutoSize = true;
		this.lblInvNo.Location = new System.Drawing.Point(135, 21);
		this.lblInvNo.Name = "lblInvNo";
		this.lblInvNo.Size = new System.Drawing.Size(32, 18);
		this.lblInvNo.TabIndex = 2;
		this.lblInvNo.Text = "111";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 86);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(122, 18);
		this.label3.TabIndex = 2;
		this.label3.Text = "تعديل التاريخ إلي :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(757, 250);
		base.Controls.Add(this.lblInvNo);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dateTimePicker1);
		base.Name = "frmDateEdit";
		this.Text = "تعديل تاريخ الفاتورة";
		base.Load += new System.EventHandler(frmDateEdit_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
