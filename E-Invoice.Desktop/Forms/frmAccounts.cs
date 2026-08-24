using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;
namespace E_Invoice.Desktop.Forms
{
    public partial class frmAccounts : Master
    {
        
        public Account account;
        public AccountType _AccountType;
        public frmAccounts(AccountType accountType)
        {
            _AccountType = accountType;
            InitializeComponent();
            
            New();
            Refresh();
            
        }
        public override void Refresh()
        {
            FillMaster(_unitOfWork.Accounts.GetAllBy(x => x.AccountType == _AccountType));
            Fill(combCanonicalType, Info.CanonicalTypesList);
            base.Refresh();
        }

        public override void New()
        {
            account = new Account();
            base.New();
        }
        public override void GetData()
        {
            txtName.Text = account.Name;
            txtCountry.Text = account.Country;
            txtGovernate.Text = account.Governate;
            txtRegionCity.Text = account.RegionCity;
            txtStreet.Text = account.Street;
            txtBuildingNumber.Text = account.BuildingNumber;
            txtCommercialRegNo.Text = account.CommercialRegNo;
            txtTaxReg.Text = account.TaxReg;
            txtAddress.Text = account.Address;
            txtEmail.Text = account.Email;
            txtPhone.Text = account.Phone;
            // (account.CanonicalType != null)
                combCanonicalType.SelectedValue = (int)account.CanonicalType;
           // else
                //combCanonicalType.SelectedIndex = -1;
            base.GetData();
        }
        public override void SetData()
        {
            account.Name = txtName.Text;
            account.Country = txtCountry.Text;
            account.Governate = txtGovernate.Text;
            account.RegionCity = txtRegionCity.Text;
            account.Street = txtStreet.Text;
            account.BuildingNumber = txtBuildingNumber.Text;
            account.CommercialRegNo = txtCommercialRegNo.Text;
            account.TaxReg = txtTaxReg.Text;
            account.AccountType = _AccountType;
            account.Address = txtAddress.Text;
            account.Email = txtEmail.Text;
            account.Phone = txtPhone.Text;
            account.CanonicalType = (CanonicalType)combCanonicalType.SelectedValue;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (account.Id == 0)
            {
                _unitOfWork.Accounts.Add(account);
                Mess.Save();
            }
                
            else
            {
                _unitOfWork.Accounts.Update(account);
                Mess.Update();
            }


            Refresh();
            New();
            base.Save();
        }
        public override void Delete()
        {
            if(account != null)
            {
                
                if(Mess.AskDelete() == DialogResult.Yes)
                {
                    account.IsDelete = IsDelete.Deleted;
                    _unitOfWork.Accounts.Update(account);
                    Mess.Delete();
                }
                
            }
            base.Delete();
        }

     

 
        private void frmAccounts_Load(object sender, EventArgs e)
        {
            if(_AccountType == AccountType.Customer)
                this.Text = "العملاء";
            else
                this.Text = "الموردون";



        }
        public override void GetSelectedItem()
        {
            Account id = (Account)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            account = _unitOfWork.Accounts.GetTById(id.Id);
            GetData();

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
