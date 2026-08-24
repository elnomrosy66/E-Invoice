using E_Invoice.DAL.Repositories;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using E_Invoice.Domian.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Forms
{
    public partial class frmMain : A
    {
        private readonly Cmd<Store> _cmd;
        private readonly UnitOfWork unitOfWork;
       
        public frmMain()
        {
            _cmd = new Cmd<Store>();
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Info.CurrenBranch = _unitOfWork.Branchs.GetAll().FirstOrDefault();
            Info._setting = _unitOfWork.Settings.GetAll().ToList().FirstOrDefault();
            Info._Company = _unitOfWork.Companies.GetAll().ToList().FirstOrDefault();

        }

        private void الأصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmProductsAdd();
            frm.ShowDialog();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Tag != null)
            {

            }
        }

        private void تعريفالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmStoresAdd();
            frm.ShowDialog();
        }

        private void مجموعاتالاصنافToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new frmCategories();
            frm.ShowDialog();
        }

        private void الوحداتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new frmUnitsAdd();
            frm.ShowDialog();
        }

        private void الافرعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmBranchesAdd();
            frm.ShowDialog();
        }

        private void فاتورةبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoice(OrderType.Sale);
            frm.ShowDialog();
        }

        private void مرتجعبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoice(OrderType.SaleReturn);
            frm.ShowDialog();
        }

        private void فاتورةشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoice(OrderType.Purcahse);
            frm.ShowDialog();
        }

        private void مرتجعشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoice(OrderType.PurchaseReturn);
            frm.ShowDialog();
        }

        private void قائمةالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAccounts(AccountType.Customer);
            frm.ShowDialog();
        }

        private void gfrgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmCompanies();
            frm.ShowDialog();
        }

        private void قائمةالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmAccounts(AccountType.supplier);
            frm.ShowDialog();
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void الاعداداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmSetting();
            frm.ShowDialog();
        }

        private void ارسالفواتيرالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.Sale);
            frm.ShowDialog();
        }

        private void ارسالمرتجعالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.SaleReturn);
            frm.ShowDialog();
        }

        private void تحميلالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DonloadInvoices();
            frm.ShowDialog();
        }

        private void الفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.Sale);
            frm.ShowDialog();
        }

        private void فواتيرالشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.Purcahse);
            frm.ShowDialog();
        }

        private void الفواتيرالمرسلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.Sale , OrderEinvSend.Sent);
            frm.ShowDialog();
        }

        private void مرتجعبيعمرسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInvoices(OrderType.SaleReturn, OrderEinvSend.Sent);
            frm.ShowDialog();
        }

        
    }
}
