using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
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

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesCustoms : frmBase
{
	private DataTable dtVessels;

	private DataTable dtOwners;

	private DataTable dtNationality;

	private DataTable dtOperationsServicesCustoms;

	private DataTable dtOperationsServicesCompanions;

	private ValueList vlNationality = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private bool ReadOnly;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblToPlace;

	private UltraTextEditor txtToPlace;

	private UltraTextEditor txtCustomDeclarationNo;

	private UltraLabel lblCustomDeclarationNo;

	private UltraTextEditor txtClearenceFees;

	private UltraLabel lblClearenceFees;

	private UltraTextEditor txtBillNo;

	private UltraLabel lblBillNo;

	public UltraTabControl UTCDetails;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraGrid ULGDataCompanions;

	private UltraTextEditor txtBaseNo;

	private UltraLabel lblBaseNo;

	private UltraComboEditor cboOwners;

	private UltraLabel lblOwner;

	public UltraButton btnOwnerSearch;

	public UltraButton btnVesselSearch;

	private UltraLabel lblVesselName;

	private UltraComboEditor cboVesselName;

	public frmOperationsServicesCustoms()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmOperationsServicesCustoms(string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool READONLY)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlNationality.ValueListItems.Clear();
		for (int i = 0; i < dtNationality.Rows.Count; i++)
		{
			vlNationality.ValueListItems.Add(dtNationality.Rows[i]["NationalityID"], dtNationality.Rows[i]["NationalityName"].ToString());
		}
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVesselName, dtVessels, "VesselID", "VesselName");
		dtOwners = Owners.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOwners, dtOwners, "SubAccountID", "OwnerName");
		dtOperationsServicesCustoms = OperationsServicesCustoms.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		dtOperationsServicesCompanions = OperationsServicesSubAccounts.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		DisplayControls();
		((UltraGridBase)ULGDataCompanions).DataSource = dtOperationsServicesCompanions;
		CompanionsInitGrid();
		((Control)(object)btnSave).Enabled = !ReadOnly;
	}

	public bool ValidateData()
	{
		if (cboVesselName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الباخرة" : "Please Select Vessel");
			((TextEditorControlBase)cboVesselName).Focus();
			cboVesselName.DropDown();
			return false;
		}
		if (cboOwners.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المالك" : "Please Select Owners");
			((TextEditorControlBase)cboOwners).Focus();
			cboOwners.DropDown();
			return false;
		}
		if (((Control)(object)txtToPlace).Text.Trim().Equals(""))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الجهة" : "Please Enter the Place");
			((TextEditorControlBase)txtToPlace).Focus();
			return false;
		}
		return true;
	}

	public void DisplayControls()
	{
		if (dtOperationsServicesCustoms != null && dtOperationsServicesCustoms.Rows.Count > 0)
		{
			((TextEditorControlBase)cboVesselName).Value = dtOperationsServicesCustoms.Rows[0]["VesselID"];
			((TextEditorControlBase)cboOwners).Value = dtOperationsServicesCustoms.Rows[0]["OwnerSubAccountID"];
			((Control)(object)txtToPlace).Text = dtOperationsServicesCustoms.Rows[0]["ToPlace"].ToString();
			((Control)(object)txtBaseNo).Text = dtOperationsServicesCustoms.Rows[0]["BaseNo"].ToString();
			((Control)(object)txtCustomDeclarationNo).Text = dtOperationsServicesCustoms.Rows[0]["CustomsDeclarationNo"].ToString();
			((Control)(object)txtBillNo).Text = dtOperationsServicesCustoms.Rows[0]["BillNo"].ToString();
			((Control)(object)txtClearenceFees).Text = dtOperationsServicesCustoms.Rows[0]["ClearanceFees"].ToString();
		}
	}

	public void CompanionsInitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCompanions);
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["OperationServiceSubAccountID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NameEn"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NameEn"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NationalityID"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NationalityID"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.1);
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NationalityID"].ValueList = (IValueList)(object)vlNationality;
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["NationalityID"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No");
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No");
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ إصدار الهوية" : "ID Issue Date");
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCompanions).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataCompanions).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
	}

	private void ULGDataCompanions_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = OperationsServicesCustoms.Insert_Update((dtOperationsServicesCustoms.Rows.Count > 0) ? dtOperationsServicesCustoms.Rows[0]["OperationServiceCustomsID"].ToString() : "-1", OperationServiceID, OperationID, ((TextEditorControlBase)cboVesselName).Value.ToString(), ((TextEditorControlBase)cboOwners).Value.ToString(), (((Control)(object)txtToPlace).Text == "") ? "Null" : ((Control)(object)txtToPlace).Text, (((Control)(object)txtBaseNo).Text == "") ? "Null" : ((Control)(object)txtBaseNo).Text, (((Control)(object)txtCustomDeclarationNo).Text == "") ? "Null" : ((Control)(object)txtCustomDeclarationNo).Text, (((Control)(object)txtBillNo).Text == "") ? "Null" : ((Control)(object)txtBillNo).Text, (((Control)(object)txtClearenceFees).Text == "") ? "Null" : ((Control)(object)txtClearenceFees).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCompanions).Rows).Count; i++)
			{
				((UltraGridBase)ULGDataCompanions).Rows[i].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGDataCompanions).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGDataCompanions).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGDataCompanions).Rows[i].Cells["OperationServiceSubAccountID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesSubAccounts", "OperationServiceID", OperationServiceID, "OperationServiceSubAccountID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCompanions).Rows).Count > 0)
			{
				OperationsServicesSubAccounts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataCompanions).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
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

	private void txtClearenceFees_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGDataCompanions_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (ReadOnly)
		{
			((GridItemBase)ULGDataCompanions.ActiveCell).Selected = true;
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
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesCustoms));
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
		UltraTab val17 = new UltraTab();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataCompanions = new UltraGrid();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblToPlace = new UltraLabel();
		this.txtToPlace = new UltraTextEditor();
		this.txtCustomDeclarationNo = new UltraTextEditor();
		this.lblCustomDeclarationNo = new UltraLabel();
		this.txtClearenceFees = new UltraTextEditor();
		this.lblClearenceFees = new UltraLabel();
		this.txtBillNo = new UltraTextEditor();
		this.lblBillNo = new UltraLabel();
		this.UTCDetails = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtBaseNo = new UltraTextEditor();
		this.lblBaseNo = new UltraLabel();
		this.cboOwners = new UltraComboEditor();
		this.lblOwner = new UltraLabel();
		this.btnOwnerSearch = new UltraButton();
		this.btnVesselSearch = new UltraButton();
		this.lblVesselName = new UltraLabel();
		this.cboVesselName = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCompanions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtToPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomDeclarationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClearenceFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBaseNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCompanions);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataCompanions, "ULGDataCompanions");
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCompanions).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCompanions).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGDataCompanions).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.ULGDataCompanions).Name = "ULGDataCompanions";
		this.ULGDataCompanions.AfterEnterEditMode += new System.EventHandler(ULGDataCompanions_AfterEnterEditMode);
		this.ULGDataCompanions.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataCompanions_BeforeRowsDeleted);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val14).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val14;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblToPlace, "lblToPlace");
		this.lblToPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToPlace).Name = "lblToPlace";
		((ControlBase)this.lblToPlace).WrapText = false;
		resources.ApplyResources(this.txtToPlace, "txtToPlace");
		((System.Windows.Forms.Control)(object)this.txtToPlace).Name = "txtToPlace";
		resources.ApplyResources(this.txtCustomDeclarationNo, "txtCustomDeclarationNo");
		((System.Windows.Forms.Control)(object)this.txtCustomDeclarationNo).Name = "txtCustomDeclarationNo";
		resources.ApplyResources(this.lblCustomDeclarationNo, "lblCustomDeclarationNo");
		this.lblCustomDeclarationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo).Name = "lblCustomDeclarationNo";
		((ControlBase)this.lblCustomDeclarationNo).WrapText = false;
		resources.ApplyResources(this.txtClearenceFees, "txtClearenceFees");
		((System.Windows.Forms.Control)(object)this.txtClearenceFees).Name = "txtClearenceFees";
		((System.Windows.Forms.Control)(object)this.txtClearenceFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtClearenceFees_KeyPress);
		resources.ApplyResources(this.lblClearenceFees, "lblClearenceFees");
		this.lblClearenceFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClearenceFees).Name = "lblClearenceFees";
		((ControlBase)this.lblClearenceFees).WrapText = false;
		resources.ApplyResources(this.txtBillNo, "txtBillNo");
		((System.Windows.Forms.Control)(object)this.txtBillNo).Name = "txtBillNo";
		resources.ApplyResources(this.lblBillNo, "lblBillNo");
		this.lblBillNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBillNo).Name = "lblBillNo";
		((ControlBase)this.lblBillNo).WrapText = false;
		resources.ApplyResources(this.UTCDetails, "UTCDetails");
		resources.ApplyResources(val16, "appearance16");
		((AppearanceBase)val16).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCDetails).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Name = "UTCDetails";
		((UltraTabControlBase)this.UTCDetails).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.UTCDetails).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val17).Key = "Companion";
		val17.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val17, "ultraTab1");
		((SubObjectBase)val17).ForceApplyResources = "";
		((UltraTabControlBase)this.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val17 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtBaseNo, "txtBaseNo");
		((System.Windows.Forms.Control)(object)this.txtBaseNo).Name = "txtBaseNo";
		resources.ApplyResources(this.lblBaseNo, "lblBaseNo");
		this.lblBaseNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBaseNo).Name = "lblBaseNo";
		((ControlBase)this.lblBaseNo).WrapText = false;
		resources.ApplyResources(this.cboOwners, "cboOwners");
		((TextEditorControlBase)this.cboOwners).AlwaysInEditMode = true;
		this.cboOwners.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOwners).Name = "cboOwners";
		resources.ApplyResources(this.lblOwner, "lblOwner");
		this.lblOwner.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOwner).Name = "lblOwner";
		((ControlBase)this.lblOwner).WrapText = false;
		resources.ApplyResources(this.btnOwnerSearch, "btnOwnerSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance17");
		((ControlBase)this.btnOwnerSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnOwnerSearch).Name = "btnOwnerSearch";
		resources.ApplyResources(this.btnVesselSearch, "btnVesselSearch");
		((AppearanceBase)val19).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val19, "appearance18");
		((ControlBase)this.btnVesselSearch).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.btnVesselSearch).Name = "btnVesselSearch";
		resources.ApplyResources(this.lblVesselName, "lblVesselName");
		this.lblVesselName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselName).Name = "lblVesselName";
		((ControlBase)this.lblVesselName).WrapText = false;
		resources.ApplyResources(this.cboVesselName, "cboVesselName");
		((TextEditorControlBase)this.cboVesselName).AlwaysInEditMode = true;
		this.cboVesselName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVesselName).Name = "cboVesselName";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVesselSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOwners);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOwner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOwnerSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBaseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBaseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClearenceFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClearenceFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBillNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBillNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomDeclarationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtToPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmOperationsServicesCustoms";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtToPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomDeclarationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBillNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBillNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClearenceFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClearenceFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBaseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBaseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOwnerSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOwner, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOwners, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVesselSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataCompanions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtToPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomDeclarationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClearenceFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBaseNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
