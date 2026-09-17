using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using BusinessLayer.SafesAndBanks;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.SafeTransactions;

public class frmCustody : frmHeaderDetails
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private DataTable dtEmployees;

	private DataTable dtReports;

	private DataTable dtJvDetails;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSubCostCenters = new ValueList();

	private bool UseSubAccounts;

	private bool UseCostCenters;

	private bool UseCurrency;

	private bool useSubCostCenters;

	private string CustodyAccountID = "";

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	private UltraLabel lblEmployeeName;

	private UltraComboEditor cboEmployee;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangrRate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnJV;

	public UltraButton btnEmployeeSearch;

	public frmCustody()
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
		TableName = "SB_Custody";
		IDCol = "CustodyID";
		NoCol = "CustodyNo";
		DateCol = "CustodyDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public frmCustody(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
		dtpDate.DateTime = DateTime.Now;
		dtpDate.ValueChanged += dtpJVDate_ValueChanged;
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		useSubCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_SubCostCenter Where IsMain = 0").Rows[0][0].ToString()) > 0;
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlCostCenters.ValueListItems.Clear();
			for (int k = 0; k < dtCostCenters.Rows.Count; k++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[k]["CostCenterID"], dtCostCenters.Rows[k]["Name"].ToString());
			}
		}
		if (useSubCostCenters)
		{
			dtSubCostCenters = SubCostCenter.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubCostCenters.ValueListItems.Clear();
			for (int l = 0; l < dtSubCostCenters.Rows.Count; l++)
			{
				vlSubCostCenters.ValueListItems.Add(dtSubCostCenters.Rows[l]["SubCostCenterID"], dtSubCostCenters.Rows[l]["Name"].ToString());
			}
		}
		FillCurrencyDropDown();
		dtDetails = CustodyDetails.SelectByCustodyID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtEmployees = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboEmployee, dtEmployees, "SubAccountID", "Name");
		}
		else
		{
			GlobalVariables.InformationMB.Show("حساب العهد غير معرف فى حسابات النظام", "Custody Account Not Defined in System Account");
		}
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Custody.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["CustodyNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["CustodyDate"];
			((TextEditorControlBase)cboEmployee).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["Total"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo"].ToString() + ")قيد") : ("No( " + drMaster["JVNo"].ToString() + " )"));
			((Control)(object)btnJV).Visible = ((drMaster["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = CustodyDetails.SelectByCustodyID(drMaster["CustodyID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			if (drMaster["Approved"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
			dtpDate.ValueChanged += dtpJVDate_ValueChanged;
			ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode || !CanEditDate;
		((EditorButtonControlBase)cboEmployee).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnEmployeeSearch).Visible = !NavMode;
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
		if (!CanEditDate)
		{
			FillCurrencyDropDown();
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
			dtpDate.ValueChanged += dtpJVDate_ValueChanged;
		}
		cboEmployee.SelectedIndex = ((((DisposableObjectCollectionBase)cboEmployee.Items).Count <= 0) ? (-1) : 0);
		((Control)(object)txtCode).Text = (Adding ? Custody.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboCurrency.SelectedIndex = ((((DisposableObjectCollectionBase)cboCurrency.Items).Count != 1) ? (-1) : 0);
		((Control)(object)txtExchangeRate).Text = ((((TextEditorControlBase)cboCurrency).Value != null && dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString()).Length != 0) ? dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString() : "");
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyDetailsID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].DefaultCellValue = false;
		if (UseCostCenters)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.32);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلي" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة التحليلى" : "Sub Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Header).Caption = (GlobalVariables.IsArabic ? "مستندى" : "Documented");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = !UseSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = !UseCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Hidden = !useSubCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].ValueList = (IValueList)(object)vlSubCostCenters;
	}

	public override bool ValidateData()
	{
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboEmployee.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم الموظف" : "Please Select Employee Name ");
			((TextEditorControlBase)cboEmployee).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SB_Custody", "CustodyNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CustodyNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Custody.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		else
		{
			if (cboCurrency.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار العملة", "Please select Currency");
				((TextEditorControlBase)cboCurrency).Focus();
				return false;
			}
			if (decimal.Parse(((Control)(object)txtExchangeRate).Text) <= 0m || ((Control)(object)txtExchangeRate).Text == "")
			{
				GlobalVariables.InformationMB.Show("سعر التحويل لابد ان يكون اكبر من الصفر", "Exchange Rate Must Be Greater Than Zero");
				((TextEditorControlBase)txtExchangeRate).Focus();
				return false;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الحساب أو حذف السطر  ", "Please Enter Account Name or Delete Row ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال القيمة  ", "Please Enter Amount ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Value"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UseSubAccounts && GlobalFunctions.GetOption("EnforceSubAccountsUse") && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار حساب تحليلي", "Please choose Sub-Account");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UseCostCenters && GlobalFunctions.GetOption("EnforceCostCentersUse") && (dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "3" || dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "4") && ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار مركز تكلفة", "Please choose Cost-Center");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Custody.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboEmployee).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
			DataRow dataRow;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value;
				dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value;
				dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value;
				dataRow["SubCostCenterID"] = ((UltraGridBase)ULGData).Rows[i].Cells["SubCostCenterID"].Value;
				dataRow["Debit"] = ((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["IsDocumented"] = ((UltraGridBase)ULGData).Rows[i].Cells["IsDocumented"].Value;
				dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["CustodyDetailsID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["CustodyID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			dataRow = dtJvDetails.NewRow();
			dataRow["JVDetailID"] = "-1";
			dataRow["AccountID"] = dtEmployees.Select(" SubAccountID= " + ((TextEditorControlBase)cboEmployee).Value.ToString())[0]["AccountID"];
			dataRow["SubAccountID"] = ((TextEditorControlBase)cboEmployee).Value;
			dataRow["Debit"] = "0";
			dataRow["Credit"] = decimal.Parse(((Control)(object)txtTotal).Text);
			dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
			dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
			dataRow["LocalDebit"] = "0";
			dataRow["LocalCredit"] = decimal.Parse(((Control)(object)txtTotal).Text) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtJvDetails.Rows.Add(dataRow);
			CustodyDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "23");
			int num2 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "23", num.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
			Main.ExecuteNonQuery(" Update SB_Custody  set JvID= " + num2 + " Where CustodyID=" + num.ToString());
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Custody.Insert_Update(drMaster["CustodyID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboEmployee).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, drMaster["JVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.UserID);
			JVDetails.DeleteByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID);
			dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
			string text = ",";
			DataRow dataRow;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value;
				dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value;
				dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value;
				dataRow["SubCostCenterID"] = ((UltraGridBase)ULGData).Rows[i].Cells["SubCostCenterID"].Value;
				dataRow["Debit"] = ((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["IsDocumented"] = ((UltraGridBase)ULGData).Rows[i].Cells["IsDocumented"].Value;
				dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["CustodyID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["CustodyDetailsID"].Value.ToString() + ",";
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			dataRow = dtJvDetails.NewRow();
			dataRow["JVDetailID"] = "-1";
			dataRow["AccountID"] = dtEmployees.Select(" SubAccountID= " + ((TextEditorControlBase)cboEmployee).Value.ToString())[0]["AccountID"];
			dataRow["SubAccountID"] = ((TextEditorControlBase)cboEmployee).Value;
			dataRow["Debit"] = "0";
			dataRow["Credit"] = decimal.Parse(((Control)(object)txtTotal).Text);
			dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
			dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
			dataRow["LocalDebit"] = "0";
			dataRow["LocalCredit"] = decimal.Parse(((Control)(object)txtTotal).Text) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtJvDetails.Rows.Add(dataRow);
			Main.DeleteForUpdate("SB_CustodyDetails", "CustodyID", drMaster["CustodyID"].ToString(), "CustodyDetailsID", text);
			CustodyDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			int num2 = JV.GenerateJV_Update(JVNo: (drMaster["JVDate"] == DBNull.Value || (DateTime.Parse(drMaster["JVDate"].ToString()).Month == dtpDate.DateTime.Month && DateTime.Parse(drMaster["JVDate"].ToString()).Year == dtpDate.DateTime.Year)) ? drMaster["JVNo"].ToString() : JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "23"), JVID: drMaster["JVID"].ToString(), JVDate: dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), TransTypeID: "23", VoucherID: num.ToString(), ReceiptNo: ((Control)(object)txtCode).Text, HNotes: ((Control)(object)txtNotes).Text, IsOpenningJv: "0", Approved: "1", dtJvDetails: dtJvDetails, Deleted: "0", BranchID: GlobalVariables.CurrentBranchID, IsInternalJV: "1", UserID: GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Custody.DeleteVirtual(drMaster["CustodyID"].ToString(), GlobalVariables.UserID);
			CustodyDetails.DeleteVirtualByCustodyID(drMaster["CustodyID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SB_Custody_A.rpt" : "Rep_SB_Custody_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@CustodyIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CustodyReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CustodyID"].ToString();
			FillData();
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (UseSubAccounts && ((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID")
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
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Value" && e.Cell.Value == DBNull.Value)
		{
			e.Cell.Value = 0;
		}
		CalculateTotals();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		if (!e.Row.IsAddRow || e.Row.Index <= 0 || e.Row.Cells["AccountID"].Value != DBNull.Value || ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["AccountID"].Value.Equals(DBNull.Value))
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		e.Row.Cells["AccountID"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["AccountID"].Value;
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
		e.Row.Cells["SubAccountID"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["SubAccountID"].Value;
		e.Row.Cells["CostCenterID"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["CostCenterID"].Value;
		e.Row.Cells["Notes"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["Notes"].Value;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyValue != 119)
		{
			return;
		}
		if (Adding || Updating)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AccountID")
			{
				int num = SearchFunctions.Accounts(IsFromServer: false);
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value = num;
				}
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SubAccountID" && ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value != DBNull.Value)
			{
				int num2 = SearchFunctions.SubAccounts(((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value.ToString(), IsFromServer: false);
				if (num2 != 0)
				{
					ULGData.ActiveCell.Value = num2;
				}
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CostCenterID")
			{
				int num3 = SearchFunctions.CostCenter(IsFromServer: false);
				if (num3 != 0)
				{
					ULGData.ActiveCell.Value = num3;
				}
			}
		}
		e.Handled = true;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Value")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
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

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void dtpJVDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Custody.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString());
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public override void btnRefreshDataClick()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlCostCenters.ValueListItems.Clear();
			for (int k = 0; k < dtCostCenters.Rows.Count; k++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[k]["CostCenterID"], dtCostCenters.Rows[k]["Name"].ToString());
			}
		}
		if (useSubCostCenters)
		{
			dtSubCostCenters = SubCostCenter.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubCostCenters.ValueListItems.Clear();
			for (int l = 0; l < dtSubCostCenters.Rows.Count; l++)
			{
				vlSubCostCenters.ValueListItems.Add(dtSubCostCenters.Rows[l]["SubCostCenterID"], dtSubCostCenters.Rows[l]["Name"].ToString());
			}
		}
		FillCurrencyDropDown();
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtEmployees = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboEmployee, dtEmployees, "SubAccountID", "Name");
		}
		else
		{
			GlobalVariables.InformationMB.Show("حساب العهد غير معرف فى حسابات النظام", "Custody Account Not Defined in System Account");
		}
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboEmployee_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if ((Adding || Updating) && e.KeyValue == 119 && CustodyAccountID != "")
		{
			int num = SearchFunctions.SubAccounts(CustodyAccountID, IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)(UltraComboEditor)sender).Value = num;
			}
		}
	}

	private void btnEmployeeSearch_Click(object sender, EventArgs e)
	{
		if ((Adding || Updating) && CustodyAccountID != "")
		{
			int num = SearchFunctions.SubAccounts(CustodyAccountID, IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboEmployee).Value = num;
			}
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
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Expected O, but got Unknown
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Expected O, but got Unknown
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.SafeTransactions.frmCustody));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblEmployeeName = new UltraLabel();
		this.cboEmployee = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangrRate = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnJV = new UltraButton();
		this.btnEmployeeSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployee).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpJVDate_ValueChanged);
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.lblEmployeeName, "lblEmployeeName");
		this.lblEmployeeName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmployeeName).Name = "lblEmployeeName";
		((ControlBase)this.lblEmployeeName).WrapText = false;
		resources.ApplyResources(this.cboEmployee, "cboEmployee");
		((TextEditorControlBase)this.cboEmployee).AlwaysInEditMode = true;
		this.cboEmployee.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEmployee).Name = "cboEmployee";
		((System.Windows.Forms.Control)(object)this.cboEmployee).KeyDown += new System.Windows.Forms.KeyEventHandler(cboEmployee_KeyDown);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangrRate, "lblExchangrRate");
		this.lblExchangrRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangrRate).Name = "lblExchangrRate";
		((ControlBase)this.lblExchangrRate).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		((UltraButtonBase)this.btnEmployeeSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnEmployeeSearch, "btnEmployeeSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnEmployeeSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnEmployeeSearch).Name = "btnEmployeeSearch";
		((System.Windows.Forms.Control)(object)this.btnEmployeeSearch).Click += new System.EventHandler(btnEmployeeSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployeeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEmployeeName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangrRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmCustody";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangrRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEmployeeName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEmployeeSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployee).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
