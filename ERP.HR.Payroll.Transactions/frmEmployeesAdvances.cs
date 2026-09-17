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
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Payroll.Transactions;

public class frmEmployeesAdvances : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtEmployees;

	private DataTable dtAdvanceTypes;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblAdvanceValue;

	private UltraTextEditor txtAdvanceValue;

	private UltraLabel lblInstallmentValue;

	private UltraTextEditor txtInstallmentValue;

	private UltraComboEditor cboEmployees;

	private UltraLabel lblEmployees;

	private UltraLabel lblFirstPaymentDate;

	private UltraDateTimeEditor dtpFirstPaymentDate;

	private UltraTextEditor txtInstallmentCount;

	private UltraLabel lblInstallmentCount;

	public UltraButton btnEmployeesSearch;

	private UltraCheckEditor chkAllInstallmentsPaid;

	private UltraLabel lblAdvanceType;

	private UltraComboEditor cboAdvanceTypes;

	public frmEmployeesAdvances()
	{
		InitializeComponent();
		TableName = "HR_EmployeesAdvances";
		IDCol = "EmployeeAdvanceID";
		NoCol = "EmployeeAdvanceNo";
		DateCol = "EmployeeAdvanceDate";
	}

	public frmEmployeesAdvances(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboEmployees, dtEmployees, "SubAccountID", "SubAccountName");
		dtAdvanceTypes = AdvancesTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAdvanceTypes, dtAdvanceTypes, "AdvanceTypeID", "AdvanceTypeName");
		dtDetails = EmployeesAdvancesDetails.SelectByEmployeeAdvanceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeAdvanceDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ القسط" : "Installment Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة القسط" : "Installment Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Header).Caption = (GlobalVariables.IsArabic ? "مدفوع" : "Paid");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].DefaultCellValue = false;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = EmployeesAdvances.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((TextEditorControlBase)txtAdvanceValue).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtInstallmentValue).ValueChanged -= txt_ValueChanged;
			dtpFirstPaymentDate.ValueChanged -= dtpFirstPaymentDate_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["EmployeeAdvanceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["EmployeeAdvanceDate"];
			((TextEditorControlBase)cboEmployees).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboAdvanceTypes).Value = drMaster["AdvanceTypeID"];
			((Control)(object)txtAdvanceValue).Text = decimal.Parse(drMaster["AdvanceValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtInstallmentCount).Text = drMaster["InstallmentCount"].ToString();
			((Control)(object)txtInstallmentValue).Text = decimal.Parse(drMaster["InstallmentValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			dtpFirstPaymentDate.Value = (DateTime)drMaster["FirstPaymentDate"];
			((UltraToggleEditorBase)chkAllInstallmentsPaid).Checked = bool.Parse(drMaster["AllInstallmentPaid"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = EmployeesAdvancesDetails.SelectByEmployeeAdvanceID(drMaster["EmployeeAdvanceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			((TextEditorControlBase)txtAdvanceValue).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtInstallmentValue).ValueChanged += txt_ValueChanged;
			dtpFirstPaymentDate.ValueChanged += dtpFirstPaymentDate_ValueChanged;
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
		((EditorButtonControlBase)dtpDate).ReadOnly = !NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString());
		((EditorButtonControlBase)cboEmployees).ReadOnly = !NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString());
		((EditorButtonControlBase)cboAdvanceTypes).ReadOnly = NavMode || (!NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString()));
		((EditorButtonControlBase)txtAdvanceValue).ReadOnly = !NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString());
		((EditorButtonControlBase)txtInstallmentValue).ReadOnly = !NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString());
		((EditorButtonControlBase)txtInstallmentCount).ReadOnly = true;
		((Control)(object)chkAllInstallmentsPaid).Enabled = false;
		((EditorButtonControlBase)dtpFirstPaymentDate).ReadOnly = !NavMode && drMaster != null && bool.Parse(drMaster["Approved"].ToString());
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnEmployeesSearch).Enabled = !NavMode && drMaster != null && !bool.Parse(drMaster["Approved"].ToString());
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtAdvanceValue).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtInstallmentValue).ValueChanged -= txt_ValueChanged;
		dtpFirstPaymentDate.ValueChanged -= dtpFirstPaymentDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? EmployeesAdvances.GetCode(IsFromServer: true) : "");
		((Control)(object)txtAdvanceValue).Text = "0";
		((Control)(object)txtInstallmentCount).Text = "0";
		((Control)(object)txtInstallmentValue).Text = "0";
		UltraDateTimeEditor obj = dtpDate;
		DateTime dateTime = (dtpFirstPaymentDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		obj.DateTime = dateTime;
		((UltraToggleEditorBase)chkAllInstallmentsPaid).Checked = false;
		cboEmployees.SelectedIndex = -1;
		cboAdvanceTypes.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtAdvanceValue).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtInstallmentValue).ValueChanged += txt_ValueChanged;
		dtpFirstPaymentDate.ValueChanged += dtpFirstPaymentDate_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ السلفة" : "Please Enter Advance Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtInstallmentValue).Text == "" || decimal.Parse(((Control)(object)txtInstallmentValue).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة القسط" : "Please Enter Installment Amount");
			((TextEditorControlBase)txtInstallmentValue).Focus();
			return false;
		}
		if (((Control)(object)txtAdvanceValue).Text == "" || decimal.Parse(((Control)(object)txtAdvanceValue).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة السلفة" : "Please Enter Advance Value");
			((TextEditorControlBase)txtAdvanceValue).Focus();
			return false;
		}
		if (dtpFirstPaymentDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ بداية السلفة" : "Please Enter Advance Start Date");
			((Control)(object)dtpFirstPaymentDate).Focus();
			dtpFirstPaymentDate.DropDown();
			return false;
		}
		if (cboEmployees.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الموظف" : "Please Select Employee");
			((TextEditorControlBase)cboEmployees).Focus();
			cboEmployees.DropDown();
			return false;
		}
		if (Main.CheckForValue("HR_EmployeesAdvances", "EmployeeAdvanceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["EmployeeAdvanceNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = EmployeesAdvances.GetCode(IsFromServer: true);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["InstallmentDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال تاريخ القسط  ", "Please Enter Installment Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["InstallmentDate"];
				((UltraGridBase)ULGData).Rows[i].Cells["InstallmentDate"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["InstallmentValue"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["InstallmentValue"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة القسط   ", "Please Enter Installment Amount ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["InstallmentValue"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		object obj = dtDetails.Compute(" Sum(InstallmentValue) ", "");
		if (obj != DBNull.Value && decimal.Parse(obj.ToString()) != decimal.Parse(((Control)(object)txtAdvanceValue).Text))
		{
			GlobalVariables.InformationMB.Show("إجمالى الاقساط لا تساوى قيمة السلفة ", "Total Installments Not Equal Advance Value");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = EmployeesAdvances.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboEmployees).Value.ToString(), (cboAdvanceTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdvanceTypes).Value.ToString(), ((Control)(object)txtAdvanceValue).Text.ToString(), ((Control)(object)txtInstallmentCount).Text.ToString(), ((Control)(object)txtInstallmentValue).Text.ToString(), dtpFirstPaymentDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkAllInstallmentsPaid).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["EmployeeAdvanceDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["EmployeeAdvanceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			EmployeesAdvancesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			int num = EmployeesAdvances.Insert_Update(drMaster["EmployeeAdvanceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboEmployees).Value.ToString(), (cboAdvanceTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdvanceTypes).Value.ToString(), ((Control)(object)txtAdvanceValue).Text.ToString(), ((Control)(object)txtInstallmentCount).Text.ToString(), ((Control)(object)txtInstallmentValue).Text.ToString(), dtpFirstPaymentDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkAllInstallmentsPaid).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["EmployeeAdvanceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["EmployeeAdvanceDetailID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("HR_EmployeesAdvancesDetails", "EmployeeAdvanceID", drMaster["EmployeeAdvanceID"].ToString(), "EmployeeAdvanceDetailID", text, IsFromServer: true);
			EmployeesAdvancesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			EmployeesAdvancesDetails.DeleteByEmployeeAdvanceID(drMaster["EmployeeAdvanceID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			EmployeesAdvances.Delete(drMaster["EmployeeAdvanceID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_EmployeesAdvances_A.rpt" : "Rep_HR_EmployeesAdvances_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@EmployeeAdvanceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesAdvancesReport(-1, 0, IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["EmployeeAdvanceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboEmployees, dtEmployees, "SubAccountID", "SubAccountName");
		dtAdvanceTypes = AdvancesTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAdvanceTypes, dtAdvanceTypes, "AdvanceTypeID", "AdvanceTypeName");
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsPaid" && ((UltraGridBase)ULGData).ActiveRow != null && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsPaid"].Value.ToString()) && ((UltraGridBase)ULGData).ActiveRow.Cells["SalaryListEmployeeMonthlyPrintID"].Value != DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsPaid"].Value.ToString()) && ((UltraGridBase)ULGData).ActiveRow.Cells["SalaryListEmployeeMonthlyPrintID"].Value != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن حذف هذا القسط تم سداده" : "Cannot Delete This Installment Is Already Paid");
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "InstallmentValue")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)txtInstallmentCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txt_ValueChanged(object sender, EventArgs e)
	{
		CalculateInstallments();
	}

	private void btnEmployeesSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboEmployees).Value = num;
		}
	}

	private void dtpFirstPaymentDate_ValueChanged(object sender, EventArgs e)
	{
		CalculateInstallments();
	}

	public void CalculateInstallments()
	{
		if (((Control)(object)txtAdvanceValue).Text != "" && ((Control)(object)txtAdvanceValue).Text != "." && decimal.Parse(((Control)(object)txtAdvanceValue).Text) > 0m && ((Control)(object)txtInstallmentValue).Text != "" && ((Control)(object)txtInstallmentValue).Text != "." && decimal.Parse(((Control)(object)txtInstallmentValue).Text) > 0m && dtpFirstPaymentDate.Value != null)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			num2 = decimal.Parse(((Control)(object)txtAdvanceValue).Text) / decimal.Parse(((Control)(object)txtInstallmentValue).Text);
			dtDetails.Clear();
			for (int i = 0; (decimal)i < num2; i++)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["EmployeeAdvanceDetailID"] = -1;
				dataRow["InstallmentDate"] = dtpFirstPaymentDate.DateTime.AddMonths(i);
				dataRow["InstallmentValue"] = ((decimal.Parse(((Control)(object)txtAdvanceValue).Text) - num < decimal.Parse(((Control)(object)txtInstallmentValue).Text)) ? (decimal.Parse(((Control)(object)txtAdvanceValue).Text) - num) : decimal.Parse(((Control)(object)txtInstallmentValue).Text));
				dataRow["IsPaid"] = false;
				dtDetails.Rows.Add(dataRow);
				num += decimal.Parse(((Control)(object)txtInstallmentValue).Text);
			}
			((UltraGridBase)ULGData).UpdateData();
			((Control)(object)txtInstallmentCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
		}
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentValue"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentValue"].Value.ToString()) > 0m)
		{
			((Control)(object)txtInstallmentCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
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
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Payroll.Transactions.frmEmployeesAdvances));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblAdvanceValue = new UltraLabel();
		this.txtAdvanceValue = new UltraTextEditor();
		this.lblInstallmentValue = new UltraLabel();
		this.txtInstallmentValue = new UltraTextEditor();
		this.cboEmployees = new UltraComboEditor();
		this.lblEmployees = new UltraLabel();
		this.lblFirstPaymentDate = new UltraLabel();
		this.dtpFirstPaymentDate = new UltraDateTimeEditor();
		this.txtInstallmentCount = new UltraTextEditor();
		this.lblInstallmentCount = new UltraLabel();
		this.btnEmployeesSearch = new UltraButton();
		this.chkAllInstallmentsPaid = new UltraCheckEditor();
		this.lblAdvanceType = new UltraLabel();
		this.cboAdvanceTypes = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvanceValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFirstPaymentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllInstallmentsPaid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceTypes).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		resources.ApplyResources(this.lblAdvanceValue, "lblAdvanceValue");
		this.lblAdvanceValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdvanceValue).Name = "lblAdvanceValue";
		((ControlBase)this.lblAdvanceValue).WrapText = false;
		resources.ApplyResources(this.txtAdvanceValue, "txtAdvanceValue");
		((System.Windows.Forms.Control)(object)this.txtAdvanceValue).Name = "txtAdvanceValue";
		((TextEditorControlBase)this.txtAdvanceValue).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAdvanceValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblInstallmentValue, "lblInstallmentValue");
		this.lblInstallmentValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallmentValue).Name = "lblInstallmentValue";
		((ControlBase)this.lblInstallmentValue).WrapText = false;
		resources.ApplyResources(this.txtInstallmentValue, "txtInstallmentValue");
		((System.Windows.Forms.Control)(object)this.txtInstallmentValue).Name = "txtInstallmentValue";
		((TextEditorControlBase)this.txtInstallmentValue).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInstallmentValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.cboEmployees, "cboEmployees");
		((TextEditorControlBase)this.cboEmployees).AlwaysInEditMode = true;
		this.cboEmployees.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEmployees).Name = "cboEmployees";
		resources.ApplyResources(this.lblEmployees, "lblEmployees");
		this.lblEmployees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmployees).Name = "lblEmployees";
		((ControlBase)this.lblEmployees).WrapText = false;
		resources.ApplyResources(this.lblFirstPaymentDate, "lblFirstPaymentDate");
		this.lblFirstPaymentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFirstPaymentDate).Name = "lblFirstPaymentDate";
		((ControlBase)this.lblFirstPaymentDate).WrapText = false;
		resources.ApplyResources(this.dtpFirstPaymentDate, "dtpFirstPaymentDate");
		((UltraWinEditorMaskedControlBase)this.dtpFirstPaymentDate).AlwaysInEditMode = true;
		this.dtpFirstPaymentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFirstPaymentDate).Name = "dtpFirstPaymentDate";
		this.dtpFirstPaymentDate.ValueChanged += new System.EventHandler(dtpFirstPaymentDate_ValueChanged);
		resources.ApplyResources(this.txtInstallmentCount, "txtInstallmentCount");
		((System.Windows.Forms.Control)(object)this.txtInstallmentCount).Name = "txtInstallmentCount";
		resources.ApplyResources(this.lblInstallmentCount, "lblInstallmentCount");
		this.lblInstallmentCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallmentCount).Name = "lblInstallmentCount";
		((ControlBase)this.lblInstallmentCount).WrapText = false;
		resources.ApplyResources(this.btnEmployeesSearch, "btnEmployeesSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnEmployeesSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Name = "btnEmployeesSearch";
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Click += new System.EventHandler(btnEmployeesSearch_Click);
		resources.ApplyResources(this.chkAllInstallmentsPaid, "chkAllInstallmentsPaid");
		((System.Windows.Forms.Control)(object)this.chkAllInstallmentsPaid).Name = "chkAllInstallmentsPaid";
		resources.ApplyResources(this.lblAdvanceType, "lblAdvanceType");
		this.lblAdvanceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdvanceType).Name = "lblAdvanceType";
		((ControlBase)this.lblAdvanceType).WrapText = false;
		resources.ApplyResources(this.cboAdvanceTypes, "cboAdvanceTypes");
		((TextEditorControlBase)this.cboAdvanceTypes).AlwaysInEditMode = true;
		this.cboAdvanceTypes.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAdvanceTypes).Name = "cboAdvanceTypes";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllInstallmentsPaid);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployeesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFirstPaymentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFirstPaymentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAdvanceTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdvanceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInstallmentValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdvanceValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdvanceValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmEmployeesAdvances";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdvanceValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdvanceValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInstallmentValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdvanceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAdvanceTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFirstPaymentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFirstPaymentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEmployeesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllInstallmentsPaid, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvanceValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFirstPaymentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllInstallmentsPaid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceTypes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
