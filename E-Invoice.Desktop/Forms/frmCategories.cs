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
    public partial class frmCategories : Master
    {
        private readonly UnitOfWork _unitOfWork;
        public Category category;
        public frmCategories()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            New();
            Refresh();
            
        }

        public override void Refresh()
        {
            FillMaster(_unitOfWork.Categories.GetAll());
            base.Refresh();
        }
        public override void New()
        {
            category = new Category();
            base.New();
        }
        public override void GetData()
        {
            txtName.Text = category.Name;
            txtCode.Text = category.Code;
            base.GetData();
        }
        public override void SetData()
        {
            category.Name = txtName.Text;
            category.Code = txtCode.Text;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (category.Id == 0)
            {
                _unitOfWork.Categories.Add(category);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Categories.Update(category);
                Mess.Update();
            }

            Refresh();
            New();
            base.Save();
        }
        public override void Delete()
        {
            
            if (Mess.AskDelete() == DialogResult.Yes)
            {
                if (category != null)
                {
                    category.IsDelete = IsDelete.Deleted;
                    _unitOfWork.Categories.Update(category);
                    Mess.Delete();
                    base.Delete();
                }
            }
        }

        public override void GetSelectedItem()
        {
            Category id = (Category)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            category = _unitOfWork.Categories.GetTById(id.Id);
            GetData();

        }
    }
}
