
namespace E_Invoice.Desktop.Forms
{
    partial class frmCategories
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
        /// Engenner Mohammed Mahmoud Phone 01062660800  // 01155519519 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.txtCode = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
            this.lbCode = new E_Invoice.Desktop.Controls.LabelEx();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.IsNumber = false;
            this.txtName.Location = new System.Drawing.Point(216, 56);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(564, 27);
            this.txtName.TabIndex = 6;
            // 
            // txtCode
            // 
            this.txtCode.IsNumber = false;
            this.txtCode.Location = new System.Drawing.Point(216, 94);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCode.Size = new System.Drawing.Size(564, 27);
            this.txtCode.TabIndex = 7;
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(24, 56);
            this.lbName.Name = "lbName";
            this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbName.Size = new System.Drawing.Size(186, 29);
            this.lbName.TabIndex = 8;
            this.lbName.Text = "الاسم";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCode
            // 
            this.lbCode.Location = new System.Drawing.Point(24, 94);
            this.lbCode.Name = "lbCode";
            this.lbCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCode.Size = new System.Drawing.Size(186, 29);
            this.lbCode.TabIndex = 9;
            this.lbCode.Text = "الكود العالمي الموحد";
            this.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 384);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbCode);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "frmCategories";
            this.Text = "مجموعات الاصناف";
            this.Controls.SetChildIndex(this.lbCode, 0);
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.txtCode, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TextBoxEx txtName;
        private Controls.TextBoxEx txtCode;
        private Controls.LabelEx lbName;
        private Controls.LabelEx lbCode;
    }
}