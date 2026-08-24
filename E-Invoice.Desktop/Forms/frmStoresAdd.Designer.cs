
namespace E_Invoice.Desktop.Forms
{
    partial class frmStoresAdd
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
            this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.IsNumber = false;
            this.txtName.Location = new System.Drawing.Point(95, 71);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(564, 27);
            this.txtName.TabIndex = 3;
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(1, 70);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(66, 29);
            this.lbName.TabIndex = 4;
            this.lbName.Text = "الاسم";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmStoresAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 305);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbName);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "frmStoresAdd";
            this.Text = "المخازن";
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TextBoxEx txtName;
        private Controls.LabelEx lbName;
    }
}