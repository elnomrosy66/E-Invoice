using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmProductSearch : A
{
	public IEnumerable<ProducrVM> products;

	private DataTable b;

	private IContainer components = null;

	private BindingSource producrVMBindingSource;

	private Button button1;

	public ToolStrip toolStrip1;

	private ToolStripButton toolStripButton1;

	public ToolStripButton toolStripButton2;

	public ToolStripButton toolStripButton3;

	private ToolStripSeparator toolStripSeparator1;

	public ToolStripButton toolStripSplitButton1;

	public ToolStripButton toolStripButton4;

	public ToolStripLabel toolStripLabel1;

	public ToolStripTextBox toolStripTextBox1;

	public ToolStripButton toolStripButton5;

	public ToolStripButton toolStripSplitButton2;

	private ToolStripButton toolStripButton6;

	private ToolStripComboBox toolStripComboBox1;

	private TextBoxEx txtSearch;

	private LabelEx labelEx3;

	public DataGridView DGVItems;

	private LabelEx labelEx1;

	private TextBoxEx txtName;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;

	public frmProductSearch()
	{
		InitializeComponent();
	}

	private void frmProductSearch_Load(object sender, EventArgs e)
	{
		products = (from p in _unitOfWork.Products.GetAll()
			select new ProducrVM
			{
				Id = p.Id,
				Barcode = p.Code,
				Unit = "قطعة",
				Name = p.Name,
				Price = p.SalePrice,
				Code = p.itemCode
			}).ToList();
		DGVItems.DataSource = products;
		toolStripComboBox1.ComboBox.DataSource = PropertiesFromType(new ProducrVM());
		toolStripComboBox1.ComboBox.DisplayMember = "DisplayName";
		toolStripComboBox1.ComboBox.ValueMember = "Name";
	}

	private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		string text = DGVItems.Rows[e.RowIndex].Index.ToString();
		string text2 = DGVItems.Rows[e.RowIndex].Cells[0].Value.ToString();
		Info._ProductSelected = true;
		Info._SelectedProduct = text2.ToInt();
		button1.PerformClick();
	}

	private void advancedDataGridViewSearchToolBar1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		CollectionFilterer collectionFilterer = new CollectionFilterer();
		List<ProducrVM> dataSource = collectionFilterer.Filter(products, "myItem => myItem." + (string)toolStripComboBox1.ComboBox.SelectedValue + ".Contains('ثلاجة')").Result.ToList();
		DGVItems.DataSource = dataSource;
	}

	public static List<ClassNames> PropertiesFromType(object atype)
	{
		if (atype == null)
		{
			return new List<ClassNames>();
		}
		Type type = atype.GetType();
		PropertyInfo[] properties = type.GetProperties();
		List<ClassNames> list = new List<ClassNames>();
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			DisplayNameAttribute displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), inherit: true).FirstOrDefault() as DisplayNameAttribute;
			list.Add(new ClassNames
			{
				DisplayName = displayNameAttribute.DisplayName,
				Name = propertyInfo.Name
			});
		}
		return list;
	}

	public void MappingColumnName(DataGridView dgv)
	{
		if (dgv.DataSource is DataTable dataTable)
		{
			for (int i = 0; i < dgv.Columns.Count; i++)
			{
				dgv.Columns[i].HeaderText = dataTable.Columns[i].Caption;
			}
		}
	}

	private void DGVItems_SortStringChanged(object sender, EventArgs e)
	{
	}

	private void DGVItems_FilterStringChanged(object sender, EventArgs e)
	{
	}

	private void txtBarcode_TextChanged(object sender, EventArgs e)
	{
		IEnumerable<ProducrVM> source = products.Where((ProducrVM x) => x.Barcode == txtSearch.Text);
		DGVItems.DataSource = source.ToList();
	}

	private void txtName_TextChanged(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void DGVItems_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
	{
		int index = DGVItems.SelectedRows[0].Index;
		int selectedProduct = DGVItems.Rows[index].Cells[0].Value.ToString().ToInt();
		Info._SelectedProduct = selectedProduct;
		button1.PerformClick();
	}

	private void txtName_TextChanged_1(object sender, EventArgs e)
	{
		IEnumerable<ProducrVM> source = products.Where((ProducrVM x) => x.Name.Contains(txtName.Text) || x.Code == txtSearch.Text);
		DGVItems.DataSource = source.ToList();
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
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
		this.txtSearch = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.producrVMBindingSource).BeginInit();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		base.SuspendLayout();
		this.producrVMBindingSource.DataSource = typeof(E_Invoice.Domain.Models.ProducrVM);
		this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.button1.Location = new System.Drawing.Point(34, 549);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(88, 42);
		this.button1.TabIndex = 4;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Black", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.toolStripButton1, this.toolStripButton2, this.toolStripButton3, this.toolStripSeparator1, this.toolStripSplitButton1, this.toolStripButton4, this.toolStripLabel1, this.toolStripTextBox1, this.toolStripButton5, this.toolStripSplitButton2,
			this.toolStripButton6, this.toolStripComboBox1
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(1183, 39);
		this.toolStrip1.TabIndex = 5;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStripButton1.Image = E_Invoice.Desktop.Properties.Resources.Add_1_Icon_72;
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(82, 36);
		this.toolStripButton1.Tag = "New";
		this.toolStripButton1.Text = "جديد";
		this.toolStripButton2.Image = E_Invoice.Desktop.Properties.Resources.Save_Icon_72;
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(80, 36);
		this.toolStripButton2.Text = "حفظ";
		this.toolStripButton3.Image = E_Invoice.Desktop.Properties.Resources.Remove_Icon_72;
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(86, 36);
		this.toolStripButton3.Text = "حذف";
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
		this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButton1.Image = E_Invoice.Desktop.Properties.Resources.Hide_right_Icon_72;
		this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton1.Name = "toolStripSplitButton1";
		this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripSplitButton1.Text = "toolStripSplitButton1";
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Image = E_Invoice.Desktop.Properties.Resources.Navigate_right_Icon_72;
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(46, 36);
		this.toolStripLabel1.Text = "بحث";
		this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 10.5f);
		this.toolStripTextBox1.Name = "toolStripTextBox1";
		this.toolStripTextBox1.Size = new System.Drawing.Size(100, 39);
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Image = E_Invoice.Desktop.Properties.Resources.Navigate_left_Icon_72;
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton5.Text = "toolStripButton5";
		this.toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
		this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButton2.Image = E_Invoice.Desktop.Properties.Resources.Hide_left_Icon_72;
		this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton2.Name = "toolStripSplitButton2";
		this.toolStripSplitButton2.Size = new System.Drawing.Size(36, 36);
		this.toolStripSplitButton2.Text = "toolStripSplitButton2";
		this.toolStripButton6.Image = E_Invoice.Desktop.Properties.Resources.Windows_Close_Program_Icon_72;
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(93, 36);
		this.toolStripButton6.Text = "خروج";
		this.toolStripComboBox1.Name = "toolStripComboBox1";
		this.toolStripComboBox1.Size = new System.Drawing.Size(121, 39);
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.AllowUserToDeleteRows = false;
		this.DGVItems.AllowUserToOrderColumns = true;
		this.DGVItems.AutoGenerateColumns = false;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.barcodeDataGridViewTextBoxColumn, this.nameDataGridViewTextBoxColumn, this.unitDataGridViewTextBoxColumn, this.priceDataGridViewTextBoxColumn, this.codeDataGridViewTextBoxColumn);
		this.DGVItems.DataSource = this.producrVMBindingSource;
		this.DGVItems.Location = new System.Drawing.Point(0, 96);
		this.DGVItems.Margin = new System.Windows.Forms.Padding(4);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(1183, 446);
		this.DGVItems.TabIndex = 6;
		this.DGVItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVItems_CellDoubleClick_1);
		this.txtSearch.IsNumber = false;
		this.txtSearch.Location = new System.Drawing.Point(95, 51);
		this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtSearch.Size = new System.Drawing.Size(186, 27);
		this.txtSearch.TabIndex = 38;
		this.txtSearch.TextChanged += new System.EventHandler(txtBarcode_TextChanged);
		this.labelEx3.Location = new System.Drawing.Point(11, 50);
		this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(73, 29);
		this.labelEx3.TabIndex = 39;
		this.labelEx3.Text = "باركود";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx1.Location = new System.Drawing.Point(308, 49);
		this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(100, 29);
		this.labelEx1.TabIndex = 40;
		this.labelEx1.Text = "بحث بالاسم";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(401, 51);
		this.txtName.Margin = new System.Windows.Forms.Padding(4);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtName.Size = new System.Drawing.Size(315, 27);
		this.txtName.TabIndex = 41;
		this.txtName.TextChanged += new System.EventHandler(txtName_TextChanged_1);
		this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
		this.idDataGridViewTextBoxColumn.HeaderText = "المعرف";
		this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
		this.barcodeDataGridViewTextBoxColumn.HeaderText = "الباركود";
		this.barcodeDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
		this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
		this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
		this.nameDataGridViewTextBoxColumn.HeaderText = "الصنف";
		this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
		this.nameDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
		this.unitDataGridViewTextBoxColumn.HeaderText = "الوحدة";
		this.unitDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
		this.unitDataGridViewTextBoxColumn.ReadOnly = true;
		this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
		this.priceDataGridViewTextBoxColumn.HeaderText = "السعر";
		this.priceDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
		this.priceDataGridViewTextBoxColumn.ReadOnly = true;
		this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
		this.codeDataGridViewTextBoxColumn.HeaderText = "الكود العالمي";
		this.codeDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
		this.codeDataGridViewTextBoxColumn.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1183, 612);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.labelEx1);
		base.Controls.Add(this.labelEx3);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGVItems);
		base.Controls.Add(this.toolStrip1);
		base.Controls.Add(this.button1);
		base.Name = "frmProductSearch";
		this.Text = "بحث الاصناف";
		base.Load += new System.EventHandler(frmProductSearch_Load);
		((System.ComponentModel.ISupportInitialize)this.producrVMBindingSource).EndInit();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
