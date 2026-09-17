using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.MasterData;

public class frmUpdateGroupItems : frmBase
{
	private DataTable dtItemsTypes;

	private DataTable dtUnitGroup;

	private DataTable dtUnits;

	private DataTable dtCostCenters;

	private DataTable dtStores;

	private DataTable dtGroups;

	private DataTable dtItemPrices;

	private DataTable dtItemStockLevels;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private DataTable dtAccounts;

	private DataTable dtTaxes;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private string GroupID;

	private DataRow drGroup;

	private IContainer components = null;

	public UltraLabel lblRoot;

	public UltraComboEditor cboGroup;

	public UltraLabel lblUnitGroup;

	public UltraComboEditor cboUnitGroup;

	public UltraLabel lblUnit;

	public UltraComboEditor cboUnit;

	public UltraComboEditor cboStore;

	public UltraLabel lblStore;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	public UltraGrid ULGPrices;

	private UltraPanel pnlPrice;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	private UltraCheckEditor chkIsActive;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	private UltraCheckEditor chkEnforceBatchNo;

	private UltraCheckEditor chkIsProductionItem;

	private UltraCheckEditor chkIsSalesItem;

	private UltraTextEditor txtValidityDays;

	private UltraLabel lblValidityDays;

	public UltraButton btnServiceAccountSearch;

	private UltraComboEditor cboServiceAccount;

	private UltraLabel lblServiceAccount;

	public UltraButton btnDepartmentIssueAccount;

	private UltraComboEditor cboDepartmentIssueAccount;

	private UltraLabel lblDepartmentIssueAccount;

	public UltraButton btnPurchaseReturnsAccount;

	private UltraComboEditor cboPurchaseReturnsAccount;

	private UltraLabel lblPurchaseReturnsAccount;

	public UltraButton btnPurchaseAccount;

	private UltraComboEditor cboPurchaseAccount;

	private UltraLabel lblPurchaseAccount;

	public UltraButton btnCostOfSalesAccount;

	private UltraComboEditor cboCostOfSalesAccount;

	private UltraLabel lblCostOfSalesAccount;

	public UltraButton btnSalesReturnsAccount;

	private UltraComboEditor cboSalesReturnsAccount;

	private UltraLabel lblSalesReturnsAccount;

	public UltraButton btnSalesAccount;

	private UltraComboEditor cboSalesAccount;

	private UltraLabel lblSalesAccount;

	private UltraCheckEditor chkModAccounts;

	private UltraPanel pnlAccounts;

	private UltraPanel pnlLevels;

	private UltraCheckEditor chkModLevels;

	private UltraCheckEditor chkModPrices;

	private UltraCheckEditor chkModStore;

	private UltraPanel pnlUnit;

	private UltraPanel pnlBatchNo;

	private UltraCheckEditor chkModBatchNo;

	private UltraCheckEditor chkModUnit;

	private UltraCheckEditor chkModActive;

	private UltraCheckEditor chkModSalseItem;

	private UltraCheckEditor chkModProductionItem;

	private UltraCheckEditor chkModTax;

	private UltraCheckEditor chkCanModPrice;

	private UltraCheckEditor chkModEditPrice;

	private UltraComboEditor cboPrinterName;

	private UltraTextEditor txtPrepareTime;

	private UltraLabel lblPrepareTime;

	private UltraLabel lblPrinterName;

	private UltraPanel pnlPOS;

	private UltraCheckEditor chkModPOS;

	private UltraComboEditor cboItemsTypes;

	private UltraLabel lblItemsTypes;

	private UltraCheckEditor chkModXY;

	public UltraTextEditor txtSphFrom;

	public UltraLabel lblSph;

	public UltraLabel lblCyl;

	public UltraTextEditor txtCylFrom;

	public UltraTextEditor txtSphTo;

	public UltraTextEditor txtCylTo;

	private RadioButton rbStockLevelForEachBranch;

	public UltraGrid ULGLevels;

	private RadioButton rbStockLevelsForAllBranches;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	private UltraCheckEditor chkModItemsTypes;

	private UltraCheckEditor chkHideFromReports;

	private UltraCheckEditor chkModHideFromReports;

	private UltraComboEditor cboCostCenter;

	private UltraLabel lblCostCenter;

	private UltraCheckEditor chkModCostCenter;

	private UltraCheckEditor chkModPurchasePrice;

	private UltraCheckEditor chkCanModPurchasePrice;

	private UltraComboEditor cboGrowthFeesTax;

	private UltraLabel lblGrowthFeesTax;

	private UltraCheckEditor chkModGrowthFeesTax;

	private UltraComboEditor cboTableTax;

	private UltraLabel lblTableTax;

	private UltraCheckEditor chkModTableTax;

	public frmUpdateGroupItems(string groupID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		GroupID = groupID;
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		((Control)(object)chkModBatchNo).Enabled = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		dtGroups = Items.FillGroups("-1", "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGroup, dtGroups, "ItemID", "Name");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGrowthFeesTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboTableTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboCostOfSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboDepartmentIssueAccount, dtAccounts, "AccountID", "Name");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		vlBranches2.ValueListItems.Clear();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[j]["PriceTypeID"], dtPricesTypes.Rows[j]["PriceName"].ToString());
		}
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]) && !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]))
		{
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cboPrinterName.Items.Add((object)installedPrinter, installedPrinter);
			}
			((Control)(object)pnlPOS).Visible = true;
			((Control)(object)chkModPOS).Visible = true;
		}
		dtItemsTypes = ItemsTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboItemsTypes, dtItemsTypes, "ItemTypeID", GlobalVariables.IsArabic ? "ItemTypeNameAr" : "ItemTypeNameEn");
		UltraCheckEditor obj = chkModXY;
		UltraLabel obj2 = lblSph;
		UltraLabel obj3 = lblCyl;
		UltraTextEditor obj4 = txtCylFrom;
		UltraTextEditor obj5 = txtCylTo;
		UltraTextEditor obj6 = txtSphFrom;
		bool flag = (((Control)(object)txtSphTo).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]));
		bool flag3 = (((Control)(object)obj6).Visible = flag);
		bool flag5 = (((Control)(object)obj5).Visible = flag3);
		bool flag7 = (((Control)(object)obj4).Visible = flag5);
		bool flag9 = (((Control)(object)obj3).Visible = flag7);
		bool visible = (((Control)(object)obj2).Visible = flag9);
		((Control)(object)obj).Visible = visible;
		DisplayData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public void DisplayData()
	{
		drGroup = Items.Select(GroupID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
		((TextEditorControlBase)cboGroup).Value = GroupID;
		((TextEditorControlBase)cboUnit).Value = drGroup["UnitID"];
		((TextEditorControlBase)cboUnitGroup).Value = drGroup["UnitTypeID"];
		((TextEditorControlBase)cboStore).Value = drGroup["DefaultStoreID"];
		((TextEditorControlBase)cboServiceAccount).Value = drGroup["ServiceAccountID"];
		((TextEditorControlBase)cboSalesAccount).Value = drGroup["SalesAccount"];
		((TextEditorControlBase)cboSalesReturnsAccount).Value = drGroup["SalesReturnsAccount"];
		((TextEditorControlBase)cboCostOfSalesAccount).Value = drGroup["CostOfSalesAccount"];
		((TextEditorControlBase)cboPurchaseAccount).Value = drGroup["PurchaseAccount"];
		((TextEditorControlBase)cboPurchaseReturnsAccount).Value = drGroup["PurchaseReturnsAccount"];
		((TextEditorControlBase)cboDepartmentIssueAccount).Value = drGroup["DepartmentIssueAccount"];
		((TextEditorControlBase)cboGrowthFeesTax).Value = drGroup["GrowthTaxID"];
		((TextEditorControlBase)cboTableTax).Value = drGroup["TableTaxID"];
		((TextEditorControlBase)cboTax).Value = drGroup["TaxID"];
		((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(drGroup["IsActive"]);
		((UltraToggleEditorBase)chkIsSalesItem).Checked = Convert.ToBoolean(drGroup["IsSalesItem"]);
		((UltraToggleEditorBase)chkIsProductionItem).Checked = Convert.ToBoolean(drGroup["IsProductionItem"]);
		((UltraToggleEditorBase)chkEnforceBatchNo).Checked = Convert.ToBoolean(drGroup["EnforceBatchNo"]);
		rbPriceForAllBranches.CheckedChanged -= rbPriceForAllBranches_CheckedChanged;
		rbPriceForAllBranches.Checked = Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		rbPriceForEachBranches.Checked = !Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		rbPriceForAllBranches.CheckedChanged += rbPriceForAllBranches_CheckedChanged;
		rbStockLevelsForAllBranches.CheckedChanged -= rbStockLevelsForAllBranches_CheckedChanged;
		rbStockLevelsForAllBranches.Checked = drGroup["StockLevelsForAllBranch"].Equals(DBNull.Value) || Convert.ToBoolean(drGroup["StockLevelsForAllBranch"]);
		rbStockLevelForEachBranch.Checked = !drGroup["StockLevelsForAllBranch"].Equals(DBNull.Value) && !Convert.ToBoolean(drGroup["StockLevelsForAllBranch"]);
		rbStockLevelsForAllBranches.CheckedChanged += rbStockLevelsForAllBranches_CheckedChanged;
		((Control)(object)txtValidityDays).Text = drGroup["ValidityDays"].ToString();
		dtItemPrices = ItemsPrices.SelectByGroupID(GroupID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
		dtItemStockLevels = ItemsStockLevels.SelectByGroupID(GroupID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridStockLevels();
	}

	public bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkModXY).Checked && (((Control)(object)txtSphFrom).Text == "" || ((Control)(object)txtSphTo).Text == "" || ((Control)(object)txtCylFrom).Text == "" || ((Control)(object)txtCylTo).Text == ""))
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال قيم في X Y ", "Please Enter Value in X Y ");
			((TextEditorControlBase)cboUnitGroup).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkModUnit).Checked)
		{
			if (cboUnitGroup.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار مجموعه الوحده", "Please select Unit Group");
				((TextEditorControlBase)cboUnitGroup).Focus();
				return false;
			}
			if (cboUnit.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحده", "Please select Unit ");
				((TextEditorControlBase)cboUnit).Focus();
				return false;
			}
		}
		if (((UltraToggleEditorBase)chkModStore).Checked && cboStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
			((TextEditorControlBase)cboStore).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkModBatchNo).Checked && ((UltraToggleEditorBase)chkEnforceBatchNo).Checked && ((Control)(object)txtValidityDays).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد أيام الصلاحيه", "Please Enter Validity Days");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count; i++)
		{
			if (Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"].Value) < Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MinLevel"].Value))
			{
				GlobalVariables.InformationMB.Show("الحد الأقصي يجب ان يكون اكبر من الحد الأدني", "MaxLevel Must Be Greater Than MinLevel");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"];
				return false;
			}
		}
		return true;
	}

	public void Save()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			DataTable dataTable = Items.UpdateGroupItems(((TextEditorControlBase)cboGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModUnit).Checked) ? "-1" : ((TextEditorControlBase)cboUnit).Value.ToString(), (!((UltraToggleEditorBase)chkModUnit).Checked) ? "-1" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModLevels).Checked) ? "-1" : (rbStockLevelsForAllBranches.Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModStore).Checked) ? "-1" : ((cboStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStore).Value.ToString()), (!((UltraToggleEditorBase)chkModProductionItem).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsProductionItem).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModActive).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModHideFromReports).Checked) ? "-1" : (((UltraToggleEditorBase)chkHideFromReports).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModSalseItem).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsSalesItem).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModPrices).Checked) ? "-1" : (rbPriceForAllBranches.Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModBatchNo).Checked) ? "-1" : (((UltraToggleEditorBase)chkEnforceBatchNo).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModBatchNo).Checked || ((Control)(object)txtValidityDays).Text.Trim() == "") ? "-1" : ((Control)(object)txtValidityDays).Text, (!((UltraToggleEditorBase)chkModGrowthFeesTax).Checked) ? "-1" : ((cboGrowthFeesTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGrowthFeesTax).Value.ToString()), (!((UltraToggleEditorBase)chkModTableTax).Checked) ? "-1" : ((cboTableTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTableTax).Value.ToString()), (!((UltraToggleEditorBase)chkModTax).Checked) ? "-1" : ((cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString()), (!((UltraToggleEditorBase)chkModCostCenter).Checked) ? "-1" : ((cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesReturnsAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboCostOfSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostOfSalesAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboPurchaseAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboPurchaseReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseReturnsAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboDepartmentIssueAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDepartmentIssueAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModPOS).Checked || ((Control)(object)txtPrepareTime).Text.Trim() == "") ? "-1" : ((Control)(object)txtPrepareTime).Text, (!((UltraToggleEditorBase)chkModEditPrice).Checked) ? "-1" : (((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModPurchasePrice).Checked) ? "-1" : (((UltraToggleEditorBase)chkCanModPurchasePrice).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModPOS).Checked) ? "-1" : ((cboPrinterName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPrinterName).Value.ToString()), (!((UltraToggleEditorBase)chkModItemsTypes).Checked) ? "-1" : ((cboItemsTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItemsTypes).Value.ToString()), (!((UltraToggleEditorBase)chkModXY).Checked) ? "-1000" : ((Control)(object)txtSphFrom).Text, (!((UltraToggleEditorBase)chkModXY).Checked) ? "1000" : ((Control)(object)txtSphTo).Text, (!((UltraToggleEditorBase)chkModXY).Checked) ? "-1000" : ((Control)(object)txtCylFrom).Text, (!((UltraToggleEditorBase)chkModXY).Checked) ? "1000" : ((Control)(object)txtCylTo).Text, GlobalVariables.UserID, dtItemStockLevels, dtItemPrices, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void cboUnitGroup_ValueChanged(object sender, EventArgs e)
	{
		if (cboUnitGroup.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtUnits);
			dataView.RowFilter = "UnitTypeID=" + ((TextEditorControlBase)cboUnitGroup).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboUnit.DataSource = dataView;
			cboUnit.DisplayMember = "UnitName";
			cboUnit.ValueMember = "UnitID";
		}
	}

	private void cboRoot_ValueChanged(object sender, EventArgs e)
	{
	}

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void rbStockLevelsForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridStockLevels();
	}

	private void FillGridPrices()
	{
		dtItemPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtItemPrices.NewRow();
				dataRow["ItemPriceID"] = -1;
				dataRow["Selected"] = false;
				dataRow["ItemID"] = -1;
				dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
				dataRow["Price"] = 0;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = DBNull.Value;
				dtItemPrices.Rows.Add(dataRow);
			}
		}
		else
		{
			for (int j = 0; j < dtBranches.Rows.Count; j++)
			{
				for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
				{
					DataRow dataRow2 = dtItemPrices.NewRow();
					dataRow2["ItemPriceID"] = -1;
					dataRow2["Selected"] = false;
					dataRow2["ItemID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtItemPrices.Rows.Add(dataRow2);
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtItemPrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemPriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = "ـ";
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
		if (rbPriceForAllBranches.Checked)
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.1);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		}
		else
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.25);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.1);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.25);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
		}
	}

	private void FillGridStockLevels()
	{
		dtItemStockLevels.Rows.Clear();
		if (rbStockLevelsForAllBranches.Checked)
		{
			DataRow dataRow = dtItemStockLevels.NewRow();
			dataRow["ItemStockLevels"] = -1;
			dataRow["Selected"] = false;
			dataRow["ItemID"] = -1;
			dataRow["MinLevel"] = 0;
			dataRow["ReOrderLevel"] = 0;
			dataRow["MaxLevel"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtItemStockLevels.Rows.Add(dataRow);
		}
		else
		{
			for (int i = 0; i < dtBranches.Rows.Count; i++)
			{
				DataRow dataRow2 = dtItemStockLevels.NewRow();
				dataRow2["ItemStockLevels"] = -1;
				dataRow2["Selected"] = false;
				dataRow2["ItemID"] = -1;
				dataRow2["MinLevel"] = 0;
				dataRow2["ReOrderLevel"] = 0;
				dataRow2["MaxLevel"] = 0;
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = dtBranches.Rows[i]["BranchID"];
				dtItemStockLevels.Rows.Add(dataRow2);
			}
		}
		InitGridStockLevels();
	}

	private void InitGridStockLevels()
	{
		((UltraGridBase)ULGLevels).DataSource = dtItemStockLevels;
		GlobalFunctions.PrepareGrid(ULGLevels);
		((UltraGridBase)ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ItemStockLevels"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأدني" : "Min");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Header).Caption = (GlobalVariables.IsArabic ? "اعدة الطلب" : "Reorder");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأقصي" : "Max");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = "ـ";
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
		if (rbStockLevelsForAllBranches.Checked)
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.1);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		}
		else
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.2);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.2);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches2;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void chkEnforceBatchNo_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtValidityDays;
		bool visible = (((Control)(object)lblValidityDays).Visible = ((UltraToggleEditorBase)chkEnforceBatchNo).Checked);
		((Control)(object)obj).Visible = visible;
	}

	private void chkModUnit_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlUnit).Enabled = ((UltraToggleEditorBase)chkModUnit).Checked;
	}

	private void chkModAccounts_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlAccounts).Enabled = ((UltraToggleEditorBase)chkModAccounts).Checked;
	}

	private void chkModPrices_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlPrice).Enabled = ((UltraToggleEditorBase)chkModPrices).Checked;
	}

	private void chkModBatchNo_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlBatchNo).Enabled = ((UltraToggleEditorBase)chkModBatchNo).Checked;
	}

	private void chkModLevels_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlLevels).Enabled = ((UltraToggleEditorBase)chkModLevels).Checked;
	}

	private void chkModTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboTax).Enabled = ((UltraToggleEditorBase)chkModTax).Checked;
	}

	private void chkModGrowthFeesTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboGrowthFeesTax).Enabled = ((UltraToggleEditorBase)chkModGrowthFeesTax).Checked;
	}

	private void ModTableTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboTableTax).Enabled = ((UltraToggleEditorBase)chkModTableTax).Checked;
	}

	private void chkModStore_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboStore).Enabled = ((UltraToggleEditorBase)chkModStore).Checked;
	}

	private void chkModActive_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsActive).Enabled = ((UltraToggleEditorBase)chkModActive).Checked;
	}

	private void chkModHideFromReports_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkHideFromReports).Enabled = ((UltraToggleEditorBase)chkModHideFromReports).Checked;
	}

	private void chkModPurchasePrice_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkCanModPurchasePrice).Enabled = ((UltraToggleEditorBase)chkModPurchasePrice).Checked;
	}

	private void chkModCostCenter_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboCostCenter).Enabled = ((UltraToggleEditorBase)chkModCostCenter).Checked;
	}

	private void chkModProductionItem_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsProductionItem).Enabled = ((UltraToggleEditorBase)chkModProductionItem).Checked;
	}

	private void chkModSalseItem_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsSalesItem).Enabled = ((UltraToggleEditorBase)chkModSalseItem).Checked;
	}

	private void chkModPOS_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlPOS).Enabled = ((UltraToggleEditorBase)chkModPOS).Checked;
	}

	private void chkModEditPrice_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkCanModPrice).Enabled = ((UltraToggleEditorBase)chkModEditPrice).Checked;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void txtInt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void chkModXY_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtSphFrom;
		UltraTextEditor obj2 = txtSphTo;
		UltraTextEditor obj3 = txtCylFrom;
		bool flag = (((Control)(object)txtCylTo).Enabled = ((UltraToggleEditorBase)chkModXY).Checked);
		bool flag3 = (((Control)(object)obj3).Enabled = flag);
		bool enabled = (((Control)(object)obj2).Enabled = flag3);
		((Control)(object)obj).Enabled = enabled;
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID" || (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "Price" && !Convert.ToBoolean(((UltraGridBase)ULGPrices).ActiveRow.Cells["Selected"].Value)))
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void ULGLevels_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGLevels.ActiveCell.Column).Key == "BranchID" || (((KeyedSubObjectBase)ULGLevels.ActiveCell.Column).Key != "Selected" && !Convert.ToBoolean(((UltraGridBase)ULGLevels).ActiveRow.Cells["Selected"].Value)))
		{
			((GridItemBase)((UltraGridBase)ULGLevels).ActiveRow).Selected = true;
		}
	}

	private void chkModItemsTypes_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboItemsTypes).Enabled = ((UltraToggleEditorBase)chkModItemsTypes).Checked;
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
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected O, but got Unknown
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Expected O, but got Unknown
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Expected O, but got Unknown
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Expected O, but got Unknown
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Expected O, but got Unknown
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Expected O, but got Unknown
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Expected O, but got Unknown
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Expected O, but got Unknown
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Expected O, but got Unknown
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Expected O, but got Unknown
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Expected O, but got Unknown
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Expected O, but got Unknown
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Expected O, but got Unknown
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Expected O, but got Unknown
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Expected O, but got Unknown
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Expected O, but got Unknown
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Expected O, but got Unknown
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Expected O, but got Unknown
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Expected O, but got Unknown
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Expected O, but got Unknown
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Expected O, but got Unknown
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Expected O, but got Unknown
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Expected O, but got Unknown
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmUpdateGroupItems));
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
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		Appearance val57 = new Appearance();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		Appearance val61 = new Appearance();
		Appearance val62 = new Appearance();
		Appearance val63 = new Appearance();
		Appearance val64 = new Appearance();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		this.pnlPrice = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ULGPrices = new UltraGrid();
		this.ultraLabel2 = new UltraLabel();
		this.pnlAccounts = new UltraPanel();
		this.cboServiceAccount = new UltraComboEditor();
		this.lblServiceAccount = new UltraLabel();
		this.btnDepartmentIssueAccount = new UltraButton();
		this.btnServiceAccountSearch = new UltraButton();
		this.cboDepartmentIssueAccount = new UltraComboEditor();
		this.lblSalesAccount = new UltraLabel();
		this.lblDepartmentIssueAccount = new UltraLabel();
		this.cboSalesAccount = new UltraComboEditor();
		this.btnPurchaseReturnsAccount = new UltraButton();
		this.btnSalesAccount = new UltraButton();
		this.cboPurchaseReturnsAccount = new UltraComboEditor();
		this.lblSalesReturnsAccount = new UltraLabel();
		this.lblPurchaseReturnsAccount = new UltraLabel();
		this.cboSalesReturnsAccount = new UltraComboEditor();
		this.btnPurchaseAccount = new UltraButton();
		this.btnSalesReturnsAccount = new UltraButton();
		this.cboPurchaseAccount = new UltraComboEditor();
		this.lblCostOfSalesAccount = new UltraLabel();
		this.lblPurchaseAccount = new UltraLabel();
		this.cboCostOfSalesAccount = new UltraComboEditor();
		this.btnCostOfSalesAccount = new UltraButton();
		this.pnlLevels = new UltraPanel();
		this.rbStockLevelForEachBranch = new System.Windows.Forms.RadioButton();
		this.ULGLevels = new UltraGrid();
		this.rbStockLevelsForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraLabel1 = new UltraLabel();
		this.pnlUnit = new UltraPanel();
		this.cboUnitGroup = new UltraComboEditor();
		this.lblUnitGroup = new UltraLabel();
		this.cboUnit = new UltraComboEditor();
		this.lblUnit = new UltraLabel();
		this.pnlBatchNo = new UltraPanel();
		this.chkEnforceBatchNo = new UltraCheckEditor();
		this.txtValidityDays = new UltraTextEditor();
		this.lblValidityDays = new UltraLabel();
		this.pnlPOS = new UltraPanel();
		this.cboPrinterName = new UltraComboEditor();
		this.lblPrinterName = new UltraLabel();
		this.txtPrepareTime = new UltraTextEditor();
		this.lblPrepareTime = new UltraLabel();
		this.cboItemsTypes = new UltraComboEditor();
		this.lblItemsTypes = new UltraLabel();
		this.lblRoot = new UltraLabel();
		this.cboGroup = new UltraComboEditor();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.chkIsProductionItem = new UltraCheckEditor();
		this.chkIsSalesItem = new UltraCheckEditor();
		this.chkModAccounts = new UltraCheckEditor();
		this.chkModLevels = new UltraCheckEditor();
		this.chkModPrices = new UltraCheckEditor();
		this.chkModStore = new UltraCheckEditor();
		this.chkModBatchNo = new UltraCheckEditor();
		this.chkModUnit = new UltraCheckEditor();
		this.chkModActive = new UltraCheckEditor();
		this.chkModSalseItem = new UltraCheckEditor();
		this.chkModProductionItem = new UltraCheckEditor();
		this.chkModTax = new UltraCheckEditor();
		this.chkCanModPrice = new UltraCheckEditor();
		this.chkModEditPrice = new UltraCheckEditor();
		this.chkModPOS = new UltraCheckEditor();
		this.chkModXY = new UltraCheckEditor();
		this.txtSphFrom = new UltraTextEditor();
		this.lblSph = new UltraLabel();
		this.lblCyl = new UltraLabel();
		this.txtCylFrom = new UltraTextEditor();
		this.txtSphTo = new UltraTextEditor();
		this.txtCylTo = new UltraTextEditor();
		this.chkModItemsTypes = new UltraCheckEditor();
		this.chkHideFromReports = new UltraCheckEditor();
		this.chkModHideFromReports = new UltraCheckEditor();
		this.cboCostCenter = new UltraComboEditor();
		this.lblCostCenter = new UltraLabel();
		this.chkModCostCenter = new UltraCheckEditor();
		this.chkModPurchasePrice = new UltraCheckEditor();
		this.chkCanModPurchasePrice = new UltraCheckEditor();
		this.cboGrowthFeesTax = new UltraComboEditor();
		this.lblGrowthFeesTax = new UltraLabel();
		this.chkModGrowthFeesTax = new UltraCheckEditor();
		this.cboTableTax = new UltraComboEditor();
		this.lblTableTax = new UltraLabel();
		this.chkModTableTax = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlPrice).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlAccounts).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentIssueAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseReturnsAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostOfSalesAccount).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlLevels).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlUnit).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlBatchNo).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceBatchNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlPOS).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrepareTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemsTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModLevels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBatchNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalseItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModProductionItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEditPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPOS).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModXY).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSphFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSphTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModItemsTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHideFromReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModHideFromReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPurchasePrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPurchasePrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGrowthFeesTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModGrowthFeesTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTableTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTableTax).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlPrice, "pnlPrice");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance74");
		this.pnlPrice.Appearance = (AppearanceBase)(object)val;
		this.pnlPrice.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlPrice.ClientArea, "pnlPrice.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.pnlPrice).Name = "pnlPrice";
		((UltraControlBase)this.pnlPrice).UseAppStyling = false;
		resources.ApplyResources(this.rbPriceForEachBranches, "rbPriceForEachBranches");
		this.rbPriceForEachBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForEachBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForEachBranches.Name = "rbPriceForEachBranches";
		this.rbPriceForEachBranches.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbPriceForAllBranches, "rbPriceForAllBranches");
		this.rbPriceForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForAllBranches.Checked = true;
		this.rbPriceForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForAllBranches.Name = "rbPriceForAllBranches";
		this.rbPriceForAllBranches.TabStop = true;
		this.rbPriceForAllBranches.UseVisualStyleBackColor = false;
		this.rbPriceForAllBranches.CheckedChanged += new System.EventHandler(rbPriceForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val2).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val2, "appearance2");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val3;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance8");
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.pnlAccounts, "pnlAccounts");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance1");
		this.pnlAccounts.Appearance = (AppearanceBase)(object)val12;
		this.pnlAccounts.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlAccounts.ClientArea, "pnlAccounts.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboServiceAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.btnCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.pnlAccounts).Name = "pnlAccounts";
		((UltraControlBase)this.pnlAccounts).UseAppStyling = false;
		resources.ApplyResources(this.cboServiceAccount, "cboServiceAccount");
		this.cboServiceAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).Name = "cboServiceAccount";
		resources.ApplyResources(this.lblServiceAccount, "lblServiceAccount");
		this.lblServiceAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceAccount).Name = "lblServiceAccount";
		((ControlBase)this.lblServiceAccount).WrapText = false;
		resources.ApplyResources(this.btnDepartmentIssueAccount, "btnDepartmentIssueAccount");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance75");
		((ControlBase)this.btnDepartmentIssueAccount).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnDepartmentIssueAccount).Name = "btnDepartmentIssueAccount";
		resources.ApplyResources(this.btnServiceAccountSearch, "btnServiceAccountSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance76");
		((ControlBase)this.btnServiceAccountSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Name = "btnServiceAccountSearch";
		resources.ApplyResources(this.cboDepartmentIssueAccount, "cboDepartmentIssueAccount");
		this.cboDepartmentIssueAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboDepartmentIssueAccount).Name = "cboDepartmentIssueAccount";
		resources.ApplyResources(this.lblSalesAccount, "lblSalesAccount");
		this.lblSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesAccount).Name = "lblSalesAccount";
		((ControlBase)this.lblSalesAccount).WrapText = false;
		resources.ApplyResources(this.lblDepartmentIssueAccount, "lblDepartmentIssueAccount");
		this.lblDepartmentIssueAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentIssueAccount).Name = "lblDepartmentIssueAccount";
		((ControlBase)this.lblDepartmentIssueAccount).WrapText = false;
		resources.ApplyResources(this.cboSalesAccount, "cboSalesAccount");
		this.cboSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesAccount).Name = "cboSalesAccount";
		resources.ApplyResources(this.btnPurchaseReturnsAccount, "btnPurchaseReturnsAccount");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance77");
		((ControlBase)this.btnPurchaseReturnsAccount).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnPurchaseReturnsAccount).Name = "btnPurchaseReturnsAccount";
		resources.ApplyResources(this.btnSalesAccount, "btnSalesAccount");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance78");
		((ControlBase)this.btnSalesAccount).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnSalesAccount).Name = "btnSalesAccount";
		resources.ApplyResources(this.cboPurchaseReturnsAccount, "cboPurchaseReturnsAccount");
		this.cboPurchaseReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboPurchaseReturnsAccount).Name = "cboPurchaseReturnsAccount";
		resources.ApplyResources(this.lblSalesReturnsAccount, "lblSalesReturnsAccount");
		this.lblSalesReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount).Name = "lblSalesReturnsAccount";
		((ControlBase)this.lblSalesReturnsAccount).WrapText = false;
		resources.ApplyResources(this.lblPurchaseReturnsAccount, "lblPurchaseReturnsAccount");
		this.lblPurchaseReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseReturnsAccount).Name = "lblPurchaseReturnsAccount";
		((ControlBase)this.lblPurchaseReturnsAccount).WrapText = false;
		resources.ApplyResources(this.cboSalesReturnsAccount, "cboSalesReturnsAccount");
		this.cboSalesReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccount).Name = "cboSalesReturnsAccount";
		resources.ApplyResources(this.btnPurchaseAccount, "btnPurchaseAccount");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance79");
		((ControlBase)this.btnPurchaseAccount).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnPurchaseAccount).Name = "btnPurchaseAccount";
		resources.ApplyResources(this.btnSalesReturnsAccount, "btnSalesReturnsAccount");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance80");
		((ControlBase)this.btnSalesReturnsAccount).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccount).Name = "btnSalesReturnsAccount";
		resources.ApplyResources(this.cboPurchaseAccount, "cboPurchaseAccount");
		this.cboPurchaseAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboPurchaseAccount).Name = "cboPurchaseAccount";
		resources.ApplyResources(this.lblCostOfSalesAccount, "lblCostOfSalesAccount");
		this.lblCostOfSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostOfSalesAccount).Name = "lblCostOfSalesAccount";
		((ControlBase)this.lblCostOfSalesAccount).WrapText = false;
		resources.ApplyResources(this.lblPurchaseAccount, "lblPurchaseAccount");
		this.lblPurchaseAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseAccount).Name = "lblPurchaseAccount";
		((ControlBase)this.lblPurchaseAccount).WrapText = false;
		resources.ApplyResources(this.cboCostOfSalesAccount, "cboCostOfSalesAccount");
		this.cboCostOfSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCostOfSalesAccount).Name = "cboCostOfSalesAccount";
		resources.ApplyResources(this.btnCostOfSalesAccount, "btnCostOfSalesAccount");
		((AppearanceBase)val19).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val19, "appearance81");
		((ControlBase)this.btnCostOfSalesAccount).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.btnCostOfSalesAccount).Name = "btnCostOfSalesAccount";
		resources.ApplyResources(this.pnlLevels, "pnlLevels");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val20, "appearance12");
		this.pnlLevels.Appearance = (AppearanceBase)(object)val20;
		this.pnlLevels.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlLevels.ClientArea, "pnlLevels.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).Controls.Add(this.rbStockLevelForEachBranch);
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ULGLevels);
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).Controls.Add(this.rbStockLevelsForAllBranches);
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.pnlLevels).Name = "pnlLevels";
		((UltraControlBase)this.pnlLevels).UseAppStyling = false;
		resources.ApplyResources(this.rbStockLevelForEachBranch, "rbStockLevelForEachBranch");
		this.rbStockLevelForEachBranch.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelForEachBranch.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelForEachBranch.Name = "rbStockLevelForEachBranch";
		this.rbStockLevelForEachBranch.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.ULGLevels, "ULGLevels");
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val21, "appearance21");
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val21;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val22;
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val23).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val25, "appearance25");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val27, "appearance27");
		((AppearanceBase)val27).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val30;
		((System.Windows.Forms.Control)(object)this.ULGLevels).Name = "ULGLevels";
		this.ULGLevels.AfterEnterEditMode += new System.EventHandler(ULGLevels_AfterEnterEditMode);
		resources.ApplyResources(this.rbStockLevelsForAllBranches, "rbStockLevelsForAllBranches");
		this.rbStockLevelsForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelsForAllBranches.Checked = true;
		this.rbStockLevelsForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelsForAllBranches.Name = "rbStockLevelsForAllBranches";
		this.rbStockLevelsForAllBranches.TabStop = true;
		this.rbStockLevelsForAllBranches.UseVisualStyleBackColor = false;
		this.rbStockLevelsForAllBranches.CheckedChanged += new System.EventHandler(rbStockLevelsForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.pnlUnit, "pnlUnit");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val31, "appearance20");
		this.pnlUnit.Appearance = (AppearanceBase)(object)val31;
		this.pnlUnit.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlUnit.ClientArea, "pnlUnit.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		((System.Windows.Forms.Control)(object)this.pnlUnit).Name = "pnlUnit";
		((UltraControlBase)this.pnlUnit).UseAppStyling = false;
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		this.cboUnitGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).Nullable = false;
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.cboUnit, "cboUnit");
		this.cboUnit.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		((TextEditorControlBase)this.cboUnit).Nullable = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.pnlBatchNo, "pnlBatchNo");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.Black;
		resources.ApplyResources(val32, "appearance31");
		this.pnlBatchNo.Appearance = (AppearanceBase)(object)val32;
		this.pnlBatchNo.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlBatchNo.ClientArea, "pnlBatchNo.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceBatchNo);
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtValidityDays);
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblValidityDays);
		((System.Windows.Forms.Control)(object)this.pnlBatchNo).Name = "pnlBatchNo";
		((UltraControlBase)this.pnlBatchNo).UseAppStyling = false;
		resources.ApplyResources(this.chkEnforceBatchNo, "chkEnforceBatchNo");
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance82");
		((UltraToggleEditorBase)this.chkEnforceBatchNo).Appearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.chkEnforceBatchNo).Name = "chkEnforceBatchNo";
		((UltraToggleEditorBase)this.chkEnforceBatchNo).CheckedChanged += new System.EventHandler(chkEnforceBatchNo_CheckedChanged);
		resources.ApplyResources(this.txtValidityDays, "txtValidityDays");
		((System.Windows.Forms.Control)(object)this.txtValidityDays).Name = "txtValidityDays";
		((System.Windows.Forms.Control)(object)this.txtValidityDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblValidityDays, "lblValidityDays");
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance83");
		((ControlBase)this.lblValidityDays).Appearance = (AppearanceBase)(object)val34;
		this.lblValidityDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValidityDays).Name = "lblValidityDays";
		((ControlBase)this.lblValidityDays).WrapText = false;
		resources.ApplyResources(this.pnlPOS, "pnlPOS");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val35, "appearance32");
		this.pnlPOS.Appearance = (AppearanceBase)(object)val35;
		this.pnlPOS.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlPOS.ClientArea, "pnlPOS.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboPrinterName);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrinterName);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrepareTime);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrepareTime);
		((System.Windows.Forms.Control)(object)this.pnlPOS).Name = "pnlPOS";
		((UltraControlBase)this.pnlPOS).UseAppStyling = false;
		resources.ApplyResources(this.cboPrinterName, "cboPrinterName");
		((System.Windows.Forms.Control)(object)this.cboPrinterName).Name = "cboPrinterName";
		resources.ApplyResources(this.lblPrinterName, "lblPrinterName");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance84");
		((ControlBase)this.lblPrinterName).Appearance = (AppearanceBase)(object)val36;
		this.lblPrinterName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrinterName).Name = "lblPrinterName";
		resources.ApplyResources(this.txtPrepareTime, "txtPrepareTime");
		((System.Windows.Forms.Control)(object)this.txtPrepareTime).Name = "txtPrepareTime";
		((System.Windows.Forms.Control)(object)this.txtPrepareTime).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblPrepareTime, "lblPrepareTime");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance85");
		((ControlBase)this.lblPrepareTime).Appearance = (AppearanceBase)(object)val37;
		this.lblPrepareTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrepareTime).Name = "lblPrepareTime";
		resources.ApplyResources(this.cboItemsTypes, "cboItemsTypes");
		((System.Windows.Forms.Control)(object)this.cboItemsTypes).Name = "cboItemsTypes";
		resources.ApplyResources(this.lblItemsTypes, "lblItemsTypes");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance35");
		((ControlBase)this.lblItemsTypes).Appearance = (AppearanceBase)(object)val38;
		this.lblItemsTypes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsTypes).Name = "lblItemsTypes";
		resources.ApplyResources(this.lblRoot, "lblRoot");
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		((TextEditorControlBase)this.cboGroup).ValueChanged += new System.EventHandler(cboRoot_ValueChanged);
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((TextEditorControlBase)this.cboStore).Nullable = false;
		resources.ApplyResources(this.lblStore, "lblStore");
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val39).Image = resources.GetObject("appearance86.Image");
		resources.ApplyResources(val39, "appearance86");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val39;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val40).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val40).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val40, "appearance87");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val40;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val41).Image = resources.GetObject("appearance88.Image");
		resources.ApplyResources(val41, "appearance88");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val41;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val42).Image = resources.GetObject("appearance89.Image");
		resources.ApplyResources(val42, "appearance89");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val42;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val43).Image = resources.GetObject("appearance90.Image");
		resources.ApplyResources(val43, "appearance90");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val43;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val44).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val44, "appearance91");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val44;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val45, "appearance92");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val45;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.lblTax, "lblTax");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val46, "appearance93");
		((ControlBase)this.lblTax).Appearance = (AppearanceBase)(object)val46;
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		resources.ApplyResources(this.chkIsProductionItem, "chkIsProductionItem");
		((AppearanceBase)val47).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val47, "appearance94");
		((UltraToggleEditorBase)this.chkIsProductionItem).Appearance = (AppearanceBase)(object)val47;
		((System.Windows.Forms.Control)(object)this.chkIsProductionItem).Name = "chkIsProductionItem";
		resources.ApplyResources(this.chkIsSalesItem, "chkIsSalesItem");
		((AppearanceBase)val48).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val48, "appearance95");
		((UltraToggleEditorBase)this.chkIsSalesItem).Appearance = (AppearanceBase)(object)val48;
		((System.Windows.Forms.Control)(object)this.chkIsSalesItem).Name = "chkIsSalesItem";
		resources.ApplyResources(this.chkModAccounts, "chkModAccounts");
		((AppearanceBase)val49).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val49, "appearance96");
		((UltraToggleEditorBase)this.chkModAccounts).Appearance = (AppearanceBase)(object)val49;
		((System.Windows.Forms.Control)(object)this.chkModAccounts).Name = "chkModAccounts";
		((UltraToggleEditorBase)this.chkModAccounts).CheckedChanged += new System.EventHandler(chkModAccounts_CheckedChanged);
		resources.ApplyResources(this.chkModLevels, "chkModLevels");
		((AppearanceBase)val50).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val50, "appearance97");
		((UltraToggleEditorBase)this.chkModLevels).Appearance = (AppearanceBase)(object)val50;
		((System.Windows.Forms.Control)(object)this.chkModLevels).Name = "chkModLevels";
		((UltraToggleEditorBase)this.chkModLevels).CheckedChanged += new System.EventHandler(chkModLevels_CheckedChanged);
		resources.ApplyResources(this.chkModPrices, "chkModPrices");
		((AppearanceBase)val51).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val51, "appearance98");
		((UltraToggleEditorBase)this.chkModPrices).Appearance = (AppearanceBase)(object)val51;
		((System.Windows.Forms.Control)(object)this.chkModPrices).Name = "chkModPrices";
		((UltraToggleEditorBase)this.chkModPrices).CheckedChanged += new System.EventHandler(chkModPrices_CheckedChanged);
		resources.ApplyResources(this.chkModStore, "chkModStore");
		((AppearanceBase)val52).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val52, "appearance99");
		((UltraToggleEditorBase)this.chkModStore).Appearance = (AppearanceBase)(object)val52;
		((System.Windows.Forms.Control)(object)this.chkModStore).Name = "chkModStore";
		((UltraToggleEditorBase)this.chkModStore).CheckedChanged += new System.EventHandler(chkModStore_CheckedChanged);
		resources.ApplyResources(this.chkModBatchNo, "chkModBatchNo");
		((AppearanceBase)val53).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val53, "appearance100");
		((UltraToggleEditorBase)this.chkModBatchNo).Appearance = (AppearanceBase)(object)val53;
		((System.Windows.Forms.Control)(object)this.chkModBatchNo).Name = "chkModBatchNo";
		((UltraToggleEditorBase)this.chkModBatchNo).CheckedChanged += new System.EventHandler(chkModBatchNo_CheckedChanged);
		resources.ApplyResources(this.chkModUnit, "chkModUnit");
		((AppearanceBase)val54).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val54, "appearance101");
		((UltraToggleEditorBase)this.chkModUnit).Appearance = (AppearanceBase)(object)val54;
		((System.Windows.Forms.Control)(object)this.chkModUnit).Name = "chkModUnit";
		((UltraToggleEditorBase)this.chkModUnit).CheckedChanged += new System.EventHandler(chkModUnit_CheckedChanged);
		resources.ApplyResources(this.chkModActive, "chkModActive");
		((AppearanceBase)val55).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val55, "appearance102");
		((UltraToggleEditorBase)this.chkModActive).Appearance = (AppearanceBase)(object)val55;
		((System.Windows.Forms.Control)(object)this.chkModActive).Name = "chkModActive";
		((UltraToggleEditorBase)this.chkModActive).CheckedChanged += new System.EventHandler(chkModActive_CheckedChanged);
		resources.ApplyResources(this.chkModSalseItem, "chkModSalseItem");
		((AppearanceBase)val56).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val56, "appearance103");
		((UltraToggleEditorBase)this.chkModSalseItem).Appearance = (AppearanceBase)(object)val56;
		((System.Windows.Forms.Control)(object)this.chkModSalseItem).Name = "chkModSalseItem";
		((UltraToggleEditorBase)this.chkModSalseItem).CheckedChanged += new System.EventHandler(chkModSalseItem_CheckedChanged);
		resources.ApplyResources(this.chkModProductionItem, "chkModProductionItem");
		((AppearanceBase)val57).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val57, "appearance104");
		((UltraToggleEditorBase)this.chkModProductionItem).Appearance = (AppearanceBase)(object)val57;
		((System.Windows.Forms.Control)(object)this.chkModProductionItem).Name = "chkModProductionItem";
		((UltraToggleEditorBase)this.chkModProductionItem).CheckedChanged += new System.EventHandler(chkModProductionItem_CheckedChanged);
		resources.ApplyResources(this.chkModTax, "chkModTax");
		((AppearanceBase)val58).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val58, "appearance105");
		((UltraToggleEditorBase)this.chkModTax).Appearance = (AppearanceBase)(object)val58;
		((System.Windows.Forms.Control)(object)this.chkModTax).Name = "chkModTax";
		((UltraToggleEditorBase)this.chkModTax).CheckedChanged += new System.EventHandler(chkModTax_CheckedChanged);
		resources.ApplyResources(this.chkCanModPrice, "chkCanModPrice");
		((AppearanceBase)val59).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val59, "appearance106");
		((UltraToggleEditorBase)this.chkCanModPrice).Appearance = (AppearanceBase)(object)val59;
		((System.Windows.Forms.Control)(object)this.chkCanModPrice).Name = "chkCanModPrice";
		resources.ApplyResources(this.chkModEditPrice, "chkModEditPrice");
		((AppearanceBase)val60).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val60, "appearance107");
		((UltraToggleEditorBase)this.chkModEditPrice).Appearance = (AppearanceBase)(object)val60;
		((System.Windows.Forms.Control)(object)this.chkModEditPrice).Name = "chkModEditPrice";
		((UltraToggleEditorBase)this.chkModEditPrice).CheckedChanged += new System.EventHandler(chkModEditPrice_CheckedChanged);
		resources.ApplyResources(this.chkModPOS, "chkModPOS");
		((AppearanceBase)val61).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val61, "appearance108");
		((UltraToggleEditorBase)this.chkModPOS).Appearance = (AppearanceBase)(object)val61;
		((System.Windows.Forms.Control)(object)this.chkModPOS).Name = "chkModPOS";
		((UltraToggleEditorBase)this.chkModPOS).CheckedChanged += new System.EventHandler(chkModPOS_CheckedChanged);
		resources.ApplyResources(this.chkModXY, "chkModXY");
		((AppearanceBase)val62).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val62, "appearance109");
		((UltraToggleEditorBase)this.chkModXY).Appearance = (AppearanceBase)(object)val62;
		((System.Windows.Forms.Control)(object)this.chkModXY).Name = "chkModXY";
		((UltraToggleEditorBase)this.chkModXY).CheckedChanged += new System.EventHandler(chkModXY_CheckedChanged);
		resources.ApplyResources(this.txtSphFrom, "txtSphFrom");
		((System.Windows.Forms.Control)(object)this.txtSphFrom).Name = "txtSphFrom";
		((System.Windows.Forms.Control)(object)this.txtSphFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSph, "lblSph");
		this.lblSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSph).Name = "lblSph";
		((ControlBase)this.lblSph).WrapText = false;
		resources.ApplyResources(this.lblCyl, "lblCyl");
		this.lblCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCyl).Name = "lblCyl";
		((ControlBase)this.lblCyl).WrapText = false;
		resources.ApplyResources(this.txtCylFrom, "txtCylFrom");
		((System.Windows.Forms.Control)(object)this.txtCylFrom).Name = "txtCylFrom";
		((System.Windows.Forms.Control)(object)this.txtCylFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSphTo, "txtSphTo");
		((System.Windows.Forms.Control)(object)this.txtSphTo).Name = "txtSphTo";
		((System.Windows.Forms.Control)(object)this.txtSphTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtCylTo, "txtCylTo");
		((System.Windows.Forms.Control)(object)this.txtCylTo).Name = "txtCylTo";
		((System.Windows.Forms.Control)(object)this.txtCylTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkModItemsTypes, "chkModItemsTypes");
		((AppearanceBase)val63).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val63, "appearance110");
		((UltraToggleEditorBase)this.chkModItemsTypes).Appearance = (AppearanceBase)(object)val63;
		((System.Windows.Forms.Control)(object)this.chkModItemsTypes).Name = "chkModItemsTypes";
		((UltraToggleEditorBase)this.chkModItemsTypes).CheckedChanged += new System.EventHandler(chkModItemsTypes_CheckedChanged);
		resources.ApplyResources(this.chkHideFromReports, "chkHideFromReports");
		((AppearanceBase)val64).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val64, "appearance111");
		((UltraToggleEditorBase)this.chkHideFromReports).Appearance = (AppearanceBase)(object)val64;
		((System.Windows.Forms.Control)(object)this.chkHideFromReports).Name = "chkHideFromReports";
		resources.ApplyResources(this.chkModHideFromReports, "chkModHideFromReports");
		((AppearanceBase)val65).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val65, "appearance112");
		((UltraToggleEditorBase)this.chkModHideFromReports).Appearance = (AppearanceBase)(object)val65;
		((System.Windows.Forms.Control)(object)this.chkModHideFromReports).Name = "chkModHideFromReports";
		((UltraToggleEditorBase)this.chkModHideFromReports).CheckedChanged += new System.EventHandler(chkModHideFromReports_CheckedChanged);
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		((AppearanceBase)val66).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val66).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val66, "appearance113");
		((ControlBase)this.lblCostCenter).Appearance = (AppearanceBase)(object)val66;
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		resources.ApplyResources(this.chkModCostCenter, "chkModCostCenter");
		((AppearanceBase)val67).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val67, "appearance114");
		((UltraToggleEditorBase)this.chkModCostCenter).Appearance = (AppearanceBase)(object)val67;
		((System.Windows.Forms.Control)(object)this.chkModCostCenter).Name = "chkModCostCenter";
		((UltraToggleEditorBase)this.chkModCostCenter).CheckedChanged += new System.EventHandler(chkModCostCenter_CheckedChanged);
		resources.ApplyResources(this.chkModPurchasePrice, "chkModPurchasePrice");
		((AppearanceBase)val68).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val68, "appearance115");
		((UltraToggleEditorBase)this.chkModPurchasePrice).Appearance = (AppearanceBase)(object)val68;
		((System.Windows.Forms.Control)(object)this.chkModPurchasePrice).Name = "chkModPurchasePrice";
		((UltraToggleEditorBase)this.chkModPurchasePrice).CheckedChanged += new System.EventHandler(chkModPurchasePrice_CheckedChanged);
		resources.ApplyResources(this.chkCanModPurchasePrice, "chkCanModPurchasePrice");
		((AppearanceBase)val69).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val69, "appearance116");
		((UltraToggleEditorBase)this.chkCanModPurchasePrice).Appearance = (AppearanceBase)(object)val69;
		((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice).Name = "chkCanModPurchasePrice";
		resources.ApplyResources(this.cboGrowthFeesTax, "cboGrowthFeesTax");
		((System.Windows.Forms.Control)(object)this.cboGrowthFeesTax).Name = "cboGrowthFeesTax";
		resources.ApplyResources(this.lblGrowthFeesTax, "lblGrowthFeesTax");
		((AppearanceBase)val70).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val70).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val70, "appearance117");
		((ControlBase)this.lblGrowthFeesTax).Appearance = (AppearanceBase)(object)val70;
		this.lblGrowthFeesTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthFeesTax).Name = "lblGrowthFeesTax";
		resources.ApplyResources(this.chkModGrowthFeesTax, "chkModGrowthFeesTax");
		((AppearanceBase)val71).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val71, "appearance118");
		((UltraToggleEditorBase)this.chkModGrowthFeesTax).Appearance = (AppearanceBase)(object)val71;
		((System.Windows.Forms.Control)(object)this.chkModGrowthFeesTax).Name = "chkModGrowthFeesTax";
		((UltraToggleEditorBase)this.chkModGrowthFeesTax).CheckedChanged += new System.EventHandler(chkModGrowthFeesTax_CheckedChanged);
		resources.ApplyResources(this.cboTableTax, "cboTableTax");
		((System.Windows.Forms.Control)(object)this.cboTableTax).Name = "cboTableTax";
		resources.ApplyResources(this.lblTableTax, "lblTableTax");
		((AppearanceBase)val72).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val72).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val72, "appearance119");
		((ControlBase)this.lblTableTax).Appearance = (AppearanceBase)(object)val72;
		this.lblTableTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTableTax).Name = "lblTableTax";
		resources.ApplyResources(this.chkModTableTax, "chkModTableTax");
		((AppearanceBase)val73).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val73, "appearance120");
		((UltraToggleEditorBase)this.chkModTableTax).Appearance = (AppearanceBase)(object)val73;
		((System.Windows.Forms.Control)(object)this.chkModTableTax).Name = "chkModTableTax";
		((UltraToggleEditorBase)this.chkModTableTax).CheckedChanged += new System.EventHandler(ModTableTax_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemsTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemsTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlPOS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCylTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCylFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSphTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSphFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSph);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModTableTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModGrowthFeesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPurchasePrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModEditPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModProductionItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModSalseItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModHideFromReports);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModXY);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModItemsTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPOS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPrices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModLevels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlLevels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHideFromReports);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsProductionItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTableTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrowthFeesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTableTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGrowthFeesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Name = "frmUpdateGroupItems";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGrowthFeesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTableTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrowthFeesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTableTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsProductionItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHideFromReports, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlLevels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModLevels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPrices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPOS, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModItemsTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModXY, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModHideFromReports, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModSalseItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModProductionItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModEditPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPurchasePrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModGrowthFeesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModTableTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSphFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSphTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCylFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCylTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlPOS, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemsTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemsTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlPrice).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlAccounts).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentIssueAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseReturnsAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostOfSalesAccount).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlLevels.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlLevels).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlUnit.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlUnit).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlBatchNo.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlBatchNo).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.chkEnforceBatchNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlPOS).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrepareTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemsTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModLevels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBatchNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalseItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModProductionItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEditPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPOS).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModXY).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSphFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSphTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModItemsTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHideFromReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModHideFromReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPurchasePrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPurchasePrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGrowthFeesTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModGrowthFeesTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTableTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTableTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
