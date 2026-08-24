
namespace E_Invoice.Desktop.Forms
{
    partial class frmProductSearch
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.producrVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.DGVItems = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.txtInitCode = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx2 = new E_Invoice.Desktop.Controls.LabelEx();
            ((System.ComponentModel.ISupportInitialize)(this.producrVMBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).BeginInit();
            this.SuspendLayout();
            // 
            // producrVMBindingSource
            // 
            this.producrVMBindingSource.DataSource = typeof(E_Invoice.Domain.Models.ProducrVM);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(34, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.toolStripButton4,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripButton5,
            this.toolStripSplitButton2,
            this.toolStripButton6,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1183, 39);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::E_Invoice.Desktop.Properties.Resources.Add_1_Icon_72;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(82, 36);
            this.toolStripButton1.Tag = "New";
            this.toolStripButton1.Text = "جديد";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::E_Invoice.Desktop.Properties.Resources.Save_Icon_72;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(80, 36);
            this.toolStripButton2.Text = "حفظ";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::E_Invoice.Desktop.Properties.Resources.Remove_Icon_72;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(86, 36);
            this.toolStripButton3.Text = "حذف";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = global::E_Invoice.Desktop.Properties.Resources.Hide_right_Icon_72;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::E_Invoice.Desktop.Properties.Resources.Navigate_right_Icon_72;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(46, 36);
            this.toolStripLabel1.Text = "بحث";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 39);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::E_Invoice.Desktop.Properties.Resources.Navigate_left_Icon_72;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton2.Image = global::E_Invoice.Desktop.Properties.Resources.Hide_left_Icon_72;
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::E_Invoice.Desktop.Properties.Resources.Windows_Close_Program_Icon_72;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(93, 36);
            this.toolStripButton6.Text = "خروج";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 39);
            // 
            // DGVItems
            // 
            this.DGVItems.AllowUserToAddRows = false;
            this.DGVItems.AllowUserToDeleteRows = false;
            this.DGVItems.AllowUserToOrderColumns = true;
            this.DGVItems.AutoGenerateColumns = false;
            this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVItems.BackgroundColor = System.Drawing.Color.White;
            this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.barcodeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.unitDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn});
            this.DGVItems.DataSource = this.producrVMBindingSource;
            this.DGVItems.Location = new System.Drawing.Point(0, 96);
            this.DGVItems.Margin = new System.Windows.Forms.Padding(4);
            this.DGVItems.Name = "DGVItems";
            this.DGVItems.ReadOnly = true;
            this.DGVItems.RowHeadersWidth = 51;
            this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVItems.Size = new System.Drawing.Size(1183, 446);
            this.DGVItems.TabIndex = 6;
            this.DGVItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVItems_CellDoubleClick_1);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "المعرف";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeDataGridViewTextBoxColumn
            // 
            this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
            this.barcodeDataGridViewTextBoxColumn.HeaderText = "الباركود";
            this.barcodeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
            this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "الصنف";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            this.unitDataGridViewTextBoxColumn.HeaderText = "الوحدة";
            this.unitDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            this.unitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "السعر";
            this.priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "الكود العالمي";
            this.codeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // txtSearch
            // 
            this.txtSearch.IsNumber = false;
            this.txtSearch.Location = new System.Drawing.Point(95, 51);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.Size = new System.Drawing.Size(186, 27);
            this.txtSearch.TabIndex = 38;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            // 
            // txtName
            // 
            this.txtName.IsNumber = false;
            this.txtName.Location = new System.Drawing.Point(399, 53);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtName.Size = new System.Drawing.Size(186, 27);
            this.txtName.TabIndex = 38;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtInitCode
            // 
            this.txtInitCode.IsNumber = false;
            this.txtInitCode.Location = new System.Drawing.Point(693, 53);
            this.txtInitCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtInitCode.Name = "txtInitCode";
            this.txtInitCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInitCode.Size = new System.Drawing.Size(186, 27);
            this.txtInitCode.TabIndex = 38;
            // 
            // labelEx3
            // 
            this.labelEx3.Location = new System.Drawing.Point(11, 50);
            this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx3.Size = new System.Drawing.Size(73, 29);
            this.labelEx3.TabIndex = 39;
            this.labelEx3.Text = "الباركود";
            this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx1
            // 
            this.labelEx1.Location = new System.Drawing.Point(300, 50);
            this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx1.Size = new System.Drawing.Size(82, 29);
            this.labelEx1.TabIndex = 40;
            this.labelEx1.Text = "اسم الصنف";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx2
            // 
            this.labelEx2.Location = new System.Drawing.Point(589, 52);
            this.labelEx2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx2.Size = new System.Drawing.Size(89, 29);
            this.labelEx2.TabIndex = 41;
            this.labelEx2.Text = "الكود العالمي";
            this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmProductSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 612);
            this.Controls.Add(this.labelEx2);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this.labelEx3);
            this.Controls.Add(this.txtInitCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.DGVItems);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Name = "frmProductSearch";
            this.Text = "بحث الاصناف";
            this.Load += new System.EventHandler(this.frmProductSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.producrVMBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource producrVMBindingSource;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.ToolStripButton toolStripButton2;
        public System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton toolStripSplitButton1;
        public System.Windows.Forms.ToolStripButton toolStripButton4;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        public System.Windows.Forms.ToolStripButton toolStripButton5;
        public System.Windows.Forms.ToolStripButton toolStripSplitButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private Controls.TextBoxEx txtSearch;
        private Controls.TextBoxEx txtName;
        private Controls.TextBoxEx txtInitCode;
        private Controls.LabelEx labelEx3;
        private Controls.LabelEx labelEx1;
        private Controls.LabelEx labelEx2;
        public System.Windows.Forms.DataGridView DGVItems;
    }
}