using E_Invoice.Desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop
{
    public static class HelperForm
    {
        public static void ClearText(Control FormName)
        {
            foreach (Control ctrl in FormName.Controls)
            {
                if (ctrl is TextBox || ctrl is RichTextBox)
                {
                    ctrl.Text = "";
                }

                if (ctrl is DataGridView)
                {
                    ((DataGridView)ctrl).Rows.Clear();
                }

                try
                {
                    if (ctrl is ComboBoxEx)
                    {
                        ((ComboBoxEx)ctrl).SelectedIndex = -1;
                    }
                }
                catch (Exception)
                {

                }

                if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).Value = DateTime.Now;
                }
                //if (ctrl is PictureBox)
                //{
                //    ((PictureBox)ctrl).Image = null;
                //}
                if (ctrl.HasChildren)
                {
                    ClearText(ctrl);
                }
            }
        }
    }
}
