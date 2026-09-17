using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Defaults;
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

namespace ERP.SystemOptions.GeneralOptions;

public class frmSystemOptions : frmBase
{
	private DataTable dtSystemOptions;

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

	private UltraTabPageControl ultraTabPageControl6;

	public UltraGrid ULGSysOption;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabControl tcSystemDefaults;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	private UltraTextEditor txtUpdatePath;

	private UltraLabel ultraLabel2;

	private UltraButton btnUpdatePath;

	private FolderBrowserDialog fbdPath;

	private UltraTextEditor txtReportPath;

	private UltraLabel ultraLabel3;

	private UltraButton btnReportPath;

	private UltraButton btnBackupPath;

	private UltraLabel lblBackupPath;

	private UltraTextEditor txtBackupPath;

	private UltraButton btnArchivingPath;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtArchivingPath;

	private UltraTabPageControl ultraTabPageControl1;

	private UltraLabel lblCompanyEmail;

	private UltraTextEditor txtCompanyEmail;

	private UltraTextEditor txtCompanyEmailPassword;

	private UltraLabel lblCompanyEmailPassword;

	private UltraTextEditor txtOutgoingServerPort;

	private UltraLabel lblOutgoingServerPort;

	private UltraTextEditor txtOutgoingMailServer;

	private UltraLabel lblOutgoingMailServer;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraTextEditor txtEINVClientSecret2;

	private UltraTextEditor txtEINVClientSecret1;

	private UltraLabel lblEINVClientSecret1;

	private UltraTextEditor txtEINVClientID;

	private UltraLabel lblEINVClientSecret2;

	private UltraLabel lblEINVClientID;

	public frmSystemOptions()
	{
		InitializeComponent();
	}

	public void InitGrids()
	{
		((UltraGridBase)ULGSysOption).DataSource = dtSystemOptions;
		GlobalFunctions.PrepareGrid(ULGSysOption);
		((UltraGridBase)ULGSysOption).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Header).Caption = (GlobalVariables.IsArabic ? "" : "");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.6);
	}

	private void frmSystemOptions_Load(object sender, EventArgs e)
	{
		FillData();
	}

	private void FillData()
	{
		((Control)(object)txtUpdatePath).Text = GlobalFunctions.GetDefault("UpdatePath");
		((Control)(object)txtReportPath).Text = GlobalFunctions.GetDefault("PathReport");
		((Control)(object)txtBackupPath).Text = GlobalFunctions.GetDefault("BackupPath");
		((Control)(object)txtArchivingPath).Text = GlobalFunctions.GetDefault("ArchivingPath");
		((Control)(object)txtOutgoingServerPort).Text = GlobalFunctions.GetDefault("OutgoingServerPort");
		((Control)(object)txtOutgoingMailServer).Text = GlobalFunctions.GetDefault("OutgoingMailServer(SMTP)");
		((Control)(object)txtCompanyEmail).Text = GlobalFunctions.GetDefault("CompanyEmail");
		((Control)(object)txtCompanyEmailPassword).Text = GlobalFunctions.GetDefault("CompanyEmailPassword");
		((Control)(object)txtEINVClientID).Text = GlobalFunctions.GetDefault("EINVClientID");
		((Control)(object)txtEINVClientSecret1).Text = GlobalFunctions.GetDefault("EINVClientSecret1");
		((Control)(object)txtEINVClientSecret2).Text = GlobalFunctions.GetDefault("EINVClientSecret2");
		dtSystemOptions = BusinessLayer.Defaults.SystemOptions.SelectForModule("0", "100", GlobalVariables.IsArabic ? "1" : "0");
		((UltraTabControlBase)tcSystemDefaults).Tabs["EInvoice"].Visible = GlobalFunctions.GetOption("UsingElectronicInvoice");
		InitGrids();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtCompanyEmail).Text != "" && !GlobalFunctions.IsEmailValid(((Control)(object)txtCompanyEmail).Text))
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال بريد اليكتروني صحيح  ", "Please Enter A Valid Email Address");
			((TextEditorControlBase)txtCompanyEmail).Focus();
			return;
		}
		if (((UltraTabControlBase)tcSystemDefaults).Tabs["EInvoice"].Visible)
		{
			if (((Control)(object)txtEINVClientID).Text == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم عميل صحيح  ", "Please Enter A Valid Client ID");
				((TextEditorControlBase)txtEINVClientID).Focus();
				return;
			}
			if (((Control)(object)txtEINVClientSecret1).Text == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم  سري 1 صحيح  ", "Please Enter A Valid Secret 1");
				((TextEditorControlBase)txtEINVClientSecret1).Focus();
				return;
			}
			if (((Control)(object)txtEINVClientSecret2).Text == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم  سري 2 صحيح  ", "Please Enter A Valid Secret 2");
				((TextEditorControlBase)txtEINVClientSecret2).Focus();
				return;
			}
		}
		UpdateData();
		FillData();
	}

	private void UpdateData()
	{
		GlobalFunctions.SetDefault("UpdatePath", ((Control)(object)txtUpdatePath).Text);
		GlobalFunctions.SetDefault("PathReport", ((Control)(object)txtReportPath).Text);
		GlobalVariables.ReportsPath = ((Control)(object)txtReportPath).Text;
		GlobalFunctions.SetDefault("ArchivingPath", ((Control)(object)txtArchivingPath).Text);
		GlobalVariables.ArchivingPath = ((Control)(object)txtArchivingPath).Text;
		GlobalFunctions.SetDefault("BackupPath", ((Control)(object)txtBackupPath).Text);
		GlobalFunctions.SetDefault("OutgoingMailServer(SMTP)", ((Control)(object)txtOutgoingMailServer).Text);
		GlobalFunctions.SetDefault("OutgoingServerPort", ((Control)(object)txtOutgoingServerPort).Text);
		GlobalFunctions.SetDefault("CompanyEmail", ((Control)(object)txtCompanyEmail).Text);
		GlobalFunctions.SetDefault("CompanyEmailPassword", ((Control)(object)txtCompanyEmailPassword).Text);
		GlobalFunctions.SetDefault("EINVClientID", ((Control)(object)txtEINVClientID).Text);
		GlobalFunctions.SetDefault("EINVClientSecret1", ((Control)(object)txtEINVClientSecret1).Text);
		GlobalFunctions.SetDefault("EINVClientSecret2", ((Control)(object)txtEINVClientSecret2).Text);
		BusinessLayer.Defaults.SystemOptions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGSysOption).DataSource, GlobalVariables.UserID);
		GlobalVariables.InformationMB.Show("تــم تخزين البيانات بنجــــاح", "Updates were saved successfully");
		GlobalVariables.dtSystemOptions = BusinessLayer.Defaults.SystemOptions.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		FillData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void ULGSysOption_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "OptionEnName" || (bool)((UltraGridBase)ULGSysOption).ActiveRow.Cells["ReadOnly"].Value)
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "Description")
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
	}

	private void ULGSysOption_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		ULGSysOption.CellChange -= new CellEventHandler(ULGSysOption_CellChange);
		((UltraGridBase)ULGSysOption).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "OptionValue" && (e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "FIFOStockControl") && (bool)e.Cell.Value)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysOption).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "FIFOStockControl")
				{
					((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionValue"].Value = false;
				}
			}
			e.Cell.Value = true;
		}
		ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnUpdatePath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtUpdatePath).Text = fbdPath.SelectedPath;
		}
	}

	private void btnReportPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtReportPath).Text = fbdPath.SelectedPath + "\\";
		}
	}

	private void btnBackupPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtBackupPath).Text = fbdPath.SelectedPath;
		}
	}

	private void btnArchivingPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtArchivingPath).Text = fbdPath.SelectedPath + "\\";
		}
	}

	private void txtOutgoingServerPort_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Expected O, but got Unknown
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Expected O, but got Unknown
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Expected O, but got Unknown
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Expected O, but got Unknown
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Expected O, but got Unknown
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Expected O, but got Unknown
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Expected O, but got Unknown
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Expected O, but got Unknown
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Expected O, but got Unknown
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Expected O, but got Unknown
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Expected O, but got Unknown
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmSystemOptions));
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
		Appearance val57 = new Appearance();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		Appearance val61 = new Appearance();
		UltraTab val62 = new UltraTab();
		UltraTab val63 = new UltraTab();
		UltraTab val64 = new UltraTab();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.ULGSysOption = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.lblCompanyEmail = new UltraLabel();
		this.txtCompanyEmail = new UltraTextEditor();
		this.txtCompanyEmailPassword = new UltraTextEditor();
		this.lblCompanyEmailPassword = new UltraLabel();
		this.txtOutgoingServerPort = new UltraTextEditor();
		this.lblOutgoingServerPort = new UltraLabel();
		this.txtOutgoingMailServer = new UltraTextEditor();
		this.lblOutgoingMailServer = new UltraLabel();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.lblEINVClientSecret2 = new UltraLabel();
		this.txtEINVClientSecret2 = new UltraTextEditor();
		this.txtEINVClientSecret1 = new UltraTextEditor();
		this.lblEINVClientSecret1 = new UltraLabel();
		this.txtEINVClientID = new UltraTextEditor();
		this.lblEINVClientID = new UltraLabel();
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
		this.tcSystemDefaults = new UltraTabControl();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtUpdatePath = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.btnUpdatePath = new UltraButton();
		this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
		this.txtReportPath = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.btnReportPath = new UltraButton();
		this.btnBackupPath = new UltraButton();
		this.lblBackupPath = new UltraLabel();
		this.txtBackupPath = new UltraTextEditor();
		this.btnArchivingPath = new UltraButton();
		this.ultraLabel4 = new UltraLabel();
		this.txtArchivingPath = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyEmail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyEmailPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutgoingServerPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutgoingMailServer).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientSecret2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientSecret1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientID).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).BeginInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtUpdatePath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReportPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBackupPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArchivingPath).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ULGSysOption);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.ULGSysOption, "ULGSysOption");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGSysOption).Name = "ULGSysOption";
		this.ULGSysOption.AfterEnterEditMode += new System.EventHandler(ULGSysOption_AfterEnterEditMode);
		this.ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyEmail);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyEmail);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyEmailPassword);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyEmailPassword);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.txtOutgoingServerPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblOutgoingServerPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.txtOutgoingMailServer);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblOutgoingMailServer);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.lblCompanyEmail, "lblCompanyEmail");
		this.lblCompanyEmail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyEmail).Name = "lblCompanyEmail";
		((ControlBase)this.lblCompanyEmail).WrapText = false;
		resources.ApplyResources(this.txtCompanyEmail, "txtCompanyEmail");
		((System.Windows.Forms.Control)(object)this.txtCompanyEmail).Name = "txtCompanyEmail";
		resources.ApplyResources(this.txtCompanyEmailPassword, "txtCompanyEmailPassword");
		((System.Windows.Forms.Control)(object)this.txtCompanyEmailPassword).Name = "txtCompanyEmailPassword";
		this.txtCompanyEmailPassword.PasswordChar = '●';
		resources.ApplyResources(this.lblCompanyEmailPassword, "lblCompanyEmailPassword");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblCompanyEmailPassword).Appearance = (AppearanceBase)(object)val11;
		this.lblCompanyEmailPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyEmailPassword).Name = "lblCompanyEmailPassword";
		((ControlBase)this.lblCompanyEmailPassword).WrapText = false;
		resources.ApplyResources(this.txtOutgoingServerPort, "txtOutgoingServerPort");
		((System.Windows.Forms.Control)(object)this.txtOutgoingServerPort).Name = "txtOutgoingServerPort";
		((System.Windows.Forms.Control)(object)this.txtOutgoingServerPort).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtOutgoingServerPort_KeyPress);
		resources.ApplyResources(this.lblOutgoingServerPort, "lblOutgoingServerPort");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblOutgoingServerPort).Appearance = (AppearanceBase)(object)val12;
		this.lblOutgoingServerPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOutgoingServerPort).Name = "lblOutgoingServerPort";
		((ControlBase)this.lblOutgoingServerPort).WrapText = false;
		resources.ApplyResources(this.txtOutgoingMailServer, "txtOutgoingMailServer");
		((System.Windows.Forms.Control)(object)this.txtOutgoingMailServer).Name = "txtOutgoingMailServer";
		resources.ApplyResources(this.lblOutgoingMailServer, "lblOutgoingMailServer");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblOutgoingMailServer).Appearance = (AppearanceBase)(object)val13;
		this.lblOutgoingMailServer.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOutgoingMailServer).Name = "lblOutgoingMailServer";
		((ControlBase)this.lblOutgoingMailServer).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVClientSecret2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtEINVClientSecret2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtEINVClientSecret1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVClientSecret1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtEINVClientID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVClientID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.lblEINVClientSecret2, "lblEINVClientSecret2");
		this.lblEINVClientSecret2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVClientSecret2).Name = "lblEINVClientSecret2";
		((ControlBase)this.lblEINVClientSecret2).WrapText = false;
		resources.ApplyResources(this.txtEINVClientSecret2, "txtEINVClientSecret2");
		((System.Windows.Forms.Control)(object)this.txtEINVClientSecret2).Name = "txtEINVClientSecret2";
		this.txtEINVClientSecret2.PasswordChar = '*';
		resources.ApplyResources(this.txtEINVClientSecret1, "txtEINVClientSecret1");
		((System.Windows.Forms.Control)(object)this.txtEINVClientSecret1).Name = "txtEINVClientSecret1";
		this.txtEINVClientSecret1.PasswordChar = '*';
		resources.ApplyResources(this.lblEINVClientSecret1, "lblEINVClientSecret1");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblEINVClientSecret1).Appearance = (AppearanceBase)(object)val14;
		this.lblEINVClientSecret1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVClientSecret1).Name = "lblEINVClientSecret1";
		((ControlBase)this.lblEINVClientSecret1).WrapText = false;
		resources.ApplyResources(this.txtEINVClientID, "txtEINVClientID");
		((System.Windows.Forms.Control)(object)this.txtEINVClientID).Name = "txtEINVClientID";
		resources.ApplyResources(this.lblEINVClientID, "lblEINVClientID");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblEINVClientID).Appearance = (AppearanceBase)(object)val15;
		this.lblEINVClientID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVClientID).Name = "lblEINVClientID";
		((ControlBase)this.lblEINVClientID).WrapText = false;
		resources.ApplyResources(this.ultraTabSharedControlsPage2, "ultraTabSharedControlsPage2");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage2).Name = "ultraTabSharedControlsPage2";
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ultraPictureBox1, "ultraPictureBox1");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance16");
		this.ultraPictureBox1.Appearance = (AppearanceBase)(object)val16;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox1).Name = "ultraPictureBox1";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((UltraButtonBase)this.ultraButton2).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		resources.ApplyResources(this.ultraTextEditor1, "ultraTextEditor1");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val18, "appearance18");
		((TextEditorControlBase)this.ultraTextEditor1).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).Name = "ultraTextEditor1";
		resources.ApplyResources(this.ultraTextEditor2, "ultraTextEditor2");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val19, "appearance19");
		((TextEditorControlBase)this.ultraTextEditor2).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).Name = "ultraTextEditor2";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.ultraButton3, "ultraButton3");
		((ControlBase)this.ultraButton3).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraButton3).Name = "ultraButton3";
		resources.ApplyResources(this.ultraPictureBox2, "ultraPictureBox2");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val21, "appearance21");
		this.ultraPictureBox2.Appearance = (AppearanceBase)(object)val21;
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox2.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox2).Name = "ultraPictureBox2";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		resources.ApplyResources(this.ultraPictureBox3, "ultraPictureBox3");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance23");
		this.ultraPictureBox3.Appearance = (AppearanceBase)(object)val23;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox3).Name = "ultraPictureBox3";
		resources.ApplyResources(this.ultraButton4, "ultraButton4");
		((System.Windows.Forms.Control)(object)this.ultraButton4).Name = "ultraButton4";
		resources.ApplyResources(this.ultraButton5, "ultraButton5");
		((System.Windows.Forms.Control)(object)this.ultraButton5).Name = "ultraButton5";
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.ultraCheckEditor1, "ultraCheckEditor1");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val26, "appearance26");
		((UltraToggleEditorBase)this.ultraCheckEditor1).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.ultraCheckEditor1).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).Name = "ultraCheckEditor1";
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val27, "appearance27");
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add(this.numericUpDown1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.ultraDateTimeEditor1, "ultraDateTimeEditor1");
		this.ultraDateTimeEditor1.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
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
		resources.ApplyResources(this.ultraGroupBox2, "ultraGroupBox2");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val28, "appearance28");
		this.ultraGroupBox2.Appearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Name = "ultraGroupBox2";
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		resources.ApplyResources(this.ultraCheckEditor2, "ultraCheckEditor2");
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor2).Name = "ultraCheckEditor2";
		resources.ApplyResources(this.ultraDateTimeEditor2, "ultraDateTimeEditor2");
		this.ultraDateTimeEditor2.FormatString = "";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2).Name = "ultraDateTimeEditor2";
		this.ultraDateTimeEditor2.PromptChar = '-';
		resources.ApplyResources(this.ultraDateTimeEditor3, "ultraDateTimeEditor3");
		this.ultraDateTimeEditor3.FormatString = "";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3).Name = "ultraDateTimeEditor3";
		this.ultraDateTimeEditor3.PromptChar = '-';
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Red;
		resources.ApplyResources(val29, "appearance29");
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val29;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		resources.ApplyResources(this.ultraGroupBox3, "ultraGroupBox3");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val30, "appearance30");
		this.ultraGroupBox3.Appearance = (AppearanceBase)(object)val30;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton3);
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
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		resources.ApplyResources(this.ultraGrid1, "ultraGrid1");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val31).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val31).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val31, "appearance31");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val32, "appearance32");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val32;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val34, "appearance34");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val34;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val35).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val36, "appearance36");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val37, "appearance37");
		((AppearanceBase)val37).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val38).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val38).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val38).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val39).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val40, "appearance40");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid1).Name = "ultraGrid1";
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		resources.ApplyResources(this.ultraGrid2, "ultraGrid2");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val41).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val41).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val41, "appearance41");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val41;
		((AppearanceBase)val42).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val42, "appearance42");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val42;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val43).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val43).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val43, "appearance43");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val44).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val44, "appearance44");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val44;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val45).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val45, "appearance45");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val45;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val46, "appearance46");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val46;
		((AppearanceBase)val47).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val47, "appearance47");
		((AppearanceBase)val47).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val47;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val48).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val48).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val48).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val48).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val48, "appearance48");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val49).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val49, "appearance49");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val49;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val50, "appearance50");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val50;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid2).Name = "ultraGrid2";
		resources.ApplyResources(this.ultraTabPageControl10, "ultraTabPageControl10");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Name = "ultraTabPageControl10";
		resources.ApplyResources(this.ultraGrid3, "ultraGrid3");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val51).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val51).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val51).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val51, "appearance51");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val51;
		((AppearanceBase)val52).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val52, "appearance52");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val52;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val53).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val53).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val53).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val53, "appearance53");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val54).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val54, "appearance54");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val54;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val55).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val55, "appearance55");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val56, "appearance56");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val56;
		((AppearanceBase)val57).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val57, "appearance57");
		((AppearanceBase)val57).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val57;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val58).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val58).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val58).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val58).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val58, "appearance58");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val59).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val59, "appearance59");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val59;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val60).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val60, "appearance60");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val60;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid3).Name = "ultraGrid3";
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.tcSystemDefaults, "tcSystemDefaults");
		((AppearanceBase)val61).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val61, "appearance61");
		((AppearanceBase)val61).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcSystemDefaults).Appearance = (AppearanceBase)(object)val61;
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Name = "tcSystemDefaults";
		((UltraTabControlBase)this.tcSystemDefaults).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcSystemDefaults).TabOrientation = (TabOrientation)1;
		val62.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val62, "ultraTab5");
		((SubObjectBase)val62).ForceApplyResources = "";
		((KeyedSubObjectBase)val63).Key = "CompanyEmailSettings";
		val63.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val63, "ultraTab1");
		((SubObjectBase)val63).ForceApplyResources = "";
		((KeyedSubObjectBase)val64).Key = "EInvoice";
		val64.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val64, "ultraTab2");
		val64.Visible = false;
		((SubObjectBase)val64).ForceApplyResources = "";
		((UltraTabControlBase)this.tcSystemDefaults).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val62, val63, val64 });
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val65).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val65, "appearance62");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val65;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val66).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val66).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val66).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val66, "appearance63");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val66;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val67).Image = resources.GetObject("appearance64.Image");
		resources.ApplyResources(val67, "appearance64");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val67;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val68).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val68).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val68).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val68, "appearance65");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val68;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val69).Image = resources.GetObject("appearance66.Image");
		resources.ApplyResources(val69, "appearance66");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val69;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val70).Image = resources.GetObject("appearance67.Image");
		resources.ApplyResources(val70, "appearance67");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val70;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtUpdatePath, "txtUpdatePath");
		((System.Windows.Forms.Control)(object)this.txtUpdatePath).Name = "txtUpdatePath";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val71).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val71, "appearance68");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val71;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.btnUpdatePath, "btnUpdatePath");
		((System.Windows.Forms.Control)(object)this.btnUpdatePath).Name = "btnUpdatePath";
		((System.Windows.Forms.Control)(object)this.btnUpdatePath).Click += new System.EventHandler(btnUpdatePath_Click);
		resources.ApplyResources(this.fbdPath, "fbdPath");
		resources.ApplyResources(this.txtReportPath, "txtReportPath");
		((System.Windows.Forms.Control)(object)this.txtReportPath).Name = "txtReportPath";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val72).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val72, "appearance69");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val72;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.btnReportPath, "btnReportPath");
		((System.Windows.Forms.Control)(object)this.btnReportPath).Name = "btnReportPath";
		((System.Windows.Forms.Control)(object)this.btnReportPath).Click += new System.EventHandler(btnReportPath_Click);
		resources.ApplyResources(this.btnBackupPath, "btnBackupPath");
		((System.Windows.Forms.Control)(object)this.btnBackupPath).Name = "btnBackupPath";
		((System.Windows.Forms.Control)(object)this.btnBackupPath).Click += new System.EventHandler(btnBackupPath_Click);
		resources.ApplyResources(this.lblBackupPath, "lblBackupPath");
		this.lblBackupPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBackupPath).Name = "lblBackupPath";
		((ControlBase)this.lblBackupPath).WrapText = false;
		resources.ApplyResources(this.txtBackupPath, "txtBackupPath");
		((System.Windows.Forms.Control)(object)this.txtBackupPath).Name = "txtBackupPath";
		resources.ApplyResources(this.btnArchivingPath, "btnArchivingPath");
		((System.Windows.Forms.Control)(object)this.btnArchivingPath).Name = "btnArchivingPath";
		((System.Windows.Forms.Control)(object)this.btnArchivingPath).Click += new System.EventHandler(btnArchivingPath_Click);
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val73).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val73, "appearance70");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val73;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtArchivingPath, "txtArchivingPath");
		((System.Windows.Forms.Control)(object)this.txtArchivingPath).Name = "txtArchivingPath";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBackupPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBackupPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBackupPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArchivingPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReportPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnArchivingPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReportPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUpdatePath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdatePath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tcSystemDefaults);
		base.Name = "frmSystemOptions";
		base.Load += new System.EventHandler(frmSystemOptions_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tcSystemDefaults, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdatePath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUpdatePath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReportPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnArchivingPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReportPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArchivingPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBackupPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBackupPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBackupPath, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyEmail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyEmailPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutgoingServerPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutgoingMailServer).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientSecret2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientSecret1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVClientID).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).EndInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtUpdatePath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReportPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBackupPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArchivingPath).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
