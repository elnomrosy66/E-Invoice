using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels;
using E_Invoice.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Forms
{
    public partial class DonloadInvoices : Master
    {
        public Tax _tax;
        public DonloadInvoices()
        {
            _tax = new Tax();
            InitializeComponent();
        }

        private void DonloadInvoices_Load(object sender, EventArgs e)
        {
            //Tax t = new Tax();
            PackageRequests package = _tax.GetPackageRequests();
            if (package.result != null)
                foreach (var p in package.result)
                {
                    DataGridViewRow row = (DataGridViewRow)DGVItems.RowTemplate.Clone();
                    row.CreateCells(DGVItems, p.packageId, p.submissionDate.ToShortDateString(), p.format == 3 ? "Json" : "CSV", p.status == 2 ? "جاهز" : p.status == 1? "غير جاهز" : p.status == 4? "محذوف" : "خطاء", p.queryParams.dateFrom.ToShortDateString(), p.queryParams.dateTo.ToShortDateString());
                    DGVItems.Rows.Add(row);
                }
            else
                Mess.Warning("لا يوجد أي طلبات مسبقة");
            //DGVItems.Columns["queryParams"].Visible = false;
        }

        private void btnEx1_Click(object sender, EventArgs e)
        {
            List<string> Statuses = new List<string>();
            List<string> DocTypes = new List<string>();
            //checkBoxEx1.CheckState = CheckState.Checked;
            foreach (Control ctrl in grpDocStatus.Controls)
            {
                if(ctrl is CheckBoxEx)
                {
                    var s = ctrl as CheckBoxEx;
                    if (s.CheckState== CheckState.Checked)
                    {
                        Statuses.Add(ctrl.Tag.ToString());
                    }
                }
            }
            foreach (Control ctrl in grpDocType.Controls)
            {
                if (ctrl is CheckBoxEx)
                {
                    var s = ctrl as CheckBoxEx;
                    if (s.CheckState == CheckState.Checked)
                    {
                        DocTypes.Add(ctrl.Tag.ToString());
                    }
                }
            }
            PackageRequest packageRequest = new PackageRequest()
            {
                format = "JSON",
                type = "Summary",
                queryParameters = new QueryParameters
                {
                    dateFrom = DTFrom.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
                    dateTo = DTTo.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
                    statuses = Statuses,
                    documentTypeNames = DocTypes,

                }
            };
            // _tax.PrintInvoice("YCA7W673VEJ3S3PRRYQ7WW4G10");
            PackageRequestResponse respo = _tax.RequestPakage(packageRequest);
            if (respo.requestId != null)
                Mess.Save("رقم الطلب : " + respo.requestId);
            else
                Mess.Warning(respo.error.details[0].message);
        }

        private void DGVItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var sendergrid = (DataGridView)sender;
            if (sendergrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DownloadPackageResponse response = new DownloadPackageResponse();
                string rid = DGVItems.Rows[e.RowIndex].Cells[0].Value.ToString();  // "162480";
                response =  _tax.DownloadPackage(rid);
                if (response.error != null)
                    Mess.Warning(response.error.details[0].message);
                else
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
                        dialog.FileName = rid;
                        dialog.RestoreDirectory = true;
                        dialog.Title = "Save an Image File";
                        dialog.DefaultExt = "Zip";
                        
                        //dialog.ShowDialog();
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(dialog.FileName, response.zip);
                            Mess.Save(); 
                        }
                    }
            }
        }
    }
}
