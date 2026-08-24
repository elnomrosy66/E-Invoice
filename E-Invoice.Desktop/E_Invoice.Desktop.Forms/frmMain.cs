using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_Invoice.DAL.Repositories;
using E_Invoice.Desktop.Properties;
using E_Invoice.Domain.Helpers;
using E_Invoice.Domian.Models;

namespace E_Invoice.Desktop.Forms;

public class frmMain : A
{
	private readonly Cmd<Store> _cmd;

	private readonly UnitOfWork unitOfWork;

	private IContainer components = null;

	private ToolStrip toolStrip1;

	private ToolStripDropDownButton toolStripDropDownButton1;

	private ToolStripMenuItem gfrgToolStripMenuItem;

	private ToolStripMenuItem الاعداداتToolStripMenuItem;

	private ToolStripMenuItem تعريفالمخازنToolStripMenuItem;

	private ToolStripMenuItem مجموعاتالاصنافToolStripMenuItem1;

	private ToolStripMenuItem الوحداتToolStripMenuItem1;

	private ToolStripDropDownButton toolStripDropDownButton2;

	private ToolStripMenuItem فاتورةبيعToolStripMenuItem;

	private ToolStripMenuItem مرتجعبيعToolStripMenuItem;

	private ToolStripMenuItem الفواتيرToolStripMenuItem;

	private ToolStripDropDownButton toolStripDropDownButton3;

	private ToolStripMenuItem فاتورةشراءToolStripMenuItem;

	private ToolStripMenuItem مرتجعشراءToolStripMenuItem;

	private ToolStripMenuItem فواتيرالشراءToolStripMenuItem;

	private ToolStripDropDownButton toolStripDropDownButton4;

	private ToolStripMenuItem ارسالفواتيرالبيعToolStripMenuItem;

	private ToolStripMenuItem ارسالمرتجعالبيعToolStripMenuItem;

	private ToolStripMenuItem الفواتيرالمرسلةToolStripMenuItem;

	private ToolStripMenuItem الافرعToolStripMenuItem;

	private ToolStripDropDownButton toolStripDropDownButton5;

	private ToolStripMenuItem قائمةالعملاءToolStripMenuItem;

	private ToolStripDropDownButton toolStripDropDownButton6;

	private ToolStripMenuItem قائمةالموردينToolStripMenuItem;

	private ToolStrip toolStrip2;

	private ToolStripLabel toolStripLabel1;

	private ToolStripMenuItem تحميلالفواتيرToolStripMenuItem;

	private ToolStripMenuItem مرتجعبيعمرسلToolStripMenuItem;

	private ToolStripMenuItem رفعالاصنافاكسلToolStripMenuItem;

	private ToolStripMenuItem الاصنافToolStripMenuItem;

	public frmMain()
	{
		_cmd = new Cmd<Store>();
		unitOfWork = new UnitOfWork();
		InitializeComponent();
	}

	private void toolStripDropDownButton1_Click(object sender, EventArgs e)
	{
	}

	private void frmMain_Load(object sender, EventArgs e)
	{
		this.toolStripDropDownButton3.Visible = false;
		this.toolStripDropDownButton6.Visible = false;
		this.تعريفالمخازنToolStripMenuItem.Visible = false;
		Info.CurrenBranch = _unitOfWork.Branchs.GetAll().FirstOrDefault();
		Info._setting = _unitOfWork.Settings.GetAll().ToList().FirstOrDefault();
		Info._Company = _unitOfWork.Companies.GetAll().ToList().FirstOrDefault();
	}

	private void الأصنافToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (e.ClickedItem.Tag == null)
		{
		}
	}

	private void تعريفالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmStoresAdd frmStoresAdd2 = new frmStoresAdd();
		frmStoresAdd2.ShowDialog();
	}

	private void مجموعاتالاصنافToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		frmCategories frmCategories2 = new frmCategories();
		frmCategories2.ShowDialog();
	}

	private void الوحداتToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		frmUnitsAdd frmUnitsAdd2 = new frmUnitsAdd();
		frmUnitsAdd2.ShowDialog();
	}

	private void الافرعToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmBranchesAdd frmBranchesAdd2 = new frmBranchesAdd();
		frmBranchesAdd2.ShowDialog();
	}

	private void فاتورةبيعToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoice frmInvoice2 = new frmInvoice(OrderType.Sale);
		frmInvoice2.ShowDialog();
	}

	private void مرتجعبيعToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoice frmInvoice2 = new frmInvoice(OrderType.SaleReturn);
		frmInvoice2.ShowDialog();
	}

	private void فاتورةشراءToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoice frmInvoice2 = new frmInvoice(OrderType.Purcahse);
		frmInvoice2.ShowDialog();
	}

	private void مرتجعشراءToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoice frmInvoice2 = new frmInvoice(OrderType.PurchaseReturn);
		frmInvoice2.ShowDialog();
	}

	private void قائمةالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmAccounts frmAccounts2 = new frmAccounts(AccountType.Customer);
		frmAccounts2.ShowDialog();
	}

	private void gfrgToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmCompanies frmCompanies2 = new frmCompanies();
		frmCompanies2.ShowDialog();
	}

	private void قائمةالموردينToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmAccounts frmAccounts2 = new frmAccounts(AccountType.supplier);
		frmAccounts2.ShowDialog();
	}

	private void toolStripDropDownButton5_Click(object sender, EventArgs e)
	{
	}

	private void metroTile1_Click(object sender, EventArgs e)
	{
	}

	private void الاعداداتToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmSetting frmSetting2 = new frmSetting();
		frmSetting2.ShowDialog();
	}

	private void ارسالفواتيرالبيعToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.Sale, OrderEinvSend.NotSent, EinvMoode: true);
		frmInvoices2.ShowDialog();
	}

	private void ارسالمرتجعالبيعToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.SaleReturn, OrderEinvSend.NotSent, EinvMoode: true);
		frmInvoices2.ShowDialog();
	}

	private void تحميلالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DonloadInvoices donloadInvoices = new DonloadInvoices();
		donloadInvoices.ShowDialog();
	}

	private void الفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.Sale);
		frmInvoices2.ShowDialog();
	}

	private void فواتيرالشراءToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.Purcahse);
		frmInvoices2.ShowDialog();
	}

	private void الفواتيرالمرسلةToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.Sale, OrderEinvSend.Sent, EinvMoode: true);
		frmInvoices2.ShowDialog();
	}

	private void مرتجعبيعمرسلToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmInvoices frmInvoices2 = new frmInvoices(OrderType.SaleReturn, OrderEinvSend.Sent, EinvMoode: true);
		frmInvoices2.ShowDialog();
	}

	private void رفعالاصنافاكسلToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Upload upload = new Upload();
		upload.ShowDialog();
	}

	private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		switch (MessageBox.Show("سيتم إغلاق البرنامج هل أنت متأكد؟", "خروج", MessageBoxButtons.YesNo))
		{
		case DialogResult.Yes:
			Application.Exit();
			break;
		case DialogResult.No:
			e.Cancel = true;
			break;
		}
	}

	private void الاصنافToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmProductsAdd frmProductsAdd2 = new frmProductsAdd();
		frmProductsAdd2.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(E_Invoice.Desktop.Forms.frmMain));
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
		this.gfrgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الاعداداتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.تعريفالمخازنToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.مجموعاتالاصنافToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الاصنافToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الوحداتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الافرعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.رفعالاصنافاكسلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
		this.فاتورةبيعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.مرتجعبيعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الفواتيرToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
		this.فاتورةشراءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.مرتجعشراءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.فواتيرالشراءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
		this.ارسالفواتيرالبيعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ارسالمرتجعالبيعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الفواتيرالمرسلةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.مرتجعبيعمرسلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.تحميلالفواتيرToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripDropDownButton5 = new System.Windows.Forms.ToolStripDropDownButton();
		this.قائمةالعملاءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripDropDownButton6 = new System.Windows.Forms.ToolStripDropDownButton();
		this.قائمةالموردينToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStrip2 = new System.Windows.Forms.ToolStrip();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.toolStrip1.SuspendLayout();
		this.toolStrip2.SuspendLayout();
		base.SuspendLayout();
		this.toolStrip1.AutoSize = false;
		this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(121, 134, 203);
		this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.toolStripDropDownButton1, this.toolStripDropDownButton2, this.toolStripDropDownButton3, this.toolStripDropDownButton4, this.toolStripDropDownButton5, this.toolStripDropDownButton6 });
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(1228, 86);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
		this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.gfrgToolStripMenuItem, this.الاعداداتToolStripMenuItem, this.تعريفالمخازنToolStripMenuItem, this.مجموعاتالاصنافToolStripMenuItem1, this.الاصنافToolStripMenuItem, this.الوحداتToolStripMenuItem1, this.الافرعToolStripMenuItem, this.رفعالاصنافاكسلToolStripMenuItem });
		this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
		this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
		this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 83);
		this.toolStripDropDownButton1.Text = "ملف";
		this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.toolStripDropDownButton1.Click += new System.EventHandler(toolStripDropDownButton1_Click);
		this.gfrgToolStripMenuItem.BackColor = System.Drawing.Color.White;
		this.gfrgToolStripMenuItem.Name = "gfrgToolStripMenuItem";
		this.gfrgToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.gfrgToolStripMenuItem.Text = "البيانات الاساسية";
		this.gfrgToolStripMenuItem.Click += new System.EventHandler(gfrgToolStripMenuItem_Click);
		this.الاعداداتToolStripMenuItem.Name = "الاعداداتToolStripMenuItem";
		this.الاعداداتToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.الاعداداتToolStripMenuItem.Text = "الاعدادات";
		this.الاعداداتToolStripMenuItem.Click += new System.EventHandler(الاعداداتToolStripMenuItem_Click);
		this.تعريفالمخازنToolStripMenuItem.Name = "تعريفالمخازنToolStripMenuItem";
		this.تعريفالمخازنToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.تعريفالمخازنToolStripMenuItem.Text = "تعريف المخازن";
		this.تعريفالمخازنToolStripMenuItem.Click += new System.EventHandler(تعريفالمخازنToolStripMenuItem_Click);
		this.مجموعاتالاصنافToolStripMenuItem1.Name = "مجموعاتالاصنافToolStripMenuItem1";
		this.مجموعاتالاصنافToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
		this.مجموعاتالاصنافToolStripMenuItem1.Text = "مجموعات الاصناف";
		this.مجموعاتالاصنافToolStripMenuItem1.Click += new System.EventHandler(مجموعاتالاصنافToolStripMenuItem1_Click);
		this.الاصنافToolStripMenuItem.Name = "الاصنافToolStripMenuItem";
		this.الاصنافToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.الاصنافToolStripMenuItem.Text = "الاصناف";
		this.الاصنافToolStripMenuItem.Click += new System.EventHandler(الاصنافToolStripMenuItem_Click);
		this.الوحداتToolStripMenuItem1.Name = "الوحداتToolStripMenuItem1";
		this.الوحداتToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
		this.الوحداتToolStripMenuItem1.Text = "الوحدات";
		this.الوحداتToolStripMenuItem1.Click += new System.EventHandler(الوحداتToolStripMenuItem1_Click);
		this.الافرعToolStripMenuItem.Name = "الافرعToolStripMenuItem";
		this.الافرعToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.الافرعToolStripMenuItem.Text = "الافرع";
		this.الافرعToolStripMenuItem.Click += new System.EventHandler(الافرعToolStripMenuItem_Click);
		this.رفعالاصنافاكسلToolStripMenuItem.Name = "رفعالاصنافاكسلToolStripMenuItem";
		this.رفعالاصنافاكسلToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
		this.رفعالاصنافاكسلToolStripMenuItem.Text = "رفع الاصناف اكسل";
		this.رفعالاصنافاكسلToolStripMenuItem.Click += new System.EventHandler(رفعالاصنافاكسلToolStripMenuItem_Click);
		this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.فاتورةبيعToolStripMenuItem, this.مرتجعبيعToolStripMenuItem, this.الفواتيرToolStripMenuItem });
		this.toolStripDropDownButton2.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton2.Image = E_Invoice.Desktop.Properties.Resources.icons8_sales_64__2_;
		this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
		this.toolStripDropDownButton2.Size = new System.Drawing.Size(103, 83);
		this.toolStripDropDownButton2.Text = "المبيعات";
		this.toolStripDropDownButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.فاتورةبيعToolStripMenuItem.Name = "فاتورةبيعToolStripMenuItem";
		this.فاتورةبيعToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		this.فاتورةبيعToolStripMenuItem.Text = "فاتورة بيع";
		this.فاتورةبيعToolStripMenuItem.Click += new System.EventHandler(فاتورةبيعToolStripMenuItem_Click);
		this.مرتجعبيعToolStripMenuItem.Name = "مرتجعبيعToolStripMenuItem";
		this.مرتجعبيعToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		this.مرتجعبيعToolStripMenuItem.Text = "مرتجع بيع";
		this.مرتجعبيعToolStripMenuItem.Click += new System.EventHandler(مرتجعبيعToolStripMenuItem_Click);
		this.الفواتيرToolStripMenuItem.Name = "الفواتيرToolStripMenuItem";
		this.الفواتيرToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		this.الفواتيرToolStripMenuItem.Text = "فواتير البيع";
		this.الفواتيرToolStripMenuItem.Click += new System.EventHandler(الفواتيرToolStripMenuItem_Click);
		this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.فاتورةشراءToolStripMenuItem, this.مرتجعشراءToolStripMenuItem, this.فواتيرالشراءToolStripMenuItem });
		this.toolStripDropDownButton3.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton3.Image = E_Invoice.Desktop.Properties.Resources.icons8_sales_64__1_;
		this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
		this.toolStripDropDownButton3.Size = new System.Drawing.Size(114, 83);
		this.toolStripDropDownButton3.Text = "المشتريات";
		this.toolStripDropDownButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.toolStripDropDownButton3.Visible = false;
		this.فاتورةشراءToolStripMenuItem.Name = "فاتورةشراءToolStripMenuItem";
		this.فاتورةشراءToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.فاتورةشراءToolStripMenuItem.Text = "فاتورة شراء";
		this.فاتورةشراءToolStripMenuItem.Click += new System.EventHandler(فاتورةشراءToolStripMenuItem_Click);
		this.مرتجعشراءToolStripMenuItem.Name = "مرتجعشراءToolStripMenuItem";
		this.مرتجعشراءToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.مرتجعشراءToolStripMenuItem.Text = "مرتجع شراء";
		this.مرتجعشراءToolStripMenuItem.Click += new System.EventHandler(مرتجعشراءToolStripMenuItem_Click);
		this.فواتيرالشراءToolStripMenuItem.Name = "فواتيرالشراءToolStripMenuItem";
		this.فواتيرالشراءToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.فواتيرالشراءToolStripMenuItem.Text = "فواتير الشراء";
		this.فواتيرالشراءToolStripMenuItem.Click += new System.EventHandler(فواتيرالشراءToolStripMenuItem_Click);
		this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.ارسالفواتيرالبيعToolStripMenuItem, this.ارسالمرتجعالبيعToolStripMenuItem, this.الفواتيرالمرسلةToolStripMenuItem, this.مرتجعبيعمرسلToolStripMenuItem, this.تحميلالفواتيرToolStripMenuItem });
		this.toolStripDropDownButton4.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton4.Image = E_Invoice.Desktop.Properties.Resources.logo;
		this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
		this.toolStripDropDownButton4.Size = new System.Drawing.Size(165, 83);
		this.toolStripDropDownButton4.Text = "الفاتورة الالكترونية";
		this.toolStripDropDownButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.ارسالفواتيرالبيعToolStripMenuItem.Name = "ارسالفواتيرالبيعToolStripMenuItem";
		this.ارسالفواتيرالبيعToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
		this.ارسالفواتيرالبيعToolStripMenuItem.Text = "ارسال فواتير البيع";
		this.ارسالفواتيرالبيعToolStripMenuItem.Click += new System.EventHandler(ارسالفواتيرالبيعToolStripMenuItem_Click);
		this.ارسالمرتجعالبيعToolStripMenuItem.Name = "ارسالمرتجعالبيعToolStripMenuItem";
		this.ارسالمرتجعالبيعToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
		this.ارسالمرتجعالبيعToolStripMenuItem.Text = "ارسال مرتجع البيع";
		this.ارسالمرتجعالبيعToolStripMenuItem.Click += new System.EventHandler(ارسالمرتجعالبيعToolStripMenuItem_Click);
		this.الفواتيرالمرسلةToolStripMenuItem.Name = "الفواتيرالمرسلةToolStripMenuItem";
		this.الفواتيرالمرسلةToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
		this.الفواتيرالمرسلةToolStripMenuItem.Text = "الفواتير المرسلة";
		this.الفواتيرالمرسلةToolStripMenuItem.Click += new System.EventHandler(الفواتيرالمرسلةToolStripMenuItem_Click);
		this.مرتجعبيعمرسلToolStripMenuItem.Name = "مرتجعبيعمرسلToolStripMenuItem";
		this.مرتجعبيعمرسلToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
		this.مرتجعبيعمرسلToolStripMenuItem.Text = "مرتجع بيع مرسل";
		this.مرتجعبيعمرسلToolStripMenuItem.Click += new System.EventHandler(مرتجعبيعمرسلToolStripMenuItem_Click);
		this.تحميلالفواتيرToolStripMenuItem.Name = "تحميلالفواتيرToolStripMenuItem";
		this.تحميلالفواتيرToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
		this.تحميلالفواتيرToolStripMenuItem.Text = "تحميل الفواتير";
		this.تحميلالفواتيرToolStripMenuItem.Click += new System.EventHandler(تحميلالفواتيرToolStripMenuItem_Click);
		this.toolStripDropDownButton5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.قائمةالعملاءToolStripMenuItem });
		this.toolStripDropDownButton5.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton5.Image = E_Invoice.Desktop.Properties.Resources.icons8_customer_64;
		this.toolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton5.Name = "toolStripDropDownButton5";
		this.toolStripDropDownButton5.Size = new System.Drawing.Size(93, 83);
		this.toolStripDropDownButton5.Text = "العملاء";
		this.toolStripDropDownButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.toolStripDropDownButton5.Click += new System.EventHandler(toolStripDropDownButton5_Click);
		this.قائمةالعملاءToolStripMenuItem.Name = "قائمةالعملاءToolStripMenuItem";
		this.قائمةالعملاءToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
		this.قائمةالعملاءToolStripMenuItem.Text = "قائمة العملاء";
		this.قائمةالعملاءToolStripMenuItem.Click += new System.EventHandler(قائمةالعملاءToolStripMenuItem_Click);
		this.toolStripDropDownButton6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.قائمةالموردينToolStripMenuItem });
		this.toolStripDropDownButton6.ForeColor = System.Drawing.Color.Snow;
		this.toolStripDropDownButton6.Image = E_Invoice.Desktop.Properties.Resources.icons8_supplier_64;
		this.toolStripDropDownButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton6.Name = "toolStripDropDownButton6";
		this.toolStripDropDownButton6.Size = new System.Drawing.Size(106, 83);
		this.toolStripDropDownButton6.Text = "الموردون";
		this.toolStripDropDownButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.toolStripDropDownButton6.Visible = false;
		this.قائمةالموردينToolStripMenuItem.Name = "قائمةالموردينToolStripMenuItem";
		this.قائمةالموردينToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
		this.قائمةالموردينToolStripMenuItem.Text = "قائمة الموردين";
		this.قائمةالموردينToolStripMenuItem.Click += new System.EventHandler(قائمةالموردينToolStripMenuItem_Click);
		this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripLabel1 });
		this.toolStrip2.Location = new System.Drawing.Point(0, 584);
		this.toolStrip2.Name = "toolStrip2";
		this.toolStrip2.Size = new System.Drawing.Size(1228, 25);
		this.toolStrip2.TabIndex = 1;
		this.toolStrip2.Text = "toolStrip2";
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(10, 22);
		this.toolStripLabel1.Text = " ";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImage = E_Invoice.Desktop.Properties.Resources.siddhesh_mangela_CXXQfm5YeTk_unsplash;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		base.ClientSize = new System.Drawing.Size(1228, 609);
		base.Controls.Add(this.toolStrip2);
		base.Controls.Add(this.toolStrip1);
		base.Name = "frmMain";
		this.Text = "";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmMain_FormClosing);
		base.Load += new System.EventHandler(frmMain_Load);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.toolStrip2.ResumeLayout(false);
		this.toolStrip2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
