
namespace E_Invoice.Desktop.Forms
{
    partial class frmInvoice
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
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTitle = new E_Invoice.Desktop.Controls.LabelEx();
            this.DTInvDate = new E_Invoice.Desktop.Controls.DateTimePickerEx();
            this.txtInvCode = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.labelEx6 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx5 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelAccount = new E_Invoice.Desktop.Controls.LabelEx();
            this.combStore = new E_Invoice.Desktop.Controls.ComboBoxEx();
            this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
            this.combAccount = new E_Invoice.Desktop.Controls.ComboBoxEx();
            this.combItems = new E_Invoice.Desktop.Controls.ComboBoxEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnOpenProductsSearch = new System.Windows.Forms.Button();
            this.txtItemTotal = new System.Windows.Forms.NumericUpDown();
            this.txtUnitValue = new System.Windows.Forms.NumericUpDown();
            this.txtQty = new System.Windows.Forms.NumericUpDown();
            this.btnEx1 = new E_Invoice.Desktop.Controls.btnEx();
            this.labelEx10 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx8 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx7 = new E_Invoice.Desktop.Controls.LabelEx();
            this.txtBarcode = new E_Invoice.Desktop.Controls.TextBoxEx();
            this.labelEx2 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx11 = new E_Invoice.Desktop.Controls.LabelEx();
            this.txtInvTotal = new System.Windows.Forms.NumericUpDown();
            this.txtInvDesc = new System.Windows.Forms.NumericUpDown();
            this.txtInvTax = new System.Windows.Forms.NumericUpDown();
            this.txtInvNet = new System.Windows.Forms.NumericUpDown();
            this.labelEx12 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx13 = new E_Invoice.Desktop.Controls.LabelEx();
            this.labelEx14 = new E_Invoice.Desktop.Controls.LabelEx();
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvNet)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVItems
            // 
            this.DGVItems.AllowUserToAddRows = false;
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
            this.Id,
            this.Name,
            this.Unit,
            this.UnitValue,
            this.Qty,
            this.ItemTotal,
            this.Cost});
            this.DGVItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVItems.Location = new System.Drawing.Point(3, 19);
            this.DGVItems.Name = "DGVItems";
            this.DGVItems.ReadOnly = true;
            this.DGVItems.RowHeadersWidth = 51;
            this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVItems.Size = new System.Drawing.Size(1093, 315);
            this.DGVItems.TabIndex = 1;
            this.DGVItems.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DGVItems_RowsAdded);
            this.DGVItems.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DGVItems_RowsRemoved);
            // 
            // Id
            // 
            this.Id.HeaderText = "كود الصنف";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.HeaderText = "اسم الصنف";
            this.Name.MinimumWidth = 6;
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "الوحدة";
            this.Unit.MinimumWidth = 6;
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // UnitValue
            // 
            this.UnitValue.HeaderText = "سعر الوحدة";
            this.UnitValue.MinimumWidth = 6;
            this.UnitValue.Name = "UnitValue";
            this.UnitValue.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "الكمية";
            this.Qty.MinimumWidth = 6;
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // ItemTotal
            // 
            this.ItemTotal.HeaderText = "الإجمالي";
            this.ItemTotal.MinimumWidth = 6;
            this.ItemTotal.Name = "ItemTotal";
            this.ItemTotal.ReadOnly = true;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "التكلفة للوحدة";
            this.Cost.MinimumWidth = 6;
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTitle);
            this.groupBox1.Controls.Add(this.DTInvDate);
            this.groupBox1.Controls.Add(this.txtInvCode);
            this.groupBox1.Controls.Add(this.labelEx6);
            this.groupBox1.Controls.Add(this.labelEx5);
            this.groupBox1.Controls.Add(this.labelAccount);
            this.groupBox1.Controls.Add(this.combStore);
            this.groupBox1.Controls.Add(this.labelEx3);
            this.groupBox1.Controls.Add(this.combAccount);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(1097, 186);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات الفاتورة";
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(422, 11);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelTitle.Size = new System.Drawing.Size(280, 76);
            this.labelTitle.TabIndex = 39;
            this.labelTitle.Text = "إجمالي الفاتورة";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTInvDate
            // 
            this.DTInvDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTInvDate.Location = new System.Drawing.Point(82, 69);
            this.DTInvDate.Name = "DTInvDate";
            this.DTInvDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DTInvDate.Size = new System.Drawing.Size(263, 23);
            this.DTInvDate.TabIndex = 38;
            // 
            // txtInvCode
            // 
            this.txtInvCode.IsNumber = false;
            this.txtInvCode.Location = new System.Drawing.Point(720, 40);
            this.txtInvCode.Name = "txtInvCode";
            this.txtInvCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInvCode.Size = new System.Drawing.Size(263, 23);
            this.txtInvCode.TabIndex = 37;
            // 
            // labelEx6
            // 
            this.labelEx6.Location = new System.Drawing.Point(349, 69);
            this.labelEx6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx6.Name = "labelEx6";
            this.labelEx6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx6.Size = new System.Drawing.Size(74, 25);
            this.labelEx6.TabIndex = 33;
            this.labelEx6.Text = "تاريخ الاصدار";
            this.labelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx5
            // 
            this.labelEx5.Location = new System.Drawing.Point(350, 39);
            this.labelEx5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx5.Name = "labelEx5";
            this.labelEx5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx5.Size = new System.Drawing.Size(68, 20);
            this.labelEx5.TabIndex = 33;
            this.labelEx5.Text = "المخزن";
            this.labelEx5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAccount
            // 
            this.labelAccount.Location = new System.Drawing.Point(988, 70);
            this.labelAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelAccount.Size = new System.Drawing.Size(81, 25);
            this.labelAccount.TabIndex = 33;
            this.labelAccount.Text = "العميل";
            this.labelAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combStore
            // 
            this.combStore.Location = new System.Drawing.Point(82, 37);
            this.combStore.Name = "combStore";
            this.combStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.combStore.Size = new System.Drawing.Size(263, 25);
            this.combStore.TabIndex = 32;
            // 
            // labelEx3
            // 
            this.labelEx3.Location = new System.Drawing.Point(988, 40);
            this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx3.Size = new System.Drawing.Size(81, 25);
            this.labelEx3.TabIndex = 33;
            this.labelEx3.Text = "كود الفاتورة";
            this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combAccount
            // 
            this.combAccount.Location = new System.Drawing.Point(720, 71);
            this.combAccount.Name = "combAccount";
            this.combAccount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.combAccount.Size = new System.Drawing.Size(263, 25);
            this.combAccount.TabIndex = 32;
            // 
            // combItems
            // 
            this.combItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combItems.DropDownHeight = 150;
            this.combItems.IntegralHeight = false;
            this.combItems.Location = new System.Drawing.Point(628, 16);
            this.combItems.Name = "combItems";
            this.combItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.combItems.Size = new System.Drawing.Size(235, 25);
            this.combItems.TabIndex = 32;
            this.combItems.SelectedIndexChanged += new System.EventHandler(this.combItems_SelectedIndexChanged_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGVItems);
            this.groupBox2.Location = new System.Drawing.Point(12, 280);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1099, 337);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "أصناف الفاتورة";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnOpenProductsSearch);
            this.groupBox3.Controls.Add(this.txtItemTotal);
            this.groupBox3.Controls.Add(this.txtUnitValue);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.btnEx1);
            this.groupBox3.Controls.Add(this.labelEx10);
            this.groupBox3.Controls.Add(this.labelEx8);
            this.groupBox3.Controls.Add(this.labelEx7);
            this.groupBox3.Controls.Add(this.txtBarcode);
            this.groupBox3.Controls.Add(this.combItems);
            this.groupBox3.Controls.Add(this.labelEx2);
            this.groupBox3.Controls.Add(this.labelEx1);
            this.groupBox3.Location = new System.Drawing.Point(12, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1100, 51);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "أختيار الاصناف";
            // 
            // BtnOpenProductsSearch
            // 
            this.BtnOpenProductsSearch.Location = new System.Drawing.Point(599, 17);
            this.BtnOpenProductsSearch.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOpenProductsSearch.Name = "BtnOpenProductsSearch";
            this.BtnOpenProductsSearch.Size = new System.Drawing.Size(23, 23);
            this.BtnOpenProductsSearch.TabIndex = 49;
            this.BtnOpenProductsSearch.Text = "button1";
            this.BtnOpenProductsSearch.UseVisualStyleBackColor = true;
            this.BtnOpenProductsSearch.Click += new System.EventHandler(this.BtnOpenProductsSearch_Click);
            // 
            // txtItemTotal
            // 
            this.txtItemTotal.Enabled = false;
            this.txtItemTotal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemTotal.Location = new System.Drawing.Point(110, 16);
            this.txtItemTotal.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtItemTotal.Name = "txtItemTotal";
            this.txtItemTotal.Size = new System.Drawing.Size(82, 26);
            this.txtItemTotal.TabIndex = 48;
            this.txtItemTotal.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // txtUnitValue
            // 
            this.txtUnitValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnitValue.Location = new System.Drawing.Point(273, 16);
            this.txtUnitValue.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtUnitValue.Name = "txtUnitValue";
            this.txtUnitValue.Size = new System.Drawing.Size(82, 26);
            this.txtUnitValue.TabIndex = 39;
            this.txtUnitValue.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.txtUnitValue.ValueChanged += new System.EventHandler(this.txtUnitValue_ValueChanged);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(452, 16);
            this.txtQty.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(82, 26);
            this.txtQty.TabIndex = 39;
            this.txtQty.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.txtQty.ValueChanged += new System.EventHandler(this.txtQty_ValueChanged);
            // 
            // btnEx1
            // 
            this.btnEx1.Location = new System.Drawing.Point(19, 17);
            this.btnEx1.Name = "btnEx1";
            this.btnEx1.Size = new System.Drawing.Size(75, 23);
            this.btnEx1.TabIndex = 39;
            this.btnEx1.Text = "إضافة";
            this.btnEx1.UseVisualStyleBackColor = true;
            this.btnEx1.Click += new System.EventHandler(this.btnEx1_Click);
            // 
            // labelEx10
            // 
            this.labelEx10.Location = new System.Drawing.Point(195, 20);
            this.labelEx10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx10.Name = "labelEx10";
            this.labelEx10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx10.Size = new System.Drawing.Size(64, 18);
            this.labelEx10.TabIndex = 47;
            this.labelEx10.Text = "الاجمالي";
            this.labelEx10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx8
            // 
            this.labelEx8.Location = new System.Drawing.Point(359, 20);
            this.labelEx8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx8.Name = "labelEx8";
            this.labelEx8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx8.Size = new System.Drawing.Size(81, 18);
            this.labelEx8.TabIndex = 43;
            this.labelEx8.Text = "سعر الوحدة";
            this.labelEx8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx7
            // 
            this.labelEx7.Location = new System.Drawing.Point(537, 20);
            this.labelEx7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx7.Name = "labelEx7";
            this.labelEx7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx7.Size = new System.Drawing.Size(52, 17);
            this.labelEx7.TabIndex = 41;
            this.labelEx7.Text = "الكمية";
            this.labelEx7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBarcode
            // 
            this.txtBarcode.IsNumber = false;
            this.txtBarcode.Location = new System.Drawing.Point(943, 18);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBarcode.Size = new System.Drawing.Size(71, 23);
            this.txtBarcode.TabIndex = 40;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // labelEx2
            // 
            this.labelEx2.Location = new System.Drawing.Point(1029, 20);
            this.labelEx2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx2.Size = new System.Drawing.Size(55, 20);
            this.labelEx2.TabIndex = 39;
            this.labelEx2.Text = "الباركود";
            this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx1
            // 
            this.labelEx1.Location = new System.Drawing.Point(866, 20);
            this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx1.Size = new System.Drawing.Size(75, 20);
            this.labelEx1.TabIndex = 35;
            this.labelEx1.Text = "اسم الصنف";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx11
            // 
            this.labelEx11.Location = new System.Drawing.Point(181, 632);
            this.labelEx11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx11.Name = "labelEx11";
            this.labelEx11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx11.Size = new System.Drawing.Size(63, 20);
            this.labelEx11.TabIndex = 39;
            this.labelEx11.Text = "الخصم";
            this.labelEx11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInvTotal
            // 
            this.txtInvTotal.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvTotal.ForeColor = System.Drawing.Color.Black;
            this.txtInvTotal.Location = new System.Drawing.Point(77, 625);
            this.txtInvTotal.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtInvTotal.Name = "txtInvTotal";
            this.txtInvTotal.Size = new System.Drawing.Size(82, 33);
            this.txtInvTotal.TabIndex = 40;
            this.txtInvTotal.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // txtInvDesc
            // 
            this.txtInvDesc.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvDesc.ForeColor = System.Drawing.Color.Red;
            this.txtInvDesc.Location = new System.Drawing.Point(257, 625);
            this.txtInvDesc.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtInvDesc.Name = "txtInvDesc";
            this.txtInvDesc.Size = new System.Drawing.Size(82, 33);
            this.txtInvDesc.TabIndex = 41;
            this.txtInvDesc.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.txtInvDesc.ValueChanged += new System.EventHandler(this.txtInvDesc_ValueChanged);
            // 
            // txtInvTax
            // 
            this.txtInvTax.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvTax.ForeColor = System.Drawing.Color.Green;
            this.txtInvTax.Location = new System.Drawing.Point(430, 625);
            this.txtInvTax.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtInvTax.Name = "txtInvTax";
            this.txtInvTax.Size = new System.Drawing.Size(82, 33);
            this.txtInvTax.TabIndex = 42;
            this.txtInvTax.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.txtInvTax.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.txtInvTax.ValueChanged += new System.EventHandler(this.txtInvTax_ValueChanged);
            // 
            // txtInvNet
            // 
            this.txtInvNet.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvNet.ForeColor = System.Drawing.Color.Blue;
            this.txtInvNet.Location = new System.Drawing.Point(586, 625);
            this.txtInvNet.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtInvNet.Name = "txtInvNet";
            this.txtInvNet.Size = new System.Drawing.Size(82, 33);
            this.txtInvNet.TabIndex = 40;
            this.txtInvNet.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelEx12
            // 
            this.labelEx12.Location = new System.Drawing.Point(360, 632);
            this.labelEx12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx12.Name = "labelEx12";
            this.labelEx12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx12.Size = new System.Drawing.Size(63, 20);
            this.labelEx12.TabIndex = 39;
            this.labelEx12.Text = "الضريبة";
            this.labelEx12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx13
            // 
            this.labelEx13.Location = new System.Drawing.Point(523, 632);
            this.labelEx13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx13.Name = "labelEx13";
            this.labelEx13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx13.Size = new System.Drawing.Size(60, 18);
            this.labelEx13.TabIndex = 39;
            this.labelEx13.Text = "الصافي";
            this.labelEx13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEx14
            // 
            this.labelEx14.Location = new System.Drawing.Point(10, 632);
            this.labelEx14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEx14.Name = "labelEx14";
            this.labelEx14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelEx14.Size = new System.Drawing.Size(60, 18);
            this.labelEx14.TabIndex = 43;
            this.labelEx14.Text = "الاجمالي";
            this.labelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 667);
            this.Controls.Add(this.labelEx14);
            this.Controls.Add(this.txtInvNet);
            this.Controls.Add(this.txtInvTax);
            this.Controls.Add(this.txtInvDesc);
            this.Controls.Add(this.txtInvTotal);
            this.Controls.Add(this.labelEx13);
            this.Controls.Add(this.labelEx12);
            this.Controls.Add(this.labelEx11);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            //this.Name = "frmInvoice";
            this.Text = "";
            this.Load += new System.EventHandler(this.frmInvoice_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.labelEx11, 0);
            this.Controls.SetChildIndex(this.labelEx12, 0);
            this.Controls.SetChildIndex(this.labelEx13, 0);
            this.Controls.SetChildIndex(this.txtInvTotal, 0);
            this.Controls.SetChildIndex(this.txtInvDesc, 0);
            this.Controls.SetChildIndex(this.txtInvTax, 0);
            this.Controls.SetChildIndex(this.txtInvNet, 0);
            this.Controls.SetChildIndex(this.labelEx14, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DGVItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvNet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVItems;
        private Controls.ComboBoxEx combAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.ComboBoxEx combStore;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.DateTimePickerEx DTInvDate;
        private Controls.TextBoxEx txtInvCode;
        private Controls.LabelEx labelEx6;
        private Controls.LabelEx labelEx5;
        private Controls.LabelEx labelAccount;
        private Controls.LabelEx labelEx3;
        private System.Windows.Forms.GroupBox groupBox3;
        private Controls.LabelEx labelEx10;
        private Controls.LabelEx labelEx8;
        private Controls.LabelEx labelEx7;
        private Controls.TextBoxEx txtBarcode;
        private Controls.LabelEx labelEx2;
        private Controls.LabelEx labelEx1;
        private System.Windows.Forms.NumericUpDown txtItemTotal;
        private System.Windows.Forms.NumericUpDown txtUnitValue;
        private System.Windows.Forms.NumericUpDown txtQty;
        private Controls.btnEx btnEx1;
        private Controls.LabelEx labelEx11;
        private System.Windows.Forms.NumericUpDown txtInvTotal;
        private System.Windows.Forms.NumericUpDown txtInvDesc;
        private System.Windows.Forms.NumericUpDown txtInvTax;
        private System.Windows.Forms.NumericUpDown txtInvNet;
        private Controls.LabelEx labelEx12;
        private Controls.LabelEx labelEx13;
        private Controls.LabelEx labelTitle;
        private Controls.LabelEx labelEx14;
        private Controls.ComboBoxEx combItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.Button BtnOpenProductsSearch;
    }
}