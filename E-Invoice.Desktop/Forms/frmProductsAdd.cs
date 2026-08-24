using E_Invoice.DAL.Repositories;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Forms
{
    public partial class frmProductsAdd : Master
    {
        
        public Product product;
        public frmProductsAdd()
        {
           
            
            InitializeComponent();
            New();
            Refresh();
            txtSalePrice.IsNumber = true;
            
        }

        public override void New()
        {
            product = new Product();
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
            base.SetData();
        }
        public override void Save()
        {
            SetData();
            if (product.Id == 0)
            {
                product.ProductUnites = new List<ProductUnites>()
                {
                    new ProductUnites
                    {
                        UnitId = combProductUnitId.SelectedValue.ToInt(),
                        UnitConvert = 1,
                        QtySmallUnit = 1,
                        BuyPrice = product.BuyPrice,
                        SellPrice = product.SalePrice,
                        Barcode = product.Code,
                        Avg = 0,
                    }
                };
                _unitOfWork.Products.Add(product);
            }
            
            else
            {
                var pu = _unitOfWork.ProductUnites.GetAllBy(x => x.QtySmallUnit == 1 && x.ProductId == product.Id).FirstOrDefault();
                pu.UnitId = combProductUnitId.SelectedValue.ToInt();
                pu.UnitConvert = 1;
                pu.QtySmallUnit = 1;
                pu.BuyPrice = product.BuyPrice;
                pu.SellPrice = product.SalePrice;
                pu.Barcode = product.Code;

                _unitOfWork.ProductUnites.Update(pu);
                _unitOfWork.Products.Update(product);
            }
            Mess.Save();
            Refresh();
            New();
            base.Save();
        }
        public override void Delete()
        {
            if (Mess.AskDelete() == DialogResult.Yes)
            {
                if (product != null)
                {
                    product.IsDelete = IsDelete.Deleted;
                    _unitOfWork.Products.Update(product);
                    Mess.Delete();
                    base.Delete();
                }
            }
        }
        public override void Refresh()
        {
            Fill(combProductUnitId, _unitOfWork.Units.GetAll());
            Fill(combCategoryId, _unitOfWork.Categories.GetAll());
            Fill(combitemType, Info.ItemTypesList);
            FillMaster(_unitOfWork.Products.GetAll());
            //FillMaster(_unitOfWork.Products.GetAll().ToList());
            base.Refresh();
        }

        public override void GetSelectedItem()
        {
            Product id = (Product)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            //product = _unitOfWork.Products.GetTById(id.Id);
            string[] s = { "ProductUnites" };
            product = _unitOfWork.Products.GetAllBy(x=>x.Id == id.Id , s  ).FirstOrDefault();
            GetData();

        }

    }
}
