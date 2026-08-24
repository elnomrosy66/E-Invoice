using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.Domain.Models;

namespace E_Invoice.Desktop.Forms;

public class frmAddTaxForItem : A
{
	private frmInvoiceNew _frm;

	private IContainer components = null;

	private ComboBox cmbTaxTypes;

	private ComboBox cmbTaxSupTypes;

	private NumericUpDown ntaxRate;

	private NumericUpDown nTaxAmount;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private DataGridView DGVTaxes;

	private Button btnAdd;

	private DataGridViewTextBoxColumn taxTypeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn subTypeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn rateDataGridViewTextBoxColumn;

	private BindingSource taxableItemsBindingSource;

	public frmAddTaxForItem(frmInvoiceNew frm)
	{
		InitializeComponent();
		_frm = frm;
	}

	private void frmAddTaxForItem_Load(object sender, EventArgs e)
	{
		taxableItemsBindingSource.DataSource = _frm.SelectedProductNM.taxableItems.ToList();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		taxableItems taxableItems2 = new taxableItems
		{
			taxType = cmbTaxTypes.Text,
			subType = cmbTaxSupTypes.Text,
			rate = ntaxRate.Value,
			amount = nTaxAmount.Value
		};
		taxableItemsBindingSource.Add(taxableItems2);
		_frm.SelectedProductNM.taxableItems.Add(taxableItems2);
		_frm.SelectedProductNM.Taxes = "";
		foreach (taxableItems taxableItem in _frm.SelectedProductNM.taxableItems)
		{
			ProductInvoiceVM selectedProductNM = _frm.SelectedProductNM;
			selectedProductNM.Taxes = selectedProductNM.Taxes + taxableItem.taxType + "-" + taxableItem.rate + Environment.NewLine;
		}
	}

	private void ntaxRate_ValueChanged(object sender, EventArgs e)
	{
		nTaxAmount.Value = (_frm.SelectedProductNM.ItemTotal - _frm.SelectedProductNM.DiscountRate * _frm.SelectedProductNM.ItemTotal / 100m) * ntaxRate.Value / 100m;
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
		this.cmbTaxTypes = new System.Windows.Forms.ComboBox();
		this.cmbTaxSupTypes = new System.Windows.Forms.ComboBox();
		this.ntaxRate = new System.Windows.Forms.NumericUpDown();
		this.nTaxAmount = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.DGVTaxes = new System.Windows.Forms.DataGridView();
		this.taxTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.subTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.rateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.taxableItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.btnAdd = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.ntaxRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nTaxAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGVTaxes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.taxableItemsBindingSource).BeginInit();
		base.SuspendLayout();
		this.cmbTaxTypes.FormattingEnabled = true;
		this.cmbTaxTypes.Items.AddRange(new object[3] { "T1", "T4", "T3" });
		this.cmbTaxTypes.Location = new System.Drawing.Point(90, 12);
		this.cmbTaxTypes.Name = "cmbTaxTypes";
		this.cmbTaxTypes.Size = new System.Drawing.Size(141, 25);
		this.cmbTaxTypes.TabIndex = 0;
		this.cmbTaxSupTypes.FormattingEnabled = true;
		this.cmbTaxSupTypes.Items.AddRange(new object[2] { "V003", "V009" });
		this.cmbTaxSupTypes.Location = new System.Drawing.Point(90, 43);
		this.cmbTaxSupTypes.Name = "cmbTaxSupTypes";
		this.cmbTaxSupTypes.Size = new System.Drawing.Size(141, 25);
		this.cmbTaxSupTypes.TabIndex = 0;
		this.ntaxRate.Location = new System.Drawing.Point(341, 15);
		this.ntaxRate.Name = "ntaxRate";
		this.ntaxRate.Size = new System.Drawing.Size(120, 25);
		this.ntaxRate.TabIndex = 1;
		this.ntaxRate.ValueChanged += new System.EventHandler(ntaxRate_ValueChanged);
		this.nTaxAmount.Location = new System.Drawing.Point(341, 43);
		this.nTaxAmount.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
		this.nTaxAmount.Name = "nTaxAmount";
		this.nTaxAmount.Size = new System.Drawing.Size(120, 25);
		this.nTaxAmount.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(7, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 18);
		this.label1.TabIndex = 2;
		this.label1.Text = "الضريبة";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(7, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(78, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "نوع الضريبة";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(248, 17);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(51, 18);
		this.label3.TabIndex = 2;
		this.label3.Text = "النسبة";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(248, 45);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(47, 18);
		this.label4.TabIndex = 2;
		this.label4.Text = "القيمة";
		this.DGVTaxes.AutoGenerateColumns = false;
		this.DGVTaxes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVTaxes.BackgroundColor = System.Drawing.Color.White;
		this.DGVTaxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVTaxes.Columns.AddRange(this.taxTypeDataGridViewTextBoxColumn, this.amountDataGridViewTextBoxColumn, this.subTypeDataGridViewTextBoxColumn, this.rateDataGridViewTextBoxColumn);
		this.DGVTaxes.DataSource = this.taxableItemsBindingSource;
		this.DGVTaxes.Location = new System.Drawing.Point(11, 133);
		this.DGVTaxes.Name = "DGVTaxes";
		this.DGVTaxes.RowHeadersVisible = false;
		this.DGVTaxes.Size = new System.Drawing.Size(497, 150);
		this.DGVTaxes.TabIndex = 3;
		this.taxTypeDataGridViewTextBoxColumn.DataPropertyName = "taxType";
		this.taxTypeDataGridViewTextBoxColumn.HeaderText = "taxType";
		this.taxTypeDataGridViewTextBoxColumn.Name = "taxTypeDataGridViewTextBoxColumn";
		this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
		this.amountDataGridViewTextBoxColumn.HeaderText = "amount";
		this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
		this.subTypeDataGridViewTextBoxColumn.DataPropertyName = "subType";
		this.subTypeDataGridViewTextBoxColumn.HeaderText = "subType";
		this.subTypeDataGridViewTextBoxColumn.Name = "subTypeDataGridViewTextBoxColumn";
		this.rateDataGridViewTextBoxColumn.DataPropertyName = "rate";
		this.rateDataGridViewTextBoxColumn.HeaderText = "rate";
		this.rateDataGridViewTextBoxColumn.Name = "rateDataGridViewTextBoxColumn";
		this.taxableItemsBindingSource.DataSource = typeof(E_Invoice.Domain.Models.taxableItems);
		this.btnAdd.Location = new System.Drawing.Point(210, 92);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(106, 35);
		this.btnAdd.TabIndex = 4;
		this.btnAdd.Text = "أضف الضريبة";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(520, 313);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.DGVTaxes);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.nTaxAmount);
		base.Controls.Add(this.ntaxRate);
		base.Controls.Add(this.cmbTaxSupTypes);
		base.Controls.Add(this.cmbTaxTypes);
		base.Name = "frmAddTaxForItem";
		this.Text = "الضرائب المطبقه علي الصنف";
		base.Load += new System.EventHandler(frmAddTaxForItem_Load);
		((System.ComponentModel.ISupportInitialize)this.ntaxRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nTaxAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGVTaxes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.taxableItemsBindingSource).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
