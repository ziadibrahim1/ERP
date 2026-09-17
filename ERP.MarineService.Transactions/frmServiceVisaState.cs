using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmServiceVisaState : frmPosted
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtSettings;

	private DataTable dtRejectionReasons = new DataTable();

	private DataTable dtVisaStates = new DataTable();

	private DataTable dtAirLines = new DataTable();

	private DataTable dtAirPorts = new DataTable();

	private DataTable dtCompanies = new DataTable();

	private ValueList vlAirLines = new ValueList();

	private ValueList vlVisaStates = new ValueList();

	private ValueList vlRejectionReasons = new ValueList();

	private DataTable dtVessels = new DataTable();

	private ValueList vlVessels = new ValueList();

	private ValueList vlOnOff = new ValueList();

	private ValueList vlAirPorts = new ValueList();

	private ValueList vlCompanies = new ValueList();

	private ValueList vlPassengerType = new ValueList();

	private int VisaActualPeriodDays = 0;

	private int VisaValidPeriodDays = 0;

	private IContainer components = null;

	public UltraButton btnNewVisa;

	public UltraButton btnPrint;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	public frmServiceVisaState()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		InitializeComponent();
		NoCol = "OperationNo";
	}

	public override void PrepareData()
	{
		((Control)(object)btnPost).Enabled = false;
		((Control)(object)btnSaveClose).Enabled = false;
		dtVisaStates = VisaStates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlVisaStates.ValueListItems.Clear();
		for (int i = 0; i < dtVisaStates.Rows.Count; i++)
		{
			vlVisaStates.ValueListItems.Add(dtVisaStates.Rows[i]["VisaStateID"], dtVisaStates.Rows[i]["VisaStateName"].ToString());
		}
		dtRejectionReasons = RejectionReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlRejectionReasons.ValueListItems.Clear();
		for (int j = 0; j < dtRejectionReasons.Rows.Count; j++)
		{
			vlRejectionReasons.ValueListItems.Add(dtRejectionReasons.Rows[j]["RejectionReasonID"], dtRejectionReasons.Rows[j]["RejectionReasonName"].ToString());
		}
		dtAirLines = AirLines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAirLines.ValueListItems.Clear();
		for (int k = 0; k < dtAirLines.Rows.Count; k++)
		{
			vlAirLines.ValueListItems.Add(dtAirLines.Rows[k]["AirLineID"], dtAirLines.Rows[k]["AirLineName"].ToString());
		}
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlVessels.ValueListItems.Clear();
		for (int l = 0; l < dtVessels.Rows.Count; l++)
		{
			vlVessels.ValueListItems.Add(dtVessels.Rows[l]["VesselID"], dtVessels.Rows[l]["VesselName"].ToString());
		}
		dtAirPorts = AirPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAirPorts.ValueListItems.Clear();
		for (int m = 0; m < dtAirPorts.Rows.Count; m++)
		{
			vlAirPorts.ValueListItems.Add(dtAirPorts.Rows[m]["AirPortID"], dtAirPorts.Rows[m]["AirPortName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCompanies.ValueListItems.Clear();
		for (int n = 0; n < dtCompanies.Rows.Count; n++)
		{
			vlCompanies.ValueListItems.Add(dtCompanies.Rows[n]["CompanyID"], dtCompanies.Rows[n]["CompanyName"].ToString());
		}
		vlOnOff.ValueListItems.Clear();
		vlOnOff.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "On" : "On");
		vlOnOff.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "OFF" : "OFF");
		vlPassengerType.ValueListItems.Clear();
		vlPassengerType.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerType.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerType.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
		dtSettings = Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtSettings.Rows.Count > 0)
		{
			VisaActualPeriodDays = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaActualPeriodDays"] != DBNull.Value) ? int.Parse(dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaActualPeriodDays"].ToString()) : 0);
			VisaValidPeriodDays = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaValidPeriodDays"] != DBNull.Value) ? int.Parse(dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaValidPeriodDays"].ToString()) : 0);
		}
	}

	public override void FillGrid()
	{
		dtsource = OperationsServicesVisas.State(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	public override void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselID"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselID"].ValueList = (IValueList)(object)vlVessels;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفيزا" : "Serial No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التأشيرة" : "Visa No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).Caption = (GlobalVariables.IsArabic ? "الحاله" : "ON/OFF");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].ValueList = (IValueList)(object)vlOnOff;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإصدار" : "ID Issue Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "صالح من" : "Visa Issue Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Header).Caption = (GlobalVariables.IsArabic ? "حتي" : "Visa Expiry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "دخول" : "Entry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Exit Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحلة" : "Voyage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة" : "Status");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].ValueList = (IValueList)(object)vlVisaStates;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Completed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Header).Caption = (GlobalVariables.IsArabic ? "طبعت" : "PrintOut");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة التأشيرة" : "Visa Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة الطباعه" : "PrintOut Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPortID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPortID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPortID"].Header).Caption = (GlobalVariables.IsArabic ? "المطار" : "AirPort");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPortID"].ValueList = (IValueList)(object)vlAirPorts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReasonID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReasonID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReasonID"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الرفض" : "Rejection Reason");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReasonID"].ValueList = (IValueList)(object)vlRejectionReasons;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Header).Caption = (GlobalVariables.IsArabic ? "مرفوضه" : "Rejected");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].ValueList = (IValueList)(object)vlCompanies;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Header).Caption = (GlobalVariables.IsArabic ? "عاجل" : "Urgent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الراكب" : "Passenger Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].ValueList = (IValueList)(object)vlPassengerType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Header).Caption = (GlobalVariables.IsArabic ? "ملغاة" : "Cancelled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Cancellation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الالغاء" : "Cancellation Reason");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Header).Caption = (GlobalVariables.IsArabic ? "الخروج المتوقع" : "Entry Expire");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void SelectFullRow()
	{
		((GridItemBase)ULGData.ActiveCell).Selected = true;
	}

	public override void SaveData()
	{
	}

	private void btnNewVisa_Click(object sender, EventArgs e)
	{
		frmInsertSeaManVisa frmInsertSeaManVisa2 = new frmInsertSeaManVisa();
		frmInsertSeaManVisa2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		((Control)(object)frmInsertSeaManVisa2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات" : "Visas");
		frmInsertSeaManVisa2.Location = new Point(0, 0);
		frmInsertSeaManVisa2.ShowDialog();
		RefrechData();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + "Rep_MS_VesselsVisasSummary.rpt");
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ValidFromDate" && !e.Cell.Value.Equals(null) && !e.Cell.Value.Equals(""))
		{
			e.Cell.Row.Cells["ValidToDate"].Value = DateTime.Parse(e.Cell.Value.ToString()).AddDays(VisaValidPeriodDays - 1);
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ActualFromDate" && !e.Cell.Value.Equals(null) && !e.Cell.Value.Equals(""))
		{
			e.Cell.Row.Cells["EntryExpireDate"].Value = DateTime.Parse(e.Cell.Value.ToString()).AddDays(VisaActualPeriodDays - 1);
		}
		else if (e.Cell.Value != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "IsCancelled")
		{
			if (Convert.ToBoolean(e.Cell.Value))
			{
				e.Cell.Row.Cells["IsRejected"].Value = false;
				return;
			}
			e.Cell.Row.Cells["CancellationDate"].Value = DBNull.Value;
			e.Cell.Row.Cells["CancellationReason"].Value = DBNull.Value;
		}
		else if (e.Cell.Value != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "IsRejected")
		{
			if (Convert.ToBoolean(e.Cell.Value))
			{
				e.Cell.Row.Cells["IsCancelled"].Value = false;
			}
			else
			{
				e.Cell.Row.Cells["RejectionReasonID"].Value = DBNull.Value;
			}
		}
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		bool flag = false;
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete These Data ?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			for (int i = 0; i < e.Rows.Length; i++)
			{
				if (e.Rows[i].Cells["OperationID"].Value != DBNull.Value)
				{
					if (!flag)
					{
						flag = true;
					}
					((CancelEventArgs)(object)e).Cancel = true;
				}
				else
				{
					JV.DeleteVirtual(e.Rows[i].Cells["ExpenseJVID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: false);
					JVDetails.DeleteVirtualByJVID(e.Rows[i].Cells["ExpenseJVID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: false);
					OperationsServicesVisas.DeleteVirtual(e.Rows[i].Cells["OperationServiceVisaID"].Value.ToString(), GlobalVariables.UserID);
					((CancelEventArgs)(object)e).Cancel = true;
				}
			}
			if (flag)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن حذف هذه التأشيرة لأنها مضافه على عملية" : "Can not Delete this Visa Because it is on operation");
			}
			RefrechData();
		}
		else
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6 && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IDNo")
		{
			frmUpdateIDNo frmUpdateIDNo2 = new frmUpdateIDNo(((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountTypeID"].Value.ToString(), GlobalVariables.IsArabic ? ":رقم الهوية" : "ID No.:", "IDNo", ((UltraGridBase)ULGData).ActiveRow.Cells["IDNo"].Value.ToString());
			frmUpdateIDNo2.WindowState = FormWindowState.Normal;
			frmUpdateIDNo2.ShowDialog();
			if (frmUpdateIDNo2.Value != "")
			{
				RefrechData();
			}
		}
	}

	private void ULGData_DoubleClick(object sender, EventArgs e)
	{
		if (CanPost && ((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmUpdateSeaManVisa frmUpdateSeaManVisa2 = new frmUpdateSeaManVisa(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceVisaID"].Value.ToString());
			frmUpdateSeaManVisa2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateSeaManVisa2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات" : "Visas");
			frmUpdateSeaManVisa2.ShowDialog();
			FillGrid();
		}
	}

	private void ULGData_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmServiceVisaState));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.btnNewVisa = new UltraButton();
		this.btnPrint = new UltraButton();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorStyle = (HeaderStyle)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((UltraGridBase)base.ULGData).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGData_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)base.ULGData).DoubleClick += new System.EventHandler(ULGData_DoubleClick);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.Add((System.Windows.Forms.Control)(object)this.btnNewVisa);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewVisa, 0);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		((System.Windows.Forms.Control)(object)base.UGBByName).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val6, "appearance9");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnNewVisa, "btnNewVisa");
		((AppearanceBase)val7).Image = resources.GetObject("appearance8.Image");
		((ControlBase)this.btnNewVisa).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnNewVisa).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewVisa).Name = "btnNewVisa";
		((System.Windows.Forms.Control)(object)this.btnNewVisa).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewVisa).Click += new System.EventHandler(btnNewVisa_Click);
		resources.ApplyResources(this.btnPrint, "btnPrint");
		((AppearanceBase)val8).Image = resources.GetObject("appearance7.Image");
		((ControlBase)this.btnPrint).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnPrint).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((System.Windows.Forms.Control)(object)this.btnPrint).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val9, "appearance6");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Name = "frmServiceVisaState";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)base.UGBByName).PerformLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
