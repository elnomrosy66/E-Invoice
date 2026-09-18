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
	private bool _isUpdatingGrid = false;
	private OrderType _receiptType = OrderType.Sale;
	private string _referenceOldUUID = null;

	// Controls
	private GroupBox grpPosInfo;
	private ComboBoxEx combPosDevice;
	private LabelEx lbPosDevice;
	private TextBoxEx txtReceiptNumber;
	private LabelEx lbReceiptNumber;
	private DateTimePickerEx dtReceiptDate;
	private LabelEx lbReceiptDate;
	private ComboBoxEx combReceiptType;
	private LabelEx lbReceiptType;
	private TextBoxEx txtOriginalReceipt;
	private LabelEx lbOriginalReceipt;
	private Button btnSearchOriginalReceipt;

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

	public frmReceipt(OrderType orderType = OrderType.Sale)
	{
		_receiptType = orderType;
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		LoadReceiptTypes();
		LoadPosDevices();
		LoadBuyerTypes();
		LoadPaymentMethods();
		ApplyReceiptTypeUI();
		NewReceipt();
	}

	private void LoadReceiptTypes()
	{
		combReceiptType.Items.Clear();
		combReceiptType.Items.Add(new KeyValueItem { Text = "إيصال بيع (Sale)", Value = "Sale" });
		combReceiptType.Items.Add(new KeyValueItem { Text = "مرتجع إيصال (Return)", Value = "SaleReturn" });
		combReceiptType.DisplayMember = "Text";
		combReceiptType.ValueMember = "Value";
		combReceiptType.SelectedIndex = _receiptType == OrderType.SaleReturn ? 1 : 0;
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

	private void combReceiptType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (combReceiptType.SelectedItem is KeyValueItem selected)
		{
			_receiptType = selected.Value == "SaleReturn" ? OrderType.SaleReturn : OrderType.Sale;
			ApplyReceiptTypeUI();
		}
	}

	private void ApplyReceiptTypeUI()
	{
		if (_receiptType == OrderType.SaleReturn)
		{
			this.Text = "إصدار مرتجع إيصال إلكتروني - (Return E-Receipt POS)";
			this.grpPosInfo.Text = "بيانات المرتجع ونقطة البيع [وضع مرتجع]";
			this.lbOriginalReceipt.Visible = true;
			this.txtOriginalReceipt.Visible = true;
			this.btnSearchOriginalReceipt.Visible = true;
			this.btnSaveAndPrint.Text = "حفظ وطباعة المرتجع [F5]";
			this.btnSaveAndPrint.BackColor = System.Drawing.Color.DarkOrange;
		}
		else
		{
			this.Text = "إصدار إيصال إلكتروني - مبيعات الكاشير (E-Receipt POS)";
			this.grpPosInfo.Text = "بيانات نقطة البيع والإيصال";
			this.lbOriginalReceipt.Visible = false;
			this.txtOriginalReceipt.Visible = false;
			this.btnSearchOriginalReceipt.Visible = false;
			this.btnSaveAndPrint.Text = "حفظ وطباعة الإيصال [F5]";
			this.btnSaveAndPrint.BackColor = System.Drawing.Color.ForestGreen;
		}
	}

	private void NewReceipt()
	{
		currentReceipt = new Order
		{
			Date = DateTime.Now,
			OrderType = _receiptType,
			sent = OrderEinvSend.NotSent,
			Status = "Draft"
		};
		receiptItems.Clear();
		_referenceOldUUID = null;
		txtOriginalReceipt.Text = "";
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

	private void btnSearchOriginalReceipt_Click(object sender, EventArgs e)
	{
		frmReceiptSearch searchForm = new frmReceiptSearch();
		if (searchForm.ShowDialog() == DialogResult.OK && searchForm.SelectedReceipt != null)
		{
			var originalOrder = searchForm.SelectedReceipt;

			// Switch to return mode
			_receiptType = OrderType.SaleReturn;
			combReceiptType.SelectedIndex = 1;

			// Set reference
			_referenceOldUUID = originalOrder.uuid ?? originalOrder.FullReceiptNumber ?? originalOrder.Id.ToString();
			txtOriginalReceipt.Text = originalOrder.FullReceiptNumber ?? originalOrder.ReceiptNumber?.ToString() ?? originalOrder.Id.ToString();

			// Set buyer details
			txtBuyerName.Text = originalOrder.CustumerName ?? "عميل نقدي";
			txtBuyerId.Text = originalOrder.CustomerId ?? "";

			// Load items
			receiptItems.Clear();
			if (originalOrder.OrderDetails != null && originalOrder.OrderDetails.Count > 0)
			{
				foreach (var origItem in originalOrder.OrderDetails)
				{
					var prod = origItem.Product ?? (origItem.ProductId.HasValue && origItem.ProductId.Value > 0 ? _unitOfWork.Products.GetTById(origItem.ProductId.Value) : null);
					var returnItem = new OrderDetail
					{
						ProductId = origItem.ProductId,
						Product = prod,
						Quantity = origItem.Quantity,
						Price = origItem.Price,
						TotalPrice = origItem.TotalPrice,
						Discount = origItem.Discount,
						NetBeforeTax = origItem.NetBeforeTax,
						Vat = origItem.Vat > 0 ? origItem.Vat : 14.0,
						VatPrice = origItem.VatPrice,
						NetAfterTax = origItem.NetAfterTax
					};
					receiptItems.Add(returnItem);
				}
			}

			RefreshGrid();
			CalculateTotals();
			MessageBox.Show("تم استيراد بيانات وأصناف الإيصال الأصلي بنجاح. يمكنك تعديل كميات الأصناف المرتجعة أو حذف ما لم يتم إرجاعه.", "استيراد ناجح", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
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
		if (!double.TryParse(txtQty.Text, out double qty) || qty <= 0)
		{
			MessageBox.Show("الكمية يجب أن تكون رقماً أكبر من الصفر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			txtQty.Text = "1";
			return;
		}

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
		_isUpdatingGrid = true;
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
		_isUpdatingGrid = false;
	}

	private void dgvReceiptItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		if (e.RowIndex < 0 || e.RowIndex >= receiptItems.Count) return;

		string enteredValue = e.FormattedValue?.ToString().Trim() ?? "";

		// Column 3: الكمية (Quantity)
		if (e.ColumnIndex == 3)
		{
			if (!double.TryParse(enteredValue, out double qty) || qty <= 0)
			{
				MessageBox.Show("الكمية يجب أن تكون رقماً أكبر من الصفر ولا يمكن أن تكون سالبة أو صفراً.", "تنبيه فاليديشن", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Cancel = true;
			}
		}
		// Column 4: السعر (Price)
		else if (e.ColumnIndex == 4)
		{
			if (!double.TryParse(enteredValue, out double price) || price < 0)
			{
				MessageBox.Show("سعر بيع الصنف لا يمكن أن يكون سالباً.", "تنبيه فاليديشن", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Cancel = true;
			}
		}
		// Column 5: الخصم (Discount)
		else if (e.ColumnIndex == 5)
		{
			if (!double.TryParse(enteredValue, out double discount) || discount < 0)
			{
				MessageBox.Show("قيمة الخصم لا يمكن أن تكون سالبة.", "تنبيه فاليديشن", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Cancel = true;
				return;
			}

			// Validate that discount does not exceed item total price (Qty * Price)
			var row = dgvReceiptItems.Rows[e.RowIndex];
			double currentQty = double.TryParse(row.Cells[3].Value?.ToString(), out double q) ? q : 1;
			double currentPrice = double.TryParse(row.Cells[4].Value?.ToString(), out double p) ? p : 0;
			double lineTotal = currentQty * currentPrice;

			if (discount > lineTotal)
			{
				MessageBox.Show($"قيمة الخصم ({discount:N2}) لا يمكن أن تكون أكبر من إجمالي سعر الصنف ({lineTotal:N2}).", "تنبيه فاليديشن", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Cancel = true;
			}
		}
	}

	private void dgvReceiptItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (_isUpdatingGrid || e.RowIndex < 0 || e.RowIndex >= receiptItems.Count) return;

		// When Quantity (3), Price (4), or Discount (5) changes, recalculate row
		if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
		{
			RecalculateRow(e.RowIndex);
		}
	}

	private void RecalculateRow(int rowIndex)
	{
		if (rowIndex < 0 || rowIndex >= receiptItems.Count) return;

		var item = receiptItems[rowIndex];
		var row = dgvReceiptItems.Rows[rowIndex];

		double qty = double.TryParse(row.Cells[3].Value?.ToString(), out double q) ? q : 1;
		double price = double.TryParse(row.Cells[4].Value?.ToString(), out double p) ? p : 0;
		double discount = double.TryParse(row.Cells[5].Value?.ToString(), out double d) ? d : 0;

		if (qty <= 0) qty = 1;
		if (price < 0) price = 0;
		if (discount < 0) discount = 0;
		if (discount > (qty * price)) discount = qty * price;

		item.Quantity = qty;
		item.Price = price;
		item.TotalPrice = qty * price;
		item.Discount = discount;
		item.NetBeforeTax = item.TotalPrice - item.Discount;

		double vatPercent = item.Vat > 0 ? item.Vat : 14.0;
		item.Vat = vatPercent;
		item.VatPrice = item.NetBeforeTax * (vatPercent / 100.0);
		item.NetAfterTax = item.NetBeforeTax + item.VatPrice;

		_isUpdatingGrid = true;
		row.Cells[3].Value = item.Quantity;
		row.Cells[4].Value = item.Price.ToString("N2");
		row.Cells[5].Value = item.Discount.ToString("N2");
		row.Cells[6].Value = item.VatPrice.ToString("N2");
		row.Cells[7].Value = item.NetAfterTax.ToString("N2");
		_isUpdatingGrid = false;

		CalculateTotals();
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
		if (e.Row != null && e.Row.Index >= 0 && e.Row.Index < receiptItems.Count)
		{
			receiptItems.RemoveAt(e.Row.Index);
			BeginInvoke(new Action(CalculateTotals));
		}
	}

	private void dgvReceiptItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		e.ThrowException = false;
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

		// If Return Receipt, check reference
		if (_receiptType == OrderType.SaleReturn && string.IsNullOrEmpty(txtOriginalReceipt.Text.Trim()))
		{
			var confirmNoRef = MessageBox.Show("لم يتم تحديد مرجع الإيصال الأصلي. هل تريد الاستمرار في حفظ المرتجع كمرتجع عام؟", "تأكيد المرتجع", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (confirmNoRef != DialogResult.Yes)
			{
				return;
			}
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
			currentReceipt.OrderType = _receiptType;
			currentReceipt.ReferenceOldUUID = _referenceOldUUID ?? txtOriginalReceipt.Text.Trim();
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

			// Complete transaction
			_unitOfWork.complete();

			string docTitle = _receiptType == OrderType.SaleReturn ? "إيصال المرتجع" : "الإيصال";
			MessageBox.Show($"تم حفظ {docTitle} رقم ({currentReceipt.FullReceiptNumber}) بنجاح وجاهز للطباعة والترحيل الضريبي!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
		this.btnSearchOriginalReceipt = new System.Windows.Forms.Button();
		this.txtOriginalReceipt = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbOriginalReceipt = new E_Invoice.Desktop.Controls.LabelEx();
		this.combReceiptType = new E_Invoice.Desktop.Controls.ComboBoxEx();
		this.lbReceiptType = new E_Invoice.Desktop.Controls.LabelEx();
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
		this.grpPosInfo.Controls.Add(this.btnSearchOriginalReceipt);
		this.grpPosInfo.Controls.Add(this.txtOriginalReceipt);
		this.grpPosInfo.Controls.Add(this.lbOriginalReceipt);
		this.grpPosInfo.Controls.Add(this.combReceiptType);
		this.grpPosInfo.Controls.Add(this.lbReceiptType);
		this.grpPosInfo.Controls.Add(this.dtReceiptDate);
		this.grpPosInfo.Controls.Add(this.lbReceiptDate);
		this.grpPosInfo.Controls.Add(this.txtReceiptNumber);
		this.grpPosInfo.Controls.Add(this.lbReceiptNumber);
		this.grpPosInfo.Controls.Add(this.combPosDevice);
		this.grpPosInfo.Controls.Add(this.lbPosDevice);
		this.grpPosInfo.Location = new System.Drawing.Point(12, 8);
		this.grpPosInfo.Name = "grpPosInfo";
		this.grpPosInfo.Size = new System.Drawing.Size(960, 95);
		this.grpPosInfo.TabIndex = 0;
		this.grpPosInfo.TabStop = false;
		this.grpPosInfo.Text = "بيانات نقطة البيع والإيصال";

		// lbReceiptType
		this.lbReceiptType.AutoSize = true;
		this.lbReceiptType.Location = new System.Drawing.Point(880, 26);
		this.lbReceiptType.Name = "lbReceiptType";
		this.lbReceiptType.Size = new System.Drawing.Size(73, 18);
		this.lbReceiptType.Text = "نوع الإيصال:";

		// combReceiptType
		this.combReceiptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combReceiptType.FormattingEnabled = true;
		this.combReceiptType.Location = new System.Drawing.Point(740, 22);
		this.combReceiptType.Name = "combReceiptType";
		this.combReceiptType.Size = new System.Drawing.Size(140, 26);
		this.combReceiptType.SelectedIndexChanged += new System.EventHandler(this.combReceiptType_SelectedIndexChanged);

		// lbPosDevice
		this.lbPosDevice.AutoSize = true;
		this.lbPosDevice.Location = new System.Drawing.Point(655, 26);
		this.lbPosDevice.Name = "lbPosDevice";
		this.lbPosDevice.Size = new System.Drawing.Size(74, 18);
		this.lbPosDevice.Text = "نقطة البيع:";

		// combPosDevice
		this.combPosDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combPosDevice.FormattingEnabled = true;
		this.combPosDevice.Location = new System.Drawing.Point(485, 22);
		this.combPosDevice.Name = "combPosDevice";
		this.combPosDevice.Size = new System.Drawing.Size(165, 26);
		this.combPosDevice.SelectedIndexChanged += new System.EventHandler(this.combPosDevice_SelectedIndexChanged);

		// lbReceiptNumber
		this.lbReceiptNumber.AutoSize = true;
		this.lbReceiptNumber.Location = new System.Drawing.Point(400, 26);
		this.lbReceiptNumber.Name = "lbReceiptNumber";
		this.lbReceiptNumber.Size = new System.Drawing.Size(78, 18);
		this.lbReceiptNumber.Text = "رقم الإيصال:";

		// txtReceiptNumber
		this.txtReceiptNumber.BackColor = System.Drawing.Color.LightYellow;
		this.txtReceiptNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
		this.txtReceiptNumber.Location = new System.Drawing.Point(265, 23);
		this.txtReceiptNumber.Name = "txtReceiptNumber";
		this.txtReceiptNumber.ReadOnly = true;
		this.txtReceiptNumber.Size = new System.Drawing.Size(130, 24);

		// lbReceiptDate
		this.lbReceiptDate.AutoSize = true;
		this.lbReceiptDate.Location = new System.Drawing.Point(205, 26);
		this.lbReceiptDate.Name = "lbReceiptDate";
		this.lbReceiptDate.Size = new System.Drawing.Size(48, 18);
		this.lbReceiptDate.Text = "التاريخ:";

		// dtReceiptDate
		this.dtReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtReceiptDate.Location = new System.Drawing.Point(15, 22);
		this.dtReceiptDate.Name = "dtReceiptDate";
		this.dtReceiptDate.Size = new System.Drawing.Size(185, 26);

		// lbOriginalReceipt
		this.lbOriginalReceipt.AutoSize = true;
		this.lbOriginalReceipt.ForeColor = System.Drawing.Color.DarkRed;
		this.lbOriginalReceipt.Location = new System.Drawing.Point(850, 62);
		this.lbOriginalReceipt.Name = "lbOriginalReceipt";
		this.lbOriginalReceipt.Size = new System.Drawing.Size(102, 18);
		this.lbOriginalReceipt.Text = "الإيصال الأصلي:";
		this.lbOriginalReceipt.Visible = false;

		// txtOriginalReceipt
		this.txtOriginalReceipt.BackColor = System.Drawing.Color.MistyRose;
		this.txtOriginalReceipt.Location = new System.Drawing.Point(620, 58);
		this.txtOriginalReceipt.Name = "txtOriginalReceipt";
		this.txtOriginalReceipt.Size = new System.Drawing.Size(225, 26);
		this.txtOriginalReceipt.Visible = false;

		// btnSearchOriginalReceipt
		this.btnSearchOriginalReceipt.BackColor = System.Drawing.Color.SteelBlue;
		this.btnSearchOriginalReceipt.ForeColor = System.Drawing.Color.White;
		this.btnSearchOriginalReceipt.Location = new System.Drawing.Point(375, 56);
		this.btnSearchOriginalReceipt.Name = "btnSearchOriginalReceipt";
		this.btnSearchOriginalReceipt.Size = new System.Drawing.Size(235, 30);
		this.btnSearchOriginalReceipt.Text = "🔍 بحث واسترجاع إيصال سابق";
		this.btnSearchOriginalReceipt.UseVisualStyleBackColor = false;
		this.btnSearchOriginalReceipt.Visible = false;
		this.btnSearchOriginalReceipt.Click += new System.EventHandler(this.btnSearchOriginalReceipt_Click);

		// grpBuyer
		this.grpBuyer.Controls.Add(this.txtBuyerMobile);
		this.grpBuyer.Controls.Add(this.lbBuyerMobile);
		this.grpBuyer.Controls.Add(this.txtBuyerName);
		this.grpBuyer.Controls.Add(this.lbBuyerName);
		this.grpBuyer.Controls.Add(this.txtBuyerId);
		this.grpBuyer.Controls.Add(this.lbBuyerId);
		this.grpBuyer.Controls.Add(this.combBuyerType);
		this.grpBuyer.Controls.Add(this.lbBuyerType);
		this.grpBuyer.Location = new System.Drawing.Point(12, 107);
		this.grpBuyer.Name = "grpBuyer";
		this.grpBuyer.Size = new System.Drawing.Size(960, 62);
		this.grpBuyer.TabIndex = 1;
		this.grpBuyer.TabStop = false;
		this.grpBuyer.Text = "بيانات المشتري (المستهلك)";

		// lbBuyerType
		this.lbBuyerType.AutoSize = true;
		this.lbBuyerType.Location = new System.Drawing.Point(880, 26);
		this.lbBuyerType.Name = "lbBuyerType";
		this.lbBuyerType.Size = new System.Drawing.Size(73, 18);
		this.lbBuyerType.Text = "نوع المشتري:";

		// combBuyerType
		this.combBuyerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combBuyerType.FormattingEnabled = true;
		this.combBuyerType.Location = new System.Drawing.Point(700, 22);
		this.combBuyerType.Name = "combBuyerType";
		this.combBuyerType.Size = new System.Drawing.Size(175, 26);

		// lbBuyerId
		this.lbBuyerId.AutoSize = true;
		this.lbBuyerId.Location = new System.Drawing.Point(610, 26);
		this.lbBuyerId.Name = "lbBuyerId";
		this.lbBuyerId.Size = new System.Drawing.Size(84, 18);
		this.lbBuyerId.Text = "الرقم القومي:";

		// txtBuyerId
		this.txtBuyerId.Location = new System.Drawing.Point(470, 22);
		this.txtBuyerId.Name = "txtBuyerId";
		this.txtBuyerId.Size = new System.Drawing.Size(135, 26);

		// lbBuyerName
		this.lbBuyerName.AutoSize = true;
		this.lbBuyerName.Location = new System.Drawing.Point(385, 26);
		this.lbBuyerName.Name = "lbBuyerName";
		this.lbBuyerName.Size = new System.Drawing.Size(81, 18);
		this.lbBuyerName.Text = "اسم العميل:";

		// txtBuyerName
		this.txtBuyerName.Location = new System.Drawing.Point(230, 22);
		this.txtBuyerName.Name = "txtBuyerName";
		this.txtBuyerName.Size = new System.Drawing.Size(150, 26);

		// lbBuyerMobile
		this.lbBuyerMobile.AutoSize = true;
		this.lbBuyerMobile.Location = new System.Drawing.Point(170, 26);
		this.lbBuyerMobile.Name = "lbBuyerMobile";
		this.lbBuyerMobile.Size = new System.Drawing.Size(55, 18);
		this.lbBuyerMobile.Text = "الموبايل:";

		// txtBuyerMobile
		this.txtBuyerMobile.Location = new System.Drawing.Point(15, 22);
		this.txtBuyerMobile.Name = "txtBuyerMobile";
		this.txtBuyerMobile.Size = new System.Drawing.Size(150, 26);

		// grpItems
		this.grpItems.Controls.Add(this.dgvReceiptItems);
		this.grpItems.Controls.Add(this.btnAddItem);
		this.grpItems.Controls.Add(this.txtQty);
		this.grpItems.Controls.Add(this.lbQty);
		this.grpItems.Controls.Add(this.btnSearchProduct);
		this.grpItems.Controls.Add(this.txtBarcode);
		this.grpItems.Controls.Add(this.lbBarcode);
		this.grpItems.Location = new System.Drawing.Point(12, 172);
		this.grpItems.Name = "grpItems";
		this.grpItems.Size = new System.Drawing.Size(960, 275);
		this.grpItems.TabIndex = 2;
		this.grpItems.TabStop = false;
		this.grpItems.Text = "أصناف الإيصال (يمكنك تعديل الكمية والسعر والخصم مباشرة في الجدول)";

		// lbBarcode
		this.lbBarcode.AutoSize = true;
		this.lbBarcode.Location = new System.Drawing.Point(850, 26);
		this.lbBarcode.Name = "lbBarcode";
		this.lbBarcode.Size = new System.Drawing.Size(98, 18);
		this.lbBarcode.Text = "الباركود / الكود:";

		// txtBarcode
		this.txtBarcode.Location = new System.Drawing.Point(540, 22);
		this.txtBarcode.Name = "txtBarcode";
		this.txtBarcode.Size = new System.Drawing.Size(305, 26);
		this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);

		// btnSearchProduct
		this.btnSearchProduct.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.btnSearchProduct.Location = new System.Drawing.Point(500, 21);
		this.btnSearchProduct.Name = "btnSearchProduct";
		this.btnSearchProduct.Size = new System.Drawing.Size(35, 28);
		this.btnSearchProduct.UseVisualStyleBackColor = true;
		this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);

		// lbQty
		this.lbQty.AutoSize = true;
		this.lbQty.Location = new System.Drawing.Point(435, 26);
		this.lbQty.Name = "lbQty";
		this.lbQty.Size = new System.Drawing.Size(53, 18);
		this.lbQty.Text = "الكمية:";

		// txtQty
		this.txtQty.IsNumber = true;
		this.txtQty.Location = new System.Drawing.Point(340, 22);
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(90, 26);
		this.txtQty.Text = "1";

		// btnAddItem
		this.btnAddItem.BackColor = System.Drawing.Color.SteelBlue;
		this.btnAddItem.ForeColor = System.Drawing.Color.White;
		this.btnAddItem.Location = new System.Drawing.Point(200, 20);
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
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "م", Width = 40, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "كود الصنف", Width = 90, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "اسم الصنف", Width = 250, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الكمية", Width = 80, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "السعر", Width = 90, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الخصم", Width = 80, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الضريبة", Width = 90, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الإجمالي", Width = 110, ReadOnly = true }
		});
		this.dgvReceiptItems.Location = new System.Drawing.Point(15, 56);
		this.dgvReceiptItems.Name = "dgvReceiptItems";
		this.dgvReceiptItems.RowHeadersWidth = 30;
		this.dgvReceiptItems.Size = new System.Drawing.Size(930, 205);
		this.dgvReceiptItems.TabIndex = 6;
		this.dgvReceiptItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvReceiptItems_CellValidating);
		this.dgvReceiptItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceiptItems_CellValueChanged);
		this.dgvReceiptItems.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvReceiptItems_UserDeletingRow);
		this.dgvReceiptItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvReceiptItems_DataError);

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
		this.grpTotals.Location = new System.Drawing.Point(12, 452);
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
		this.btnNewReceipt.Location = new System.Drawing.Point(680, 532);
		this.btnNewReceipt.Name = "btnNewReceipt";
		this.btnNewReceipt.Size = new System.Drawing.Size(140, 42);
		this.btnNewReceipt.Text = "إيصال جديد [F2]";
		this.btnNewReceipt.UseVisualStyleBackColor = false;
		this.btnNewReceipt.Click += new System.EventHandler(this.btnNewReceipt_Click);

		// btnSaveAndPrint
		this.btnSaveAndPrint.BackColor = System.Drawing.Color.ForestGreen;
		this.btnSaveAndPrint.Font = new System.Drawing.Font("Tahoma", 11.5F, System.Drawing.FontStyle.Bold);
		this.btnSaveAndPrint.ForeColor = System.Drawing.Color.White;
		this.btnSaveAndPrint.Location = new System.Drawing.Point(440, 532);
		this.btnSaveAndPrint.Name = "btnSaveAndPrint";
		this.btnSaveAndPrint.Size = new System.Drawing.Size(220, 42);
		this.btnSaveAndPrint.Text = "حفظ وطباعة الإيصال [F5]";
		this.btnSaveAndPrint.UseVisualStyleBackColor = false;
		this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);

		// btnClose
		this.btnClose.BackColor = System.Drawing.Color.Crimson;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold);
		this.btnClose.ForeColor = System.Drawing.Color.White;
		this.btnClose.Location = new System.Drawing.Point(300, 532);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(120, 42);
		this.btnClose.Text = "إغلاق";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

		// frmReceipt
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.ClientSize = new System.Drawing.Size(984, 586);
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
