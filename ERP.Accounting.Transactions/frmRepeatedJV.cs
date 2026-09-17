using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Transactions;

public class frmRepeatedJV : frmHeaderDetails
{
	private DataTable dtJVDefaults;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCostCenters;

	private DataTable dtSubCostCenters;

	private DataTable dtCurrency;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSubCostCenters = new ValueList();

	private ValueList vlCurency = new ValueList();

	private bool UseSubAccounts;

	private bool UseCostCenters;

	private bool UseCurrency;

	private bool useSubCostCenters;

	private IContainer components = null;

	private UltraLabel lblTotalCredit;

	private UltraLabel lblTotalDebit;

	private UltraTextEditor txtTotalCredit;

	private UltraTextEditor txtTotalDebit;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraComboEditor cboCurrency;

	private UltraCheckEditor chkCurrency;

	private UltraLabel lblTransType;

	private UltraComboEditor cboTransType;

	private UltraButton btnGenerateJV;

	public frmRepeatedJV()
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
		TableName = "A_RepeatedJV";
		IDCol = "RepeatedJVID";
		NoCol = "JVName";
		DateCol = "GetDate()";
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
		dtJVDefaults = JVDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransType, dtJVDefaults, "TransTypeID", GlobalVariables.IsArabic ? "TransTypeNameAr" : "TransTypeNameEn");
		dtDetails = RepeatedJVDetails.SelectByRepeatedJVID("0", GlobalVariables.IsArabic ? "1" : "0");
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
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = RepeatedJV.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		((Control)(object)btnGenerateJV).Visible = drMaster != null;
		if (drMaster != null)
		{
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["JVName"].ToString();
			((TextEditorControlBase)cboTransType).Value = drMaster["TransTypeID"];
			((Control)(object)txtTotalDebit).Text = decimal.Parse(drMaster["TotalDebit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalCredit).Text = decimal.Parse(drMaster["TotalCredit"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = RepeatedJVDetails.SelectByRepeatedJVID(drMaster["RepeatedJVID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
		((Control)(object)btnGenerateJV).Visible = drMaster != null && NavMode;
		((EditorButtonControlBase)cboTransType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkCurrency).Visible = !NavMode && UseCurrency;
		((Control)(object)cboCurrency).Visible = !NavMode && UseCurrency;
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
		((Control)(object)txtCode).Text = (Adding ? RepeatedJV.GetCode() : "");
		((UltraToggleEditorBase)chkCurrency).Checked = false;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).Clear();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RepeatedJVDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Debit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Credit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalDebit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LocalCredit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExchangeRate"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].DefaultCellValue = GlobalVariables.LocalCurrencyID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDocumented"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Deleted"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
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
		if (cboTransType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء أن تختار نوع الحركه", "Please select a Tans. type for this JV");
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
		for (int i = 0; i < ((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الحساب أو حذف السطر  ", "Please Enter Account Name or Delete Row ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"];
				return false;
			}
			if (decimal.Parse(((DataTable)((UltraGridBase)ULGData).DataSource).Rows[i]["ExchangeRate"].ToString()) == 0m)
			{
				GlobalVariables.InformationMB.Show("سعر التحويل لابد ان يكون اكبر من الصفر", "Exchange Rate Must Be Greater Than Zero");
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
		if (Main.CheckForValue("A_RepeatedJV", "JVName", ((Control)(object)txtCode).Text, Adding ? "" : drMaster["JVName"].ToString(), IsFromServer: false) > 0)
		{
			GlobalVariables.InformationMB.Show("رقم هذا القيد متواجد من قبل \n ", "The JV Number Already Exists ");
			base.ActiveControl = (Control)(object)txtCode;
			drMaster = null;
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = RepeatedJV.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtTotalDebit).Text, ((Control)(object)txtTotalCredit).Text, ((TextEditorControlBase)cboTransType).Value.ToString(), "", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["RepeatedJVDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["RepeatedJVID"].Value = num;
			}
			RepeatedJVDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = RepeatedJV.Insert_Update(drMaster["RepeatedJVID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtTotalDebit).Text, ((Control)(object)txtTotalCredit).Text, ((TextEditorControlBase)cboTransType).Value.ToString(), "", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["RepeatedJVID"].Value = num;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["RepeatedJVDetailID"].Value.ToString() + ",";
			}
			if (text != ",")
			{
				Main.DeleteForUpdate("A_RepeatedJVDetails", "RepeatedJVID", num.ToString(), "RepeatedJVDetailID", text);
			}
			RepeatedJVDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			RepeatedJVDetails.DeleteVirtualByRepeatedJVID(drMaster["RepeatedJVID"].ToString(), GlobalVariables.UserID);
			RepeatedJV.DeleteVirtual(drMaster["RepeatedJVID"].ToString(), GlobalVariables.UserID);
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
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.RepeatedJVReport(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			DataTable dataTable = RepeatedJV.Select(dtSearchResult.Rows[0]["RepeatedJVID"].ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
		}
		DisplayData();
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(DateTime.Now.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Debit")
			{
				e.Cell.Row.Cells["localDebit"].Value = (e.Cell.Row.Cells["Debit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Debit"].Value.ToString()) * ((e.Cell.Row.Cells["CurrencyID"].Value != null) ? decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) : 0m))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Credit")
			{
				e.Cell.Row.Cells["localCredit"].Value = (e.Cell.Row.Cells["Credit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Credit"].Value.ToString()) * ((e.Cell.Row.Cells["CurrencyID"].Value != null) ? decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) : 0m))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ExchangeRate" || ((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyID")
			{
				e.Cell.Row.Cells["localDebit"].Value = (e.Cell.Row.Cells["Debit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Debit"].Value.ToString()) * ((e.Cell.Row.Cells["CurrencyID"].Value != null) ? decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) : 0m))).ToString();
				e.Cell.Row.Cells["localCredit"].Value = (e.Cell.Row.Cells["Credit"].Value.Equals(DBNull.Value) ? 0m : (decimal.Parse(e.Cell.Row.Cells["Credit"].Value.ToString()) * ((e.Cell.Row.Cells["CurrencyID"].Value != null) ? decimal.Parse(e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) : 0m))).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalDebit")
			{
				e.Cell.Row.Cells["ExchangeRate"].Value = (decimal.Parse(e.Cell.Row.Cells["localDebit"].Value.ToString()) / decimal.Parse(e.Cell.Row.Cells["Debit"].Value.ToString())).ToString();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "LocalCredit")
			{
				e.Cell.Row.Cells["ExchangeRate"].Value = (decimal.Parse(e.Cell.Row.Cells["localCredit"].Value.ToString()) / decimal.Parse(e.Cell.Row.Cells["Credit"].Value.ToString())).ToString();
			}
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

	private void btnGenerateJV_Click(object sender, EventArgs e)
	{
		frmJV frmJV2 = new frmJV(drMaster);
		frmJV2.Size = new Size(base.Width, base.Height);
		frmJV2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
		frmJV2.ShowDialog();
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
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Expected O, but got Unknown
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Transactions.frmRepeatedJV));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.txtTotalDebit = new UltraTextEditor();
		this.txtTotalCredit = new UltraTextEditor();
		this.lblTotalDebit = new UltraLabel();
		this.lblTotalCredit = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.chkCurrency = new UltraCheckEditor();
		this.lblTransType = new UltraLabel();
		this.cboTransType = new UltraComboEditor();
		this.btnGenerateJV = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransType).BeginInit();
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtTotalDebit, "txtTotalDebit");
		((System.Windows.Forms.Control)(object)this.txtTotalDebit).Name = "txtTotalDebit";
		resources.ApplyResources(this.txtTotalCredit, "txtTotalCredit");
		((System.Windows.Forms.Control)(object)this.txtTotalCredit).Name = "txtTotalCredit";
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
		((EditorButtonControlBase)this.cboCurrency).ReadOnly = true;
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.chkCurrency, "chkCurrency");
		((System.Windows.Forms.Control)(object)this.chkCurrency).Name = "chkCurrency";
		((UltraToggleEditorBase)this.chkCurrency).CheckedChanged += new System.EventHandler(chkCurrency_CheckedChanged);
		resources.ApplyResources(this.lblTransType, "lblTransType");
		this.lblTransType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTransType).Name = "lblTransType";
		((ControlBase)this.lblTransType).WrapText = false;
		resources.ApplyResources(this.cboTransType, "cboTransType");
		((TextEditorControlBase)this.cboTransType).AlwaysInEditMode = true;
		this.cboTransType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTransType).Name = "cboTransType";
		resources.ApplyResources(this.btnGenerateJV, "btnGenerateJV");
		((System.Windows.Forms.Control)(object)this.btnGenerateJV).Name = "btnGenerateJV";
		((System.Windows.Forms.Control)(object)this.btnGenerateJV).Click += new System.EventHandler(btnGenerateJV_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGenerateJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalCredit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalDebit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalCredit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalDebit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTransType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTransType);
		base.Name = "frmRepeatedJV";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTransType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTransType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalDebit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalCredit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalDebit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalCredit, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGenerateJV, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalDebit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCredit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransType).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
