using E_Invoice.DAL.Data;
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
    public partial class frmInvoice : Master
    {
        public OrderType _orderType;
        public Order order;
        public OrderDetail OrderDetails;
        public Product item;
        string accountType = "Customer";
        int AccountTypeInt = 0;
        private readonly ApplicationDBContext _context;//
        public frmInvoice(OrderType invType)
        {
            _context = new ApplicationDBContext();
            item = new Product();
            _orderType = invType;
            InitializeComponent();
            New();
        }
        public frmInvoice(int id)
        {
            _context = new ApplicationDBContext();
            order = _unitOfWork.Orders.GetTBy(x=>x.Id == id);
            item = new Product();
            _orderType = order.OrderType;
            InitializeComponent();   
            //GetData();
        }

        public override void New()
        {
            order = new Order() {
                Date = DateTime.Now,
                TotalVat = 14,
                OrderType = _orderType
            };
            OrderDetails = new OrderDetail();
            GetData();
            GetNewCode();
        }
        public override void GetData()
        {
            txtInvCode.Text = order.OrderBarcode;
            _orderType = order.OrderType;
            DTInvDate.Value = Convert.ToDateTime(order.Date.ToShortDateString());

            if (order.AccountId != null)
                combAccount.SelectedValue = order.AccountId;
            if (order.StoreId != 0)
                combStore.SelectedValue = order.StoreId;

            txtInvTotal.Value = (decimal)order.NetBeforeTax;
            txtInvTax.Value = (decimal)order.TotalVat;
            txtInvDesc.Value = (decimal)order.TotalDiscount;
            txtInvNet.Value = (decimal)order.Paid;
            List<OrderDetail> OrderDetailsList = _unitOfWork.OrderDetails.GetAllBy(x => x.OrderId == order.Id).ToList();
            foreach(OrderDetail od in OrderDetailsList)
            {
                DataGridViewRow row = (DataGridViewRow)DGVItems.RowTemplate.Clone();
                row.CreateCells(DGVItems, od.ProductId, od.ProductId, "قطعة", od.Price, od.Quantity, od.NetAfterTax);
                //row.Cells[0].Value = 50.2;
                DGVItems.Rows.Add(row);
                ClacRow(row);
                CalcTotalInv();
            }
            base.GetData();
        }
        public override void SetData()
        {
            order.OrderBarcode = txtInvCode.Text;
            order.OrderNumber = txtInvCode.Text.ToInt();
            order.OrderType = _orderType;
            order.Date = DTInvDate.Value;
            order.AccountId = (int)combAccount.SelectedValue;
            order.StoreId = (int)combStore.SelectedValue;
            order.BranchId = Info.CurrenBranch.BranchId;
            order.ToStoreId = (int)combStore.SelectedValue;

            order.NetBeforeTax = txtInvTotal.Value.ToDouble();
            order.NetInvoice = txtInvNet.Value.ToDouble();
            order.TotalVat = txtInvTax.Value.ToDouble();
            order.TotalDiscount = txtInvDesc.Value.ToDouble();
            order.DiscountRate = (order.TotalDiscount / order.NetBeforeTax)*100;

            order.Paid = txtInvNet.Value.ToDouble();
            order.Rest = 0;
            order.TotalCost = 0;
            order.TotalProfit = 0;
            
            List<OrderDetail> OrderDetailsList = new List<OrderDetail>();
            foreach (DataGridViewRow row in DGVItems.Rows)
            {
                int itemId = (int)Convert.ToInt32(row.Cells["Id"].Value.ToString());
                Product item = _unitOfWork.Products.GetAllBy(x => x.Id == itemId, new string[] { "ProductUnites" }).FirstOrDefault();
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = row.Cells["Id"].Value.ToInt(),
                    ProdcutUnitId = item.ProductUnites.FirstOrDefault().UnitId,
                    Quantity = row.Cells["Qty"].Value.ToDouble(),
                    Price = row.Cells["UnitValue"].Value.ToDouble(),
                    TotalPrice = row.Cells["ItemTotal"].Value.ToDouble(),
                    Discount = 0,
                    Extra = 0,
                    NetBeforeTax = row.Cells["ItemTotal"].Value.ToDouble(),
                    Vat = 0,
                    VatPrice = 0,
                    NetAfterTax = row.Cells["ItemTotal"].Value.ToDouble(),
                    Cost = order.OrderType == OrderType.Sale || order.OrderType == OrderType.SaleReturn ? row.Cells["Cost"].Value.ToDouble() : row.Cells["UnitValue"].Value.ToDouble()
                };
                OrderDetailsList.Add(orderDetail);
            };
            order.OrderDetails = OrderDetailsList;

            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (order.Id == 0)
            {
                _unitOfWork.Orders.Add(order);
                List<Invintory> invintoryList = new List<Invintory>();
                foreach (OrderDetail detail in order.OrderDetails)
                {
                    Product item = _unitOfWork.Products.GetAllBy(x=>x.Id == (int)detail.ProductId , new string[] { "ProductUnites" }).FirstOrDefault();
                    Invintory invintory = new Invintory()
                    {
                        OrderId = order.Id,
                        Date = order.Date,
                        ProductUnitId = item.ProductUnites.FirstOrDefault().UnitId,
                        OrderNumber = order.Id,
                        StoreId = order.StoreId,
                        StoreToId = order.StoreId,
                        ProductId = (int)detail.ProductId,
                        OrderType = order.OrderType,
                        Qty = detail.Quantity,
                        Cost = order.OrderType == OrderType.Purcahse || order.OrderType == OrderType.SaleReturn  || order.OrderType == OrderType.InitialBalance? detail.Price : item.SmallUnitCost,
                    };
                    _unitOfWork.Invintory.Add(invintory);
                    //invintoryList.Add(invintory);
                    if (order.OrderType == OrderType.Purcahse || order.OrderType == OrderType.InitialBalance || order.OrderType == OrderType.PurchaseReturn)
                    {
                       item.SmallUnitCost = GetAverage(item.Id);
                        _unitOfWork.Products.Update(item);
                    }
                }
               // _unitOfWork.Invintory.AddRange(invintoryList);
                Mess.Save();
                New();
                this.Refresh();
            }

            else
                _unitOfWork.Orders.Update(order);
            base.Save();
        }

        public override void Refresh()
        {
            //DGVItems.DataSource = _unitOfWork.Accounts.GetAll();
            AccountType a = (AccountType)Enum.Parse(typeof(AccountType), accountType);
            Fill(combAccount, _unitOfWork.Accounts.GetAllBy(x => x.AccountType == (AccountType)AccountTypeInt));
            Fill(combStore, _unitOfWork.Stores.GetAll());
            Fill(combItems, _unitOfWork.Products.GetAll());
            base.Refresh();
        }

        private void combItems_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int ItemId = 0;
            try
            {
                if (combItems.SelectedValue != null)
                {
                    ItemId = (int)combItems.SelectedValue;
                    GetItemData(ItemId);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void GetItemData(int id)
        {
            var item = _unitOfWork.Products.GetTById(id);
            txtUnitValue.Value = item.SalePrice;
            txtQty.Value = 1;
            //textDescItem.Text = "0";

        }
        private void CalcNewRow()
        {
            //var qty = 
            txtItemTotal.Value = ((decimal)txtQty.Value * (decimal)txtUnitValue.Value);
            //MessageBox.Show(txtQty.Value.ToString());
        }
        private void ClacRow(DataGridViewRow Row)
        {
            decimal qty = 0;
            decimal price = 0;
            if (Row != null)
            {
                qty = Row.Cells["Qty"].Value.ToDecimal();
                price = Row.Cells["UnitValue"].Value.ToDecimal();
                Row.Cells["ItemTotal"].Value = (qty * price);
            }
        }

        private void CalcTotalInv()
        {
            decimal Total = 0;
            decimal desc = txtInvDesc.Value;
            decimal tax = txtInvTax.Value;
            decimal net = 0;
            if (DGVItems.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DGVItems.Rows)
                {
                    Total = Total + row.Cells[5].Value.ToDecimal();
                }

                txtInvTotal.Value = Total;
                txtInvNet.Value = (Total - desc) + ((tax / 100) * ((Total - desc)));
            }
            else
            {
                txtInvDesc.Value = 0;
                net = (Total - desc) + ((tax / 100) * ((Total - desc)));
                if (net > 0)
                    txtInvNet.Value = net;
                else
                    txtInvNet.Value = 0;
            }
        }
        private void AddNewItem(int ItemId)
        {
            var AvailableInStore = Info._setting.UseStoreBalance == true? getAvailableQty(ItemId) : 10;
            if(_orderType == OrderType.Sale || _orderType == OrderType.PurchaseReturn)
            {
                if (txtQty.Value > (decimal)AvailableInStore)
                {
                    Mess.Warning(AvailableInStore.ToString() + " الكمية المتاحة في المخزن لاتكفي متاح عدد");
                    return;
                }
                foreach (DataGridViewRow r in DGVItems.Rows)
                    if (r.Cells[0].Value.ToInt() == ItemId)
                    {
                        if (txtQty.Value + r.Cells["Qty"].Value.ToDecimal() > (decimal)AvailableInStore)
                        {
                            Mess.Warning(AvailableInStore.ToString() + " الكمية المتاحة في المخزن لاتكفي متاح عدد");
                            r.Cells["Qty"].Value = r.Cells["Qty"].Value.ToDecimal() + ((decimal)AvailableInStore- r.Cells["Qty"].Value.ToDecimal());
                        }
                        else
                        {
                            r.Cells["Qty"].Value = r.Cells["Qty"].Value.ToDecimal() + txtQty.Value;
                        }
                        ClacRow(r);
                        CalcTotalInv();
                        return;
                    }
                item = _unitOfWork.Products.GetTById(ItemId);
                DataGridViewRow row = (DataGridViewRow)DGVItems.RowTemplate.Clone();
                row.CreateCells(DGVItems, item.Id, item.Name, "قطعة", txtUnitValue.Value, txtQty.Value, txtItemTotal.Value , item.SmallUnitCost);
                //row.Cells[0].Value = 50.2;
                DGVItems.Rows.Add(row);
                ClacRow(row);
                CalcTotalInv();
            }
            else
            {
                foreach (DataGridViewRow r in DGVItems.Rows)
                    if (r.Cells[0].Value.ToInt() == ItemId)
                    {
                        r.Cells["Qty"].Value = r.Cells["Qty"].Value.ToDecimal() + txtQty.Value;
                        ClacRow(r);
                        CalcTotalInv();
                        return;
                    }
                item = _unitOfWork.Products.GetTById(ItemId);
                DataGridViewRow row = (DataGridViewRow)DGVItems.RowTemplate.Clone();
                row.CreateCells(DGVItems, item.Id, item.Name, "قطعة", txtUnitValue.Value, txtQty.Value, txtItemTotal.Value);
                //row.Cells[0].Value = 50.2;
                DGVItems.Rows.Add(row);
                ClacRow(row);
                CalcTotalInv();
            }
            

            
        }

        private double getAvailableQty(int id)
        {
            double inns = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Purcahse || x.OrderType == OrderType.SaleReturn).Where(z=>z.ProductId == id).Sum(y => y.Qty);
            double outs = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Sale || x.OrderType == OrderType.PurchaseReturn).Where(z=>z.ProductId == id).Sum(y => y.Qty);
            var avilable = inns - outs;
            return avilable;
            // مجموع اسعار الشر + مجموع مرتج المبيعات  - المبيعات + مرتجع المشتريات
            //costin - priceout   شرا او مرتجع شر اة اصناف بداية فترة
            //nin - nout
        }

        private double GetAverage(int id)
        {
            double inns = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Purcahse || x.OrderType == OrderType.SaleReturn || x.OrderType == OrderType.InitialBalance).Where(z => z.ProductId == id).Sum(y => y.Qty);
            double innsCost = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Purcahse || x.OrderType == OrderType.SaleReturn || x.OrderType == OrderType.InitialBalance).Where(z => z.ProductId == id).Sum(y => y.Cost * y.Qty);

            double outs = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Sale || x.OrderType == OrderType.PurchaseReturn).Where(z => z.ProductId == id).Sum(y => y.Qty);
            double outsCost = _unitOfWork.Invintory.GetAllBy(x => x.OrderType == OrderType.Sale || x.OrderType == OrderType.PurchaseReturn).Where(z => z.ProductId == id).Sum(y => y.Cost * y.Qty);

            double QtyAvailable = inns - outs;
            double TotalCost = innsCost - outsCost;

            return TotalCost/ QtyAvailable;
        }




        private void txtUnitValue_ValueChanged(object sender, EventArgs e)
        {
            CalcNewRow();
        }

        private void txtQty_ValueChanged(object sender, EventArgs e)
        {
            CalcNewRow();
        }

        private void btnEx1_Click(object sender, EventArgs e)
        {
            var ItemId = (int)combItems.SelectedValue;
            AddNewItem(ItemId);
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            this.MasterCompo.Visible = false;
            this.toolStripSplitButton1.Visible = false;
            this.toolStripSplitButton2.Visible = false;
            this.toolStripButton5.Visible = false;
            this.toolStripButton4.Visible = false;
            this.toolStripTextBox1.Visible = false;
            this.toolStripLabel1.Visible = false;
            this.compBranch.Visible = true;
            this.lblBranch.Visible = true;

            Fill(this.compBranch.ComboBox, _unitOfWork.Branchs.GetAll());
            //Purcahse, SaleReturn, PurchaseReturn, Transfer
            switch (_orderType)
            {
                case OrderType.Sale:
                  this.Text = "فاتورة مبيعات";
                    labelAccount.Text = "العميل";
                    AccountTypeInt = (int)AccountType.Customer;
                    break;
                case OrderType.SaleReturn:
                    this.Text = "فاتورة مرتجع بيع";
                    labelAccount.Text = "العميل";
                    AccountTypeInt = (int)AccountType.Customer;
                    break;
                case OrderType.Purcahse:
                    this.Text = "فاتورة مشتريات";
                    labelAccount.Text = "المورد";
                    AccountTypeInt = (int)AccountType.supplier;
                    break;
                case OrderType.PurchaseReturn:
                    this.Text = "فاتورة مرتجع شراء";
                    labelAccount.Text = "المورد";
                    AccountTypeInt = (int)AccountType.supplier;
                    break;
                case OrderType.InitialBalance:
                    this.Text = "ارصده افتتاحيه";
                    break;
                case OrderType.Transfer:
                    this.Text = "تحويل مخزون";
                    break;
                case OrderType.Destroy:
                    this.Text = "هالك اصناف" ;
                    break;
                default:
                   
                    break;
            }
            Refresh();
            GetData();
            GetNewCode();
            labelTitle.Text = this.Text;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                item = _unitOfWork.Products.GetTBy(x => x.Code == txtBarcode.Text);
                if (item == null)
                {
                    MessageBox.Show("لا يوجد صنف مسجل لهذا الباركود");
                    txtBarcode.Clear();
                    return;
                }
                GetItemData(item.Id);
                AddNewItem(item.Id);
                txtBarcode.Clear();
            }
        }

        private void DGVItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalcTotalInv();
        }

        private void DGVItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcTotalInv();
        }

        private void reinitialize()
        {
            //frmInvoice NewForm = new frmInvoice(_invType);
            //NewForm.Show();
            //this.Dispose(false);
        }


        public override void GetNewCode()
        {
            string maxCode;
            //int ordertypeint = (int)Enum.Parse(typeof(OrderType), _invType);
            maxCode = _context.orders.Where(xx => xx.OrderType == _orderType && xx.BranchId == Info.CurrenBranch.Id).Select(x => x.OrderBarcode).Max();
            txtInvCode.Text =  GetNextNumberInString(maxCode);
            base.GetNewCode();
        }
        string GetNextNumberInString(string number)
        {
            if (number == string.Empty || number == null)
            {
                return "1";
            }
            string str1 = "";
            foreach (char c in number)
                str1 = char.IsDigit(c) ? str1 + c.ToString() : "";
            if (str1 == string.Empty)
                return number = "1";
            string str2 = str1.Insert(0, "1");
            str2 = (Convert.ToInt32(str2) + 1).ToString();
            string str3 = str2[0] == '1' ? str2.Remove(0, 1) : str2.Remove(0, 1).Insert(0, "1");
            int index = number.LastIndexOf(str1);
            number = number.Remove(index);
            number = number.Insert(index, str3);
            return number;
        }

        private void txtInvDesc_ValueChanged(object sender, EventArgs e)
        {
            CalcTotalInv();
        }

        private void txtInvTax_ValueChanged(object sender, EventArgs e)
        {
            CalcTotalInv();
        }

        private void BtnOpenProductsSearch_Click(object sender, EventArgs e)
        {
            Info._ProductSelected = false;
            Info._SelectedProduct = 0;
            var frm = new frmProductSearch();
            if(frm.ShowDialog()== DialogResult.OK)
            {
                if (Info._SelectedProduct != 0)
                {
                    combItems.SelectedValue = Info._SelectedProduct;
                    txtUnitValue.Focus();
                    txtUnitValue.Select(0, txtUnitValue.Text.Length); ;
                    //GetItemData(Info._SelectedProduct);
                    //AddNewItem(Info._SelectedProduct);
                }
            }
        }
    }
}
