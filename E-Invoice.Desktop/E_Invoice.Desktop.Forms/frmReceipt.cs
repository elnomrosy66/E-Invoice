using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class KeyValueItem
{
	public string Text { get; set; }
	public string Value { get; set; }
}

public class frmReceipt : Form
{
	private IContainer components = null;
	private UnitOfWork _unitOfWork;
	public Order currentReceipt;
	private List<OrderDetail> receiptItems = new List<OrderDetail>();

	// Controls
	private GroupBox grpPosInfo;
	private ComboBoxEx combPosDevice;
	private LabelEx lbPosDevice;
	private TextBoxEx txtReceiptNumber;
	private LabelEx lbReceiptNumber;
	private DateTimePickerEx dtReceiptDate;
	private LabelEx lbReceiptDate;

	private GroupBox grpBuyer;
	private ComboBoxEx combBuyerType;
	private LabelEx lbBuyerType;
	private TextBoxEx txtBuyerId;
	private LabelEx lbBuyerId;
	private TextBoxEx txtBuyerName;
	private LabelEx lbBuyerName;
	private TextBoxEx txtBuyerMobile;
	private LabelEx lbBuyerMobile;

	private GroupBox grpItems;
	private TextBoxEx txtBarcode;
	private LabelEx lbBarcode;
	private Button btnSearchProduct;
	private TextBoxEx txtQty;
	private LabelEx lbQty;
	private Button btnAddItem;
	private DataGridView dgvReceiptItems;

	private GroupBox grpTotals;
	private TextBoxEx txtTotalBeforeTax;
	private LabelEx lbTotalBeforeTax;
	private TextBoxEx txtTotalDiscount;
	private LabelEx lbTotalDiscount;
	private TextBoxEx txtTotalVat;
	private LabelEx lbTotalVat;
	private TextBoxEx txtNetTotal;
	private LabelEx lbNetTotal;
	private ComboBoxEx combPaymentMethod;
	private LabelEx lbPaymentMethod;

	private Button btnNewReceipt;
	private Button btnSaveAndPrint;
	private Button btnClose;

	public frmReceipt()
	{
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		LoadPosDevices();
		LoadBuyerTypes();
		LoadPaymentMethods();
		NewReceipt();
	}

	private void LoadPosDevices()
	{
		var devices = _unitOfWork.PosDevices.GetAll().Where(p => p.IsActive).ToList();
		combPosDevice.DataSource = devices;
		combPosDevice.DisplayMember = "PosName";
		combPosDevice.ValueMember = "Id";
		if (devices.Count > 0)
		{
			combPosDevice.SelectedIndex = 0;
			UpdateNextReceiptNumber();
		}
	}

	private void LoadBuyerTypes()
	{
		combBuyerType.Items.Clear();
		combBuyerType.Items.Add(new KeyValueItem { Text = "مستهلك نهائي فرد (Person)", Value = "P" });
		combBuyerType.Items.Add(new KeyValueItem { Text = "شركة / أعمال (Business)", Value = "B" });
		combBuyerType.Items.Add(new KeyValueItem { Text = "أجنبي (Foreigner)", Value = "F" });
		combBuyerType.DisplayMember = "Text";
		combBuyerType.ValueMember = "Value";
		combBuyerType.SelectedIndex = 0;
	}

	private void LoadPaymentMethods()
	{
		combPaymentMethod.Items.Clear();
		combPaymentMethod.Items.Add(new KeyValueItem { Text = "نقدي (Cash)", Value = "C" });
		combPaymentMethod.Items.Add(new KeyValueItem { Text = "بطاقة ائتمان / فيزا (Card)", Value = "CC" });
		combPaymentMethod.Items.Add(new KeyValueItem { Text = "تحويل بنكي (Bank)", Value = "B" });
		combPaymentMethod.Items.Add(new KeyValueItem { Text = "قسيمة شراء (Voucher)", Value = "V" });
		combPaymentMethod.DisplayMember = "Text";
		combPaymentMethod.ValueMember = "Value";
		combPaymentMethod.SelectedIndex = 0;
	}

	private void UpdateNextReceiptNumber()
	{
		if (combPosDevice.SelectedItem is PosDevice pos)
		{
			string prefix = pos.ReceiptPrefix ?? "";
			txtReceiptNumber.Text = prefix + pos.CurrentSequence.ToString();
		}
	}

	private void combPosDevice_SelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateNextReceiptNumber();
	}

	private void NewReceipt()
	{
		currentReceipt = new Order
		{
			Date = DateTime.Now,
			OrderType = OrderType.Sale,
			sent = OrderEinvSend.NotSent,
			Status = "Draft"
		};
		receiptItems.Clear();
		RefreshGrid();
		UpdateNextReceiptNumber();
		txtBuyerId.Text = "";
		txtBuyerName.Text = "عميل نقدي (مستهلك)";
		txtBuyerMobile.Text = "";
		txtBarcode.Text = "";
		txtQty.Text = "1";
		CalculateTotals();
		txtBarcode.Focus();
	}

	private void btnNewReceipt_Click(object sender, EventArgs e)
	{
		NewReceipt();
	}

	private void btnSearchProduct_Click(object sender, EventArgs e)
	{
		Info._SelectedProduct = 0;
		frmProductSearch frm = new frmProductSearch();
		frm.ShowDialog();
		if (Info._SelectedProduct > 0)
		{
			var prod = _unitOfWork.Products.GetTById(Info._SelectedProduct);
			if (prod != null)
			{
				AddProductToReceipt(prod);
			}
		}
	}

	private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
			string barcode = txtBarcode.Text.Trim();
			if (!string.IsNullOrEmpty(barcode))
			{
				var prod = _unitOfWork.Products.GetAll().FirstOrDefault(p => p.Code == barcode || p.itemCode == barcode || p.Name.Contains(barcode));
				if (prod != null)
				{
					AddProductToReceipt(prod);
					txtBarcode.Text = "";
				}
				else
				{
					MessageBox.Show("لم يتم العثور على صنف بهذا الباركود أو الكود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}
	}

	private void btnAddItem_Click(object sender, EventArgs e)
	{
		string barcode = txtBarcode.Text.Trim();
		if (!string.IsNullOrEmpty(barcode))
		{
			var prod = _unitOfWork.Products.GetAll().FirstOrDefault(p => p.Code == barcode || p.itemCode == barcode || p.Name == barcode);
			if (prod != null)
			{
				AddProductToReceipt(prod);
				txtBarcode.Text = "";
			}
		}
	}

	private void AddProductToReceipt(Product prod)
	{
		double qty = 1;
		double.TryParse(txtQty.Text, out qty);
		if (qty <= 0) qty = 1;

		var existing = receiptItems.FirstOrDefault(i => i.ProductId == prod.Id);
		if (existing != null)
		{
			existing.Quantity += qty;
			existing.TotalPrice = existing.Quantity * existing.Price;
			existing.NetBeforeTax = existing.TotalPrice - existing.Discount;
			existing.VatPrice = existing.NetBeforeTax * (existing.Vat / 100.0);
			existing.NetAfterTax = existing.NetBeforeTax + existing.VatPrice;
		}
		else
		{
			double price = (double)prod.SalePrice;
			double vatPercent = 14.0; // ضريبة القيمة المضافة القياسية 14%
			double total = qty * price;
			double vatPrice = total * (vatPercent / 100.0);

			var itemDetail = new OrderDetail
			{
				ProductId = prod.Id,
				Product = prod,
				Quantity = qty,
				Price = price,
				TotalPrice = total,
				Discount = 0,
				NetBeforeTax = total,
				Vat = vatPercent,
				VatPrice = vatPrice,
				NetAfterTax = total + vatPrice
			};
			receiptItems.Add(itemDetail);
		}

		RefreshGrid();
		CalculateTotals();
		txtBarcode.Focus();
	}

	private void RefreshGrid()
	{
		dgvReceiptItems.Rows.Clear();
		int idx = 1;
		foreach (var item in receiptItems)
		{
			string pName = item.Product != null ? item.Product.Name : ("صنف " + item.ProductId);
			dgvReceiptItems.Rows.Add(
				idx++,
				item.ProductId,
				pName,
				item.Quantity,
				item.Price.ToString("N2"),
				item.Discount.ToString("N2"),
				item.VatPrice.ToString("N2"),
				item.NetAfterTax.ToString("N2")
			);
		}
	}

	private void CalculateTotals()
	{
		double totalBeforeTax = receiptItems.Sum(i => i.TotalPrice);
		double totalDiscount = receiptItems.Sum(i => i.Discount);
		double totalVat = receiptItems.Sum(i => i.VatPrice);
		double netTotal = (totalBeforeTax - totalDiscount) + totalVat;

		txtTotalBeforeTax.Text = totalBeforeTax.ToString("N2");
		txtTotalDiscount.Text = totalDiscount.ToString("N2");
		txtTotalVat.Text = totalVat.ToString("N2");
		txtNetTotal.Text = netTotal.ToString("N2");
	}

	private void dgvReceiptItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
	{
		if (e.Row.Index >= 0 && e.Row.Index < receiptItems.Count)
		{
			receiptItems.RemoveAt(e.Row.Index);
			CalculateTotals();
		}
	}

	private void btnSaveAndPrint_Click(object sender, EventArgs e)
	{
		if (receiptItems.Count == 0)
		{
			MessageBox.Show("يرجى إضافة أصناف للإيصال أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		if (combPosDevice.SelectedItem is not PosDevice pos)
		{
			MessageBox.Show("يرجى اختيار نقطة بيع صالحة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		try
		{
			// Set Header Info
			currentReceipt.PosDeviceId = pos.Id;
			currentReceipt.BranchId = pos.BranchId > 0 ? pos.BranchId : 1;
			currentReceipt.ReceiptNumber = pos.CurrentSequence;
			currentReceipt.FullReceiptNumber = (pos.ReceiptPrefix ?? "") + pos.CurrentSequence.ToString();
			currentReceipt.Date = dtReceiptDate.Value;
			currentReceipt.DateSent = null;
			currentReceipt.sent = OrderEinvSend.NotSent;
			currentReceipt.OrderType = OrderType.Sale;
			currentReceipt.CustumerName = string.IsNullOrWhiteSpace(txtBuyerName.Text) ? "مستهلك نهائي" : txtBuyerName.Text.Trim();
			currentReceipt.CustomerId = txtBuyerId.Text.Trim();
			currentReceipt.PaymentMethod = (combPaymentMethod.SelectedItem as KeyValueItem)?.Value ?? "C";

			// Financials
			currentReceipt.NetBeforeTax = receiptItems.Sum(i => i.TotalPrice - i.Discount);
			currentReceipt.TotalDiscount = receiptItems.Sum(i => i.Discount);
			currentReceipt.TotalVat = receiptItems.Sum(i => i.VatPrice);
			currentReceipt.NetInvoice = currentReceipt.NetBeforeTax + currentReceipt.TotalVat;
			currentReceipt.Paid = currentReceipt.NetInvoice;
			currentReceipt.Rest = 0;

			// Save Order
			_unitOfWork.Orders.Add(currentReceipt);

			// Save Details
			foreach (var detail in receiptItems)
			{
				detail.OrderId = currentReceipt.Id;
				_unitOfWork.OrderDetails.Add(detail);
			}

			// Increment Sequence for the POS Device atomically
			pos.CurrentSequence++;
			_unitOfWork.PosDevices.Update(pos);

			// Complete
			_unitOfWork.complete();

			MessageBox.Show($"تم حفظ الإيصال رقم ({currentReceipt.FullReceiptNumber}) بنجاح وجاهز للطباعة والترحيل الضريبي!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

			// Reset for next receipt
			NewReceipt();
		}
		catch (Exception ex)
		{
			MessageBox.Show("حدث خطأ أثناء حفظ الإيصال: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		this.Close();
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
		this.grpPosInfo = new System.Windows.Forms.GroupBox();
		this.lbPosDevice = new E_Invoice.Desktop.Controls.LabelEx();
		this.combPosDevice = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.lbReceiptNumber = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtReceiptNumber = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbReceiptDate = new E_Invoice.Desktop.Controls.LabelEx();
		this.dtReceiptDate = new E_Invoice.Desktop.Controls.DateTimePickerEx();

		this.grpBuyer = new System.Windows.Forms.GroupBox();
		this.lbBuyerType = new E_Invoice.Desktop.Controls.LabelEx();
		this.combBuyerType = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.lbBuyerId = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtBuyerId = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbBuyerName = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtBuyerName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbBuyerMobile = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtBuyerMobile = new E_Invoice.Desktop.Controls.TextBoxEx();

		this.grpItems = new System.Windows.Forms.GroupBox();
		this.lbBarcode = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtBarcode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.btnSearchProduct = new System.Windows.Forms.Button();
		this.lbQty = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtQty = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.btnAddItem = new System.Windows.Forms.Button();
		this.dgvReceiptItems = new System.Windows.Forms.DataGridView();

		this.grpTotals = new System.Windows.Forms.GroupBox();
		this.lbTotalBeforeTax = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtTotalBeforeTax = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTotalDiscount = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtTotalDiscount = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTotalVat = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtTotalVat = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbNetTotal = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtNetTotal = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbPaymentMethod = new E_Invoice.Desktop.Controls.LabelEx();
		this.combPaymentMethod = new E_Invoice.Desktop.Controls.ComboBoxEx();

		this.btnNewReceipt = new System.Windows.Forms.Button();
		this.btnSaveAndPrint = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();

		this.grpPosInfo.SuspendLayout();
		this.grpBuyer.SuspendLayout();
		this.grpItems.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceiptItems)).BeginInit();
		this.grpTotals.SuspendLayout();
		this.SuspendLayout();

		// grpPosInfo
		this.grpPosInfo.Controls.Add(this.dtReceiptDate);
		this.grpPosInfo.Controls.Add(this.lbReceiptDate);
		this.grpPosInfo.Controls.Add(this.txtReceiptNumber);
		this.grpPosInfo.Controls.Add(this.lbReceiptNumber);
		this.grpPosInfo.Controls.Add(this.combPosDevice);
		this.grpPosInfo.Controls.Add(this.lbPosDevice);
		this.grpPosInfo.Location = new System.Drawing.Point(12, 12);
		this.grpPosInfo.Name = "grpPosInfo";
		this.grpPosInfo.Size = new System.Drawing.Size(960, 65);
		this.grpPosInfo.TabIndex = 0;
		this.grpPosInfo.TabStop = false;
		this.grpPosInfo.Text = "بيانات نقطة البيع والإيصال";

		// lbPosDevice
		this.lbPosDevice.AutoSize = true;
		this.lbPosDevice.Location = new System.Drawing.Point(870, 28);
		this.lbPosDevice.Name = "lbPosDevice";
		this.lbPosDevice.Size = new System.Drawing.Size(74, 18);
		this.lbPosDevice.Text = "نقطة البيع:";

		// combPosDevice
		this.combPosDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combPosDevice.FormattingEnabled = true;
		this.combPosDevice.Location = new System.Drawing.Point(620, 24);
		this.combPosDevice.Name = "combPosDevice";
		this.combPosDevice.Size = new System.Drawing.Size(240, 26);
		this.combPosDevice.SelectedIndexChanged += new System.EventHandler(this.combPosDevice_SelectedIndexChanged);

		// lbReceiptNumber
		this.lbReceiptNumber.AutoSize = true;
		this.lbReceiptNumber.Location = new System.Drawing.Point(530, 28);
		this.lbReceiptNumber.Name = "lbReceiptNumber";
		this.lbReceiptNumber.Size = new System.Drawing.Size(78, 18);
		this.lbReceiptNumber.Text = "رقم الإيصال:";

		// txtReceiptNumber
		this.txtReceiptNumber.BackColor = System.Drawing.Color.LightYellow;
		this.txtReceiptNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
		this.txtReceiptNumber.Location = new System.Drawing.Point(340, 24);
		this.txtReceiptNumber.Name = "txtReceiptNumber";
		this.txtReceiptNumber.ReadOnly = true;
		this.txtReceiptNumber.Size = new System.Drawing.Size(180, 24);

		// lbReceiptDate
		this.lbReceiptDate.AutoSize = true;
		this.lbReceiptDate.Location = new System.Drawing.Point(260, 28);
		this.lbReceiptDate.Name = "lbReceiptDate";
		this.lbReceiptDate.Size = new System.Drawing.Size(48, 18);
		this.lbReceiptDate.Text = "التاريخ:";

		// dtReceiptDate
		this.dtReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtReceiptDate.Location = new System.Drawing.Point(40, 24);
		this.dtReceiptDate.Name = "dtReceiptDate";
		this.dtReceiptDate.Size = new System.Drawing.Size(210, 26);

		// grpBuyer
		this.grpBuyer.Controls.Add(this.txtBuyerMobile);
		this.grpBuyer.Controls.Add(this.lbBuyerMobile);
		this.grpBuyer.Controls.Add(this.txtBuyerName);
		this.grpBuyer.Controls.Add(this.lbBuyerName);
		this.grpBuyer.Controls.Add(this.txtBuyerId);
		this.grpBuyer.Controls.Add(this.lbBuyerId);
		this.grpBuyer.Controls.Add(this.combBuyerType);
		this.grpBuyer.Controls.Add(this.lbBuyerType);
		this.grpBuyer.Location = new System.Drawing.Point(12, 83);
		this.grpBuyer.Name = "grpBuyer";
		this.grpBuyer.Size = new System.Drawing.Size(960, 65);
		this.grpBuyer.TabIndex = 1;
		this.grpBuyer.TabStop = false;
		this.grpBuyer.Text = "بيانات المشتري (المستهلك)";

		// lbBuyerType
		this.lbBuyerType.AutoSize = true;
		this.lbBuyerType.Location = new System.Drawing.Point(880, 28);
		this.lbBuyerType.Name = "lbBuyerType";
		this.lbBuyerType.Size = new System.Drawing.Size(73, 18);
		this.lbBuyerType.Text = "نوع المشتري:";

		// combBuyerType
		this.combBuyerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combBuyerType.FormattingEnabled = true;
		this.combBuyerType.Location = new System.Drawing.Point(700, 24);
		this.combBuyerType.Name = "combBuyerType";
		this.combBuyerType.Size = new System.Drawing.Size(175, 26);

		// lbBuyerId
		this.lbBuyerId.AutoSize = true;
		this.lbBuyerId.Location = new System.Drawing.Point(610, 28);
		this.lbBuyerId.Name = "lbBuyerId";
		this.lbBuyerId.Size = new System.Drawing.Size(84, 18);
		this.lbBuyerId.Text = "الرقم القومي:";

		// txtBuyerId
		this.txtBuyerId.Location = new System.Drawing.Point(470, 24);
		this.txtBuyerId.Name = "txtBuyerId";
		this.txtBuyerId.Size = new System.Drawing.Size(135, 26);

		// lbBuyerName
		this.lbBuyerName.AutoSize = true;
		this.lbBuyerName.Location = new System.Drawing.Point(385, 28);
		this.lbBuyerName.Name = "lbBuyerName";
		this.lbBuyerName.Size = new System.Drawing.Size(81, 18);
		this.lbBuyerName.Text = "اسم العميل:";

		// txtBuyerName
		this.txtBuyerName.Location = new System.Drawing.Point(230, 24);
		this.txtBuyerName.Name = "txtBuyerName";
		this.txtBuyerName.Size = new System.Drawing.Size(150, 26);

		// lbBuyerMobile
		this.lbBuyerMobile.AutoSize = true;
		this.lbBuyerMobile.Location = new System.Drawing.Point(170, 28);
		this.lbBuyerMobile.Name = "lbBuyerMobile";
		this.lbBuyerMobile.Size = new System.Drawing.Size(55, 18);
		this.lbBuyerMobile.Text = "الموبايل:";

		// txtBuyerMobile
		this.txtBuyerMobile.Location = new System.Drawing.Point(20, 24);
		this.txtBuyerMobile.Name = "txtBuyerMobile";
		this.txtBuyerMobile.Size = new System.Drawing.Size(145, 26);

		// grpItems
		this.grpItems.Controls.Add(this.dgvReceiptItems);
		this.grpItems.Controls.Add(this.btnAddItem);
		this.grpItems.Controls.Add(this.txtQty);
		this.grpItems.Controls.Add(this.lbQty);
		this.grpItems.Controls.Add(this.btnSearchProduct);
		this.grpItems.Controls.Add(this.txtBarcode);
		this.grpItems.Controls.Add(this.lbBarcode);
		this.grpItems.Location = new System.Drawing.Point(12, 154);
		this.grpItems.Name = "grpItems";
		this.grpItems.Size = new System.Drawing.Size(960, 280);
		this.grpItems.TabIndex = 2;
		this.grpItems.TabStop = false;
		this.grpItems.Text = "أصناف الإيصال";

		// lbBarcode
		this.lbBarcode.AutoSize = true;
		this.lbBarcode.Location = new System.Drawing.Point(850, 28);
		this.lbBarcode.Name = "lbBarcode";
		this.lbBarcode.Size = new System.Drawing.Size(98, 18);
		this.lbBarcode.Text = "الباركود / الكود:";

		// txtBarcode
		this.txtBarcode.Location = new System.Drawing.Point(540, 24);
		this.txtBarcode.Name = "txtBarcode";
		this.txtBarcode.Size = new System.Drawing.Size(305, 26);
		this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);

		// btnSearchProduct
		this.btnSearchProduct.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.btnSearchProduct.Location = new System.Drawing.Point(500, 23);
		this.btnSearchProduct.Name = "btnSearchProduct";
		this.btnSearchProduct.Size = new System.Drawing.Size(35, 28);
		this.btnSearchProduct.UseVisualStyleBackColor = true;
		this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);

		// lbQty
		this.lbQty.AutoSize = true;
		this.lbQty.Location = new System.Drawing.Point(435, 28);
		this.lbQty.Name = "lbQty";
		this.lbQty.Size = new System.Drawing.Size(53, 18);
		this.lbQty.Text = "الكمية:";

		// txtQty
		this.txtQty.IsNumber = true;
		this.txtQty.Location = new System.Drawing.Point(340, 24);
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(90, 26);
		this.txtQty.Text = "1";

		// btnAddItem
		this.btnAddItem.BackColor = System.Drawing.Color.SteelBlue;
		this.btnAddItem.ForeColor = System.Drawing.Color.White;
		this.btnAddItem.Location = new System.Drawing.Point(200, 22);
		this.btnAddItem.Name = "btnAddItem";
		this.btnAddItem.Size = new System.Drawing.Size(125, 30);
		this.btnAddItem.Text = "إضافة صنف +";
		this.btnAddItem.UseVisualStyleBackColor = false;
		this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

		// dgvReceiptItems
		this.dgvReceiptItems.AllowUserToAddRows = false;
		this.dgvReceiptItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvReceiptItems.BackgroundColor = System.Drawing.Color.White;
		this.dgvReceiptItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvReceiptItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "م", Width = 40 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "كود الصنف", Width = 90 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "اسم الصنف", Width = 250 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الكمية", Width = 80 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "السعر", Width = 90 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الخصم", Width = 80 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الضريبة", Width = 90 },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الإجمالي", Width = 110 }
		});
		this.dgvReceiptItems.Location = new System.Drawing.Point(15, 60);
		this.dgvReceiptItems.Name = "dgvReceiptItems";
		this.dgvReceiptItems.RowHeadersWidth = 30;
		this.dgvReceiptItems.Size = new System.Drawing.Size(930, 205);
		this.dgvReceiptItems.TabIndex = 6;
		this.dgvReceiptItems.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvReceiptItems_UserDeletingRow);

		// grpTotals
		this.grpTotals.Controls.Add(this.combPaymentMethod);
		this.grpTotals.Controls.Add(this.lbPaymentMethod);
		this.grpTotals.Controls.Add(this.txtNetTotal);
		this.grpTotals.Controls.Add(this.lbNetTotal);
		this.grpTotals.Controls.Add(this.txtTotalVat);
		this.grpTotals.Controls.Add(this.lbTotalVat);
		this.grpTotals.Controls.Add(this.txtTotalDiscount);
		this.grpTotals.Controls.Add(this.lbTotalDiscount);
		this.grpTotals.Controls.Add(this.txtTotalBeforeTax);
		this.grpTotals.Controls.Add(this.lbTotalBeforeTax);
		this.grpTotals.Location = new System.Drawing.Point(12, 440);
		this.grpTotals.Name = "grpTotals";
		this.grpTotals.Size = new System.Drawing.Size(960, 75);
		this.grpTotals.TabIndex = 3;
		this.grpTotals.TabStop = false;
		this.grpTotals.Text = "الإجماليات وطريقة السداد";

		// lbTotalBeforeTax
		this.lbTotalBeforeTax.AutoSize = true;
		this.lbTotalBeforeTax.Location = new System.Drawing.Point(870, 32);
		this.lbTotalBeforeTax.Name = "lbTotalBeforeTax";
		this.lbTotalBeforeTax.Size = new System.Drawing.Size(82, 18);
		this.lbTotalBeforeTax.Text = "قبل الضريبة:";

		// txtTotalBeforeTax
		this.txtTotalBeforeTax.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtTotalBeforeTax.Location = new System.Drawing.Point(770, 28);
		this.txtTotalBeforeTax.Name = "txtTotalBeforeTax";
		this.txtTotalBeforeTax.ReadOnly = true;
		this.txtTotalBeforeTax.Size = new System.Drawing.Size(95, 26);
		this.txtTotalBeforeTax.Text = "0.00";

		// lbTotalDiscount
		this.lbTotalDiscount.AutoSize = true;
		this.lbTotalDiscount.Location = new System.Drawing.Point(715, 32);
		this.lbTotalDiscount.Name = "lbTotalDiscount";
		this.lbTotalDiscount.Size = new System.Drawing.Size(51, 18);
		this.lbTotalDiscount.Text = "الخصم:";

		// txtTotalDiscount
		this.txtTotalDiscount.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtTotalDiscount.Location = new System.Drawing.Point(625, 28);
		this.txtTotalDiscount.Name = "txtTotalDiscount";
		this.txtTotalDiscount.ReadOnly = true;
		this.txtTotalDiscount.Size = new System.Drawing.Size(85, 26);
		this.txtTotalDiscount.Text = "0.00";

		// lbTotalVat
		this.lbTotalVat.AutoSize = true;
		this.lbTotalVat.Location = new System.Drawing.Point(555, 32);
		this.lbTotalVat.Name = "lbTotalVat";
		this.lbTotalVat.Size = new System.Drawing.Size(65, 18);
		this.lbTotalVat.Text = "الضريبة:";

		// txtTotalVat
		this.txtTotalVat.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtTotalVat.Location = new System.Drawing.Point(465, 28);
		this.txtTotalVat.Name = "txtTotalVat";
		this.txtTotalVat.ReadOnly = true;
		this.txtTotalVat.Size = new System.Drawing.Size(85, 26);
		this.txtTotalVat.Text = "0.00";

		// lbNetTotal
		this.lbNetTotal.AutoSize = true;
		this.lbNetTotal.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold);
		this.lbNetTotal.ForeColor = System.Drawing.Color.DarkGreen;
		this.lbNetTotal.Location = new System.Drawing.Point(365, 32);
		this.lbNetTotal.Name = "lbNetTotal";
		this.lbNetTotal.Size = new System.Drawing.Size(95, 18);
		this.lbNetTotal.Text = "صافي الإيصال:";

		// txtNetTotal
		this.txtNetTotal.BackColor = System.Drawing.Color.LightGreen;
		this.txtNetTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
		this.txtNetTotal.ForeColor = System.Drawing.Color.DarkGreen;
		this.txtNetTotal.Location = new System.Drawing.Point(235, 28);
		this.txtNetTotal.Name = "txtNetTotal";
		this.txtNetTotal.ReadOnly = true;
		this.txtNetTotal.Size = new System.Drawing.Size(125, 27);
		this.txtNetTotal.Text = "0.00";

		// lbPaymentMethod
		this.lbPaymentMethod.AutoSize = true;
		this.lbPaymentMethod.Location = new System.Drawing.Point(145, 32);
		this.lbPaymentMethod.Name = "lbPaymentMethod";
		this.lbPaymentMethod.Size = new System.Drawing.Size(83, 18);
		this.lbPaymentMethod.Text = "طريقة الدفع:";

		// combPaymentMethod
		this.combPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combPaymentMethod.FormattingEnabled = true;
		this.combPaymentMethod.Location = new System.Drawing.Point(15, 28);
		this.combPaymentMethod.Name = "combPaymentMethod";
		this.combPaymentMethod.Size = new System.Drawing.Size(125, 26);

		// btnNewReceipt
		this.btnNewReceipt.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
		this.btnNewReceipt.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold);
		this.btnNewReceipt.Location = new System.Drawing.Point(680, 525);
		this.btnNewReceipt.Name = "btnNewReceipt";
		this.btnNewReceipt.Size = new System.Drawing.Size(140, 42);
		this.btnNewReceipt.Text = "إيصال جديد [F2]";
		this.btnNewReceipt.UseVisualStyleBackColor = false;
		this.btnNewReceipt.Click += new System.EventHandler(this.btnNewReceipt_Click);

		// btnSaveAndPrint
		this.btnSaveAndPrint.BackColor = System.Drawing.Color.ForestGreen;
		this.btnSaveAndPrint.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
		this.btnSaveAndPrint.ForeColor = System.Drawing.Color.White;
		this.btnSaveAndPrint.Location = new System.Drawing.Point(440, 525);
		this.btnSaveAndPrint.Name = "btnSaveAndPrint";
		this.btnSaveAndPrint.Size = new System.Drawing.Size(220, 42);
		this.btnSaveAndPrint.Text = "حفظ وطباعة الإيصال [F5]";
		this.btnSaveAndPrint.UseVisualStyleBackColor = false;
		this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);

		// btnClose
		this.btnClose.BackColor = System.Drawing.Color.Crimson;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold);
		this.btnClose.ForeColor = System.Drawing.Color.White;
		this.btnClose.Location = new System.Drawing.Point(300, 525);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(120, 42);
		this.btnClose.Text = "إغلاق";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

		// frmReceipt
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.ClientSize = new System.Drawing.Size(984, 580);
		this.Controls.Add(this.btnClose);
		this.Controls.Add(this.btnSaveAndPrint);
		this.Controls.Add(this.btnNewReceipt);
		this.Controls.Add(this.grpTotals);
		this.Controls.Add(this.grpItems);
		this.Controls.Add(this.grpBuyer);
		this.Controls.Add(this.grpPosInfo);
		this.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.KeyPreview = true;
		this.MaximizeBox = false;
		this.Name = "frmReceipt";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "إصدار إيصال إلكتروني - مبيعات الكاشير (E-Receipt POS)";
		this.grpPosInfo.ResumeLayout(false);
		this.grpPosInfo.PerformLayout();
		this.grpBuyer.ResumeLayout(false);
		this.grpBuyer.PerformLayout();
		this.grpItems.ResumeLayout(false);
		this.grpItems.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceiptItems)).EndInit();
		this.grpTotals.ResumeLayout(false);
		this.grpTotals.PerformLayout();
		this.ResumeLayout(false);
	}
}
