using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
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

public class frmBankOut : frmHeaderDetails
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private DataTable dtBanks;

	private DataTable dtsafes;

	private DataTable dtTaxs;

	private DataTable dtReports;

	private DataTable dtJvDetails;

	private DataTable dtRequests;

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

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangrRate;

	private UltraTextEditor txtChargedPerson;

	private UltraLabel lblChargePerson;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnJV;

	private UltraPanel pnlCheckType;

	private RadioButton rbPayable;

	private RadioButton rbIsCheck;

	private UltraTextEditor txtCheckNo;

	private UltraLabel lblCheckNo;

	private UltraLabel lblCheckDate;

	private UltraDateTimeEditor dtpCheckDate;

	private UltraTextEditor txtPayingBank;

	private UltraLabel lblPayingBank;

	public UltraButton btnJV2;

	public UltraButton btnJV3;

	private UltraLabel lblTotalAfterTax;

	private UltraTextEditor txtTotalAfterTax;

	private UltraTextEditor txtTaxValue;

	private UltraLabel lblTaxValue;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	private UltraButton btnChangeStatus;

	private UltraLabel lblCheckStatus;

	private UltraComboEditor cboBinderSafe;

	private UltraLabel lblCheckBinder;

	public UltraButton btnBankSearch;

	private UltraCheckEditor chkRequest;

	protected internal UltraCheckEditor chkAll;

	protected internal CheckedListBox clbRequestNo;

	public frmBankOut()
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
		TableName = "SB_BankOut";
		IDCol = "BankOutID";
		NoCol = "BankOutNo";
		DateCol = "BankOutDate";
		((Control)(object)btnJV3).Text = (((Control)(object)btnJV2).Text = (((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد " : "JV")));
	}

	public frmBankOut(int ID)
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
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBinderSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtDetails = BankOutDetails.SelectByBankOutID("0", GlobalVariables.IsArabic ? "1" : "0");
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
			DataTable dataTable = BankOut.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			((TextEditorControlBase)txtTaxValue).ValueChanged -= txtTaxValue_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BankOutNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["BankOutDate"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			rbPayable.Checked = drMaster["IsCheck"].Equals(false);
			rbIsCheck.Checked = drMaster["IsCheck"].Equals(true);
			((UltraToggleEditorBase)chkRequest).Checked = bool.Parse(drMaster["IsRequest"].ToString());
			((Control)(object)txtCheckNo).Text = drMaster["CheckNo"].ToString();
			dtpCheckDate.Value = drMaster["CheckDate"];
			((TextEditorControlBase)cboBank).Value = drMaster["BankID"];
			((Control)(object)txtChargedPerson).Text = drMaster["ChargedPerson"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["Total"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboTax).Value = drMaster["TaxID"].ToString();
			((Control)(object)txtTaxValue).Text = decimal.Parse(drMaster["TaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalAfterTax).Text = decimal.Parse(drMaster["TotalAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtPayingBank).Text = drMaster["PayingBank"].ToString();
			((TextEditorControlBase)cboBinderSafe).Value = drMaster["BinderSafeID"];
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo"].ToString() + ")قيد  ") : ("JV ( " + drMaster["JVNo"].ToString() + " )"));
			((Control)(object)btnJV2).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo2"].ToString() + ")قيد  ") : ("JV ( " + drMaster["JVNo2"].ToString() + " )"));
			((Control)(object)btnJV3).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo3"].ToString() + ")قيد  ") : ("JV ( " + drMaster["JVNo3"].ToString() + " )"));
			((Control)(object)btnJV).Visible = ((drMaster["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			((Control)(object)btnJV2).Visible = ((drMaster["JVNo2"] != DBNull.Value) ? true : false) && CanViewJV;
			((Control)(object)btnJV3).Visible = ((drMaster["JVNo3"] != DBNull.Value) ? true : false) && CanViewJV;
			((Control)(object)lblCheckStatus).Text = drMaster["CheckStatus"].ToString();
			((Control)(object)lblCheckStatus).Visible = ((drMaster["CheckStatus"] != DBNull.Value) ? true : false);
			((Control)(object)btnChangeStatus).Visible = ((drMaster["CheckStatus"] != DBNull.Value) ? true : false) && !bool.Parse(drMaster["Collected"].ToString()) && !bool.Parse(drMaster["Returned"].ToString());
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = BankOutDetails.SelectByBankOutID(drMaster["BankOutID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)txtTaxValue).ValueChanged += txtTaxValue_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
		}
		else
		{
			ClearControls();
			UltraLabel obj = lblCheckStatus;
			UltraButton obj2 = btnChangeStatus;
			UltraButton obj3 = btnJV;
			UltraButton obj4 = btnJV2;
			bool flag = (((Control)(object)btnJV3).Visible = false);
			bool flag3 = (((Control)(object)obj4).Visible = flag);
			bool flag5 = (((Control)(object)obj3).Visible = flag3);
			bool visible = (((Control)(object)obj2).Visible = flag5);
			((Control)(object)obj).Visible = visible;
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
		((EditorButtonControlBase)txtCheckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCheckDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBank).ReadOnly = NavMode;
		((Control)(object)chkRequest).Enabled = Adding;
		clbRequestNo.Enabled = Adding;
		((Control)(object)chkAll).Enabled = Adding;
		((EditorButtonControlBase)cboCurrency).ReadOnly = true;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtChargedPerson).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBinderSafe).ReadOnly = NavMode;
		((Control)(object)cboBinderSafe).Visible = rbIsCheck.Checked;
		((Control)(object)lblCheckBinder).Visible = rbIsCheck.Checked;
		((EditorButtonControlBase)txtTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotalAfterTax).ReadOnly = true;
		((EditorButtonControlBase)txtPayingBank).ReadOnly = NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnJV2).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnJV3).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnChangeStatus).Visible = NavMode && drMaster != null && rbIsCheck.Checked && !bool.Parse(drMaster["Collected"].ToString()) && !bool.Parse(drMaster["Returned"].ToString());
		((Control)(object)lblCheckStatus).Visible = NavMode && drMaster != null && rbIsCheck.Checked;
		if (Updating && UseSubAccounts)
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
		if (((UltraToggleEditorBase)chkRequest).Checked)
		{
			((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
			clbRequestNo.SelectedValueChanged -= clbRequestNo_SelectedValueChanged;
			dtRequests = Main.ExecuteQuery_DataTable(" Select BankOutRequestID,BankOutRequestNo from  SB_BankOutRequests Where BankOutID =" + drMaster["BankOutID"].ToString());
			Main.Fillclb(clbRequestNo, dtRequests, "BankOutRequestID", "BankOutRequestNo");
			for (int j = 0; j < clbRequestNo.Items.Count; j++)
			{
				clbRequestNo.SetItemChecked(j, value: true);
			}
			((UltraToggleEditorBase)chkAll).Checked = true;
			((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
			clbRequestNo.SelectedValueChanged += clbRequestNo_SelectedValueChanged;
		}
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		clbRequestNo.SelectedValueChanged -= clbRequestNo_SelectedValueChanged;
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		for (int i = 0; i < clbRequestNo.Items.Count; i++)
		{
			clbRequestNo.SetItemChecked(i, ((UltraToggleEditorBase)chkAll).Checked);
		}
		clbRequestNo.SelectedValueChanged += clbRequestNo_SelectedValueChanged;
		string requestsIDs = GetRequestsIDs();
		if (requestsIDs != ",")
		{
			dtDetails = BankOutDetails.FillByBankOutRequestIDs(requestsIDs, GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateTotals();
		}
	}

	private void clbRequestNo_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll).Checked = clbRequestNo.CheckedItems.Count == clbRequestNo.Items.Count && clbRequestNo.Items.Count > 0;
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		string requestsIDs = GetRequestsIDs();
		if (requestsIDs != ",")
		{
			dtDetails = BankOutDetails.FillByBankOutRequestIDs(requestsIDs, GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateTotals();
		}
	}

	public string GetRequestsIDs()
	{
		string text = ",";
		for (int i = 0; i < clbRequestNo.Items.Count; i++)
		{
			if (clbRequestNo.GetItemChecked(i))
			{
				text = text + dtRequests.Rows[i]["BankOutRequestID"].ToString() + ",";
			}
		}
		return text;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		cboBank.SelectedIndex = ((((DisposableObjectCollectionBase)cboBank.Items).Count <= 0) ? (-1) : 0);
		dtpDate.DateTime = DateTime.Now;
		((Control)(object)txtCode).Text = (Adding ? BankOut.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpCheckDate.DateTime = DateTime.Now;
		rbPayable.Checked = true;
		((TextEditorControlBase)txtCheckNo).Clear();
		((TextEditorControlBase)txtPayingBank).Clear();
		cboBinderSafe.SelectedIndex = -1;
		((TextEditorControlBase)txtChargedPerson).Clear();
		((UltraToggleEditorBase)chkRequest).Checked = false;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)txtTaxValue).ValueChanged -= txtTaxValue_ValueChanged;
		((Control)(object)txtTaxValue).Text = "0";
		((TextEditorControlBase)txtTaxValue).ValueChanged += txtTaxValue_ValueChanged;
		((Control)(object)txtTotalAfterTax).Text = "0";
		UltraButton obj = btnJV3;
		UltraButton obj2 = btnJV2;
		string text = (((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "JV"));
		string text3 = (((Control)(object)obj2).Text = text);
		((Control)(object)obj).Text = text3;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutDetailsID"].DefaultCellValue = -1;
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
		if (Updating && DateTime.Parse(drMaster["BankOutDate"].ToString()).Year != dtpDate.DateTime.Year)
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
		if (rbIsCheck.Checked && ((Control)(object)txtCheckNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الشيك" : "Please Enter  Check No ");
			((TextEditorControlBase)txtCheckNo).Focus();
			return false;
		}
		if (rbIsCheck.Checked && Main.CheckForValue("SB_BankOut", "CheckNo", ((Control)(object)txtCheckNo).Text, Adding ? "0" : drMaster["CheckNo"].ToString(), IsFromServer: false, " And BankID = " + ((TextEditorControlBase)cboBank).Value.ToString() + (Adding ? " And IsCheck = 1 " : (" And IsCheck = 1 And BankOutID <> " + drMaster["BankOutID"].ToString()))) > 0)
		{
			GlobalVariables.QuestionMB.Show("رقم الشيك متواجد من قبل على نفس البنك\n هل تريد الحفظ؟", "This Check No Exists For The Same Bank Before. \n Do You Want To Save?");
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCheckNo).Focus();
				return false;
			}
		}
		else
		{
			if ((((Control)(object)txtTaxValue).Text == "" || decimal.Parse(((Control)(object)txtTaxValue).Text) <= 0m) && cboTax.SelectedIndex > -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "من فضلك قم بادخال قيمة الضريبة" : "Please Insert Tax Value");
				((TextEditorControlBase)txtTaxValue).Focus();
				return false;
			}
			if (Main.CheckForValueByBranchIDAndFiscalYearID("SB_BankOut", "BankOutNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BankOutNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
			{
				string codeByBranchID = BankOut.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
				if (((Control)(object)txtChargedPerson).Text == "")
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "من فضلك قم بادخال من السيد" : "Please Insert From Mrs.");
					((TextEditorControlBase)txtChargedPerson).Focus();
					return false;
				}
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
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Expected O, but got Unknown
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BankOut.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((UltraToggleEditorBase)chkRequest).Checked ? "1" : "0", rbIsCheck.Checked ? "1" : "0", ((Control)(object)txtCheckNo).Text, rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtPayingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (((Control)(object)txtTaxValue).Text == "") ? "0" : ((Control)(object)txtTaxValue).Text, ((Control)(object)txtTotalAfterTax).Text, (dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPAccID"].ToString() == dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPUnderCollectionAccID"].ToString()) ? "1" : "0", "Null", "0", "0", "Null", "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			BankOutRequests.UpdateBankOutID(GetRequestsIDs(), num.ToString(), GlobalVariables.UserID);
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
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["IsDocumented"] = ((UltraGridBase)ULGData).Rows[i].Cells["IsDocumented"].Value;
				dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutDetailsID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			dataRow = dtJvDetails.NewRow();
			dataRow["JVDetailID"] = "-1";
			dataRow["AccountID"] = (rbIsCheck.Checked ? dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPAccID"].ToString() : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"]);
			dataRow["SubAccountID"] = (rbIsCheck.Checked ? DBNull.Value : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
			dataRow["Debit"] = "0";
			dataRow["Credit"] = (rbIsCheck.Checked ? ((Control)(object)txtTotal).Text : ((Control)(object)txtTotalAfterTax).Text);
			dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
			dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
			dataRow["LocalDebit"] = "0";
			dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtJvDetails.Rows.Add(dataRow);
			if (cboTax.SelectedIndex > -1 && decimal.Parse(((Control)(object)txtTaxValue).Text) > 0m && !rbIsCheck.Checked)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["AccountID"];
				dataRow["SubAccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["SubAccountID"];
				dataRow["Debit"] = "0";
				dataRow["Credit"] = decimal.Parse(((Control)(object)txtTaxValue).Text) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["CurrencyID"] = GlobalVariables.LocalCurrencyID.ToString();
				dataRow["ExchangeRate"] = "1";
				dataRow["LocalDebit"] = "0";
				dataRow["LocalCredit"] = dataRow["Credit"].ToString();
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
			}
			BankOutDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "4");
			int num2 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "4", num.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
			Main.ExecuteNonQuery(" Update SB_BankOut  set JvID= " + num2 + " Where BankOutID=" + num.ToString());
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
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Expected O, but got Unknown
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Expected O, but got Unknown
		//IL_0acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Expected O, but got Unknown
		//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BankOut.Insert_Update(drMaster["BankOutID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((UltraToggleEditorBase)chkRequest).Checked ? "1" : "0", rbIsCheck.Checked ? "1" : "0", ((Control)(object)txtCheckNo).Text, rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtPayingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (((Control)(object)txtTaxValue).Text == "") ? "0" : ((Control)(object)txtTaxValue).Text, ((Control)(object)txtTotalAfterTax).Text, (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked && dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPAccID"].ToString() != dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPUnderCollectionAccID"].ToString()) ? "0" : (drMaster["UnderCollection"].Equals(true) ? "1" : "0"), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((drMaster["UnderCollectionDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["UnderCollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate)), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (drMaster["Collected"].Equals(true) ? "1" : "0"), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (drMaster["Returned"].Equals(true) ? "1" : "0"), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((drMaster["CollectionDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["CollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate)), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((drMaster["CollectionExpense"] == DBNull.Value) ? "Null" : drMaster["CollectionExpense"].ToString()), (drMaster["JVID"] == DBNull.Value) ? "Null" : drMaster["JVID"].ToString(), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((drMaster["JVID2"] == DBNull.Value) ? "Null" : drMaster["JVID2"].ToString()), (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((drMaster["JVID3"] == DBNull.Value) ? "Null" : drMaster["JVID3"].ToString()), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["BankOutID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BankOutDetailsID"].Value.ToString() + ",";
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			Main.DeleteForUpdate("SB_BankOutDetails", "BankOutID", drMaster["BankOutID"].ToString(), "BankOutDetailsID", text);
			BankOutDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			if (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked)
			{
				if (drMaster["JVID2"] != DBNull.Value)
				{
					JV.DeleteVirtual(drMaster["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				}
				if (drMaster["JVID3"] != DBNull.Value)
				{
					JV.DeleteVirtual(drMaster["JVID3"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				}
				JVDetails.DeleteByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID);
			}
			else
			{
				JVDetails.DeleteByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID);
				if (drMaster["JVID2"] != DBNull.Value)
				{
					JVDetails.DeleteByJVID(drMaster["JVID2"].ToString(), GlobalVariables.UserID);
				}
				if (drMaster["JVID3"] != DBNull.Value)
				{
					JVDetails.DeleteByJVID(drMaster["JVID3"].ToString(), GlobalVariables.UserID);
				}
			}
			dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
			DataRow dataRow;
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value;
				dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value;
				dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value;
				dataRow["SubCostCenterID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubCostCenterID"].Value;
				dataRow["Debit"] = ((UltraGridBase)ULGData).Rows[j].Cells["Value"].Value;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["IsDocumented"] = ((UltraGridBase)ULGData).Rows[j].Cells["IsDocumented"].Value;
				dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[j].Cells["BankOutID"].Value = num;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			dataRow = dtJvDetails.NewRow();
			dataRow["JVDetailID"] = "-1";
			dataRow["AccountID"] = (rbIsCheck.Checked ? dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPAccID"] : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"]);
			dataRow["SubAccountID"] = (rbIsCheck.Checked ? DBNull.Value : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
			dataRow["Debit"] = "0";
			dataRow["Credit"] = (rbIsCheck.Checked ? ((Control)(object)txtTotal).Text : ((Control)(object)txtTotalAfterTax).Text);
			dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
			dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
			dataRow["LocalDebit"] = "0";
			dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtJvDetails.Rows.Add(dataRow);
			if (cboTax.SelectedIndex > -1 && decimal.Parse(((Control)(object)txtTaxValue).Text) > 0m && !rbIsCheck.Checked)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["AccountID"];
				dataRow["SubAccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["SubAccountID"];
				dataRow["Debit"] = "0";
				dataRow["Credit"] = decimal.Parse(((Control)(object)txtTaxValue).Text) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["CurrencyID"] = GlobalVariables.LocalCurrencyID.ToString();
				dataRow["ExchangeRate"] = 1;
				dataRow["LocalDebit"] = "0";
				dataRow["LocalCredit"] = dataRow["Credit"].ToString();
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
			}
			JV.GenerateJV_Update(JVNo: (drMaster["JVDate"] == DBNull.Value || (DateTime.Parse(drMaster["JVDate"].ToString()).Month == dtpDate.DateTime.Month && DateTime.Parse(drMaster["JVDate"].ToString()).Year == dtpDate.DateTime.Year)) ? drMaster["JVNo"].ToString() : JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "4"), JVID: drMaster["JVID"].ToString(), JVDate: dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), TransTypeID: "4", VoucherID: num.ToString(), ReceiptNo: ((Control)(object)txtCode).Text, HNotes: " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, IsOpenningJv: "0", Approved: "1", dtJvDetails: dtJvDetails, Deleted: "0", BranchID: GlobalVariables.CurrentBranchID, IsInternalJV: "1", UserID: GlobalVariables.UserID);
			dtJvDetails.Clear();
			dataRow.Delete();
			if (drMaster["JVID2"] != DBNull.Value && bool.Parse(drMaster["IsCheck"].ToString()) == rbIsCheck.Checked)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPAccID"].ToString();
				dataRow["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPUnderCollectionAccID"].ToString();
				dataRow["Debit"] = "0";
				dataRow["Credit"] = ((Control)(object)txtTotal).Text;
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = "0";
				dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				JV.GenerateJV_Update(drMaster["JVID2"].ToString(), drMaster["JVNo2"].ToString(), DateTime.Parse(drMaster["UnderCollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "18", num.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				dtJvDetails.Clear();
				dataRow.Delete();
			}
			if (drMaster["JVID3"] != DBNull.Value && bool.Parse(drMaster["IsCheck"].ToString()) == rbIsCheck.Checked)
			{
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NPUnderCollectionAccID"];
				dataRow["Debit"] = decimal.Parse(((Control)(object)txtTotal).Text);
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				if (drMaster["CollectionExpense"] != DBNull.Value && decimal.Parse(drMaster["CollectionExpense"].ToString()) > 0m)
				{
					dataRow = dtJvDetails.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CollectionExpensesAccID"];
					dataRow["Debit"] = ((drMaster["CollectionExpense"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["CollectionExpense"].ToString()));
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow);
				}
				if (drMaster["Returned"].Equals(true))
				{
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
					{
						dataRow = dtJvDetails.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[k].Cells["AccountID"].Value;
						dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[k].Cells["SubAccountID"].Value;
						dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[k].Cells["CostCenterID"].Value;
						dataRow["SubCostCenterID"] = ((UltraGridBase)ULGData).Rows[k].Cells["SubCostCenterID"].Value;
						dataRow["Debit"] = "0";
						dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[k].Cells["Value"].Value;
						dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow["LocalDebit"] = "0";
						dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow["IsDocumented"] = ((UltraGridBase)ULGData).Rows[k].Cells["IsDocumented"].Value;
						dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value;
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow);
					}
				}
				else
				{
					dataRow = dtJvDetails.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"];
					dataRow["SubAccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"];
					dataRow["Debit"] = "0";
					dataRow["Credit"] = decimal.Parse(((Control)(object)txtTotalAfterTax).Text) + ((drMaster["CollectionExpense"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["CollectionExpense"].ToString()));
					dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow);
				}
				if (cboTax.SelectedIndex > -1 && decimal.Parse(((Control)(object)txtTaxValue).Text) > 0m)
				{
					dataRow = dtJvDetails.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["AccountID"];
					dataRow["SubAccountID"] = dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["SubAccountID"];
					dataRow["Debit"] = "0";
					dataRow["Credit"] = decimal.Parse(((Control)(object)txtTaxValue).Text) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow["CurrencyID"] = GlobalVariables.LocalCurrencyID.ToString();
					dataRow["ExchangeRate"] = "1";
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = dataRow["Credit"].ToString();
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow);
				}
				JV.GenerateJV_Update(drMaster["JVID3"].ToString(), drMaster["JVNo3"].ToString(), DateTime.Parse(drMaster["CollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "19", num.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				dtJvDetails.Clear();
				dataRow.Delete();
			}
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
			if (drMaster["JVID2"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["JVID3"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["JVID3"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			BankOut.DeleteVirtual(drMaster["BankOutID"].ToString(), GlobalVariables.UserID);
			BankOutDetails.DeleteVirtualByBankOutID(drMaster["BankOutID"].ToString(), GlobalVariables.UserID);
			BankOutRequests.UpdateBankOutID(GetRequestsIDs(), "Null", GlobalVariables.UserID);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SB_BankOut_A.rpt" : "Rep_SB_BankOut_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BankOutIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BanKOutReport(-1, -1, -1, -1, -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BankOutID"].ToString();
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
		if (drMaster != null && int.Parse(drMaster["PaymentCount"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة سداد  ", "Cannot Delete This Transaction Because Payment Invoice Was Made ");
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

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			if (!CanUpdate)
			{
				GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
				return;
			}
			if (!CanModifyOtherBranch)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
				return;
			}
			if (ClosedPeriod)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
				return;
			}
			if (drMaster != null && int.Parse(drMaster["PaymentCount"].ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة سداد  ", "Cannot Update This Transaction Because Payment Invoice Was Made ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
			((Control)(object)txtCode).Text = BankOut.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		if (cboTax.SelectedIndex > -1)
		{
			((Control)(object)txtTaxValue).Text = decimal.Parse((num * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		((Control)(object)txtTotalAfterTax).Text = decimal.Parse((num - decimal.Parse(((Control)(object)txtTaxValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboBank).Value;
		object value2 = ((TextEditorControlBase)cboBinderSafe).Value;
		object value3 = ((TextEditorControlBase)cboCurrency).Value;
		object value4 = ((TextEditorControlBase)cboTax).Value;
		object value5 = ((TextEditorControlBase)cboTransactionBranch).Value;
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
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBinderSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		((TextEditorControlBase)cboBank).Value = value;
		((TextEditorControlBase)cboBinderSafe).Value = value2;
		((TextEditorControlBase)cboCurrency).Value = value3;
		((TextEditorControlBase)cboTax).Value = value4;
		((TextEditorControlBase)cboTransactionBranch).Value = value5;
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtTaxValue_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtTaxValue).Text = decimal.Parse((((Control)(object)txtTaxValue).Text == "") ? "0" : ((Control)(object)txtTaxValue).Text).ToString(GlobalVariables.txtDecimalFormate);
		if (decimal.Parse(((Control)(object)txtTaxValue).Text) > decimal.Parse(((Control)(object)txtTotal).Text))
		{
			GlobalVariables.InformationMB.Show("لابد أن قيمة الضريبة لا تتعدى إجمالى الإذن", "Tax Amount Does Not Exceed Voucher Total Amount");
			((Control)(object)txtTaxValue).Text = decimal.Parse((decimal.Parse(((Control)(object)txtTotal).Text) - 1m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString());
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtTotalAfterTax).Text = decimal.Parse((num - decimal.Parse(((Control)(object)txtTaxValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtTaxValue).ValueChanged -= txtTaxValue_ValueChanged;
		if (cboTax.SelectedIndex == -1)
		{
			((Control)(object)txtTaxValue).Text = "0";
		}
		CalculateTotals();
		((TextEditorControlBase)txtTaxValue).ValueChanged += txtTaxValue_ValueChanged;
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
				frmNPAndNRApproval frmNPAndNRApproval2 = new frmNPAndNRApproval(int.Parse(drMaster["BankOutID"].ToString()), IsBankIn: false);
				frmNPAndNRApproval2.Size = new Size(base.Width, base.Height);
				frmNPAndNRApproval2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNPAndNRApproval2.lblTitle).Text = (GlobalVariables.IsArabic ? "تـرحـيـــــل ا ق  و  ا د" : "NP And NR Approval");
				frmNPAndNRApproval2.ShowDialog();
				drMaster = BankOut.Select(drMaster["BankOutID"].ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0").Rows[0];
				DisplayData();
			}
			else if (rbIsCheck.Checked && !bool.Parse(drMaster["Collected"].ToString()) && !bool.Parse(drMaster["Returned"].ToString()))
			{
				frmNotesUnderCollection frmNotesUnderCollection2 = new frmNotesUnderCollection(int.Parse(drMaster["BankOutID"].ToString()), IsBankIn: false);
				frmNotesUnderCollection2.Size = new Size(base.Width, base.Height);
				frmNotesUnderCollection2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNotesUnderCollection2.lblTitle).Text = (GlobalVariables.IsArabic ? "اوراق تحـت التحصيــل" : "Notes Under Collection");
				frmNotesUnderCollection2.ShowDialog();
				drMaster = BankOut.Select(drMaster["BankOutID"].ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0").Rows[0];
				DisplayData();
			}
		}
	}

	private void rbIsCheck_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboBinderSafe).Visible = rbIsCheck.Checked;
		((Control)(object)lblCheckBinder).Visible = rbIsCheck.Checked;
		if (((UltraToggleEditorBase)chkRequest).Checked && cboCurrency.SelectedIndex != -1 && cboBank.SelectedIndex != -1 && Adding)
		{
			dtRequests = Main.ExecuteQuery_DataTable(" Select BankOutRequestID,BankOutRequestNo from  SB_BankOutRequests Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID + " And IsCheck = " + (rbIsCheck.Checked ? (" 1 and cast(Checkdate as DATE) ='" + dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'") : "0") + " And CurrencyID=" + ((TextEditorControlBase)cboCurrency).Value.ToString() + " And BankID =" + ((TextEditorControlBase)cboBank).Value.ToString() + " And Approved=1 And BankOutID is null ");
			Main.Fillclb(clbRequestNo, dtRequests, "BankOutRequestID", "BankOutRequestNo");
		}
	}

	private void btnBankSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.BanksSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboBank).Value = num;
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (((UltraToggleEditorBase)chkRequest).Checked && cboCurrency.SelectedIndex != -1 && cboBank.SelectedIndex != -1 && Adding)
			{
				dtRequests = Main.ExecuteQuery_DataTable(" Select BankOutRequestID,BankOutRequestNo from  SB_BankOutRequests Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID + " And IsCheck = " + (rbIsCheck.Checked ? (" 1 and cast(Checkdate as DATE) ='" + dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'") : "0") + " And CurrencyID=" + ((TextEditorControlBase)cboCurrency).Value.ToString() + " And BankID =" + ((TextEditorControlBase)cboBank).Value.ToString() + " And Approved=1 And BankOutID is null ");
				Main.Fillclb(clbRequestNo, dtRequests, "BankOutRequestID", "BankOutRequestNo");
			}
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void chkRequest_CheckedChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			clbRequestNo.DataSource = null;
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			if (((UltraToggleEditorBase)chkRequest).Checked && cboCurrency.SelectedIndex != -1 && cboBank.SelectedIndex != -1 && Adding)
			{
				dtRequests = Main.ExecuteQuery_DataTable(" Select BankOutRequestID,BankOutRequestNo from  SB_BankOutRequests Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID + " And IsCheck = " + (rbIsCheck.Checked ? (" 1 and cast(Checkdate as DATE) ='" + dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'") : "0") + " And CurrencyID=" + ((TextEditorControlBase)cboCurrency).Value.ToString() + " And BankID =" + ((TextEditorControlBase)cboBank).Value.ToString() + " And Approved=1 And BankOutID is null ");
				Main.Fillclb(clbRequestNo, dtRequests, "BankOutRequestID", "BankOutRequestNo");
			}
			InitGrid();
			CalculateTotals();
		}
		UltraCheckEditor obj = chkAll;
		bool visible = (clbRequestNo.Visible = ((UltraToggleEditorBase)chkRequest).Checked);
		((Control)(object)obj).Visible = visible;
	}

	private void cboBank_ValueChanged(object sender, EventArgs e)
	{
		if (cboBank.SelectedIndex != -1)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Expected O, but got Unknown
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Expected O, but got Unknown
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.BankTransactions.frmBankOut));
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
		this.pnlCheckType = new UltraPanel();
		this.rbPayable = new System.Windows.Forms.RadioButton();
		this.rbIsCheck = new System.Windows.Forms.RadioButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblBank = new UltraLabel();
		this.cboBank = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangrRate = new UltraLabel();
		this.txtChargedPerson = new UltraTextEditor();
		this.lblChargePerson = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnJV = new UltraButton();
		this.txtCheckNo = new UltraTextEditor();
		this.lblCheckNo = new UltraLabel();
		this.lblCheckDate = new UltraLabel();
		this.dtpCheckDate = new UltraDateTimeEditor();
		this.txtPayingBank = new UltraTextEditor();
		this.lblPayingBank = new UltraLabel();
		this.btnJV2 = new UltraButton();
		this.btnJV3 = new UltraButton();
		this.lblTotalAfterTax = new UltraLabel();
		this.txtTotalAfterTax = new UltraTextEditor();
		this.txtTaxValue = new UltraTextEditor();
		this.lblTaxValue = new UltraLabel();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.btnChangeStatus = new UltraButton();
		this.lblCheckStatus = new UltraLabel();
		this.cboBinderSafe = new UltraComboEditor();
		this.lblCheckBinder = new UltraLabel();
		this.btnBankSearch = new UltraButton();
		this.chkRequest = new UltraCheckEditor();
		this.chkAll = new UltraCheckEditor();
		this.clbRequestNo = new System.Windows.Forms.CheckedListBox();
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
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPayingBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalAfterTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBinderSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRequest).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
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
		resources.ApplyResources(val9, "appearance14");
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
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangrRate, "lblExchangrRate");
		this.lblExchangrRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangrRate).Name = "lblExchangrRate";
		((ControlBase)this.lblExchangrRate).WrapText = false;
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
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.txtCheckNo, "txtCheckNo");
		((System.Windows.Forms.Control)(object)this.txtCheckNo).Name = "txtCheckNo";
		resources.ApplyResources(this.lblCheckNo, "lblCheckNo");
		this.lblCheckNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckNo).Name = "lblCheckNo";
		((ControlBase)this.lblCheckNo).WrapText = false;
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
		resources.ApplyResources(this.btnJV2, "btnJV2");
		((System.Windows.Forms.Control)(object)this.btnJV2).Name = "btnJV2";
		((System.Windows.Forms.Control)(object)this.btnJV2).Click += new System.EventHandler(btnJV2_Click);
		resources.ApplyResources(this.btnJV3, "btnJV3");
		((System.Windows.Forms.Control)(object)this.btnJV3).Name = "btnJV3";
		((System.Windows.Forms.Control)(object)this.btnJV3).Click += new System.EventHandler(btnJV3_Click);
		resources.ApplyResources(this.lblTotalAfterTax, "lblTotalAfterTax");
		this.lblTotalAfterTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalAfterTax).Name = "lblTotalAfterTax";
		((ControlBase)this.lblTotalAfterTax).WrapText = false;
		resources.ApplyResources(this.txtTotalAfterTax, "txtTotalAfterTax");
		((System.Windows.Forms.Control)(object)this.txtTotalAfterTax).Name = "txtTotalAfterTax";
		resources.ApplyResources(this.txtTaxValue, "txtTaxValue");
		((System.Windows.Forms.Control)(object)this.txtTaxValue).Name = "txtTaxValue";
		((TextEditorControlBase)this.txtTaxValue).ValueChanged += new System.EventHandler(txtTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblTaxValue, "lblTaxValue");
		this.lblTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxValue).Name = "lblTaxValue";
		((ControlBase)this.lblTaxValue).WrapText = false;
		resources.ApplyResources(this.lblTax, "lblTax");
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.btnChangeStatus, "btnChangeStatus");
		((System.Windows.Forms.Control)(object)this.btnChangeStatus).Name = "btnChangeStatus";
		((System.Windows.Forms.Control)(object)this.btnChangeStatus).Click += new System.EventHandler(btnChangeStatus_Click);
		resources.ApplyResources(this.lblCheckStatus, "lblCheckStatus");
		resources.ApplyResources(val10, "appearance9");
		((ControlBase)this.lblCheckStatus).Appearance = (AppearanceBase)(object)val10;
		this.lblCheckStatus.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckStatus).Name = "lblCheckStatus";
		((ControlBase)this.lblCheckStatus).WrapText = false;
		resources.ApplyResources(this.cboBinderSafe, "cboBinderSafe");
		((TextEditorControlBase)this.cboBinderSafe).AlwaysInEditMode = true;
		this.cboBinderSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBinderSafe).Name = "cboBinderSafe";
		resources.ApplyResources(this.lblCheckBinder, "lblCheckBinder");
		resources.ApplyResources(val11, "appearance15");
		((ControlBase)this.lblCheckBinder).Appearance = (AppearanceBase)(object)val11;
		this.lblCheckBinder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckBinder).Name = "lblCheckBinder";
		((ControlBase)this.lblCheckBinder).WrapText = false;
		resources.ApplyResources(this.btnBankSearch, "btnBankSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance16");
		((ControlBase)this.btnBankSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnBankSearch).Name = "btnBankSearch";
		((System.Windows.Forms.Control)(object)this.btnBankSearch).Click += new System.EventHandler(btnBankSearch_Click);
		resources.ApplyResources(this.chkRequest, "chkRequest");
		((System.Windows.Forms.Control)(object)this.chkRequest).Name = "chkRequest";
		((System.Windows.Forms.Control)(object)this.chkRequest).TabStop = false;
		((UltraToggleEditorBase)this.chkRequest).CheckedChanged += new System.EventHandler(chkRequest_CheckedChanged);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance17");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.clbRequestNo, "clbRequestNo");
		this.clbRequestNo.CheckOnClick = true;
		this.clbRequestNo.FormattingEnabled = true;
		this.clbRequestNo.Name = "clbRequestNo";
		this.clbRequestNo.SelectedValueChanged += new System.EventHandler(clbRequestNo_SelectedValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add(this.clbRequestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBankSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBinderSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckBinder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnChangeStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalAfterTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalAfterTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPayingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPayingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtChargedPerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChargePerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangrRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmBankOut";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangrRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChargePerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtChargedPerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPayingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPayingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV3, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalAfterTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalAfterTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnChangeStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckBinder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBinderSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBankSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRequest, 0);
		base.Controls.SetChildIndex(this.clbRequestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPayingBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalAfterTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBinderSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRequest).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
