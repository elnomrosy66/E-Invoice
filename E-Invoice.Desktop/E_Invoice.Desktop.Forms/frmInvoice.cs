using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Data;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmInvoice : Master
{
	public OrderType _orderType;

	public Order order;

	public OrderDetail OrderDetails;

	public Product item;

	private string accountType = "Customer";

	private int AccountTypeInt = 0;

	private readonly ApplicationDBContext _context;

	private IContainer components = null;

	private DataGridView DGVItems;

	private ComboBoxEx combAccount;

	private GroupBox groupBox1;

	private ComboBoxEx combStore;

	private GroupBox groupBox2;

	private DateTimePickerEx DTInvDate;

	private TextBoxEx txtInvCode;

	private LabelEx labelEx6;

	private LabelEx labelEx5;

	private LabelEx labelEx3;

	private GroupBox groupBox3;

	private LabelEx labelEx10;

	private LabelEx labelEx8;

	private LabelEx labelEx7;

	private TextBoxEx txtBarcode;

	private LabelEx labelEx2;

	private LabelEx labelEx1;

	private NumericUpDown txtItemTotal;

	private NumericUpDown txtUnitValue;

	private NumericUpDown txtQty;

	private LabelEx labelEx11;

	private NumericUpDown txtInvTotal;

	private NumericUpDown txtInvDesc;

	private NumericUpDown txtInvTax;

	private NumericUpDown txtInvNet;

	private LabelEx labelEx12;

	private LabelEx labelEx13;

	private LabelEx labelTitle;

	private LabelEx labelEx14;

	private ComboBoxEx combItems;

	private Button BtnOpenProductsSearch;

	private Button BtnOpenAccountsSearch;

	private LabelEx labelEx4;

	private TextBoxEx txtCustomerCity;

	private TextBoxEx txtCustomerGov;

	private TextBoxEx txtCustomerName;

	private LabelEx labelEx9;

	private CheckBox checkBox1;

	private LabelEx labelEx15;

	private TextBoxEx txtCustomerId;

	private GroupBox groupBox4;

	private TextBoxEx txtProductDesc;

	private btnEx btnEx1;

	private DataGridViewTextBoxColumn Id;

	private DataGridViewTextBoxColumn Barcode;

	private new DataGridViewTextBoxColumn Name;

	private DataGridViewTextBoxColumn Unit;

	private DataGridViewTextBoxColumn UnitValue;

	private DataGridViewTextBoxColumn Qty;

	private DataGridViewTextBoxColumn ItemTotal;

	private DataGridViewTextBoxColumn Cost;

	private NumericUpDown Tax4;

	private LabelEx labelEx16;

	private Button button1;

	private LabelEx labelEx17;

	private TextBoxEx textBoxEx1;

	private GroupBox groupBox5;

	private Button button2;

	private LabelEx labelEx19;

	private TextBoxEx txtCustomerBuilding;

	private LabelEx labelEx18;

	private TextBoxEx txtCustomerSt;

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
		order = _unitOfWork.Orders.GetTBy((Order x) => x.Id == id);
		item = new Product();
		_orderType = order.OrderType;
		InitializeComponent();
	}

	public override void New()
	{
		order = new Order
		{
			Date = DateTime.Now,
			TotalVat = 14.0,
			Tax4 = 0.0,
			OrderType = _orderType
		};
		OrderDetails = new OrderDetail();
		GetData();
		GetNewCode();
		DGVItems.Rows.Clear();
	}

	public override void GetData()
	{
		txtInvCode.Text = order.OrderBarcode;
		_orderType = order.OrderType;
		DTInvDate.Value = Convert.ToDateTime(order.Date.ToShortDateString());
		if (order.AccountId.HasValue)
		{
			combAccount.SelectedValue = order.AccountId;
		}
		if (order.StoreId != 0)
		{
			combStore.SelectedValue = order.StoreId;
		}
		txtInvTotal.Value = (decimal)order.NetBeforeTax;
		txtInvTax.Value = (decimal)order.TotalVat;
		txtInvDesc.Value = (decimal)order.TotalDiscount;
		txtInvNet.Value = (decimal)order.Paid;
		Tax4.Value = (decimal)order.Tax4;
		List<OrderDetail> list = _unitOfWork.OrderDetails.GetAllBy((OrderDetail x) => x.OrderId == order.Id).ToList();
		foreach (OrderDetail item in list)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)DGVItems.RowTemplate.Clone();
			dataGridViewRow.CreateCells(DGVItems, item.ProductId, item.ProductId, "قطعة", item.Price, item.Quantity, item.NetAfterTax);
			DGVItems.Rows.Add(dataGridViewRow);
			ClacRow(dataGridViewRow);
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
		order.DiscountRate = order.TotalDiscount / order.NetBeforeTax * 100.0;
		order.Paid = txtInvNet.Value.ToDouble();
		order.Rest = 0.0;
		order.TotalCost = 0.0;
		order.TotalProfit = 0.0;
		order.Tax4 = (double)Tax4.Value;
		if (order.CustomerNakdy)
		{
			order.CustumerName = txtCustomerName.Text;
			order.CustomerId = txtCustomerId.Text;
			order.CustumerCity = txtCustomerCity.Text;
			order.CustumerGovernate = txtCustomerGov.Text;
			order.CustumerGovernate = txtCustomerGov.Text;
			order.CustumerGovernate = txtCustomerGov.Text;
			order.CustumerStreet = txtCustomerSt.Text;
			order.CustumerBuilding = txtCustomerBuilding.Text;
		}
		else
		{
			order.CustumerName = " ";
			order.CustomerId = " ";
			order.CustumerCity = " ";
			order.CustumerGovernate = " ";
			order.CustumerStreet = " ";
			order.CustumerBuilding = " ";
		}
		List<OrderDetail> list = new List<OrderDetail>();
		foreach (DataGridViewRow item in (IEnumerable)DGVItems.Rows)
		{
			int itemId = Convert.ToInt32(item.Cells["Id"].Value.ToString());
			Product product = _unitOfWork.Products.GetAllBy((Product x) => x.Id == itemId, new string[1] { "ProductUnites" }).FirstOrDefault();
			OrderDetail orderDetail = new OrderDetail
			{
				ProductId = item.Cells["Id"].Value.ToInt(),
				ProdcutUnitId = product.ProductUnites.FirstOrDefault().UnitId,
				Quantity = item.Cells["Qty"].Value.ToDouble(),
				Price = item.Cells["UnitValue"].Value.ToDouble(),
				TotalPrice = item.Cells["ItemTotal"].Value.ToDouble(),
				Discount = 0.0,
				Extra = 0.0,
				NetBeforeTax = item.Cells["ItemTotal"].Value.ToDouble(),
				Vat = 0.0,
				VatPrice = 0.0,
				NetAfterTax = item.Cells["ItemTotal"].Value.ToDouble(),
				Cost = ((order.OrderType == OrderType.Sale || order.OrderType == OrderType.SaleReturn) ? item.Cells["Cost"].Value.ToDouble() : item.Cells["UnitValue"].Value.ToDouble()),
				ProductDesc = item.Cells["Name"].Value.ToString()
			};
			list.Add(orderDetail);
		}
		order.OrderDetails = list;
		base.SetData();
	}

	public bool validate()
	{
		if (combAccount.SelectedValue == null || !(combAccount.SelectedValue is int))
		{
			return false;
		}
		if (txtCustomerName.Text == string.Empty)
		{
			return false;
		}
		if (txtCustomerCity.Text == string.Empty)
		{
			return false;
		}
		if (txtCustomerGov.Text == string.Empty)
		{
			return false;
		}
		if (txtCustomerId.Text == string.Empty)
		{
			return false;
		}
		if (DGVItems.Rows.Count == 0)
		{
			return false;
		}
		if (txtInvNet.Value <= 0m)
		{
			return false;
		}
		return true;
	}

	public override void Save()
	{
		if (!validate())
		{
			Mess.Warning("خطاء في بيانات الفاتورة لا يمكن الحفظ الرجاء امراجعة مرة اخري");
			return;
		}
		SetData();
		if (order.Id == 0)
		{
			_unitOfWork.Orders.Add(order);
			List<Invintory> list = new List<Invintory>();
			foreach (OrderDetail detail in order.OrderDetails)
			{
				Product product = _unitOfWork.Products.GetAllBy((Product x) => x.Id == (int)detail.ProductId, new string[1] { "ProductUnites" }).FirstOrDefault();
				Invintory entity = new Invintory
				{
					OrderId = order.Id,
					Date = order.Date,
					ProductUnitId = product.ProductUnites.FirstOrDefault().UnitId,
					OrderNumber = order.Id,
					StoreId = order.StoreId,
					StoreToId = order.StoreId,
					ProductId = detail.ProductId.Value,
					OrderType = order.OrderType,
					Qty = detail.Quantity,
					Cost = ((order.OrderType == OrderType.Purcahse || order.OrderType == OrderType.SaleReturn || order.OrderType == OrderType.InitialBalance) ? detail.Price : product.SmallUnitCost)
				};
				_unitOfWork.Invintory.Add(entity);
				if (order.OrderType == OrderType.Purcahse || order.OrderType == OrderType.InitialBalance || order.OrderType == OrderType.PurchaseReturn)
				{
					product.SmallUnitCost = GetAverage(product.Id);
					_unitOfWork.Products.Update(product);
				}
			}
			Mess.Save();
			New();
			Refresh();
		}
		else
		{
			_unitOfWork.Orders.Update(order);
		}
		base.Save();
	}

	public override void Refresh()
	{
		AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), this.accountType);
		Fill(combAccount, _unitOfWork.Accounts.GetAllBy((Account x) => (int)x.AccountType == (int)(AccountType)AccountTypeInt));
		Fill(combStore, _unitOfWork.Stores.GetAll());
		Fill(combItems, _unitOfWork.Products.GetAll());
		base.Refresh();
	}

	private void combItems_SelectedIndexChanged_1(object sender, EventArgs e)
	{
		int num = 0;
		try
		{
			if (combItems.SelectedValue != null)
			{
				num = (int)combItems.SelectedValue;
				GetItemData(num);
			}
		}
		catch (Exception)
		{
		}
	}

	private void GetItemData(int id)
	{
		Product tById = _unitOfWork.Products.GetTById(id);
		txtUnitValue.Value = tById.SalePrice;
		txtQty.Value = 1m;
		txtProductDesc.Text = tById.Name;
	}

	private void CalcNewRow()
	{
		txtItemTotal.Value = txtQty.Value * txtUnitValue.Value;
	}

	private void ClacRow(DataGridViewRow Row)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		if (Row != null)
		{
			num = Row.Cells["Qty"].Value.ToDecimal();
			num2 = Row.Cells["UnitValue"].Value.ToDecimal();
			Row.Cells["ItemTotal"].Value = num * num2;
		}
	}

	private void CalcTotalInv()
	{
		decimal num = default(decimal);
		decimal value = txtInvDesc.Value;
		decimal value2 = txtInvTax.Value;
		decimal value3 = Tax4.Value;
		decimal num2 = default(decimal);
		if (DGVItems.Rows.Count > 0)
		{
			foreach (DataGridViewRow item in (IEnumerable)DGVItems.Rows)
			{
				num += item.Cells[6].Value.ToDecimal();
			}
			txtInvTotal.Value = num;
			txtInvNet.Value = num - value + (value2 - value3) / 100m * (num - value);
		}
		else
		{
			txtInvDesc.Value = 0m;
			num2 = num - value + value2 / 100m * (num - value);
			if (num2 > 0m)
			{
				txtInvNet.Value = num2;
			}
			else
			{
				txtInvNet.Value = 0m;
			}
		}
	}

	private void AddNewItem(int ItemId)
	{
		double num = (Info._setting.UseStoreBalance ? getAvailableQty(ItemId) : 100000.0);
		if (_orderType == OrderType.Sale || _orderType == OrderType.PurchaseReturn)
		{
			if (txtQty.Value > (decimal)num)
			{
				Mess.Warning(num + " الكمية المتاحة في المخزن لاتكفي متاح عدد");
				return;
			}
			item = _unitOfWork.Products.GetTById(ItemId);
			DataGridViewRow dataGridViewRow = (DataGridViewRow)DGVItems.RowTemplate.Clone();
			dataGridViewRow.CreateCells(DGVItems, item.Id, item.Code, txtProductDesc.Text, "قطعة", txtUnitValue.Value, txtQty.Value, txtItemTotal.Value, item.SmallUnitCost);
			DGVItems.Rows.Add(dataGridViewRow);
			ClacRow(dataGridViewRow);
			CalcTotalInv();
			return;
		}
		foreach (DataGridViewRow item in (IEnumerable)DGVItems.Rows)
		{
			if (item.Cells[0].Value.ToInt() == ItemId)
			{
				item.Cells["Qty"].Value = item.Cells["Qty"].Value.ToDecimal() + txtQty.Value;
				ClacRow(item);
				CalcTotalInv();
				return;
			}
		}
		this.item = _unitOfWork.Products.GetTById(ItemId);
		DataGridViewRow dataGridViewRow3 = (DataGridViewRow)DGVItems.RowTemplate.Clone();
		dataGridViewRow3.CreateCells(DGVItems, this.item.Id, this.item.Name, "قطعة", txtUnitValue.Value, txtQty.Value, txtItemTotal.Value);
		DGVItems.Rows.Add(dataGridViewRow3);
		ClacRow(dataGridViewRow3);
		CalcTotalInv();
	}

	private double getAvailableQty(int id)
	{
		double num = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 1 || (int)x.OrderType == 2)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Qty);
		double num2 = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 0 || (int)x.OrderType == 3)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Qty);
		return num - num2;
	}

	private double GetAverage(int id)
	{
		double num = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 1 || (int)x.OrderType == 2 || (int)x.OrderType == 5)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Qty);
		double num2 = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 1 || (int)x.OrderType == 2 || (int)x.OrderType == 5)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Cost * y.Qty);
		double num3 = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 0 || (int)x.OrderType == 3)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Qty);
		double num4 = (from z in _unitOfWork.Invintory.GetAllBy((Invintory x) => (int)x.OrderType == 0 || (int)x.OrderType == 3)
			where z.ProductId == id
			select z).Sum((Invintory y) => y.Cost * y.Qty);
		double num5 = num - num3;
		double num6 = num2 - num4;
		return num6 / num5;
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
		int itemId = (int)combItems.SelectedValue;
		AddNewItem(itemId);
	}

	private void frmInvoice_Load(object sender, EventArgs e)
	{
		MasterCompo.Visible = false;
		toolStripSplitButton1.Visible = false;
		toolStripSplitButton2.Visible = false;
		toolStripButton5.Visible = false;
		toolStripButton4.Visible = false;
		toolStripTextBox1.Visible = false;
		toolStripLabel1.Visible = false;
		compBranch.Visible = true;
		lblBranch.Visible = true;
		combAccount.DropDownStyle = ComboBoxStyle.DropDown;
		combAccount.AutoCompleteSource = AutoCompleteSource.ListItems;
		combAccount.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		combAccount.SelectedIndexChanged += new EventHandler(combAccount_SelectedIndexChanged);
		Fill(compBranch.ComboBox, _unitOfWork.Branchs.GetAll());
		switch (_orderType)
		{
		case OrderType.Sale:
			Text = "فاتورة مبيعات";
			AccountTypeInt = 0;
			break;
		case OrderType.SaleReturn:
			Text = "فاتورة مرتجع بيع";
			AccountTypeInt = 0;
			break;
		case OrderType.Purcahse:
			Text = "فاتورة مشتريات";
			AccountTypeInt = 1;
			break;
		case OrderType.PurchaseReturn:
			Text = "فاتورة مرتجع شراء";
			AccountTypeInt = 1;
			break;
		case OrderType.InitialBalance:
			Text = "ارصده افتتاحيه";
			break;
		case OrderType.Transfer:
			Text = "تحويل مخزون";
			break;
		case OrderType.Destroy:
			Text = "هالك اصناف";
			break;
		}
		Refresh();
		GetData();
		GetNewCode();
		labelTitle.Text = Text;
	}

	private void combAccount_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (combAccount.SelectedValue != null && combAccount.SelectedValue is int accountId)
			{
				var account = _unitOfWork.Accounts.GetTById(accountId);
				if (account != null)
				{
					txtCustomerName.Text = account.Name;
					txtCustomerCity.Text = account.RegionCity ?? "";
					txtCustomerGov.Text = account.Governate ?? "";
					txtCustomerId.Text = account.TaxReg ?? "";
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			item = _unitOfWork.Products.GetTBy((Product x) => x.Code == txtBarcode.Text);
			if (item == null)
			{
				MessageBox.Show("لا يوجد صنف مسجل لهذا الباركود");
				txtBarcode.Clear();
			}
			else
			{
				GetItemData(item.Id);
				AddNewItem(item.Id);
				txtBarcode.Clear();
			}
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
	}

	public override void GetNewCode()
	{
		string number = (from x in _context.orders
			where (int)x.OrderType == (int)_orderType && x.BranchId == Info.CurrenBranch.Id
			select x.OrderBarcode).Max();
		txtInvCode.Text = GetNextNumberInString(number);
		base.GetNewCode();
	}

	private string GetNextNumberInString(string number)
	{
		if (number == string.Empty || number == null)
		{
			return "1";
		}
		string text = "";
		string text2 = number;
		for (int i = 0; i < text2.Length; i++)
		{
			char c = text2[i];
			text = (char.IsDigit(c) ? (text + c) : "");
		}
		if (text == string.Empty)
		{
			return number = "1";
		}
		string value = text.Insert(0, "1");
		value = (Convert.ToInt32(value) + 1).ToString();
		string value2 = ((value[0] == '1') ? value.Remove(0, 1) : value.Remove(0, 1).Insert(0, "1"));
		int startIndex = number.LastIndexOf(text);
		number = number.Remove(startIndex);
		number = number.Insert(startIndex, value2);
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
		frmProductSearch frmProductSearch2 = new frmProductSearch();
		if (frmProductSearch2.ShowDialog() == DialogResult.OK && Info._SelectedProduct != 0)
		{
			combItems.SelectedValue = Info._SelectedProduct;
			txtUnitValue.Focus();
			txtUnitValue.Select(0, txtUnitValue.Text.Length);
		}
	}

	private void BtnOpenAccountsSearch_Click(object sender, EventArgs e)
	{
		Info._SelectedAccount = 0;
		AccountType accountType = (AccountType)AccountTypeInt;
		frmAccountSearch frmAccountSearch2 = new frmAccountSearch(accountType);
		if (frmAccountSearch2.ShowDialog() == DialogResult.OK && Info._SelectedAccount != 0)
		{
			combAccount.SelectedValue = Info._SelectedAccount;
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void checkBox1_CheckStateChanged(object sender, EventArgs e)
	{
		if (checkBox1.CheckState == CheckState.Checked)
		{
			groupBox4.Visible = true;
			order.CustomerNakdy = true;
			combAccount.SelectedValue = _unitOfWork.Accounts.GetTBy((Account x) => x.Name == "نقدي").Id;
		}
		else
		{
			groupBox4.Visible = false;
			order.CustomerNakdy = false;
		}
	}

	private void Tax4_ValueChanged(object sender, EventArgs e)
	{
		if (checkBox1.CheckState == CheckState.Checked)
		{
			if (Tax4.Value > 0m && Mess.Ask("الفاتورة الحالية مسجلة لعميل نقدي لايمكن اضافة ضريبة خصم هل تريد متابعة اضافة الضريبة") == DialogResult.Yes)
			{
				CalcTotalInv();
			}
		}
		else
		{
			CalcTotalInv();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.Sale, OrderEinvSend.Sent);
		frmInvoices2.ShowDialog();
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVItems = new System.Windows.Forms.DataGridView();
		this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.UnitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ItemTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.labelEx19 = new E_Invoice.Desktop.Controls.LabelEx();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.txtCustomerBuilding = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtCustomerCity = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtCustomerName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx18 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx9 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtCustomerId = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx15 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtCustomerSt = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.txtCustomerGov = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx4 = new E_Invoice.Desktop.Controls.LabelEx();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtInvCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.button1 = new System.Windows.Forms.Button();
		this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx17 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx5 = new E_Invoice.Desktop.Controls.LabelEx();
		this.DTInvDate = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.textBoxEx1 = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.combStore = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.labelEx6 = new E_Invoice.Desktop.Controls.LabelEx();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.labelTitle = new E_Invoice.Desktop.Controls.LabelEx();
		this.combAccount = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.combItems = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txtProductDesc = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.BtnOpenProductsSearch = new System.Windows.Forms.Button();
		this.BtnOpenAccountsSearch = new System.Windows.Forms.Button();
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
		this.Tax4 = new System.Windows.Forms.NumericUpDown();
		this.labelEx16 = new E_Invoice.Desktop.Controls.LabelEx();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvDesc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tax4).BeginInit();
		base.SuspendLayout();
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Columns.AddRange(this.Id, this.Barcode, this.Name, this.Unit, this.UnitValue, this.Qty, this.ItemTotal, this.Cost);
		this.DGVItems.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVItems.Location = new System.Drawing.Point(3, 17);
		this.DGVItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(1038, 249);
		this.DGVItems.TabIndex = 1;
		this.DGVItems.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(DGVItems_RowsAdded);
		this.DGVItems.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(DGVItems_RowsRemoved);
		this.Id.HeaderText = "كود الصنف";
		this.Id.MinimumWidth = 6;
		this.Id.Name = "Id";
		this.Id.ReadOnly = true;
		this.Id.Visible = false;
		this.Barcode.HeaderText = "الباركود";
		this.Barcode.MinimumWidth = 6;
		this.Barcode.Name = "Barcode";
		this.Barcode.ReadOnly = true;
		this.Name.HeaderText = "اسم الصنف";
		this.Name.MinimumWidth = 6;
		this.Name.Name = "Name";
		this.Name.ReadOnly = true;
		this.Unit.HeaderText = "الوحدة";
		this.Unit.MinimumWidth = 6;
		this.Unit.Name = "Unit";
		this.Unit.ReadOnly = true;
		this.UnitValue.HeaderText = "سعر الوحدة";
		this.UnitValue.MinimumWidth = 6;
		this.UnitValue.Name = "UnitValue";
		this.UnitValue.ReadOnly = true;
		this.Qty.HeaderText = "الكمية";
		this.Qty.MinimumWidth = 6;
		this.Qty.Name = "Qty";
		this.Qty.ReadOnly = true;
		this.ItemTotal.HeaderText = "الإجمالي";
		this.ItemTotal.MinimumWidth = 6;
		this.ItemTotal.Name = "ItemTotal";
		this.ItemTotal.ReadOnly = true;
		this.Cost.HeaderText = "التكلفة للوحدة";
		this.Cost.MinimumWidth = 6;
		this.Cost.Name = "Cost";
		this.Cost.ReadOnly = true;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.labelEx19);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.groupBox5);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.labelTitle);
		this.groupBox1.Controls.Add(this.combAccount);
		this.groupBox1.Controls.Add(this.combItems);
		this.groupBox1.Controls.Add(this.BtnOpenAccountsSearch);
		this.groupBox1.Location = new System.Drawing.Point(10, 25);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox1.Size = new System.Drawing.Size(1042, 166);
		this.groupBox1.TabIndex = 34;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات الفاتورة";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.labelEx19.Location = new System.Drawing.Point(307, 23);
		this.labelEx19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx19.Name = "labelEx19";
		this.labelEx19.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx19.Size = new System.Drawing.Size(79, 21);
		this.labelEx19.TabIndex = 52;
		this.labelEx19.Text = "العميل";
		this.labelEx19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox4.Controls.Add(this.button2);
		this.groupBox4.Controls.Add(this.txtCustomerBuilding);
		this.groupBox4.Controls.Add(this.txtCustomerCity);
		this.groupBox4.Controls.Add(this.txtCustomerName);
		this.groupBox4.Controls.Add(this.labelEx18);
		this.groupBox4.Controls.Add(this.labelEx9);
		this.groupBox4.Controls.Add(this.txtCustomerId);
		this.groupBox4.Controls.Add(this.labelEx15);
		this.groupBox4.Controls.Add(this.txtCustomerSt);
		this.groupBox4.Controls.Add(this.txtCustomerGov);
		this.groupBox4.Controls.Add(this.labelEx4);
		this.groupBox4.Location = new System.Drawing.Point(5, 44);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
		this.groupBox4.Size = new System.Drawing.Size(395, 119);
		this.groupBox4.TabIndex = 42;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "بيانات العميل";
		this.groupBox4.Visible = false;
		this.button2.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button2.Location = new System.Drawing.Point(9, 19);
		this.button2.Margin = new System.Windows.Forms.Padding(2);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(20, 19);
		this.button2.TabIndex = 51;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.txtCustomerBuilding.BackColor = System.Drawing.Color.White;
		this.txtCustomerBuilding.IsNumber = false;
		this.txtCustomerBuilding.Location = new System.Drawing.Point(48, 91);
		this.txtCustomerBuilding.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerBuilding.Name = "txtCustomerBuilding";
		this.txtCustomerBuilding.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerBuilding.Size = new System.Drawing.Size(113, 22);
		this.txtCustomerBuilding.TabIndex = 37;
		this.txtCustomerBuilding.Text = " ";
		this.txtCustomerCity.BackColor = System.Drawing.Color.White;
		this.txtCustomerCity.IsNumber = false;
		this.txtCustomerCity.Location = new System.Drawing.Point(48, 65);
		this.txtCustomerCity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerCity.Name = "txtCustomerCity";
		this.txtCustomerCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerCity.Size = new System.Drawing.Size(113, 22);
		this.txtCustomerCity.TabIndex = 37;
		this.txtCustomerCity.Text = " ";
		this.txtCustomerName.BackColor = System.Drawing.Color.White;
		this.txtCustomerName.IsNumber = false;
		this.txtCustomerName.Location = new System.Drawing.Point(47, 17);
		this.txtCustomerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerName.Name = "txtCustomerName";
		this.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerName.Size = new System.Drawing.Size(231, 22);
		this.txtCustomerName.TabIndex = 37;
		this.txtCustomerName.Text = " ";
		this.labelEx18.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelEx18.Location = new System.Drawing.Point(286, 94);
		this.labelEx18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx18.Name = "labelEx18";
		this.labelEx18.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx18.Size = new System.Drawing.Size(100, 15);
		this.labelEx18.TabIndex = 40;
		this.labelEx18.Text = "الشارع/المبني";
		this.labelEx18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx9.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelEx9.Location = new System.Drawing.Point(286, 68);
		this.labelEx9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx9.Name = "labelEx9";
		this.labelEx9.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx9.Size = new System.Drawing.Size(100, 15);
		this.labelEx9.TabIndex = 40;
		this.labelEx9.Text = "المحافظة / المدينة";
		this.labelEx9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCustomerId.BackColor = System.Drawing.Color.White;
		this.txtCustomerId.IsNumber = false;
		this.txtCustomerId.Location = new System.Drawing.Point(48, 40);
		this.txtCustomerId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerId.Name = "txtCustomerId";
		this.txtCustomerId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerId.Size = new System.Drawing.Size(231, 22);
		this.txtCustomerId.TabIndex = 37;
		this.txtCustomerId.Text = " ";
		this.labelEx15.Location = new System.Drawing.Point(301, 40);
		this.labelEx15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx15.Name = "labelEx15";
		this.labelEx15.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx15.Size = new System.Drawing.Size(80, 21);
		this.labelEx15.TabIndex = 40;
		this.labelEx15.Text = "الرقم القومي";
		this.labelEx15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCustomerSt.BackColor = System.Drawing.Color.White;
		this.txtCustomerSt.IsNumber = false;
		this.txtCustomerSt.Location = new System.Drawing.Point(165, 91);
		this.txtCustomerSt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerSt.Name = "txtCustomerSt";
		this.txtCustomerSt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerSt.Size = new System.Drawing.Size(113, 22);
		this.txtCustomerSt.TabIndex = 37;
		this.txtCustomerSt.Text = " ";
		this.txtCustomerGov.BackColor = System.Drawing.Color.White;
		this.txtCustomerGov.IsNumber = false;
		this.txtCustomerGov.Location = new System.Drawing.Point(165, 65);
		this.txtCustomerGov.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtCustomerGov.Name = "txtCustomerGov";
		this.txtCustomerGov.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCustomerGov.Size = new System.Drawing.Size(113, 22);
		this.txtCustomerGov.TabIndex = 37;
		this.txtCustomerGov.Text = " ";
		this.labelEx4.Location = new System.Drawing.Point(300, 17);
		this.labelEx4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx4.Name = "labelEx4";
		this.labelEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx4.Size = new System.Drawing.Size(79, 21);
		this.labelEx4.TabIndex = 40;
		this.labelEx4.Text = "اسم العميل";
		this.labelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.Controls.Add(this.txtInvCode);
		this.groupBox5.Controls.Add(this.button1);
		this.groupBox5.Controls.Add(this.labelEx3);
		this.groupBox5.Controls.Add(this.labelEx17);
		this.groupBox5.Controls.Add(this.labelEx5);
		this.groupBox5.Controls.Add(this.DTInvDate);
		this.groupBox5.Controls.Add(this.textBoxEx1);
		this.groupBox5.Controls.Add(this.combStore);
		this.groupBox5.Controls.Add(this.labelEx6);
		this.groupBox5.Location = new System.Drawing.Point(659, 18);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(368, 133);
		this.groupBox5.TabIndex = 51;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "معلومات الفاتورة";
		this.txtInvCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtInvCode.IsNumber = false;
		this.txtInvCode.Location = new System.Drawing.Point(30, 17);
		this.txtInvCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtInvCode.Name = "txtInvCode";
		this.txtInvCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtInvCode.Size = new System.Drawing.Size(231, 22);
		this.txtInvCode.TabIndex = 37;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.Location = new System.Drawing.Point(5, 72);
		this.button1.Margin = new System.Windows.Forms.Padding(2);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(20, 19);
		this.button1.TabIndex = 50;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.labelEx3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelEx3.Location = new System.Drawing.Point(264, 17);
		this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(71, 21);
		this.labelEx3.TabIndex = 33;
		this.labelEx3.Text = "كود الفاتورة";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelEx17.Location = new System.Drawing.Point(268, 99);
		this.labelEx17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx17.Name = "labelEx17";
		this.labelEx17.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx17.Size = new System.Drawing.Size(71, 21);
		this.labelEx17.TabIndex = 44;
		this.labelEx17.Text = "رقم المرجع";
		this.labelEx17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelEx5.Location = new System.Drawing.Point(279, 75);
		this.labelEx5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx5.Name = "labelEx5";
		this.labelEx5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx5.Size = new System.Drawing.Size(60, 16);
		this.labelEx5.TabIndex = 33;
		this.labelEx5.Text = "المخزن";
		this.labelEx5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.DTInvDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DTInvDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.DTInvDate.Location = new System.Drawing.Point(30, 46);
		this.DTInvDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.DTInvDate.Name = "DTInvDate";
		this.DTInvDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.DTInvDate.Size = new System.Drawing.Size(231, 22);
		this.DTInvDate.TabIndex = 38;
		this.textBoxEx1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBoxEx1.IsNumber = false;
		this.textBoxEx1.Location = new System.Drawing.Point(30, 98);
		this.textBoxEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.textBoxEx1.Name = "textBoxEx1";
		this.textBoxEx1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.textBoxEx1.Size = new System.Drawing.Size(231, 22);
		this.textBoxEx1.TabIndex = 43;
		this.textBoxEx1.Visible = false;
		this.combStore.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.combStore.Location = new System.Drawing.Point(30, 71);
		this.combStore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.combStore.Name = "combStore";
		this.combStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.combStore.Size = new System.Drawing.Size(231, 22);
		this.combStore.TabIndex = 32;
		this.labelEx6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelEx6.Location = new System.Drawing.Point(263, 46);
		this.labelEx6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx6.Name = "labelEx6";
		this.labelEx6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx6.Size = new System.Drawing.Size(76, 21);
		this.labelEx6.TabIndex = 33;
		this.labelEx6.Text = "تاريخ الاصدار";
		this.labelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(29, 22);
		this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.checkBox1.Size = new System.Drawing.Size(52, 18);
		this.checkBox1.TabIndex = 41;
		this.checkBox1.Text = "نقدي";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.checkBox1.CheckStateChanged += new System.EventHandler(checkBox1_CheckStateChanged);
		this.labelTitle.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelTitle.Location = new System.Drawing.Point(369, 9);
		this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelTitle.Name = "labelTitle";
		this.labelTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.labelTitle.Size = new System.Drawing.Size(245, 63);
		this.labelTitle.TabIndex = 39;
		this.labelTitle.Text = "إجمالي الفاتورة";
		this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.combAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combAccount.Location = new System.Drawing.Point(90, 22);
		this.combAccount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.combAccount.Name = "combAccount";
		this.combAccount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.combAccount.Size = new System.Drawing.Size(180, 22);
		this.combAccount.TabIndex = 32;
		this.BtnOpenAccountsSearch.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.BtnOpenAccountsSearch.Location = new System.Drawing.Point(275, 21);
		this.BtnOpenAccountsSearch.Margin = new System.Windows.Forms.Padding(2);
		this.BtnOpenAccountsSearch.Name = "BtnOpenAccountsSearch";
		this.BtnOpenAccountsSearch.Size = new System.Drawing.Size(22, 22);
		this.BtnOpenAccountsSearch.TabIndex = 50;
		this.BtnOpenAccountsSearch.UseVisualStyleBackColor = true;
		this.BtnOpenAccountsSearch.Click += new System.EventHandler(this.BtnOpenAccountsSearch_Click);
		this.combItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.combItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.combItems.DropDownHeight = 150;
		this.combItems.IntegralHeight = false;
		this.combItems.Location = new System.Drawing.Point(435, 142);
		this.combItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.combItems.Name = "combItems";
		this.combItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.combItems.Size = new System.Drawing.Size(206, 22);
		this.combItems.TabIndex = 32;
		this.combItems.Visible = false;
		this.combItems.SelectedIndexChanged += new System.EventHandler(combItems_SelectedIndexChanged_1);
		this.groupBox2.Controls.Add(this.DGVItems);
		this.groupBox2.Location = new System.Drawing.Point(10, 241);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Size = new System.Drawing.Size(1044, 268);
		this.groupBox2.TabIndex = 35;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "أصناف الفاتورة";
		this.groupBox3.Controls.Add(this.txtProductDesc);
		this.groupBox3.Controls.Add(this.BtnOpenProductsSearch);
		this.groupBox3.Controls.Add(this.txtItemTotal);
		this.groupBox3.Controls.Add(this.txtUnitValue);
		this.groupBox3.Controls.Add(this.txtQty);
		this.groupBox3.Controls.Add(this.btnEx1);
		this.groupBox3.Controls.Add(this.labelEx10);
		this.groupBox3.Controls.Add(this.labelEx8);
		this.groupBox3.Controls.Add(this.labelEx7);
		this.groupBox3.Controls.Add(this.txtBarcode);
		this.groupBox3.Controls.Add(this.labelEx2);
		this.groupBox3.Controls.Add(this.labelEx1);
		this.groupBox3.Location = new System.Drawing.Point(10, 198);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.Size = new System.Drawing.Size(1044, 42);
		this.groupBox3.TabIndex = 36;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "أختيار الاصناف";
		this.txtProductDesc.IsNumber = false;
		this.txtProductDesc.Location = new System.Drawing.Point(550, 14);
		this.txtProductDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtProductDesc.Name = "txtProductDesc";
		this.txtProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtProductDesc.Size = new System.Drawing.Size(206, 22);
		this.txtProductDesc.TabIndex = 43;
		this.BtnOpenProductsSearch.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.BtnOpenProductsSearch.Location = new System.Drawing.Point(523, 14);
		this.BtnOpenProductsSearch.Margin = new System.Windows.Forms.Padding(2);
		this.BtnOpenProductsSearch.Name = "BtnOpenProductsSearch";
		this.BtnOpenProductsSearch.Size = new System.Drawing.Size(21, 21);
		this.BtnOpenProductsSearch.TabIndex = 49;
		this.BtnOpenProductsSearch.UseVisualStyleBackColor = true;
		this.BtnOpenProductsSearch.Click += new System.EventHandler(BtnOpenProductsSearch_Click);
		this.txtItemTotal.DecimalPlaces = 3;
		this.txtItemTotal.Enabled = false;
		this.txtItemTotal.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtItemTotal.Location = new System.Drawing.Point(96, 13);
		this.txtItemTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtItemTotal.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtItemTotal.Name = "txtItemTotal";
		this.txtItemTotal.Size = new System.Drawing.Size(72, 26);
		this.txtItemTotal.TabIndex = 48;
		this.txtItemTotal.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtUnitValue.DecimalPlaces = 3;
		this.txtUnitValue.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUnitValue.Location = new System.Drawing.Point(239, 13);
		this.txtUnitValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtUnitValue.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtUnitValue.Name = "txtUnitValue";
		this.txtUnitValue.Size = new System.Drawing.Size(72, 26);
		this.txtUnitValue.TabIndex = 39;
		this.txtUnitValue.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtUnitValue.ValueChanged += new System.EventHandler(txtUnitValue_ValueChanged);
		this.txtQty.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtQty.Location = new System.Drawing.Point(396, 13);
		this.txtQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtQty.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(72, 26);
		this.txtQty.TabIndex = 39;
		this.txtQty.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtQty.ValueChanged += new System.EventHandler(txtQty_ValueChanged);
		this.btnEx1.Location = new System.Drawing.Point(17, 14);
		this.btnEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.btnEx1.Name = "btnEx1";
		this.btnEx1.Size = new System.Drawing.Size(66, 23);
		this.btnEx1.TabIndex = 39;
		this.btnEx1.Text = "إضافة";
		this.btnEx1.UseVisualStyleBackColor = true;
		this.btnEx1.Click += new System.EventHandler(btnEx1_Click);
		this.labelEx10.Location = new System.Drawing.Point(171, 16);
		this.labelEx10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx10.Name = "labelEx10";
		this.labelEx10.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx10.Size = new System.Drawing.Size(56, 18);
		this.labelEx10.TabIndex = 47;
		this.labelEx10.Text = "الاجمالي";
		this.labelEx10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx8.Location = new System.Drawing.Point(314, 16);
		this.labelEx8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx8.Name = "labelEx8";
		this.labelEx8.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx8.Size = new System.Drawing.Size(71, 18);
		this.labelEx8.TabIndex = 43;
		this.labelEx8.Text = "سعر الوحدة";
		this.labelEx8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx7.Location = new System.Drawing.Point(471, 17);
		this.labelEx7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx7.Name = "labelEx7";
		this.labelEx7.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx7.Size = new System.Drawing.Size(47, 16);
		this.labelEx7.TabIndex = 41;
		this.labelEx7.Text = "الكمية";
		this.labelEx7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtBarcode.IsNumber = false;
		this.txtBarcode.Location = new System.Drawing.Point(832, 15);
		this.txtBarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtBarcode.Name = "txtBarcode";
		this.txtBarcode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtBarcode.Size = new System.Drawing.Size(63, 22);
		this.txtBarcode.TabIndex = 40;
		this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBarcode_KeyDown);
		this.labelEx2.Location = new System.Drawing.Point(900, 16);
		this.labelEx2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx2.Name = "labelEx2";
		this.labelEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx2.Size = new System.Drawing.Size(48, 16);
		this.labelEx2.TabIndex = 39;
		this.labelEx2.Text = "الباركود";
		this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx1.Location = new System.Drawing.Point(758, 16);
		this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(66, 18);
		this.labelEx1.TabIndex = 35;
		this.labelEx1.Text = "اسم الصنف";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx11.Location = new System.Drawing.Point(179, 520);
		this.labelEx11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx11.Name = "labelEx11";
		this.labelEx11.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx11.Size = new System.Drawing.Size(55, 20);
		this.labelEx11.TabIndex = 39;
		this.labelEx11.Text = "الخصم";
		this.labelEx11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtInvTotal.DecimalPlaces = 3;
		this.txtInvTotal.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtInvTotal.ForeColor = System.Drawing.Color.Black;
		this.txtInvTotal.Location = new System.Drawing.Point(66, 514);
		this.txtInvTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtInvTotal.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtInvTotal.Name = "txtInvTotal";
		this.txtInvTotal.Size = new System.Drawing.Size(105, 33);
		this.txtInvTotal.TabIndex = 40;
		this.txtInvTotal.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtInvDesc.DecimalPlaces = 3;
		this.txtInvDesc.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtInvDesc.ForeColor = System.Drawing.Color.Red;
		this.txtInvDesc.Location = new System.Drawing.Point(229, 514);
		this.txtInvDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtInvDesc.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtInvDesc.Name = "txtInvDesc";
		this.txtInvDesc.Size = new System.Drawing.Size(102, 33);
		this.txtInvDesc.TabIndex = 41;
		this.txtInvDesc.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtInvDesc.ValueChanged += new System.EventHandler(txtInvDesc_ValueChanged);
		this.txtInvTax.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtInvTax.ForeColor = System.Drawing.Color.Green;
		this.txtInvTax.Location = new System.Drawing.Point(410, 514);
		this.txtInvTax.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtInvTax.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtInvTax.Name = "txtInvTax";
		this.txtInvTax.Size = new System.Drawing.Size(61, 33);
		this.txtInvTax.TabIndex = 42;
		this.txtInvTax.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.txtInvTax.Value = new decimal(new int[4] { 14, 0, 0, 0 });
		this.txtInvTax.ValueChanged += new System.EventHandler(txtInvTax_ValueChanged);
		this.txtInvNet.DecimalPlaces = 3;
		this.txtInvNet.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtInvNet.ForeColor = System.Drawing.Color.Blue;
		this.txtInvNet.Location = new System.Drawing.Point(725, 514);
		this.txtInvNet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtInvNet.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.txtInvNet.Name = "txtInvNet";
		this.txtInvNet.Size = new System.Drawing.Size(103, 33);
		this.txtInvNet.TabIndex = 40;
		this.txtInvNet.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.labelEx12.Location = new System.Drawing.Point(344, 521);
		this.labelEx12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx12.Name = "labelEx12";
		this.labelEx12.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx12.Size = new System.Drawing.Size(64, 19);
		this.labelEx12.TabIndex = 39;
		this.labelEx12.Text = "ق.مضافة";
		this.labelEx12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx13.Location = new System.Drawing.Point(670, 521);
		this.labelEx13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx13.Name = "labelEx13";
		this.labelEx13.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx13.Size = new System.Drawing.Size(52, 21);
		this.labelEx13.TabIndex = 39;
		this.labelEx13.Text = "الصافي";
		this.labelEx13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx14.Location = new System.Drawing.Point(9, 521);
		this.labelEx14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx14.Name = "labelEx14";
		this.labelEx14.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx14.Size = new System.Drawing.Size(52, 18);
		this.labelEx14.TabIndex = 43;
		this.labelEx14.Text = "الاجمالي";
		this.labelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Tax4.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.Tax4.ForeColor = System.Drawing.Color.Green;
		this.Tax4.Location = new System.Drawing.Point(583, 514);
		this.Tax4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.Tax4.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
		this.Tax4.Name = "Tax4";
		this.Tax4.Size = new System.Drawing.Size(72, 33);
		this.Tax4.TabIndex = 45;
		this.Tax4.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
		this.Tax4.ValueChanged += new System.EventHandler(Tax4_ValueChanged);
		this.labelEx16.Location = new System.Drawing.Point(487, 521);
		this.labelEx16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx16.Name = "labelEx16";
		this.labelEx16.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx16.Size = new System.Drawing.Size(61, 21);
		this.labelEx16.TabIndex = 44;
		this.labelEx16.Text = "ض .خصم";
		this.labelEx16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(1064, 596);
		base.Controls.Add(this.Tax4);
		base.Controls.Add(this.labelEx16);
		base.Controls.Add(this.labelEx14);
		base.Controls.Add(this.txtInvNet);
		base.Controls.Add(this.txtInvTax);
		base.Controls.Add(this.txtInvDesc);
		base.Controls.Add(this.txtInvTotal);
		base.Controls.Add(this.labelEx13);
		base.Controls.Add(this.labelEx12);
		base.Controls.Add(this.labelEx11);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		base.Load += new System.EventHandler(frmInvoice_Load);
		base.Controls.SetChildIndex(this.groupBox1, 0);
		base.Controls.SetChildIndex(this.groupBox2, 0);
		base.Controls.SetChildIndex(this.groupBox3, 0);
		base.Controls.SetChildIndex(this.labelEx11, 0);
		base.Controls.SetChildIndex(this.labelEx12, 0);
		base.Controls.SetChildIndex(this.labelEx13, 0);
		base.Controls.SetChildIndex(this.txtInvTotal, 0);
		base.Controls.SetChildIndex(this.txtInvDesc, 0);
		base.Controls.SetChildIndex(this.txtInvTax, 0);
		base.Controls.SetChildIndex(this.txtInvNet, 0);
		base.Controls.SetChildIndex(this.labelEx14, 0);
		base.Controls.SetChildIndex(this.labelEx16, 0);
		base.Controls.SetChildIndex(this.Tax4, 0);
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvDesc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tax4).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
