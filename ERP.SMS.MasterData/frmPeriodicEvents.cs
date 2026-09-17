using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using BusinessLayer.SMS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SMS.MasterData;

public class frmPeriodicEvents : frmGrid
{
	private DataTable dtSMSTemplates;

	private ValueList vlSMSTemplates = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtCode;

	private UltraLabel lblCode;

	private UltraComboEditor cboTemplate;

	private UltraLabel lblTemplate;

	private UltraDateTimeEditor dtpEventDate;

	private UltraLabel lblCreationDate;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpEventTime;

	private NumericUpDown numMonthsCount;

	private UltraLabel ultraLabel2;

	public frmPeriodicEvents()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SMS_PeriodicEvents";
		IDCol = "PeriodicEventID";
		dtpEventDate.MaskInput = "dd/mm";
		dtpEventTime.MaskInput = "hh:mm tt";
		dtSMSTemplates = Templates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTemplate, dtSMSTemplates, "TemplateID", "TemplateName");
		vlSMSTemplates.ValueListItems.Clear();
		for (int i = 0; i < dtSMSTemplates.Rows.Count; i++)
		{
			vlSMSTemplates.ValueListItems.Add((object)dtSMSTemplates.Rows[i]["TemplateID"].ToString(), dtSMSTemplates.Rows[i]["TemplateName"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTemplate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpEventDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpEventTime).ReadOnly = NavMode;
		numMonthsCount.ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtCode).Text = (Adding ? PeriodicEvents.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		cboTemplate.SelectedIndex = -1;
		numMonthsCount.Value = 1m;
		dtpEventDate.Value = GlobalFunctions.GetServerDateTimeNow();
		dtpEventTime.Value = GlobalFunctions.GetServerDateTimeNow();
		dtpEventDate.MaskInput = "dd/mm";
		dtpEventTime.MaskInput = "hh:mm tt";
	}

	public override void FillData()
	{
		dataTable = PeriodicEvents.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الحدث" : "Event Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم بالعربية" : "Arabic Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالإنجليزية" : "English Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PeriodicEventNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MonthsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الاشهر" : "Months Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MonthsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MonthsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الحدث" : "Event Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventDate"].MaskInput = "dd/mm";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventTime"].Header).Caption = (GlobalVariables.IsArabic ? "وقت الحدث" : "Event Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EventTime"].MaskInput = "hh:mm tt";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TemplateID"].Header).Caption = (GlobalVariables.IsArabic ? "النموذج" : "Template");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TemplateID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TemplateID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TemplateID"].ValueList = (IValueList)(object)vlSMSTemplates;
	}

	public override void AfterRowActivate()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventCode"].Value.ToString();
			((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventNameAr"].Value.ToString();
			((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventNameEn"].Value.ToString();
			((TextEditorControlBase)cboTemplate).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["TemplateID"].Value;
			dtpEventDate.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EventDate"].Value;
			dtpEventTime.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EventTime"].Value;
			numMonthsCount.Value = ((((UltraGridBase)ULGData).ActiveRow.Cells["MonthsCount"].Value != DBNull.Value) ? Convert.ToInt16(((UltraGridBase)ULGData).ActiveRow.Cells["MonthsCount"].Value) : 0);
			dtpEventDate.MaskInput = "dd/mm";
			dtpEventTime.MaskInput = "hh:mm tt";
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الحدث", "Please Insert Event No");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم الحدث بالعربية", "Please Enter Event In Arabic");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (dtpEventDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الحدث", "Please Enter Event Date");
			((Control)(object)dtpEventDate).Focus();
			return false;
		}
		if (dtpEventTime.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال وقت الحدث", "Please Enter Event Time");
			((Control)(object)dtpEventTime).Focus();
			return false;
		}
		if (numMonthsCount.Value == 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الأشهر", "Please Enter Months Count");
			numMonthsCount.Focus();
			return false;
		}
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select("PeriodicEventCode = " + ((Control)(object)txtCode).Text + (Updating ? (" And PeriodicEventID <> " + ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventID"].Value.ToString()) : ""), "").Length != 0)
		{
			GlobalVariables.InformationMB.Show("هذا الكود موجود من قبل", "This Code Is Already Exists");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select("PeriodicEventNameAr = '" + ((Control)(object)txtArabicName).Text + "'" + (Updating ? (" And PeriodicEventID <> " + ((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventID"].Value.ToString()) : ""), "").Length != 0)
		{
			GlobalVariables.InformationMB.Show("هذا الاسم موجود من قبل", "This Name Is Already Exists");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		PeriodicEvents.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text, (cboTemplate.SelectedIndex > -1) ? ((TextEditorControlBase)cboTemplate).Value.ToString() : "Null", numMonthsCount.Value.ToString(), dtpEventDate.Value.ToString(), dtpEventTime.Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		PeriodicEvents.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboTemplate.SelectedIndex > -1) ? ((TextEditorControlBase)cboTemplate).Value.ToString() : "Null", numMonthsCount.Value.ToString(), dtpEventDate.Value.ToString(), dtpEventTime.Value.ToString(), bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		PeriodicEvents.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["PeriodicEventID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void txtPeriodicEventCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SMS.MasterData.frmPeriodicEvents));
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
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.lblArabicName = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.cboTemplate = new UltraComboEditor();
		this.lblTemplate = new UltraLabel();
		this.dtpEventDate = new UltraDateTimeEditor();
		this.lblCreationDate = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.dtpEventTime = new UltraDateTimeEditor();
		this.numMonthsCount = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numMonthsCount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance14");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		resources.ApplyResources(this.cboTemplate, "cboTemplate");
		((System.Windows.Forms.Control)(object)this.cboTemplate).Name = "cboTemplate";
		((EditorButtonControlBase)this.cboTemplate).ReadOnly = true;
		resources.ApplyResources(this.lblTemplate, "lblTemplate");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance15");
		((ControlBase)this.lblTemplate).Appearance = (AppearanceBase)(object)val10;
		this.lblTemplate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTemplate).Name = "lblTemplate";
		((ControlBase)this.lblTemplate).WrapText = false;
		resources.ApplyResources(this.dtpEventDate, "dtpEventDate");
		((UltraWinEditorMaskedControlBase)this.dtpEventDate).AlwaysInEditMode = true;
		this.dtpEventDate.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		this.dtpEventDate.MaskInput = "dd/mm";
		((System.Windows.Forms.Control)(object)this.dtpEventDate).Name = "dtpEventDate";
		((EditorButtonControlBase)this.dtpEventDate).ReadOnly = true;
		resources.ApplyResources(this.lblCreationDate, "lblCreationDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance16");
		((ControlBase)this.lblCreationDate).Appearance = (AppearanceBase)(object)val11;
		this.lblCreationDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCreationDate).Name = "lblCreationDate";
		((ControlBase)this.lblCreationDate).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance17");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val12;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpEventTime, "dtpEventTime");
		((UltraWinEditorMaskedControlBase)this.dtpEventTime).AlwaysInEditMode = true;
		this.dtpEventTime.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		this.dtpEventTime.MaskInput = "{time}";
		((System.Windows.Forms.Control)(object)this.dtpEventTime).Name = "dtpEventTime";
		((EditorButtonControlBase)this.dtpEventTime).ReadOnly = true;
		resources.ApplyResources(this.numMonthsCount, "numMonthsCount");
		this.numMonthsCount.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.numMonthsCount.Name = "numMonthsCount";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance18");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val13;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.numMonthsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEventTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEventDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCreationDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTemplate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTemplate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmPeriodicEvents";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTemplate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTemplate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCreationDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEventDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEventTime, 0);
		base.Controls.SetChildIndex(this.numMonthsCount, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numMonthsCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
