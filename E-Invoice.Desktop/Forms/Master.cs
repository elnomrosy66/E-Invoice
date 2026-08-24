using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Forms
{
    public partial class Master : A
    {
        public int currentItem;
        public Master()
        {
            InitializeComponent();
        }

        public virtual void Save()
        {

        }
        public virtual void New()
        {
            GetData();
            //Refresh();
        }
        public virtual void Refresh()
        {

        }
        public virtual void Delete()
        {
            Refresh();
            New();
        }
        public virtual void GetData()
        {

        }
        public virtual void SetData()
        {

        }
        public virtual void CloseApp()
        {
            this.Close();
        }

        public virtual void FillMaster(IEnumerable<object> data)
        {
            this.MasterCompo.ComboBox.DataSource = data;
            this.MasterCompo.ComboBox.DisplayMember = "Name";
            this.MasterCompo.ComboBox.ValueMember = "Id";
        }

        public void Fill(ComboBox combo, IEnumerable<object> data)
        {
            if (data != null)
            {
                combo.DataSource = data;
                combo.DisplayMember = nameof(BaseObj.Name);
                combo.ValueMember = nameof(Base.Id);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            New();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            CloseApp();
        }



        private void MasterCompo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MasterCompo != null && MasterCompo.ComboBox.SelectedIndex > -1)
                GetSelectedItem();
        }

        public virtual void GetSelectedItem()
        {

        }



        private void Master_Load(object sender, EventArgs e)
        {
            //FillMaster(_unitOfWork.Branchs.GetAll());
            currentItem = 0;
        }
        //first
        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            currentItem = 0;
            if (MasterCompo.Items.Count > 0)
                MasterCompo.SelectedIndex = currentItem;
        }
        //last
        private void toolStripSplitButton2_Click(object sender, EventArgs e)
        {
            currentItem = MasterCompo.Items.Count > 0 ? MasterCompo.Items.Count - 1 : 0;
            if (MasterCompo.Items.Count > 0)
                MasterCompo.SelectedIndex = currentItem;
            else
                currentItem = MasterCompo.Items.Count - 1;
        }
        //next
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentItem++;
            if (MasterCompo.Items.Count > 0 && MasterCompo.Items.Count > currentItem)
                MasterCompo.SelectedIndex = currentItem;
            else
            {
                currentItem = MasterCompo.Items.Count - 1;
                MasterCompo.SelectedIndex = currentItem;
            }

        }
        //prevous
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentItem--;
            if (MasterCompo.Items.Count > 0 && 0 < currentItem)
                MasterCompo.SelectedIndex = currentItem;
            else
            {
                currentItem = 0;
                MasterCompo.SelectedIndex = currentItem;
            }
        }

        private void compBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (compBranch != null && compBranch.ComboBox.SelectedIndex > -1)
            {
                Branch id = (Branch)compBranch.ComboBox.SelectedItem;
                if (id == null)
                    return;
                Info.CurrenBranch = _unitOfWork.Branchs.GetTById(id.Id);
                GetNewCode();
            }
        }

        public virtual void GetNewCode()
        {

        }



    }
}
