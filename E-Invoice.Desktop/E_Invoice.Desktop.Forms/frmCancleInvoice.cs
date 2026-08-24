using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmCancleInvoice : A
{
	public double _OrderId;

	public Order Order;

	private IContainer components = null;

	private TextBoxEx txtOrderUUID;

	private LabelEx lbName;

	private TextBoxEx txtOrderId;

	private LabelEx lbTaxReg;

	private RichTextBox txtReason;

	private Button button1;

	private LabelEx labelEx1;

	public frmCancleInvoice(int OrderId)
	{
		InitializeComponent();
		_OrderId = OrderId;
		Order = _unitOfWork.Orders.GetTById((int)_OrderId);
		GetData();
	}

	public void GetData()
	{
		txtOrderId.Text = Order.OrderBarcode;
		txtOrderUUID.Text = Order.uuid;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		ErrorResponce errorResponce = _tax.CancleDocument(txtOrderUUID.Text, txtReason.Text);
		if (errorResponce.error != null)
		{
			MessageBox.Show(errorResponce.error.details[0].message);
		}
		else
		{
			MessageBox.Show("تم طلب إلغاء الفاتورة بنجاح");
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
		this.txtOrderUUID = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtOrderId = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTaxReg = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtReason = new System.Windows.Forms.RichTextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		base.SuspendLayout();
		this.txtOrderUUID.IsNumber = false;
		this.txtOrderUUID.Location = new System.Drawing.Point(175, 68);
		this.txtOrderUUID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtOrderUUID.Name = "txtOrderUUID";
		this.txtOrderUUID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtOrderUUID.Size = new System.Drawing.Size(326, 25);
		this.txtOrderUUID.TabIndex = 38;
		this.lbName.Location = new System.Drawing.Point(18, 28);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(131, 28);
		this.lbName.TabIndex = 41;
		this.lbName.Text = "رقم الفاتورة";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtOrderId.IsNumber = false;
		this.txtOrderId.Location = new System.Drawing.Point(175, 29);
		this.txtOrderId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtOrderId.Name = "txtOrderId";
		this.txtOrderId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtOrderId.Size = new System.Drawing.Size(326, 25);
		this.txtOrderId.TabIndex = 39;
		this.lbTaxReg.Location = new System.Drawing.Point(15, 69);
		this.lbTaxReg.Name = "lbTaxReg";
		this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbTaxReg.Size = new System.Drawing.Size(136, 28);
		this.lbTaxReg.TabIndex = 40;
		this.lbTaxReg.Text = "UUID";
		this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtReason.Location = new System.Drawing.Point(175, 112);
		this.txtReason.Name = "txtReason";
		this.txtReason.Size = new System.Drawing.Size(326, 159);
		this.txtReason.TabIndex = 42;
		this.txtReason.Text = "";
		this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.button1.Location = new System.Drawing.Point(253, 322);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(157, 41);
		this.button1.TabIndex = 43;
		this.button1.Text = "إرسال طلب الإلغاء";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.labelEx1.Location = new System.Drawing.Point(12, 112);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(104, 28);
		this.labelEx1.TabIndex = 44;
		this.labelEx1.Text = "سبب الإلغاء";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.labelEx1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.txtReason);
		base.Controls.Add(this.txtOrderUUID);
		base.Controls.Add(this.lbName);
		base.Controls.Add(this.txtOrderId);
		base.Controls.Add(this.lbTaxReg);
		base.Name = "frmCancleInvoice";
		this.Text = "";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
