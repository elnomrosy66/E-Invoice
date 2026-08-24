using E_Invoice.DAL.Repositories;
using E_Invoice.Domain;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
using E_Invoice.Domain.Services;
using E_Invoice.Domian.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//add validation roles
//add descount
//alow adding other taxes
//allow edit customer info
//allow send after save
//allow print invoice
//allow share puplic url
//read downloades pakage
//download pakage with csv
//backup database
//Read notifications
//add flow notifications
//configure login and securety roles
//confiure database connection
//complete other modules
//create reports
namespace E_Invoice.Desktop.Forms
{
    public partial class frmInvoices : A
    {
        public OrderType _orderType;
        public OrderEinvSend _OrderEinvSend;
        Tax _tax;
        public frmInvoices(OrderType invType , OrderEinvSend orderEinvSend = OrderEinvSend.NotSent)
        {
            InitializeComponent();
            _tax = new Tax();
            _orderType = invType;
            _OrderEinvSend = orderEinvSend;
            GetData();
        }

        public  void GetData()
        {
            var t = _unitOfWork.Orders.GetAllBy(x => x.OrderType == (_orderType) && x.sent == _OrderEinvSend);
            DGVItems.DataSource = (from s in _unitOfWork.Orders.GetAllBy(x=>x.OrderType == (_orderType) && x.sent == _OrderEinvSend)
                                   join acc in _unitOfWork.Accounts.GetAll() on s.AccountId equals acc.Id
                                  select  new OrdersVM
                                  {
                                      Id = s.Id.ToLong(),
                                      Barcode = s.OrderBarcode,
                                      Customer = acc.Name,
                                      Date = s.Date.ToShortDateString(),
                                      UUID = s.uuid,
                                      Sent = s.sent,
                                      Total = s.NetInvoice,
                                  }).ToList<OrdersVM>();
           // DGVItems.DataSource = _tax.GetRecentDocuments(1, 10);
        }

        private void DGVItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var dataIndexNo = DGVItems.Rows[e.RowIndex].Index.ToString();
            //string cellValue = DGVItems.Rows[e.RowIndex].Cells[0].Value.ToString();
            //var frm = new frmInvoice(cellValue.ToInt());
            //frm.ShowDialog();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            //if (DGVItems.Rows.Count > 0)
            //    DGVItems.SelectedRows = 0;
            int index = DGVItems.SelectedRows[0].Index;
            int id = (int)DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
            send(id);
        }

        //send
        public void send(int id)
        {
            Order order = _unitOfWork.Orders.GetTBy(x => x.Id == id);
            if(order.sent == OrderEinvSend.Sent)
            {
                GetDocumentResponse doc = _tax.GetDocument(order.uuid);
                if (doc.status != "Valid")
                    MessageBox.Show("سيتم ااعادة ارسال الفاتورة مرة اخري");
                else
                {
                    order.Status = doc.status;
                    _unitOfWork.Orders.Update(order);
                    MessageBox.Show("تم ارسال الفاتورة من قبل");
                    return;
                }
                    
            }
            List<OrderDetail> OrderDetail = _unitOfWork.OrderDetails.GetAllBy(x => x.OrderId == order.Id).ToList();
            List<invoiceLines> invoiceLineList = new List<invoiceLines>();
            //string message;
            List<documents> documentsList = new List<documents>(); // list of invoices

            documents document1 = new documents();
            List<validationErrors> errors = new List<validationErrors>();
            //List<documents> documentsList = new List<documents>();
            Branch branch = _unitOfWork.Branchs.GetTById(order.BranchId);
            Company comp = _unitOfWork.Companies.GetAll().FirstOrDefault();
            issuer issuer = new issuer
            {
                type = "B", //الشكل القانوني
                id = comp.TaxReg,
                name = comp.Name,
                address = new address
                {
                    branchId = branch.Code,
                    country = branch.Country,
                    governate = branch.Governate,
                    regionCity = branch.RegionCity,
                    street = branch.Street,
                    buildingNumber = branch.BuildingNumber,
                    floor = " ",
                    landmark = " ",
                    postalCode = " ",
                    room = " ",
                    additionalInformation = " "
                }
            };

            Account gLCODE = _unitOfWork.Accounts.GetAllBy(x => x.Id == order.AccountId).FirstOrDefault();
            receiver receiver = new receiver();
            if (order.CustomerNakdy == false)
                receiver = new receiver
                {
                    id = gLCODE.TaxReg,
                    type = ((CanonicalType)gLCODE.CanonicalType).ToString(),
                    name = gLCODE.Name,
                    address = new address
                    {
                        country = gLCODE.Country.ToString(),
                        governate = gLCODE.Governate.ToString(),
                        regionCity = gLCODE.RegionCity.ToString(),
                        street = gLCODE.Street.ToString(),
                        buildingNumber = gLCODE.BuildingNumber.ToString(),
                        floor = " ",
                        landmark = " ",
                        postalCode = " ",
                        room = " ",
                        additionalInformation = " "
                    }
                };
            else
                receiver = new receiver
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
                };

            List<invoiceLines> invoiceLines = new List<invoiceLines>();

            if (OrderDetail.Count() == 0)
            {
                MessageBox.Show("لا يوجد بنود في الفاتورة");
                return;
            }
            foreach (OrderDetail item in OrderDetail)
            {
                //if (item.itemCode == null) { errors.Add(new validationErrors { Message = " الخدمة  ليس له تكويد عالمي لا يمكن استكمال الارسال", Reference = item.description, Color = "red" }); }
                unitValue unit = new unitValue { currencySold = "EGP", amountEGP = Convert.ToDecimal(item.Price.ToString("N5")) };
                //discount discount = new discount { amount = (decimal)(item.Price*(order.DiscountRate/100)), rate = (decimal)order.DiscountRate };
                Product product = _unitOfWork.Products.GetTById((int)item.ProductId);
                invoiceLines invoiceLine = new invoiceLines();
                invoiceLine.description = item.ProductDesc;  //product.Name;
                invoiceLine.itemType = Info._setting.UseGpc == true?"GS1": product.itemType.ToString();
                invoiceLine.itemCode = Info._setting.UseGpc == true ? product.GPCCode.ToString() : product.itemCode;
                invoiceLine.unitType = "EA";//                item.unitType;
                invoiceLine.quantity = Convert.ToDecimal(((decimal)(item.Quantity)).ToString("N5"));
                invoiceLine.unitValue = unit;
                invoiceLine.salesTotal = Convert.ToDecimal((unit.amountEGP * (decimal)item.Quantity).ToString("N5"));
                invoiceLine.discount = new discount { amount = (Convert.ToDecimal((0).ToString("N5"))), rate = (Convert.ToDecimal((0).ToString("N5")))};
                invoiceLine.netTotal = Convert.ToDecimal((invoiceLine.salesTotal - invoiceLine.discount.amount).ToString("N5"));
                invoiceLine.valueDifference = (Convert.ToDecimal((0).ToString("N5"))); //When tax is included in unit price must put tax value in valuedifference
                invoiceLine.totalTaxableFees = (Convert.ToDecimal((0).ToString("N5"))); //(decimal)item.totalTaxableFees;    
                invoiceLine.taxableItems = new List<taxableItems>();

                if (order.TotalVat > 0)
                {

                    var ttax = new taxableItems
                    {
                        taxType = "T1",
                        amount = Convert.ToDecimal(((invoiceLine.netTotal * (decimal)(order.TotalVat / 100))).ToString("N5")),
                        subType = "V009",
                        rate = (decimal)order.TotalVat
                    };

                    invoiceLine.taxableItems.Add(ttax);
                }
                        
                decimal t4Total = 0;
                if (order.Tax4 > 0)
                {
                    var t4 = new taxableItems
                    {
                        taxType = "T4",
                        amount = Convert.ToDecimal(((invoiceLine.netTotal * (decimal)(order.Tax4 / 100))).ToString("N5")),
                        subType = "W003",
                        rate = (decimal)order.Tax4
                    };
                    t4Total = t4.amount;
                    invoiceLine.taxableItems.Add(t4);
                }
                
                invoiceLine.total = Convert.ToDecimal((invoiceLine.netTotal + invoiceLine.taxableItems.Where(xx=>xx.taxType=="T1").Sum(x => x.amount) - t4Total).ToString("N5"));
                invoiceLine.itemsDiscount = (Convert.ToDecimal((0).ToString("N5")));  //Non - taxable items discount.
                invoiceLine.internalCode = product.Code;
                invoiceLines.Add(invoiceLine);
            }
            document1.invoiceLines = invoiceLines;
            document1.internalID = order.OrderBarcode.ToString();// "12345659";
            document1.issuer = issuer;
            document1.receiver = receiver;
            document1.documentType = order.OrderType == OrderType.Sale? "I" : "C";
            document1.documentTypeVersion = Info._setting.ProdEnv == true ? "1.0" : "0.9"; //} else { document1.documentTypeVersion = "0.9"; };
            document1.purchaseOrderReference = " ";
            document1.purchaseOrderDescription = " ";
            document1.salesOrderReference = " ";
            document1.salesOrderDescription = " ";
            document1.proformaInvoiceNumber = " ";
            document1.payment = new payment
            {
                bankName = " ",
                bankAddress = "",
                bankAccountNo = " ",
                bankAccountIBAN = "",
                swiftCode = "",
                terms = " "
            };
            DateTime d = (DateTime)order.Date;
            document1.dateTimeIssued = d.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            document1.taxpayerActivityCode = comp.ActivityCode;
            document1.totalSalesAmount = Convert.ToDecimal((invoiceLines.Sum(i => i.salesTotal)).ToString("N5"));//(decimal)newinvoice.AMOUNT,  
            document1.totalAmount = Convert.ToDecimal((invoiceLines.Sum(i => i.discount.amount)).ToString("N5"));     //Total amount of discounts: sum of all Discount amount elements of InvoiceLine items.
            document1.extraDiscountAmount = (Convert.ToDecimal((0).ToString("N5")));// (decimal)order.TotalDiscount;
            document1.totalDiscountAmount = (Convert.ToDecimal((0).ToString("N5")));
            document1.netAmount = Convert.ToDecimal((invoiceLines.Sum(i => i.netTotal)).ToString("N5"));
            document1.totalAmount = Convert.ToDecimal((invoiceLines.Sum(i => i.total)).ToString("N5"));
            document1.delivery = new delivery
            {
                approach = " ",
                packaging = " ",
                dateValidity = d.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
                exportPort = " ",
                countryOfOrigin = " ",
                grossWeight = 0,
                netWeight = 0,
            };
            document1.totalItemsDiscountAmount = (Convert.ToDecimal((0).ToString("N5")));
            document1.references = new List<string> { };
            List<taxTotals> tax = new List<taxTotals>();
            
            if (order.TotalVat > 0)
            {
                tax.Add(new taxTotals
                {
                    taxType = "T1",
                    amount = invoiceLines.Sum(x => x.taxableItems.Where(y => y.taxType == "T1").Sum(z => z.amount))
                });
            }
            if (order.Tax4 > 0)
            {
                tax.Add(new taxTotals
                {
                    taxType = "T4",
                    amount = invoiceLines.Sum(x => x.taxableItems.Where(y => y.taxType == "T4").Sum(z => z.amount))
                });
            }
            document1.taxTotals = tax;
            documentsList.Add(document1);
            Tax t = new Tax();
            Access a = t.GetAccess();
            if (a.AccessError != null)
            {
                MessageBox.Show(a.AccessError);
                return;
            }
            JsonSerializerSettings jss = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.None
            };
            string jsonInvoice = JsonConvert.SerializeObject(document1, jss);
            TokenSigner t123 = new TokenSigner();
            string signedinvoice = t123.PutSignatures(jsonInvoice);

            signeddoc dot = JsonConvert.DeserializeObject<signeddoc>(signedinvoice);
            if (dot.signatures[0].value == "No slots found")
            {
                MessageBox.Show("No slots found");
            }
            if (dot.signatures[0].value == "Certificate not found")
            {
                MessageBox.Show("Certificate not found");
            }
            if (dot.signatures[0].value == "no device detected")
            {
                MessageBox.Show("no device detected");
            }
            string signedjsonInvoice = "{\"documents\":[" + signedinvoice + "]}";
            RestClient client = new RestClient("https://api.preprod.invoicing.eta.gov.eg/api/v1/documentsubmissions");
            if (Info._setting.ProdEnv == true)
            {
                client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1/documentsubmissions");
            }
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + a.access_token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", signedjsonInvoice, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show("BadRequest /n" + response.Content);
                return;
            }
            if (response.StatusCode.ToString() == "422")
            {
                MessageBox.Show("الفاتورة مرسلة من قبل /n" + response.Content);
                return;
            }
            if (response.StatusCode.ToString() == "400")
            {
                MessageBox.Show("تم تخطي الحد الاقصي /n" + response.Content);
                return;
            }
            if (response.StatusCode.ToString() == "403")
            {
                MessageBox.Show("خطاء في رقم التسجيل الضريبي /n" + response.Content);
                return;
            }
            respo r = JsonConvert.DeserializeObject<respo>(response.Content);
            if (r.acceptedDocuments.Count > 0)
            {

                order.sent = OrderEinvSend.Sent;
                order.DateSent = DateTime.Now;
                order.Status = OrderEinvSatatus.Submitted.ToString();
                order.uuid = r.acceptedDocuments[0].uuid.ToString();
                try
                {
                    _unitOfWork.Orders.Update(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                MessageBox.Show("تم الارسا بنجاح UUID = " + r.acceptedDocuments[0].uuid);
            }
            else
                MessageBox.Show(JsonConvert.SerializeObject(r, jss));
        }
        private void DGVItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var sendergrid = (DataGridView)sender;
            if(sendergrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MessageBox.Show("fff");
            }
            else
            {
                int index = DGVItems.SelectedRows[0].Index;
                bool selecte = Convert.ToBoolean(DGVItems.Rows[index].Cells["Selected"].Value);
                if (selecte == true)
                    DGVItems.Rows[index].Cells["Selected"].Value = false;
                else DGVItems.Rows[index].Cells["Selected"].Value = true;               
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int index = DGVItems.SelectedRows[0].Index;
            int id = (int)DGVItems.Rows[index].Cells[1].Value.ToString().ToInt();
            var frm = new frmCancleInvoice(id);
            frm.ShowDialog();
        }
    }
}
