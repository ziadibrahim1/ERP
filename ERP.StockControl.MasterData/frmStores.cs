using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.MasterData;

public class frmStores : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtCostCenters;

	private DataTable dtBranches;

	private ValueList vlBranches = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlAccounts = new ValueList();

	private ValueList vlAddedSettlementAccounts = new ValueList();

	private ValueList vlSubtractSettlementAccounts = new ValueList();

	private ValueList vlAddedStoreTakingAccounts = new ValueList();

	private ValueList vlSubtractStoreTakingAccounts = new ValueList();

	private bool UseCostCenters;

	private IContainer components = null;

	private UltraTextEditor txtStoreNameEn;

	private UltraLabel lblStoreNameEn;

	private UltraTextEditor txtStoreNameAr;

	private UltraLabel lblStoreNameAr;

	private UltraTextEditor txtStoreCode;

	private UltraLabel lblStoreCode;

	public UltraButton btnStoreAccountSearch;

	private UltraComboEditor cboStoreAccount;

	private UltraLabel lblStoreAccount;

	public UltraButton btnCostCenterSearch;

	private UltraComboEditor cboCostCenter;

	private UltraLabel lblCostCenter;

	private UltraCheckEditor chkLocked;

	private UltraComboEditor cboBranchs;

	private UltraLabel lblBranch;

	private UltraLabel lblWeight;

	private UltraTextEditor txtWeight;

	private UltraLabel lblAddedSettlementAccount;

	private UltraComboEditor cboAddedSettlementAccount;

	public UltraButton btnAddedSettlementAccounSearch;

	private UltraLabel lblSubtractSettlementAccount;

	private UltraComboEditor cboSubtractSettlementAccount;

	public UltraButton btnSubtractSettlementAccountSearch;

	private UltraLabel lblAddedStoreTakingAccount;

	private UltraComboEditor cboAddedStoreTakingAccount;

	public UltraButton btnAddedStoreTakingAccountSearch;

	private UltraLabel lblSubtractStoreTakingAccount;

	private UltraComboEditor cboSubtractStoreTakingAccount;

	public UltraButton btnSubtractStoreTakingAccountSearch;

	public frmStores()
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SC_Stores";
		IDCol = "StoreID";
	}

	public override void PrepareData()
	{
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlCostCenters.ValueListItems.Clear();
			for (int i = 0; i < dtCostCenters.Rows.Count; i++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[i]["CostCenterID"], dtCostCenters.Rows[i]["Name"].ToString());
			}
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		}
		dtBranches = Branches.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int j = 0; j < dtBranches.Rows.Count; j++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[j]["BranchID"], dtBranches.Rows[j][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		GlobalFunctions.FillCombo(cboBranchs, dtBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStoreAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboAddedSettlementAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSubtractSettlementAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboAddedStoreTakingAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSubtractStoreTakingAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		vlAddedSettlementAccounts.ValueListItems.Clear();
		vlAddedStoreTakingAccounts.ValueListItems.Clear();
		vlSubtractSettlementAccounts.ValueListItems.Clear();
		vlSubtractStoreTakingAccounts.ValueListItems.Clear();
		for (int k = 0; k < dtAccounts.Rows.Count; k++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlAddedSettlementAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlAddedStoreTakingAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlSubtractSettlementAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlSubtractStoreTakingAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = NavMode;
		((EditorButtonControlBase)txtStoreCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStoreNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStoreNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCostCenter).ReadOnly = NavMode;
		((EditorButtonControlBase)cboStoreAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAddedStoreTakingAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubtractStoreTakingAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAddedSettlementAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubtractSettlementAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranchs).ReadOnly = NavMode || Updating;
		((Control)(object)chkLocked).Enabled = !NavMode;
		((Control)(object)btnStoreAccountSearch).Visible = !NavMode;
		((Control)(object)btnCostCenterSearch).Visible = !NavMode && UseCostCenters;
		((Control)(object)lblCostCenter).Visible = UseCostCenters;
		((Control)(object)cboCostCenter).Visible = UseCostCenters;
		((TextEditorControlBase)txtStoreCode).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtStoreCode).Text = (Adding ? Stores.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtStoreNameAr).Clear();
		((TextEditorControlBase)txtStoreNameEn).Clear();
		((TextEditorControlBase)txtWeight).Clear();
		((UltraToggleEditorBase)chkLocked).Checked = false;
		cboCostCenter.SelectedIndex = -1;
		cboStoreAccount.SelectedIndex = -1;
		cboAddedStoreTakingAccount.SelectedIndex = -1;
		cboSubtractStoreTakingAccount.SelectedIndex = -1;
		cboAddedSettlementAccount.SelectedIndex = -1;
		cboSubtractSettlementAccount.SelectedIndex = -1;
		cboBranchs.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Stores.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = !UseCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود المخزن" : "Store Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن النسبى" : "Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Locked");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب المخزن" : "Store Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreAccID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedSettlementAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب تسوية بالأضافة" : "Added Settlement Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedSettlementAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedSettlementAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedSettlementAccountID"].ValueList = (IValueList)(object)vlAddedSettlementAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractSettlementAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب تسوية بالخصم" : "Subtract Settlement Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractSettlementAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractSettlementAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractSettlementAccountID"].ValueList = (IValueList)(object)vlSubtractSettlementAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedStoreTakingAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الجرد بالأضافة" : "Added Store Taking Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedStoreTakingAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedStoreTakingAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedStoreTakingAccountID"].ValueList = (IValueList)(object)vlAddedStoreTakingAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractStoreTakingAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الجرد بالخصم" : "Subtract Store Taking Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractStoreTakingAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractStoreTakingAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubtractStoreTakingAccountID"].ValueList = (IValueList)(object)vlSubtractStoreTakingAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtStoreCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["StoreCode"].Value.ToString();
		((Control)(object)txtStoreNameAr).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["StoreNameAr"].Value.ToString();
		((Control)(object)txtStoreNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["StoreNameEn"].Value.ToString();
		((Control)(object)txtWeight).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Weight"].Value.ToString();
		((UltraToggleEditorBase)chkLocked).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Locked"].Value.ToString());
		((TextEditorControlBase)cboCostCenter).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CostCenterID"].Value;
		((TextEditorControlBase)cboStoreAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["StoreAccID"].Value;
		((TextEditorControlBase)cboAddedSettlementAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AddedSettlementAccountID"].Value;
		((TextEditorControlBase)cboSubtractSettlementAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubtractSettlementAccountID"].Value;
		((TextEditorControlBase)cboAddedStoreTakingAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AddedStoreTakingAccountID"].Value;
		((TextEditorControlBase)cboSubtractStoreTakingAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubtractStoreTakingAccountID"].Value;
		((TextEditorControlBase)cboBranchs).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtStoreCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود المخزن", "Please Insert Store Code");
			((TextEditorControlBase)txtStoreCode).Focus();
			return false;
		}
		if (((Control)(object)txtStoreNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم المخزن بالعربية", "Please Insert Bank Arabic Name");
			((TextEditorControlBase)txtStoreNameAr).Focus();
			return false;
		}
		if (cboStoreAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب المخزن", "Please Select Store Account Name");
			((TextEditorControlBase)cboStoreAccount).Focus();
			return false;
		}
		if (cboBranchs.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الفرع التابع له المخزن", "Please Select Store Branch");
			((TextEditorControlBase)cboBranchs).Focus();
			return false;
		}
		if ((Adding && dataTable.Select("StoreNameAr='" + ((Control)(object)txtStoreNameAr).Text + "'").Length != 0) || (Updating && dataTable.Select("StoreNameAr='" + ((Control)(object)txtStoreNameAr).Text + "'").Length > 1))
		{
			GlobalVariables.InformationMB.Show("يوجد مخزن بنفس الاسم", "A store with the same name already exists");
			((TextEditorControlBase)txtStoreNameAr).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Stores.Insert_Update("-1", ((Control)(object)txtStoreCode).Text, ((Control)(object)txtStoreNameAr).Text, (((Control)(object)txtStoreNameEn).Text == "") ? "Null" : ((Control)(object)txtStoreNameEn).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), ((TextEditorControlBase)cboStoreAccount).Value.ToString(), (cboAddedSettlementAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAddedSettlementAccount).Value.ToString(), (cboSubtractSettlementAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubtractSettlementAccount).Value.ToString(), (cboAddedStoreTakingAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAddedStoreTakingAccount).Value.ToString(), (cboSubtractStoreTakingAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubtractStoreTakingAccount).Value.ToString(), ((UltraToggleEditorBase)chkLocked).Checked ? "1" : "0", (((Control)(object)txtWeight).Text == "") ? "1" : ((Control)(object)txtWeight).Text, ((TextEditorControlBase)cboBranchs).Value.ToString(), "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Stores.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), ((Control)(object)txtStoreCode).Text, ((Control)(object)txtStoreNameAr).Text, (((Control)(object)txtStoreNameEn).Text == "") ? "Null" : ((Control)(object)txtStoreNameEn).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), ((TextEditorControlBase)cboStoreAccount).Value.ToString(), (cboAddedSettlementAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAddedSettlementAccount).Value.ToString(), (cboSubtractSettlementAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubtractSettlementAccount).Value.ToString(), (cboAddedStoreTakingAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAddedStoreTakingAccount).Value.ToString(), (cboSubtractStoreTakingAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubtractStoreTakingAccount).Value.ToString(), ((UltraToggleEditorBase)chkLocked).Checked ? "1" : "0", (((Control)(object)txtWeight).Text == "") ? "1" : ((Control)(object)txtWeight).Text, ((TextEditorControlBase)cboBranchs).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Stores.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlCostCenters.ValueListItems.Clear();
			for (int i = 0; i < dtCostCenters.Rows.Count; i++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[i]["CostCenterID"], dtCostCenters.Rows[i]["Name"].ToString());
			}
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		}
		dtBranches = Branches.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int j = 0; j < dtBranches.Rows.Count; j++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[j]["BranchID"], dtBranches.Rows[j][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		GlobalFunctions.FillCombo(cboBranchs, dtBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStoreAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboAddedSettlementAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSubtractSettlementAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboAddedStoreTakingAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSubtractStoreTakingAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		vlAddedSettlementAccounts.ValueListItems.Clear();
		vlAddedStoreTakingAccounts.ValueListItems.Clear();
		vlSubtractSettlementAccounts.ValueListItems.Clear();
		vlSubtractStoreTakingAccounts.ValueListItems.Clear();
		for (int k = 0; k < dtAccounts.Rows.Count; k++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlAddedSettlementAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlAddedStoreTakingAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlSubtractSettlementAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
			vlSubtractStoreTakingAccounts.ValueListItems.Add((object)dtAccounts.Rows[k]["AccountID"].ToString(), dtAccounts.Rows[k]["Name"].ToString());
		}
	}

	private void ComboAccount_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)(UltraComboEditor)sender).Value = num;
			}
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		if (((Control)(UltraButton)sender).Name == "btnStoreAccountSearch")
		{
			((TextEditorControlBase)cboStoreAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnCostCenterSearch")
		{
			((TextEditorControlBase)cboCostCenter).Value = SearchFunctions.CostCenter(IsFromServer: true);
		}
	}

	private void cboCostCenter_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.CostCenter(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostCenter).Value = num;
			}
		}
	}

	private void btnAddedSettlementAccounSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboAddedSettlementAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void btnSubtractSettlementAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboSubtractSettlementAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void btnAddedStoreTakingAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboAddedStoreTakingAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void btnSubtractStoreTakingAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboSubtractStoreTakingAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public override void Search()
	{
		int num = SearchFunctions.AllStores(IsFromServer: false);
		if (num != 0)
		{
			if (((UltraGridBase)ULGData).ActiveRow != null)
			{
				((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = false;
			}
			int num2 = dataTable.Rows.IndexOf(dataTable.Select("StoreID=" + num)[0]);
			((UltraGridBase)ULGData).Rows[num2].Activate();
			((GridItemBase)((UltraGridBase)ULGData).Rows[num2]).Selected = true;
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
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmStores));
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
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		this.txtStoreNameEn = new UltraTextEditor();
		this.lblStoreNameEn = new UltraLabel();
		this.txtStoreNameAr = new UltraTextEditor();
		this.lblStoreNameAr = new UltraLabel();
		this.txtStoreCode = new UltraTextEditor();
		this.lblStoreCode = new UltraLabel();
		this.btnStoreAccountSearch = new UltraButton();
		this.cboStoreAccount = new UltraComboEditor();
		this.lblStoreAccount = new UltraLabel();
		this.btnCostCenterSearch = new UltraButton();
		this.cboCostCenter = new UltraComboEditor();
		this.lblCostCenter = new UltraLabel();
		this.chkLocked = new UltraCheckEditor();
		this.cboBranchs = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		this.lblWeight = new UltraLabel();
		this.txtWeight = new UltraTextEditor();
		this.lblAddedSettlementAccount = new UltraLabel();
		this.cboAddedSettlementAccount = new UltraComboEditor();
		this.btnAddedSettlementAccounSearch = new UltraButton();
		this.lblSubtractSettlementAccount = new UltraLabel();
		this.cboSubtractSettlementAccount = new UltraComboEditor();
		this.btnSubtractSettlementAccountSearch = new UltraButton();
		this.lblAddedStoreTakingAccount = new UltraLabel();
		this.cboAddedStoreTakingAccount = new UltraComboEditor();
		this.btnAddedStoreTakingAccountSearch = new UltraButton();
		this.lblSubtractStoreTakingAccount = new UltraLabel();
		this.cboSubtractStoreTakingAccount = new UltraComboEditor();
		this.btnSubtractStoreTakingAccountSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStoreAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkLocked).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedSettlementAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubtractSettlementAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedStoreTakingAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubtractStoreTakingAccount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance16");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtStoreNameEn, "txtStoreNameEn");
		((System.Windows.Forms.Control)(object)this.txtStoreNameEn).Name = "txtStoreNameEn";
		resources.ApplyResources(this.lblStoreNameEn, "lblStoreNameEn");
		this.lblStoreNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoreNameEn).Name = "lblStoreNameEn";
		((ControlBase)this.lblStoreNameEn).WrapText = false;
		resources.ApplyResources(this.txtStoreNameAr, "txtStoreNameAr");
		((System.Windows.Forms.Control)(object)this.txtStoreNameAr).Name = "txtStoreNameAr";
		resources.ApplyResources(this.lblStoreNameAr, "lblStoreNameAr");
		this.lblStoreNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoreNameAr).Name = "lblStoreNameAr";
		((ControlBase)this.lblStoreNameAr).WrapText = false;
		resources.ApplyResources(this.txtStoreCode, "txtStoreCode");
		((System.Windows.Forms.Control)(object)this.txtStoreCode).Name = "txtStoreCode";
		resources.ApplyResources(this.lblStoreCode, "lblStoreCode");
		this.lblStoreCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoreCode).Name = "lblStoreCode";
		((ControlBase)this.lblStoreCode).WrapText = false;
		resources.ApplyResources(this.btnStoreAccountSearch, "btnStoreAccountSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance17");
		((ControlBase)this.btnStoreAccountSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnStoreAccountSearch).Name = "btnStoreAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboStoreAccount, "cboStoreAccount");
		this.cboStoreAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStoreAccount).Name = "cboStoreAccount";
		((System.Windows.Forms.Control)(object)this.cboStoreAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.lblStoreAccount, "lblStoreAccount");
		this.lblStoreAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoreAccount).Name = "lblStoreAccount";
		((ControlBase)this.lblStoreAccount).WrapText = false;
		resources.ApplyResources(this.btnCostCenterSearch, "btnCostCenterSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance18");
		((ControlBase)this.btnCostCenterSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Name = "btnCostCenterSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		((System.Windows.Forms.Control)(object)this.cboCostCenter).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCostCenter_KeyDown);
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.chkLocked, "chkLocked");
		((System.Windows.Forms.Control)(object)this.chkLocked).Name = "chkLocked";
		resources.ApplyResources(this.cboBranchs, "cboBranchs");
		this.cboBranchs.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchs).Name = "cboBranchs";
		resources.ApplyResources(this.lblBranch, "lblBranch");
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.lblWeight, "lblWeight");
		this.lblWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeight).Name = "lblWeight";
		((ControlBase)this.lblWeight).WrapText = false;
		resources.ApplyResources(this.txtWeight, "txtWeight");
		((System.Windows.Forms.Control)(object)this.txtWeight).Name = "txtWeight";
		((System.Windows.Forms.Control)(object)this.txtWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtWeight_KeyPress);
		resources.ApplyResources(this.lblAddedSettlementAccount, "lblAddedSettlementAccount");
		this.lblAddedSettlementAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedSettlementAccount).Name = "lblAddedSettlementAccount";
		((ControlBase)this.lblAddedSettlementAccount).WrapText = false;
		resources.ApplyResources(this.cboAddedSettlementAccount, "cboAddedSettlementAccount");
		this.cboAddedSettlementAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAddedSettlementAccount).Name = "cboAddedSettlementAccount";
		((System.Windows.Forms.Control)(object)this.cboAddedSettlementAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnAddedSettlementAccounSearch, "btnAddedSettlementAccounSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance19");
		((ControlBase)this.btnAddedSettlementAccounSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnAddedSettlementAccounSearch).Name = "btnAddedSettlementAccounSearch";
		((System.Windows.Forms.Control)(object)this.btnAddedSettlementAccounSearch).Click += new System.EventHandler(btnAddedSettlementAccounSearch_Click);
		resources.ApplyResources(this.lblSubtractSettlementAccount, "lblSubtractSettlementAccount");
		this.lblSubtractSettlementAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubtractSettlementAccount).Name = "lblSubtractSettlementAccount";
		((ControlBase)this.lblSubtractSettlementAccount).WrapText = false;
		resources.ApplyResources(this.cboSubtractSettlementAccount, "cboSubtractSettlementAccount");
		this.cboSubtractSettlementAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubtractSettlementAccount).Name = "cboSubtractSettlementAccount";
		((System.Windows.Forms.Control)(object)this.cboSubtractSettlementAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnSubtractSettlementAccountSearch, "btnSubtractSettlementAccountSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance20");
		((ControlBase)this.btnSubtractSettlementAccountSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnSubtractSettlementAccountSearch).Name = "btnSubtractSettlementAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubtractSettlementAccountSearch).Click += new System.EventHandler(btnSubtractSettlementAccountSearch_Click);
		resources.ApplyResources(this.lblAddedStoreTakingAccount, "lblAddedStoreTakingAccount");
		this.lblAddedStoreTakingAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedStoreTakingAccount).Name = "lblAddedStoreTakingAccount";
		((ControlBase)this.lblAddedStoreTakingAccount).WrapText = false;
		resources.ApplyResources(this.cboAddedStoreTakingAccount, "cboAddedStoreTakingAccount");
		this.cboAddedStoreTakingAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAddedStoreTakingAccount).Name = "cboAddedStoreTakingAccount";
		((System.Windows.Forms.Control)(object)this.cboAddedStoreTakingAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnAddedStoreTakingAccountSearch, "btnAddedStoreTakingAccountSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance21");
		((ControlBase)this.btnAddedStoreTakingAccountSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnAddedStoreTakingAccountSearch).Name = "btnAddedStoreTakingAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAddedStoreTakingAccountSearch).Click += new System.EventHandler(btnAddedStoreTakingAccountSearch_Click);
		resources.ApplyResources(this.lblSubtractStoreTakingAccount, "lblSubtractStoreTakingAccount");
		this.lblSubtractStoreTakingAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubtractStoreTakingAccount).Name = "lblSubtractStoreTakingAccount";
		((ControlBase)this.lblSubtractStoreTakingAccount).WrapText = false;
		resources.ApplyResources(this.cboSubtractStoreTakingAccount, "cboSubtractStoreTakingAccount");
		this.cboSubtractStoreTakingAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubtractStoreTakingAccount).Name = "cboSubtractStoreTakingAccount";
		((System.Windows.Forms.Control)(object)this.cboSubtractStoreTakingAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnSubtractStoreTakingAccountSearch, "btnSubtractStoreTakingAccountSearch");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance22");
		((ControlBase)this.btnSubtractStoreTakingAccountSearch).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnSubtractStoreTakingAccountSearch).Name = "btnSubtractStoreTakingAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubtractStoreTakingAccountSearch).Click += new System.EventHandler(btnSubtractStoreTakingAccountSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchs);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkLocked);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCenterSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddedStoreTakingAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtractSettlementAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtractStoreTakingAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddedSettlementAccounSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAddedStoreTakingAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubtractSettlementAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedStoreTakingAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubtractStoreTakingAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAddedSettlementAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubtractSettlementAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubtractStoreTakingAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStoreAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedSettlementAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoreAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStoreNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoreNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStoreNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoreNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStoreCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoreCode);
		base.Name = "frmStores";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoreCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStoreCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoreNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStoreNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoreNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStoreNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoreAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedSettlementAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStoreAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubtractStoreTakingAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubtractSettlementAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAddedSettlementAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubtractStoreTakingAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedStoreTakingAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubtractSettlementAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAddedStoreTakingAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddedSettlementAccounSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtractStoreTakingAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtractSettlementAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddedStoreTakingAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCostCenterSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkLocked, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchs, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStoreAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkLocked).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedSettlementAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubtractSettlementAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedStoreTakingAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubtractStoreTakingAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
