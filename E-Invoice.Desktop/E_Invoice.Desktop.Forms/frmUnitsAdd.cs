using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmUnitsAdd : Master
{
	private new readonly UnitOfWork _unitOfWork;

	public Unit unit;

	private IContainer components = null;

	private TextBoxEx txtName;

	private TextBoxEx txtCode;

	private LabelEx lbName;

	private LabelEx lbCode;

	public frmUnitsAdd()
	{
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		New();
		Refresh();
	}

	public override void Refresh()
	{
		FillMaster(_unitOfWork.Units.GetAll());
		base.Refresh();
	}

	public override void New()
	{
		unit = new Unit();
		base.New();
	}

	public override void GetData()
	{
		txtName.Text = unit.Name;
		txtCode.Text = unit.Code;
		base.GetData();
	}

	public override void SetData()
	{
		unit.Name = txtName.Text;
		unit.Code = txtCode.Text;
		base.SetData();
	}

	public override void Save()
	{
		SetData();
		if (unit.Id == 0)
		{
			_unitOfWork.Units.Add(unit);
			Mess.Save();
		}
		else
		{
			_unitOfWork.Units.Update(unit);
			Mess.Update();
		}
		Refresh();
		New();
		base.Save();
	}

	public override void Delete()
	{
		if (Mess.AskDelete() == DialogResult.Yes && unit != null)
		{
			unit.IsDelete = IsDelete.Deleted;
			_unitOfWork.Units.Update(unit);
			Mess.Delete();
			base.Delete();
		}
	}

	public override void GetSelectedItem()
	{
		Unit unit = (Unit)MasterCompo.ComboBox.SelectedItem;
		if (unit != null)
		{
			this.unit = _unitOfWork.Units.GetTById(unit.Id);
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
		this.txtCode = new E_Invoice.Desktop.Controls.TextBoxEx();
		this.lbName = new E_Invoice.Desktop.Controls.LabelEx();
		this.lbCode = new E_Invoice.Desktop.Controls.LabelEx();
		base.SuspendLayout();
		this.txtName.IsNumber = false;
		this.txtName.Location = new System.Drawing.Point(102, 58);
		this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtName.Name = "txtName";
		this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtName.Size = new System.Drawing.Size(564, 27);
		this.txtName.TabIndex = 6;
		this.txtCode.IsNumber = false;
		this.txtCode.Location = new System.Drawing.Point(102, 97);
		this.txtCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.txtCode.Name = "txtCode";
		this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCode.Size = new System.Drawing.Size(564, 27);
		this.txtCode.TabIndex = 7;
		this.lbName.Location = new System.Drawing.Point(16, 58);
		this.lbName.Name = "lbName";
		this.lbName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbName.Size = new System.Drawing.Size(66, 28);
		this.lbName.TabIndex = 8;
		this.lbName.Text = "الاسم";
		this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbCode.Location = new System.Drawing.Point(16, 97);
		this.lbCode.Name = "lbCode";
		this.lbCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lbCode.Size = new System.Drawing.Size(66, 28);
		this.lbCode.TabIndex = 9;
		this.lbCode.Text = "الكود";
		this.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(866, 286);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.txtCode);
		base.Controls.Add(this.lbName);
		base.Controls.Add(this.lbCode);
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "frmUnitsAdd";
		this.Text = "الوحدات";
		base.Controls.SetChildIndex(this.lbCode, 0);
		base.Controls.SetChildIndex(this.lbName, 0);
		base.Controls.SetChildIndex(this.txtCode, 0);
		base.Controls.SetChildIndex(this.txtName, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
