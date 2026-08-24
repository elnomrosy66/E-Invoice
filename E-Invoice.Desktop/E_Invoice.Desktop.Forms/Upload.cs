using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using E_Invoice.Domian.Models;
using IronXL;

namespace E_Invoice.Desktop.Forms;

public class Upload : A
{
	public DataTable dtExcel;

	private IContainer components = null;

	private DataGridView dataGridView1;

	private Button button1;

	private Button button2;

	public Upload()
	{
		InitializeComponent();
	}

	private DataTable ReadExcel(string fileName)
	{
		WorkBook workBook = WorkBook.Load(fileName);
		WorkSheet defaultWorkSheet = workBook.DefaultWorkSheet;
		return defaultWorkSheet.ToDataTable(useFirstRowAsColumnNames: true);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string extension = Path.GetExtension(openFileDialog.FileName);
		if (extension.CompareTo(".xls") == 0 || extension.CompareTo(".xlsx") == 0)
		{
			try
			{
				dtExcel = ReadExcel(openFileDialog.FileName);
				dataGridView1.Visible = true;
				dataGridView1.DataSource = dtExcel;
				MessageBox.Show(dataGridView1.Rows.Count.ToString());
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
				return;
			}
		}
		MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void button2_Click(object sender, EventArgs e)
	{
		foreach (DataRow row in dtExcel.Rows)
		{
			Product product = new Product();
			product.SalePrice = 0m;
			product.BuyPrice = 0m;
			product.itemType = ItemType.GS1;
			product.itemCode = "EG-232389578-" + row[0].ToString();
			product.requestLimit = 0;
			product.CategoryId = 1;
			product.Name = row[1].ToString();
			product.Code = row[0].ToString();
			product.GPCCode = row[2].ToString();
			product.ProductUnites = new List<ProductUnites>
			{
				new ProductUnites
				{
					UnitId = 1,
					UnitConvert = 1m,
					QtySmallUnit = 1m,
					BuyPrice = product.BuyPrice,
					SellPrice = product.SalePrice,
					Barcode = product.Code,
					Avg = 0m
				}
			};
			_unitOfWork.Products.Add(product);
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
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(21, 146);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersWidth = 51;
		this.dataGridView1.RowTemplate.Height = 26;
		this.dataGridView1.Size = new System.Drawing.Size(1261, 572);
		this.dataGridView1.TabIndex = 0;
		this.button1.Location = new System.Drawing.Point(733, 41);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(167, 42);
		this.button1.TabIndex = 1;
		this.button1.Text = "تحميل الملف";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(309, 41);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(180, 48);
		this.button2.TabIndex = 2;
		this.button2.Text = "حفظ في قاعدة البيانات";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1294, 730);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dataGridView1);
		base.Name = "Upload";
		this.Text = "Upload";
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
