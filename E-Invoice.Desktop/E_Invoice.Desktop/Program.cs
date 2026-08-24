using System;
using System.Windows.Forms;
using E_Invoice.Desktop.Forms;

namespace E_Invoice.Desktop;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Login());
	}
}
