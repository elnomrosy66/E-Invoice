using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmProductsAdd : Master
{
	public Product product;

	private IContainer components = null;

	private TextBoxEx txtSalePrice;

	private TextBoxEx txtBuyPrice;

	private ComboBoxEx combitemType;

	private TextBoxEx txtitemCode;

	private TextBoxEx txtrequestLimit;

	private ComboBoxEx combCategoryId;

	private ComboBoxEx combProductUnitId;

	private TextBoxEx txtName;

	private Button BtnSearchProduct;

	private TextBoxEx txtCode;

	private LabelEx lbSalePrice;

	private LabelEx lbBuyPrice;

	private LabelEx lbitemType;

	private LabelEx lbitemCode;

	private LabelEx lbrequestLimit;

	private LabelEx lbCategoryId;

	private LabelEx lbProductUnitId;

	private LabelEx lbName;

	private LabelEx lbCode;

	private LabelEx GPC;

	private TextBoxEx txtGPCCode;

	private Button btnUpload;

	public frmProductsAdd()
	{
		InitializeComponent();
		New();
		Refresh();
		txtSalePrice.IsNumber = true;
	}

	public override void New()
	{
		product = new Product
		{
			itemType = ItemType.EGS,
			CategoryId = _unitOfWork.Categories.GetAll().FirstOrDefault().Id
		};
		base.New();
	}

	public override void GetData()
	{
		txtSalePrice.Text = product.SalePrice.ToString();
		txtBuyPrice.Text = product.BuyPrice.ToString();
		combitemType.SelectedValue = (int)product.itemType;
		txtitemCode.Text = product.itemCode;
		txtrequestLimit.Text = product.requestLimit.ToString();
		combCategoryId.SelectedValue = product.CategoryId;
		if (product.Id != 0)
		{
			combProductUnitId.SelectedValue = product.ProductUnites.FirstOrDefault().UnitId;
		}
		txtName.Text = product.Name;
		txtCode.Text = product.Code;
		txtGPCCode.Text = product.GPCCode;
	}

	public override void SetData()
	{
		product.SalePrice = Convert.ToDecimal(txtSalePrice.Text);
		product.BuyPrice = txtBuyPrice.Text.ToDecimal();
		product.itemType = (ItemType)combitemType.SelectedValue;
		product.itemCode = txtitemCode.Text;
		product.requestLimit = txtrequestLimit.Text.ToInt();
		product.CategoryId = combCategoryId.SelectedValue.ToInt();
		product.Name = txtName.Text;
		product.Code = txtCode.Text;
		product.GPCCode = txtGPCCode.Text;
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (product.Id == 0)
		{
			product.ProductUnites = new List<ProductUnites>
			{
				new ProductUnites
				{
					UnitId = combProductUnitId.SelectedValue.ToInt(),
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
		else
		{
			ProductUnites productUnites = _unitOfWork.ProductUnites.GetAllBy((ProductUnites x) => x.QtySmallUnit == 1m && x.ProductId == product.Id).FirstOrDefault();
			productUnites.UnitId = combProductUnitId.SelectedValue.ToInt();
			productUnites.UnitConvert = 1m;
			productUnites.QtySmallUnit = 1m;
			productUnites.BuyPrice = product.BuyPrice;
			productUnites.SellPrice = product.SalePrice;
			productUnites.Barcode = product.Code;
			_unitOfWork.ProductUnites.Update(productUnites);
			_unitOfWork.Products.Update(product);
		}
		Mess.Save();
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (Mess.AskDelete() == DialogResult.Yes && product != null)
		{
			product.IsDelete = IsDelete.Deleted;
			_unitOfWork.Products.Update(product);
			Mess.Delete();
			base.Delete();
		}
	}

	private void BtnSearchProduct_Click(object sender, EventArgs e)
	{
		Info._ProductSelected = false;
		Info._SelectedProduct = 0;
		frmProductSearch frmProductSearch2 = new frmProductSearch();
		if (frmProductSearch2.ShowDialog() == DialogResult.OK && Info._SelectedProduct != 0)
		{
			product = _unitOfWork.Products.GetTById(Info._SelectedProduct);
			if (product != null)
			{
				GetData();
			}
		}
	}

	public override void Refresh()
	{
		Fill(combProductUnitId, _unitOfWork.Units.GetAll());
		Fill(combCategoryId, _unitOfWork.Categories.GetAll());
		Fill(combitemType, Info.ItemTypesList);
		FillMaster(_unitOfWork.Products.GetAll());
		base.Refresh();
	}

	public override void GetSelectedItem()
	{
		Product id = (Product)MasterCompo.ComboBox.SelectedItem;
		if (id != null)
		{
			string[] includes = new string[1] { "ProductUnites" };
			product = _unitOfWork.Products.GetAllBy((Product x) => x.Id == id.Id, includes).FirstOrDefault();
			GetData();
		}
	}

	private void btnUpload_Click(object sender, EventArgs e)
	{
		ItemCodesRoot itemCodes = new ItemCodesRoot
		{
			items = new List<ItemCode>
			{
				new ItemCode
				{
					codeType = "EGS",
					parentCode = product.GPCCode,
					itemCode = product.itemCode,
					codeName = product.Name,
					codeNameAr = product.Name,
					activeFrom = DateTime.UtcNow,
					activeTo = DateTime.UtcNow.AddYears(50),
					description = product.Name,
					descriptionAr = product.Name
				}
			}
		};
		try
		{
			ItemCodeResponce itemCodeResponce = _tax.CreateEGSCode(itemCodes);
			if (itemCodeResponce.failedItems.Count > 0)
			{
				Mess.Warning(itemCodeResponce.failedItems[0].errors[0]);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		this.txtSalePrice = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtBuyPrice = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.combitemType = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.txtitemCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtrequestLimit = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.combCategoryId = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.combProductUnitId = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.BtnSearchProduct = new System.Windows.Forms.Button();
		this.txtCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbSalePrice = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbBuyPrice = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbitemType = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbitemCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbrequestLimit = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCategoryId = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbProductUnitId = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCode = new E_Invoice.Desktop.Controls.LabelEx();
		this.GPC = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtGPCCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.btnUpload = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.txtSalePrice.IsNumber = true;
		this.txtSalePrice.Location = new System.Drawing.Point(122, 175);
		this.txtSalePrice.Name = "txtSalePrice";
		this.txtSalePrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtSalePrice.Size = new System.Drawing.Size(452, 25);
		this.txtSalePrice.TabIndex = 20;
		this.txtBuyPrice.IsNumber = true;
		this.txtBuyPrice.Location = new System.Drawing.Point(122, 207);
		this.txtBuyPrice.Name = "txtBuyPrice";
		this.txtBuyPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtBuyPrice.Size = new System.Drawing.Size(452, 25);
		this.txtBuyPrice.TabIndex = 21;
		this.combitemType.Location = new System.Drawing.Point(122, 239);
		this.combitemType.Name = "combitemType";
		this.combitemType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.combitemType.Size = new System.Drawing.Size(452, 25);
		this.combitemType.TabIndex = 22;
		this.txtitemCode.IsNumber = false;
		this.txtitemCode.Location = new System.Drawing.Point(122, 271);
		this.txtitemCode.Name = "txtitemCode";
		this.txtitemCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtitemCode.Size = new System.Drawing.Size(452, 25);
		this.txtitemCode.TabIndex = 23;
		this.txtrequestLimit.IsNumber = true;
		this.txtrequestLimit.Location = new System.Drawing.Point(122, 303);
		this.txtrequestLimit.Name = "txtrequestLimit";
		this.txtrequestLimit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtrequestLimit.Size = new System.Drawing.Size(452, 25);
		this.txtrequestLimit.TabIndex = 24;
		this.combCategoryId.Location = new System.Drawing.Point(122, 112);
		this.combCategoryId.Name = "combCategoryId";
		this.combCategoryId.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.combCategoryId.Size = new System.Drawing.Size(452, 25);
		this.combCategoryId.TabIndex = 25;
		this.combProductUnitId.Location = new System.Drawing.Point(122, 145);
		this.combProductUnitId.Name = "combProductUnitId";
		this.combProductUnitId.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.combProductUnitId.Size = new System.Drawing.Size(452, 25);
		this.combProductUnitId.TabIndex = 26;
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(152, 48);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtName.Size = new System.Drawing.Size(422, 25);
		this.txtName.TabIndex = 27;
		this.BtnSearchProduct.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.BtnSearchProduct.Location = new System.Drawing.Point(122, 48);
		this.BtnSearchProduct.Size = new System.Drawing.Size(25, 25);
		this.BtnSearchProduct.TabIndex = 40;
		this.BtnSearchProduct.UseVisualStyleBackColor = true;
		this.BtnSearchProduct.Click += new System.EventHandler(this.BtnSearchProduct_Click);
		this.txtCode.IsNumber = false;
		this.txtCode.Location = new System.Drawing.Point(122, 78);
		this.txtCode.Name = "txtCode";
		this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCode.Size = new System.Drawing.Size(452, 25);
		this.txtCode.TabIndex = 28;
		this.lbSalePrice.Location = new System.Drawing.Point(12, 175);
		this.lbSalePrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbSalePrice.Name = "lbSalePrice";
		this.lbSalePrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbSalePrice.Size = new System.Drawing.Size(97, 24);
		this.lbSalePrice.TabIndex = 29;
		this.lbSalePrice.Text = "سعر البيع";
		this.lbSalePrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbBuyPrice.Location = new System.Drawing.Point(12, 207);
		this.lbBuyPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbBuyPrice.Name = "lbBuyPrice";
		this.lbBuyPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbBuyPrice.Size = new System.Drawing.Size(97, 24);
		this.lbBuyPrice.TabIndex = 30;
		this.lbBuyPrice.Text = "سعر الشراء";
		this.lbBuyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbitemType.Location = new System.Drawing.Point(12, 239);
		this.lbitemType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbitemType.Name = "lbitemType";
		this.lbitemType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbitemType.Size = new System.Drawing.Size(97, 24);
		this.lbitemType.TabIndex = 31;
		this.lbitemType.Text = "نوع التكويد";
		this.lbitemType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbitemCode.Location = new System.Drawing.Point(12, 271);
		this.lbitemCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbitemCode.Name = "lbitemCode";
		this.lbitemCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbitemCode.Size = new System.Drawing.Size(97, 24);
		this.lbitemCode.TabIndex = 32;
		this.lbitemCode.Text = "الكود العالمي";
		this.lbitemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbrequestLimit.Location = new System.Drawing.Point(12, 303);
		this.lbrequestLimit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbrequestLimit.Name = "lbrequestLimit";
		this.lbrequestLimit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbrequestLimit.Size = new System.Drawing.Size(97, 24);
		this.lbrequestLimit.TabIndex = 33;
		this.lbrequestLimit.Text = "حد الطلب";
		this.lbrequestLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCategoryId.Location = new System.Drawing.Point(12, 112);
		this.lbCategoryId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbCategoryId.Name = "lbCategoryId";
		this.lbCategoryId.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCategoryId.Size = new System.Drawing.Size(97, 24);
		this.lbCategoryId.TabIndex = 34;
		this.lbCategoryId.Text = "المجموعة";
		this.lbCategoryId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbProductUnitId.Location = new System.Drawing.Point(12, 145);
		this.lbProductUnitId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbProductUnitId.Name = "lbProductUnitId";
		this.lbProductUnitId.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbProductUnitId.Size = new System.Drawing.Size(97, 24);
		this.lbProductUnitId.TabIndex = 35;
		this.lbProductUnitId.Text = "الوحدة";
		this.lbProductUnitId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbName.Location = new System.Drawing.Point(12, 48);
		this.lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(97, 24);
		this.lbName.TabIndex = 36;
		this.lbName.Text = "الاسم";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCode.Location = new System.Drawing.Point(12, 78);
		this.lbCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbCode.Name = "lbCode";
		this.lbCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCode.Size = new System.Drawing.Size(97, 24);
		this.lbCode.TabIndex = 37;
		this.lbCode.Text = "الكود الداخلي";
		this.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.GPC.Location = new System.Drawing.Point(12, 337);
		this.GPC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.GPC.Name = "GPC";
		this.GPC.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.GPC.Size = new System.Drawing.Size(97, 24);
		this.GPC.TabIndex = 33;
		this.GPC.Text = "كود GPC";
		this.GPC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtGPCCode.IsNumber = true;
		this.txtGPCCode.Location = new System.Drawing.Point(122, 337);
		this.txtGPCCode.Name = "txtGPCCode";
		this.txtGPCCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtGPCCode.Size = new System.Drawing.Size(452, 25);
		this.txtGPCCode.TabIndex = 24;
		this.btnUpload.Location = new System.Drawing.Point(596, 385);
		this.btnUpload.Name = "btnUpload";
		this.btnUpload.Size = new System.Drawing.Size(112, 36);
		this.btnUpload.TabIndex = 38;
		this.btnUpload.UseVisualStyleBackColor = true;
		this.btnUpload.Click += new System.EventHandler(btnUpload_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(720, 433);
		base.Controls.Add(this.btnUpload);
		base.Controls.Add(this.txtSalePrice);
		base.Controls.Add(this.txtBuyPrice);
		base.Controls.Add(this.combitemType);
		base.Controls.Add(this.txtitemCode);
		base.Controls.Add(this.txtGPCCode);
		base.Controls.Add(this.txtrequestLimit);
		base.Controls.Add(this.combCategoryId);
		base.Controls.Add(this.combProductUnitId);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.BtnSearchProduct);
		base.Controls.Add(this.txtCode);
		base.Controls.Add(this.lbSalePrice);
		base.Controls.Add(this.lbBuyPrice);
		base.Controls.Add(this.lbitemType);
		base.Controls.Add(this.lbitemCode);
		base.Controls.Add(this.GPC);
		base.Controls.Add(this.lbrequestLimit);
		base.Controls.Add(this.lbCategoryId);
		base.Controls.Add(this.lbProductUnitId);
		base.Controls.Add(this.lbName);
		base.Controls.Add(this.lbCode);
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.Name = "frmProductsAdd";
		this.Text = "الأصناف";
		base.Controls.SetChildIndex(this.lbCode, 0);
		base.Controls.SetChildIndex(this.lbName, 0);
		base.Controls.SetChildIndex(this.lbProductUnitId, 0);
		base.Controls.SetChildIndex(this.lbCategoryId, 0);
		base.Controls.SetChildIndex(this.lbrequestLimit, 0);
		base.Controls.SetChildIndex(this.GPC, 0);
		base.Controls.SetChildIndex(this.lbitemCode, 0);
		base.Controls.SetChildIndex(this.lbitemType, 0);
		base.Controls.SetChildIndex(this.lbBuyPrice, 0);
		base.Controls.SetChildIndex(this.lbSalePrice, 0);
		base.Controls.SetChildIndex(this.txtCode, 0);
		base.Controls.SetChildIndex(this.txtName, 0);
		base.Controls.SetChildIndex(this.BtnSearchProduct, 0);
		base.Controls.SetChildIndex(this.combProductUnitId, 0);
		base.Controls.SetChildIndex(this.combCategoryId, 0);
		base.Controls.SetChildIndex(this.txtrequestLimit, 0);
		base.Controls.SetChildIndex(this.txtGPCCode, 0);
		base.Controls.SetChildIndex(this.txtitemCode, 0);
		base.Controls.SetChildIndex(this.combitemType, 0);
		base.Controls.SetChildIndex(this.txtBuyPrice, 0);
		base.Controls.SetChildIndex(this.txtSalePrice, 0);
		base.Controls.SetChildIndex(this.btnUpload, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
