using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmEnterValue : frmBase
{
	private string labelText = "";

	private bool IsInt = false;

	private bool IsNumeric = false;

	public string Value = "";

	public DataTable dtDetails;

	private IContainer components = null;

	private UltraButton btnClose;

	private UltraButton btnSave;

	private UltraTextEditor txtvalue;

	private UltraLabel lblText;

	public UltraButton btnKeyboard;

	private Label lblHistory;

	public UltraGrid ULGData;

	public frmEnterValue()
	{
		InitializeComponent();
		base.Width = 312;
	}

	public frmEnterValue(string _labelText, bool _IsInt, bool _IsNumeric)
		: this()
	{
		labelText = _labelText;
		IsInt = _IsInt;
		IsNumeric = _IsNumeric;
	}

	public frmEnterValue(DataTable dthistory, string _labelText, bool _IsInt, bool _IsNumeric)
		: this()
	{
		labelText = _labelText;
		IsInt = _IsInt;
		IsNumeric = _IsNumeric;
		base.Width = 812;
		dtDetails = dthistory;
		InitGrid();
		((Control)(object)ULGData).Visible = (lblHistory.Visible = true);
	}

	public frmEnterValue(string _labelText, bool _IsInt, bool _IsNumeric, string oldValue)
		: this()
	{
		labelText = _labelText;
		IsInt = _IsInt;
		IsNumeric = _IsNumeric;
		((Control)(object)txtvalue).Text = oldValue;
	}

	public void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Unit"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Unit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Unit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if ((IsInt || IsNumeric) && (((Control)(object)txtvalue).Text == "" || decimal.Parse(((Control)(object)txtvalue).Text) <= 0m))
		{
			GlobalVariables.InformationMB.Show("لابد ان تكون القيمة أكبر من الصفر", "Value Must Exceed Zero");
			return;
		}
		if (((Control)(object)txtvalue).Text == "")
		{
			GlobalVariables.InformationMB.Show("  لابد من إدخال  " + labelText, "  Please Insert  " + labelText);
			return;
		}
		Value = ((Control)(object)txtvalue).Text;
		Close();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void frmEnterValue_Load(object sender, EventArgs e)
	{
		((Control)(object)lblText).Text = labelText;
	}

	private void txtvalue_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (IsInt)
		{
			GlobalFunctions.CheckForIntegers(sender, e);
		}
		else if (IsNumeric)
		{
			GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmEnterValue));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtvalue = new UltraTextEditor();
		this.lblText = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.lblHistory = new System.Windows.Forms.Label();
		this.ULGData = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((UltraButtonBase)this.btnSave).DialogResult = System.Windows.Forms.DialogResult.OK;
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtvalue, "txtvalue");
		((System.Windows.Forms.Control)(object)this.txtvalue).Name = "txtvalue";
		((System.Windows.Forms.Control)(object)this.txtvalue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtvalue_KeyPress);
		this.lblText.AutoEllipsis = false;
		resources.ApplyResources(this.lblText, "lblText");
		((System.Windows.Forms.Control)(object)this.lblText).Name = "lblText";
		((ControlBase)this.lblText).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblHistory, "lblHistory");
		this.lblHistory.Name = "lblHistory";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val2).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val3;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add(this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtvalue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblText);
		base.Name = "frmEnterValue";
		base.Load += new System.EventHandler(frmEnterValue_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblText, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtvalue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex(this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
