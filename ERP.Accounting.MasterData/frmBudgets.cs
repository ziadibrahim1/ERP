using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.MasterData;

public class frmBudgets : frmHeaderDetails
{
	private bool UseSubAccounts;

	private bool UseCostCenters;

	private DataTable dtReports;

	private DataTable dtFiscalYears = new DataTable();

	private DataTable dtMonthsCount = new DataTable();

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtBudgetDetailsPeriods;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlMonthsCount = new ValueList();

	private ValueList vlMonths1 = new ValueList();

	private ValueList vlMonths2 = new ValueList();

	private int newID = -100000;

	private DataSet ds;

	private IContainer components = null;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameAr;

	private UltraLabel lblFiscalYear;

	private UltraComboEditor cboFiscalYear;

	public frmBudgets()
	{
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		InitializeComponent();
		TableName = "A_Budget";
		IDCol = "BudgetID";
		NoCol = "BudgetNo";
		DateCol = "GetDate()";
		dtMonthsCount.Columns.Add("Period_MonthsCount");
		dtMonthsCount.Columns.Add("Period_MonthsCountName");
		dtMonthsCount.Rows.Add("1", GlobalVariables.IsArabic ? "شهري" : "Monthly");
		dtMonthsCount.Rows.Add("3", GlobalVariables.IsArabic ? "ربع سنوي" : "Quarterly");
		dtMonthsCount.Rows.Add("6", GlobalVariables.IsArabic ? "نصف سنوي" : "Semi-Yearly");
		dtMonthsCount.Rows.Add("12", GlobalVariables.IsArabic ? "سنوي" : "Yearly");
		vlMonths1.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "يناير" : "January");
		vlMonths2.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "يناير" : "January");
		vlMonths1.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "فبراير" : "February");
		vlMonths2.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "فبراير" : "February");
		vlMonths1.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "مارس" : "March");
		vlMonths2.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "مارس" : "March");
		vlMonths1.ValueListItems.Add((object)"4", GlobalVariables.IsArabic ? "ابريل" : "April");
		vlMonths2.ValueListItems.Add((object)"4", GlobalVariables.IsArabic ? "ابريل" : "April");
		vlMonths1.ValueListItems.Add((object)"5", GlobalVariables.IsArabic ? "مايو" : "May");
		vlMonths2.ValueListItems.Add((object)"5", GlobalVariables.IsArabic ? "مايو" : "May");
		vlMonths1.ValueListItems.Add((object)"6", GlobalVariables.IsArabic ? "يونيو" : "June");
		vlMonths2.ValueListItems.Add((object)"6", GlobalVariables.IsArabic ? "يونيو" : "June");
		vlMonths1.ValueListItems.Add((object)"7", GlobalVariables.IsArabic ? "يوليو" : "July");
		vlMonths2.ValueListItems.Add((object)"7", GlobalVariables.IsArabic ? "يوليو" : "July");
		vlMonths1.ValueListItems.Add((object)"8", GlobalVariables.IsArabic ? "اغسطس" : "August");
		vlMonths2.ValueListItems.Add((object)"8", GlobalVariables.IsArabic ? "اغسطس" : "August");
		vlMonths1.ValueListItems.Add((object)"9", GlobalVariables.IsArabic ? "سبتمبر" : "September");
		vlMonths2.ValueListItems.Add((object)"9", GlobalVariables.IsArabic ? "سبتمبر" : "September");
		vlMonths1.ValueListItems.Add((object)"10", GlobalVariables.IsArabic ? "اكتوبر" : "October");
		vlMonths2.ValueListItems.Add((object)"10", GlobalVariables.IsArabic ? "اكتوبر" : "October");
		vlMonths1.ValueListItems.Add((object)"11", GlobalVariables.IsArabic ? "نوفمبر" : "November");
		vlMonths2.ValueListItems.Add((object)"11", GlobalVariables.IsArabic ? "نوفمبر" : "November");
		vlMonths1.ValueListItems.Add((object)"12", GlobalVariables.IsArabic ? "ديسمبر" : "December");
		vlMonths2.ValueListItems.Add((object)"12", GlobalVariables.IsArabic ? "ديسمبر" : "December");
	}

	public override void PrepareData()
	{
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		base.PrepareData();
		vlMonthsCount.ValueListItems.Clear();
		for (int i = 0; i < dtMonthsCount.Rows.Count; i++)
		{
			vlMonthsCount.ValueListItems.Add(dtMonthsCount.Rows[i]["Period_MonthsCount"], dtMonthsCount.Rows[i]["Period_MonthsCountName"].ToString());
		}
		dtAccounts = Accounts.FillComboByType(",3,4,", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtAccounts.Rows.Count; j++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[j]["AccountID"], dtAccounts.Rows[j]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubAccounts.ValueListItems.Clear();
			for (int k = 0; k < dtSubAccounts.Rows.Count; k++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[k]["SubAccountID"], dtSubAccounts.Rows[k]["Name"].ToString());
			}
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlCostCenters.ValueListItems.Clear();
			for (int l = 0; l < dtCostCenters.Rows.Count; l++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[l]["CostCenterID"], dtCostCenters.Rows[l]["Name"].ToString());
			}
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtFiscalYears = FiscalYear.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFiscalYear, dtFiscalYears, "FiscalYearID", "FiscalYearName");
		dtDetails = BudgetDetails.SelectByBudgetID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtBudgetDetailsPeriods = BudgetDetailsPeriods.SelectByBudgetID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtBudgetDetailsPeriods);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtBudgetDetailsPeriods";
		ds.Relations.Add(ds.Tables[0].Columns["BudgetDetailID"], ds.Tables[1].Columns["BudgetDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.19);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.19);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period_MonthsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowingPercent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period_MonthsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowingPercent"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلي" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفه" : "Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period_MonthsCount"].Header).Caption = (GlobalVariables.IsArabic ? "الفتره" : "Period");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowingPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة النمو" : "Growing Percent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Period_MonthsCount"].ValueList = (IValueList)(object)vlMonthsCount;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromMonth"].Header).Caption = (GlobalVariables.IsArabic ? "من شهر" : "From Month");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToMonth"].Header).Caption = (GlobalVariables.IsArabic ? "الي شهر" : "To Month");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromMonth"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToMonth"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["value"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromMonth"].ValueList = (IValueList)(object)vlMonths1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToMonth"].ValueList = (IValueList)(object)vlMonths2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["value"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Budget.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["BudgetNo"].ToString();
			((TextEditorControlBase)cboFiscalYear).Value = drMaster["FiscalYearID"];
			((Control)(object)txtNameAr).Text = drMaster["BudgetNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["BudgetNameEn"].ToString();
			dtDetails = BudgetDetails.SelectByBudgetID(drMaster["BudgetID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtBudgetDetailsPeriods = BudgetDetailsPeriods.SelectByBudgetID(drMaster["BudgetID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtBudgetDetailsPeriods);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtBudgetDetailsPeriods";
			ds.Relations.Add(ds.Tables[0].Columns["BudgetDetailID"], ds.Tables[1].Columns["BudgetDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			if (UseSubAccounts)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value != DBNull.Value)
					{
						int accountID = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString());
						ValueList subAccountValueList = getSubAccountValueList(accountID);
						((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
						if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = DBNull.Value;
						}
					}
				}
			}
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
		((EditorButtonControlBase)cboFiscalYear).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		if (!Updating || !UseSubAccounts)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value != DBNull.Value)
			{
				int accountID = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString());
				ValueList subAccountValueList = getSubAccountValueList(accountID);
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
				if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = DBNull.Value;
				}
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Budget.GetCode(IsFromServer: true) : "");
		cboFiscalYear.SelectedIndex = -1;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكود" : "Please Enter The Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboFiscalYear.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار السنه الماليه" : "Please Select Fiscal Year ");
			((TextEditorControlBase)cboFiscalYear).Focus();
			cboFiscalYear.DropDown();
			return false;
		}
		if (((Control)(object)txtNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال إسم الموازنه" : "Please  Enter Budget Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال حسابات الموازنه" : "Please Enter Accounts For This Budget ");
			return false;
		}
		if (Main.CheckForValue("A_Budget", "BudgetNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BudgetNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = Budget.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (Main.CheckForValue("A_Budget", "BudgetNameAr", ((Control)(object)txtNameAr).Text, Updating ? drMaster["BudgetNameAr"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" إسم الموازنه متواجد من قبل ", "Budget Arabic Name Already Exist");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الحساب أو حذف السطر  ", "Please Enter Account Name or Delete Row ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"];
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Budget.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboFiscalYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFiscalYear).Value.ToString(), "0", "1", GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = BudgetDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Period_MonthsCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["GrowingPercent"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["GrowingPercent"].Value.ToString(), dtAccounts.Select("AccountID=" + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value)[0]["AccountTypeID"].Equals(3) ? "1" : "0", "0", "1", GlobalVariables.UserID, IsFromServer: true);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					BudgetDetailsPeriods.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromMonth"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToMonth"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["value"].Value.ToString(), "0", "1", GlobalVariables.UserID, IsFromServer: true);
				}
			}
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
			int num = Budget.Insert_Update(drMaster["BudgetID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboFiscalYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFiscalYear).Value.ToString(), "0", "1", GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BudgetDetailID"].Value.ToString() + ",";
			}
			BudgetDetailsPeriods.DeleteByBudgetID(drMaster["BudgetID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.SyncDeleteForUpdate("A_BudgetDetails", "BudgetID", num.ToString(), "BudgetDetailID", text, IsFromServer: true);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = BudgetDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["BudgetDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["BudgetDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[j].Cells["BudgetDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Period_MonthsCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["GrowingPercent"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["GrowingPercent"].Value.ToString(), dtAccounts.Select("AccountID=" + ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value)[0]["AccountTypeID"].Equals(3) ? "1" : "0", "0", "1", GlobalVariables.UserID, IsFromServer: true);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					BudgetDetailsPeriods.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["FromMonth"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ToMonth"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["value"].Value.ToString(), "0", "1", GlobalVariables.UserID, IsFromServer: true);
				}
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
		base.DeleteData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			BudgetDetailsPeriods.DeleteByBudgetID(drMaster["BudgetID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BudgetDetails.DeleteByBudgetID(drMaster["BudgetID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Budget.Delete(drMaster["BudgetID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		dtFiscalYears = FiscalYear.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboFiscalYear, dtFiscalYears, "FiscalYearID", "FiscalYearName");
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_BudgetDetails_A.rpt" : "Rep_A_BudgetDetails_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BudgetIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		if (((GridItemBase)e.Cell).Band.Index == 0 && ((KeyedSubObjectBase)e.Cell.Column).Key == "Period_MonthsCount")
		{
			string text = e.Cell.Row.Cells["BudgetDetailID"].Value.ToString();
			DataRow[] array = dtBudgetDetailsPeriods.Select("BudgetDetailID=" + text);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Delete();
			}
			if (e.Cell.Value.Equals(1))
			{
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(1, 1, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(2, 2, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(3, 3, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(4, 4, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(5, 5, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(6, 6, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(7, 7, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(8, 8, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(9, 9, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(10, 10, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(11, 11, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(12, 12, text));
			}
			else if (e.Cell.Value.Equals(3))
			{
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(1, 3, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(4, 6, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(7, 9, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(10, 12, text));
			}
			else if (e.Cell.Value.Equals(6))
			{
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(1, 6, text));
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(7, 12, text));
			}
			else if (e.Cell.Value.Equals(12))
			{
				dtBudgetDetailsPeriods.Rows.Add(GetdtBudgetRow(1, 12, text));
			}
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["BudgetDetailID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID" && UseSubAccounts)
		{
			int num = ((vlAccounts.SelectedItem != null) ? int.Parse(dtAccounts.Rows[vlAccounts.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["SubAccountID"].ValueList = null;
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["CostCenterID"].Value = DBNull.Value;
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Period_MonthsCount")
		{
			((UltraGridBase)ULGData).UpdateData();
		}
		else if (((GridItemBase)e.Cell.Row).Band.Index == 0 && e.Cell.Row.IsAddRow && e.Cell.Row.Index > 0 && e.Cell.Row.Cells["Period_MonthsCount"].Value.Equals(DBNull.Value))
		{
			((UltraGridBase)ULGData).UpdateData();
			e.Cell.Row.Cells["Period_MonthsCount"].Value = ((UltraGridBase)ULGData).Rows[e.Cell.Row.Index - 1].Cells["Period_MonthsCount"].Value;
		}
	}

	private DataRow GetdtBudgetRow(int from, int to, string DetailID)
	{
		DataRow dataRow = dtBudgetDetailsPeriods.NewRow();
		dataRow["BudgetDetailID"] = DetailID;
		dataRow["FromMonth"] = from;
		dataRow["ToMonth"] = to;
		dataRow["value"] = 0;
		return dataRow;
	}

	private ValueList getSubAccountValueList(int AccountID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtSubAccounts.Select("AccountID=" + AccountID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		if (!e.Row.IsAddRow || e.Row.Index <= 0 || e.Row.Cells["AccountID"].Value != DBNull.Value || ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["AccountID"].Value.Equals(DBNull.Value))
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		e.Row.Cells["AccountID"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["AccountID"].Value;
		e.Row.Cells["GrowingPercent"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["GrowingPercent"].Value;
		if (UseSubAccounts)
		{
			int accountID = int.Parse(e.Row.Cells["AccountID"].Value.ToString());
			ValueList subAccountValueList = getSubAccountValueList(accountID);
			e.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
			if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
			{
				e.Row.Cells["SubAccountID"].Value = DBNull.Value;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)ULGData.ActiveCell).Band.Index == 1 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "value")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((GridItemBase)ULGData.ActiveCell).Band.Index == 0 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Period_MonthsCount" && ULGData.ActiveCell.Row.IsAddRow)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Expected O, but got Unknown
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmBudgets));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.lblFiscalYear = new UltraLabel();
		this.cboFiscalYear = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFiscalYear).BeginInit();
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.lblFiscalYear, "lblFiscalYear");
		this.lblFiscalYear.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFiscalYear).Name = "lblFiscalYear";
		((ControlBase)this.lblFiscalYear).WrapText = false;
		resources.ApplyResources(this.cboFiscalYear, "cboFiscalYear");
		((TextEditorControlBase)this.cboFiscalYear).AlwaysInEditMode = true;
		this.cboFiscalYear.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFiscalYear).Name = "cboFiscalYear";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFiscalYear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFiscalYear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Name = "frmBudgets";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFiscalYear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFiscalYear, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFiscalYear).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
