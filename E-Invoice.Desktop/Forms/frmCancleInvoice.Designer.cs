
namespace E_Invoice.Desktop.Forms
{
    partial class frmCancleInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOrderUUID = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
            this.txtOrderId = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.lbTaxReg = new E_Invoice.Desktop.Controls.LabelEx();
            this.txtReason = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
            this.SuspendLayout();
            // 
            // txtOrderUUID
            // 
            this.txtOrderUUID.IsNumber = false;
            this.txtOrderUUID.Location = new System.Drawing.Point(175, 68);
            this.txtOrderUUID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOrderUUID.Name = "txtOrderUUID";
            this.txtOrderUUID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrderUUID.Size = new System.Drawing.Size(326, 27);
            this.txtOrderUUID.TabIndex = 38;
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(18, 28);
            this.lbName.Name = "lbName";
            this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbName.Size = new System.Drawing.Size(131, 28);
            this.lbName.TabIndex = 41;
            this.lbName.Text = "رقم الفاتورة";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrderId
            // 
            this.txtOrderId.IsNumber = false;
            this.txtOrderId.Location = new System.Drawing.Point(175, 29);
            this.txtOrderId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrderId.Size = new System.Drawing.Size(326, 27);
            this.txtOrderId.TabIndex = 39;
            // 
            // lbTaxReg
            // 
            this.lbTaxReg.Location = new System.Drawing.Point(15, 69);
            this.lbTaxReg.Name = "lbTaxReg";
            this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbTaxReg.Size = new System.Drawing.Size(136, 28);
            this.lbTaxReg.TabIndex = 40;
            this.lbTaxReg.Text = "UUID";
            this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(175, 112);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(326, 159);
            this.txtReason.TabIndex = 42;
            this.txtReason.Text = "";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(253, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 41);
            this.button1.TabIndex = 43;
            this.button1.Text = "إرسال طلب الإلغاء";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelEx1
            // 
            this.labelEx1.Location = new System.Drawing.Point(12, 112);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx1.Size = new System.Drawing.Size(104, 28);
            this.labelEx1.TabIndex = 44;
            this.labelEx1.Text = "سبب الإلغاء";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCancleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtOrderUUID);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.lbTaxReg);
            this.Name = "frmCancleInvoice";
            this.Text = "frmCancleInvoice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TextBoxEx txtOrderUUID;
        private Controls.LabelEx lbName;
        private Controls.TextBoxEx txtOrderId;
        private Controls.LabelEx lbTaxReg;
        private System.Windows.Forms.RichTextBox txtReason;
        private System.Windows.Forms.Button button1;
        private Controls.LabelEx labelEx1;
    }
}