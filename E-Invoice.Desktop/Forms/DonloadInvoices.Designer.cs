
namespace E_Invoice.Desktop.Forms
{
    partial class DonloadInvoices
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVItems = new System.Windows.Forms.DataGridView();
            this.DTFrom = new E_Invoice.Desktop.Controls.DateTimePickerEx();
            this.DTTo = new E_Invoice.Desktop.Controls.DateTimePickerEx();
            this.I = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.C = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.D = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.valid = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.invalid = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.checkBoxEx6 = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.checkBoxEx7 = new E_Invoice.Desktop.Controls.CheckBoxEx();
            this.btnEx1 = new E_Invoice.Desktop.Controls.btnEx();
            this.grpDocType = new System.Windows.Forms.GroupBox();
            this.grpDocStatus = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PackageId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).BeginInit();
            this.grpDocType.SuspendLayout();
            this.grpDocStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVItems
            // 
            this.DGVItems.AllowUserToAddRows = false;
            this.DGVItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVItems.BackgroundColor = System.Drawing.Color.White;
            this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PackageId,
            this.RequestDate,
            this.Format,
            this.Status,
            this.DateFrom,
            this.DateTo,
            this.Download});
            this.DGVItems.Location = new System.Drawing.Point(0, 256);
            this.DGVItems.Margin = new System.Windows.Forms.Padding(4);
            this.DGVItems.Name = "DGVItems";
            this.DGVItems.ReadOnly = true;
            this.DGVItems.RowHeadersWidth = 51;
            this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVItems.Size = new System.Drawing.Size(1251, 319);
            this.DGVItems.TabIndex = 2;
            this.DGVItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVItems_CellContentClick);
            // 
            // DTFrom
            // 
            this.DTFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTFrom.Location = new System.Drawing.Point(69, 67);
            this.DTFrom.Margin = new System.Windows.Forms.Padding(4);
            this.DTFrom.Name = "DTFrom";
            this.DTFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DTFrom.Size = new System.Drawing.Size(147, 29);
            this.DTFrom.TabIndex = 39;
            // 
            // DTTo
            // 
            this.DTTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTTo.Location = new System.Drawing.Point(69, 121);
            this.DTTo.Margin = new System.Windows.Forms.Padding(4);
            this.DTTo.Name = "DTTo";
            this.DTTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DTTo.Size = new System.Drawing.Size(147, 29);
            this.DTTo.TabIndex = 39;
            // 
            // I
            // 
            this.I.AutoSize = true;
            this.I.Checked = true;
            this.I.CheckState = System.Windows.Forms.CheckState.Checked;
            this.I.Location = new System.Drawing.Point(6, 27);
            this.I.Name = "I";
            this.I.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.I.Size = new System.Drawing.Size(108, 28);
            this.I.TabIndex = 40;
            this.I.Tag = "I";
            this.I.Text = "فـاتـــــــــورة";
            this.I.UseVisualStyleBackColor = true;
            // 
            // C
            // 
            this.C.AutoSize = true;
            this.C.Location = new System.Drawing.Point(6, 60);
            this.C.Name = "C";
            this.C.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.C.Size = new System.Drawing.Size(107, 28);
            this.C.TabIndex = 40;
            this.C.Tag = "C";
            this.C.Text = "إشعار اضافة";
            this.C.UseVisualStyleBackColor = true;
            // 
            // D
            // 
            this.D.AutoSize = true;
            this.D.Location = new System.Drawing.Point(6, 92);
            this.D.Name = "D";
            this.D.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.D.Size = new System.Drawing.Size(107, 28);
            this.D.TabIndex = 40;
            this.D.Tag = "D";
            this.D.Text = "إشعـار خصم";
            this.D.UseVisualStyleBackColor = true;
            // 
            // valid
            // 
            this.valid.AutoSize = true;
            this.valid.Checked = true;
            this.valid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valid.Location = new System.Drawing.Point(6, 26);
            this.valid.Name = "valid";
            this.valid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.valid.Size = new System.Drawing.Size(101, 28);
            this.valid.TabIndex = 40;
            this.valid.Tag = "Valid";
            this.valid.Text = "صــحـيــــح";
            this.valid.UseVisualStyleBackColor = true;
            // 
            // invalid
            // 
            this.invalid.AutoSize = true;
            this.invalid.Location = new System.Drawing.Point(6, 59);
            this.invalid.Name = "invalid";
            this.invalid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.invalid.Size = new System.Drawing.Size(102, 28);
            this.invalid.TabIndex = 40;
            this.invalid.Tag = "Invalid";
            this.invalid.Text = "غير صحيح";
            this.invalid.UseVisualStyleBackColor = true;
            // 
            // checkBoxEx6
            // 
            this.checkBoxEx6.AutoSize = true;
            this.checkBoxEx6.Location = new System.Drawing.Point(6, 91);
            this.checkBoxEx6.Name = "checkBoxEx6";
            this.checkBoxEx6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxEx6.Size = new System.Drawing.Size(101, 28);
            this.checkBoxEx6.TabIndex = 40;
            this.checkBoxEx6.Tag = "Cancelled";
            this.checkBoxEx6.Text = "ملغــــــــــي";
            this.checkBoxEx6.UseVisualStyleBackColor = true;
            // 
            // checkBoxEx7
            // 
            this.checkBoxEx7.AutoSize = true;
            this.checkBoxEx7.Location = new System.Drawing.Point(6, 125);
            this.checkBoxEx7.Name = "checkBoxEx7";
            this.checkBoxEx7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxEx7.Size = new System.Drawing.Size(100, 28);
            this.checkBoxEx7.TabIndex = 40;
            this.checkBoxEx7.Tag = "Rejected";
            this.checkBoxEx7.Text = "rejected";
            this.checkBoxEx7.UseVisualStyleBackColor = true;
            // 
            // btnEx1
            // 
            this.btnEx1.BackColor = System.Drawing.Color.Maroon;
            this.btnEx1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEx1.Location = new System.Drawing.Point(1036, 94);
            this.btnEx1.Name = "btnEx1";
            this.btnEx1.Size = new System.Drawing.Size(138, 82);
            this.btnEx1.TabIndex = 41;
            this.btnEx1.Text = "طلب الحزمة";
            this.btnEx1.UseVisualStyleBackColor = false;
            this.btnEx1.Click += new System.EventHandler(this.btnEx1_Click);
            // 
            // grpDocType
            // 
            this.grpDocType.Controls.Add(this.D);
            this.grpDocType.Controls.Add(this.I);
            this.grpDocType.Controls.Add(this.C);
            this.grpDocType.Location = new System.Drawing.Point(243, 58);
            this.grpDocType.Name = "grpDocType";
            this.grpDocType.Size = new System.Drawing.Size(120, 157);
            this.grpDocType.TabIndex = 42;
            this.grpDocType.TabStop = false;
            this.grpDocType.Text = "نوع المستند";
            // 
            // grpDocStatus
            // 
            this.grpDocStatus.Controls.Add(this.checkBoxEx7);
            this.grpDocStatus.Controls.Add(this.valid);
            this.grpDocStatus.Controls.Add(this.invalid);
            this.grpDocStatus.Controls.Add(this.checkBoxEx6);
            this.grpDocStatus.Location = new System.Drawing.Point(391, 59);
            this.grpDocStatus.Name = "grpDocStatus";
            this.grpDocStatus.Size = new System.Drawing.Size(116, 156);
            this.grpDocStatus.TabIndex = 43;
            this.grpDocStatus.TabStop = false;
            this.grpDocStatus.Text = "الحالة";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 24);
            this.label1.TabIndex = 44;
            this.label1.Text = "من :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 24);
            this.label2.TabIndex = 44;
            this.label2.Text = "إلي :";
            // 
            // PackageId
            // 
            this.PackageId.HeaderText = "Id";
            this.PackageId.MinimumWidth = 6;
            this.PackageId.Name = "PackageId";
            this.PackageId.ReadOnly = true;
            // 
            // RequestDate
            // 
            this.RequestDate.HeaderText = "تاريخ الطلب";
            this.RequestDate.MinimumWidth = 6;
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.ReadOnly = true;
            // 
            // Format
            // 
            this.Format.HeaderText = "الصيغة";
            this.Format.MinimumWidth = 6;
            this.Format.Name = "Format";
            this.Format.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "الحالة";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // DateFrom
            // 
            this.DateFrom.HeaderText = "من تاريخ";
            this.DateFrom.MinimumWidth = 6;
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.ReadOnly = true;
            // 
            // DateTo
            // 
            this.DateTo.HeaderText = "إلي تاريخ";
            this.DateTo.MinimumWidth = 6;
            this.DateTo.Name = "DateTo";
            this.DateTo.ReadOnly = true;
            // 
            // Download
            // 
            this.Download.HeaderText = "تحميل";
            this.Download.MinimumWidth = 6;
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Download.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Download.Text = "تحميل الحزمة";
            this.Download.UseColumnTextForButtonValue = true;
            // 
            // DonloadInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 575);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpDocStatus);
            this.Controls.Add(this.grpDocType);
            this.Controls.Add(this.btnEx1);
            this.Controls.Add(this.DTTo);
            this.Controls.Add(this.DTFrom);
            this.Controls.Add(this.DGVItems);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DonloadInvoices";
            this.Text = "تحميل الفواتير";
            this.Load += new System.EventHandler(this.DonloadInvoices_Load);
            this.Controls.SetChildIndex(this.DGVItems, 0);
            this.Controls.SetChildIndex(this.DTFrom, 0);
            this.Controls.SetChildIndex(this.DTTo, 0);
            this.Controls.SetChildIndex(this.btnEx1, 0);
            this.Controls.SetChildIndex(this.grpDocType, 0);
            this.Controls.SetChildIndex(this.grpDocStatus, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).EndInit();
            this.grpDocType.ResumeLayout(false);
            this.grpDocType.PerformLayout();
            this.grpDocStatus.ResumeLayout(false);
            this.grpDocStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVItems;
        private Controls.DateTimePickerEx DTFrom;
        private Controls.DateTimePickerEx DTTo;
        private Controls.CheckBoxEx I;
        private Controls.CheckBoxEx C;
        private Controls.CheckBoxEx D;
        private Controls.CheckBoxEx valid;
        private Controls.CheckBoxEx invalid;
        private Controls.CheckBoxEx checkBoxEx6;
        private Controls.CheckBoxEx checkBoxEx7;
        private Controls.btnEx btnEx1;
        private System.Windows.Forms.GroupBox grpDocType;
        private System.Windows.Forms.GroupBox grpDocStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackageId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTo;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
    }
}