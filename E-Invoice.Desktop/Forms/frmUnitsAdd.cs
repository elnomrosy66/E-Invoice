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
    public partial class frmUnitsAdd : Master
    {
        private readonly UnitOfWork _unitOfWork;
        public Unit unit;
        public frmUnitsAdd()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            New();
            Refresh();
        }
        public override void Refresh()
        {
            FillMaster(_unitOfWork.Units.GetAll());
            base.Refresh();
        }
        public override void New()
        {
            unit = new Unit();
            base.New();
        }
        public override void GetData()
        {
            txtName.Text = unit.Name;
            txtCode.Text = unit.Code;
            base.GetData();
        }
        public override void SetData()
        {
            unit.Name = txtName.Text;
            unit.Code = txtCode.Text;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (unit.Id == 0)
            {
                _unitOfWork.Units.Add(unit);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Units.Update(unit);
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
                if (unit != null)
                {
                    unit.IsDelete = IsDelete.Deleted;
                    _unitOfWork.Units.Update(unit);
                    Mess.Delete();
                    base.Delete();
                }
            }
        }

        
        public override void GetSelectedItem()
        {
            Unit id = (Unit)MasterCompo.ComboBox.SelectedItem;
            if (id == null)
                return;
            unit = _unitOfWork.Units.GetTById(id.Id);
            GetData();

        }
    }
}
