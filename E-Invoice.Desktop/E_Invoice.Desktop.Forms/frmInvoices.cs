using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
using E_Invoice.Domain.Services;
using E_Invoice.Domian.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace E_Invoice.Desktop.Forms;

public class frmInvoices : A
{
	public OrderType _orderType;

	public OrderEinvSend _OrderEinvSend;

	public IEnumerable<OrdersVM> orders;

	public bool _EinvMoode;

	private IContainer components = null;

	private ToolStrip toolStrip1;

	private ToolStripSplitButton toolStripSplitButton1;

	private DataGridView DGVItems;

	private BindingSource ordersVMBindingSource;

	private ToolStripButton toolStripButton1;

	private DataGridViewCheckBoxColumn Selected;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn Barcode;

	private DataGridViewTextBoxColumn customerDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;

	private DataGridViewCheckBoxColumn sentDataGridViewCheckBoxColumn;

	private DataGridViewTextBoxColumn uUIDDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn Print;

	private TextBoxEx txtName;

	private LabelEx labelEx1;

	private LabelEx labelEx3;

	private TextBoxEx txtCode;

	private DateTimePickerEx dtFrom;

	private DateTimePickerEx dtTo;

	private Button button1;

	private ToolStripButton toolStripButton2;

	private ToolStripButton toolStripButton4;

	public frmInvoices(OrderType invType, OrderEinvSend orderEinvSend = OrderEinvSend.NotSent, bool EinvMoode = false)
	{
		InitializeComponent();
		_orderType = invType;
		_OrderEinvSend = orderEinvSend;
		_EinvMoode = EinvMoode;
		GetData();
	}

	public void GetData()
	{
		orders = (from s in _unitOfWork.Orders.GetAllBy((Order x) => (int)x.OrderType == (int)_orderType && (int)x.sent == (int)_OrderEinvSend)
			join acc in _unitOfWork.Accounts.GetAll() on s.AccountId equals acc.Id
			select new OrdersVM
			{
				Id = s.Id.ToLong(),
				Barcode = s.OrderBarcode,
				Customer = acc.Name,
				Date = s.Date.ToShortDateString(),
				UUID = s.uuid,
				Sent = s.sent,
				Total = s.NetInvoice
			}).ToList();
		DGVItems.DataSource = orders;
	}

	private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		string text = DGVItems.Rows[e.RowIndex].Index.ToString();
		string text2 = DGVItems.Rows[e.RowIndex].Cells[1].Value.ToString();
		frmInvoice frmInvoice2 = new frmInvoice(text2.ToInt());
		frmInvoice2.ShowDialog();
	}

	private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
	{
		int index = DGVItems.SelectedRows[0].Index;
		int id = DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
		send(id);
		GetData();
	}

	public void send(int id)
	{
		Order order = _unitOfWork.Orders.GetTBy((Order x) => x.Id == id);
		if (order.sent == OrderEinvSend.Sent)
		{
			GetDocumentResponse document = _tax.GetDocument(order.uuid);
			if (!(document.status != "Valid"))
			{
				order.Status = document.status;
				_unitOfWork.Orders.Update(order);
				MessageBox.Show("تم ارسال الفاتورة من قبل");
				return;
			}
			MessageBox.Show("سيتم ااعادة ارسال الفاتورة مرة اخري");
		}
		List<OrderDetail> list = _unitOfWork.OrderDetails.GetAllBy((OrderDetail x) => x.OrderId == order.Id).ToList();
		List<invoiceLines> list2 = new List<invoiceLines>();
		List<documents> list3 = new List<documents>();
		documents documents2 = new documents();
		List<validationErrors> list4 = new List<validationErrors>();
		Branch tById = _unitOfWork.Branchs.GetTById(order.BranchId);
		Company company = _unitOfWork.Companies.GetAll().FirstOrDefault();
		issuer issuer2 = new issuer
		{
			type = "B",
			id = company.TaxReg,
			name = company.Name,
			address = new address
			{
				branchId = tById.Code,
				country = tById.Country,
				governate = tById.Governate,
				regionCity = tById.RegionCity,
				street = tById.Street,
				buildingNumber = tById.BuildingNumber,
				floor = " ",
				landmark = " ",
				postalCode = " ",
				room = " ",
				additionalInformation = " "
			}
		};
		Account account = _unitOfWork.Accounts.GetAllBy((Account x) => (int?)x.Id == order.AccountId).FirstOrDefault();
		receiver receiver2 = new receiver();
		receiver2 = (order.CustomerNakdy ? new receiver
		{
			id = order.CustomerId,
			type = "P",
			name = order.CustumerName,
			address = new address
			{
				country = "EG",
				governate = order.CustumerGovernate,
				regionCity = order.CustumerCity,
				street = order.CustumerStreet,
				buildingNumber = order.CustumerBuilding,
				floor = " ",
				landmark = " ",
				postalCode = " ",
				room = " ",
				additionalInformation = " "
			}
		} : new receiver
		{
			id = account.TaxReg,
			type = account.CanonicalType.ToString(),
			name = account.Name,
			address = new address
			{
				country = account.Country.ToString(),
				governate = account.Governate.ToString(),
				regionCity = account.RegionCity.ToString(),
				street = account.Street.ToString(),
				buildingNumber = account.BuildingNumber.ToString(),
				floor = " ",
				landmark = " ",
				postalCode = " ",
				room = " ",
				additionalInformation = " "
			}
		});
		List<invoiceLines> list5 = new List<invoiceLines>();
		if (list.Count() == 0)
		{
			MessageBox.Show("لا يوجد بنود في الفاتورة");
			return;
		}
		foreach (OrderDetail item2 in list)
		{
			unitValue unitValue2 = new unitValue
			{
				currencySold = "EGP",
				amountEGP = Convert.ToDecimal(item2.Price.ToString("N5"))
			};
			Product tById2 = _unitOfWork.Products.GetTById(item2.ProductId.Value);
			invoiceLines invoiceLines2 = new invoiceLines();
			invoiceLines2.description = item2.ProductDesc;
			invoiceLines2.itemType = (Info._setting.UseGpc ? "GS1" : tById2.itemType.ToString());
			invoiceLines2.itemCode = (Info._setting.UseGpc ? tById2.GPCCode.ToString() : tById2.itemCode);
			invoiceLines2.unitType = "EA";
			invoiceLines2.quantity = Convert.ToDecimal(((decimal)item2.Quantity).ToString("N5"));
			invoiceLines2.unitValue = unitValue2;
			invoiceLines2.salesTotal = Convert.ToDecimal((unitValue2.amountEGP * (decimal)item2.Quantity).ToString("N5"));
			invoiceLines2.discount = new discount
			{
				amount = Convert.ToDecimal((item2.Price * (order.DiscountRate / 100.0)).ToString("N5")),
				rate = Convert.ToDecimal(order.DiscountRate.ToString("N5"))
			};
			invoiceLines2.netTotal = Convert.ToDecimal((invoiceLines2.salesTotal - invoiceLines2.discount.amount).ToString("N5"));
			invoiceLines2.valueDifference = Convert.ToDecimal(0.ToString("N5"));
			invoiceLines2.totalTaxableFees = Convert.ToDecimal(0.ToString("N5"));
			string subType = "V009";
			if (order.TotalVat == 0.0)
			{
				subType = "V003";
			}
			invoiceLines2.taxableItems = new List<taxableItems>
			{
				new taxableItems
				{
					taxType = "T1",
					amount = Convert.ToDecimal((invoiceLines2.netTotal * (decimal)(order.TotalVat / 100.0)).ToString("N5")),
					subType = subType,
					rate = (decimal)order.TotalVat
				}
			};
			if (order.Tax4 > 0.0)
			{
				invoiceLines2.taxableItems.Add(new taxableItems
				{
					taxType = "T4",
					amount = Convert.ToDecimal((invoiceLines2.netTotal * (decimal)(order.Tax4 / 100.0)).ToString("N5")),
					subType = "W003",
					rate = (decimal)order.Tax4
				});
			}
			decimal num = Convert.ToDecimal(invoiceLines2.taxableItems.Where((taxableItems zz) => zz.taxType == "T1").Sum((taxableItems x) => x.amount));
			decimal num2 = Convert.ToDecimal(invoiceLines2.taxableItems.Where((taxableItems zz) => zz.taxType == "T4").Sum((taxableItems x) => x.amount));
			invoiceLines2.total = Convert.ToDecimal((invoiceLines2.netTotal + num - num2).ToString("N5"));
			invoiceLines2.itemsDiscount = Convert.ToDecimal(0.ToString("N5"));
			invoiceLines2.internalCode = tById2.Code;
			list5.Add(invoiceLines2);
		}
		documents2.invoiceLines = list5;
		documents2.internalID = order.OrderBarcode.ToString();
		documents2.issuer = issuer2;
		documents2.receiver = receiver2;
		documents2.documentType = ((order.OrderType == OrderType.Sale) ? "I" : "C");
		documents2.documentTypeVersion = (Info._setting.Signer ? "1.0" : "0.9");
		documents2.purchaseOrderReference = " ";
		documents2.purchaseOrderDescription = " ";
		documents2.salesOrderReference = " ";
		documents2.salesOrderDescription = " ";
		documents2.proformaInvoiceNumber = " ";
		documents2.payment = new payment
		{
			bankName = " ",
			bankAddress = "",
			bankAccountNo = " ",
			bankAccountIBAN = "",
			swiftCode = "",
			terms = " "
		};
		DateTime date = order.Date;
		documents2.dateTimeIssued = date.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
		documents2.taxpayerActivityCode = company.ActivityCode;
		documents2.totalSalesAmount = Convert.ToDecimal(list5.Sum((invoiceLines i) => i.salesTotal).ToString("N5"));
		documents2.extraDiscountAmount = Convert.ToDecimal(0.ToString("N5"));
		documents2.totalDiscountAmount = Convert.ToDecimal(documents2.invoiceLines.Sum((invoiceLines x) => x.discount.amount).ToString("N5"));
		documents2.netAmount = Convert.ToDecimal(list5.Sum((invoiceLines i) => i.netTotal).ToString("N5"));
		documents2.totalAmount = Convert.ToDecimal(list5.Sum((invoiceLines i) => i.total).ToString("N5"));
		documents2.totalItemsDiscountAmount = 0m;
		documents2.delivery = new delivery
		{
			approach = " ",
			packaging = " ",
			dateValidity = date.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
			exportPort = " ",
			countryOfOrigin = " ",
			grossWeight = 0m,
			netWeight = 0m
		};
		taxTotals item = new taxTotals
		{
			taxType = "T1",
			amount = list5.Sum((invoiceLines x) => x.taxableItems.Where((taxableItems z) => z.taxType == "T1").Sum((taxableItems y) => y.amount))
		};
		List<taxTotals> list6 = new List<taxTotals>();
		list6.Add(item);
		if (order.Tax4 > 0.0)
		{
			list6.Add(new taxTotals
			{
				taxType = "T4",
				amount = list5.Sum((invoiceLines x) => x.taxableItems.Where((taxableItems z) => z.taxType == "T4").Sum((taxableItems y) => y.amount))
			});
		}
		documents2.taxTotals = list6;
		list3.Add(documents2);
		Tax tax = new Tax();
		Access access = tax.GetAccess();
		if (access.AccessError != null)
		{
			MessageBox.Show(access.AccessError);
			return;
		}
		JsonSerializerSettings settings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented,
			FloatFormatHandling = FloatFormatHandling.String,
			FloatParseHandling = FloatParseHandling.Decimal,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.None
		};
		string json = JsonConvert.SerializeObject(documents2, settings);
		TokenSigner tokenSigner = new TokenSigner();
		string text = tokenSigner.PutSignatures(json);
		JObject jObject = JsonConvert.DeserializeObject<JObject>(text, new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			Formatting = Formatting.Indented,
			FloatFormatHandling = FloatFormatHandling.String,
			FloatParseHandling = FloatParseHandling.Decimal,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.None
		});
		signeddoc signeddoc2 = JsonConvert.DeserializeObject<signeddoc>(text);
		if (signeddoc2.signatures[0].value == "No slots found")
		{
			MessageBox.Show("No slots found");
		}
		if (signeddoc2.signatures[0].value == "Certificate not found")
		{
			MessageBox.Show("Certificate not found");
		}
		if (signeddoc2.signatures[0].value == "no device detected")
		{
			MessageBox.Show("no device detected");
		}
		string obj = "{\"documents\":[" + text + "]}";
		RestClient restClient = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentsubmissions");
		if (Info._setting.ProdEnv)
		{
			restClient = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentsubmissions");
		}
		RestRequest restRequest = new RestRequest(Method.POST);
		restRequest.AddHeader("Authorization", "Bearer " + access.access_token);
		restRequest.AddHeader("Content-Type", "application/json");
		restRequest.AddJsonBody(obj, "application/json");
		IRestResponse restResponse = restClient.Execute(restRequest);
		if (restResponse.StatusCode == HttpStatusCode.BadRequest)
		{
			MessageBox.Show("BadRequest /n" + restResponse.Content);
			return;
		}
		if (restResponse.StatusCode.ToString() == "422")
		{
			MessageBox.Show("الفاتورة مرسلة من قبل /n" + restResponse.Content);
			return;
		}
		if (restResponse.StatusCode.ToString() == "400")
		{
			MessageBox.Show("تم تخطي الحد الاقصي /n" + restResponse.Content);
			return;
		}
		if (restResponse.StatusCode.ToString() == "403")
		{
			MessageBox.Show("خطاء في رقم التسجيل الضريبي /n" + restResponse.Content);
			return;
		}
		respo respo2 = JsonConvert.DeserializeObject<respo>(restResponse.Content);
		if (respo2.acceptedDocuments.Count > 0)
		{
			order.sent = OrderEinvSend.Sent;
			order.DateSent = DateTime.Now;
			order.Status = OrderEinvSatatus.Submitted.ToString();
			order.uuid = respo2.acceptedDocuments[0].uuid.ToString();
			try
			{
				_unitOfWork.Orders.Update(order);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			MessageBox.Show("تم الارسا بنجاح UUID = " + respo2.acceptedDocuments[0].uuid);
		}
		else
		{
			MessageBox.Show(JsonConvert.SerializeObject(respo2, settings));
		}
	}

	private void DGVItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
		if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
		{
			int index = DGVItems.SelectedRows[0].Index;
			string text = DGVItems.Rows[index].Cells[7].Value.ToString();
			PrintInvoiceResponse printInvoiceResponse = _tax.PrintInvoice(text);
			if (printInvoiceResponse.Success)
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Filter = "zip files (*.pdf)|*.pdf|All files (*.*)|*.*";
					saveFileDialog.FileName = text;
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.Title = "Save an Image File";
					saveFileDialog.DefaultExt = "pdf";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						File.WriteAllBytes(saveFileDialog.FileName, printInvoiceResponse.Pdf);
						Mess.Save();
					}
					return;
				}
			}
			Mess.Warning(printInvoiceResponse.Message);
		}
		else
		{
			int index2 = DGVItems.SelectedRows[0].Index;
			if (Convert.ToBoolean(DGVItems.Rows[index2].Cells["Selected"].Value))
			{
				DGVItems.Rows[index2].Cells["Selected"].Value = false;
			}
			else
			{
				DGVItems.Rows[index2].Cells["Selected"].Value = true;
			}
		}
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		int index = DGVItems.SelectedRows[0].Index;
		int orderId = DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
		frmCancleInvoice frmCancleInvoice2 = new frmCancleInvoice(orderId);
		frmCancleInvoice2.ShowDialog();
	}

	private void txtCode_TextChanged(object sender, EventArgs e)
	{
		if (txtCode.Text != string.Empty)
		{
			IEnumerable<OrdersVM> source = orders.Where((OrdersVM x) => x.Barcode == txtCode.Text);
			DGVItems.DataSource = source.ToList();
		}
		else
		{
			DGVItems.DataSource = orders.ToList();
		}
	}

	private void txtName_TextChanged(object sender, EventArgs e)
	{
		IEnumerable<OrdersVM> source = orders.Where((OrdersVM x) => x.Customer.Contains(txtName.Text));
		DGVItems.DataSource = source.ToList();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		IEnumerable<OrdersVM> source = orders.Where((OrdersVM x) => x.Date.ToDate() >= dtFrom.Value.ToDate() && x.Date.ToDate() <= dtTo.Value.ToDate());
		DGVItems.DataSource = source.ToList();
	}

	private void frmInvoices_Load(object sender, EventArgs e)
	{
		if (_OrderEinvSend == OrderEinvSend.Sent)
		{
			toolStripSplitButton1.Text = "اعادة ارسال الفاتورة المحددة";
			toolStripButton1.Visible = true;
		}
		if (_EinvMoode)
		{
			toolStrip1.Visible = true;
		}
	}

	private void toolStripButton2_Click(object sender, EventArgs e)
	{
		if (Mess.AskDelete() == DialogResult.Yes)
		{
			int index = DGVItems.SelectedRows[0].Index;
			int id = DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
			Order tById = _unitOfWork.Orders.GetTById(id);
			_unitOfWork.Orders.Delete(tById);
			GetData();
			Mess.Delete();
		}
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		int index = DGVItems.SelectedRows[0].Index;
		int id = DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
		frmDateEdit frmDateEdit2 = new frmDateEdit(id);
		if (frmDateEdit2.ShowDialog() == DialogResult.OK)
		{
			Mess.Save("تم تعديل الفاتورة بنجاح");
			_unitOfWork = new UnitOfWork();
			GetData();
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(E_Invoice.Desktop.Forms.frmInvoices));
		this.DGVItems = new System.Windows.Forms.DataGridView();
		this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.customerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sentDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.uUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Print = new System.Windows.Forms.DataGridViewButtonColumn();
		this.ordersVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.labelEx1 = new E_Invoice.Desktop.Controls.LabelEx();
		this.labelEx3 = new E_Invoice.Desktop.Controls.LabelEx();
		this.txtCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.dtFrom = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.dtTo = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.button1 = new System.Windows.Forms.Button();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ordersVMBindingSource).BeginInit();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.AllowUserToDeleteRows = false;
		this.DGVItems.AllowUserToResizeRows = false;
		this.DGVItems.AutoGenerateColumns = false;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Columns.AddRange(this.Selected, this.idDataGridViewTextBoxColumn, this.Barcode, this.customerDataGridViewTextBoxColumn, this.dateDataGridViewTextBoxColumn, this.totalDataGridViewTextBoxColumn, this.sentDataGridViewCheckBoxColumn, this.uUIDDataGridViewTextBoxColumn, this.Print);
		this.DGVItems.DataSource = this.ordersVMBindingSource;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.DGVItems.DefaultCellStyle = dataGridViewCellStyle2;
		this.DGVItems.Location = new System.Drawing.Point(0, 116);
		this.DGVItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(1472, 557);
		this.DGVItems.TabIndex = 2;
		this.DGVItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVItems_CellContentClick);
		this.DGVItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVItems_CellDoubleClick);
		this.Selected.FillWeight = 10f;
		this.Selected.HeaderText = "تحديد";
		this.Selected.MinimumWidth = 6;
		this.Selected.Name = "Selected";
		this.Selected.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
		this.idDataGridViewTextBoxColumn.FillWeight = 35f;
		this.idDataGridViewTextBoxColumn.HeaderText = "رقم الفاتورة";
		this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.Barcode.DataPropertyName = "Barcode";
		this.Barcode.HeaderText = "رقم الفاتورة";
		this.Barcode.MinimumWidth = 6;
		this.Barcode.Name = "Barcode";
		this.Barcode.ReadOnly = true;
		this.customerDataGridViewTextBoxColumn.DataPropertyName = "Customer";
		this.customerDataGridViewTextBoxColumn.HeaderText = "العميل";
		this.customerDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.customerDataGridViewTextBoxColumn.Name = "customerDataGridViewTextBoxColumn";
		this.customerDataGridViewTextBoxColumn.ReadOnly = true;
		this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
		this.dateDataGridViewTextBoxColumn.FillWeight = 45f;
		this.dateDataGridViewTextBoxColumn.HeaderText = "التاريخ";
		this.dateDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
		this.dateDataGridViewTextBoxColumn.ReadOnly = true;
		this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
		this.totalDataGridViewTextBoxColumn.FillWeight = 35f;
		this.totalDataGridViewTextBoxColumn.HeaderText = "الإجمالي";
		this.totalDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
		this.totalDataGridViewTextBoxColumn.ReadOnly = true;
		this.sentDataGridViewCheckBoxColumn.DataPropertyName = "Sent";
		this.sentDataGridViewCheckBoxColumn.FillWeight = 10f;
		this.sentDataGridViewCheckBoxColumn.HeaderText = "مرسل";
		this.sentDataGridViewCheckBoxColumn.MinimumWidth = 6;
		this.sentDataGridViewCheckBoxColumn.Name = "sentDataGridViewCheckBoxColumn";
		this.sentDataGridViewCheckBoxColumn.ReadOnly = true;
		this.uUIDDataGridViewTextBoxColumn.DataPropertyName = "UUID";
		this.uUIDDataGridViewTextBoxColumn.HeaderText = "UUID";
		this.uUIDDataGridViewTextBoxColumn.MinimumWidth = 6;
		this.uUIDDataGridViewTextBoxColumn.Name = "uUIDDataGridViewTextBoxColumn";
		this.uUIDDataGridViewTextBoxColumn.ReadOnly = true;
		this.Print.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.Print.FillWeight = 35f;
		this.Print.HeaderText = "طباعه";
		this.Print.MinimumWidth = 6;
		this.Print.Name = "Print";
		this.Print.ReadOnly = true;
		this.Print.Text = "طباعه";
		this.Print.UseColumnTextForButtonValue = true;
		this.ordersVMBindingSource.DataSource = typeof(E_Invoice.Domain.Models.OrdersVM);
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.toolStripSplitButton1, this.toolStripButton1, this.toolStripButton2, this.toolStripButton4 });
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.toolStrip1.Size = new System.Drawing.Size(1472, 27);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStrip1.Visible = false;
		this.toolStripSplitButton1.Image = E_Invoice.Desktop.Properties.Resources.Send_Icon_72;
		this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton1.Name = "toolStripSplitButton1";
		this.toolStripSplitButton1.Size = new System.Drawing.Size(107, 24);
		this.toolStripSplitButton1.Text = "ارسال المحدد";
		this.toolStripSplitButton1.ToolTipText = "ارسال الفاتورة المحددة";
		this.toolStripSplitButton1.ButtonClick += new System.EventHandler(toolStripSplitButton1_ButtonClick);
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(92, 24);
		this.toolStripButton1.Text = "إلغاء الفاتورة";
		this.toolStripButton1.Visible = false;
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(95, 24);
		this.toolStripButton2.Text = "حذف الفاتورة";
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(419, 46);
		this.txtName.Margin = new System.Windows.Forms.Padding(4);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtName.Size = new System.Drawing.Size(315, 25);
		this.txtName.TabIndex = 45;
		this.txtName.TextChanged += new System.EventHandler(txtName_TextChanged);
		this.labelEx1.Location = new System.Drawing.Point(313, 44);
		this.labelEx1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx1.Name = "labelEx1";
		this.labelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx1.Size = new System.Drawing.Size(100, 29);
		this.labelEx1.TabIndex = 44;
		this.labelEx1.Text = "اسم العميل";
		this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelEx3.Location = new System.Drawing.Point(16, 45);
		this.labelEx3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.labelEx3.Name = "labelEx3";
		this.labelEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labelEx3.Size = new System.Drawing.Size(73, 29);
		this.labelEx3.TabIndex = 43;
		this.labelEx3.Text = "باركود";
		this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCode.IsNumber = false;
		this.txtCode.Location = new System.Drawing.Point(100, 46);
		this.txtCode.Margin = new System.Windows.Forms.Padding(4);
		this.txtCode.Name = "txtCode";
		this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.txtCode.Size = new System.Drawing.Size(186, 25);
		this.txtCode.TabIndex = 42;
		this.txtCode.TextChanged += new System.EventHandler(txtCode_TextChanged);
		this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtFrom.Location = new System.Drawing.Point(832, 46);
		this.dtFrom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.dtFrom.Name = "dtFrom";
		this.dtFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.dtFrom.Size = new System.Drawing.Size(140, 25);
		this.dtFrom.TabIndex = 46;
		this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtTo.Location = new System.Drawing.Point(1063, 47);
		this.dtTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.dtTo.Name = "dtTo";
		this.dtTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.dtTo.Size = new System.Drawing.Size(140, 25);
		this.dtTo.TabIndex = 46;
		this.button1.Location = new System.Drawing.Point(1259, 36);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(96, 45);
		this.button1.TabIndex = 47;
		this.button1.Text = "بحث";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.toolStripButton4.Image = (System.Drawing.Image)resources.GetObject("toolStripButton4.Image");
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(126, 24);
		this.toolStripButton4.Text = "تعديل تاريخ الفاتورة";
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1472, 673);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dtTo);
		base.Controls.Add(this.dtFrom);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.labelEx1);
		base.Controls.Add(this.labelEx3);
		base.Controls.Add(this.txtCode);
		base.Controls.Add(this.DGVItems);
		base.Controls.Add(this.toolStrip1);
		base.Name = "frmInvoices";
		this.Text = "";
		base.Load += new System.EventHandler(frmInvoices_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ordersVMBindingSource).EndInit();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
