using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Domain.Interfaces;
using E_Invoice.Domain.Services;

namespace E_Invoice.Desktop.Forms;

public class A : Form
{
	public IUnitOfWork _unitOfWork;

	public Tax _tax;

	private IContainer components = null;

	public A()
	{
		InitializeComponent();
		_unitOfWork = new UnitOfWork();
		_tax = new Tax();
	}

	private void A_Load(object sender, EventArgs e)
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
		base.SuspendLayout();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(772, 475);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Tahoma", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "A";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(A_Load);
		base.ResumeLayout(false);
	}
}
