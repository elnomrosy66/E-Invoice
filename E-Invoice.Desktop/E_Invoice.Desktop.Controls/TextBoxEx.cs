using System;
using System.Drawing;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Controls;

internal class TextBoxEx : TextBox
{
	private bool _IsNumber;

	public bool IsNumber
	{
		get
		{
			return _IsNumber;
		}
		set
		{
			_IsNumber = value;
		}
	}

	protected override void OnTextChanged(EventArgs e)
	{
		BackColor = Color.White;
		RightToLeft = RightToLeft.Yes;
		base.OnTextChanged(e);
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		if (IsNumber)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && Text.Contains("."))
			{
				e.Handled = true;
			}
		}
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
		if (IsNumber && Text.StartsWith("."))
		{
			Text = "0" + Text;
			SendKeys.Send("{End}");
		}
	}
}
