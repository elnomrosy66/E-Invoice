using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Controls;

internal class GroupBoxEx : GroupBox
{
	private bool myRightToLeftLayout = false;

	[Localizable(true)]
	public bool RightToLeftLayout
	{
		get
		{
			return myRightToLeftLayout;
		}
		set
		{
			if (value == myRightToLeftLayout)
			{
				return;
			}
			foreach (Control control in base.Controls)
			{
				try
				{
					control.RightToLeft = ((!value) ? RightToLeft.Yes : RightToLeft.No);
					control.Location = new Point(base.Size.Width - control.Size.Width - control.Location.X, control.Location.Y);
				}
				catch
				{
				}
			}
			myRightToLeftLayout = value;
			RecreateHandle();
		}
	}

	protected override CreateParams CreateParams => Control_RTF(base.CreateParams, base.RightToLeft);

	public GroupBoxEx()
	{
		BackColor = Color.Transparent;
		RightToLeft = RightToLeft.No;
		RightToLeftLayout = true;
	}

	private CreateParams Control_RTF(CreateParams CP, RightToLeft rightToLeft)
	{
		if (rightToLeft == RightToLeft.Yes)
		{
			myRightToLeftLayout = true;
			RightToLeftLayout = true;
			CP.ExStyle = CP.ExStyle | 0x400000 | 0x100000;
		}
		else
		{
			myRightToLeftLayout = false;
			RightToLeftLayout = false;
		}
		return CP;
	}
}
