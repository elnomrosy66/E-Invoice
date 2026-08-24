using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domain.Models;
using MetroFramework.Controls;
namespace E_Invoice.Desktop.Forms
{
    public partial class frmSetting : Master
    {
        public setting setting;
        public frmSetting()
        {
            InitializeComponent();
            New();
        }

        private void frmSetting_Load(object sender, EventArgs e)
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



        public override void New()
        {
            //company = 
            setting = _unitOfWork.Settings.GetAll().FirstOrDefault() == null ? new setting() { ProdEnv = false , Signer = false } : _unitOfWork.Settings.GetAll().FirstOrDefault();
            base.New();
        }
        public override void GetData()
        {

            txtClientId.Text = setting.ClientId;
            txtClientSecrit.Text = setting.ClientSecret;
            txtSignerPass.Text = setting.TokenPass;
            if(setting.ProdEnv == true )
            {
                togProdEnv.CheckState = CheckState.Checked;
            }
            else
                togSigner.CheckState = CheckState.Unchecked;
            if (setting.Signer == true)
            {
                togSigner.CheckState = CheckState.Checked;
            }
            else
                togSigner.CheckState = CheckState.Unchecked;
            if (setting.UseStoreBalance == true)
            {
                togUseStoreBalanse.CheckState = CheckState.Checked;
            }
            else
                togUseStoreBalanse.CheckState = CheckState.Unchecked;

            if (setting.UseGpc == true)
            {
                togUseGpc.CheckState = CheckState.Checked;
            }
            else
                togUseGpc.CheckState = CheckState.Unchecked;

            base.GetData();
        }
        public override void SetData()
        {
            setting.ClientId = txtClientId.Text;
            setting.ClientSecret = txtClientSecrit.Text;
            setting.TokenPass = txtSignerPass.Text;
            if (togProdEnv.CheckState == CheckState.Checked)
            {
                setting.ProdEnv = true;
            }
            else
                setting.ProdEnv = false;
            if (togSigner.CheckState == CheckState.Checked)
            {
                setting.Signer = true;
            }
            else
                setting.Signer = false;
            if (togUseStoreBalanse.CheckState == CheckState.Checked)
            {
                setting.UseStoreBalance = true;
            }
            else
                setting.UseStoreBalance = false;  

            if (togUseGpc.CheckState == CheckState.Checked)
            {
                setting.UseGpc = true;
            }
            else
                setting.UseGpc = false;
            base.SetData();
        }

        public override void Save()
        {
            SetData();
            if (setting.Id == 0)
            {
                _unitOfWork.Settings.Add(setting);
                Mess.Save();
            }

            else
            {
                _unitOfWork.Settings.Update(setting);
                Mess.Update();
            }


            New();
            Info._setting = _unitOfWork.Settings.GetAll().FirstOrDefault();
            base.Save();
        }
        public override void Delete()
        {
           
            base.Delete();
        }





        
    }
}
