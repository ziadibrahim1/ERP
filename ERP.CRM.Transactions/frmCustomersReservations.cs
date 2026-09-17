using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CRM;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.Transactions;

public class frmCustomersReservations : frmHeaderDetails
{
	private DataTable dtPOSDefaultStore;

	private DataTable dtPriceType;

	private DataTable dtItemPrices;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtCurrency;

	private DataTable dtReports;

	private DataTable dtSalesMan;

	private DataTable dtStores;

	private DataTable dtBatchs;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtTaxes;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlDetailsTaxs = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private int rowIndex = -1;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblBalance;

	public UltraButton btnClientBalance;

	private UltraTextEditor txtBranchBalance;

	public UltraButton btnSalesManSearch;

	private UltraComboEditor cboSalesMan;

	private UltraLabel lblSalesMan;

	public UltraButton btnClientSearch;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblClient;

	private UltraComboEditor cboClient;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblGrowthFees;

	private UltraTextEditor txtGrowthFees;

	private UltraLabel lblStampValue;

	private UltraTextEditor txtStampValue;

	private UltraLabel lblCommercialTax;

	private UltraTextEditor txtCommercialTax;

	private UltraLabel lblDiscAfterTaxRatio;

	private UltraTextEditor txtDiscAfterTaxRatio;

	private UltraLabel lblDiscAfterTaxValue;

	private UltraTextEditor txtDiscAfterTaxValue;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraCheckEditor chkIsCash;

	private UltraDateTimeEditor dtpReceivingDate;

	private UltraLabel lblReceivingDate;

	private UltraLabel lblSerial;

	private UltraTextEditor txtSerial;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private UltraCheckEditor chkTax;

	private UltraComboEditor cboTax;

	public UltraButton btnPriceTypeSearch;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	public frmCustomersReservations()
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CRM_CustomersReservations";
		IDCol = "CustomerReservationID";
		NoCol = "CustomerReservationNo";
		DateCol = "CustomerReservationDate";
	}

	public frmCustomersReservations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		AutoPrint = false;
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		((Control)(object)txtSerial).Visible = UsingBatchNoAndValidityPeriod;
		((Control)(object)lblSerial).Visible = UsingBatchNoAndValidityPeriod;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxes.Rows.Count; num++)
		{
			vlDetailsTaxs.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		FillCurrencyDropDown();
		dtDetails = CustomersReservationsDetails.SelectByCustomerReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerReservationDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if (ULGData.ActiveCell != null && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		CalcTotalQty();
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a3: Expected O, but got Unknown
		//IL_10b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bb: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : dataRow["DefaultStoreID"]);
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + e.Cell.Value.ToString()) : (" ItemID = " + e.Cell.Value.ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj2.Value = value;
				}
				CalculateGoss();
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (UsingBatchNoAndValidityPeriod)
			{
				if (num != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			if (AutomaticlyAddItemTaxToSalesInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
			int num2 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ColorID"].Value = 1;
				e.Cell.Row.Cells["ColorID"].ValueList = null;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = 1;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "UnitID" && e.Cell.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			if (e.Cell.Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				if (bool.Parse(dataRow2["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow[] array2 = dtItemPrices.Select(" UnitID = " + e.Cell.Value.ToString() + " and ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
					if (array2 != null && array2.Length != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array2[0]["Price"].ToString()) - decimal.Parse(array2[0]["Price"].ToString()) * decimal.Parse(array2[0]["DiscountPercentage"].ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					}
					else
					{
						UltraGridCell obj4 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
						obj4.Value = value;
					}
					CalculateGoss();
					CalculateRow(e.Cell.Row);
					CalculateTotalsTax();
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "StoreID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			((UltraGridBase)ULGData).UpdateData();
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"] != DBNull.Value)
		{
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow3 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj6 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num3);
				obj6.Value = value;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				int num4 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num4 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num4);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				}
				else
				{
					e.Cell.Row.Cells["UnitID"].ValueList = null;
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = 1;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
				}
			}
		}
		CalcTotalQty();
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = CustomersReservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["CustomerReservationNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["CustomerReservationDate"];
			dtpReceivingDate.Value = (DateTime)drMaster["ReceivingDate"];
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboSalesMan).Value = drMaster["EmployeeSubAccountID"];
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			((UltraToggleEditorBase)chkIsCash).Checked = bool.Parse(drMaster["IsCash"].ToString());
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtStampValue).Text = decimal.Parse(drMaster["StampsValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrowthFees).Text = decimal.Parse(drMaster["GrowthFees"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), GlobalVariables.LocalCurrencyID.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = CustomersReservationsDetails.SelectByCustomerReservationID(drMaster["CustomerReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			CalcTotalQty();
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
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtStampValue).ValueChanged += txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtGrowthFees).ValueChanged += txtAdditionalValues_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		((EditorButtonControlBase)dtpReceivingDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStampValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGrowthFees).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)btnSalesManSearch).Visible = !NavMode;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode;
		((Control)(object)chkIsCash).Enabled = !NavMode;
		((Control)(object)txtBranchBalance).Visible = !NavMode;
		((Control)(object)lblBalance).Visible = !NavMode;
		((Control)(object)btnClientBalance).Visible = !NavMode;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
		((Control)(object)chkTax).Visible = !NavMode;
		((Control)(object)cboTax).Visible = !NavMode;
		((Control)(object)txtBarCode).Visible = !NavMode;
		((Control)(object)txtSerial).Visible = !NavMode;
		((Control)(object)lblBarCode).Visible = !NavMode;
		((Control)(object)lblSerial).Visible = !NavMode;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
			GlobalFunctions.FillCombo(cboStore, dataTable, "StoreID", "StoreName");
		}
		else
		{
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
			GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		}
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			DataTable dataTable2 = dataView2.ToTable();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
			}
		}
		if (Updating)
		{
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
			{
				if (((UltraGridBase)ULGData).Rows[m].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[m].Cells["ItemID"].Value.ToString())[0];
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
					((UltraGridBase)ULGData).Rows[m].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[m].Cells["BatchID"].Value = DBNull.Value;
					}
				}
				if (!bool.Parse(dataRow["IsService"].ToString()))
				{
					int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[m].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList = getUnitsValueList(unitTypeID);
					((UltraGridBase)ULGData).Rows[m].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[m].Cells["UnitID"].Value = DBNull.Value;
					}
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[m].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[m].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[m].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[m].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[m].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[m].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((Adding || Updating) ? 6 : 2);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpReceivingDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? CustomersReservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkIsCash).Checked = false;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		cboTax.SelectedIndex = -1;
		cboStore.SelectedIndex = -1;
		((UltraToggleEditorBase)chkStore).Checked = false;
		((UltraToggleEditorBase)chkTax).Checked = false;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtExchangeRate).Text = "0";
		cboSalesMan.SelectedIndex = -1;
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtDiscAfterTaxValue).Text = "0";
		((Control)(object)txtDiscAfterTaxRatio).Text = "0";
		((Control)(object)txtCommercialTax).Text = "0";
		((Control)(object)txtStampValue).Text = "0";
		((Control)(object)txtGrowthFees).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)txtBranchBalance).Clear();
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtStampValue).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtGrowthFees).ValueChanged += txtAdditionalValues_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (dtpReceivingDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ التسليم" : "Please Enter The Receiving Date");
			((Control)(object)dtpReceivingDate).Focus();
			dtpReceivingDate.DropDown();
			return false;
		}
		if (dtpReceivingDate.DateTime < dtpDate.DateTime)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تاريخ التسليم قبل تاريخ الحجز" : "Receiving Date Is Before The Reservation Date");
			((Control)(object)dtpReceivingDate).Focus();
			dtpReceivingDate.DropDown();
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع السعر" : "Please Select Price Type");
			((TextEditorControlBase)cboPriceType).Focus();
			cboPriceType.DropDown();
			return false;
		}
		if (cboSalesMan.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مندوب المبيعات" : "Please Select Sales man");
			((TextEditorControlBase)cboSalesMan).Focus();
			cboSalesMan.DropDown();
			return false;
		}
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العملة" : "Please Select Currency");
			((TextEditorControlBase)cboCurrency).Focus();
			cboCurrency.DropDown();
			return false;
		}
		if (((Control)(object)txtExchangeRate).Text.Trim() == "" || decimal.Parse(((Control)(object)txtExchangeRate).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			((TextEditorControlBase)txtExchangeRate).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CRM_CustomersReservations", "CustomerReservationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CustomerReservationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = CustomersReservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم المخزن  ", "Please Enter Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingColors && (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()), 8) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (UsingBatchNoAndValidityPeriod && i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف مع رقم التشغيلة مع نفس المخزن", "Cannot Duplicate The Same Item With the Same Batch No With Same Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
				if (!UsingBatchNoAndValidityPeriod && i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("  لا يمكن تكرار الصنف مع نفس المخزن ", "Cannot Duplicate The Same Item with Same Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && (decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) > 0m || decimal.Parse(((Control)(object)txtDiscAfterTaxValue).Text) > 0m))
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='DiscountTaxAccount-Sales' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtCommercialTax).Text != "" && decimal.Parse(((Control)(object)txtCommercialTax).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الخصم من حسابات النظام  ", "Please Select Discount Tax Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='StampsTaxAccount' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtStampValue).Text != "" && decimal.Parse(((Control)(object)txtStampValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الدمغة من حسابات النظام  ", "Please Select Stamps Tax Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='AddTaxAccount-Sales' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtGrowthFees).Text != "" && decimal.Parse(((Control)(object)txtGrowthFees).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الاضافة من حسابات النظام  ", "Please Select Add Tax Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = CustomersReservations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkIsCash).Checked ? "1" : "0", ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (dtpReceivingDate.Value == null) ? "Null" : dtpReceivingDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["CustomerReservationID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["CustomerReservationDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			CustomersReservationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (DataSaved)
		{
			ItemsTransactions.ManageInThread();
			RowID = num.ToString();
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = CustomersReservations.Insert_Update(drMaster["CustomerReservationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkIsCash).Checked ? "1" : "0", ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (dtpReceivingDate.Value == null) ? "Null" : dtpReceivingDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV");
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["CustomerReservationID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["CustomerReservationDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("CRM_CustomersReservationsDetails", "CustomerReservationID", drMaster["CustomerReservationID"].ToString(), "CustomerReservationDetailID", text);
			CustomersReservationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (DataSaved)
		{
			ItemsTransactions.ManageInThread();
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			CustomersReservations.DeleteVirtual(drMaster["CustomerReservationID"].ToString(), GlobalVariables.UserID);
			CustomersReservationsDetails.DeleteVirtualByCustomerReservationID(drMaster["CustomerReservationID"].ToString(), GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["CustomerReservationID"].ToString(), "RLnsMIV", "RLnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["CustomerReservationID"].ToString(), "RLnsMIV", "RLnsMIV");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		if (cboTax.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = ((TextEditorControlBase)cboTax).Value;
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].DefaultCellValue = ((TextEditorControlBase)cboTax).Value;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "0" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / CalculateGrossWithoutItemUnderDiscount() * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 8).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		}
	}

	private void txtDiscAfterTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		}
	}

	private void txtDiscAfterTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtAdditionalValues_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void btnSalesManSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("1", "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan).Value = num;
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CRM_CustomersReservations_A.rpt" : "Rep_CRM_CustomersReservations_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@CustomerReservationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CustomersReservationsReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CustomerReservationID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboPriceType).Value;
		object value2 = ((TextEditorControlBase)cboClient).Value;
		object value3 = ((TextEditorControlBase)cboSalesMan).Value;
		object value4 = ((TextEditorControlBase)cboCurrency).Value;
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		((Control)(object)txtSerial).Visible = UsingBatchNoAndValidityPeriod;
		((Control)(object)lblSerial).Visible = UsingBatchNoAndValidityPeriod;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxes.Rows.Count; num++)
		{
			vlDetailsTaxs.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		FillCurrencyDropDown();
		((TextEditorControlBase)cboPriceType).Value = value;
		((TextEditorControlBase)cboClient).Value = value2;
		((TextEditorControlBase)cboSalesMan).Value = value3;
		((TextEditorControlBase)cboCurrency).Value = value4;
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
			if (bool.Parse(drMaster["Approved"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها معتمدة", "Cannot Update This Transaction Because It Is Approved ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		dtUsersTransactions = UsersTransactions.CheckTransaction("0", "0");
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (bool.Parse(drMaster["Approved"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها معتمدة", "Cannot Delete This Transaction Because It Is Approved ");
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PriceTypes(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboPriceType).Value = num;
		}
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex > -1)
		{
			((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), GlobalVariables.LocalCurrencyID.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboSalesMan).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"];
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				((TextEditorControlBase)cboPriceType).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"];
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			else
			{
				dtItemPrices = null;
			}
			dtDetails = CustomersReservationsDetails.FillByCustomerSubAccountID(((TextEditorControlBase)cboClient).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateGoss();
			CalculateTotalsTax();
		}
		else
		{
			dtItemPrices = null;
		}
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		((UltraGridBase)ULGData).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
		dataView.RowFilter = " UnitID is not null";
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0 && dataTable.Select(" UnitID<> " + dataTable.Rows[0]["UnitID"].ToString()).Length == 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = CustomersReservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void chkStore_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboStore).ReadOnly = !((UltraToggleEditorBase)chkStore).Checked;
		if (!((UltraToggleEditorBase)chkStore).Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = DBNull.Value;
			((TextEditorControlBase)cboStore).Value = DBNull.Value;
		}
	}

	private void cboStore_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = ((TextEditorControlBase)cboStore).Value;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = ((TextEditorControlBase)cboStore).Value;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
			{
				frmItemsBatches frmItemsBatches2 = new frmItemsBatches(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				frmItemsBatches2.WindowState = FormWindowState.Normal;
				((Control)(object)frmItemsBatches2.lblTitle).Text = (GlobalVariables.IsArabic ? "سريل" : "Items Batches");
				frmItemsBatches2.ShowDialog();
				dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(frmItemsBatches2.RowID));
				vlBatchs.ValueListItems.Clear();
				for (int i = 0; i < dtBatchs.Rows.Count; i++)
				{
					vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
				}
			}
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.F8)
		{
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
				{
					int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
					if (num != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
				{
					int num2 = SearchFunctions.TaxsSearch(IsFromServer: false);
					if (num2 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = num2;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["EnforceBatchNo"].ToString()))
				{
					int num3 = SearchFunctions.ItemsBatchesSearch(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()), 0, IsFromServer: false);
					if (num3 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = num3;
					}
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F9)
			{
				return;
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), CanModifyPriceType);
				frmImageViewer2.WindowState = FormWindowState.Normal;
				frmImageViewer2.ShowDialog();
				if (Adding && frmImageViewer2.Saved)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = (frmImageViewer2.ColorID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value : frmImageViewer2.ColorID);
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = (frmImageViewer2.ItemSizeID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value : frmImageViewer2.ItemSizeID);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = (frmImageViewer2.BatchID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value : frmImageViewer2.BatchID);
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = (frmImageViewer2.StoreID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value : frmImageViewer2.StoreID);
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
				CalculateGoss();
			}
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		}
		else if (ULGData.ActiveCell != null)
		{
			CalculateRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		rowIndex = -1;
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		base.ULGData_BeforeRowsDeleted(sender, e);
		CalcTotalQty();
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 8).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		}
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(Row.Cells["TotalPrice"].Value.ToString());
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString());
	}

	private void CalculateNetTotals()
	{
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscAfterTaxRatio).Text == "" || ((Control)(object)txtDiscAfterTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text) / 100m * (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (cboClient.SelectedIndex > -1)
		{
			if (bool.Parse(dtClients.Select("SubAccountID =" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["IsDiscountTax"].ToString()))
			{
				((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
				((Control)(object)txtCommercialTax).Text = decimal.Parse((double.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * GlobalVariables.DiscountTax).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
			}
			if (bool.Parse(dtClients.Select("SubAccountID =" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["IsAddedTax"].ToString()))
			{
				((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
				((Control)(object)txtGrowthFees).Text = decimal.Parse((double.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * GlobalVariables.AddedTax).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtGrowthFees).ValueChanged += txtAdditionalValues_ValueChanged;
			}
		}
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				result += 0m;
				continue;
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		return result;
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
		else if (rowIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > rowIndex)
		{
			frmEnterQuantity frmEnterQuantity2 = new frmEnterQuantity(((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Text.Split('-')[0].ToString(), ((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			frmEnterQuantity2.WindowState = FormWindowState.Normal;
			if (frmEnterQuantity2.ShowDialog() == DialogResult.OK)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) + decimal.Parse(frmEnterQuantity2.Value);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[rowIndex]);
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	private void txtSerial_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Expected O, but got Unknown
		if (e.KeyCode != Keys.Return || !(((Control)(object)txtSerial).Text != ""))
		{
			return;
		}
		if (dtBatchs.Select(" BatchName = '" + ((Control)(object)txtSerial).Text + "'").Length == 0)
		{
			((TextEditorControlBase)txtSerial).Clear();
			((TextEditorControlBase)txtSerial).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == ((Control)(object)txtSerial).Text)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateTotalsTax();
				((TextEditorControlBase)txtSerial).Clear();
				((TextEditorControlBase)txtSerial).Focus();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtBatchs.Select(" BatchName = '" + ((Control)(object)txtSerial).Text + "'")[0]["ItemID"].ToString());
		obj.Value = value;
		DataRow dataRow = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["DefaultStoreID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dtBatchs.Select(" BatchName = '" + ((Control)(object)txtSerial).Text + "'")[0]["BatchID"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
		}
		if (AutomaticlyAddItemTaxToSalesInvoice)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
		}
		int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
		ValueList unitsValueList = getUnitsValueList(unitTypeID);
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtSerial).Clear();
		((TextEditorControlBase)txtSerial).Focus();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public void AddItemInGid()
	{
		//IL_0fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe6: Expected O, but got Unknown
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Expected O, but got Unknown
		//IL_0e3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e46: Expected O, but got Unknown
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		//IL_17ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d8: Expected O, but got Unknown
		//IL_0cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		bool flag = dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0;
		bool flag2 = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + text + "'").Length == 0;
		if (flag && flag2)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		object value;
		if (flag)
		{
			DataRow dataRow = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + text + "'")[0];
			text = dtItems.Select("ItemID = " + dataRow["ItemID"].ToString())[0]["ItemBarCode"].ToString();
			string text5 = dataRow["UnitID"].ToString();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3 && ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() == text5 && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((cboClient.SelectedIndex > -1) & (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) == decimal.Parse(dtItemPrices.Select(" UnitID = " + text5 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" UnitID = " + text5 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" UnitID = " + text5 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m)))
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					rowIndex = i;
					CalculateGoss();
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
					CalculateTotalsTax();
					((TextEditorControlBase)txtBarCode).Clear();
					((TextEditorControlBase)txtBarCode).Focus();
					CalcTotalQty();
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
					return;
				}
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = text5;
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			if (AutomaticlyAddItemTaxToSalesInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow dataRow2 = dtItemPrices.Select(" UnitID = " + text5 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalsTax();
			}
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (text4 != "")
				{
					string value2 = dtBatchs.Select("(ItemID is null or ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + " ) And BatchName='" + text4 + "'")[0]["BatchID"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = value2;
				}
				else
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			CalcTotalQty();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			return;
		}
		DataRow dataRow3 = dtItems.Select(" ItemBarcode = '" + text + "'")[0];
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGData).Rows[j].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() == text3 && (!bool.Parse(dataRow3["IsUnitPrice"].ToString()) || ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString().Equals(dataRow3["UnitID"].ToString())))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
				rowIndex = j;
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[j]);
				CalculateTotalsTax();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				CalcTotalQty();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow3["ItemID"].ToString());
		obj3.Value = value;
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		if (AutomaticlyAddItemTaxToSalesInvoice)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
		}
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow3["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow3["UnitID"].ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) : (" ItemID = " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			if (array != null && array.Length != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else
			{
				UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
				obj5.Value = value;
			}
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalsTax();
		}
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList2 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
			if (text4 != "")
			{
				string value3 = dtBatchs.Select("(ItemID is null or ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + " ) And BatchName='" + text4 + "'")[0]["BatchID"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = value3;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
		}
		int unitTypeID2 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
		ValueList unitsValueList2 = getUnitsValueList(unitTypeID2);
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList2;
		if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		}
		if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		CalcTotalQty();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private ValueList getBatchsValueList(int ItemID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtBatchs.Select("ItemID is null or ItemID=" + ItemID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["BatchID"].ToString(), array[i]["BatchName"].ToString());
		}
		return val;
	}

	private ValueList getUnitsValueList(int UnitTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtUnits.Select("UnitTypeID=" + UnitTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["UnitID"].ToString(), array[i]["UnitName"].ToString());
		}
		return val;
	}

	private ValueList getColorsValueList(int ItemColorCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsColorCategorysDetails.Select("ItemColorCategoryID=" + ItemColorCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ColorID"].ToString(), dtColors.Select("ColorID =" + array[i]["ColorID"].ToString())[0]["ColorName"].ToString());
		}
		return val;
	}

	private ValueList getSizesValueList(int ItemSizeCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID=" + ItemSizeCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemSizeID"].ToString(), dtSizes.Select("ItemSizeID =" + array[i]["ItemSizeID"].ToString())[0]["ItemSizeName"].ToString());
		}
		return val;
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Expected O, but got Unknown
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Expected O, but got Unknown
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.Transactions.frmCustomersReservations));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblBalance = new UltraLabel();
		this.btnClientBalance = new UltraButton();
		this.txtBranchBalance = new UltraTextEditor();
		this.btnSalesManSearch = new UltraButton();
		this.cboSalesMan = new UltraComboEditor();
		this.lblSalesMan = new UltraLabel();
		this.btnClientSearch = new UltraButton();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.cboClient = new UltraComboEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblGrowthFees = new UltraLabel();
		this.txtGrowthFees = new UltraTextEditor();
		this.lblStampValue = new UltraLabel();
		this.txtStampValue = new UltraTextEditor();
		this.lblCommercialTax = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.lblDiscAfterTaxRatio = new UltraLabel();
		this.txtDiscAfterTaxRatio = new UltraTextEditor();
		this.lblDiscAfterTaxValue = new UltraLabel();
		this.txtDiscAfterTaxValue = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.chkIsCash = new UltraCheckEditor();
		this.dtpReceivingDate = new UltraDateTimeEditor();
		this.lblReceivingDate = new UltraLabel();
		this.lblSerial = new UltraLabel();
		this.txtSerial = new UltraTextEditor();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.chkTax = new UltraCheckEditor();
		this.cboTax = new UltraComboEditor();
		this.btnPriceTypeSearch = new UltraButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCash).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReceivingDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblBalance.AutoEllipsis = false;
		resources.ApplyResources(this.lblBalance, "lblBalance");
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((ControlBase)this.btnClientBalance).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.btnClientBalance, "btnClientBalance");
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Name = "btnClientBalance";
		resources.ApplyResources(this.txtBranchBalance, "txtBranchBalance");
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).Name = "txtBranchBalance";
		((EditorButtonControlBase)this.txtBranchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).TabStop = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		this.lblSalesMan.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		this.lblCurrency.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		this.lblClient.AutoEllipsis = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.lblGrowthFees, "lblGrowthFees");
		this.lblGrowthFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthFees).Name = "lblGrowthFees";
		((ControlBase)this.lblGrowthFees).WrapText = false;
		resources.ApplyResources(this.txtGrowthFees, "txtGrowthFees");
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).Name = "txtGrowthFees";
		((TextEditorControlBase)this.txtGrowthFees).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblStampValue, "lblStampValue");
		this.lblStampValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStampValue).Name = "lblStampValue";
		((ControlBase)this.lblStampValue).WrapText = false;
		resources.ApplyResources(this.txtStampValue, "txtStampValue");
		((System.Windows.Forms.Control)(object)this.txtStampValue).Name = "txtStampValue";
		((TextEditorControlBase)this.txtStampValue).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtStampValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		this.lblCommercialTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		((ControlBase)this.lblCommercialTax).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscAfterTaxRatio, "lblDiscAfterTaxRatio");
		this.lblDiscAfterTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio).Name = "lblDiscAfterTaxRatio";
		((ControlBase)this.lblDiscAfterTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscAfterTaxRatio, "txtDiscAfterTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).Name = "txtDiscAfterTaxRatio";
		((TextEditorControlBase)this.txtDiscAfterTaxRatio).ValueChanged += new System.EventHandler(txtDiscAfterTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscAfterTaxValue, "lblDiscAfterTaxValue");
		this.lblDiscAfterTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue).Name = "lblDiscAfterTaxValue";
		((ControlBase)this.lblDiscAfterTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscAfterTaxValue, "txtDiscAfterTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).Name = "txtDiscAfterTaxValue";
		((TextEditorControlBase)this.txtDiscAfterTaxValue).ValueChanged += new System.EventHandler(txtDiscAfterTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.chkIsCash, "chkIsCash");
		((System.Windows.Forms.Control)(object)this.chkIsCash).Name = "chkIsCash";
		((UltraWinEditorMaskedControlBase)this.dtpReceivingDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpReceivingDate, "dtpReceivingDate");
		this.dtpReceivingDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpReceivingDate).Name = "dtpReceivingDate";
		((System.Windows.Forms.Control)(object)this.dtpReceivingDate).TabStop = false;
		this.dtpReceivingDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		this.lblReceivingDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblReceivingDate, "lblReceivingDate");
		((System.Windows.Forms.Control)(object)this.lblReceivingDate).Name = "lblReceivingDate";
		((ControlBase)this.lblReceivingDate).WrapText = false;
		this.lblSerial.AutoEllipsis = false;
		resources.ApplyResources(this.lblSerial, "lblSerial");
		((System.Windows.Forms.Control)(object)this.lblSerial).Name = "lblSerial";
		((ControlBase)this.lblSerial).WrapText = false;
		resources.ApplyResources(this.txtSerial, "txtSerial");
		((System.Windows.Forms.Control)(object)this.txtSerial).Name = "txtSerial";
		((System.Windows.Forms.Control)(object)this.txtSerial).KeyUp += new System.Windows.Forms.KeyEventHandler(txtSerial_KeyUp);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		this.lblBarCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboStore, "cboStore");
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCash);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrowthFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrowthFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStampValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStampValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpReceivingDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmCustomersReservations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpReceivingDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceivingDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStampValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStampValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrowthFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrowthFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCash, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCash).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReceivingDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
