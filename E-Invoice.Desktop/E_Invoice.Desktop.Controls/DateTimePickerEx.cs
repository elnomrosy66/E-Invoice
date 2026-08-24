using System.Windows.Forms;

namespace E_Invoice.Desktop.Controls;

internal class DateTimePickerEx : DateTimePicker
{
	public DateTimePickerEx()
	{
		base.Format = DateTimePickerFormat.Short;
	}
}
