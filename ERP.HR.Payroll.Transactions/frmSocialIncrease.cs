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

namespace ERP.HR.Payroll.Transactions;

public class frmSocialIncrease : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtEmployees;

	private ValueList vlEmployees = new ValueList();

	private IContainer components = null;

	private UltraLabel lblSocialIncreasePercentage;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblSocialIncreaseMinValue;

	private UltraTextEditor txtSocialIncreaseMinValue;

	private UltraLabel lblSocialIncreaseMaxValue;

	private UltraTextEditor txtSocialIncreaseMaxValue;

	private UltraTextEditor txtSocialIncreasePercentage;

	private UltraButton btnEmployees;

	public frmSocialIncrease()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "HR_SocialIncrease";
		IDCol = "SocialIncreaseID";
		NoCol = "SocialIncreaseNo";
		DateCol = "SocialIncreaseStartDate";
	}

	public frmSocialIncrease(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlEmployees.ValueListItems.Clear();
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString() + " - " + dtEmployees.Rows[i]["EmployeeNo"].ToString());
		}
		dtDetails = SocialIncreaseDetails.SelectBySocialIncreaseID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAddedToVariant"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة العلاوة" : "Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAddedToVariant"].Header).Caption = (GlobalVariables.IsArabic ? "مضاف للمتغير" : "Add To Variant");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAddedToVariant"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsAddedToVariant"].DefaultCellValue = false;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = SocialIncrease.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((TextEditorControlBase)txtSocialIncreasePercentage).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtSocialIncreaseMaxValue).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtSocialIncreaseMinValue).ValueChanged -= txt_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["SocialIncreaseNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SocialIncreaseStartDate"];
			((Control)(object)txtSocialIncreasePercentage).Text = decimal.Parse(drMaster["SocialIncreasePercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSocialIncreaseMinValue).Text = decimal.Parse(drMaster["SocialIncreaseMinValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSocialIncreaseMaxValue).Text = decimal.Parse(drMaster["SocialIncreaseMaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = SocialIncreaseDetails.SelectBySocialIncreaseID(drMaster["SocialIncreaseID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			((TextEditorControlBase)txtSocialIncreasePercentage).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtSocialIncreaseMaxValue).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtSocialIncreaseMinValue).ValueChanged += txt_ValueChanged;
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSocialIncreasePercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSocialIncreaseMinValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSocialIncreasePercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnEmployees).Enabled = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtSocialIncreasePercentage).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtSocialIncreaseMaxValue).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtSocialIncreaseMinValue).ValueChanged -= txt_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? SocialIncrease.GetCode(IsFromServer: true) : "");
		((Control)(object)txtSocialIncreaseMinValue).Text = "0";
		((Control)(object)txtSocialIncreasePercentage).Text = "0";
		((Control)(object)txtSocialIncreaseMaxValue).Text = "0";
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtSocialIncreasePercentage).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtSocialIncreaseMaxValue).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtSocialIncreaseMinValue).ValueChanged += txt_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ العلاوة" : "Please Enter The Social Increase Date");
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
		if (((Control)(object)txtSocialIncreasePercentage).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة نسبة العلاوة الاجتماعية" : "Please Enter Social Increase percentage");
			((TextEditorControlBase)txtSocialIncreasePercentage).Focus();
			return false;
		}
		if (((Control)(object)txtSocialIncreaseMaxValue).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة الحد الاقصى للعلاوة الاجتماعية" : "Please Enter Social Increase Max Value");
			((TextEditorControlBase)txtSocialIncreaseMaxValue).Focus();
			return false;
		}
		if (((Control)(object)txtSocialIncreaseMinValue).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة الحد الادنى للعلاوة الاجتماعية" : "Please Enter Social Increase Min Value");
			((TextEditorControlBase)txtSocialIncreaseMinValue).Focus();
			return false;
		}
		if (Main.CheckForValue("HR_SocialIncrease", "SocialIncreaseNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["SocialIncreaseNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = SocialIncrease.GetCode(IsFromServer: true);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الموظف  ", "Please Enter Employee Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseValue"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة العلاوة الاجتماعية  ", "Please Enter Social Increase Amount ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseValue"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الموظف ", "Cannot Duplicate The Same Employee");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
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
			int num = SocialIncrease.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtSocialIncreasePercentage).Text.ToString(), ((Control)(object)txtSocialIncreaseMinValue).Text.ToString(), ((Control)(object)txtSocialIncreaseMaxValue).Text.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			SocialIncreaseDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			RowID = num.ToString();
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
			int num = SocialIncrease.Insert_Update(drMaster["SocialIncreaseID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtSocialIncreasePercentage).Text.ToString(), ((Control)(object)txtSocialIncreaseMinValue).Text.ToString(), ((Control)(object)txtSocialIncreaseMaxValue).Text.ToString(), ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseDetailID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("HR_SocialIncreaseDetails", "SocialIncreaseID", drMaster["SocialIncreaseID"].ToString(), "SocialIncreaseDetailID", text, IsFromServer: true);
			SocialIncreaseDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			SocialIncreaseDetails.DeleteBySocialIncreaseID(drMaster["SocialIncreaseID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			SocialIncrease.Delete(drMaster["SocialIncreaseID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_SocialIncrease_A.rpt" : "Rep_HR_SocialIncrease_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@SocialIncreaseIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SocialIncreaseReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SocialIncreaseID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlEmployees.ValueListItems.Clear();
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString() + " - " + dtEmployees.Rows[i]["EmployeeNo"].ToString());
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && ((Control)(object)txtSocialIncreasePercentage).Text != "" && ((Control)(object)txtSocialIncreaseMinValue).Text != "" && ((Control)(object)txtSocialIncreaseMaxValue).Text != "")
		{
			decimal num = default(decimal);
			num = decimal.Parse((dtEmployees.Select(" SubAccountID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString())[0]["CurrentBasicSalary"] == DBNull.Value) ? "0" : dtEmployees.Select(" SubAccountID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString())[0]["CurrentBasicSalary"].ToString()) * decimal.Parse(((Control)(object)txtSocialIncreasePercentage).Text) / 100m;
			if (num >= decimal.Parse(((Control)(object)txtSocialIncreaseMaxValue).Text))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["SocialIncreaseValue"].Value = ((Control)(object)txtSocialIncreaseMaxValue).Text;
			}
			else if (num <= decimal.Parse(((Control)(object)txtSocialIncreaseMinValue).Text))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["SocialIncreaseValue"].Value = ((Control)(object)txtSocialIncreaseMinValue).Text;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["SocialIncreaseValue"].Value = num;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsAddedToVariant")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SocialIncreaseValue")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txt_ValueChanged(object sender, EventArgs e)
	{
		if (!(((Control)(object)txtSocialIncreasePercentage).Text != "") || !(((Control)(object)txtSocialIncreaseMinValue).Text != "") || !(((Control)(object)txtSocialIncreaseMaxValue).Text != ""))
		{
			return;
		}
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num = decimal.Parse((dtEmployees.Select(" SubAccountID =" + ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString())[0]["CurrentBasicSalary"] == DBNull.Value) ? "0" : dtEmployees.Select(" SubAccountID =" + ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString())[0]["CurrentBasicSalary"].ToString()) * decimal.Parse(((Control)(object)txtSocialIncreasePercentage).Text) / 100m;
			if (num >= decimal.Parse(((Control)(object)txtSocialIncreaseMaxValue).Text))
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseValue"].Value = ((Control)(object)txtSocialIncreaseMaxValue).Text;
			}
			else if (num <= decimal.Parse(((Control)(object)txtSocialIncreaseMinValue).Text))
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseValue"].Value = ((Control)(object)txtSocialIncreaseMinValue).Text;
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SocialIncreaseValue"].Value = num;
			}
		}
	}

	private void btnEmployees_Click(object sender, EventArgs e)
	{
		string text = ",";
		dtSearchResult = SearchFunctions.EmployeesReport("-1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			if (((DataTable)((UltraGridBase)ULGData).DataSource).Select("SubAccountID =" + dtSearchResult.Rows[i]["SubAccountID"].ToString()).Length == 0)
			{
				text = text + dtSearchResult.Rows[i]["SubAccountID"].ToString() + ",";
			}
		}
		if (text != "," && ((Control)(object)txtSocialIncreasePercentage).Text != "" && ((Control)(object)txtSocialIncreaseMinValue).Text != "" && ((Control)(object)txtSocialIncreaseMaxValue).Text != "")
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Merge(SocialIncreaseDetails.SelectBySubAccountIDs(text, ((Control)(object)txtSocialIncreasePercentage).Text, ((Control)(object)txtSocialIncreaseMinValue).Text, ((Control)(object)txtSocialIncreaseMaxValue).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true));
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Payroll.Transactions.frmSocialIncrease));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblSocialIncreasePercentage = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblSocialIncreaseMinValue = new UltraLabel();
		this.txtSocialIncreaseMinValue = new UltraTextEditor();
		this.lblSocialIncreaseMaxValue = new UltraLabel();
		this.txtSocialIncreaseMaxValue = new UltraTextEditor();
		this.txtSocialIncreasePercentage = new UltraTextEditor();
		this.btnEmployees = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreaseMinValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreaseMaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreasePercentage).BeginInit();
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
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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
		resources.ApplyResources(this.lblSocialIncreasePercentage, "lblSocialIncreasePercentage");
		this.lblSocialIncreasePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialIncreasePercentage).Name = "lblSocialIncreasePercentage";
		((ControlBase)this.lblSocialIncreasePercentage).WrapText = false;
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
		resources.ApplyResources(this.lblSocialIncreaseMinValue, "lblSocialIncreaseMinValue");
		this.lblSocialIncreaseMinValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMinValue).Name = "lblSocialIncreaseMinValue";
		((ControlBase)this.lblSocialIncreaseMinValue).WrapText = false;
		resources.ApplyResources(this.txtSocialIncreaseMinValue, "txtSocialIncreaseMinValue");
		((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMinValue).Name = "txtSocialIncreaseMinValue";
		((TextEditorControlBase)this.txtSocialIncreaseMinValue).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMinValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSocialIncreaseMaxValue, "lblSocialIncreaseMaxValue");
		this.lblSocialIncreaseMaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMaxValue).Name = "lblSocialIncreaseMaxValue";
		((ControlBase)this.lblSocialIncreaseMaxValue).WrapText = false;
		resources.ApplyResources(this.txtSocialIncreaseMaxValue, "txtSocialIncreaseMaxValue");
		((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMaxValue).Name = "txtSocialIncreaseMaxValue";
		((TextEditorControlBase)this.txtSocialIncreaseMaxValue).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSocialIncreasePercentage, "txtSocialIncreasePercentage");
		((System.Windows.Forms.Control)(object)this.txtSocialIncreasePercentage).Name = "txtSocialIncreasePercentage";
		((TextEditorControlBase)this.txtSocialIncreasePercentage).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSocialIncreasePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.btnEmployees, "btnEmployees");
		((System.Windows.Forms.Control)(object)this.btnEmployees).Name = "btnEmployees";
		((System.Windows.Forms.Control)(object)this.btnEmployees).Click += new System.EventHandler(btnEmployees_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSocialIncreasePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMinValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMinValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialIncreasePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmSocialIncrease";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSocialIncreasePercentage, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMinValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMinValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSocialIncreaseMaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSocialIncreaseMaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSocialIncreasePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEmployees, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreaseMinValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreaseMaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialIncreasePercentage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
