using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Controls
{
    class GroupBoxEx: GroupBox
    {
        public GroupBoxEx()
        {
            this.BackColor = Color.Transparent;
            this.RightToLeft = RightToLeft.No;
            //if (Info.Lang == Language.ar)
            //{

                this.RightToLeftLayout = true;
            //}
            //else
            //{

            //    this.RightToLeftLayout = false;
            //}


        }

        private bool myRightToLeftLayout = false;
        [Localizable(true)]
        public bool RightToLeftLayout
        {
            get { return myRightToLeftLayout; }
            set
            {
                if (value != myRightToLeftLayout)
                {
                    foreach (Control item in base.Controls)
                    {
                        try
                        {
                            item.RightToLeft = value == true ? RightToLeft.No : RightToLeft.Yes;
                            item.Location = new System.Drawing.Point(base.Size.Width - item.Size.Width - item.Location.X, item.Location.Y);
                        }
                        catch { }
                    }
                    myRightToLeftLayout = value;
                    this.RecreateHandle();
                }
            }
        }


        protected override CreateParams CreateParams
        {
            get
            {
                return Control_RTF(base.CreateParams, base.RightToLeft);
            }
        }

        private CreateParams Control_RTF(CreateParams CP, RightToLeft rightToLeft)
        {
            if (rightToLeft == System.Windows.Forms.RightToLeft.Yes)
            {
                myRightToLeftLayout = true;
                RightToLeftLayout = true;
                CP.ExStyle = ((CP.ExStyle | 0x400000) | 0x100000);
            }
            else
            {
                myRightToLeftLayout = false;
                RightToLeftLayout = false;
            }
            return CP;
        }


    }
}
