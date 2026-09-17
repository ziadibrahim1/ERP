using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
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

public class frmOperationsServicesEquipmentsAndVechiles : frmBase
{
	private DataTable dtNationality;

	private DataTable dtOperationsServicesBoats;

	private DataTable dtOperationsServicesVechiles;

	private DataTable dtOperationsServicesEquipments;

	private DataTable dtOperationsServicesWorkers;

	private DataTable dtSuppliers;

	private DataTable dtVessels;

	private ValueList vlNationality = new ValueList();

	private ValueList vlDriverNationality = new ValueList();

	private ValueList vlDriverNationality2 = new ValueList();

	private ValueList vlVessels = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private object SupplierID;

	private bool ReadOnly;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraTabControl UTCDetails;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGData;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraGrid ULGDataWorkers;

	private UltraLabel lblSupplier;

	private UltraComboEditor cboSupplier;

	public UltraButton btnSupplierSearch;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGDataVehicles;

	private UltraTabPageControl ultraTabPageControl4;

	public UltraGrid ULGBoats;

	public frmOperationsServicesEquipmentsAndVechiles()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmOperationsServicesEquipmentsAndVechiles(string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, object SUPPLIERID, bool READONLY)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		SupplierID = SUPPLIERID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlNationality.ValueListItems.Clear();
		vlDriverNationality.ValueListItems.Clear();
		vlDriverNationality2.ValueListItems.Clear();
		for (int i = 0; i < dtNationality.Rows.Count; i++)
		{
			vlNationality.ValueListItems.Add(dtNationality.Rows[i]["NationalityID"], dtNationality.Rows[i]["NationalityName"].ToString());
			vlDriverNationality.ValueListItems.Add(dtNationality.Rows[i]["NationalityID"], dtNationality.Rows[i]["NationalityName"].ToString());
			vlDriverNationality2.ValueListItems.Add(dtNationality.Rows[i]["NationalityID"], dtNationality.Rows[i]["NationalityName"].ToString());
		}
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int j = 0; j < dtVessels.Rows.Count; j++)
		{
			vlVessels.ValueListItems.Add(dtVessels.Rows[j]["VesselID"], dtVessels.Rows[j]["VesselName"].ToString());
		}
		dtOperationsServicesVechiles = OperationsServicesVechiles.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		dtOperationsServicesBoats = OperationsServicesTransferBoats.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		dtOperationsServicesEquipments = OperationsServicesEquipments.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		dtOperationsServicesWorkers = OperationsServicesSubAccounts.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		((TextEditorControlBase)cboSupplier).Value = SupplierID;
		((UltraGridBase)ULGBoats).DataSource = dtOperationsServicesBoats;
		BoatsInitGrid();
		((UltraGridBase)ULGDataVehicles).DataSource = dtOperationsServicesVechiles;
		VehiclesInitGrid();
		((UltraGridBase)ULGData).DataSource = dtOperationsServicesEquipments;
		EquipmentInitGrid();
		((UltraGridBase)ULGDataWorkers).DataSource = dtOperationsServicesWorkers;
		WorkersInitGrid();
		((Control)(object)btnSave).Enabled = !ReadOnly;
	}

	public void BoatsInitGrid()
	{
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGBoats);
		((UltraGridBase)ULGBoats).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGBoats).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGBoats).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["OperationServiceTransferBoatID"].DefaultCellValue = -1;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["VesselID"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["VesselID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["VesselID"].ValueList = (IValueList)(object)vlVessels;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["VesselID"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المركب" : "Boat Name");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Position"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Position"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Position"].Header).Caption = (GlobalVariables.IsArabic ? "الموقع" : "Position");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["LogSheetNo"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["LogSheetNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["LogSheetNo"].Header).Caption = (GlobalVariables.IsArabic ? "Log Sheet No" : "Log Sheet No");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DistanceInMiles"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DistanceInMiles"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DistanceInMiles"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DistanceInMiles"].Header).Caption = (GlobalVariables.IsArabic ? "المسافة ب الميل" : "Distance(Miles)");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DepartureDate"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DepartureDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DepartureDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DepartureDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["DepartureDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المغادرة" : "Departure Date");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ArrivalDate"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ArrivalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ArrivalDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ArrivalDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ArrivalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الوصول" : "Arrival Date");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["TotalPAX"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["TotalPAX"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["TotalPAX"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["TotalPAX"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الركاب" : "Total PAX");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ONCount"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ONCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ONCount"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["ONCount"].Header).Caption = (GlobalVariables.IsArabic ? "ON" : "ON");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["OFFCount"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["OFFCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["OFFCount"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["OFFCount"].Header).Caption = (GlobalVariables.IsArabic ? "OFF" : "OFF");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["RentAmount"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["RentAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["RentAmount"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["RentAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الايجار" : "Rent Amount");
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Remarks"].Hidden = false;
		((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Remarks"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGBoats).DisplayLayout.Bands[0].Columns["Remarks"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Remarks");
	}

	public void VehiclesInitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataVehicles);
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["OperationServiceVechileID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["VechileNo"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["VechileNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["VechileNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السيارة" : "Vechile No");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم السائق" : "Driver Name");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID"].ValueList = (IValueList)(object)vlDriverNationality;
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID"].Header).Caption = (GlobalVariables.IsArabic ? "جنسية السائق" : "Driver Nationality");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID"].Header).Caption = (GlobalVariables.IsArabic ? "هوية السائق" : "Driver ID");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم تليفون السائق" : "Driver Contact No");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName2"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverName2"].Header).Caption = (GlobalVariables.IsArabic ? "2إسم السائق" : "Driver Name2");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID2"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID2"].ValueList = (IValueList)(object)vlDriverNationality2;
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverNationalityID2"].Header).Caption = (GlobalVariables.IsArabic ? "جنسية السائق2" : "Driver Nationality2");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID2"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverID2"].Header).Caption = (GlobalVariables.IsArabic ? "هوية السائق2" : "Driver ID2");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo2"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["DriverContactNo2"].Header).Caption = (GlobalVariables.IsArabic ? "رقم تليفون السائق2" : "Driver Contact No2");
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["PurposeOfVisit"].Hidden = false;
		((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["PurposeOfVisit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVehicles).DisplayLayout.Bands[0].Columns["PurposeOfVisit"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الزياره" : "Purpose Of Visit");
	}

	public void EquipmentInitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceEquipmentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EquipmentName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EquipmentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EquipmentName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم المعدة" : "Equipment Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
	}

	public void WorkersInitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataWorkers);
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["OperationServiceSubAccountID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NameEn"].Hidden = false;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NameEn"].Width = (int)((double)((Control)(object)ULGDataWorkers).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NationalityID"].Hidden = false;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NationalityID"].Width = (int)((double)((Control)(object)ULGDataWorkers).Width * 0.2);
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NationalityID"].ValueList = (IValueList)(object)vlNationality;
		((HeaderBase)((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["NationalityID"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGDataWorkers).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No");
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGDataWorkers).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No");
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGDataWorkers).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataWorkers).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ إصدار الهوية" : "ID Issue Date");
	}

	public void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataWorkers_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataVehicles_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGBoats_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
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
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGBoats).Rows).Count; i++)
			{
				((UltraGridBase)ULGBoats).Rows[i].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGBoats).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGBoats).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGBoats).Rows[i].Cells["OperationServiceTransferBoatID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesTransferBoats", "OperationServiceID", OperationServiceID, "OperationServiceTransferBoatID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBoats).Rows).Count > 0)
			{
				OperationsServicesTransferBoats.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBoats).DataSource, GlobalVariables.UserID);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataVehicles).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataVehicles).Rows[j].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGDataVehicles).Rows[j].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGDataVehicles).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text2 = text2 + ((UltraGridBase)ULGDataVehicles).Rows[j].Cells["OperationServiceVechileID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesVechiles", "OperationServiceID", OperationServiceID, "OperationServiceVechileID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataVehicles).Rows).Count > 0)
			{
				OperationsServicesVechiles.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataVehicles).DataSource, GlobalVariables.UserID);
			}
			string text3 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				((UltraGridBase)ULGData).Rows[k].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGData).Rows[k].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGData).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text3 = text3 + ((UltraGridBase)ULGData).Rows[k].Cells["OperationServiceEquipmentID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesEquipments", "OperationServiceID", OperationServiceID, "OperationServiceEquipmentID", text3);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsServicesEquipments.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			string text4 = ",";
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataWorkers).Rows).Count; l++)
			{
				((UltraGridBase)ULGDataWorkers).Rows[l].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGDataWorkers).Rows[l].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGDataWorkers).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text4 = text4 + ((UltraGridBase)ULGDataWorkers).Rows[l].Cells["OperationServiceSubAccountID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesSubAccounts", "OperationServiceID", OperationServiceID, "OperationServiceSubAccountID", text4);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataWorkers).Rows).Count > 0)
			{
				OperationsServicesSubAccounts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataWorkers).DataSource, GlobalVariables.UserID);
			}
			Main.ExecuteNonQuery(" Update MS_OperationsServices  set SupplierSubAccountID= " + ((cboSupplier.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplier).Value.ToString()) + " Where OperationServiceID=" + OperationServiceID.ToString());
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

	private void btnSupplierSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Suppliers("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSupplier).Value = num;
		}
	}

	private bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGBoats).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGBoats).Rows[i].Cells["VesselID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار اسم المركب", "Please Select Boat Name");
				ULGBoats.ActiveCell = ((UltraGridBase)ULGBoats).Rows[i].Cells["VesselID"];
				return false;
			}
			if (((UltraGridBase)ULGBoats).Rows[i].Cells["DepartureDate"].Value != DBNull.Value && ((UltraGridBase)ULGBoats).Rows[i].Cells["ArrivalDate"].Value != DBNull.Value && Convert.ToDateTime(((UltraGridBase)ULGBoats).Rows[i].Cells["DepartureDate"].Value) > Convert.ToDateTime(((UltraGridBase)ULGBoats).Rows[i].Cells["ArrivalDate"].Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الرحيل بعد تاريخ الوصول", "The Departure Date is after the Arrival");
				ULGBoats.ActiveCell = ((UltraGridBase)ULGBoats).Rows[i].Cells["DepartureDate"];
				return false;
			}
		}
		return true;
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
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Expected O, but got Unknown
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Expected O, but got Unknown
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f39: Expected O, but got Unknown
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_1350: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesEquipmentsAndVechiles));
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
		UltraTab val47 = new UltraTab();
		UltraTab val48 = new UltraTab();
		UltraTab val49 = new UltraTab();
		UltraTab val50 = new UltraTab();
		Appearance val51 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataVehicles = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGData = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataWorkers = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGBoats = new UltraGrid();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.UTCDetails = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.lblSupplier = new UltraLabel();
		this.cboSupplier = new UltraComboEditor();
		this.btnSupplierSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataVehicles).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataWorkers).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBoats).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataVehicles);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGDataVehicles, "ULGDataVehicles");
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataVehicles).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataVehicles).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGDataVehicles).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.ULGDataVehicles).Name = "ULGDataVehicles";
		this.ULGDataVehicles.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataVehicles_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val11, "appearance11");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val12;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val17, "appearance17");
		((AppearanceBase)val17).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val18).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val18).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataWorkers);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataWorkers, "ULGDataWorkers");
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val21).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(val21, "appearance21");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataWorkers).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val21;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val22;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataWorkers).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val23).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.HighlightText;
		((AppearanceBase)val25).Image = resources.GetObject("appearance25.Image");
		resources.ApplyResources(val25, "appearance25");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val27, "appearance27");
		((AppearanceBase)val27).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val29).Image = resources.GetObject("appearance29.Image");
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGDataWorkers).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val30;
		((System.Windows.Forms.Control)(object)this.ULGDataWorkers).Name = "ULGDataWorkers";
		this.ULGDataWorkers.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataWorkers_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBoats);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGBoats, "ULGBoats");
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val31).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val31).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val31).Image = resources.GetObject("appearance31.Image");
		resources.ApplyResources(val31, "appearance31");
		((SpecialBoxBase)((UltraGridBase)this.ULGBoats).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val32, "appearance32");
		((UltraGridBase)this.ULGBoats).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val32;
		((SpecialBoxBase)((UltraGridBase)this.ULGBoats).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ULGBoats).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGBoats).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBoats).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val34, "appearance34");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val34;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val35).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val36, "appearance36");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val37, "appearance37");
		((AppearanceBase)val37).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val38).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val38).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val38).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val39).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val39).Image = resources.GetObject("appearance39.Image");
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val40, "appearance40");
		((UltraGridBase)this.ULGBoats).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val40;
		((System.Windows.Forms.Control)(object)this.ULGBoats).Name = "ULGBoats";
		this.ULGBoats.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGBoats_BeforeRowsDeleted);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val41).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val41, "appearance41");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val41;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val42).Image = resources.GetObject("appearance42.Image");
		resources.ApplyResources(val42, "appearance42");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val42;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val43).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val43, "appearance43");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val43;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val44).Image = resources.GetObject("appearance44.Image");
		resources.ApplyResources(val44, "appearance44");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val44;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val45).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val45).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val45).Image = resources.GetObject("appearance45.Image");
		resources.ApplyResources(val45, "appearance45");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.UTCDetails, "UTCDetails");
		resources.ApplyResources(val46, "appearance46");
		((AppearanceBase)val46).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCDetails).Appearance = (AppearanceBase)(object)val46;
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Name = "UTCDetails";
		((UltraTabControlBase)this.UTCDetails).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.UTCDetails).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val47).Key = "Vechiles";
		val47.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val47, "ultraTab3");
		((SubObjectBase)val47).ForceApplyResources = "";
		((KeyedSubObjectBase)val48).Key = "Equipments";
		val48.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val48, "ultraTab2");
		((SubObjectBase)val48).ForceApplyResources = "";
		((KeyedSubObjectBase)val49).Key = "Workers";
		val49.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val49, "ultraTab1");
		((SubObjectBase)val49).ForceApplyResources = "";
		((KeyedSubObjectBase)val50).Key = "Boats";
		val50.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val50, "ultraTab4");
		((SubObjectBase)val50).ForceApplyResources = "";
		((UltraTabControlBase)this.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[4] { val47, val48, val49, val50 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		resources.ApplyResources(this.btnSupplierSearch, "btnSupplierSearch");
		((AppearanceBase)val51).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val51, "appearance47");
		((ControlBase)this.btnSupplierSearch).Appearance = (AppearanceBase)(object)val51;
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Name = "btnSupplierSearch";
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Click += new System.EventHandler(btnSupplierSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSupplierSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmOperationsServicesEquipmentsAndVechiles";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSupplierSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataVehicles).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataWorkers).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGBoats).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
