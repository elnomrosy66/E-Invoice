using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using E_Invoice.Desktop.Controls;
using E_Invoice.Domain.Models;
using E_Invoice.Domain.Models.EInvoiceModels;
using E_Invoice.Domain.Services;

namespace E_Invoice.Desktop.Forms;

public class DonloadInvoices : Master
{
	private IContainer components = null;

	private DataGridView DGVItems;

	private DateTimePickerEx DTFrom;

	private DateTimePickerEx DTTo;

	private CheckBoxEx I;

	private CheckBoxEx C;

	private CheckBoxEx D;

	private CheckBoxEx valid;

	private CheckBoxEx invalid;

	private CheckBoxEx checkBoxEx6;

	private CheckBoxEx checkBoxEx7;

	private btnEx btnEx1;

	private GroupBox grpDocType;

	private GroupBox grpDocStatus;

	private Label label1;

	private Label label2;

	private DataGridViewTextBoxColumn PackageId;

	private DataGridViewTextBoxColumn RequestDate;

	private DataGridViewTextBoxColumn Format;

	private DataGridViewTextBoxColumn Status;

	private DataGridViewTextBoxColumn DateFrom;

	private DataGridViewTextBoxColumn DateTo;

	private DataGridViewButtonColumn Download;

	public DonloadInvoices()
	{
		_tax = new Tax();
		InitializeComponent();
	}

	private void DonloadInvoices_Load(object sender, EventArgs e)
	{
		PackageRequests packageRequests = _tax.GetPackageRequests();
		if (packageRequests.result != null)
		{
			foreach (Result item in packageRequests.result)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)DGVItems.RowTemplate.Clone();
				dataGridViewRow.CreateCells(DGVItems, item.packageId, item.submissionDate.ToShortDateString(), (item.format == 3) ? "Json" : "CSV", (item.status == 2) ? "جاهز" : ((item.status == 1) ? "غير جاهز" : ((item.status == 4) ? "محذوف" : "خطاء")), item.queryParams.dateFrom.ToShortDateString(), item.queryParams.dateTo.ToShortDateString());
				DGVItems.Rows.Add(dataGridViewRow);
			}
			return;
		}
		Mess.Warning("لا يوجد أي طلبات مسبقة");
	}

	private void btnEx1_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (Control control3 in grpDocStatus.Controls)
		{
			if (control3 is CheckBoxEx)
			{
				CheckBoxEx checkBoxEx = control3 as CheckBoxEx;
				if (checkBoxEx.CheckState == CheckState.Checked)
				{
					list.Add(control3.Tag.ToString());
				}
			}
		}
		foreach (Control control4 in grpDocType.Controls)
		{
			if (control4 is CheckBoxEx)
			{
				CheckBoxEx checkBoxEx2 = control4 as CheckBoxEx;
				if (checkBoxEx2.CheckState == CheckState.Checked)
				{
					list2.Add(control4.Tag.ToString());
				}
			}
		}
		PackageRequest packageRequest = new PackageRequest
		{
			format = "JSON",
			type = "Summary",
			queryParameters = new QueryParameters
			{
				dateFrom = DTFrom.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
				dateTo = DTTo.Value.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
				statuses = list,
				documentTypeNames = list2
			}
		};
		PackageRequestResponse packageRequestResponse = _tax.RequestPakage(packageRequest);
		if (packageRequestResponse.requestId != null)
		{
			Mess.Save("رقم الطلب : " + packageRequestResponse.requestId);
		}
		else
		{
			Mess.Warning(packageRequestResponse.error.details[0].message);
		}
	}

	private void DGVItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
		if (!(dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn) || e.RowIndex < 0)
		{
			return;
		}
		DownloadPackageResponse downloadPackageResponse = new DownloadPackageResponse();
		string text = DGVItems.Rows[e.RowIndex].Cells[0].Value.ToString();
		downloadPackageResponse = _tax.DownloadPackage(text);
		if (downloadPackageResponse.error != null)
		{
			Mess.Warning(downloadPackageResponse.error.details[0].message);
			return;
		}
		using SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
		saveFileDialog.FileName = text;
		saveFileDialog.RestoreDirectory = true;
		saveFileDialog.Title = "Save an Image File";
		saveFileDialog.DefaultExt = "Zip";
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			File.WriteAllBytes(saveFileDialog.FileName, downloadPackageResponse.zip);
			Mess.Save();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVItems = new System.Windows.Forms.DataGridView();
		this.PackageId = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DateFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DateTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
		this.DTFrom = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.DTTo = new E_Invoice.Desktop.Controls.DateTimePickerEx();
		this.I = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.C = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.D = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.valid = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.invalid = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.checkBoxEx6 = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.checkBoxEx7 = new E_Invoice.Desktop.Controls.CheckBoxEx();
		this.btnEx1 = new E_Invoice.Desktop.Controls.btnEx();
		this.grpDocType = new System.Windows.Forms.GroupBox();
		this.grpDocStatus = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.DGVItems).BeginInit();
		this.grpDocType.SuspendLayout();
		this.grpDocStatus.SuspendLayout();
		base.SuspendLayout();
		this.DGVItems.AllowUserToAddRows = false;
		this.DGVItems.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.DGVItems.BackgroundColor = System.Drawing.Color.White;
		this.DGVItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 10.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVItems.Columns.AddRange(this.PackageId, this.RequestDate, this.Format, this.Status, this.DateFrom, this.DateTo, this.Download);
		this.DGVItems.Location = new System.Drawing.Point(0, 181);
		this.DGVItems.Name = "DGVItems";
		this.DGVItems.ReadOnly = true;
		this.DGVItems.RowHeadersWidth = 51;
		this.DGVItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVItems.Size = new System.Drawing.Size(910, 226);
		this.DGVItems.TabIndex = 2;
		this.DGVItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVItems_CellContentClick);
		this.PackageId.HeaderText = "Id";
		this.PackageId.MinimumWidth = 6;
		this.PackageId.Name = "PackageId";
		this.PackageId.ReadOnly = true;
		this.RequestDate.HeaderText = "تاريخ الطلب";
		this.RequestDate.MinimumWidth = 6;
		this.RequestDate.Name = "RequestDate";
		this.RequestDate.ReadOnly = true;
		this.Format.HeaderText = "الصيغة";
		this.Format.MinimumWidth = 6;
		this.Format.Name = "Format";
		this.Format.ReadOnly = true;
		this.Status.HeaderText = "الحالة";
		this.Status.MinimumWidth = 6;
		this.Status.Name = "Status";
		this.Status.ReadOnly = true;
		this.DateFrom.HeaderText = "من تاريخ";
		this.DateFrom.MinimumWidth = 6;
		this.DateFrom.Name = "DateFrom";
		this.DateFrom.ReadOnly = true;
		this.DateTo.HeaderText = "إلي تاريخ";
		this.DateTo.MinimumWidth = 6;
		this.DateTo.Name = "DateTo";
		this.DateTo.ReadOnly = true;
		this.Download.HeaderText = "تحميل";
		this.Download.MinimumWidth = 6;
		this.Download.Name = "Download";
		this.Download.ReadOnly = true;
		this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Download.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.Download.Text = "تحميل الحزمة";
		this.Download.UseColumnTextForButtonValue = true;
		this.DTFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.DTFrom.Location = new System.Drawing.Point(50, 48);
		this.DTFrom.Name = "DTFrom";
		this.DTFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.DTFrom.Size = new System.Drawing.Size(108, 25);
		this.DTFrom.TabIndex = 39;
		this.DTTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.DTTo.Location = new System.Drawing.Point(50, 86);
		this.DTTo.Name = "DTTo";
		this.DTTo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.DTTo.Size = new System.Drawing.Size(108, 25);
		this.DTTo.TabIndex = 39;
		this.I.AutoSize = true;
		this.I.Checked = true;
		this.I.CheckState = System.Windows.Forms.CheckState.Checked;
		this.I.Location = new System.Drawing.Point(4, 19);
		this.I.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.I.Name = "I";
		this.I.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.I.Size = new System.Drawing.Size(100, 22);
		this.I.TabIndex = 40;
		this.I.Tag = "I";
		this.I.Text = "فـاتـــــــــورة";
		this.I.UseVisualStyleBackColor = true;
		this.C.AutoSize = true;
		this.C.Location = new System.Drawing.Point(4, 43);
		this.C.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.C.Name = "C";
		this.C.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.C.Size = new System.Drawing.Size(102, 22);
		this.C.TabIndex = 40;
		this.C.Tag = "C";
		this.C.Text = "إشعار اضافة";
		this.C.UseVisualStyleBackColor = true;
		this.D.AutoSize = true;
		this.D.Location = new System.Drawing.Point(4, 65);
		this.D.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.D.Name = "D";
		this.D.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.D.Size = new System.Drawing.Size(104, 22);
		this.D.TabIndex = 40;
		this.D.Tag = "D";
		this.D.Text = "إشعـار خصم";
		this.D.UseVisualStyleBackColor = true;
		this.valid.AutoSize = true;
		this.valid.Checked = true;
		this.valid.CheckState = System.Windows.Forms.CheckState.Checked;
		this.valid.Location = new System.Drawing.Point(4, 19);
		this.valid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.valid.Name = "valid";
		this.valid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.valid.Size = new System.Drawing.Size(93, 22);
		this.valid.TabIndex = 40;
		this.valid.Tag = "Valid";
		this.valid.Text = "صــحـيــــح";
		this.valid.UseVisualStyleBackColor = true;
		this.invalid.AutoSize = true;
		this.invalid.Location = new System.Drawing.Point(4, 42);
		this.invalid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.invalid.Name = "invalid";
		this.invalid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.invalid.Size = new System.Drawing.Size(90, 22);
		this.invalid.TabIndex = 40;
		this.invalid.Tag = "Invalid";
		this.invalid.Text = "غير صحيح";
		this.invalid.UseVisualStyleBackColor = true;
		this.checkBoxEx6.AutoSize = true;
		this.checkBoxEx6.Location = new System.Drawing.Point(4, 65);
		this.checkBoxEx6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.checkBoxEx6.Name = "checkBoxEx6";
		this.checkBoxEx6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.checkBoxEx6.Size = new System.Drawing.Size(102, 22);
		this.checkBoxEx6.TabIndex = 40;
		this.checkBoxEx6.Tag = "Cancelled";
		this.checkBoxEx6.Text = "ملغــــــــــي";
		this.checkBoxEx6.UseVisualStyleBackColor = true;
		this.checkBoxEx7.AutoSize = true;
		this.checkBoxEx7.Location = new System.Drawing.Point(4, 88);
		this.checkBoxEx7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.checkBoxEx7.Name = "checkBoxEx7";
		this.checkBoxEx7.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.checkBoxEx7.Size = new System.Drawing.Size(80, 22);
		this.checkBoxEx7.TabIndex = 40;
		this.checkBoxEx7.Tag = "Rejected";
		this.checkBoxEx7.Text = "rejected";
		this.checkBoxEx7.UseVisualStyleBackColor = true;
		this.btnEx1.BackColor = System.Drawing.Color.Maroon;
		this.btnEx1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.btnEx1.Location = new System.Drawing.Point(754, 66);
		this.btnEx1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.btnEx1.Name = "btnEx1";
		this.btnEx1.Size = new System.Drawing.Size(100, 58);
		this.btnEx1.TabIndex = 41;
		this.btnEx1.Text = "طلب الحزمة";
		this.btnEx1.UseVisualStyleBackColor = false;
		this.btnEx1.Click += new System.EventHandler(btnEx1_Click);
		this.grpDocType.Controls.Add(this.D);
		this.grpDocType.Controls.Add(this.I);
		this.grpDocType.Controls.Add(this.C);
		this.grpDocType.Location = new System.Drawing.Point(177, 41);
		this.grpDocType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.grpDocType.Name = "grpDocType";
		this.grpDocType.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.grpDocType.Size = new System.Drawing.Size(103, 111);
		this.grpDocType.TabIndex = 42;
		this.grpDocType.TabStop = false;
		this.grpDocType.Text = "نوع المستند";
		this.grpDocStatus.Controls.Add(this.checkBoxEx7);
		this.grpDocStatus.Controls.Add(this.valid);
		this.grpDocStatus.Controls.Add(this.invalid);
		this.grpDocStatus.Controls.Add(this.checkBoxEx6);
		this.grpDocStatus.Location = new System.Drawing.Point(284, 42);
		this.grpDocStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.grpDocStatus.Name = "grpDocStatus";
		this.grpDocStatus.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
		this.grpDocStatus.Size = new System.Drawing.Size(107, 111);
		this.grpDocStatus.TabIndex = 43;
		this.grpDocStatus.TabStop = false;
		this.grpDocStatus.Text = "الحالة";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 50);
		this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 18);
		this.label1.TabIndex = 44;
		this.label1.Text = "من :";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 89);
		this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 18);
		this.label2.TabIndex = 44;
		this.label2.Text = "إلي :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(910, 407);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.grpDocStatus);
		base.Controls.Add(this.grpDocType);
		base.Controls.Add(this.btnEx1);
		base.Controls.Add(this.DTTo);
		base.Controls.Add(this.DTFrom);
		base.Controls.Add(this.DGVItems);
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.Name = "DonloadInvoices";
		this.Text = "تحميل الفواتير";
		base.Load += new System.EventHandler(DonloadInvoices_Load);
		base.Controls.SetChildIndex(this.DGVItems, 0);
		base.Controls.SetChildIndex(this.DTFrom, 0);
		base.Controls.SetChildIndex(this.DTTo, 0);
		base.Controls.SetChildIndex(this.btnEx1, 0);
		base.Controls.SetChildIndex(this.grpDocType, 0);
		base.Controls.SetChildIndex(this.grpDocStatus, 0);
		base.Controls.SetChildIndex(this.label1, 0);
		base.Controls.SetChildIndex(this.label2, 0);
		((System.ComponentModel.ISupportInitialize)this.DGVItems).EndInit();
		this.grpDocType.ResumeLayout(false);
		this.grpDocType.PerformLayout();
		this.grpDocStatus.ResumeLayout(false);
		this.grpDocStatus.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
