
namespace E_Invoice.Desktop.Forms
{
    partial class frmSetting
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
            this.SuspendLayout();
            // 
            // txtClientSecrit
            // 
            this.txtClientSecrit.IsNumber = false;
            this.txtClientSecrit.Location = new System.Drawing.Point(125, 98);
            this.txtClientSecrit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtClientSecrit.Name = "txtClientSecrit";
            this.txtClientSecrit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtClientSecrit.Size = new System.Drawing.Size(422, 27);
            this.txtClientSecrit.TabIndex = 38;
            // 
            // txtSignerPass
            // 
            this.txtSignerPass.IsNumber = false;
            this.txtSignerPass.Location = new System.Drawing.Point(125, 139);
            this.txtSignerPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSignerPass.Name = "txtSignerPass";
            this.txtSignerPass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSignerPass.Size = new System.Drawing.Size(422, 27);
            this.txtSignerPass.TabIndex = 39;
            // 
            // txtClientId
            // 
            this.txtClientId.IsNumber = false;
            this.txtClientId.Location = new System.Drawing.Point(125, 57);
            this.txtClientId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtClientId.Size = new System.Drawing.Size(422, 27);
            this.txtClientId.TabIndex = 40;
            // 
            // lbTaxReg
            // 
            this.lbTaxReg.Location = new System.Drawing.Point(10, 98);
            this.lbTaxReg.Name = "lbTaxReg";
            this.lbTaxReg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbTaxReg.Size = new System.Drawing.Size(115, 28);
            this.lbTaxReg.TabIndex = 41;
            this.lbTaxReg.Text = "Client_Secret";
            this.lbTaxReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCommercialRegNo
            // 
            this.lbCommercialRegNo.Location = new System.Drawing.Point(10, 137);
            this.lbCommercialRegNo.Name = "lbCommercialRegNo";
            this.lbCommercialRegNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCommercialRegNo.Size = new System.Drawing.Size(126, 28);
            this.lbCommercialRegNo.TabIndex = 42;
            this.lbCommercialRegNo.Text = "Signer Password";
            this.lbCommercialRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(10, 57);
            this.lbName.Name = "lbName";
            this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbName.Size = new System.Drawing.Size(115, 28);
            this.lbName.TabIndex = 43;
            this.lbName.Text = "ClientId";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // togProdEnv
            // 
            this.togProdEnv.AutoSize = true;
            this.togProdEnv.Location = new System.Drawing.Point(185, 178);
            this.togProdEnv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.togProdEnv.Name = "togProdEnv";
            this.togProdEnv.Size = new System.Drawing.Size(80, 24);
            this.togProdEnv.TabIndex = 44;
            this.togProdEnv.Text = "Off";
            this.togProdEnv.UseSelectable = true;
            // 
            // togSigner
            // 
            this.togSigner.AutoSize = true;
            this.togSigner.Location = new System.Drawing.Point(185, 214);
            this.togSigner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.togSigner.Name = "togSigner";
            this.togSigner.Size = new System.Drawing.Size(80, 24);
            this.togSigner.TabIndex = 45;
            this.togSigner.Text = "Off";
            this.togSigner.UseSelectable = true;
            // 
            // labelEx1
            // 
            this.labelEx1.Location = new System.Drawing.Point(11, 172);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx1.Size = new System.Drawing.Size(144, 28);
            this.labelEx1.TabIndex = 42;
            this.labelEx1.Text = "Prod Enviroment";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx2
            // 
            this.labelEx2.Location = new System.Drawing.Point(11, 208);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx2.Size = new System.Drawing.Size(139, 28);
            this.labelEx2.TabIndex = 42;
            this.labelEx2.Text = "Sign Invoices";
            this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // togUseStoreBalanse
            // 
            this.togUseStoreBalanse.AutoSize = true;
            this.togUseStoreBalanse.Location = new System.Drawing.Point(185, 253);
            this.togUseStoreBalanse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.togUseStoreBalanse.Name = "togUseStoreBalanse";
            this.togUseStoreBalanse.Size = new System.Drawing.Size(80, 24);
            this.togUseStoreBalanse.TabIndex = 47;
            this.togUseStoreBalanse.Text = "Off";
            this.togUseStoreBalanse.UseSelectable = true;
            // 
            // labelEx3
            // 
            this.labelEx3.Location = new System.Drawing.Point(11, 247);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx3.Size = new System.Drawing.Size(164, 28);
            this.labelEx3.TabIndex = 46;
            this.labelEx3.Text = "البيع من رصيد المخزن فقط";
            this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // togUseGpc
            // 
            this.togUseGpc.AutoSize = true;
            this.togUseGpc.Location = new System.Drawing.Point(186, 296);
            this.togUseGpc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.togUseGpc.Name = "togUseGpc";
            this.togUseGpc.Size = new System.Drawing.Size(80, 24);
            this.togUseGpc.TabIndex = 49;
            this.togUseGpc.Text = "Off";
            this.togUseGpc.UseSelectable = true;
            // 
            // labelEx4
            // 
            this.labelEx4.Location = new System.Drawing.Point(12, 294);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx4.Size = new System.Drawing.Size(164, 28);
            this.labelEx4.TabIndex = 48;
            this.labelEx4.Text = "استخدام اكواد GS1 المؤقته";
            this.labelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 348);
            this.Controls.Add(this.togUseGpc);
            this.Controls.Add(this.labelEx4);
            this.Controls.Add(this.togUseStoreBalanse);
            this.Controls.Add(this.labelEx3);
            this.Controls.Add(this.togSigner);
            this.Controls.Add(this.togProdEnv);
            this.Controls.Add(this.txtClientSecrit);
            this.Controls.Add(this.txtSignerPass);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.lbTaxReg);
            this.Controls.Add(this.labelEx2);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this.lbCommercialRegNo);
            this.Controls.Add(this.lbName);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmSetting";
            this.Text = "اعدادات";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.lbCommercialRegNo, 0);
            this.Controls.SetChildIndex(this.labelEx1, 0);
            this.Controls.SetChildIndex(this.labelEx2, 0);
            this.Controls.SetChildIndex(this.lbTaxReg, 0);
            this.Controls.SetChildIndex(this.txtClientId, 0);
            this.Controls.SetChildIndex(this.txtSignerPass, 0);
            this.Controls.SetChildIndex(this.txtClientSecrit, 0);
            this.Controls.SetChildIndex(this.togProdEnv, 0);
            this.Controls.SetChildIndex(this.togSigner, 0);
            this.Controls.SetChildIndex(this.labelEx3, 0);
            this.Controls.SetChildIndex(this.togUseStoreBalanse, 0);
            this.Controls.SetChildIndex(this.labelEx4, 0);
            this.Controls.SetChildIndex(this.togUseGpc, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TextBoxEx txtClientSecrit;
        private Controls.TextBoxEx txtSignerPass;
        private Controls.TextBoxEx txtClientId;
        private Controls.LabelEx lbTaxReg;
        private Controls.LabelEx lbCommercialRegNo;
        private Controls.LabelEx lbName;
        private MetroFramework.Controls.MetroToggle togProdEnv;
        private MetroFramework.Controls.MetroToggle togSigner;
        private Controls.LabelEx labelEx1;
        private Controls.LabelEx labelEx2;
        private MetroFramework.Controls.MetroToggle togUseStoreBalanse;
        private Controls.LabelEx labelEx3;
        private MetroFramework.Controls.MetroToggle togUseGpc;
        private Controls.LabelEx labelEx4;
    }
}