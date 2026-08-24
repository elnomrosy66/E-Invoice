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
    public partial class frmStoresAdd : Master
    {

        public Store store;
        public frmStoresAdd()
        {
            InitializeComponent();
            New();
            Refresh();
        }
        public override void Refresh()
        {
            FillMaster(_unitOfWork.Stores.GetAll());

            base.Refresh();
        }
        public override void New()
        {
            store = new Store();
            base.New();
        }
        public override void GetData()
        {
            txtName.Text = store.Name;
            
            base.GetData();
        }
        public override void SetData()
        {
            store.Name = txtName.Text;
            
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (store.Id == 0)
            {
                _unitOfWork.Stores.Add(store);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Stores.Update(store);
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
                if (store != null)
                {
                    store.IsDelete = IsDelete.Deleted;
                    _unitOfWork.Stores.Update(store);
                    Mess.Delete();
                    base.Delete();
                }
            }
        }

        public override void GetSelectedItem()
        {
            Store id = (Store)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            store = _unitOfWork.Stores.GetTById(id.Id);
            GetData();

        }
    }
}
