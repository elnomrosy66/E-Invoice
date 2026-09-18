using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Helpers;
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

	// Top Banner
	private Panel pnlHeader;
	private Label lbHeaderTitle;
	private Label lbHeaderBadge;
	private Label lbBranchInfo;

	// Cards
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

	// Totals Ribbon
	private Panel pnlTotals;
	private Panel pnlNetTotalCard;
	private Label lbNetTotalTitle;
	private TextBoxEx txtNetTotal;
	private Label lbCurrency;

	private Panel pnlVatCard;
	private Label lbTotalVatTitle;
	private TextBoxEx txtTotalVat;

	private Panel pnlDiscountCard;
	private Label lbTotalDiscountTitle;
	private TextBoxEx txtTotalDiscount;

	private Panel pnlSubtotalCard;
	private Label lbSubtotalTitle;
	private TextBoxEx txtTotalBeforeTax;

	private ComboBoxEx combPaymentMethod;
	private LabelEx lbPaymentMethod;

	// Footer Actions
	private Button btnNewReceipt;
	private Button btnSaveAndPrint;
	private Button btnClose;

	public frmReceipt(OrderType orderType = OrderType.Sale)
	{
		_receiptType = orderType;
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		UITheme.ApplyTheme(this);
		UITheme.ApplyGridTheme(dgvReceiptItems);
		ApplyCustomStyles();
		LoadReceiptTypes();
		LoadPosDevices();
		LoadBuyerTypes();
		LoadPaymentMethods();
		ApplyReceiptTypeUI();
		NewReceipt();
	}

	private void ApplyCustomStyles()
	{
		// Buttons
		UITheme.ApplySuccessButton(btnSaveAndPrint);
		UITheme.ApplyPrimaryButton(btnNewReceipt);
		UITheme.ApplyDangerButton(btnClose);
		UITheme.ApplyPrimaryButton(btnAddItem);
		UITheme.ApplyButton(btnSearchOriginalReceipt, UITheme.Primary, Color.White, UITheme.PrimaryHover);
		UITheme.ApplyButton(btnSearchProduct, Color.FromArgb(241, 245, 249), UITheme.TextPrimary, Color.FromArgb(226, 232, 240));

		// DataGridView Columns Alignment & Styles
		if (dgvReceiptItems.Columns.Count >= 8)
		{
			dgvReceiptItems.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // م
			dgvReceiptItems.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // كود
			dgvReceiptItems.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // الاسم
			dgvReceiptItems.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // الكمية
			dgvReceiptItems.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // السعر
			dgvReceiptItems.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // الخصم
			dgvReceiptItems.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // الضريبة
			dgvReceiptItems.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // الإجمالي

			// Highlight editable columns slightly
			dgvReceiptItems.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232); // Light yellow
			dgvReceiptItems.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);
			dgvReceiptItems.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);

			dgvReceiptItems.Columns[7].DefaultCellStyle.Font = UITheme.BoldFont;
			dgvReceiptItems.Columns[7].DefaultCellStyle.ForeColor = Color.FromArgb(5, 150, 105);
		}
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
			lbBranchInfo.Text = $"الفرع: {pos.Branch?.Name ?? "الرئيسي"}  |  ماكينة: {pos.PosName}  |  السيريال: {pos.DeviceSerialNumber ?? "---"}";
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
			pnlHeader.BackColor = Color.FromArgb(194, 65, 12); // Deep Orange
			lbHeaderTitle.Text = "منظومة الإيصال الإلكتروني - مرتجع إيصال بيع (Return POS)";
			lbHeaderBadge.Text = "🔄 وضع المرتجع";
			lbHeaderBadge.BackColor = Color.FromArgb(234, 88, 12);
			this.grpPosInfo.Text = "بيانات المرتجع ونقطة البيع";
			this.lbOriginalReceipt.Visible = true;
			this.txtOriginalReceipt.Visible = true;
			this.btnSearchOriginalReceipt.Visible = true;
			this.btnSaveAndPrint.Text = "حفظ وطباعة المرتجع [F5]";
			UITheme.ApplyWarningButton(btnSaveAndPrint);
		}
		else
		{
			this.Text = "إصدار إيصال إلكتروني - مبيعات الكاشير (E-Receipt POS)";
			pnlHeader.BackColor = UITheme.HeaderBg;
			lbHeaderTitle.Text = "منظومة الإيصال الإلكتروني - نقطة البيع ومبيعات الكاشير";
			lbHeaderBadge.Text = "⚡ إيصال جديد";
			lbHeaderBadge.BackColor = Color.FromArgb(16, 185, 129);
			this.grpPosInfo.Text = "بيانات نقطة البيع والإيصال";
			this.lbOriginalReceipt.Visible = false;
			this.txtOriginalReceipt.Visible = false;
			this.btnSearchOriginalReceipt.Visible = false;
			this.btnSaveAndPrint.Text = "حفظ وطباعة الإيصال [F5]";
			UITheme.ApplySuccessButton(btnSaveAndPrint);
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

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.F5)
		{
			btnSaveAndPrint.PerformClick();
			return true;
		}
		if (keyData == Keys.F2)
		{
			btnNewReceipt.PerformClick();
			return true;
		}
		if (keyData == Keys.Escape)
		{
			this.Close();
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
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
		this.pnlHeader = new System.Windows.Forms.Panel();
		this.lbBranchInfo = new System.Windows.Forms.Label();
		this.lbHeaderBadge = new System.Windows.Forms.Label();
		this.lbHeaderTitle = new System.Windows.Forms.Label();

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

		this.pnlTotals = new System.Windows.Forms.Panel();
		this.pnlNetTotalCard = new System.Windows.Forms.Panel();
		this.lbCurrency = new System.Windows.Forms.Label();
		this.txtNetTotal = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbNetTotalTitle = new System.Windows.Forms.Label();

		this.pnlVatCard = new System.Windows.Forms.Panel();
		this.txtTotalVat = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTotalVatTitle = new System.Windows.Forms.Label();

		this.pnlDiscountCard = new System.Windows.Forms.Panel();
		this.txtTotalDiscount = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbTotalDiscountTitle = new System.Windows.Forms.Label();

		this.pnlSubtotalCard = new System.Windows.Forms.Panel();
		this.txtTotalBeforeTax = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbSubtotalTitle = new System.Windows.Forms.Label();

		this.lbPaymentMethod = new E_Invoice.Desktop.Controls.LabelEx();
		this.combPaymentMethod = new E_Invoice.Desktop.Controls.ComboBoxEx();

		this.btnNewReceipt = new System.Windows.Forms.Button();
		this.btnSaveAndPrint = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();

		this.pnlHeader.SuspendLayout();
		this.grpPosInfo.SuspendLayout();
		this.grpBuyer.SuspendLayout();
		this.grpItems.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceiptItems)).BeginInit();
		this.pnlTotals.SuspendLayout();
		this.pnlNetTotalCard.SuspendLayout();
		this.pnlVatCard.SuspendLayout();
		this.pnlDiscountCard.SuspendLayout();
		this.pnlSubtotalCard.SuspendLayout();
		this.SuspendLayout();

		// pnlHeader
		this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.pnlHeader.Controls.Add(this.lbBranchInfo);
		this.pnlHeader.Controls.Add(this.lbHeaderBadge);
		this.pnlHeader.Controls.Add(this.lbHeaderTitle);
		this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlHeader.Location = new System.Drawing.Point(0, 0);
		this.pnlHeader.Name = "pnlHeader";
		this.pnlHeader.Size = new System.Drawing.Size(1004, 52);
		this.pnlHeader.TabIndex = 0;

		// lbHeaderTitle
		this.lbHeaderTitle.AutoSize = true;
		this.lbHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
		this.lbHeaderTitle.ForeColor = System.Drawing.Color.White;
		this.lbHeaderTitle.Location = new System.Drawing.Point(540, 14);
		this.lbHeaderTitle.Name = "lbHeaderTitle";
		this.lbHeaderTitle.Size = new System.Drawing.Size(445, 28);
		this.lbHeaderTitle.Text = "منظومة الإيصال الإلكتروني - نقطة البيع ومبيعات الكاشير";

		// lbHeaderBadge
		this.lbHeaderBadge.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
		this.lbHeaderBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.lbHeaderBadge.ForeColor = System.Drawing.Color.White;
		this.lbHeaderBadge.Location = new System.Drawing.Point(400, 13);
		this.lbHeaderBadge.Name = "lbHeaderBadge";
		this.lbHeaderBadge.Size = new System.Drawing.Size(125, 28);
		this.lbHeaderBadge.TabIndex = 1;
		this.lbHeaderBadge.Text = "⚡ إيصال جديد";
		this.lbHeaderBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// lbBranchInfo
		this.lbBranchInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBranchInfo.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
		this.lbBranchInfo.Location = new System.Drawing.Point(12, 14);
		this.lbBranchInfo.Name = "lbBranchInfo";
		this.lbBranchInfo.Size = new System.Drawing.Size(370, 25);
		this.lbBranchInfo.TabIndex = 2;
		this.lbBranchInfo.Text = "الفرع: الرئيسي | ماكينة POS-1";
		this.lbBranchInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

		// grpPosInfo
		this.grpPosInfo.BackColor = System.Drawing.Color.White;
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
		this.grpPosInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpPosInfo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpPosInfo.Location = new System.Drawing.Point(12, 60);
		this.grpPosInfo.Name = "grpPosInfo";
		this.grpPosInfo.Size = new System.Drawing.Size(980, 95);
		this.grpPosInfo.TabIndex = 1;
		this.grpPosInfo.TabStop = false;
		this.grpPosInfo.Text = "بيانات نقطة البيع والإيصال";

		// lbReceiptType
		this.lbReceiptType.AutoSize = true;
		this.lbReceiptType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbReceiptType.Location = new System.Drawing.Point(895, 26);
		this.lbReceiptType.Name = "lbReceiptType";
		this.lbReceiptType.Size = new System.Drawing.Size(74, 21);
		this.lbReceiptType.Text = "نوع الإيصال:";

		// combReceiptType
		this.combReceiptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combReceiptType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.combReceiptType.FormattingEnabled = true;
		this.combReceiptType.Location = new System.Drawing.Point(745, 23);
		this.combReceiptType.Name = "combReceiptType";
		this.combReceiptType.Size = new System.Drawing.Size(145, 29);
		this.combReceiptType.SelectedIndexChanged += new System.EventHandler(this.combReceiptType_SelectedIndexChanged);

		// lbPosDevice
		this.lbPosDevice.AutoSize = true;
		this.lbPosDevice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbPosDevice.Location = new System.Drawing.Point(665, 26);
		this.lbPosDevice.Name = "lbPosDevice";
		this.lbPosDevice.Size = new System.Drawing.Size(74, 21);
		this.lbPosDevice.Text = "نقطة البيع:";

		// combPosDevice
		this.combPosDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combPosDevice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.combPosDevice.FormattingEnabled = true;
		this.combPosDevice.Location = new System.Drawing.Point(490, 23);
		this.combPosDevice.Name = "combPosDevice";
		this.combPosDevice.Size = new System.Drawing.Size(170, 29);
		this.combPosDevice.SelectedIndexChanged += new System.EventHandler(this.combPosDevice_SelectedIndexChanged);

		// lbReceiptNumber
		this.lbReceiptNumber.AutoSize = true;
		this.lbReceiptNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbReceiptNumber.Location = new System.Drawing.Point(400, 26);
		this.lbReceiptNumber.Name = "lbReceiptNumber";
		this.lbReceiptNumber.Size = new System.Drawing.Size(81, 21);
		this.lbReceiptNumber.Text = "رقم الإيصال:";

		// txtReceiptNumber
		this.txtReceiptNumber.BackColor = System.Drawing.Color.FromArgb(254, 252, 232);
		this.txtReceiptNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
		this.txtReceiptNumber.ForeColor = System.Drawing.Color.FromArgb(133, 77, 14);
		this.txtReceiptNumber.Location = new System.Drawing.Point(265, 23);
		this.txtReceiptNumber.Name = "txtReceiptNumber";
		this.txtReceiptNumber.ReadOnly = true;
		this.txtReceiptNumber.Size = new System.Drawing.Size(130, 30);

		// lbReceiptDate
		this.lbReceiptDate.AutoSize = true;
		this.lbReceiptDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbReceiptDate.Location = new System.Drawing.Point(205, 26);
		this.lbReceiptDate.Name = "lbReceiptDate";
		this.lbReceiptDate.Size = new System.Drawing.Size(51, 21);
		this.lbReceiptDate.Text = "التاريخ:";

		// dtReceiptDate
		this.dtReceiptDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.dtReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtReceiptDate.Location = new System.Drawing.Point(15, 23);
		this.dtReceiptDate.Name = "dtReceiptDate";
		this.dtReceiptDate.Size = new System.Drawing.Size(185, 29);

		// lbOriginalReceipt
		this.lbOriginalReceipt.AutoSize = true;
		this.lbOriginalReceipt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbOriginalReceipt.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
		this.lbOriginalReceipt.Location = new System.Drawing.Point(860, 62);
		this.lbOriginalReceipt.Name = "lbOriginalReceipt";
		this.lbOriginalReceipt.Size = new System.Drawing.Size(109, 21);
		this.lbOriginalReceipt.Text = "الإيصال الأصلي:";
		this.lbOriginalReceipt.Visible = false;

		// txtOriginalReceipt
		this.txtOriginalReceipt.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
		this.txtOriginalReceipt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtOriginalReceipt.ForeColor = System.Drawing.Color.FromArgb(153, 27, 27);
		this.txtOriginalReceipt.Location = new System.Drawing.Point(620, 58);
		this.txtOriginalReceipt.Name = "txtOriginalReceipt";
		this.txtOriginalReceipt.Size = new System.Drawing.Size(235, 29);
		this.txtOriginalReceipt.Visible = false;

		// btnSearchOriginalReceipt
		this.btnSearchOriginalReceipt.Location = new System.Drawing.Point(375, 56);
		this.btnSearchOriginalReceipt.Name = "btnSearchOriginalReceipt";
		this.btnSearchOriginalReceipt.Size = new System.Drawing.Size(235, 32);
		this.btnSearchOriginalReceipt.Text = "🔍 بحث واسترجاع إيصال سابق";
		this.btnSearchOriginalReceipt.UseVisualStyleBackColor = false;
		this.btnSearchOriginalReceipt.Visible = false;
		this.btnSearchOriginalReceipt.Click += new System.EventHandler(this.btnSearchOriginalReceipt_Click);

		// grpBuyer
		this.grpBuyer.BackColor = System.Drawing.Color.White;
		this.grpBuyer.Controls.Add(this.txtBuyerMobile);
		this.grpBuyer.Controls.Add(this.lbBuyerMobile);
		this.grpBuyer.Controls.Add(this.txtBuyerName);
		this.grpBuyer.Controls.Add(this.lbBuyerName);
		this.grpBuyer.Controls.Add(this.txtBuyerId);
		this.grpBuyer.Controls.Add(this.lbBuyerId);
		this.grpBuyer.Controls.Add(this.combBuyerType);
		this.grpBuyer.Controls.Add(this.lbBuyerType);
		this.grpBuyer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpBuyer.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpBuyer.Location = new System.Drawing.Point(12, 160);
		this.grpBuyer.Name = "grpBuyer";
		this.grpBuyer.Size = new System.Drawing.Size(980, 64);
		this.grpBuyer.TabIndex = 2;
		this.grpBuyer.TabStop = false;
		this.grpBuyer.Text = "بيانات المشتري (المستهلك)";

		// lbBuyerType
		this.lbBuyerType.AutoSize = true;
		this.lbBuyerType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBuyerType.Location = new System.Drawing.Point(890, 26);
		this.lbBuyerType.Name = "lbBuyerType";
		this.lbBuyerType.Size = new System.Drawing.Size(78, 21);
		this.lbBuyerType.Text = "نوع العميل:";

		// combBuyerType
		this.combBuyerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combBuyerType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.combBuyerType.FormattingEnabled = true;
		this.combBuyerType.Location = new System.Drawing.Point(710, 23);
		this.combBuyerType.Name = "combBuyerType";
		this.combBuyerType.Size = new System.Drawing.Size(175, 29);

		// lbBuyerId
		this.lbBuyerId.AutoSize = true;
		this.lbBuyerId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBuyerId.Location = new System.Drawing.Point(615, 26);
		this.lbBuyerId.Name = "lbBuyerId";
		this.lbBuyerId.Size = new System.Drawing.Size(91, 21);
		this.lbBuyerId.Text = "الرقم القومي:";

		// txtBuyerId
		this.txtBuyerId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtBuyerId.Location = new System.Drawing.Point(475, 23);
		this.txtBuyerId.Name = "txtBuyerId";
		this.txtBuyerId.Size = new System.Drawing.Size(135, 29);

		// lbBuyerName
		this.lbBuyerName.AutoSize = true;
		this.lbBuyerName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBuyerName.Location = new System.Drawing.Point(385, 26);
		this.lbBuyerName.Name = "lbBuyerName";
		this.lbBuyerName.Size = new System.Drawing.Size(84, 21);
		this.lbBuyerName.Text = "اسم العميل:";

		// txtBuyerName
		this.txtBuyerName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtBuyerName.Location = new System.Drawing.Point(230, 23);
		this.txtBuyerName.Name = "txtBuyerName";
		this.txtBuyerName.Size = new System.Drawing.Size(150, 29);

		// lbBuyerMobile
		this.lbBuyerMobile.AutoSize = true;
		this.lbBuyerMobile.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBuyerMobile.Location = new System.Drawing.Point(165, 26);
		this.lbBuyerMobile.Name = "lbBuyerMobile";
		this.lbBuyerMobile.Size = new System.Drawing.Size(59, 21);
		this.lbBuyerMobile.Text = "الموبايل:";

		// txtBuyerMobile
		this.txtBuyerMobile.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.txtBuyerMobile.Location = new System.Drawing.Point(15, 23);
		this.txtBuyerMobile.Name = "txtBuyerMobile";
		this.txtBuyerMobile.Size = new System.Drawing.Size(145, 29);

		// grpItems
		this.grpItems.BackColor = System.Drawing.Color.White;
		this.grpItems.Controls.Add(this.dgvReceiptItems);
		this.grpItems.Controls.Add(this.btnAddItem);
		this.grpItems.Controls.Add(this.txtQty);
		this.grpItems.Controls.Add(this.lbQty);
		this.grpItems.Controls.Add(this.btnSearchProduct);
		this.grpItems.Controls.Add(this.txtBarcode);
		this.grpItems.Controls.Add(this.lbBarcode);
		this.grpItems.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.grpItems.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.grpItems.Location = new System.Drawing.Point(12, 228);
		this.grpItems.Name = "grpItems";
		this.grpItems.Size = new System.Drawing.Size(980, 275);
		this.grpItems.TabIndex = 3;
		this.grpItems.TabStop = false;
		this.grpItems.Text = "أصناف الإيصال (يمكنك تعديل الكمية والسعر والخصم مباشرة داخل الجدول)";

		// lbBarcode
		this.lbBarcode.AutoSize = true;
		this.lbBarcode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbBarcode.Location = new System.Drawing.Point(865, 26);
		this.lbBarcode.Name = "lbBarcode";
		this.lbBarcode.Size = new System.Drawing.Size(104, 21);
		this.lbBarcode.Text = "الباركود / الكود:";

		// txtBarcode
		this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10.5F);
		this.txtBarcode.Location = new System.Drawing.Point(540, 22);
		this.txtBarcode.Name = "txtBarcode";
		this.txtBarcode.Size = new System.Drawing.Size(320, 31);
		this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);

		// btnSearchProduct
		this.btnSearchProduct.Image = E_Invoice.Desktop.Properties.Resources.SearchSmall;
		this.btnSearchProduct.Location = new System.Drawing.Point(495, 21);
		this.btnSearchProduct.Name = "btnSearchProduct";
		this.btnSearchProduct.Size = new System.Drawing.Size(38, 32);
		this.btnSearchProduct.UseVisualStyleBackColor = true;
		this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);

		// lbQty
		this.lbQty.AutoSize = true;
		this.lbQty.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.lbQty.Location = new System.Drawing.Point(430, 26);
		this.lbQty.Name = "lbQty";
		this.lbQty.Size = new System.Drawing.Size(55, 21);
		this.lbQty.Text = "الكمية:";

		// txtQty
		this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
		this.txtQty.IsNumber = true;
		this.txtQty.Location = new System.Drawing.Point(340, 22);
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(85, 30);
		this.txtQty.Text = "1";
		this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

		// btnAddItem
		this.btnAddItem.Location = new System.Drawing.Point(195, 20);
		this.btnAddItem.Name = "btnAddItem";
		this.btnAddItem.Size = new System.Drawing.Size(135, 34);
		this.btnAddItem.Text = "إضافة صنف +";
		this.btnAddItem.UseVisualStyleBackColor = false;
		this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

		// dgvReceiptItems
		this.dgvReceiptItems.AllowUserToAddRows = false;
		this.dgvReceiptItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvReceiptItems.BackgroundColor = System.Drawing.Color.White;
		this.dgvReceiptItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvReceiptItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "م", Width = 45, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "كود الصنف", Width = 95, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "اسم الصنف", Width = 260, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الكمية ✏️", Width = 80, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "السعر ✏️", Width = 95, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الخصم ✏️", Width = 85, ReadOnly = false },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الضريبة", Width = 90, ReadOnly = true },
			new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "الإجمالي", Width = 115, ReadOnly = true }
		});
		this.dgvReceiptItems.Location = new System.Drawing.Point(15, 60);
		this.dgvReceiptItems.Name = "dgvReceiptItems";
		this.dgvReceiptItems.Size = new System.Drawing.Size(950, 205);
		this.dgvReceiptItems.TabIndex = 6;
		this.dgvReceiptItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvReceiptItems_CellValidating);
		this.dgvReceiptItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceiptItems_CellValueChanged);
		this.dgvReceiptItems.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvReceiptItems_UserDeletingRow);
		this.dgvReceiptItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvReceiptItems_DataError);

		// pnlTotals (Summary Ribbon)
		this.pnlTotals.BackColor = System.Drawing.Color.White;
		this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlTotals.Controls.Add(this.combPaymentMethod);
		this.pnlTotals.Controls.Add(this.lbPaymentMethod);
		this.pnlTotals.Controls.Add(this.pnlNetTotalCard);
		this.pnlTotals.Controls.Add(this.pnlVatCard);
		this.pnlTotals.Controls.Add(this.pnlDiscountCard);
		this.pnlTotals.Controls.Add(this.pnlSubtotalCard);
		this.pnlTotals.Location = new System.Drawing.Point(12, 508);
		this.pnlTotals.Name = "pnlTotals";
		this.pnlTotals.Size = new System.Drawing.Size(980, 78);
		this.pnlTotals.TabIndex = 4;

		// pnlSubtotalCard
		this.pnlSubtotalCard.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
		this.pnlSubtotalCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlSubtotalCard.Controls.Add(this.txtTotalBeforeTax);
		this.pnlSubtotalCard.Controls.Add(this.lbSubtotalTitle);
		this.pnlSubtotalCard.Location = new System.Drawing.Point(825, 8);
		this.pnlSubtotalCard.Name = "pnlSubtotalCard";
		this.pnlSubtotalCard.Size = new System.Drawing.Size(145, 60);
		this.pnlSubtotalCard.TabIndex = 0;

		// lbSubtotalTitle
		this.lbSubtotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbSubtotalTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
		this.lbSubtotalTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
		this.lbSubtotalTitle.Location = new System.Drawing.Point(0, 0);
		this.lbSubtotalTitle.Name = "lbSubtotalTitle";
		this.lbSubtotalTitle.Size = new System.Drawing.Size(143, 20);
		this.lbSubtotalTitle.TabIndex = 0;
		this.lbSubtotalTitle.Text = "قبل الضريبة";
		this.lbSubtotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// txtTotalBeforeTax
		this.txtTotalBeforeTax.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
		this.txtTotalBeforeTax.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtTotalBeforeTax.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.txtTotalBeforeTax.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
		this.txtTotalBeforeTax.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
		this.txtTotalBeforeTax.Location = new System.Drawing.Point(0, 32);
		this.txtTotalBeforeTax.Name = "txtTotalBeforeTax";
		this.txtTotalBeforeTax.ReadOnly = true;
		this.txtTotalBeforeTax.Size = new System.Drawing.Size(143, 26);
		this.txtTotalBeforeTax.Text = "0.00";
		this.txtTotalBeforeTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

		// pnlDiscountCard
		this.pnlDiscountCard.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
		this.pnlDiscountCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlDiscountCard.Controls.Add(this.txtTotalDiscount);
		this.pnlDiscountCard.Controls.Add(this.lbTotalDiscountTitle);
		this.pnlDiscountCard.Location = new System.Drawing.Point(690, 8);
		this.pnlDiscountCard.Name = "pnlDiscountCard";
		this.pnlDiscountCard.Size = new System.Drawing.Size(125, 60);
		this.pnlDiscountCard.TabIndex = 1;

		// lbTotalDiscountTitle
		this.lbTotalDiscountTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbTotalDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
		this.lbTotalDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
		this.lbTotalDiscountTitle.Location = new System.Drawing.Point(0, 0);
		this.lbTotalDiscountTitle.Name = "lbTotalDiscountTitle";
		this.lbTotalDiscountTitle.Size = new System.Drawing.Size(123, 20);
		this.lbTotalDiscountTitle.TabIndex = 0;
		this.lbTotalDiscountTitle.Text = "إجمالي الخصم";
		this.lbTotalDiscountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// txtTotalDiscount
		this.txtTotalDiscount.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
		this.txtTotalDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtTotalDiscount.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.txtTotalDiscount.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
		this.txtTotalDiscount.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
		this.txtTotalDiscount.Location = new System.Drawing.Point(0, 32);
		this.txtTotalDiscount.Name = "txtTotalDiscount";
		this.txtTotalDiscount.ReadOnly = true;
		this.txtTotalDiscount.Size = new System.Drawing.Size(123, 26);
		this.txtTotalDiscount.Text = "0.00";
		this.txtTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

		// pnlVatCard
		this.pnlVatCard.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
		this.pnlVatCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlVatCard.Controls.Add(this.txtTotalVat);
		this.pnlVatCard.Controls.Add(this.lbTotalVatTitle);
		this.pnlVatCard.Location = new System.Drawing.Point(545, 8);
		this.pnlVatCard.Name = "pnlVatCard";
		this.pnlVatCard.Size = new System.Drawing.Size(135, 60);
		this.pnlVatCard.TabIndex = 2;

		// lbTotalVatTitle
		this.lbTotalVatTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbTotalVatTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
		this.lbTotalVatTitle.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
		this.lbTotalVatTitle.Location = new System.Drawing.Point(0, 0);
		this.lbTotalVatTitle.Name = "lbTotalVatTitle";
		this.lbTotalVatTitle.Size = new System.Drawing.Size(133, 20);
		this.lbTotalVatTitle.TabIndex = 0;
		this.lbTotalVatTitle.Text = "ضريبة القيمة المضافة 14%";
		this.lbTotalVatTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// txtTotalVat
		this.txtTotalVat.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
		this.txtTotalVat.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtTotalVat.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.txtTotalVat.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
		this.txtTotalVat.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
		this.txtTotalVat.Location = new System.Drawing.Point(0, 32);
		this.txtTotalVat.Name = "txtTotalVat";
		this.txtTotalVat.ReadOnly = true;
		this.txtTotalVat.Size = new System.Drawing.Size(133, 26);
		this.txtTotalVat.Text = "0.00";
		this.txtTotalVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

		// pnlNetTotalCard (Grand Total Emerald Box)
		this.pnlNetTotalCard.BackColor = System.Drawing.Color.FromArgb(236, 253, 245);
		this.pnlNetTotalCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlNetTotalCard.Controls.Add(this.lbCurrency);
		this.pnlNetTotalCard.Controls.Add(this.txtNetTotal);
		this.pnlNetTotalCard.Controls.Add(this.lbNetTotalTitle);
		this.pnlNetTotalCard.Location = new System.Drawing.Point(260, 8);
		this.pnlNetTotalCard.Name = "pnlNetTotalCard";
		this.pnlNetTotalCard.Size = new System.Drawing.Size(275, 60);
		this.pnlNetTotalCard.TabIndex = 3;

		// lbNetTotalTitle
		this.lbNetTotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbNetTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
		this.lbNetTotalTitle.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
		this.lbNetTotalTitle.Location = new System.Drawing.Point(0, 0);
		this.lbNetTotalTitle.Name = "lbNetTotalTitle";
		this.lbNetTotalTitle.Size = new System.Drawing.Size(273, 20);
		this.lbNetTotalTitle.TabIndex = 0;
		this.lbNetTotalTitle.Text = "صافي الإيصال النهائي";
		this.lbNetTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// txtNetTotal
		this.txtNetTotal.BackColor = System.Drawing.Color.FromArgb(236, 253, 245);
		this.txtNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtNetTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
		this.txtNetTotal.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87);
		this.txtNetTotal.Location = new System.Drawing.Point(40, 24);
		this.txtNetTotal.Name = "txtNetTotal";
		this.txtNetTotal.ReadOnly = true;
		this.txtNetTotal.Size = new System.Drawing.Size(225, 36);
		this.txtNetTotal.Text = "0.00";
		this.txtNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

		// lbCurrency
		this.lbCurrency.AutoSize = true;
		this.lbCurrency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
		this.lbCurrency.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
		this.lbCurrency.Location = new System.Drawing.Point(6, 29);
		this.lbCurrency.Name = "lbCurrency";
		this.lbCurrency.Size = new System.Drawing.Size(38, 23);
		this.lbCurrency.Text = "ج.م";

		// lbPaymentMethod
		this.lbPaymentMethod.AutoSize = true;
		this.lbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
		this.lbPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
		this.lbPaymentMethod.Location = new System.Drawing.Point(165, 27);
		this.lbPaymentMethod.Name = "lbPaymentMethod";
		this.lbPaymentMethod.Size = new System.Drawing.Size(89, 21);
		this.lbPaymentMethod.Text = "طريقة السداد:";

		// combPaymentMethod
		this.combPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.combPaymentMethod.FormattingEnabled = true;
		this.combPaymentMethod.Location = new System.Drawing.Point(12, 23);
		this.combPaymentMethod.Name = "combPaymentMethod";
		this.combPaymentMethod.Size = new System.Drawing.Size(150, 29);

		// btnNewReceipt
		this.btnNewReceipt.Location = new System.Drawing.Point(710, 594);
		this.btnNewReceipt.Name = "btnNewReceipt";
		this.btnNewReceipt.Size = new System.Drawing.Size(145, 44);
		this.btnNewReceipt.TabIndex = 5;
		this.btnNewReceipt.Text = "إيصال جديد [F2]";
		this.btnNewReceipt.UseVisualStyleBackColor = false;
		this.btnNewReceipt.Click += new System.EventHandler(this.btnNewReceipt_Click);

		// btnSaveAndPrint
		this.btnSaveAndPrint.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
		this.btnSaveAndPrint.Location = new System.Drawing.Point(440, 594);
		this.btnSaveAndPrint.Name = "btnSaveAndPrint";
		this.btnSaveAndPrint.Size = new System.Drawing.Size(255, 44);
		this.btnSaveAndPrint.TabIndex = 6;
		this.btnSaveAndPrint.Text = "حفظ وطباعة الإيصال [F5]";
		this.btnSaveAndPrint.UseVisualStyleBackColor = false;
		this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);

		// btnClose
		this.btnClose.Location = new System.Drawing.Point(295, 594);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(130, 44);
		this.btnClose.TabIndex = 7;
		this.btnClose.Text = "إغلاق [Esc]";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

		// frmReceipt
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
		this.ClientSize = new System.Drawing.Size(1004, 648);
		this.Controls.Add(this.btnClose);
		this.Controls.Add(this.btnSaveAndPrint);
		this.Controls.Add(this.btnNewReceipt);
		this.Controls.Add(this.pnlTotals);
		this.Controls.Add(this.grpItems);
		this.Controls.Add(this.grpBuyer);
		this.Controls.Add(this.grpPosInfo);
		this.Controls.Add(this.pnlHeader);
		this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.KeyPreview = true;
		this.MaximizeBox = false;
		this.Name = "frmReceipt";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "إصدار إيصال إلكتروني - مبيعات الكاشير (E-Receipt POS)";
		this.pnlHeader.ResumeLayout(false);
		this.pnlHeader.PerformLayout();
		this.grpPosInfo.ResumeLayout(false);
		this.grpPosInfo.PerformLayout();
		this.grpBuyer.ResumeLayout(false);
		this.grpBuyer.PerformLayout();
		this.grpItems.ResumeLayout(false);
		this.grpItems.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.dgvReceiptItems)).EndInit();
		this.pnlTotals.ResumeLayout(false);
		this.pnlTotals.PerformLayout();
		this.pnlNetTotalCard.ResumeLayout(false);
		this.pnlNetTotalCard.PerformLayout();
		this.pnlVatCard.ResumeLayout(false);
		this.pnlVatCard.PerformLayout();
		this.pnlDiscountCard.ResumeLayout(false);
		this.pnlDiscountCard.PerformLayout();
		this.pnlSubtotalCard.ResumeLayout(false);
		this.pnlSubtotalCard.PerformLayout();
		this.ResumeLayout(false);
	}
}
