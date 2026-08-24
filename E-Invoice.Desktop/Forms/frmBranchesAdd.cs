using E_Invoice.DAL.Repositories;
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
    public partial class frmBranchesAdd: Master
    {
        
        public Branch branch;
        public frmBranchesAdd()
        {
            InitializeComponent();
            
            New();
            Refresh();
            
        }
        public override void Refresh()
        {
            FillMaster(_unitOfWork.Branchs.GetAll());
            base.Refresh();
        }
        public override void New()
        {
            branch = new Branch();
            
            base.New();
        }
        public override void GetData()
        {
            txtName.Text = branch.Name;
            txtCode.Text = branch.Code;
            txtCountry.Text = branch.Country;
            txtGovernate.Text = branch.Governate;
            txtRegionCity.Text = branch.RegionCity;
            txtStreet.Text = branch.Street;
            txtBuildingNumber.Text = branch.BuildingNumber;
            base.GetData();
        }
        public override void SetData()
        {
            branch.Name = txtName.Text;
            branch.Code = txtCode.Text;
            branch.Country = txtCountry.Text;
            branch.Governate = txtGovernate.Text;
            branch.RegionCity = txtRegionCity.Text;
            branch.Street = txtStreet.Text;
            branch.BuildingNumber = txtBuildingNumber.Text;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (branch.Id == 0)
            {
                _unitOfWork.Branchs.Add(branch);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Branchs.Update(branch);
                Mess.Update();
            }

            Refresh();
            New();
            base.Save();
        }
        public override void Delete()
        {
            if (branch != null)
                if (Mess.AskDelete() == DialogResult.Yes)
                {
                branch.IsDelete = IsDelete.Deleted;
                _unitOfWork.Branchs.Update(branch);
                Mess.Delete();
                base.Delete();
                }
            
        }

        public override void GetSelectedItem()
        {
            Branch id = (Branch)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            branch = _unitOfWork.Branchs.GetTById(id.Id);
            GetData();

        }

        private void frmBranchesAdd_Load(object sender, EventArgs e)
        {
            //this.MasterCompo.Visible = false;
            //this.toolStripSplitButton1.Visible = false;
            //this.toolStripSplitButton2.Visible = false;
            //this.toolStripButton5.Visible = false;
            //this.toolStripButton4.Visible = false;
            //this.toolStripTextBox1.Visible = false;
            //this.toolStripLabel1.Visible = false;
        }
    }
}
