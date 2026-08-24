using E_Invoice.DAL.Repositories;
using E_Invoice.Domain.Helpers;
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
    public partial class frmCompanies : Master
    {
        
        public Company company;
        public frmCompanies()
        {
            InitializeComponent();
            New();
            //FillMaster(_unitOfWork.Accounts.GetAll());
        }
        public override void New()
        {
            //company = 
            company = _unitOfWork.Companies.GetAll().FirstOrDefault() == null ? new Company(): _unitOfWork.Companies.GetAll().FirstOrDefault();
            base.New();
        }
        public override void GetData()
        {
            
            txtName.Text = company.Name;
            txtCountry.Text = company.Country;
            txtGovernate.Text = company.Governate;
            txtRegionCity.Text = company.RegionCity;
            txtStreet.Text = company.Street;
            txtBuildingNumber.Text = company.BuildingNumber;
            txtCommercialRegNo.Text = company.CommercialRegNo;
            txtTaxReg.Text = company.TaxReg;
            txtActCode.Text = company.ActivityCode;
            txtAddress.Text = company.Address;
            txtEmail.Text = company.Email;
            txtPhone.Text = company.Phone;
            base.GetData();
        }
        public override void SetData()
        {
            company.Name = txtName.Text;
            company.Country = txtCountry.Text;
            company.Governate = txtGovernate.Text;
            company.RegionCity = txtRegionCity.Text;
            company.Street = txtStreet.Text;
            company.BuildingNumber = txtBuildingNumber.Text;
            company.CommercialRegNo = txtCommercialRegNo.Text;
            company.TaxReg = txtTaxReg.Text;
            company.ActivityCode = txtActCode.Text;
            company.Address = txtAddress.Text;
            company.Email = txtEmail.Text;
            company.Phone = txtPhone.Text;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (company.Id == 0)
            {
                _unitOfWork.Companies.Add(company);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Companies.Update(company);
                Mess.Update();
            }


            New();
            Info._Company = _unitOfWork.Companies.GetAll().FirstOrDefault();
            base.Save();
        }
        public override void Delete()
        {
            if(company != null)
            {
                company.IsDelete = IsDelete.Deleted;
                _unitOfWork.Companies.Update(company);
                Mess.Delete();
            }
            base.Delete();
        }

        public override void GetSelectedItem()
        {
            int id = (int)MasterCompo.ComboBox.SelectedValue;
            company = _unitOfWork.Companies.GetTById(id);
            GetData();

        }

        private void frmCompanies_Load(object sender, EventArgs e)
        {
            this.MasterCompo.Visible = false;
            this.toolStripSplitButton1.Visible = false;
            this.toolStripSplitButton2.Visible = false;
            this.toolStripButton5.Visible = false;
            this.toolStripButton4.Visible = false;
            this.toolStripTextBox1.Visible = false;
            this.toolStripLabel1.Visible = false;
            this.toolStripButton3.Visible = false;
            this.toolStripButton1.Visible = false;
        }
    }
}
