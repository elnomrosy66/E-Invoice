using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels.ResponceModels;
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
    public partial class frmCancleInvoice : A
    {
        public double _OrderId;
        public Order Order;
        public frmCancleInvoice(int OrderId)
        {
            InitializeComponent();
            _OrderId = OrderId;
            Order = _unitOfWork.Orders.GetTById((int)_OrderId);
            GetData();
        }

        public void GetData()
        {
            txtOrderId.Text = Order.OrderBarcode;
            txtOrderUUID.Text = Order.uuid;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ErrorResponce error = _tax.CancleDocument(txtOrderUUID.Text, txtReason.Text);
            if (error.error != null)
            {
                MessageBox.Show(error.error.details[0].message);
            }
            else
                MessageBox.Show("تم طلب إلغاء الفاتورة بنجاح");
        }
    }
}
