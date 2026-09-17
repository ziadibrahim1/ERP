using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewNewTasks : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtUsers;

	private DataTable dtServices;

	private DataTable dtsource;

	private ValueList vlUsers = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGNewTasks;

	private UltraLabel lblNewTasksCounterResult;

	private UltraLabel lblNewTasksCounter;

	public UltraButton btnSave;

	public UltraButton btnRefreshData;

	public frmViewNewTasks()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewNewTasks(bool _IsSuperVisor)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
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
		//IL_1317: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGNewTasks);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		if (IsSuperVisor)
		{
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsHold"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.04);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsHold"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsHold"].Header).Caption = (GlobalVariables.IsArabic ? "تأجيل" : "Hold");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ForAllUsers"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.03);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ForAllUsers"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ForAllUsers"].Header).Caption = (GlobalVariables.IsArabic ? "للكل" : "For All");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.04);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "المندوب" : "PRO");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCancelled"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.04);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCancelled"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCancelled"].Header).Caption = (GlobalVariables.IsArabic ? "الغاء" : "Cancell");
		}
		else
		{
			if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Exists("Reports"))
			{
				((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns.Insert(0, "Reports");
			}
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Header).Caption = (GlobalVariables.IsArabic ? "الخطابات" : "Letters");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Style = (ColumnStyle)8;
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).Rows).Count; i++)
			{
				((UltraGridBase)ULGNewTasks).Rows[i].Cells["Reports"].Value = (GlobalVariables.IsArabic ? "الخطابات" : "Letters");
			}
			if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Exists("Documents"))
			{
				((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns.Insert(0, "Documents");
			}
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Header).Caption = (GlobalVariables.IsArabic ? "مستندات" : "Documents");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Style = (ColumnStyle)8;
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).Rows).Count; j++)
			{
				((UltraGridBase)ULGNewTasks).Rows[j].Cells["Documents"].Value = (GlobalVariables.IsArabic ? "مستندات" : "Documents");
			}
			if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Exists("Expenses"))
			{
				((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns.Insert(0, "Expenses");
			}
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Header).Caption = (GlobalVariables.IsArabic ? "مصروفات" : "Expenses");
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Style = (ColumnStyle)8;
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).Rows).Count; k++)
			{
				((UltraGridBase)ULGNewTasks).Rows[k].Cells["Expenses"].Value = (GlobalVariables.IsArabic ? "مصروفات" : "Expenses");
			}
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Reports"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Count - 3));
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Documents"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Count - 2));
			((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Expenses"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Count - 1));
		}
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Exists("Details"))
		{
			((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns.Insert(0, "Details");
		}
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Header).Caption = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).Rows).Count; l++)
		{
			((UltraGridBase)ULGNewTasks).Rows[l].Cells["Details"].Value = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		}
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Details"].Index : ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns).Count);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.06);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.06);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.06);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمه" : "Service");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StepName"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StepName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StepName"].Header).Caption = (GlobalVariables.IsArabic ? "المرحله" : "Step");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["TaskName"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.06);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["TaskName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["TaskName"].Header).Caption = (GlobalVariables.IsArabic ? "المهمه" : "Task");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["EntryPort"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.06);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["EntryPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["EntryPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الدخول" : "Entry Port");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.1);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تبداء في" : "Starts at");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["StartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.05);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Done");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.1);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceUser"].Width = (int)((double)((Control)(object)ULGNewTasks).Width * 0.07);
		((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceUser"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGNewTasks).DisplayLayout.Bands[0].Columns["ServiceUser"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
	}

	public void FillGrid()
	{
		if (IsSuperVisor)
		{
			dtsource = OperationsServicesStepsTasks.Distribution(GlobalVariables.CurrentBranchID, "-1", "-1", "0", "0", "0", GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtsource = OperationsServicesStepsTasks.Distribution(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.UserID, "0", "1", "0", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGNewTasks).DataSource = dtsource;
		((Control)(object)lblNewTasksCounterResult).Text = ((UltraGridBase)ULGNewTasks).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGNewTasks_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (IsSuperVisor)
		{
			if (((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "OperationNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "VesselName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "VoyageNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "ServiceName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "StepName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "TaskName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "EntryPort" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "OperationServiceNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "ServiceUser")
			{
				((GridItemBase)ULGNewTasks.ActiveCell).Selected = true;
			}
		}
		else if (((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "OperationNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "VesselName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "VoyageNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "ServiceName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "StepName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "StartDate" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "TaskName" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "EntryPort" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "OperationServiceNo" || ((KeyedSubObjectBase)ULGNewTasks.ActiveCell.Column).Key == "ServiceUser")
		{
			((GridItemBase)ULGNewTasks.ActiveCell).Selected = true;
		}
	}

	private void ULGNewTasks_ClickCellButton(object sender, CellEventArgs e)
	{
		if (IsSuperVisor)
		{
			if (e.Cell.Row.Cells["OperationID"].Value.Equals(DBNull.Value) || !(((KeyedSubObjectBase)e.Cell.Column).Key == "Details") || ((UltraGridBase)ULGNewTasks).ActiveRow == null || ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["ServiceID"].Value == DBNull.Value)
			{
				return;
			}
			DataRow dataRow = dtServices.Select(" ServiceID = " + ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["ServiceID"].Value.ToString())[0];
			if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "2")
			{
				frmOperationsServicesVisas frmOperationsServicesVisas2 = new frmOperationsServicesVisas(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
				frmOperationsServicesVisas2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisas2.lblTitle).Text = (GlobalVariables.IsArabic ? "تأشيرات" : "Visa");
				frmOperationsServicesVisas2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "3")
			{
				frmOperationsServicesCrew frmOperationsServicesCrew2 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: true, READONLY: true);
				frmOperationsServicesCrew2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
				frmOperationsServicesCrew2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "5")
			{
				frmOperationsServicesCustoms frmOperationsServicesCustoms2 = new frmOperationsServicesCustoms(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesCustoms2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCustoms2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص جمركى" : "Custom Clearence");
				frmOperationsServicesCustoms2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "6")
			{
				frmOperationsServicesTickets frmOperationsServicesTickets2 = new frmOperationsServicesTickets(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesTickets2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز الطيران" : "Ticketings");
				frmOperationsServicesTickets2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "7")
			{
				frmOperationsServicesTransfers frmOperationsServicesTransfers2 = new frmOperationsServicesTransfers(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
				frmOperationsServicesTransfers2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTransfers2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل من باخرة الى باخرة" : "Vessel Transfer");
				frmOperationsServicesTransfers2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "8")
			{
				frmOperationsServicesEquipmentsAndVechiles frmOperationsServicesEquipmentsAndVechiles2 = new frmOperationsServicesEquipmentsAndVechiles(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
				frmOperationsServicesEquipmentsAndVechiles2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesEquipmentsAndVechiles2.lblTitle).Text = (GlobalVariables.IsArabic ? "المعدات و المركبات" : "Equipments And Vechiles");
				frmOperationsServicesEquipmentsAndVechiles2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "9")
			{
				frmOperationsServicesTransportations frmOperationsServicesTransportations2 = new frmOperationsServicesTransportations(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesTransportations2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTransportations2.lblTitle).Text = (GlobalVariables.IsArabic ? "توصيل" : "Transportations");
				frmOperationsServicesTransportations2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "10")
			{
				frmOperationsServicesHotelsBooking frmOperationsServicesHotelsBooking2 = new frmOperationsServicesHotelsBooking(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesHotelsBooking2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesHotelsBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز فندق" : "Hotel Booking");
				frmOperationsServicesHotelsBooking2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "11")
			{
				frmOperationsServicesCrew frmOperationsServicesCrew3 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: false, READONLY: true);
				frmOperationsServicesCrew3.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCrew3.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
				frmOperationsServicesCrew3.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "12")
			{
				frmOperationsServicesCargosExport frmOperationsServicesCargosExport2 = new frmOperationsServicesCargosExport(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesCargosExport2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCargosExport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الصادرة" : "Export Cargos");
				frmOperationsServicesCargosExport2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "13")
			{
				frmOperationsServicesCargosImport frmOperationsServicesCargosImport2 = new frmOperationsServicesCargosImport(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesCargosImport2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCargosImport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الواردة" : "Import Cargos");
				frmOperationsServicesCargosImport2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "14")
			{
				frmOperationsServicesSupply frmOperationsServicesSupply2 = new frmOperationsServicesSupply(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
				frmOperationsServicesSupply2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSupply2.lblTitle).Text = (GlobalVariables.IsArabic ? "المهمات" : "Supply");
				frmOperationsServicesSupply2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "15")
			{
				frmOperationsServicesChangeVessel frmOperationsServicesChangeVessel2 = new frmOperationsServicesChangeVessel(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesChangeVessel2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesChangeVessel2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الباخرة" : "Change Vessel");
				frmOperationsServicesChangeVessel2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "16")
			{
				frmOperationsServicesAgentCorrection frmOperationsServicesAgentCorrection2 = new frmOperationsServicesAgentCorrection(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesAgentCorrection2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgentCorrection2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الوكيل والباخرة" : "Correction Of Agent & Vessel");
				frmOperationsServicesAgentCorrection2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "17")
			{
				frmOperationsServicesShipToShip frmOperationsServicesShipToShip2 = new frmOperationsServicesShipToShip(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesShipToShip2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesShipToShip2.lblTitle).Text = (GlobalVariables.IsArabic ? "من باخرة لباخرة" : "Ship To Ship");
				frmOperationsServicesShipToShip2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "18")
			{
				frmOperationsServicesSkipReloading frmOperationsServicesSkipReloading2 = new frmOperationsServicesSkipReloading(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesSkipReloading2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSkipReloading2.lblTitle).Text = (GlobalVariables.IsArabic ? "إعادة تحميل" : "Skip Reloading");
				frmOperationsServicesSkipReloading2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "19")
			{
				frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer2 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: true, READONLY: true);
				frmOperationsServicesAgencyTransfer2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyTransfer2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكيل" : "Agency Transfer From another Agent");
				frmOperationsServicesAgencyTransfer2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "20")
			{
				frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer3 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: false, READONLY: true);
				frmOperationsServicesAgencyTransfer3.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyTransfer3.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكالتنا" : "Agency Transfer To another Agent");
				frmOperationsServicesAgencyTransfer3.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "21")
			{
				frmOperationsServicesShortPass frmOperationsServicesShortPass2 = new frmOperationsServicesShortPass(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesShortPass2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesShortPass2.lblTitle).Text = (GlobalVariables.IsArabic ? "تصريح مؤقت" : "Short Pass");
				frmOperationsServicesShortPass2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "22")
			{
				frmOperationsServicesVisasCancellation frmOperationsServicesVisasCancellation2 = new frmOperationsServicesVisasCancellation(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
				frmOperationsServicesVisasCancellation2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasCancellation2.lblTitle).Text = (GlobalVariables.IsArabic ? "التإشيرات الملغاة" : "Visa Cancellation");
				frmOperationsServicesVisasCancellation2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "23")
			{
				frmOperationsServicesVisasSubmissions frmOperationsServicesVisasSubmissions2 = new frmOperationsServicesVisasSubmissions(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesVisasSubmissions2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasSubmissions2.lblTitle).Text = (GlobalVariables.IsArabic ? "تقديم التأشيرات" : "Visa Submissions");
				frmOperationsServicesVisasSubmissions2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "24")
			{
				frmOperationsServicesVisasClearingOverStay frmOperationsServicesVisasClearingOverStay2 = new frmOperationsServicesVisasClearingOverStay(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesVisasClearingOverStay2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasClearingOverStay2.lblTitle).Text = (GlobalVariables.IsArabic ? " تمديد اقامة" : "Visa Clearing Over Stay");
				frmOperationsServicesVisasClearingOverStay2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "25")
			{
				frmOperationsServicesSeaManBooking frmOperationsServicesSeaManBooking2 = new frmOperationsServicesSeaManBooking(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesSeaManBooking2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSeaManBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز للبحار" : "SeaMan Booking");
				frmOperationsServicesSeaManBooking2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "26")
			{
				frmOperationsServicesMedicalAssistance frmOperationsServicesMedicalAssistance2 = new frmOperationsServicesMedicalAssistance(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesMedicalAssistance2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesMedicalAssistance2.lblTitle).Text = (GlobalVariables.IsArabic ? "رعاية صحية" : "Medical Assistance");
				frmOperationsServicesMedicalAssistance2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && (dataRow["ServiceTypeID"].ToString() == "27" || dataRow["ServiceTypeID"].ToString() == "30" || dataRow["ServiceTypeID"].ToString() == "31"))
			{
				frmOperationsServicesPassengersClearance frmOperationsServicesPassengersClearance2 = new frmOperationsServicesPassengersClearance(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
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
				frmOperationsServicesVisasRejection frmOperationsServicesVisasRejection2 = new frmOperationsServicesVisasRejection(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
				frmOperationsServicesVisasRejection2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasRejection2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات المرفوضة" : "Visa Rejection");
				frmOperationsServicesVisasRejection2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "29")
			{
				frmOperationsServicesAgencyFromDateToDate frmOperationsServicesAgencyFromDateToDate2 = new frmOperationsServicesAgencyFromDateToDate(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesAgencyFromDateToDate2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyFromDateToDate2.lblTitle).Text = (GlobalVariables.IsArabic ? "وكالة من تاريخ لتاريخ" : "Agency From Date To Date");
				frmOperationsServicesAgencyFromDateToDate2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "100")
			{
				frmOperationsServicesOthers frmOperationsServicesOthers2 = new frmOperationsServicesOthers(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow["ServiceID"].ToString(), READONLY: true);
				frmOperationsServicesOthers2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesOthers2.lblTitle).Text = (GlobalVariables.IsArabic ? "خدمات أخرى" : "Other Services");
				frmOperationsServicesOthers2.ShowDialog();
			}
		}
		else
		{
			if (e.Cell.Row.Cells["OperationID"].Value.Equals(DBNull.Value))
			{
				return;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Reports")
			{
				frmPROTasksReports frmPROTasksReports2 = new frmPROTasksReports(e.Cell.Row.Cells["OperationServiceStepTaskID"].Value.ToString(), e.Cell.Row.Cells["OperationServiceID"].Value.ToString(), e.Cell.Row.Cells["CompanyID"].Value.ToString());
				frmPROTasksReports2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmPROTasksReports2.lblTitle).Text = (GlobalVariables.IsArabic ? "خطابات المهمه" : "Task Letters");
				frmPROTasksReports2.ShowDialog();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Documents")
			{
				frmPROTasksDocuments frmPROTasksDocuments2 = new frmPROTasksDocuments(e.Cell.Row.Cells["OperationServiceStepTaskID"].Value.ToString());
				frmPROTasksDocuments2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmPROTasksDocuments2.lblTitle).Text = (GlobalVariables.IsArabic ? "مستندات المهمه" : "Task Documents");
				frmPROTasksDocuments2.ShowDialog();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Expenses")
			{
				frmPROTaskExpenses frmPROTaskExpenses2 = new frmPROTaskExpenses(e.Cell.Row.Cells["OperationServiceStepTaskID"].Value.ToString(), e.Cell.Row.Cells["OperationServiceStepID"].Value.ToString(), e.Cell.Row.Cells["OperationServiceID"].Value.ToString(), e.Cell.Row.Cells["OperationID"].Value.ToString(), Convert.ToDecimal(e.Cell.Row.Cells["Qty"].Value), e.Cell.Row.Cells["TaskID"].Value.ToString());
				frmPROTaskExpenses2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmPROTaskExpenses2.lblTitle).Text = (GlobalVariables.IsArabic ? "مصروفات المهمه" : "Task Expenses");
				frmPROTaskExpenses2.ShowDialog();
			}
			else
			{
				if (!(((KeyedSubObjectBase)e.Cell.Column).Key == "Details") || ((UltraGridBase)ULGNewTasks).ActiveRow == null || ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["ServiceID"].Value == DBNull.Value)
				{
					return;
				}
				DataRow dataRow2 = dtServices.Select(" ServiceID = " + ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["ServiceID"].Value.ToString())[0];
				if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "2")
				{
					frmOperationsServicesVisas frmOperationsServicesVisas3 = new frmOperationsServicesVisas(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
					frmOperationsServicesVisas3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesVisas3.lblTitle).Text = (GlobalVariables.IsArabic ? "تأشيرات" : "Visa");
					frmOperationsServicesVisas3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "3")
				{
					frmOperationsServicesCrew frmOperationsServicesCrew4 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ISSIGNON: true, READONLY: true);
					frmOperationsServicesCrew4.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesCrew4.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
					frmOperationsServicesCrew4.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "5")
				{
					frmOperationsServicesCustoms frmOperationsServicesCustoms3 = new frmOperationsServicesCustoms(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesCustoms3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesCustoms3.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص جمركى" : "Custom Clearence");
					frmOperationsServicesCustoms3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "6")
				{
					frmOperationsServicesTickets frmOperationsServicesTickets3 = new frmOperationsServicesTickets(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesTickets3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesTickets3.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز الطيران" : "Ticketings");
					frmOperationsServicesTickets3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "7")
				{
					frmOperationsServicesTransfers frmOperationsServicesTransfers3 = new frmOperationsServicesTransfers(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
					frmOperationsServicesTransfers3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesTransfers3.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل من باخرة الى باخرة" : "Vessel Transfer");
					frmOperationsServicesTransfers3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "8")
				{
					frmOperationsServicesEquipmentsAndVechiles frmOperationsServicesEquipmentsAndVechiles3 = new frmOperationsServicesEquipmentsAndVechiles(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
					frmOperationsServicesEquipmentsAndVechiles3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesEquipmentsAndVechiles3.lblTitle).Text = (GlobalVariables.IsArabic ? "المعدات و المركبات" : "Equipments And Vechiles");
					frmOperationsServicesEquipmentsAndVechiles3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "9")
				{
					frmOperationsServicesTransportations frmOperationsServicesTransportations3 = new frmOperationsServicesTransportations(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesTransportations3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesTransportations3.lblTitle).Text = (GlobalVariables.IsArabic ? "توصيل" : "Transportations");
					frmOperationsServicesTransportations3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "10")
				{
					frmOperationsServicesHotelsBooking frmOperationsServicesHotelsBooking3 = new frmOperationsServicesHotelsBooking(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesHotelsBooking3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesHotelsBooking3.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز فندق" : "Hotel Booking");
					frmOperationsServicesHotelsBooking3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "11")
				{
					frmOperationsServicesCrew frmOperationsServicesCrew5 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ISSIGNON: false, READONLY: true);
					frmOperationsServicesCrew5.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesCrew5.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
					frmOperationsServicesCrew5.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "12")
				{
					frmOperationsServicesCargosExport frmOperationsServicesCargosExport3 = new frmOperationsServicesCargosExport(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesCargosExport3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesCargosExport3.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الصادرة" : "Export Cargos");
					frmOperationsServicesCargosExport3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "13")
				{
					frmOperationsServicesCargosImport frmOperationsServicesCargosImport3 = new frmOperationsServicesCargosImport(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesCargosImport3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesCargosImport3.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الواردة" : "Import Cargos");
					frmOperationsServicesCargosImport3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "14")
				{
					frmOperationsServicesSupply frmOperationsServicesSupply3 = new frmOperationsServicesSupply(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: true);
					frmOperationsServicesSupply3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesSupply3.lblTitle).Text = (GlobalVariables.IsArabic ? "المهمات" : "Supply");
					frmOperationsServicesSupply3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "15")
				{
					frmOperationsServicesChangeVessel frmOperationsServicesChangeVessel3 = new frmOperationsServicesChangeVessel(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesChangeVessel3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesChangeVessel3.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الباخرة" : "Change Vessel");
					frmOperationsServicesChangeVessel3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "16")
				{
					frmOperationsServicesAgentCorrection frmOperationsServicesAgentCorrection3 = new frmOperationsServicesAgentCorrection(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesAgentCorrection3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesAgentCorrection3.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الوكيل والباخرة" : "Correction Of Agent & Vessel");
					frmOperationsServicesAgentCorrection3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "17")
				{
					frmOperationsServicesShipToShip frmOperationsServicesShipToShip3 = new frmOperationsServicesShipToShip(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesShipToShip3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesShipToShip3.lblTitle).Text = (GlobalVariables.IsArabic ? "من باخرة لباخرة" : "Ship To Ship");
					frmOperationsServicesShipToShip3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "18")
				{
					frmOperationsServicesSkipReloading frmOperationsServicesSkipReloading3 = new frmOperationsServicesSkipReloading(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesSkipReloading3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesSkipReloading3.lblTitle).Text = (GlobalVariables.IsArabic ? "إعادة تحميل" : "Skip Reloading");
					frmOperationsServicesSkipReloading3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "19")
				{
					frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer4 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), FROMAGENT: true, READONLY: true);
					frmOperationsServicesAgencyTransfer4.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesAgencyTransfer4.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكيل" : "Agency Transfer From another Agent");
					frmOperationsServicesAgencyTransfer4.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "20")
				{
					frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer5 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), FROMAGENT: false, READONLY: true);
					frmOperationsServicesAgencyTransfer5.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesAgencyTransfer5.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكالتنا" : "Agency Transfer To another Agent");
					frmOperationsServicesAgencyTransfer5.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "21")
				{
					frmOperationsServicesShortPass frmOperationsServicesShortPass3 = new frmOperationsServicesShortPass(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesShortPass3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesShortPass3.lblTitle).Text = (GlobalVariables.IsArabic ? "تصريح مؤقت" : "Short Pass");
					frmOperationsServicesShortPass3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "22")
				{
					frmOperationsServicesVisasCancellation frmOperationsServicesVisasCancellation3 = new frmOperationsServicesVisasCancellation(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
					frmOperationsServicesVisasCancellation3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesVisasCancellation3.lblTitle).Text = (GlobalVariables.IsArabic ? "التإشيرات الملغاة" : "Visa Cancellation");
					frmOperationsServicesVisasCancellation3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "23")
				{
					frmOperationsServicesVisasSubmissions frmOperationsServicesVisasSubmissions3 = new frmOperationsServicesVisasSubmissions(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesVisasSubmissions3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesVisasSubmissions3.lblTitle).Text = (GlobalVariables.IsArabic ? "تقديم التأشيرات" : "Visa Submissions");
					frmOperationsServicesVisasSubmissions3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "24")
				{
					frmOperationsServicesVisasClearingOverStay frmOperationsServicesVisasClearingOverStay3 = new frmOperationsServicesVisasClearingOverStay(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesVisasClearingOverStay3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesVisasClearingOverStay3.lblTitle).Text = (GlobalVariables.IsArabic ? " تمديد اقامة" : "Visa Clearing Over Stay");
					frmOperationsServicesVisasClearingOverStay3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "25")
				{
					frmOperationsServicesSeaManBooking frmOperationsServicesSeaManBooking3 = new frmOperationsServicesSeaManBooking(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesSeaManBooking3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesSeaManBooking3.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز للبحار" : "SeaMan Booking");
					frmOperationsServicesSeaManBooking3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "26")
				{
					frmOperationsServicesMedicalAssistance frmOperationsServicesMedicalAssistance3 = new frmOperationsServicesMedicalAssistance(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesMedicalAssistance3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesMedicalAssistance3.lblTitle).Text = (GlobalVariables.IsArabic ? "رعاية صحية" : "Medical Assistance");
					frmOperationsServicesMedicalAssistance3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && (dataRow2["ServiceTypeID"].ToString() == "27" || dataRow2["ServiceTypeID"].ToString() == "30" || dataRow2["ServiceTypeID"].ToString() == "31"))
				{
					frmOperationsServicesPassengersClearance frmOperationsServicesPassengersClearance3 = new frmOperationsServicesPassengersClearance(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesPassengersClearance3.WindowState = FormWindowState.Maximized;
					if (dataRow2["ServiceTypeID"].ToString() == "27")
					{
						((Control)(object)frmOperationsServicesPassengersClearance3.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص الركاب " : "Passengers Clearance");
					}
					else if (dataRow2["ServiceTypeID"].ToString() == "30")
					{
						((Control)(object)frmOperationsServicesPassengersClearance3.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص دخول فنيين " : "Technicians Inward Clearance");
					}
					else if (dataRow2["ServiceTypeID"].ToString() == "31")
					{
						((Control)(object)frmOperationsServicesPassengersClearance3.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص خروج فنيين " : "Technicians Outward Clearance");
					}
					frmOperationsServicesPassengersClearance3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "28")
				{
					frmOperationsServicesVisasRejection frmOperationsServicesVisasRejection3 = new frmOperationsServicesVisasRejection(dtServices, ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["VesselID"].Value.ToString(), READONLY: true);
					frmOperationsServicesVisasRejection3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesVisasRejection3.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات المرفوضة" : "Visa Rejection");
					frmOperationsServicesVisasRejection3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "29")
				{
					frmOperationsServicesAgencyFromDateToDate frmOperationsServicesAgencyFromDateToDate3 = new frmOperationsServicesAgencyFromDateToDate(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesAgencyFromDateToDate3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesAgencyFromDateToDate3.lblTitle).Text = (GlobalVariables.IsArabic ? "وكالة من تاريخ لتاريخ" : "Agency From Date To Date");
					frmOperationsServicesAgencyFromDateToDate3.ShowDialog();
				}
				else if (dataRow2["ServiceTypeID"] != DBNull.Value && dataRow2["ServiceTypeID"].ToString() == "100")
				{
					frmOperationsServicesOthers frmOperationsServicesOthers3 = new frmOperationsServicesOthers(((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationServiceID"].Value.ToString(), ((UltraGridBase)ULGNewTasks).ActiveRow.Cells["OperationID"].Value.ToString(), dataRow2["ServiceID"].ToString(), READONLY: true);
					frmOperationsServicesOthers3.WindowState = FormWindowState.Maximized;
					((Control)(object)frmOperationsServicesOthers3.lblTitle).Text = (GlobalVariables.IsArabic ? "خدمات أخرى" : "Other Services");
					frmOperationsServicesOthers3.ShowDialog();
				}
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGNewTasks).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGNewTasks).Rows[i].Cells["IsCompleted"].Value.ToString()))
			{
				((UltraGridBase)ULGNewTasks).Rows[i].Cells["EndDate"].Value = DateTime.Now;
				text = text + ((UltraGridBase)ULGNewTasks).Rows[i].Cells["OperationServiceStepTaskID"].Value.ToString() + ",";
			}
		}
		if (text != "," && int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From MS_OperationsServicesStepsTasksExpenses Where Deleted=0 And IsCompleted = 0 And (charindex(','+rtrim(ltrim(str(OperationServiceStepTaskID)))+',','" + text + "' ) > 0 ) ").Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن انهاء هذه المهمة لوجود مصروفات غير منتهيه ", "Cannot Finish This Tasks Because It Has UnCompleted Expenses.");
			return;
		}
		((UltraGridBase)ULGNewTasks).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGNewTasks).DataSource);
		dataView.RowFilter = "OperationServiceStepTaskID<>0";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			OperationsServicesStepsTasks.Insert_UpdateByTable(dataView.ToTable(), GlobalVariables.UserID);
		}
		dataView = new DataView((DataTable)((UltraGridBase)ULGNewTasks).DataSource);
		dataView.RowFilter = "TaskWithoutOperationID<>0";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TaskWithoutOperation.Insert_UpdateByTable(dataView.ToTable(), GlobalVariables.UserID);
		}
		FillGrid();
		InitGrid();
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
		InitGrid();
	}

	private void ULGNewTasks_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblNewTasksCounterResult).Text = ((UltraGridBase)ULGNewTasks).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Expected O, but got Unknown
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewNewTasks));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGNewTasks = new UltraGrid();
		this.lblNewTasksCounterResult = new UltraLabel();
		this.lblNewTasksCounter = new UltraLabel();
		this.btnSave = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGNewTasks).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance17");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance18");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val3, "appearance19");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance20");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGNewTasks, "ULGNewTasks");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGNewTasks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGNewTasks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGNewTasks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGNewTasks).Name = "ULGNewTasks";
		this.ULGNewTasks.AfterEnterEditMode += new System.EventHandler(ULGNewTasks_AfterEnterEditMode);
		this.ULGNewTasks.ClickCellButton += new CellEventHandler(ULGNewTasks_ClickCellButton);
		((UltraGridBase)this.ULGNewTasks).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGNewTasks_AfterRowFilterChanged);
		resources.ApplyResources(this.lblNewTasksCounterResult, "lblNewTasksCounterResult");
		resources.ApplyResources(val15, "appearance21");
		((ControlBase)this.lblNewTasksCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblNewTasksCounterResult).Name = "lblNewTasksCounterResult";
		resources.ApplyResources(this.lblNewTasksCounter, "lblNewTasksCounter");
		this.lblNewTasksCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewTasksCounter).Name = "lblNewTasksCounter";
		((ControlBase)this.lblNewTasksCounter).WrapText = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.Save;
		resources.ApplyResources(val16, "appearance22");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewTasksCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewTasksCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGNewTasks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewNewTasks";
		base.Load += new System.EventHandler(frmViewShortPass_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGNewTasks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewTasksCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewTasksCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGNewTasks).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
