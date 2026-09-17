using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewOnProcessTasks : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtUsers;

	private DataTable dtServices;

	private DataTable dtOnProces;

	private ValueList vlUsers = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGOnProces;

	private UltraLabel lblOnProcessCounterResult;

	private UltraLabel lblOnProcessCounter;

	public UltraButton btnRefreshData;

	public frmViewOnProcessTasks()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewOnProcessTasks(bool _IsSuperVisor, bool _CaUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CaUpdate;
		TableName = "MS_OperationsServicesStepsTasks";
	}

	private void frmViewShortPass_Load(object sender, EventArgs e)
	{
		FillGrid();
		InitGrid();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
		}
	}

	public void InitGrid()
	{
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGOnProces);
		((UltraGridBase)ULGOnProces).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGOnProces).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGOnProces).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGOnProces).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns).Exists("Details"))
		{
			((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns.Insert(0, "Details");
		}
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Header).Caption = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.05);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGOnProces).Rows).Count; i++)
		{
			((UltraGridBase)ULGOnProces).Rows[i].Cells["Details"].Value = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		}
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Details"].Index : ((DisposableObjectCollectionBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns).Count);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.06);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.05);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.06);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceName"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.08);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمه" : "Service");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StepName"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.05);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StepName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StepName"].Header).Caption = (GlobalVariables.IsArabic ? "المرحله" : "Step");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["TaskName"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.05);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["TaskName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["TaskName"].Header).Caption = (GlobalVariables.IsArabic ? "المهمه" : "Task");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["EntryPort"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.07);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["EntryPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["EntryPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الدخول" : "Entry Port");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsHold"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.04);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsHold"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsHold"].Header).Caption = (GlobalVariables.IsArabic ? "تأجيل" : "Hold");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.05);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "المندوب" : "PRO");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.1);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تبداء في" : "Starts at");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["StartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.04);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Done");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCancelled"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.04);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCancelled"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["IsCancelled"].Header).Caption = (GlobalVariables.IsArabic ? "الغاء" : "Cancell");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.1);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceUser"].Width = (int)((double)((Control)(object)ULGOnProces).Width * 0.08);
		((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceUser"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGOnProces).DisplayLayout.Bands[0].Columns["ServiceUser"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
	}

	public void FillGrid()
	{
		dtOnProces = OperationsServicesStepsTasks.Distribution(GlobalVariables.CurrentBranchID, "-1", "-1", "0", "1", "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGOnProces).DataSource = dtOnProces;
		((Control)(object)lblOnProcessCounterResult).Text = ((UltraGridBase)ULGOnProces).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGOnProces_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGOnProces.ActiveCell).Selected = true;
	}

	private void ULGOnProces_ClickCellButton(object sender, CellEventArgs e)
	{
		if (e.Cell.Row.Cells["OperationID"].Value.Equals(DBNull.Value) || !(((KeyedSubObjectBase)e.Cell.Column).Key == "Details") || ((UltraGridBase)ULGOnProces).ActiveRow == null || ((UltraGridBase)ULGOnProces).ActiveRow.Cells["ServiceID"].Value == DBNull.Value)
		{
			return;
		}
		DataRow dataRow = dtServices.Select(" ServiceID = " + ((UltraGridBase)ULGOnProces).ActiveRow.Cells["ServiceID"].Value.ToString())[0];
		if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "2")
		{
			frmOperationsServicesVisas frmOperationsServicesVisas2 = new frmOperationsServicesVisas(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
			frmOperationsServicesVisas2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesVisas2.lblTitle).Text = (GlobalVariables.IsArabic ? "تأشيرات" : "Visa");
			frmOperationsServicesVisas2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "3")
		{
			frmOperationsServicesCrew frmOperationsServicesCrew2 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: true, READONLY: true);
			frmOperationsServicesCrew2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
			frmOperationsServicesCrew2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "5")
		{
			frmOperationsServicesCustoms frmOperationsServicesCustoms2 = new frmOperationsServicesCustoms(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesCustoms2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesCustoms2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص جمركى" : "Custom Clearence");
			frmOperationsServicesCustoms2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "6")
		{
			frmOperationsServicesTickets frmOperationsServicesTickets2 = new frmOperationsServicesTickets(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesTickets2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز الطيران" : "Ticketings");
			frmOperationsServicesTickets2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "7")
		{
			frmOperationsServicesTransfers frmOperationsServicesTransfers2 = new frmOperationsServicesTransfers(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
			frmOperationsServicesTransfers2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesTransfers2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل من باخرة الى باخرة" : "Vessel Transfer");
			frmOperationsServicesTransfers2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "8")
		{
			frmOperationsServicesEquipmentsAndVechiles frmOperationsServicesEquipmentsAndVechiles2 = new frmOperationsServicesEquipmentsAndVechiles(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
			frmOperationsServicesEquipmentsAndVechiles2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesEquipmentsAndVechiles2.lblTitle).Text = (GlobalVariables.IsArabic ? "المعدات و المركبات" : "Equipments And Vechiles");
			frmOperationsServicesEquipmentsAndVechiles2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "9")
		{
			frmOperationsServicesTransportations frmOperationsServicesTransportations2 = new frmOperationsServicesTransportations(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesTransportations2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesTransportations2.lblTitle).Text = (GlobalVariables.IsArabic ? "توصيل" : "Transportations");
			frmOperationsServicesTransportations2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "10")
		{
			frmOperationsServicesHotelsBooking frmOperationsServicesHotelsBooking2 = new frmOperationsServicesHotelsBooking(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesHotelsBooking2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesHotelsBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز فندق" : "Hotel Booking");
			frmOperationsServicesHotelsBooking2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "11")
		{
			frmOperationsServicesCrew frmOperationsServicesCrew3 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: false, READONLY: true);
			frmOperationsServicesCrew3.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesCrew3.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
			frmOperationsServicesCrew3.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "12")
		{
			frmOperationsServicesCargosExport frmOperationsServicesCargosExport2 = new frmOperationsServicesCargosExport(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesCargosExport2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesCargosExport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الصادرة" : "Export Cargos");
			frmOperationsServicesCargosExport2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "13")
		{
			frmOperationsServicesCargosImport frmOperationsServicesCargosImport2 = new frmOperationsServicesCargosImport(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesCargosImport2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesCargosImport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الواردة" : "Import Cargos");
			frmOperationsServicesCargosImport2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "14")
		{
			frmOperationsServicesSupply frmOperationsServicesSupply2 = new frmOperationsServicesSupply(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
			frmOperationsServicesSupply2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesSupply2.lblTitle).Text = (GlobalVariables.IsArabic ? "المهمات" : "Supply");
			frmOperationsServicesSupply2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "15")
		{
			frmOperationsServicesChangeVessel frmOperationsServicesChangeVessel2 = new frmOperationsServicesChangeVessel(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesChangeVessel2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesChangeVessel2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الباخرة" : "Change Vessel");
			frmOperationsServicesChangeVessel2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "16")
		{
			frmOperationsServicesAgentCorrection frmOperationsServicesAgentCorrection2 = new frmOperationsServicesAgentCorrection(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesAgentCorrection2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesAgentCorrection2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الوكيل والباخرة" : "Correction Of Agent & Vessel");
			frmOperationsServicesAgentCorrection2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "17")
		{
			frmOperationsServicesShipToShip frmOperationsServicesShipToShip2 = new frmOperationsServicesShipToShip(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesShipToShip2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesShipToShip2.lblTitle).Text = (GlobalVariables.IsArabic ? "من باخرة لباخرة" : "Ship To Ship");
			frmOperationsServicesShipToShip2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "18")
		{
			frmOperationsServicesSkipReloading frmOperationsServicesSkipReloading2 = new frmOperationsServicesSkipReloading(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesSkipReloading2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesSkipReloading2.lblTitle).Text = (GlobalVariables.IsArabic ? "إعادة تحميل" : "Skip Reloading");
			frmOperationsServicesSkipReloading2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "19")
		{
			frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer2 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: true, READONLY: true);
			frmOperationsServicesAgencyTransfer2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesAgencyTransfer2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكيل" : "Agency Transfer From another Agent");
			frmOperationsServicesAgencyTransfer2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "20")
		{
			frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer3 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: false, READONLY: true);
			frmOperationsServicesAgencyTransfer3.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesAgencyTransfer3.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكالتنا" : "Agency Transfer To another Agent");
			frmOperationsServicesAgencyTransfer3.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "21")
		{
			frmOperationsServicesShortPass frmOperationsServicesShortPass2 = new frmOperationsServicesShortPass(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesShortPass2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesShortPass2.lblTitle).Text = (GlobalVariables.IsArabic ? "تصريح مؤقت" : "Short Pass");
			frmOperationsServicesShortPass2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "22")
		{
			frmOperationsServicesVisasCancellation frmOperationsServicesVisasCancellation2 = new frmOperationsServicesVisasCancellation(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
			frmOperationsServicesVisasCancellation2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesVisasCancellation2.lblTitle).Text = (GlobalVariables.IsArabic ? "التإشيرات الملغاة" : "Visa Cancellation");
			frmOperationsServicesVisasCancellation2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "23")
		{
			frmOperationsServicesVisasSubmissions frmOperationsServicesVisasSubmissions2 = new frmOperationsServicesVisasSubmissions(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesVisasSubmissions2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesVisasSubmissions2.lblTitle).Text = (GlobalVariables.IsArabic ? "تقديم التأشيرات" : "Visa Submissions");
			frmOperationsServicesVisasSubmissions2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "24")
		{
			frmOperationsServicesVisasClearingOverStay frmOperationsServicesVisasClearingOverStay2 = new frmOperationsServicesVisasClearingOverStay(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesVisasClearingOverStay2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesVisasClearingOverStay2.lblTitle).Text = (GlobalVariables.IsArabic ? " تمديد اقامة" : "Visa Clearing Over Stay");
			frmOperationsServicesVisasClearingOverStay2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "25")
		{
			frmOperationsServicesSeaManBooking frmOperationsServicesSeaManBooking2 = new frmOperationsServicesSeaManBooking(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesSeaManBooking2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesSeaManBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز للبحار" : "SeaMan Booking");
			frmOperationsServicesSeaManBooking2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "26")
		{
			frmOperationsServicesMedicalAssistance frmOperationsServicesMedicalAssistance2 = new frmOperationsServicesMedicalAssistance(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesMedicalAssistance2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesMedicalAssistance2.lblTitle).Text = (GlobalVariables.IsArabic ? "رعاية صحية" : "Medical Assistance");
			frmOperationsServicesMedicalAssistance2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && (dataRow["ServiceTypeID"].ToString() == "27" || dataRow["ServiceTypeID"].ToString() == "30" || dataRow["ServiceTypeID"].ToString() == "31"))
		{
			frmOperationsServicesPassengersClearance frmOperationsServicesPassengersClearance2 = new frmOperationsServicesPassengersClearance(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesPassengersClearance2.WindowState = FormWindowState.Maximized;
			if (dataRow["ServiceTypeID"].ToString() == "27")
			{
				((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص الركاب " : "Passengers Clearance");
			}
			else if (dataRow["ServiceTypeID"].ToString() == "30")
			{
				((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص دخول فنيين " : "Technicians Inward Clearance");
			}
			else if (dataRow["ServiceTypeID"].ToString() == "31")
			{
				((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص خروج فنيين " : "Technicians Outward Clearance");
			}
			frmOperationsServicesPassengersClearance2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "28")
		{
			frmOperationsServicesVisasRejection frmOperationsServicesVisasRejection2 = new frmOperationsServicesVisasRejection(dtServices, ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
			frmOperationsServicesVisasRejection2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesVisasRejection2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات المرفوضة" : "Visa Rejection");
			frmOperationsServicesVisasRejection2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "29")
		{
			frmOperationsServicesAgencyFromDateToDate frmOperationsServicesAgencyFromDateToDate2 = new frmOperationsServicesAgencyFromDateToDate(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesAgencyFromDateToDate2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesAgencyFromDateToDate2.lblTitle).Text = (GlobalVariables.IsArabic ? "وكالة من تاريخ لتاريخ" : "Agency From Date To Date");
			frmOperationsServicesAgencyFromDateToDate2.ShowDialog();
		}
		else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "100")
		{
			frmOperationsServicesOthers frmOperationsServicesOthers2 = new frmOperationsServicesOthers(((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
			frmOperationsServicesOthers2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmOperationsServicesOthers2.lblTitle).Text = (GlobalVariables.IsArabic ? "خدمات أخرى" : "Other Services");
			frmOperationsServicesOthers2.ShowDialog();
		}
	}

	private void ULGOnProces_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGOnProces).ActiveRow != null)
		{
			bool flag = true;
			string iD;
			if (((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceStepTaskID"].Value.ToString() == "")
			{
				iD = ((UltraGridBase)ULGOnProces).ActiveRow.Cells["TaskWithoutOperationID"].Value.ToString();
				flag = false;
			}
			else
			{
				iD = ((UltraGridBase)ULGOnProces).ActiveRow.Cells["OperationServiceStepTaskID"].Value.ToString();
				flag = true;
			}
			frmUpdateSuperVisorTask frmUpdateSuperVisorTask2 = new frmUpdateSuperVisorTask(iD, flag);
			frmUpdateSuperVisorTask2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateSuperVisorTask2.lblTitle).Text = (GlobalVariables.IsArabic ? " جاري تنفيذها" : "On Process");
			frmUpdateSuperVisorTask2.ShowDialog();
			FillGrid();
			InitGrid();
		}
	}

	private void ULGOnProces_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		if (e.KeyCode != Keys.F11)
		{
			return;
		}
		if (ULGOnProces.ActiveCell.ValueList != null)
		{
			frmValueListSearch frmValueListSearch2 = new frmValueListSearch((ValueList)ULGOnProces.ActiveCell.ValueList, ((object)ULGOnProces.ActiveCell.Column.Header).ToString());
			frmValueListSearch2.WindowState = FormWindowState.Normal;
			frmValueListSearch2.ShowDialog();
			if (frmValueListSearch2.ResultID > 0)
			{
				ULGOnProces.ActiveCell.Value = frmValueListSearch2.ResultID;
			}
		}
		else if (ULGOnProces.ActiveCell.Column.ValueList != null)
		{
			frmValueListSearch frmValueListSearch3 = new frmValueListSearch((ValueList)ULGOnProces.ActiveCell.Column.ValueList, ((HeaderBase)ULGOnProces.ActiveCell.Column.Header).Caption);
			frmValueListSearch3.WindowState = FormWindowState.Normal;
			frmValueListSearch3.ShowDialog();
			if (frmValueListSearch3.ResultID > 0)
			{
				ULGOnProces.ActiveCell.Value = frmValueListSearch3.ResultID;
			}
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
		InitGrid();
	}

	private void ULGOnProces_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblOnProcessCounterResult).Text = ((UltraGridBase)ULGOnProces).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Expected O, but got Unknown
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewOnProcessTasks));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGOnProces = new UltraGrid();
		this.lblOnProcessCounterResult = new UltraLabel();
		this.lblOnProcessCounter = new UltraLabel();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGOnProces).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGOnProces, "ULGOnProces");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGOnProces).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGOnProces).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGOnProces).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGOnProces).Name = "ULGOnProces";
		this.ULGOnProces.AfterEnterEditMode += new System.EventHandler(ULGOnProces_AfterEnterEditMode);
		this.ULGOnProces.ClickCellButton += new CellEventHandler(ULGOnProces_ClickCellButton);
		((UltraGridBase)this.ULGOnProces).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGOnProces_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGOnProces).DoubleClick += new System.EventHandler(ULGOnProces_DoubleClick);
		((System.Windows.Forms.Control)(object)this.ULGOnProces).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGOnProces_KeyDown);
		resources.ApplyResources(this.lblOnProcessCounterResult, "lblOnProcessCounterResult");
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblOnProcessCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblOnProcessCounterResult).Name = "lblOnProcessCounterResult";
		resources.ApplyResources(this.lblOnProcessCounter, "lblOnProcessCounter");
		this.lblOnProcessCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOnProcessCounter).Name = "lblOnProcessCounter";
		((ControlBase)this.lblOnProcessCounter).WrapText = false;
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnProcessCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnProcessCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGOnProces);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewOnProcessTasks";
		base.Load += new System.EventHandler(frmViewShortPass_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGOnProces, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnProcessCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnProcessCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGOnProces).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
