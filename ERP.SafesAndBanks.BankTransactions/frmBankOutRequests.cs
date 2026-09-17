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
using ERP.SafesAndBanks.Approved;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.BankTransactions;

public class frmBankOutRequests : frmHeaderDetails
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private DataTable dtBanks;

	private DataTable dtReports;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSubCostCenters = new ValueList();

	private bool UseSubAccounts;

	private bool UseCostCenters;

	private bool UseCurrency;

	private bool useSubCostCenters;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	private UltraLabel lblBank;

	private UltraComboEditor cboBank;

	private UltraTextEditor txtChargedPerson;

	private UltraLabel lblChargePerson;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	private UltraPanel pnlCheckType;

	private RadioButton rbPayable;

	private RadioButton rbIsCheck;

	private UltraLabel lblCheckDate;

	private UltraDateTimeEditor dtpCheckDate;

	private UltraTextEditor txtPayingBank;

	private UltraLabel lblPayingBank;

	public UltraButton btnBankSearch;

	public frmBankOutRequests()
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
		TableName = "SB_BankOutRequests";
		IDCol = "BankOutRequestID";
		NoCol = "BankOutRequestNo";
		DateCol = "BankOutRequestDate";
	}

	public frmBankOutRequests(int ID)
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
		dtBanks = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBank, dtBanks, "BankID", "BankName");
		FillCurrencyDropDown();
		dtDetails = BankOutRequestsDetails.SelectByBankOutRequestID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BankOutRequests.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BankOutRequestNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["BankOutRequestDate"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			rbPayable.Checked = drMaster["IsCheck"].Equals(false);
			rbIsCheck.Checked = drMaster["IsCheck"].Equals(true);
			dtpCheckDate.Value = drMaster["CheckDate"];
			((TextEditorControlBase)cboBank).Value = drMaster["BankID"];
			((Control)(object)txtChargedPerson).Text = drMaster["ChargedPerson"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["Total"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtPayingBank).Text = drMaster["PayingBank"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = BankOutRequestsDetails.SelectByBankOutRequestID(drMaster["BankOutRequestID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((Control)(object)btnBankSearch).Visible = !NavMode;
		rbIsCheck.Enabled = !NavMode;
		rbPayable.Enabled = !NavMode;
		((EditorButtonControlBase)dtpCheckDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBank).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = true;
		((EditorButtonControlBase)txtChargedPerson).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((EditorButtonControlBase)txtPayingBank).ReadOnly = NavMode;
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
		cboBank.SelectedIndex = ((((DisposableObjectCollectionBase)cboBank.Items).Count <= 0) ? (-1) : 0);
		dtpDate.DateTime = DateTime.Now;
		((Control)(object)txtCode).Text = (Adding ? BankOutRequests.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpCheckDate.DateTime = DateTime.Now;
		rbPayable.Checked = true;
		((TextEditorControlBase)txtPayingBank).Clear();
		((TextEditorControlBase)txtChargedPerson).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestDetailID"].DefaultCellValue = -1;
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
		if (rbIsCheck.Checked && dtpCheckDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار تاريخ الشيك", "Please Select Cheque Date");
			((Control)(object)dtpCheckDate).Focus();
			return false;
		}
		if (rbIsCheck.Checked && !FiscalYear.ChkForConfirmedFiscalYear(dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية لتاريخ الشيك غير معتمدة", "The Fiscal Year For Check Date is not confirmed..");
			return false;
		}
		if (rbIsCheck.Checked && FiscalYear.ChkForClosingFsicalPeriod(dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ الشيك تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed For Check Date\n\r exists in closed fisical period");
			return false;
		}
		if (Updating && DateTime.Parse(drMaster["BankOutRequestDate"].ToString()).Year != dtpDate.DateTime.Year)
		{
			GlobalVariables.InformationMB.Show("لايمكن تغيير الإذن الى سنة مالية أخرى", "Cannot Change Voucher Date To Another Fiscal Year");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboBank.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار البنك" : "Please Select the Bank ");
			((TextEditorControlBase)cboBank).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SB_BankOutRequests", "BankOutRequestNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BankOutRequestNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BankOutRequests.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((Control)(object)txtChargedPerson).Text == "")
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "من فضلك قم بادخال من السيد" : "Please Insert From Mrs.");
				((TextEditorControlBase)txtChargedPerson).Focus();
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال القيمة  ", "Please Enter Amount ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Value"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value != DBNull.Value && UseSubAccounts && GlobalFunctions.GetOption("EnforceSubAccountsUse") && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار حساب تحليلي", "Please choose Sub-Account");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value != DBNull.Value && UseCostCenters && GlobalFunctions.GetOption("EnforceCostCentersUse") && (dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "3" || dtAccounts.Select("AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString())[0]["AccountTypeID"].ToString() == "4") && ((UltraGridBase)ULGData).Rows[i].Cells["CostCenterID"].Value == DBNull.Value)
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
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BankOutRequests.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), rbIsCheck.Checked ? "1" : "0", rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtPayingBank).Text, "0", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutRequestDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutRequestID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			BankOutRequestsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BankOutRequests.Insert_Update(drMaster["BankOutRequestID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), rbIsCheck.Checked ? "1" : "0", rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtPayingBank).Text, bool.Parse(drMaster["IsRefused"].ToString()) ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutRequestID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BankOutRequestDetailID"].Value.ToString() + ",";
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			Main.DeleteForUpdate("SB_BankOutRequestsDetails", "BankOutRequestID", drMaster["BankOutRequestID"].ToString(), "BankOutRequestDetailID", text);
			BankOutRequestsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			BankOutRequests.DeleteVirtual(drMaster["BankOutRequestID"].ToString(), GlobalVariables.UserID);
			BankOutRequestsDetails.DeleteVirtualByBankOutRequestID(drMaster["BankOutRequestID"].ToString(), GlobalVariables.UserID);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SB_BankOutRequests_A.rpt" : "Rep_SB_BankOutRequests_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BankOutRequestIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BanKOutRequestReport(-1, -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BankOutRequestID"].ToString();
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
		if (cboBank.SelectedIndex != -1)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
		}
	}

	private void dtpJVDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = BankOutRequests.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
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
		object value = ((TextEditorControlBase)cboBank).Value;
		object value2 = ((TextEditorControlBase)cboCurrency).Value;
		object value3 = ((TextEditorControlBase)cboTransactionBranch).Value;
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
		dtBanks = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBank, dtBanks, "BankID", "BankName");
		FillCurrencyDropDown();
		((TextEditorControlBase)cboBank).Value = value;
		((TextEditorControlBase)cboCurrency).Value = value2;
		((TextEditorControlBase)cboTransactionBranch).Value = value3;
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboBank_ValueChanged(object sender, EventArgs e)
	{
		if (cboBank.SelectedIndex != -1)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
		}
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && drMaster["JVID"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnJV2_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && drMaster["JVID2"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID2"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnJV3_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && drMaster["JVID3"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID3"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnChangeStatus_Click(object sender, EventArgs e)
	{
		if (drMaster != null)
		{
			if (rbIsCheck.Checked && !bool.Parse(drMaster["UnderCollection"].ToString()))
			{
				frmNPAndNRApproval frmNPAndNRApproval2 = new frmNPAndNRApproval(int.Parse(drMaster["BankOutRequestID"].ToString()), IsBankIn: false);
				frmNPAndNRApproval2.Size = new Size(base.Width, base.Height);
				frmNPAndNRApproval2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNPAndNRApproval2.lblTitle).Text = (GlobalVariables.IsArabic ? "تـرحـيـــــل ا ق  و  ا د" : "NP And NR Approval");
				frmNPAndNRApproval2.ShowDialog();
				drMaster = BankOutRequests.Select(drMaster["BankOutRequestID"].ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0").Rows[0];
				DisplayData();
			}
			else if (rbIsCheck.Checked && !bool.Parse(drMaster["Collected"].ToString()) && !bool.Parse(drMaster["Returned"].ToString()))
			{
				frmNotesUnderCollection frmNotesUnderCollection2 = new frmNotesUnderCollection(int.Parse(drMaster["BankOutRequestID"].ToString()), IsBankIn: false);
				frmNotesUnderCollection2.Size = new Size(base.Width, base.Height);
				frmNotesUnderCollection2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNotesUnderCollection2.lblTitle).Text = (GlobalVariables.IsArabic ? "اوراق تحـت التحصيــل" : "Notes Under Collection");
				frmNotesUnderCollection2.ShowDialog();
				drMaster = BankOutRequests.Select(drMaster["BankOutRequestID"].ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0").Rows[0];
				DisplayData();
			}
		}
	}

	private void rbIsCheck_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void btnBankSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.BanksSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboBank).Value = num;
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
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
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Expected O, but got Unknown
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.BankTransactions.frmBankOutRequests));
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
		this.pnlCheckType = new UltraPanel();
		this.rbPayable = new System.Windows.Forms.RadioButton();
		this.rbIsCheck = new System.Windows.Forms.RadioButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblBank = new UltraLabel();
		this.cboBank = new UltraComboEditor();
		this.txtChargedPerson = new UltraTextEditor();
		this.lblChargePerson = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.lblCheckDate = new UltraLabel();
		this.dtpCheckDate = new UltraDateTimeEditor();
		this.txtPayingBank = new UltraTextEditor();
		this.lblPayingBank = new UltraLabel();
		this.btnBankSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPayingBank).BeginInit();
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
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance9");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbPayable);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsCheck);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbPayable, "rbPayable");
		this.rbPayable.Name = "rbPayable";
		this.rbPayable.TabStop = true;
		this.rbPayable.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsCheck, "rbIsCheck");
		this.rbIsCheck.Name = "rbIsCheck";
		this.rbIsCheck.TabStop = true;
		this.rbIsCheck.UseVisualStyleBackColor = false;
		this.rbIsCheck.CheckedChanged += new System.EventHandler(rbIsCheck_CheckedChanged);
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
		resources.ApplyResources(this.lblBank, "lblBank");
		this.lblBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBank).Name = "lblBank";
		((ControlBase)this.lblBank).WrapText = false;
		resources.ApplyResources(this.cboBank, "cboBank");
		((TextEditorControlBase)this.cboBank).AlwaysInEditMode = true;
		this.cboBank.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBank).Name = "cboBank";
		((TextEditorControlBase)this.cboBank).ValueChanged += new System.EventHandler(cboBank_ValueChanged);
		resources.ApplyResources(this.txtChargedPerson, "txtChargedPerson");
		((System.Windows.Forms.Control)(object)this.txtChargedPerson).Name = "txtChargedPerson";
		resources.ApplyResources(this.lblChargePerson, "lblChargePerson");
		this.lblChargePerson.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblChargePerson).Name = "lblChargePerson";
		((ControlBase)this.lblChargePerson).WrapText = false;
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
		resources.ApplyResources(this.lblCheckDate, "lblCheckDate");
		this.lblCheckDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckDate).Name = "lblCheckDate";
		((ControlBase)this.lblCheckDate).WrapText = false;
		resources.ApplyResources(this.dtpCheckDate, "dtpCheckDate");
		((UltraWinEditorMaskedControlBase)this.dtpCheckDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCheckDate).Name = "dtpCheckDate";
		resources.ApplyResources(this.txtPayingBank, "txtPayingBank");
		((System.Windows.Forms.Control)(object)this.txtPayingBank).Name = "txtPayingBank";
		resources.ApplyResources(this.lblPayingBank, "lblPayingBank");
		this.lblPayingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPayingBank).Name = "lblPayingBank";
		((ControlBase)this.lblPayingBank).WrapText = false;
		resources.ApplyResources(this.btnBankSearch, "btnBankSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnBankSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnBankSearch).Name = "btnBankSearch";
		((System.Windows.Forms.Control)(object)this.btnBankSearch).Click += new System.EventHandler(btnBankSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBankSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPayingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPayingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtChargedPerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChargePerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmBankOutRequests";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChargePerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtChargedPerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPayingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPayingBank, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBankSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPayingBank).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
