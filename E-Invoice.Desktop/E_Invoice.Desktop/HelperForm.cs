using System;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;

namespace E_Invoice.Desktop;

public static class HelperForm
{
	public static void ClearText(Control FormName)
	{
		foreach (Control control in FormName.Controls)
		{
			if (control is TextBox || control is RichTextBox)
			{
				control.Text = "";
			}
			if (control is DataGridView)
			{
				((DataGridView)control).Rows.Clear();
			}
			try
			{
				if (control is ComboBoxEx)
				{
					((ComboBoxEx)control).SelectedIndex = -1;
				}
			}
			catch (Exception)
			{
			}
			if (control is DateTimePicker)
			{
				((DateTimePicker)control).Value = DateTime.Now;
			}
			if (control.HasChildren)
			{
				ClearText(control);
			}
		}
	}
}
