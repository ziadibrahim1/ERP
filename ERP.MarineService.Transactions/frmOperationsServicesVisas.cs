using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesVisas : frmDetails
{
	private DataTable dtServices;

	private DataTable dtOperationSubAccountSeaMen;

	private DataTable dtVisaState;

	private DataTable dtAirLines;

	private DataTable dtCompanies;

	private DataTable dtSettings;

	private ValueList vlOperationSubAccountSeaMen = new ValueList();

	private ValueList vlVisaState = new ValueList();

	private ValueList vlRejectionReasons = new ValueList();

	private ValueList vlAirLines = new ValueList();

	private ValueList vlCompanies = new ValueList();

	private ValueList vlInOut = new ValueList();

	private ValueList vlPassengerType = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private string VesselID;

	private bool ReadOnly;

	public int AcceptedQty = 0;

	public int RefusedQty = 0;

	public int RefusedWithReasonQty = 0;

	private DataRow drCurrentService;

	private bool NoInvoice;

	private IContainer components = null;

	private UltraButton btnSelectVisas;

	public UltraButton btnNewVisa;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	public frmOperationsServicesVisas()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الخدمة" : "Service Name");
	}

	public frmOperationsServicesVisas(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, string VESSELID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		VesselID = VESSELID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		drCurrentService = OperationsServices.Select(OperationServiceID, "-1", GlobalVariables.IsArabic ? "1" : "0").Rows[0];
		NoInvoice = ((drCurrentService["OperationInvoiceID"] == DBNull.Value) ? true : false);
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtVisaState = VisaStates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlVisaState.ValueListItems.Clear();
		for (int i = 0; i < dtVisaState.Rows.Count; i++)
		{
			vlVisaState.ValueListItems.Add(dtVisaState.Rows[i]["VisaStateID"], dtVisaState.Rows[i]["VisaStateName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCompanies.ValueListItems.Clear();
		for (int j = 0; j < dtCompanies.Rows.Count; j++)
		{
			vlCompanies.ValueListItems.Add(dtCompanies.Rows[j]["CompanyID"], dtCompanies.Rows[j]["CompanyName"].ToString());
		}
		dtAirLines = AirLines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAirLines.ValueListItems.Clear();
		for (int k = 0; k < dtAirLines.Rows.Count; k++)
		{
			vlAirLines.ValueListItems.Add(dtAirLines.Rows[k]["AirLineID"], dtAirLines.Rows[k]["AirLineName"].ToString());
		}
		vlInOut.ValueListItems.Clear();
		vlInOut.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "On" : "On");
		vlInOut.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "OFF" : "OFF");
		vlPassengerType.ValueListItems.Clear();
		vlPassengerType.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerType.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerType.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
		dtSettings = Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UltraButton obj = btnHeaderSearch;
		UltraButton obj2 = btnNext;
		bool flag = (((Control)(object)btnPriveous).Visible = false);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)cboHeader).ReadOnly = true;
		((TextEditorControlBase)cboHeader).Value = ServiceID;
		if (ReadOnly)
		{
			UltraButton obj3 = btnCancel;
			UltraButton obj4 = btnSave;
			UltraButton obj5 = btnSaveAndClose;
			UltraButton obj6 = btnSelectVisas;
			bool flag4 = (((Control)(object)btnNewVisa).Enabled = false);
			bool flag6 = (((Control)(object)obj6).Enabled = flag4);
			flag = (((Control)(object)obj5).Enabled = flag6);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlOperationSubAccountSeaMen;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).Caption = (GlobalVariables.IsArabic ? "ON/OFF" : "ON/OFF");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].ValueList = (IValueList)(object)vlInOut;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة" : "Visa Status");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].ValueList = (IValueList)(object)vlVisaState;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaStateID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفيزا" : "Serial No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceVisaNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "صالح من" : "Issue Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Header).Caption = (GlobalVariables.IsArabic ? "صالح الى" : "Expiry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الدخول" : "Entry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Exit Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirLineID"].Header).Caption = (GlobalVariables.IsArabic ? "خط الطيران" : "AirLine");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirLineID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirLineID"].ValueList = (IValueList)(object)vlAirLines;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirLineID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تام" : "Completed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].ValueList = (IValueList)(object)vlCompanies;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Header).Caption = (GlobalVariables.IsArabic ? "عاجل" : "Urgent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Header).Caption = (GlobalVariables.IsArabic ? "الخروج المتوقع" : "Entry Expire");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrintOut"].Header).Caption = (GlobalVariables.IsArabic ? "طبعت" : "PrintOut");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة التأشيرة" : "Visa Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintOutCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة الطباعه" : "PrintOut Cost");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtOperationSubAccountSeaMen = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.AgentSubAccountTypeIDs + GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs + GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlOperationSubAccountSeaMen.ValueListItems.Clear();
		for (int i = 0; i < dtOperationSubAccountSeaMen.Rows.Count; i++)
		{
			vlOperationSubAccountSeaMen.ValueListItems.Add(dtOperationSubAccountSeaMen.Rows[i]["SubAccountID"], dtOperationSubAccountSeaMen.Rows[i]["SubAccountName"].ToString());
		}
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesVisas.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		if (!NoInvoice && Convert.ToInt32(drCurrentService["Qty"]) != ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار البحار", "Please Select Sea Man");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaStateID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار حالة الفيزا", "Please Select Visa Status ");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["VisaStateID"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار إسم البحار", "Cannot Duplicate The Same SeaMan");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void SaveData()
	{
		if (NoInvoice)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
					((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceVisaID"].Value.ToString() + ",";
				}
				OperationsServicesVisas.DeleteForUpdate(text, OperationServiceID);
				DataTable dt = (DataTable)((UltraGridBase)ULGData).DataSource;
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
				{
					OperationsServicesVisas.UpdateByTable_ForOperation(dt, GlobalVariables.UserID);
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					OperationsServicesVisas.GenerateJvs(((UltraGridBase)ULGData).Rows[j].Cells["OperationServiceVisaID"].Value.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				OperationsServices.UpdateTotalExpenses(OperationServiceID, GlobalVariables.UserID);
				OperationsServices.UpdateTotals(OperationServiceID, ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString());
				Main.EndBulkTrans(FromServer: false);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
				SaveError = true;
				return;
			}
		}
		GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (e.Cell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "RejectionReasonID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["IsRejected"].Value = true;
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsRejected")
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["RejectionReasonID"].Value = ((!bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsRejected"].Value.ToString())) ? DBNull.Value : ((UltraGridBase)ULGData).ActiveRow.Cells["RejectionReasonID"].Value);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnSelectVisas_Click(object sender, EventArgs e)
	{
		if (NoInvoice)
		{
			dtSearchResult = SearchFunctions.VisasWithoutOperationsReport(VesselID, IsFromServer: false);
			if (dtSearchResult.Rows.Count <= 0)
			{
				return;
			}
			string text = ",";
			foreach (DataRow row in dtSearchResult.Rows)
			{
				text = text + row["OperationServiceVisaID"].ToString() + ",";
			}
			DataTable dataTable = OperationsServicesVisas.SelectByOperationServiceVisaIDs(text, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row2 in dataTable.Rows)
			{
				row2["OperationID"] = OperationID;
				row2["OperationServiceID"] = OperationServiceID;
				dtDetails.Rows.Add(row2.ItemArray);
			}
			((UltraGridBase)ULGData).UpdateData();
			((Control)(object)btnSave).Enabled = true;
			((Control)(object)btnSaveAndClose).Enabled = true;
			((Control)(object)btnCancel).Enabled = true;
			HasChanges = true;
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
		}
	}

	private void btnNewVisa_Click(object sender, EventArgs e)
	{
		if (NoInvoice)
		{
			frmInsertSeaManVisa frmInsertSeaManVisa2 = new frmInsertSeaManVisa(OperationID, OperationServiceID, VesselID);
			frmInsertSeaManVisa2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			((Control)(object)frmInsertSeaManVisa2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات" : "Visas");
			frmInsertSeaManVisa2.Location = new Point(0, 0);
			frmInsertSeaManVisa2.ShowDialog();
			DisplayData();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void ULGData_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	public bool HasTransactionValidation(string OperationServiceVisaIDs)
	{
		string text = OperationsServicesVisas.CheckForRelations(OperationServiceVisaIDs, "-1", GlobalVariables.IsArabic ? "1" : "0").Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return false;
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		string text = ",";
		RowEnumerator enumerator = ULGData.Selected.Rows.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				UltraGridRow current = enumerator.Current;
				text = text + current.Cells["OperationServiceVisaID"].Value.ToString() + ",";
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		if (HasTransactionValidation(text))
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
		base.ULGData_BeforeRowsDeleted(sender, e);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
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
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Expected O, but got Unknown
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesVisas));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		this.btnSelectVisas = new UltraButton();
		this.btnNewVisa = new UltraButton();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		resources.ApplyResources(this.btnSelectVisas, "btnSelectVisas");
		((System.Windows.Forms.Control)(object)this.btnSelectVisas).Name = "btnSelectVisas";
		((System.Windows.Forms.Control)(object)this.btnSelectVisas).Click += new System.EventHandler(btnSelectVisas_Click);
		resources.ApplyResources(this.btnNewVisa, "btnNewVisa");
		((AppearanceBase)val10).Image = resources.GetObject("appearance10.Image");
		((ControlBase)this.btnNewVisa).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnNewVisa).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewVisa).Name = "btnNewVisa";
		((System.Windows.Forms.Control)(object)this.btnNewVisa).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewVisa).Click += new System.EventHandler(btnNewVisa_Click);
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectVisas);
		base.Name = "frmOperationsServicesVisas";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectVisas, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
