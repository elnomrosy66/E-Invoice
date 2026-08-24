using E_Invoice.DAL.Repositories;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Services;
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
    public partial class A : Form
    {
        public IUnitOfWork _unitOfWork;
        public Tax _tax;
        public A()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            _tax = new Tax();
        }

        private void A_Load(object sender, EventArgs e)
        {
            
        }
    }
}
