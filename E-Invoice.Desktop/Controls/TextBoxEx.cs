using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Controls
{
    class TextBoxEx: TextBox
    {

        protected override void OnTextChanged(EventArgs e)
        {
            this.BackColor = Color.White;
            this.RightToLeft = RightToLeft.Yes;
            base.OnTextChanged(e);
        }

        private bool _IsNumber;
        public bool IsNumber
        {
            get { return _IsNumber; }
            set { _IsNumber = value; }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (IsNumber)
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && this.Text.Contains("."))
                {
                    e.Handled = true;

                }

            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (IsNumber)
            {
                if (Text.StartsWith("."))
                {
                    Text = "0" + Text;
                    SendKeys.Send("{End}");
                }
            }
        }
    }
}
