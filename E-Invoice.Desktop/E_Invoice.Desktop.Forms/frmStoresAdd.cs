using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmStoresAdd : Master
{
	public Store store;

	private IContainer components = null;

	private TextBoxEx txtName;

	private LabelEx lbName;

	public frmStoresAdd()
	{
		InitializeComponent();
		New();
		Refresh();
	}

	public override void Refresh()
	{
		FillMaster(_unitOfWork.Stores.GetAll());
		base.Refresh();
	}

	public override void New()
	{
		store = new Store();
		base.New();
	}

	public override void GetData()
	{
		txtName.Text = store.Name;
		base.GetData();
	}

	public override void SetData()
	{
		store.Name = txtName.Text;
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (store.Id == 0)
		{
			_unitOfWork.Stores.Add(store);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Stores.Update(store);
			Mess.Update();
		}
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (Mess.AskDelete() == DialogResult.Yes && store != null)
		{
			store.IsDelete = IsDelete.Deleted;
			_unitOfWork.Stores.Update(store);
			Mess.Delete();
			base.Delete();
		}
	}

	public override void GetSelectedItem()
	{
		Store store = (Store)MasterCompo.ComboBox.SelectedItem;
		if (store != null)
		{
			this.store = _unitOfWork.Stores.GetTById(store.Id);
			GetData();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.txtName = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		base.SuspendLayout();
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(95, 71);
		this.txtName.Margin = new System.Windows.Forms.Padding(4);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(564, 27);
		this.txtName.TabIndex = 3;
		this.lbName.Location = new System.Drawing.Point(1, 70);
		this.lbName.Name = "lbName";
		this.lbName.Size = new System.Drawing.Size(66, 29);
		this.lbName.TabIndex = 4;
		this.lbName.Text = "الاسم";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(804, 305);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.lbName);
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "frmStoresAdd";
		this.Text = "المخازن";
		base.Controls.SetChildIndex(this.lbName, 0);
		base.Controls.SetChildIndex(this.txtName, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
