using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using E_Invoice.DAL.Data;
using E_Invoice.Desktop.Helpers;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmInvoiceNew : Master
{
	public OrderType _orderType;

	public Order order;

	public OrderDetail OrderDetails;

	public Product item;

	private string accountType = "Customer";

	private int AccountTypeInt = 0;

	private BindingList<ProductInvoiceVM> list = new BindingList<ProductInvoiceVM>();

	public ProductInvoiceVM SelectedProductNM;

	private readonly ApplicationDBContext _context;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Label label1;

	private TextBox txtInvCode;

	private Label label4;

	private Label label3;

	private Label label2;

	private Button btnAddReference;

	private DateTimePicker DTInvDate;

	private ComboBox combStore;

	private TextBox txtReference;

	private GroupBox groupBox3;

	private Label label5;

	private TextBox txtCustomerName;

	private Label label6;

	private TextBox txtCustomerId;

	private Label label8;

	private Label label7;

	private TextBox txtCustomerStreet;

	private TextBox txtCustomerGov;

	private Button btnAddCustomer;

	private Label label10;

	private ComboBox cmbCustomerType;

	private TextBox txtCustomerBuildingNo;

	private TextBox txtCustomerCity;

	private Button btnGenerateCode;

	private Label label9;

	private GroupBox groupBox4;

	private GroupBox groupBox6;

	private DataGridView DGVItems;

	private Label label11;

	private TextBox txtProductDesc;

	private TextBox txtBarcode;

	private Label label12;

	private NumericUpDown nProductDiscountRate;

	private Label label16;

	private NumericUpDown NProductUnitPrice;

	private NumericUpDown nProductQuantity;

	private Label label15;

	private Label label14;

	private ComboBox cmbProductUnites;

	private Label label13;

	private DevComponents.AdvTree.ColumnHeader columnHeader1;

	private DevComponents.AdvTree.ColumnHeader columnHeader2;

	private Label label17;

	private TextBox txtProductTotal;

	private Button button2;

	private TextBox txtProductNet;

	private Label label19;

	private Label label18;

	private Button button1;

	private TextBox txtProductDiscountValue;

	private BindingSource productInvoiceVMBindingSource;

	private DataGridViewTextBoxColumn itemTotalDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn unitIdDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn unitValueDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn ItemTotal;

	private DataGridViewTextBoxColumn discountRateDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn taxesDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn netTotalDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn unitQtyDataGridViewTextBoxColumn;

	private Label label20;

	private Label label21;

	private Label label25;

	private Label label24;

	private Label label23;

	private Label label22;

	public frmInvoiceNew(OrderType invType)
	{
		_context = new ApplicationDBContext();
		item = new Product();
		_orderType = invType;
		InitializeComponent();
		New();
	}

	public frmInvoiceNew(int id)
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
		list.Clear();
	}

	public override void GetData()
	{
		base.GetData();
	}

	private void frmInvoiceNew_Load(object sender, EventArgs e)
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
	}

	public override void GetNewCode()
	{
		string number = (from x in _context.orders
			where (int)x.OrderType == (int)_orderType && x.BranchId == 1
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

	private void textBox1_DoubleClick(object sender, EventArgs e)
	{
		if (txtBarcode.Text == string.Empty)
		{
			Info._ProductSelected = false;
			Info._SelectedProduct = 0;
			frmProductSearch frmProductSearch2 = new frmProductSearch();
			if (frmProductSearch2.ShowDialog() == DialogResult.OK && Info._SelectedProduct != 0)
			{
				Product tById = _unitOfWork.Products.GetTById(Info._SelectedProduct);
				SelectedProductNM = new ProductInvoiceVM
				{
					Id = tById.Id,
					Name = tById.Name,
					UnitValue = tById.SalePrice,
					Qty = 1m,
					DiscountRate = 0m,
					taxableItems = new List<taxableItems>()
				};
				GetItemData(SelectedProductNM);
				button2.Enabled = true;
			}
		}
	}

	private void GetItemData(ProductInvoiceVM Product)
	{
		txtProductDesc.Text = Product.Name;
		List<ProductUnites> dataSource = _unitOfWork.ProductUnites.GetAllBy((ProductUnites x) => x.ProductId == Product.Id, new string[1] { "Unit" }).ToList();
		cmbProductUnites.DataSource = dataSource;
		cmbProductUnites.DisplayMember = "Name";
		cmbProductUnites.ValueMember = "UnitId";
		NProductUnitPrice.Value = Product.UnitValue;
		nProductQuantity.Value = Product.Qty;
		nProductDiscountRate.Value = Product.DiscountRate;
		CalcNewRow();
	}

	private void nProductDiscountRate_ValueChanged(object sender, EventArgs e)
	{
		SelectedProductNM.DiscountRate = nProductDiscountRate.Value;
		CalcNewRow();
	}

	private void NProductUnitPrice_ValueChanged(object sender, EventArgs e)
	{
		SelectedProductNM.UnitValue = NProductUnitPrice.Value;
		CalcNewRow();
	}

	private void nProductQuantity_ValueChanged(object sender, EventArgs e)
	{
		SelectedProductNM.Qty = nProductQuantity.Value;
		CalcNewRow();
	}

	private void cmbProductUnites_SelectedIndexChanged(object sender, EventArgs e)
	{
		CalcNewRow();
	}

	private void btnAddCustomer_Click(object sender, EventArgs e)
	{
	}

	private void CalcNewRow()
	{
		decimal value = NProductUnitPrice.Value;
		decimal value2 = nProductQuantity.Value;
		decimal num = SelectedProductNM.taxableItems.Sum((taxableItems x) => x.amount);
		decimal num2 = value * value2;
		decimal value3 = nProductDiscountRate.Value;
		decimal num3 = value3 * num2 / 100m;
		decimal num4 = num2 - num3 + num;
		txtProductTotal.Text = num2.ToString();
		txtProductNet.Text = num4.ToString();
		txtProductDiscountValue.Text = num3.ToString();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		frmAddTaxForItem frmAddTaxForItem2 = new frmAddTaxForItem(this);
		frmAddTaxForItem2.ShowDialog();
		CalcNewRow();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		DGVItems.Columns[9].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		list.Add(SelectedProductNM);
		DGVItems.DataSource = list;
		SelectedProductNM = new ProductInvoiceVM
		{
			taxableItems = new List<taxableItems>()
		};
		E_Invoice.Desktop.Helpers.Utilities.ResetAllControls(groupBox6);
		button2.Enabled = false;
	}

	private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnAddCustomer = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.cmbCustomerType = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtCustomerBuildingNo = new System.Windows.Forms.TextBox();
		this.txtCustomerStreet = new System.Windows.Forms.TextBox();
		this.txtCustomerCity = new System.Windows.Forms.TextBox();
		this.txtCustomerGov = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtCustomerId = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtCustomerName = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnGenerateCode = new System.Windows.Forms.Button();
		this.btnAddReference = new System.Windows.Forms.Button();
		this.DTInvDate = new System.Windows.Forms.DateTimePicker();
		this.combStore = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtReference = new System.Windows.Forms.TextBox();
		this.txtInvCode = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.txtProductNet = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.txtProductDiscountValue = new System.Windows.Forms.TextBox();
		this.nProductDiscountRate = new System.Windows.Forms.NumericUpDown();
		this.label16 = new System.Windows.Forms.Label();
		this.NProductUnitPrice = new System.Windows.Forms.NumericUpDown();
		this.nProductQuantity = new System.Windows.Forms.NumericUpDown();
		this.label17 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.cmbProductUnites = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtProductTotal = new System.Windows.Forms.TextBox();
		this.txtProductDesc = new System.Windows.Forms.TextBox();
		this.txtBarcode = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.DGVItems = new System.Windows.Forms.DataGridView();
		this.label11 = new System.Windows.Forms.Label();
		this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
		this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
		this.ItemTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unitIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unitValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.discountRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.taxesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.netTotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unitQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.productInvoiceVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nProductDiscountRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NProductUnitPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nProductQuantity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.productInvoiceVMBindingSource).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(7, 37);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1049, 190);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "البيانات";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label9.BackColor = System.Drawing.Color.Moccasin;
		this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label9.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(456, 40);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(150, 45);
		this.label9.TabIndex = 2;
		this.label9.Text = "فاتورة بيع";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox3.Controls.Add(this.btnAddCustomer);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.cmbCustomerType);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.txtCustomerBuildingNo);
		this.groupBox3.Controls.Add(this.txtCustomerStreet);
		this.groupBox3.Controls.Add(this.txtCustomerCity);
		this.groupBox3.Controls.Add(this.txtCustomerGov);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.txtCustomerId);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.txtCustomerName);
		this.groupBox3.Location = new System.Drawing.Point(6, 13);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(362, 161);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "بيانات العميل";
		this.btnAddCustomer.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.btnAddCustomer.Location = new System.Drawing.Point(4, 19);
		this.btnAddCustomer.Name = "btnAddCustomer";
		this.btnAddCustomer.Size = new System.Drawing.Size(24, 22);
		this.btnAddCustomer.TabIndex = 6;
		this.btnAddCustomer.UseVisualStyleBackColor = true;
		this.btnAddCustomer.Click += new System.EventHandler(btnAddCustomer_Click);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(97, 56);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(34, 16);
		this.label10.TabIndex = 5;
		this.label10.Text = "النوع";
		this.cmbCustomerType.FormattingEnabled = true;
		this.cmbCustomerType.Location = new System.Drawing.Point(9, 53);
		this.cmbCustomerType.Name = "cmbCustomerType";
		this.cmbCustomerType.Size = new System.Drawing.Size(83, 24);
		this.cmbCustomerType.TabIndex = 4;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(281, 122);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(73, 16);
		this.label8.TabIndex = 3;
		this.label8.Text = "شارع/مبني";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(274, 87);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(84, 16);
		this.label7.TabIndex = 3;
		this.label7.Text = "محافظة/مدينة";
		this.txtCustomerBuildingNo.Location = new System.Drawing.Point(10, 116);
		this.txtCustomerBuildingNo.Name = "txtCustomerBuildingNo";
		this.txtCustomerBuildingNo.Size = new System.Drawing.Size(127, 23);
		this.txtCustomerBuildingNo.TabIndex = 2;
		this.txtCustomerStreet.Location = new System.Drawing.Point(142, 117);
		this.txtCustomerStreet.Name = "txtCustomerStreet";
		this.txtCustomerStreet.Size = new System.Drawing.Size(127, 23);
		this.txtCustomerStreet.TabIndex = 2;
		this.txtCustomerCity.Location = new System.Drawing.Point(10, 85);
		this.txtCustomerCity.Name = "txtCustomerCity";
		this.txtCustomerCity.Size = new System.Drawing.Size(126, 23);
		this.txtCustomerCity.TabIndex = 2;
		this.txtCustomerGov.Location = new System.Drawing.Point(142, 84);
		this.txtCustomerGov.Name = "txtCustomerGov";
		this.txtCustomerGov.Size = new System.Drawing.Size(127, 23);
		this.txtCustomerGov.TabIndex = 2;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(277, 56);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(78, 16);
		this.label6.TabIndex = 3;
		this.label6.Text = "الرقم القومي";
		this.txtCustomerId.Location = new System.Drawing.Point(135, 53);
		this.txtCustomerId.Name = "txtCustomerId";
		this.txtCustomerId.Size = new System.Drawing.Size(134, 23);
		this.txtCustomerId.TabIndex = 2;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(281, 23);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(72, 16);
		this.label5.TabIndex = 3;
		this.label5.Text = "أسم العميل";
		this.txtCustomerName.Location = new System.Drawing.Point(32, 20);
		this.txtCustomerName.Name = "txtCustomerName";
		this.txtCustomerName.Size = new System.Drawing.Size(237, 23);
		this.txtCustomerName.TabIndex = 2;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.btnGenerateCode);
		this.groupBox2.Controls.Add(this.btnAddReference);
		this.groupBox2.Controls.Add(this.DTInvDate);
		this.groupBox2.Controls.Add(this.combStore);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.txtReference);
		this.groupBox2.Controls.Add(this.txtInvCode);
		this.groupBox2.Location = new System.Drawing.Point(739, 23);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(300, 151);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "بيانات الفاتورة";
		this.btnGenerateCode.Image = E_Invoice.Desktop.Properties.Resources.Refresh_load;
		this.btnGenerateCode.Location = new System.Drawing.Point(7, 22);
		this.btnGenerateCode.Name = "btnGenerateCode";
		this.btnGenerateCode.Size = new System.Drawing.Size(24, 22);
		this.btnGenerateCode.TabIndex = 1;
		this.btnGenerateCode.UseVisualStyleBackColor = true;
		this.btnAddReference.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.btnAddReference.Location = new System.Drawing.Point(5, 113);
		this.btnAddReference.Name = "btnAddReference";
		this.btnAddReference.Size = new System.Drawing.Size(24, 22);
		this.btnAddReference.TabIndex = 1;
		this.btnAddReference.UseVisualStyleBackColor = true;
		this.DTInvDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.DTInvDate.Location = new System.Drawing.Point(7, 52);
		this.DTInvDate.Name = "DTInvDate";
		this.DTInvDate.Size = new System.Drawing.Size(212, 23);
		this.DTInvDate.TabIndex = 3;
		this.combStore.FormattingEnabled = true;
		this.combStore.Location = new System.Drawing.Point(7, 81);
		this.combStore.Name = "combStore";
		this.combStore.Size = new System.Drawing.Size(212, 24);
		this.combStore.TabIndex = 2;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(225, 112);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 16);
		this.label4.TabIndex = 1;
		this.label4.Text = "رقم المرجع";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(248, 84);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(46, 16);
		this.label3.TabIndex = 1;
		this.label3.Text = "المخزن";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(225, 54);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "تاريخ الاصدار";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(225, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(69, 16);
		this.label1.TabIndex = 1;
		this.label1.Text = "كود الفاتورة";
		this.txtReference.Location = new System.Drawing.Point(35, 112);
		this.txtReference.Name = "txtReference";
		this.txtReference.Size = new System.Drawing.Size(184, 23);
		this.txtReference.TabIndex = 0;
		this.txtInvCode.Location = new System.Drawing.Point(35, 21);
		this.txtInvCode.Name = "txtInvCode";
		this.txtInvCode.Size = new System.Drawing.Size(184, 23);
		this.txtInvCode.TabIndex = 0;
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.label25);
		this.groupBox4.Controls.Add(this.label24);
		this.groupBox4.Controls.Add(this.label23);
		this.groupBox4.Controls.Add(this.label22);
		this.groupBox4.Controls.Add(this.label21);
		this.groupBox4.Controls.Add(this.label20);
		this.groupBox4.Location = new System.Drawing.Point(7, 565);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(1049, 109);
		this.groupBox4.TabIndex = 2;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "groupBox4";
		this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox6.Controls.Add(this.button2);
		this.groupBox6.Controls.Add(this.txtProductNet);
		this.groupBox6.Controls.Add(this.label19);
		this.groupBox6.Controls.Add(this.label18);
		this.groupBox6.Controls.Add(this.button1);
		this.groupBox6.Controls.Add(this.txtProductDiscountValue);
		this.groupBox6.Controls.Add(this.nProductDiscountRate);
		this.groupBox6.Controls.Add(this.label16);
		this.groupBox6.Controls.Add(this.NProductUnitPrice);
		this.groupBox6.Controls.Add(this.nProductQuantity);
		this.groupBox6.Controls.Add(this.label17);
		this.groupBox6.Controls.Add(this.label15);
		this.groupBox6.Controls.Add(this.label14);
		this.groupBox6.Controls.Add(this.cmbProductUnites);
		this.groupBox6.Controls.Add(this.label13);
		this.groupBox6.Controls.Add(this.txtProductTotal);
		this.groupBox6.Controls.Add(this.txtProductDesc);
		this.groupBox6.Controls.Add(this.txtBarcode);
		this.groupBox6.Controls.Add(this.label12);
		this.groupBox6.Controls.Add(this.DGVItems);
		this.groupBox6.Controls.Add(this.label11);
		this.groupBox6.Location = new System.Drawing.Point(7, 233);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(1049, 326);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "groupBox6";
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button2.FlatAppearance.BorderSize = 0;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Image = E_Invoice.Desktop.Properties.Resources.AddiconSmall;
		this.button2.Location = new System.Drawing.Point(8, 12);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(26, 51);
		this.button2.TabIndex = 14;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.txtProductNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtProductNet.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtProductNet.Location = new System.Drawing.Point(38, 40);
		this.txtProductNet.Name = "txtProductNet";
		this.txtProductNet.Size = new System.Drawing.Size(99, 22);
		this.txtProductNet.TabIndex = 13;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label19.ForeColor = System.Drawing.Color.White;
		this.label19.Location = new System.Drawing.Point(38, 15);
		this.label19.Name = "label19";
		this.label19.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
		this.label19.Size = new System.Drawing.Size(96, 18);
		this.label19.TabIndex = 12;
		this.label19.Text = "الصافي";
		this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label18.ForeColor = System.Drawing.Color.White;
		this.label18.Location = new System.Drawing.Point(141, 16);
		this.label18.Name = "label18";
		this.label18.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
		this.label18.Size = new System.Drawing.Size(65, 18);
		this.label18.TabIndex = 11;
		this.label18.Text = "الضريبة";
		this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.button1.Location = new System.Drawing.Point(141, 42);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(65, 22);
		this.button1.TabIndex = 10;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.txtProductDiscountValue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtProductDiscountValue.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtProductDiscountValue.Location = new System.Drawing.Point(208, 42);
		this.txtProductDiscountValue.Name = "txtProductDiscountValue";
		this.txtProductDiscountValue.Size = new System.Drawing.Size(127, 22);
		this.txtProductDiscountValue.TabIndex = 9;
		this.nProductDiscountRate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.nProductDiscountRate.DecimalPlaces = 2;
		this.nProductDiscountRate.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.nProductDiscountRate.Location = new System.Drawing.Point(208, 15);
		this.nProductDiscountRate.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
		this.nProductDiscountRate.Name = "nProductDiscountRate";
		this.nProductDiscountRate.Size = new System.Drawing.Size(69, 23);
		this.nProductDiscountRate.TabIndex = 8;
		this.nProductDiscountRate.ValueChanged += new System.EventHandler(nProductDiscountRate_ValueChanged);
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label16.ForeColor = System.Drawing.Color.White;
		this.label16.Location = new System.Drawing.Point(280, 16);
		this.label16.Name = "label16";
		this.label16.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
		this.label16.Size = new System.Drawing.Size(55, 18);
		this.label16.TabIndex = 7;
		this.label16.Text = "الخصم";
		this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.NProductUnitPrice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.NProductUnitPrice.DecimalPlaces = 2;
		this.NProductUnitPrice.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.NProductUnitPrice.Location = new System.Drawing.Point(467, 16);
		this.NProductUnitPrice.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
		this.NProductUnitPrice.Name = "NProductUnitPrice";
		this.NProductUnitPrice.Size = new System.Drawing.Size(112, 23);
		this.NProductUnitPrice.TabIndex = 6;
		this.NProductUnitPrice.ValueChanged += new System.EventHandler(NProductUnitPrice_ValueChanged);
		this.nProductQuantity.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.nProductQuantity.DecimalPlaces = 2;
		this.nProductQuantity.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.nProductQuantity.Location = new System.Drawing.Point(338, 16);
		this.nProductQuantity.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
		this.nProductQuantity.Name = "nProductQuantity";
		this.nProductQuantity.Size = new System.Drawing.Size(69, 23);
		this.nProductQuantity.TabIndex = 6;
		this.nProductQuantity.ValueChanged += new System.EventHandler(nProductQuantity_ValueChanged);
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label17.ForeColor = System.Drawing.Color.White;
		this.label17.Location = new System.Drawing.Point(574, 44);
		this.label17.Name = "label17";
		this.label17.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label17.Size = new System.Drawing.Size(64, 18);
		this.label17.TabIndex = 5;
		this.label17.Text = "الإجمالي";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label15.ForeColor = System.Drawing.Color.White;
		this.label15.Location = new System.Drawing.Point(582, 19);
		this.label15.Name = "label15";
		this.label15.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
		this.label15.Size = new System.Drawing.Size(53, 18);
		this.label15.TabIndex = 5;
		this.label15.Text = "السعر";
		this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label14.ForeColor = System.Drawing.Color.White;
		this.label14.Location = new System.Drawing.Point(410, 18);
		this.label14.Name = "label14";
		this.label14.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
		this.label14.Size = new System.Drawing.Size(55, 18);
		this.label14.TabIndex = 5;
		this.label14.Text = "الكمية";
		this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.cmbProductUnites.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbProductUnites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbProductUnites.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmbProductUnites.FormattingEnabled = true;
		this.cmbProductUnites.Location = new System.Drawing.Point(639, 42);
		this.cmbProductUnites.Name = "cmbProductUnites";
		this.cmbProductUnites.Size = new System.Drawing.Size(113, 24);
		this.cmbProductUnites.TabIndex = 3;
		this.cmbProductUnites.SelectedIndexChanged += new System.EventHandler(cmbProductUnites_SelectedIndexChanged);
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label13.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label13.ForeColor = System.Drawing.Color.White;
		this.label13.Location = new System.Drawing.Point(639, 19);
		this.label13.Name = "label13";
		this.label13.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
		this.label13.Size = new System.Drawing.Size(113, 18);
		this.label13.TabIndex = 4;
		this.label13.Text = "الوحدة";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtProductTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtProductTotal.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtProductTotal.Location = new System.Drawing.Point(338, 43);
		this.txtProductTotal.Name = "txtProductTotal";
		this.txtProductTotal.Size = new System.Drawing.Size(233, 22);
		this.txtProductTotal.TabIndex = 3;
		this.txtProductDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtProductDesc.Location = new System.Drawing.Point(755, 42);
		this.txtProductDesc.Multiline = true;
		this.txtProductDesc.Name = "txtProductDesc";
		this.txtProductDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.txtProductDesc.Size = new System.Drawing.Size(219, 24);
		this.txtProductDesc.TabIndex = 3;
		this.txtBarcode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtBarcode.Location = new System.Drawing.Point(977, 43);
		this.txtBarcode.Name = "txtBarcode";
		this.txtBarcode.Size = new System.Drawing.Size(65, 23);
		this.txtBarcode.TabIndex = 3;
		this.txtBarcode.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.label12.ForeColor = System.Drawing.Color.White;
		this.label12.Location = new System.Drawing.Point(755, 19);
		this.label12.Name = "label12";
		this.label12.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
		this.label12.Size = new System.Drawing.Size(219, 18);
		this.label12.TabIndex = 1;
		this.label12.Text = "أسم الصنف";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVItems.AutoGenerateColumns = false;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(24, 45, 84);
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.barcodeDataGridViewTextBoxColumn, this.nameDataGridViewTextBoxColumn, this.unitIdDataGridViewTextBoxColumn, this.unitDataGridViewTextBoxColumn, this.unitValueDataGridViewTextBoxColumn, this.qtyDataGridViewTextBoxColumn, this.ItemTotal, this.discountRateDataGridViewTextBoxColumn, this.taxesDataGridViewTextBoxColumn, this.netTotalDataGridViewTextBoxColumn, this.unitQtyDataGridViewTextBoxColumn);
		this.DGVItems.DataSource = this.productInvoiceVMBindingSource;
		this.DGVItems.EnableHeadersVisualStyles = false;
		this.DGVItems.Location = new System.Drawing.Point(4, 71);
		this.DGVItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersVisible = false;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.RowTemplate.Height = 30;
		this.DGVItems.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(1038, 250);
		this.DGVItems.TabIndex = 2;
		this.DGVItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVItems_CellDoubleClick);
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.DarkOliveGreen;
		this.label11.ForeColor = System.Drawing.Color.White;
		this.label11.Location = new System.Drawing.Point(977, 20);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 16);
		this.label11.TabIndex = 1;
		this.label11.Text = "الــبــاركــود";
		this.columnHeader1.Name = "columnHeader1";
		this.columnHeader1.Text = "Column";
		this.columnHeader1.Width.Absolute = 150;
		this.columnHeader2.Name = "columnHeader2";
		this.columnHeader2.Text = "Column";
		this.columnHeader2.Width.Absolute = 150;
		this.ItemTotal.DataPropertyName = "ItemTotal";
		this.ItemTotal.HeaderText = "الاجمالي";
		this.ItemTotal.Name = "ItemTotal";
		this.ItemTotal.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
		this.idDataGridViewTextBoxColumn.HeaderText = "كود الصنف";
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
		this.barcodeDataGridViewTextBoxColumn.HeaderText = "الباركود";
		this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
		this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
		this.barcodeDataGridViewTextBoxColumn.Visible = false;
		this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
		this.nameDataGridViewTextBoxColumn.HeaderText = "اسم الصنف";
		this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
		this.nameDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitIdDataGridViewTextBoxColumn.DataPropertyName = "UnitId";
		this.unitIdDataGridViewTextBoxColumn.HeaderText = "UnitId";
		this.unitIdDataGridViewTextBoxColumn.Name = "unitIdDataGridViewTextBoxColumn";
		this.unitIdDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitIdDataGridViewTextBoxColumn.Visible = false;
		this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
		this.unitDataGridViewTextBoxColumn.HeaderText = "الوحدة";
		this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
		this.unitDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitValueDataGridViewTextBoxColumn.DataPropertyName = "UnitValue";
		this.unitValueDataGridViewTextBoxColumn.HeaderText = "سعر الوحدة";
		this.unitValueDataGridViewTextBoxColumn.Name = "unitValueDataGridViewTextBoxColumn";
		this.unitValueDataGridViewTextBoxColumn.ReadOnly = true;
		this.qtyDataGridViewTextBoxColumn.DataPropertyName = "Qty";
		this.qtyDataGridViewTextBoxColumn.HeaderText = "الكمية";
		this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
		this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
		this.discountRateDataGridViewTextBoxColumn.DataPropertyName = "DiscountRate";
		this.discountRateDataGridViewTextBoxColumn.HeaderText = "الخصم";
		this.discountRateDataGridViewTextBoxColumn.Name = "discountRateDataGridViewTextBoxColumn";
		this.discountRateDataGridViewTextBoxColumn.ReadOnly = true;
		this.taxesDataGridViewTextBoxColumn.DataPropertyName = "Taxes";
		this.taxesDataGridViewTextBoxColumn.HeaderText = "الضرائب";
		this.taxesDataGridViewTextBoxColumn.Name = "taxesDataGridViewTextBoxColumn";
		this.taxesDataGridViewTextBoxColumn.ReadOnly = true;
		this.netTotalDataGridViewTextBoxColumn.DataPropertyName = "NetTotal";
		this.netTotalDataGridViewTextBoxColumn.HeaderText = "الصافي";
		this.netTotalDataGridViewTextBoxColumn.Name = "netTotalDataGridViewTextBoxColumn";
		this.netTotalDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitQtyDataGridViewTextBoxColumn.DataPropertyName = "UnitQty";
		this.unitQtyDataGridViewTextBoxColumn.HeaderText = "معامل التحويل";
		this.unitQtyDataGridViewTextBoxColumn.Name = "unitQtyDataGridViewTextBoxColumn";
		this.unitQtyDataGridViewTextBoxColumn.ReadOnly = true;
		this.unitQtyDataGridViewTextBoxColumn.Visible = false;
		this.productInvoiceVMBindingSource.DataSource = typeof(E_Invoice.Domain.Models.ProductInvoiceVM);
		this.label20.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(887, 32);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(128, 53);
		this.label20.TabIndex = 0;
		this.label20.Text = "إجمالي الفاتورة";
		this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label21.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(753, 32);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(128, 53);
		this.label21.TabIndex = 0;
		this.label21.Text = "51025.00";
		this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label22.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(616, 32);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(128, 53);
		this.label22.TabIndex = 0;
		this.label22.Text = "إجمالي الفاتورة";
		this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label23.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(482, 32);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(128, 53);
		this.label23.TabIndex = 0;
		this.label23.Text = "51025.00";
		this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label24.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(345, 32);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(128, 53);
		this.label24.TabIndex = 0;
		this.label24.Text = "إجمالي الفاتورة";
		this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label25.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.Location = new System.Drawing.Point(211, 32);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(128, 53);
		this.label25.TabIndex = 0;
		this.label25.Text = "51025.00";
		this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(1064, 681);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "frmInvoiceNew";
		base.Load += new System.EventHandler(frmInvoiceNew_Load);
		base.Controls.SetChildIndex(this.groupBox1, 0);
		base.Controls.SetChildIndex(this.groupBox4, 0);
		base.Controls.SetChildIndex(this.groupBox6, 0);
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nProductDiscountRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NProductUnitPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nProductQuantity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.productInvoiceVMBindingSource).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
