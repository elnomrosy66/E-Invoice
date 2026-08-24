using System.Windows.Forms;

public static class Mess
{
	public static void Save(string mess = "تم الحفظ بنجاح")
	{
		MessageBox.Show(mess, "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	public static void Update(string mess = "تم التعديل بنجاح")
	{
		MessageBox.Show(mess, "تعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	public static void Delete(string mess = "تم الحذف بنجاح")
	{
		MessageBox.Show(mess, "حذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	public static DialogResult AskDelete(string mess = "هل أنت متأكد هل تريد الحذف")
	{
		return MessageBox.Show(mess, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
	}

	public static DialogResult Ask(string mess = "")
	{
		return MessageBox.Show(mess, "!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	}

	public static void Warning(string mess = "")
	{
		MessageBox.Show(mess, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}
}
