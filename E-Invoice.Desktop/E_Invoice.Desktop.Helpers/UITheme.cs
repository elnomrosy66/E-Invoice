using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace E_Invoice.Desktop.Helpers;

public static class UITheme
{
	// Modern Slate & Material Palette
	public static readonly Color FormBg = Color.FromArgb(248, 250, 252);          // Slate 50
	public static readonly Color CardBg = Color.White;
	public static readonly Color BorderColor = Color.FromArgb(226, 232, 240);     // Slate 200
	public static readonly Color HeaderBg = Color.FromArgb(30, 41, 59);           // Slate 800
	public static readonly Color HeaderAccent = Color.FromArgb(15, 23, 42);       // Slate 900
	public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);        // Slate 900
	public static readonly Color TextSecondary = Color.FromArgb(100, 116, 139);   // Slate 500
	public static readonly Color TextMuted = Color.FromArgb(148, 163, 184);       // Slate 400

	// Semantic Action Colors
	public static readonly Color Primary = Color.FromArgb(37, 99, 235);          // Blue 600
	public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);     // Blue 700
	public static readonly Color Success = Color.FromArgb(16, 185, 129);         // Emerald 500
	public static readonly Color SuccessDark = Color.FromArgb(5, 150, 105);      // Emerald 600
	public static readonly Color Warning = Color.FromArgb(245, 158, 11);         // Amber 500
	public static readonly Color WarningDark = Color.FromArgb(217, 119, 6);      // Amber 600
	public static readonly Color Danger = Color.FromArgb(239, 68, 68);           // Red 500
	public static readonly Color DangerDark = Color.FromArgb(220, 38, 38);       // Red 600
	public static readonly Color NeutralBtn = Color.FromArgb(241, 245, 249);     // Slate 100
	public static readonly Color NeutralBtnHover = Color.FromArgb(226, 232, 240);// Slate 200

	// DataGridView Colors
	public static readonly Color GridHeaderBg = Color.FromArgb(30, 41, 59);
	public static readonly Color GridHeaderFg = Color.White;
	public static readonly Color GridRowBg = Color.White;
	public static readonly Color GridRowAlt = Color.FromArgb(248, 250, 252);
	public static readonly Color GridRowSelected = Color.FromArgb(219, 234, 254);
	public static readonly Color GridRowSelectedFg = Color.FromArgb(30, 58, 138);
	public static readonly Color GridLine = Color.FromArgb(226, 232, 240);

	// Fonts
	public static readonly Font HeaderFont = new Font("Segoe UI", 12f, FontStyle.Bold);
	public static readonly Font SubHeaderFont = new Font("Segoe UI", 10.5f, FontStyle.Bold);
	public static readonly Font BaseFont = new Font("Segoe UI", 9.75f, FontStyle.Regular);
	public static readonly Font BoldFont = new Font("Segoe UI", 9.75f, FontStyle.Bold);
	public static readonly Font LargeNumberFont = new Font("Segoe UI", 15f, FontStyle.Bold);
	public static readonly Font MediumNumberFont = new Font("Segoe UI", 12f, FontStyle.Bold);

	public static void ApplyTheme(Form form)
	{
		form.BackColor = FormBg;
		form.Font = BaseFont;
		form.RightToLeft = RightToLeft.Yes;
		form.RightToLeftLayout = true;
	}

	public static void ApplyGridTheme(DataGridView dgv)
	{
		if (dgv == null) return;

		// Enable Double Buffering to eliminate grid flickering
		EnableDoubleBuffering(dgv);

		dgv.EnableHeadersVisualStyles = false;
		dgv.BackgroundColor = Color.White;
		dgv.BorderStyle = BorderStyle.None;
		dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
		dgv.GridColor = GridLine;
		dgv.RowHeadersVisible = false;
		dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgv.MultiSelect = false;
		dgv.Font = BaseFont;

		// Header Styling
		dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBg;
		dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderFg;
		dgv.ColumnHeadersDefaultCellStyle.Font = BoldFont;
		dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		dgv.ColumnHeadersHeight = 38;
		dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

		// Row Styling
		dgv.RowTemplate.Height = 34;
		dgv.DefaultCellStyle.BackColor = GridRowBg;
		dgv.DefaultCellStyle.ForeColor = TextPrimary;
		dgv.DefaultCellStyle.SelectionBackColor = GridRowSelected;
		dgv.DefaultCellStyle.SelectionForeColor = GridRowSelectedFg;
		dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		dgv.AlternatingRowsDefaultCellStyle.BackColor = GridRowAlt;
	}

	public static void ApplyButton(Button btn, Color bg, Color fg, Color? hover = null)
	{
		if (btn == null) return;

		btn.FlatStyle = FlatStyle.Flat;
		btn.FlatAppearance.BorderSize = 0;
		btn.BackColor = bg;
		btn.ForeColor = fg;
		btn.Font = BoldFont;
		btn.Cursor = Cursors.Hand;
		if (hover.HasValue)
		{
			btn.FlatAppearance.MouseOverBackColor = hover.Value;
		}
	}

	public static void ApplyPrimaryButton(Button btn) => ApplyButton(btn, Primary, Color.White, PrimaryHover);
	public static void ApplySuccessButton(Button btn) => ApplyButton(btn, SuccessDark, Color.White, Success);
	public static void ApplyWarningButton(Button btn) => ApplyButton(btn, WarningDark, Color.White, Warning);
	public static void ApplyDangerButton(Button btn) => ApplyButton(btn, DangerDark, Color.White, Danger);
	public static void ApplyNeutralButton(Button btn) => ApplyButton(btn, NeutralBtn, TextPrimary, NeutralBtnHover);

	private static void EnableDoubleBuffering(Control control)
	{
		try
		{
			typeof(Control).InvokeMember(
				"DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null,
				control,
				new object[] { true }
			);
		}
		catch { }
	}
}
