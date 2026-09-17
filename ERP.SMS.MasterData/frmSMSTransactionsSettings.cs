using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.SMS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.SMS.MasterData;

public class frmSMSTransactionsSettings : frmBase
{
	private DataTable dtSMSTemplates;

	private DataTable dtSMSTransactionsSettings;

	private ValueList vlSMSTemplates = new ValueList();

	private IContainer components = null;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraPictureBox ultraPictureBox1;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton2;

	private UltraTextEditor ultraTextEditor1;

	private UltraTextEditor ultraTextEditor2;

	private UltraLabel ultraLabel6;

	private UltraButton ultraButton3;

	private UltraPictureBox ultraPictureBox2;

	private UltraLabel ultraLabel7;

	private UltraPictureBox ultraPictureBox3;

	private UltraButton ultraButton4;

	private UltraButton ultraButton5;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraTabPageControl ultraTabPageControl7;

	private UltraCheckEditor ultraCheckEditor1;

	private UltraGroupBox ultraGroupBox1;

	private UltraDateTimeEditor ultraDateTimeEditor1;

	private UltraLabel ultraLabel10;

	private NumericUpDown numericUpDown1;

	private UltraLabel ultraLabel11;

	private UltraGroupBox ultraGroupBox2;

	private UltraLabel ultraLabel12;

	private UltraCheckEditor ultraCheckEditor2;

	private UltraDateTimeEditor ultraDateTimeEditor2;

	private UltraDateTimeEditor ultraDateTimeEditor3;

	private UltraLabel ultraLabel13;

	private UltraGroupBox ultraGroupBox3;

	private RadioButton radioButton1;

	private RadioButton radioButton2;

	private RadioButton radioButton3;

	private UltraTabPageControl ultraTabPageControl8;

	public UltraGrid ultraGrid1;

	private UltraTabPageControl ultraTabPageControl9;

	public UltraGrid ultraGrid2;

	private UltraTabPageControl ultraTabPageControl10;

	public UltraGrid ultraGrid3;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabControl tcTransactionsSettings;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGTransactionsSettings;

	public UltraButton btnCancel;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraButton btnRefreshData;

	public frmSMSTransactionsSettings()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public void InitGridsSysOption()
	{
		((UltraGridBase)ULGTransactionsSettings).DataSource = dtSMSTransactionsSettings;
		GlobalFunctions.PrepareGrid(ULGTransactionsSettings);
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحركة بالعربية" : "Transaction Name Ar");
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameAr"].Width = (int)((double)((Control)(object)ULGTransactionsSettings).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحركة بالإنجليزية" : "Transaction Name En");
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TransactionNameEn"].Width = (int)((double)((Control)(object)ULGTransactionsSettings).Width * 0.3);
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TemplateID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TemplateID"].Header).Caption = (GlobalVariables.IsArabic ? "نموذج الرساله" : "Template");
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TemplateID"].Width = (int)((double)((Control)(object)ULGTransactionsSettings).Width * 0.2);
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["TemplateID"].ValueList = (IValueList)(object)vlSMSTemplates;
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["DelayUntilInMinutes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["DelayUntilInMinutes"].Header).Caption = (GlobalVariables.IsArabic ? "المهلة قبل الارسال/دقيقة" : "Delay Until In Minutes");
		((UltraGridBase)ULGTransactionsSettings).DisplayLayout.Bands[0].Columns["DelayUntilInMinutes"].Width = (int)((double)((Control)(object)ULGTransactionsSettings).Width * 0.2);
	}

	private void frmTransactioTypes_Load(object sender, EventArgs e)
	{
		FillData();
	}

	private void FillData()
	{
		dtSMSTransactionsSettings = TransactionsSettings.SelectForInstalledModules("-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtSMSTemplates = Templates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSMSTemplates.ValueListItems.Clear();
		for (int i = 0; i < dtSMSTemplates.Rows.Count; i++)
		{
			vlSMSTemplates.ValueListItems.Add((object)dtSMSTemplates.Rows[i]["TemplateID"].ToString(), dtSMSTemplates.Rows[i]["TemplateName"].ToString());
		}
		InitGridsSysOption();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		FillData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		UpdateData();
		FillData();
	}

	private void UpdateData()
	{
		TransactionsSettings.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGTransactionsSettings).DataSource, GlobalVariables.UserID, IsFromServer: true);
		GlobalVariables.InformationMB.Show("تــم حفظ البيانات بنجــــاح", "Updates were saved successfully");
		GlobalVariables.dtSystemOptions = TransactionsSettings.SelectForInstalledModules("-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	private void ULGTransactionsSettings_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGTransactionsSettings.ActiveCell.Column).Key == "TransactionNameAr" || ((KeyedSubObjectBase)ULGTransactionsSettings.ActiveCell.Column).Key == "TransactionNameEn")
		{
			((GridItemBase)((UltraGridBase)ULGTransactionsSettings).ActiveRow).Selected = true;
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillData();
	}

	private void ULGTransactionsSettings_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGTransactionsSettings.ActiveCell.Column).Key.Equals("DelayUntilInMinutes"))
		{
			GlobalFunctions.CheckForIntegers(sender, e);
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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected O, but got Unknown
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected O, but got Unknown
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Expected O, but got Unknown
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Expected O, but got Unknown
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Expected O, but got Unknown
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Expected O, but got Unknown
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SMS.MasterData.frmSMSTransactionsSettings));
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
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		UltraTab val57 = new UltraTab();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		Appearance val61 = new Appearance();
		Appearance val62 = new Appearance();
		Appearance val63 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGTransactionsSettings = new UltraGrid();
		this.ultraTabSharedControlsPage2 = new UltraTabSharedControlsPage();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ultraPictureBox1 = new UltraPictureBox();
		this.ultraButton1 = new UltraButton();
		this.ultraLabel1 = new UltraLabel();
		this.ultraButton2 = new UltraButton();
		this.ultraTextEditor1 = new UltraTextEditor();
		this.ultraTextEditor2 = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.ultraButton3 = new UltraButton();
		this.ultraPictureBox2 = new UltraPictureBox();
		this.ultraLabel7 = new UltraLabel();
		this.ultraPictureBox3 = new UltraPictureBox();
		this.ultraButton4 = new UltraButton();
		this.ultraButton5 = new UltraButton();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.ultraTabPageControl7 = new UltraTabPageControl();
		this.ultraCheckEditor1 = new UltraCheckEditor();
		this.ultraGroupBox1 = new UltraGroupBox();
		this.ultraDateTimeEditor1 = new UltraDateTimeEditor();
		this.ultraLabel10 = new UltraLabel();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel11 = new UltraLabel();
		this.ultraGroupBox2 = new UltraGroupBox();
		this.ultraLabel12 = new UltraLabel();
		this.ultraCheckEditor2 = new UltraCheckEditor();
		this.ultraDateTimeEditor2 = new UltraDateTimeEditor();
		this.ultraDateTimeEditor3 = new UltraDateTimeEditor();
		this.ultraLabel13 = new UltraLabel();
		this.ultraGroupBox3 = new UltraGroupBox();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton3 = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl8 = new UltraTabPageControl();
		this.ultraGrid1 = new UltraGrid();
		this.ultraTabPageControl9 = new UltraTabPageControl();
		this.ultraGrid2 = new UltraGrid();
		this.ultraTabPageControl10 = new UltraTabPageControl();
		this.ultraGrid3 = new UltraGrid();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.tcTransactionsSettings = new UltraTabControl();
		this.btnCancel = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGTransactionsSettings).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox3).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tcTransactionsSettings).BeginInit();
		((System.Windows.Forms.Control)(object)this.tcTransactionsSettings).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGTransactionsSettings);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((SpecialBoxBase)((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGTransactionsSettings).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGTransactionsSettings, "ULGTransactionsSettings");
		((System.Windows.Forms.Control)(object)this.ULGTransactionsSettings).Name = "ULGTransactionsSettings";
		this.ULGTransactionsSettings.AfterEnterEditMode += new System.EventHandler(ULGTransactionsSettings_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGTransactionsSettings).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGTransactionsSettings_KeyPress);
		resources.ApplyResources(this.ultraTabSharedControlsPage2, "ultraTabSharedControlsPage2");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage2).Name = "ultraTabSharedControlsPage2";
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTextEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTextEditor2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ultraPictureBox1, "ultraPictureBox1");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance11");
		this.ultraPictureBox1.Appearance = (AppearanceBase)(object)val11;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox1).Name = "ultraPictureBox1";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((UltraButtonBase)this.ultraButton2).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance13");
		((TextEditorControlBase)this.ultraTextEditor1).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.ultraTextEditor1, "ultraTextEditor1");
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).Name = "ultraTextEditor1";
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).Image = resources.GetObject("appearance14.Image");
		((TextEditorControlBase)this.ultraTextEditor2).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.ultraTextEditor2, "ultraTextEditor2");
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).Name = "ultraTextEditor2";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.ultraButton3, "ultraButton3");
		((ControlBase)this.ultraButton3).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraButton3).Name = "ultraButton3";
		resources.ApplyResources(this.ultraPictureBox2, "ultraPictureBox2");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		this.ultraPictureBox2.Appearance = (AppearanceBase)(object)val16;
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox2.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox2).Name = "ultraPictureBox2";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		resources.ApplyResources(this.ultraPictureBox3, "ultraPictureBox3");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		this.ultraPictureBox3.Appearance = (AppearanceBase)(object)val18;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox3).Name = "ultraPictureBox3";
		resources.ApplyResources(this.ultraButton4, "ultraButton4");
		((System.Windows.Forms.Control)(object)this.ultraButton4).Name = "ultraButton4";
		resources.ApplyResources(this.ultraButton5, "ultraButton5");
		((System.Windows.Forms.Control)(object)this.ultraButton5).Name = "ultraButton5";
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).Image = resources.GetObject("appearance19.Image");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox3);
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Blue;
		((UltraToggleEditorBase)this.ultraCheckEditor1).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.ultraCheckEditor1).BackColorInternal = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.ultraCheckEditor1, "ultraCheckEditor1");
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).Name = "ultraCheckEditor1";
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add(this.numericUpDown1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		this.ultraDateTimeEditor1.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.ultraDateTimeEditor1, "ultraDateTimeEditor1");
		this.ultraDateTimeEditor1.MaskInput = "{longtime}";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor1).Name = "ultraDateTimeEditor1";
		this.ultraDateTimeEditor1.PromptChar = '-';
		this.ultraDateTimeEditor1.SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
		this.numericUpDown1.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		this.ultraGroupBox2.Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		resources.ApplyResources(this.ultraGroupBox2, "ultraGroupBox2");
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Name = "ultraGroupBox2";
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		resources.ApplyResources(this.ultraCheckEditor2, "ultraCheckEditor2");
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor2).Name = "ultraCheckEditor2";
		resources.ApplyResources(this.ultraDateTimeEditor2, "ultraDateTimeEditor2");
		this.ultraDateTimeEditor2.FormatString = "";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2).Name = "ultraDateTimeEditor2";
		this.ultraDateTimeEditor2.PromptChar = '-';
		this.ultraDateTimeEditor3.FormatString = "";
		resources.ApplyResources(this.ultraDateTimeEditor3, "ultraDateTimeEditor3");
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3).Name = "ultraDateTimeEditor3";
		this.ultraDateTimeEditor3.PromptChar = '-';
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Red;
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val24;
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		this.ultraGroupBox3.Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton3);
		resources.ApplyResources(this.ultraGroupBox3, "ultraGroupBox3");
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Name = "ultraGroupBox3";
		resources.ApplyResources(this.radioButton1, "radioButton1");
		this.radioButton1.BackColor = System.Drawing.Color.Transparent;
		this.radioButton1.ForeColor = System.Drawing.Color.Red;
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.radioButton2, "radioButton2");
		this.radioButton2.BackColor = System.Drawing.Color.Transparent;
		this.radioButton2.ForeColor = System.Drawing.Color.Red;
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.radioButton3, "radioButton3");
		this.radioButton3.BackColor = System.Drawing.Color.Transparent;
		this.radioButton3.Checked = true;
		this.radioButton3.ForeColor = System.Drawing.Color.Red;
		this.radioButton3.Name = "radioButton3";
		this.radioButton3.TabStop = true;
		this.radioButton3.UseVisualStyleBackColor = false;
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid1);
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		((UltraGridBase)this.ultraGrid1).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val26).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val27;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val29;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val30).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val32).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val33).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ultraGrid1, "ultraGrid1");
		((System.Windows.Forms.Control)(object)this.ultraGrid1).Name = "ultraGrid1";
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid2);
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		((UltraGridBase)this.ultraGrid2).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val36).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val36).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val36).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val37;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val38).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val38).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val39).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val39;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val40).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val41;
		((AppearanceBase)val42).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val42).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val42;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val43).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val43).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val43).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val44).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val45;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ultraGrid2, "ultraGrid2");
		((System.Windows.Forms.Control)(object)this.ultraGrid2).Name = "ultraGrid2";
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid3);
		resources.ApplyResources(this.ultraTabPageControl10, "ultraTabPageControl10");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Name = "ultraTabPageControl10";
		((UltraGridBase)this.ultraGrid3).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val46).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val46).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val46).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val46;
		((AppearanceBase)val47).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val47;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val48).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val48).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val48).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val49).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val49;
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val50).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val50;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val51;
		((AppearanceBase)val52).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val52).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val52;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val53).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val53).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val53).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val53).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val54).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val54;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ultraGrid3, "ultraGrid3");
		((System.Windows.Forms.Control)(object)this.ultraGrid3).Name = "ultraGrid3";
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.tcTransactionsSettings, "tcTransactionsSettings");
		((AppearanceBase)val56).ForeColor = System.Drawing.Color.Maroon;
		((AppearanceBase)val56).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcTransactionsSettings).Appearance = (AppearanceBase)(object)val56;
		((System.Windows.Forms.Control)(object)this.tcTransactionsSettings).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcTransactionsSettings).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tcTransactionsSettings).Name = "tcTransactionsSettings";
		((UltraTabControlBase)this.tcTransactionsSettings).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcTransactionsSettings).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val57).Key = "Transactions Settings";
		val57.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val57, "ultraTab2");
		((SubObjectBase)val57).ForceApplyResources = "";
		((UltraTabControlBase)this.tcTransactionsSettings).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val57 });
		((AppearanceBase)val58).Image = resources.GetObject("appearance57.Image");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val58;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val59).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val59;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		((AppearanceBase)val60).Image = resources.GetObject("appearance59.Image");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val60;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val61).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val61).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val61).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val61;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val62).Image = resources.GetObject("appearance61.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val62;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val63).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val63).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val63).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val63;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tcTransactionsSettings);
		base.Name = "frmSMSTransactionsSettings";
		base.Load += new System.EventHandler(frmTransactioTypes_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tcTransactionsSettings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGTransactionsSettings).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox3).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tcTransactionsSettings).EndInit();
		((System.Windows.Forms.Control)(object)this.tcTransactionsSettings).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
