using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.HR.Attendance.MasterData;

public class frmShiftsPlans : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtShifts;

	private DataTable dtEmployees;

	private DataTable dtShiftsPlansEmployees;

	private ValueList vlShifts = new ValueList();

	private ValueList vlEmployees = new ValueList();

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataEmployees;

	private UltraButton btnEmployees;

	public UltraDateTimeEditor dtpDefaultPlanStartDate;

	private UltraLabel ultraLabel1;

	private UltraButton btnApplyToAll;

	private UltraCheckEditor chkHasBreak;

	public frmShiftsPlans()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "HR_ShiftsPlans";
		IDCol = "ShiftPlanID";
		NoCol = "ShiftPlanNo";
		DateCol = "GetDate()";
	}

	public frmShiftsPlans(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDefaultPlanStartDate.Value = null;
		dtShifts = Shifts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlShifts.ValueListItems.Clear();
		for (int i = 0; i < dtShifts.Rows.Count; i++)
		{
			vlShifts.ValueListItems.Add(dtShifts.Rows[i]["ShiftID"], dtShifts.Rows[i]["ShiftName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlEmployees.ValueListItems.Clear();
		for (int j = 0; j < dtEmployees.Rows.Count; j++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[j]["SubAccountID"], dtEmployees.Rows[j]["SubAccountName"].ToString() + " - " + dtEmployees.Rows[j]["EmployeeNo"].ToString());
		}
		dtDetails = ShiftsPlansDetails.SelectByShiftPlanID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtShiftsPlansEmployees = ShiftsPlansEmployees.SelectByShiftPlanID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataEmployees).DataSource = dtShiftsPlansEmployees;
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataEmployees);
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftPlanDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftOrder"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationPeriodAfter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftOrder"].Header).Caption = (GlobalVariables.IsArabic ? "ترتيب الوردية" : "Shift Order");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Header).Caption = (GlobalVariables.IsArabic ? "الوردية" : "Shift");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period"].Header).Caption = (GlobalVariables.IsArabic ? "المدة" : "Period");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationPeriodAfter"].Header).Caption = (GlobalVariables.IsArabic ? "مدة الاجازة" : "Vacation Period");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftOrder"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationPeriodAfter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].ValueList = (IValueList)(object)vlShifts;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["ShiftPlanEmployeeID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["PlanStartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((HeaderBase)((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["PlanStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ بداية الخطة" : "Plan Start Date");
		((HeaderBase)((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["PlanStartDate"].Hidden = false;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraDateTimeEditor)((UltraGridBase)ULGDataEmployees).DisplayLayout.Bands[0].Columns["PlanStartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ShiftsPlans.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["ShiftPlanNo"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["ShiftPlanNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["ShiftPlanNameEn"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkHasBreak).Checked = bool.Parse(drMaster["HasBreak"].ToString());
			dtDetails = ShiftsPlansDetails.SelectByShiftPlanID(drMaster["ShiftPlanID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtShiftsPlansEmployees = ShiftsPlansEmployees.SelectByShiftPlanID(drMaster["ShiftPlanID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataEmployees).DataSource = dtShiftsPlansEmployees;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnEmployees).Enabled = !NavMode;
		((Control)(object)btnApplyToAll).Enabled = !NavMode;
		((Control)(object)chkHasBreak).Enabled = !NavMode;
		((Control)(object)dtpDefaultPlanStartDate).Enabled = !NavMode;
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataEmployees).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? ShiftsPlans.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((UltraToggleEditorBase)chkHasBreak).Checked = true;
		((TextEditorControlBase)txtNotes).Clear();
		((DataTable)((UltraGridBase)ULGDataEmployees).DataSource).Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم خطة العمل بالعربية", "Please Enter Shift Plan Arabic Name");
			return false;
		}
		if (Main.CheckForValue("HR_ShiftsPlans", "ShiftPlanNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ShiftPlanNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = ShiftsPlans.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ShiftOrder"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ShiftOrder"].Value.ToString()) < 1)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الترتيب  ", "Please Enter Order");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ShiftOrder"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Period"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Period"].Value.ToString()) < 1m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال المدة  ", "Please Enter Period ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Period"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ShiftID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار الوردية  ", "Please Select Shift ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ShiftID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ShiftID"].DroppedDown = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ShiftOrder"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ShiftOrder"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرارالترتيب", "Cannot Duplicate The Order");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ShiftOrder"];
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataEmployees).Rows[k].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الموظف  ", "Please Select Employee Name ");
				ULGDataEmployees.ActiveCell = ((UltraGridBase)ULGDataEmployees).Rows[k].Cells["SubAccountID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataEmployees.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataEmployees).Rows[k].Cells["PlanStartDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال تاريخ بداية الوردية  ", "Please Enter Plan Start Date ");
				ULGDataEmployees.ActiveCell = ((UltraGridBase)ULGDataEmployees).Rows[k].Cells["PlanStartDate"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataEmployees.PerformAction((UltraGridAction)24);
				return false;
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count; l++)
			{
				if (k != l && ((UltraGridBase)ULGDataEmployees).Rows[k].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGDataEmployees).Rows[l].Cells["SubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الموظف", "Cannot Duplicate The Order");
					ULGDataEmployees.ActiveCell = ((UltraGridBase)ULGDataEmployees).Rows[k].Cells["SubAccountID"];
					((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = ShiftsPlans.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkHasBreak).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ShiftPlanDetailID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["ShiftPlanID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ShiftsPlansDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count > 0)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count; j++)
				{
					text = text + ((UltraGridBase)ULGDataEmployees).Rows[j].Cells["SubAccountID"].Value.ToString() + ",";
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["ShiftPlanEmployeeID"].Value = -1;
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["ShiftPlanID"].Value = num;
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ShiftsPlansEmployees.DeleteBySubAccountIDs(num.ToString(), text, IsFromServer: true);
				ShiftsPlansEmployees.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataEmployees).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = ShiftsPlans.Insert_Update(drMaster["ShiftPlanID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkHasBreak).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ShiftPlanDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["ShiftPlanID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("HR_ShiftsPlansDetails", "ShiftPlanID", drMaster["ShiftPlanID"].ToString(), "ShiftPlanDetailID", text, IsFromServer: true);
			ShiftsPlansDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			text = ",";
			string text2 = ",";
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count > 0)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["ShiftPlanEmployeeID"].Value = "-1";
					text2 = text2 + ((UltraGridBase)ULGDataEmployees).Rows[j].Cells["SubAccountID"].Value.ToString() + ",";
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["ShiftPlanID"].Value = num;
					((UltraGridBase)ULGDataEmployees).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ShiftsPlansEmployees.DeleteBySubAccountIDs(num.ToString(), text2, IsFromServer: true);
				ShiftsPlansEmployees.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataEmployees).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			ShiftsPlansEmployees.DeleteByShiftPlanID(drMaster["ShiftPlanID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ShiftsPlansDetails.DeleteByShiftPlanID(drMaster["ShiftPlanID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ShiftsPlans.Delete(drMaster["ShiftPlanID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			string val = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_ShiftsPlans_A.rpt" : "Rep_HR_ShiftsPlans_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ShiftPlanIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@ShiftPlanIDs", "," + RowID + ",", "Rep_HR_ShiftsPlansEmployees");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_HR_ShiftsPlansEmployees");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ShiftsPlansReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ShiftPlanID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtShifts = Shifts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlShifts.ValueListItems.Clear();
		for (int i = 0; i < dtShifts.Rows.Count; i++)
		{
			vlShifts.ValueListItems.Add(dtShifts.Rows[i]["ShiftID"], dtShifts.Rows[i]["ShiftName"].ToString());
		}
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlEmployees.ValueListItems.Clear();
		for (int j = 0; j < dtEmployees.Rows.Count; j++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[j]["SubAccountID"], dtEmployees.Rows[j]["SubAccountName"].ToString() + " - " + dtEmployees.Rows[j]["EmployeeNo"].ToString());
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		e.Row.Cells["ShiftOrder"].Value = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ShiftOrder" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Period" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "VacationPeriodAfter"))
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataEmployees_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDataEmployees).ActiveRow).Selected = true;
		}
	}

	private void btnEmployees_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesReport("1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			if (((DataTable)((UltraGridBase)ULGDataEmployees).DataSource).Select("SubAccountID =" + dtSearchResult.Rows[i]["SubAccountID"].ToString()).Length == 0)
			{
				DataRow dataRow = dtShiftsPlansEmployees.NewRow();
				dataRow["ShiftPlanEmployeeID"] = -1;
				dataRow["SubAccountID"] = dtSearchResult.Rows[i]["SubAccountID"];
				if (dtpDefaultPlanStartDate.Value != null)
				{
					dataRow["PlanStartDate"] = dtpDefaultPlanStartDate.Value;
				}
				dtShiftsPlansEmployees.Rows.Add(dataRow);
			}
		}
		((UltraGridBase)ULGDataEmployees).UpdateData();
		dtSearchResult = null;
	}

	private void btnApplyToAll_Click(object sender, EventArgs e)
	{
		if (dtpDefaultPlanStartDate.Value != null)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataEmployees).Rows).Count; i++)
			{
				((UltraGridBase)ULGDataEmployees).Rows[i].Cells["PlanStartDate"].Value = dtpDefaultPlanStartDate.Value;
			}
		}
	}

	private void ULGDataEmployees_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (dtpDefaultPlanStartDate.Value != null)
		{
			e.Row.Cells["PlanStartDate"].Value = dtpDefaultPlanStartDate.Value;
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
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Expected O, but got Unknown
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.MasterData.frmShiftsPlans));
		UltraTab val = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataEmployees = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.btnEmployees = new UltraButton();
		this.dtpDefaultPlanStartDate = new UltraDateTimeEditor();
		this.ultraLabel1 = new UltraLabel();
		this.btnApplyToAll = new UltraButton();
		this.chkHasBreak = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDefaultPlanStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasBreak).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Employees";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name1");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataEmployees);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataEmployees).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDataEmployees, "ULGDataEmployees");
		((System.Windows.Forms.Control)(object)this.ULGDataEmployees).Name = "ULGDataEmployees";
		((UltraControlBase)this.ULGDataEmployees).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataEmployees.AfterEnterEditMode += new System.EventHandler(ULGDataEmployees_AfterEnterEditMode);
		this.ULGDataEmployees.AfterRowInsert += new RowEventHandler(ULGDataEmployees_AfterRowInsert);
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		this.lblEnglishName.AutoEllipsis = false;
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		this.lblArabicName.AutoEllipsis = false;
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.btnEmployees, "btnEmployees");
		((System.Windows.Forms.Control)(object)this.btnEmployees).Name = "btnEmployees";
		((System.Windows.Forms.Control)(object)this.btnEmployees).Click += new System.EventHandler(btnEmployees_Click);
		resources.ApplyResources(this.dtpDefaultPlanStartDate, "dtpDefaultPlanStartDate");
		((System.Windows.Forms.Control)(object)this.dtpDefaultPlanStartDate).Name = "dtpDefaultPlanStartDate";
		this.dtpDefaultPlanStartDate.PromptChar = ' ';
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnApplyToAll, "btnApplyToAll");
		((System.Windows.Forms.Control)(object)this.btnApplyToAll).Name = "btnApplyToAll";
		((System.Windows.Forms.Control)(object)this.btnApplyToAll).Click += new System.EventHandler(btnApplyToAll_Click);
		resources.ApplyResources(this.chkHasBreak, "chkHasBreak");
		((System.Windows.Forms.Control)(object)this.chkHasBreak).Name = "chkHasBreak";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasBreak);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDefaultPlanStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnApplyToAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmShiftsPlans";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnApplyToAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDefaultPlanStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasBreak, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDefaultPlanStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasBreak).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
