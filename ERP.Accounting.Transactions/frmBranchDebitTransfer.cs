using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Transactions;

public class frmBranchDebitTransfer : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private DataTable dtBranchs;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSubCostCenters = new ValueList();

	private ValueList vlBranchs = new ValueList();

	private bool UseSubAccounts;

	private bool UseCostCenters;

	private bool useSubCostCenters;

	private IContainer components = null;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblDate;

	private UltraLabel lblTotalCredit;

	private UltraLabel lblTotalDebit;

	private UltraTextEditor txtTotalCredit;

	private UltraTextEditor txtTotalDebit;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtDiff;

	private UltraLabel lblDiff;

	private UltraLabel lblCurrency;

	private UltraLabel lblExchangeRate;

	private UltraTextEditor txtExchangeRate;

	public frmBranchDebitTransfer()
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
		InitializeComponent();
		TableName = "A_BranchDebitTransfers";
		IDCol = "BranchDebitTransferID";
		NoCol = "BranchDebitTransferNo";
		DateCol = "BranchDebitTransferDate";
	}

	public frmBranchDebitTransfer(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		useSubCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_SubCostCenter Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlCostCenters.ValueListItems.Clear();
			for (int k = 0; k < dtCostCenters.Rows.Count; k++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[k]["CostCenterID"], dtCostCenters.Rows[k]["Name"].ToString());
			}
		}
		if (useSubCostCenters)
		{
			dtSubCostCenters = SubCostCenter.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlSubCostCenters.ValueListItems.Clear();
			for (int l = 0; l < dtSubCostCenters.Rows.Count; l++)
			{
				vlSubCostCenters.ValueListItems.Add(dtSubCostCenters.Rows[l]["SubCostCenterID"], dtSubCostCenters.Rows[l]["Name"].ToString());
			}
		}
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranchs.ValueListItems.Clear();
		for (int m = 0; m < dtBranchs.Rows.Count; m++)
		{
			vlBranchs.ValueListItems.Add(dtBranchs.Rows[m]["BranchID"], dtBranchs.Rows[m]["BranchName"].ToString());
		}
		dtDetails = BranchDebitTransfersDetails.SelectByBranchDebitTransferID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		FillCurrencyDropDown();
	}

	public override void btnRefreshDataClick()
	{
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		useSubCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_SubCostCenter Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlCostCenters.ValueListItems.Clear();
			for (int k = 0; k < dtCostCenters.Rows.Count; k++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[k]["CostCenterID"], dtCostCenters.Rows[k]["Name"].ToString());
			}
		}
		if (useSubCostCenters)
		{
			dtSubCostCenters = SubCostCenter.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlSubCostCenters.ValueListItems.Clear();
			for (int l = 0; l < dtSubCostCenters.Rows.Count; l++)
			{
				vlSubCostCenters.ValueListItems.Add(dtSubCostCenters.Rows[l]["SubCostCenterID"], dtSubCostCenters.Rows[l]["Name"].ToString());
			}
		}
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranchs.ValueListItems.Clear();
		for (int m = 0; m < dtBranchs.Rows.Count; m++)
		{
			vlBranchs.ValueListItems.Add(dtBranchs.Rows[m]["BranchID"], dtBranchs.Rows[m]["BranchName"].ToString());
		}
		FillCurrencyDropDown();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BranchDebitTransfers.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BranchDebitTransferNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["BranchDebitTransferDate"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalDebit).Text = decimal.Parse(drMaster["TotalDebit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiff).Text = decimal.Parse((Convert.ToDecimal(drMaster["TotalDebit"]) - Convert.ToDecimal(drMaster["TotalCredit"])).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalCredit).Text = decimal.Parse(drMaster["TotalCredit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = BranchDebitTransfersDetails.SelectByBranchDebitTransferID(drMaster["BranchDebitTransferID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			string text = ",";
			for (int i = 0; i < dtDetails.Rows.Count; i++)
			{
				text = text + dtDetails.Rows[0]["BranchID"].ToString() + ",";
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriodByBranchs(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), text, IsFromServer: true);
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
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
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
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotalDebit).Text = "0";
		((Control)(object)txtTotalCredit).Text = "0";
		((Control)(object)txtDiff).Text = "0";
		cboCurrency.SelectedIndex = ((((DisposableObjectCollectionBase)cboCurrency.Items).Count != 1) ? (-1) : 0);
		((Control)(object)txtExchangeRate).Text = ((((TextEditorControlBase)cboCurrency).Value != null && dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString()).Length != 0) ? dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString() : "");
		((Control)(object)txtCode).Text = (Adding ? BranchDebitTransfers.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), IsFromServer: true) : "");
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلي" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة التحليلى" : "Sub Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Header).Caption = (GlobalVariables.IsArabic ? "مدين" : "Debit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Header).Caption = (GlobalVariables.IsArabic ? "دائن" : "Credit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = !UseSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = !UseCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Hidden = !useSubCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].ValueList = (IValueList)(object)vlSubCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranchs;
	}

	public override bool ValidateData()
	{
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: true))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العملة", "Please select Currency");
			((TextEditorControlBase)cboCurrency).Focus();
			return false;
		}
		if (((Control)(object)txtExchangeRate).Text == "" || decimal.Parse(((Control)(object)txtExchangeRate).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال سعر الصرف", "Please Enter Exchange Rate");
			((TextEditorControlBase)txtExchangeRate).Focus();
			return false;
		}
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الاذن", "Please Enter Voucher Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < 2)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا القيـــد", "Please insert details for this JV");
			return false;
		}
		if (decimal.Parse(((Control)(object)txtTotalDebit).Text) != decimal.Parse(((Control)(object)txtTotalCredit).Text))
		{
			GlobalVariables.InformationMB.Show("القيد غير متوازن. \r\n برجاء مراجعة تفاصيل القيد", "The J.V. is Not Balanced. Please Recheck the J.V. Details");
			return false;
		}
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString() + ",";
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الحساب أو حذف السطر  ", "Please Enter Account Name or Delete Row ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الفرع", "Please Select Branch");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"];
				return false;
			}
			if (UseSubAccounts && GlobalFunctions.GetOption("EnforceSubAccountsUse") && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار حساب تحليلي", "Please choose Sub-Account");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (UseCostCenters && GlobalFunctions.GetOption("EnforceCostCentersUse") && (dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "3" || dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "4") && ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار مركز تكلفة", "Please choose Cost-Center");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		if (FiscalYear.ChkForClosingFsicalPeriodByBranchs(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), text, IsFromServer: true))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			((Control)(object)dtpDate).Focus();
			return false;
		}
		if (Main.CheckForValueByFiscalYearID("A_BranchDebitTransfers", "BranchDebitTransferNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BranchDebitTransferNo"].ToString(), DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "", IsFromServer: true) > 0)
		{
			string code = BranchDebitTransfers.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الاذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='BranchDebitTransferAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب تحويلات المديونية بين الفروع من حسابات النظام  ", "Please Select Branch Debit Transfer Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = BranchDebitTransfers.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtTotalDebit).Text, ((Control)(object)txtTotalCredit).Text, ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["BranchDebitTransferDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchDebitTransferID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["JVID"].Value = DBNull.Value;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			BranchDebitTransfersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			BranchDebitTransfersDetails.GenerateJvs(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = BranchDebitTransfers.Insert_Update(drMaster["BranchDebitTransferID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtTotalDebit).Text, ((Control)(object)txtTotalCredit).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["BranchDebitTransferID"].Value = num;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BranchDebitTransferDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			BranchDebitTransfersDetails.DeleteForUpdate(drMaster["BranchDebitTransferID"].ToString(), text, IsFromServer: true);
			BranchDebitTransfersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			BranchDebitTransfersDetails.GenerateJvs(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["JVID"].Value != DBNull.Value)
				{
					JVDetails.DeleteVirtualByJVID(((UltraGridBase)ULGData).Rows[i].Cells["JVID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					JV.DeleteVirtual(((UltraGridBase)ULGData).Rows[i].Cells["JVID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
				}
			}
			BranchDebitTransfersDetails.DeleteVirtualByBranchDebitTransferID(drMaster["BranchDebitTransferID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BranchDebitTransfers.DeleteVirtual(drMaster["BranchDebitTransferID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_BranchDebitTransfers_A.rpt" : "Rep_A_BranchDebitTransfers_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchDebitTransferIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BranchDebitTransfersReport(-1, 0, IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BranchDebitTransferID"].ToString();
			FillData();
		}
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		cboCurrency.SelectedIndex = ((dtCurrency.Rows.Count != 0) ? (-1) : 0);
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		string key = ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key;
		if (key == "Debit" && ((UltraGridBase)ULGData).ActiveRow.Cells["Credit"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Credit"].Value.ToString()) != 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (key == "Credit" && !((UltraGridBase)ULGData).ActiveRow.Cells["Debit"].Value.Equals(DBNull.Value) && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Debit"].Value.ToString()) != 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Credit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Debit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			CalculateTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
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
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
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

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Debit"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Credit"].Value.ToString());
		}
		((Control)(object)txtTotalDebit).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtTotalCredit).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiff).Text = decimal.Parse((num - num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void dtpJVDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = BranchDebitTransfers.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), IsFromServer: true);
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

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Expected O, but got Unknown
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Expected O, but got Unknown
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Transactions.frmBranchDebitTransfer));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.txtTotalDebit = new UltraTextEditor();
		this.txtTotalCredit = new UltraTextEditor();
		this.lblTotalDebit = new UltraLabel();
		this.lblTotalCredit = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtDiff = new UltraTextEditor();
		this.lblDiff = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.lblExchangeRate = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiff).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
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
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpJVDate_ValueChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.txtTotalDebit, "txtTotalDebit");
		((System.Windows.Forms.Control)(object)this.txtTotalDebit).Name = "txtTotalDebit";
		((EditorButtonControlBase)this.txtTotalDebit).ReadOnly = true;
		resources.ApplyResources(this.txtTotalCredit, "txtTotalCredit");
		((System.Windows.Forms.Control)(object)this.txtTotalCredit).Name = "txtTotalCredit";
		((EditorButtonControlBase)this.txtTotalCredit).ReadOnly = true;
		resources.ApplyResources(this.lblTotalDebit, "lblTotalDebit");
		this.lblTotalDebit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalDebit).Name = "lblTotalDebit";
		((ControlBase)this.lblTotalDebit).WrapText = false;
		resources.ApplyResources(this.lblTotalCredit, "lblTotalCredit");
		this.lblTotalCredit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalCredit).Name = "lblTotalCredit";
		((ControlBase)this.lblTotalCredit).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.txtDiff, "txtDiff");
		((System.Windows.Forms.Control)(object)this.txtDiff).Name = "txtDiff";
		((EditorButtonControlBase)this.txtDiff).ReadOnly = true;
		resources.ApplyResources(this.lblDiff, "lblDiff");
		this.lblDiff.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiff).Name = "lblDiff";
		((ControlBase)this.lblDiff).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalCredit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalDebit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalCredit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalDebit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmBranchDebitTransfer";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalDebit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalCredit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalDebit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalCredit, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiff).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
