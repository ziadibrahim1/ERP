using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Attendance.Transactions;

public class frmEmployeesAttendanceDetails : frmBase
{
	private DataTable dtVacationtypes;

	private DataTable dtShifts;

	private DataTable dtDetails;

	private ValueList vlEmployees = new ValueList();

	private ValueList vlVacationtypes = new ValueList();

	private ValueList vlShifts = new ValueList();

	public string EmpIDs = "";

	public DateTime FromDate;

	public DateTime ToDate;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	public UltraButton btnCancel;

	public UltraLabel lblTitle;

	public UltraButton btnReset;

	private UltraLabel lblInTimeColor;

	private UltraLabel lbl15minLate;

	private UltraLabel ultraLabel3;

	private UltraLabel lbl30MinLate;

	private UltraLabel lblHourLate;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	public frmEmployeesAttendanceDetails()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmEmployeesAttendanceDetails(DataTable dtDetails, DataTable dtEmployees)
		: this()
	{
		vlEmployees.ValueListItems.Clear();
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString() + " - " + dtEmployees.Rows[i]["EmployeeNo"].ToString());
		}
		dtVacationtypes = VacationTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlVacationtypes.ValueListItems.Clear();
		for (int j = 0; j < dtVacationtypes.Rows.Count; j++)
		{
			vlVacationtypes.ValueListItems.Add(dtVacationtypes.Rows[j]["VacationtypeID"], dtVacationtypes.Rows[j]["VacationName"].ToString());
		}
		dtShifts = Shifts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlShifts.ValueListItems.Clear();
		for (int k = 0; k < dtShifts.Rows.Count; k++)
		{
			vlShifts.ValueListItems.Add(dtShifts.Rows[k]["ShiftID"], dtShifts.Rows[k]["ShiftName"].ToString());
		}
		this.dtDetails = dtDetails;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeAttendanceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMission"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayName"].Header).Caption = (GlobalVariables.IsArabic ? "يوم" : "Day");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttendanceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttendanceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التايخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttendanceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedFrom"].Header).Caption = (GlobalVariables.IsArabic ? "حضور متوقع" : "Expected From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedFrom"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedFrom"].MaskInput = "hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTo"].Header).Caption = (GlobalVariables.IsArabic ? "إنصراف متوقع" : "Expected To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTo"].MaskInput = "hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.11);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFrom"].Header).Caption = (GlobalVariables.IsArabic ? "حضور فعلي" : "Actual From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFrom"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFrom"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.11);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTo"].Header).Caption = (GlobalVariables.IsArabic ? "إنصراف فعلي" : "Actual To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTo"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakOut"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakOut"].Header).Caption = (GlobalVariables.IsArabic ? "بدأ راحه" : "Break Out");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakOut"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakOut"].MaskInput = "hh:mm tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakOut"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakIn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakIn"].Header).Caption = (GlobalVariables.IsArabic ? "نهاية راحه" : "Break In");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakIn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakIn"].MaskInput = "hh:mm tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BreakIn"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkDay"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkDay"].Header).Caption = (GlobalVariables.IsArabic ? "يوم عمل" : "Work Day");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkDay"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMission"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMission"].Header).Caption = (GlobalVariables.IsArabic ? "مأموريه" : "Mission");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMission"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationtypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationtypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الاجازه" : "Vacation type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationtypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationtypeID"].ValueList = (IValueList)(object)vlVacationtypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAutoExtraTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAutoExtraTime"].Header).Caption = (GlobalVariables.IsArabic ? "إضافي" : "Extra Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAutoExtraTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Header).Caption = (GlobalVariables.IsArabic ? "الورديه" : "Shift");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].ValueList = (IValueList)(object)vlShifts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["WorkDay"].Value.Equals(true))
			{
				DateTime dateTime = DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ExpectedFrom"].Value.ToString());
				if (((object)((UltraGridBase)ULGData).Rows[i].Cells["ActualFrom"]).Equals((object)DBNull.Value))
				{
					continue;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["ActualFrom"].Value != DBNull.Value)
				{
					DateTime dateTime2 = DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ActualFrom"].Value.ToString());
					if (dateTime2 < dateTime.AddMinutes(15.0))
					{
						((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.FromArgb(106, 201, 112);
					}
					else if (dateTime2 < dateTime.AddMinutes(30.0))
					{
						((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.FromArgb(236, 236, 160);
					}
					else if (dateTime2 < dateTime.AddHours(1.0))
					{
						((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.FromArgb(233, 161, 86);
					}
					else
					{
						((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.FromArgb(226, 69, 69);
					}
				}
				else
				{
					((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.FromArgb(119, 81, 42);
				}
			}
			else
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = Color.Transparent;
			}
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!CanUpdate || (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "WorkDay" && ((UltraGridBase)ULGData).ActiveRow.Cells["WorkDay"].Value != null && ((UltraGridBase)ULGData).ActiveRow.Cells["WorkDay"].Value.Equals(false)) || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsMission" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "VacationtypeID" || (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AttendanceDate" && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeeAttendanceID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeeAttendanceID"].Value.ToString() != "-1") || (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SubAccountID" && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeeAttendanceID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeeAttendanceID"].Value.ToString() != "-1"))
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualFrom" && ULGData.ActiveCell.Value.Equals(DBNull.Value))
		{
			ULGData.ActiveCell.Value = (((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value.Equals(DBNull.Value) ? ((UltraGridBase)ULGData).ActiveRow.Cells["AttendanceDate"].Value : ((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value);
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualTo" && ULGData.ActiveCell.Value.Equals(DBNull.Value))
		{
			ULGData.ActiveCell.Value = (((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value.Equals(DBNull.Value) ? ((UltraGridBase)ULGData).ActiveRow.Cells["AttendanceDate"].Value : ((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		DateTime result = default(DateTime);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ActualFrom" && DateTime.TryParse(e.Cell.Value.ToString(), out result))
		{
			ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
			((UltraGridBase)ULGData).UpdateData();
			if (((UltraGridBase)ULGData).ActiveRow.Cells["IsWorkByHour"].Value.Equals(true))
			{
				TimeSpan timeSpan = Convert.ToDateTime(e.Cell.Value) - Convert.ToDateTime(((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value);
				((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value = e.Cell.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value = Convert.ToDateTime(((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value) + timeSpan;
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "WorkDay")
		{
			((UltraGridBase)ULGData).UpdateData();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ActualFrom"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["ActualTo"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ActualFrom"].Value.ToString()) > DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ActualTo"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("وقت الحضور الفعلي بعد وقت الانصراف الفعلي", "Actual From Date Is After The Actual To");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				return false;
			}
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).UpdateData();
		if (ValidateData())
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				EmployeesAttendance.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
			Close();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ShiftID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && ((UltraGridBase)ULGData).ActiveRow.Cells["AttendanceDate"].Value != null && ((UltraGridBase)ULGData).ActiveRow.Cells["AttendanceDate"].Value != DBNull.Value)
		{
			TimeSpan timeSpan = TimeSpan.Parse(dtShifts.Select(" ShiftID= " + e.Cell.Value)[0]["StartTime"].ToString());
			TimeSpan timeSpan2 = TimeSpan.Parse(dtShifts.Select(" ShiftID= " + e.Cell.Value)[0]["ShiftPeriod"].ToString());
			DateTime dateTime = Convert.ToDateTime(((UltraGridBase)ULGData).ActiveRow.Cells["AttendanceDate"].Value).AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes);
			((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value = dateTime;
			((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value = dateTime.AddHours(timeSpan2.Hours).AddMinutes(timeSpan2.Minutes);
			if (GlobalFunctions.GetOption("IgnoreAttendanceMachineInOutValue"))
			{
				DataTable machinesAttByShiftID = EmployeesAttendance.GetMachinesAttByShiftID(e.Cell.Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeeAttendanceID"].Value.ToString(), IsFromServer: true);
				((UltraGridBase)ULGData).ActiveRow.Cells["ActualFrom"].Value = machinesAttByShiftID.Rows[0]["ActualFrom"];
				((UltraGridBase)ULGData).ActiveRow.Cells["ActualTo"].Value = machinesAttByShiftID.Rows[0]["ActualTo"];
				((UltraGridBase)ULGData).ActiveRow.Cells["BreakOut"].Value = machinesAttByShiftID.Rows[0]["BreakOut"];
				((UltraGridBase)ULGData).ActiveRow.Cells["BreakIn"].Value = machinesAttByShiftID.Rows[0]["BreakIn"];
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AttendanceDate" && ULGData.ActiveCell != null && ULGData.ActiveCell.Value == DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ShiftID"].Value != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ShiftID"].Value != DBNull.Value)
		{
			TimeSpan timeSpan = TimeSpan.Parse(dtShifts.Select(" ShiftID= " + e.Cell.Row.Cells["ShiftID"].Value)[0]["StartTime"].ToString());
			TimeSpan timeSpan2 = TimeSpan.Parse(dtShifts.Select(" ShiftID= " + e.Cell.Row.Cells["ShiftID"].Value)[0]["ShiftPeriod"].ToString());
			DateTime dateTime = Convert.ToDateTime(e.Cell.Value).AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes);
			((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedFrom"].Value = dateTime;
			((UltraGridBase)ULGData).ActiveRow.Cells["ExpectedTo"].Value = dateTime.AddHours(timeSpan2.Hours).AddMinutes(timeSpan2.Minutes);
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualFrom" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "WorkDay")
		{
			if (!Convert.ToBoolean(((UltraGridBase)ULGData).ActiveRow.Cells["WorkDay"].Value))
			{
				((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.Transparent;
			}
			else if (e.Cell.Row.Cells["ActualFrom"].Value.Equals(DBNull.Value))
			{
				((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.FromArgb(119, 81, 42);
			}
			else if (e.Cell.Row.Cells["ExpectedFrom"].Value != DBNull.Value)
			{
				DateTime dateTime2 = DateTime.Parse(e.Cell.Row.Cells["ExpectedFrom"].Value.ToString());
				DateTime dateTime3 = DateTime.Parse(e.Cell.Row.Cells["ActualFrom"].Value.ToString());
				if (dateTime3 < dateTime2.AddMinutes(15.0))
				{
					((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.FromArgb(106, 201, 112);
				}
				else if (dateTime3 < dateTime2.AddMinutes(30.0))
				{
					((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.FromArgb(236, 236, 160);
				}
				else if (dateTime3 < dateTime2.AddHours(1.0))
				{
					((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.FromArgb(233, 161, 86);
				}
				else
				{
					((AppearanceBase)e.Cell.Row.Appearance).BackColor = Color.FromArgb(226, 69, 69);
				}
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		dtDetails = EmployeesAttendance.ResetPlanByEmployeeIDsPeriods(EmpIDs, FromDate.ToString(GlobalVariables.DateShortFormate), ToDate.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.Transactions.frmEmployeesAttendanceDetails));
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
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnCancel = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnReset = new UltraButton();
		this.lblInTimeColor = new UltraLabel();
		this.lbl15minLate = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.lbl30MinLate = new UltraLabel();
		this.lblHourLate = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnReset, "btnReset");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnReset).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnReset).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnReset).Name = "btnReset";
		((System.Windows.Forms.Control)(object)this.btnReset).Click += new System.EventHandler(btnReset_Click);
		resources.ApplyResources(this.lblInTimeColor, "lblInTimeColor");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(106, 201, 112);
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblInTimeColor).Appearance = (AppearanceBase)(object)val5;
		this.lblInTimeColor.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lblInTimeColor).Name = "lblInTimeColor";
		((UltraControlBase)this.lblInTimeColor).UseAppStyling = false;
		resources.ApplyResources(this.lbl15minLate, "lbl15minLate");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(236, 236, 160);
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lbl15minLate).Appearance = (AppearanceBase)(object)val6;
		this.lbl15minLate.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lbl15minLate).Name = "lbl15minLate";
		((UltraControlBase)this.lbl15minLate).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		resources.ApplyResources(this.lbl30MinLate, "lbl30MinLate");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(233, 161, 86);
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lbl30MinLate).Appearance = (AppearanceBase)(object)val7;
		this.lbl30MinLate.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lbl30MinLate).Name = "lbl30MinLate";
		((UltraControlBase)this.lbl30MinLate).UseAppStyling = false;
		resources.ApplyResources(this.lblHourLate, "lblHourLate");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(226, 69, 69);
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblHourLate).Appearance = (AppearanceBase)(object)val8;
		this.lblHourLate.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lblHourLate).Name = "lblHourLate";
		((UltraControlBase)this.lblHourLate).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Black;
		((AppearanceBase)val9).BorderColor2 = System.Drawing.Color.Black;
		((AppearanceBase)val9).BorderColor3DBase = System.Drawing.Color.Black;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val9;
		this.ultraLabel2.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((UltraControlBase)this.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.FromArgb(119, 81, 42);
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel5.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((UltraControlBase)this.ultraLabel5).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHourLate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lbl30MinLate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lbl15minLate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInTimeColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReset);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmEmployeesAttendanceDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReset, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInTimeColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lbl15minLate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lbl30MinLate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHourLate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
