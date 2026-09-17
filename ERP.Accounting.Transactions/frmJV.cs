using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Transactions;

public class frmJV : frmHeaderDetails
{
	private DataTable dtJVDefaults;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private DataTable dtReports;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSubCostCenters = new ValueList();

	private ValueList vlCurency = new ValueList();

	private bool IsRepeatedJV = false;

	private bool UseSubAccounts;

	private bool UseCostCenters;

	private bool UseCurrency;

	private bool useSubCostCenters;

	private DataRow drRepeatedJV;

	private IContainer components = null;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblDate;

	private UltraLabel lblTransType;

	private UltraLabel lblTotalCredit;

	private UltraLabel lblTotalDebit;

	private UltraTextEditor txtTotalCredit;

	private UltraTextEditor txtTotalDebit;

	private UltraComboEditor cboTransType;

	private UltraLabel lblReceiptNo;

	private UltraTextEditor txtReceiptNo;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraCheckEditor chkOpeningJV;

	private UltraComboEditor cboCurrency;

	private UltraCheckEditor chkCurrency;

	public UltraButton btnView;

	private UltraTextEditor txtDiff;

	private UltraLabel lblDiff;

	public frmJV()
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
		TableName = "A_JV";
		IDCol = "JVID";
		NoCol = "JVNo";
		DateCol = "JVDate";
	}

	public frmJV(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmJV(DataRow drRepeatedJV)
		: this()
	{
		this.drRepeatedJV = drRepeatedJV;
		IsRepeatedJV = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
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
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtJVDefaults = JVDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransType, dtJVDefaults, "TransTypeID", GlobalVariables.IsArabic ? "TransTypeNameAr" : "TransTypeNameEn");
		dtDetails = JVDetails.SelectByJVID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		FillCurrencyDropDown();
	}

	public override void btnRefreshDataClick()
	{
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
		dtJVDefaults = JVDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransType, dtJVDefaults, "TransTypeID", GlobalVariables.IsArabic ? "TransTypeNameAr" : "TransTypeNameEn");
		FillCurrencyDropDown();
	}

	public override void FillData()
	{
		if (IsRepeatedJV)
		{
			btnAddClick();
			DisplayDataRepeatedJV();
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = JV.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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

	public void DisplayDataRepeatedJV()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		((TextEditorControlBase)cboTransType).ValueChanged -= cboTransType_ValueChanged;
		dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
		ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
		dtpDate.Value = DateTime.Now;
		((TextEditorControlBase)cboTransType).Value = drRepeatedJV["TransTypeID"];
		((Control)(object)txtTotalDebit).Text = decimal.Parse(drRepeatedJV["TotalDebit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtTotalCredit).Text = decimal.Parse(drRepeatedJV["TotalCredit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiff).Text = decimal.Parse((Convert.ToDecimal(drRepeatedJV["TotalDebit"]) - Convert.ToDecimal(drRepeatedJV["TotalCredit"])).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtNotes).Text = drRepeatedJV["Notes"].ToString();
		((UltraToggleEditorBase)chkOpeningJV).Checked = false;
		((Control)(object)chkOpeningJV).Visible = false;
		((Control)(object)txtCode).Text = (Adding ? JV.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, (cboTransType.SelectedIndex > -1) ? ((TextEditorControlBase)cboTransType).Value.ToString() : "") : "");
		dtDetails = RepeatedJVDetails.SelectByRepeatedJVID_ForJV(drRepeatedJV["RepeatedJVID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
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
		((TextEditorControlBase)cboTransType).ValueChanged += cboTransType_ValueChanged;
		dtpDate.ValueChanged += dtpJVDate_ValueChanged;
		ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
	}

	public override void DisplayData()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboTransType).ValueChanged -= cboTransType_ValueChanged;
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["JVNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["JVDate"];
			((TextEditorControlBase)cboTransType).Value = drMaster["TransTypeID"];
			((Control)(object)txtReceiptNo).Text = drMaster["ReceiptNo"].ToString();
			((Control)(object)txtTotalDebit).Text = decimal.Parse(drMaster["TotalDebit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiff).Text = decimal.Parse((Convert.ToDecimal(drMaster["TotalDebit"]) - Convert.ToDecimal(drMaster["TotalCredit"])).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalCredit).Text = decimal.Parse(drMaster["TotalCredit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkOpeningJV).Checked = drMaster["IsOpenningJv"].Equals(true);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = JVDetails.SelectByJVID(drMaster["JVID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || drMaster["IsInternalJV"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
			((TextEditorControlBase)cboTransType).ValueChanged += cboTransType_ValueChanged;
			dtpDate.ValueChanged += dtpJVDate_ValueChanged;
			ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		}
		else
		{
			ClearControls();
		}
		((Control)(object)btnView).Enabled = !Adding && !Updating && drMaster != null && !drMaster["VoucherID"].Equals(DBNull.Value);
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTransType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtReceiptNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkOpeningJV).Enabled = !NavMode;
		((Control)(object)chkCurrency).Visible = !NavMode && UseCurrency;
		((Control)(object)cboCurrency).Visible = !NavMode && UseCurrency;
		((Control)(object)btnView).Enabled = NavMode && drMaster != null && !drMaster["VoucherID"].Equals(DBNull.Value);
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
		((TextEditorControlBase)txtReceiptNo).Clear();
		((Control)(object)txtCode).Text = (Adding ? JV.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, (cboTransType.SelectedIndex > -1) ? ((TextEditorControlBase)cboTransType).Value.ToString() : "") : "");
		((UltraToggleEditorBase)chkCurrency).Checked = false;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).Clear();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JVDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalDebit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalCredit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].DefaultCellValue = GlobalVariables.LocalCurrencyID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.16);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalDebit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalCredit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلي" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة التحليلى" : "Sub Cost Center");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Header).Caption = (GlobalVariables.IsArabic ? "مدين" : "Debit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Header).Caption = (GlobalVariables.IsArabic ? "دائن" : "Credit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].Header).Caption = (GlobalVariables.IsArabic ? "سعر التحويل" : "Exchange Rate");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalDebit"].Header).Caption = (GlobalVariables.IsArabic ? "مدين ع م" : "Local Debit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalCredit"].Header).Caption = (GlobalVariables.IsArabic ? "دائن ع م" : "Local Credit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Header).Caption = (GlobalVariables.IsArabic ? "مستندى" : "Documented");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = !UseSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = !UseCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].Hidden = !useSubCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Hidden = !UseCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].Hidden = !UseCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalDebit"].Hidden = !UseCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalCredit"].Hidden = !UseCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubCostCenterID"].ValueList = (IValueList)(object)vlSubCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].ValueList = (IValueList)(object)vlCurency;
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
			((Control)(object)dtpDate).Focus();
			return false;
		}
		if (cboTransType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء أن تختار نوع الحركه", "Please select a Tans. type for this JV");
			((TextEditorControlBase)cboTransType).Focus();
			return false;
		}
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود القيد", "Please Enter JV Code");
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
		if (decimal.Parse(((Control)(object)txtTotalDebit).Text) == 0m)
		{
			GlobalVariables.InformationMB.Show("اجمالى المدين لابد ان يكون اكبر من الصفر", "Total Debit Must Be Greater Than Zero");
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
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("سعر التحويل لابد ان يكون اكبر من الصفر", "Exchange Rate Must Be Greater Than Zero");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UseSubAccounts && GlobalFunctions.GetOption("EnforceSubAccountsUse") && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].ValueList.ItemCount > 0 && (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value || dtSubAccounts.Select(" SubAccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار حساب تحليلي", "Please choose SubAccount");
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
		if (JV.Check_Code(Adding ? "0" : drMaster["JVID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCode).Text))
		{
			string code = JV.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, ((TextEditorControlBase)cboTransType).Value.ToString());
			GlobalVariables.QuestionMB.Show("رقم هذا القيد متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The JV Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = JV.GenerateJV_Insert(((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboTransType).Value.ToString(), "Null", ((Control)(object)txtReceiptNo).Text, ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkOpeningJV).Checked ? "1" : "0", "0", (DataTable)((UltraGridBase)ULGData).DataSource, "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (IsRepeatedJV)
		{
			Close();
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.GenerateJV_Update(drMaster["JVID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboTransType).Value.ToString(), "Null", ((Control)(object)txtReceiptNo).Text, ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkOpeningJV).Checked ? "1" : "0", "0", (DataTable)((UltraGridBase)ULGData).DataSource, "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
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
			JVDetails.DeleteVirtualByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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
		if (!(RowID != ""))
		{
			return;
		}
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else if (UseCurrency)
		{
			if (GlobalFunctions.GetOption("JVPrintGroupedByAccount"))
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_JV_A.rpt" : "Rep_A_JV_E.rpt"));
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_JVWithoutGrouping_A.rpt" : "Rep_A_JVWithoutGrouping_E.rpt"));
			}
		}
		else if (GlobalFunctions.GetOption("JVPrintGroupedByAccount"))
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_JVLocalCurrency_A.rpt" : "Rep_A_JVLocalCurrency_E.rpt"));
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_JVLocalCurrencyWithoutGrouping_A.rpt" : "Rep_A_JVLocalCurrencyWithoutGrouping_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@JVIDs", "," + RowID + ",");
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.JVReport("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["JVID"].ToString();
			FillData();
		}
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		if (UseCurrency)
		{
			vlCurency.ValueListItems.Clear();
			for (int i = 0; i < dtCurrency.Rows.Count; i++)
			{
				vlCurency.ValueListItems.Add(dtCurrency.Rows[i]["CurrencyID"], dtCurrency.Rows[i]["CurrencyCode"].ToString());
			}
		}
		else
		{
			cboCurrency.SelectedIndex = 0;
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyID")
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ExchangeRate"].Value = dtCurrency.Rows[vlCurency.SelectedIndex]["ExchangeRate"];
		}
		else if (UseSubAccounts && ((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID")
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
		else if ((key == "LocalDebit" && (((UltraGridBase)ULGData).ActiveRow.Cells["Debit"].Value == DBNull.Value || ((UltraGridBase)ULGData).ActiveRow.Cells["ExchangeRate"].Value.ToString() == "0")) || (key == "LocalCredit" && (((UltraGridBase)ULGData).ActiveRow.Cells["Credit"].Value == DBNull.Value || ((UltraGridBase)ULGData).ActiveRow.Cells["ExchangeRate"].Value.ToString() == "0")))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Debit")
			{
				e.Cell.Row.Cells["localDebit"].Value = (e.Cell.Row.Cells["Debit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Debit"].Value.ToString()) * (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString())))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Credit")
			{
				e.Cell.Row.Cells["localCredit"].Value = (e.Cell.Row.Cells["Credit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Credit"].Value.ToString()) * (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString())))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ExchangeRate" || ((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyID")
			{
				e.Cell.Row.Cells["localDebit"].Value = (e.Cell.Row.Cells["Debit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Debit"].Value.ToString()) * (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString())))).ToString();
				e.Cell.Row.Cells["localCredit"].Value = (e.Cell.Row.Cells["Credit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Credit"].Value.ToString()) * (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString())))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalDebit")
			{
				e.Cell.Row.Cells["Debit"].Value = ((e.Cell.Row.Cells["localDebit"].Value.Equals(DBNull.Value) ? 0m : decimal.Parse(e.Cell.Row.Cells["localDebit"].Value.ToString())) / (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalCredit")
			{
				e.Cell.Row.Cells["Credit"].Value = ((e.Cell.Row.Cells["localCredit"].Value.Equals(DBNull.Value) ? 0m : decimal.Parse(e.Cell.Row.Cells["localCredit"].Value.ToString())) / (e.Cell.Row.Cells["ExchangeRate"].Value.Equals(DBNull.Value) ? 1m : decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()))).ToString();
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Credit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Debit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalCredit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalDebit" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 0;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "ExchangeRate" && e.Cell.Value == DBNull.Value)
			{
				e.Cell.Value = 1;
			}
			CalculateTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["LocalDebit"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["LocalCredit"].Value.ToString());
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
			((Control)(object)txtCode).Text = JV.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, (cboTransType.SelectedIndex > -1) ? ((TextEditorControlBase)cboTransType).Value.ToString() : "");
		}
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
		e.Row.Cells["CurrencyID"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["CurrencyID"].Value;
		e.Row.Cells["ExchangeRate"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["ExchangeRate"].Value;
		e.Row.Cells["Notes"].Value = ((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["Notes"].Value;
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

	private void cboTransType_ValueChanged(object sender, EventArgs e)
	{
		if (Adding && GlobalFunctions.GetOption("JV_SerialByTransType"))
		{
			((Control)(object)txtCode).Text = JV.GetCode(((DateTime)dtpDate.Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, (cboTransType.SelectedIndex > -1) ? ((TextEditorControlBase)cboTransType).Value.ToString() : "");
		}
	}

	private void chkCurrency_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboCurrency).ReadOnly = !((UltraToggleEditorBase)chkCurrency).Checked;
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex > -1)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["CurrencyID"].Value = ((TextEditorControlBase)cboCurrency).Value;
				((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value = dtCurrency.Rows[cboCurrency.SelectedIndex]["ExchangeRate"];
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].DefaultCellValue = ((TextEditorControlBase)cboCurrency).Value;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].DefaultCellValue = dtCurrency.Rows[cboCurrency.SelectedIndex]["ExchangeRate"];
		}
	}

	private void txtReceiptNo_DoubleClick(object sender, EventArgs e)
	{
	}

	private void btnView_Click(object sender, EventArgs e)
	{
		if (drMaster["TransTypeID"].ToString() == "1")
		{
			GlobalFunctions.OpenForm("ERP.SafesAndBanks.SafeTransactions.frmSafeIn", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "2")
		{
			GlobalFunctions.OpenForm("ERP.SafesAndBanks.SafeTransactions.frmSafeOut", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "3" || drMaster["TransTypeID"].ToString() == "16" || drMaster["TransTypeID"].ToString() == "17")
		{
			GlobalFunctions.OpenForm("ERP.SafesAndBanks.BankTransactions.frmBankIn", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "4" || drMaster["TransTypeID"].ToString() == "18" || drMaster["TransTypeID"].ToString() == "19")
		{
			GlobalFunctions.OpenForm("ERP.SafesAndBanks.BankTransactions.frmBankOut", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "5")
		{
			if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.Sales.Transactions.frmSLInvoices'").Length != 0)
			{
				GlobalFunctions.OpenForm("ERP.Sales.Transactions.frmSLInvoices", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			}
			else
			{
				GlobalFunctions.OpenForm("ERP.Sales.Transactions.frmSLInvoices2", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			}
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "8")
		{
			if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.Purchasing.Transactions.frmPSInvoices'").Length != 0)
			{
				GlobalFunctions.OpenForm("ERP.Purchasing.Transactions.frmPSInvoices", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			}
			else
			{
				GlobalFunctions.OpenForm("ERP.Purchasing.Transactions.frmPSInvoices2", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			}
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "11")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmMaterialIssueVouchers", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "12")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmStoresSettlementVouchers", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "13")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmStoreTakings", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "14" || drMaster["TransTypeID"].ToString() == "26")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmSuppliersReturns", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "15" || drMaster["TransTypeID"].ToString() == "25")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmClientsDepartmentsReturns", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "23")
		{
			GlobalFunctions.OpenForm("ERP.SafesAndBanks.SafeTransactions.frmCustody", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "24")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmGoodReceiptNotes", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "27")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmStoreTransferVouchers", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "35")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Transactions.frmStoreRevaluations", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "36")
		{
			GlobalFunctions.OpenForm("ERP.StockControl.Slicing.frmSlicing", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "48")
		{
			GlobalFunctions.OpenForm("ERP.MarineService.Transactions.frmOperationInvoices", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "49")
		{
			GlobalFunctions.OpenForm("ERP.MarineService.Transactions.frmOperationExpenses", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "52")
		{
			GlobalFunctions.OpenForm("ERP.CnsProjects.Transactions.frmProjectAttendance", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "54")
		{
			GlobalFunctions.OpenForm("ERP.MarineService.Transactions.frmProExpenses", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "56")
		{
			GlobalFunctions.OpenForm("ERP.Accounting.Transactions.frmGeneralExpenses", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
		else if (drMaster["TransTypeID"].ToString() == "60")
		{
			GlobalFunctions.OpenForm("ERP.Export.Transactions.frmExpOperationsDeclarations", Convert.ToInt32(drMaster["VoucherID"]), base.Width, base.Height);
			FillData();
		}
	}

	public override void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext("," + GlobalVariables.CurrentBranchID + ",", TableName, IDCol, NoCol, DateCol, RowID, "", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public override void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext("," + GlobalVariables.CurrentBranchID + ",", TableName, IDCol, NoCol, DateCol, RowID, "", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
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
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Transactions.frmJV));
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
		this.lblTransType = new UltraLabel();
		this.cboTransType = new UltraComboEditor();
		this.txtTotalDebit = new UltraTextEditor();
		this.txtTotalCredit = new UltraTextEditor();
		this.lblTotalDebit = new UltraLabel();
		this.lblTotalCredit = new UltraLabel();
		this.lblReceiptNo = new UltraLabel();
		this.txtReceiptNo = new UltraTextEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.chkOpeningJV = new UltraCheckEditor();
		this.cboCurrency = new UltraComboEditor();
		this.chkCurrency = new UltraCheckEditor();
		this.btnView = new UltraButton();
		this.txtDiff = new UltraTextEditor();
		this.lblDiff = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceiptNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkOpeningJV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiff).BeginInit();
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
		resources.ApplyResources(this.lblTransType, "lblTransType");
		this.lblTransType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTransType).Name = "lblTransType";
		((ControlBase)this.lblTransType).WrapText = false;
		resources.ApplyResources(this.cboTransType, "cboTransType");
		((TextEditorControlBase)this.cboTransType).AlwaysInEditMode = true;
		this.cboTransType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTransType).Name = "cboTransType";
		((TextEditorControlBase)this.cboTransType).ValueChanged += new System.EventHandler(cboTransType_ValueChanged);
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
		resources.ApplyResources(this.lblReceiptNo, "lblReceiptNo");
		this.lblReceiptNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceiptNo).Name = "lblReceiptNo";
		((ControlBase)this.lblReceiptNo).WrapText = false;
		resources.ApplyResources(this.txtReceiptNo, "txtReceiptNo");
		((System.Windows.Forms.Control)(object)this.txtReceiptNo).Name = "txtReceiptNo";
		((System.Windows.Forms.Control)(object)this.txtReceiptNo).DoubleClick += new System.EventHandler(txtReceiptNo_DoubleClick);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.chkOpeningJV, "chkOpeningJV");
		((System.Windows.Forms.Control)(object)this.chkOpeningJV).Name = "chkOpeningJV";
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((EditorButtonControlBase)this.cboCurrency).ReadOnly = true;
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.chkCurrency, "chkCurrency");
		((System.Windows.Forms.Control)(object)this.chkCurrency).Name = "chkCurrency";
		((UltraToggleEditorBase)this.chkCurrency).CheckedChanged += new System.EventHandler(chkCurrency_CheckedChanged);
		resources.ApplyResources(this.btnView, "btnView");
		((System.Windows.Forms.Control)(object)this.btnView).Name = "btnView";
		((System.Windows.Forms.Control)(object)this.btnView).Click += new System.EventHandler(btnView_Click);
		resources.ApplyResources(this.txtDiff, "txtDiff");
		((System.Windows.Forms.Control)(object)this.txtDiff).Name = "txtDiff";
		((EditorButtonControlBase)this.txtDiff).ReadOnly = true;
		resources.ApplyResources(this.lblDiff, "lblDiff");
		this.lblDiff.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiff).Name = "lblDiff";
		((ControlBase)this.lblDiff).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnView);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalCredit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCurrency);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTransType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTransType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReceiptNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceiptNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkOpeningJV);
		base.Name = "frmJV";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkOpeningJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceiptNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReceiptNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTransType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTransType, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalCredit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnView, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceiptNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkOpeningJV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiff).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
