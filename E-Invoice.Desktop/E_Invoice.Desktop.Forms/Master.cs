using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class Master : A
{
	public int currentItem;

	private IContainer components = null;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripButton toolStripButton6;

	public ToolStripComboBox MasterCompo;

	public ToolStrip toolStrip1;

	public ToolStripButton toolStripButton2;

	public ToolStripButton toolStripButton3;

	public ToolStripLabel toolStripLabel1;

	public ToolStripTextBox toolStripTextBox1;

	public ToolStripButton toolStripButton4;

	public ToolStripButton toolStripButton5;

	public ToolStripButton toolStripSplitButton1;

	public ToolStripButton toolStripSplitButton2;

	public ToolStripComboBox compBranch;

	public ToolStripLabel lblBranch;

	public ToolStripButton toolStripButton1;

	public Master()
	{
		InitializeComponent();
	}

	public virtual void Save()
	{
	}

	public virtual void New()
	{
		GetData();
	}

	public new virtual void Refresh()
	{
	}

	public virtual void Delete()
	{
		Refresh();
		New();
	}

	public virtual void GetData()
	{
	}

	public virtual void SetData()
	{
	}

	public virtual void CloseApp()
	{
		Close();
	}

	public virtual void FillMaster(IEnumerable<object> data)
	{
		MasterCompo.ComboBox.DataSource = data;
		MasterCompo.ComboBox.DisplayMember = "Name";
		MasterCompo.ComboBox.ValueMember = "Id";
	}

	public void Fill(ComboBox combo, IEnumerable<object> data)
	{
		if (data != null)
		{
			combo.DataSource = data;
			combo.DisplayMember = "Name";
			combo.ValueMember = "Id";
		}
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		New();
	}

	private void toolStripButton2_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void toolStripButton3_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void toolStripButton6_Click(object sender, EventArgs e)
	{
		CloseApp();
	}

	private void MasterCompo_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (MasterCompo != null && MasterCompo.ComboBox.SelectedIndex > -1)
		{
			GetSelectedItem();
		}
	}

	public virtual void GetSelectedItem()
	{
	}

	private void Master_Load(object sender, EventArgs e)
	{
		currentItem = 0;
	}

	private void toolStripSplitButton1_Click(object sender, EventArgs e)
	{
		currentItem = 0;
		if (MasterCompo.Items.Count > 0)
		{
			MasterCompo.SelectedIndex = currentItem;
		}
	}

	private void toolStripSplitButton2_Click(object sender, EventArgs e)
	{
		currentItem = ((MasterCompo.Items.Count > 0) ? (MasterCompo.Items.Count - 1) : 0);
		if (MasterCompo.Items.Count > 0)
		{
			MasterCompo.SelectedIndex = currentItem;
		}
		else
		{
			currentItem = MasterCompo.Items.Count - 1;
		}
	}

	private void toolStripButton5_Click(object sender, EventArgs e)
	{
		currentItem++;
		if (MasterCompo.Items.Count > 0 && MasterCompo.Items.Count > currentItem)
		{
			MasterCompo.SelectedIndex = currentItem;
			return;
		}
		currentItem = MasterCompo.Items.Count - 1;
		MasterCompo.SelectedIndex = currentItem;
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		currentItem--;
		if (MasterCompo.Items.Count > 0 && 0 < currentItem)
		{
			MasterCompo.SelectedIndex = currentItem;
			return;
		}
		currentItem = 0;
		MasterCompo.SelectedIndex = currentItem;
	}

	private void compBranch_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (compBranch != null && compBranch.ComboBox.SelectedIndex > -1)
		{
			Branch branch = (Branch)compBranch.ComboBox.SelectedItem;
			if (branch != null)
			{
				Info.CurrenBranch = _unitOfWork.Branchs.GetTById(branch.Id);
				GetNewCode();
			}
		}
	}

	public virtual void GetNewCode()
	{
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
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.MasterCompo = new System.Windows.Forms.ToolStripComboBox();
		this.lblBranch = new System.Windows.Forms.ToolStripLabel();
		this.compBranch = new System.Windows.Forms.ToolStripComboBox();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Black", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.toolStripButton1, this.toolStripButton2, this.toolStripButton3, this.toolStripSeparator1, this.toolStripSplitButton1, this.toolStripButton4, this.toolStripLabel1, this.toolStripTextBox1, this.toolStripButton5, this.toolStripSplitButton2,
			this.toolStripButton6, this.MasterCompo, this.lblBranch, this.compBranch
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(1075, 27);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton1.Image = E_Invoice.Desktop.Properties.Resources.Add_1_Icon_72;
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(55, 24);
		this.toolStripButton1.Tag = "New";
		this.toolStripButton1.Text = "جديد";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton2.Image = E_Invoice.Desktop.Properties.Resources.Save_Icon_72;
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(54, 24);
		this.toolStripButton2.Text = "حفظ";
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton3.Image = E_Invoice.Desktop.Properties.Resources.Remove_Icon_72;
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(56, 24);
		this.toolStripButton3.Text = "حذف";
		this.toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
		this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButton1.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripSplitButton1.Image = E_Invoice.Desktop.Properties.Resources.Hide_right_Icon_72;
		this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton1.Name = "toolStripSplitButton1";
		this.toolStripSplitButton1.Size = new System.Drawing.Size(24, 24);
		this.toolStripSplitButton1.Text = "toolStripSplitButton1";
		this.toolStripSplitButton1.Click += new System.EventHandler(toolStripSplitButton1_Click);
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton4.Image = E_Invoice.Desktop.Properties.Resources.Navigate_right_Icon_72;
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(24, 24);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(30, 24);
		this.toolStripLabel1.Text = "بحث";
		this.toolStripTextBox1.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripTextBox1.Name = "toolStripTextBox1";
		this.toolStripTextBox1.Size = new System.Drawing.Size(100, 27);
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton5.Image = E_Invoice.Desktop.Properties.Resources.Navigate_left_Icon_72;
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.toolStripButton5.Size = new System.Drawing.Size(24, 24);
		this.toolStripButton5.Text = "toolStripButton5";
		this.toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
		this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
		this.toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
		this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButton2.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripSplitButton2.Image = E_Invoice.Desktop.Properties.Resources.Hide_left_Icon_72;
		this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButton2.Name = "toolStripSplitButton2";
		this.toolStripSplitButton2.Size = new System.Drawing.Size(24, 24);
		this.toolStripSplitButton2.Text = "toolStripSplitButton2";
		this.toolStripSplitButton2.Click += new System.EventHandler(toolStripSplitButton2_Click);
		this.toolStripButton6.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton6.Image = E_Invoice.Desktop.Properties.Resources.Windows_Close_Program_Icon_72;
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(59, 24);
		this.toolStripButton6.Text = "خروج";
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.MasterCompo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.MasterCompo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.MasterCompo.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.MasterCompo.Name = "MasterCompo";
		this.MasterCompo.Size = new System.Drawing.Size(121, 27);
		this.MasterCompo.SelectedIndexChanged += new System.EventHandler(MasterCompo_SelectedIndexChanged);
		this.lblBranch.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblBranch.Name = "lblBranch";
		this.lblBranch.Size = new System.Drawing.Size(35, 24);
		this.lblBranch.Text = "الفرع";
		this.lblBranch.Visible = false;
		this.compBranch.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.compBranch.Name = "compBranch";
		this.compBranch.Size = new System.Drawing.Size(121, 27);
		this.compBranch.Visible = false;
		this.compBranch.SelectedIndexChanged += new System.EventHandler(compBranch_SelectedIndexChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1075, 608);
		base.Controls.Add(this.toolStrip1);
		base.Name = "Master";
		base.Load += new System.EventHandler(Master_Load);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
