using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CnsProjects;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Purchasing;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Purchasing.Transactions;

public class frmPSInvoices3 : frmHeaderManyDetails
{
	private DataTable dtBranches;

	private DataTable dtItems;

	private DataTable dtReports;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtTaxs;

	private DataTable dtExpenses;

	private DataTable dtSuppliers;

	private DataTable dtPSOrders;

	private DataTable dtCurrency;

	private DataTable dtPaymentMethod;

	private DataTable dtPSOrderDetails;

	private DataTable dtInvoiceExpenses;

	private DataTable dtInvoiceDetailsExpenses;

	private DataTable dtItemPrices;

	private DataTable dtPOSDefaultStore;

	private DataTable dtgoodReceiptNotes;

	private DataSet ds;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlGrowthDetailsTaxs = new ValueList();

	private ValueList vlTableDetailsTaxs = new ValueList();

	private ValueList vlInvoiceExpenses = new ValueList();

	private ValueList vlInvoiceExpensesCurrency = new ValueList();

	private ValueList vlInvoiceDetailsExpensesCurrency = new ValueList();

	private ValueList vlInvoiceDetailsExpenses = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool CnsProjectsInstalled = false;

	private bool PurchaseItemsAuditAlert = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToPurchaseInvoice = false;

	private string CnsStoreID = "-1";

	private bool ShowSerial = false;

	private int newID = -100000;

	private int rowIndex = -1;

	private IContainer components = null;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboSupplier;

	private UltraLabel lblSupplier;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblPurchaseOrder;

	private UltraComboEditor cboPurchaseOrder;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnSupplierSearch;

	public UltraButton btnPurchaseOrderSearch;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsPurchaseOrder;

	private RadioButton rbIsDirectInvoice;

	public UltraButton btnPaymentMethodSearch;

	private UltraLabel lblPaymentMethod;

	private UltraComboEditor cboPaymentMethod;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDiscAfterTaxRatio;

	private UltraTextEditor txtDiscAfterTaxRatio;

	private UltraLabel lblDiscAfterTaxValue;

	private UltraTextEditor txtDiscAfterTaxValue;

	private UltraLabel lblCommercialTax;

	private UltraTextEditor txtCommercialTax;

	private UltraLabel lblStampValue;

	private UltraTextEditor txtStampValue;

	private UltraLabel lblGrowthFees;

	private UltraTextEditor txtGrowthFees;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataExpenses;

	private UltraComboEditor cboTax;

	private UltraCheckEditor chkTax;

	private UltraComboEditor cboItemBatchs;

	public UltraButton btnJV;

	private RadioButton rbIsGoodReceiptNote;

	protected internal UltraCheckEditor chkAll;

	protected internal CheckedListBox clbGoodReceiptNoteNo;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	public UltraButton btnGoodReceiptNoteSearch;

	public UltraButton btnSelectLenses;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBranchBalance;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	public UltraButton btnStoreSearch;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private UltraButton btnGetFromXL;

	private UltraLabel lblTable;

	private UltraTextEditor txtTableTaxTotalValue;

	private UltraLabel lblGrowthTax;

	private UltraTextEditor txtGrowthTaxTotalValue;

	public frmPSInvoices3()
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
		InitializeComponent();
		TableName = "PS_PSInvoices";
		IDCol = "PSInvoiceID";
		NoCol = "PSInvoiceNo";
		DateCol = "PSInvoiceDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public frmPSInvoices3(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		ShowSerial = GlobalFunctions.GetOption("ShowBatchNoInPurchaseOrder");
		PurchaseItemsAuditAlert = GlobalFunctions.GetOption("PurchaseItemsAuditAlert");
		CnsProjectsInstalled = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='CnsProjects'")[0]["Installed"]);
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
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToPurchaseInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToPurchaseInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboPurchaseOrder, dtPSOrders, "PSOrderID", "PSOrderNo");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceExpenses.ValueListItems.Clear();
		vlInvoiceDetailsExpenses.ValueListItems.Clear();
		for (int num = 0; num < dtExpenses.Rows.Count; num++)
		{
			vlInvoiceExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
			vlInvoiceDetailsExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
		}
		if (base.Tag != null)
		{
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		vlGrowthDetailsTaxs.ValueListItems.Clear();
		vlTableDetailsTaxs.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxs.Rows.Count; num2++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
			vlGrowthDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
			vlTableDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		FillCurrencyDropDown();
		dtDetails = PSInvoicesDetails.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceDetailsExpenses = PSInvoicesDetailsExpenses.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceExpenses = PSInvoicesExpenses.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtInvoiceDetailsExpenses);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtInvoiceDetailsExpenses";
		ds.Relations.Add(ds.Tables[0].Columns["PSInvoiceDetailID"], ds.Tables[1].Columns["PSInvoiceDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		((UltraGridBase)ULGDataExpenses).DataSource = dtInvoiceExpenses;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataExpenses);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Hidden = !PurchaseItemsAuditAlert;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التنبيه" : "Alert Date");
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.17);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة رسم تنمية" : "Growth Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة رسم تنمية" : "Growth Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة الجدول" : "Table Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة ضريبة الجدول" : "Table Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxID"].ValueList = (IValueList)(object)vlGrowthDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxID"].ValueList = (IValueList)(object)vlTableDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrowthTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdQty"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExchangeRate"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الصرف" : "Exchange Rate");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExchangeRate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyID"].ValueList = (IValueList)(object)vlInvoiceDetailsExpensesCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpenseID"].ValueList = (IValueList)(object)vlInvoiceDetailsExpenses;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Value"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CurrencyValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["PSInvoiceExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExchangeRate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExchangeRate"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الصرف  " : "Exchange Rate");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyValue"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExchangeRate"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlInvoiceExpenses;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyID"].ValueList = (IValueList)(object)vlInvoiceExpensesCurrency;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CurrencyValue"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = PSInvoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Expected O, but got Unknown
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
			GlobalFunctions.FillCombo(cboPurchaseOrder, dtPSOrders, "PSOrderID", "PSOrderNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["PSInvoiceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["PSInvoiceDate"];
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			((TextEditorControlBase)cboSupplier).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged -= cboPurchaseOrder_ValueChanged;
			((TextEditorControlBase)cboPurchaseOrder).Value = drMaster["PSOrderID"];
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged += cboPurchaseOrder_ValueChanged;
			rbIsDirectInvoice.Checked = bool.Parse(drMaster["IsDirectInvoice"].ToString());
			rbIsPurchaseOrder.Checked = !bool.Parse(drMaster["IsDirectInvoice"].ToString()) && !bool.Parse(drMaster["IsGoodReceiptNote"].ToString());
			rbIsGoodReceiptNote.Checked = bool.Parse(drMaster["IsGoodReceiptNote"].ToString());
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboPaymentMethod).Value = drMaster["PaymentMethodID"];
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTableTaxTotalValue).Text = decimal.Parse(drMaster["TableTaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrowthTaxTotalValue).Text = decimal.Parse(drMaster["GrowthTaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtStampValue).Text = decimal.Parse(drMaster["StampsValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrowthFees).Text = decimal.Parse(drMaster["GrowthFees"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo"].ToString() + ")قيد") : ("No( " + drMaster["JVNo"].ToString() + " )"));
			((Control)(object)btnJV).Visible = ((drMaster["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = PSInvoicesDetails.SelectByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceDetailsExpenses = PSInvoicesDetailsExpenses.SelectByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceExpenses = PSInvoicesExpenses.SelectByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (rbIsGoodReceiptNote.Checked)
			{
				((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
				clbGoodReceiptNoteNo.SelectedValueChanged -= clbGoodReceiptNoteNo_SelectedValueChanged;
				dtgoodReceiptNotes = Main.ExecuteQuery_DataTable(" Select GoodReceiptNoteID,(GoodReceiptNoteNo+' - '+ Convert (varchar ,GoodReceiptNoteDate,103)+' '+Convert (varchar ,GoodReceiptNoteDate,108)) as  GoodReceiptNoteNo  from  SC_GoodReceiptNotes Where PSInvoiceID =" + drMaster["PSInvoiceID"].ToString());
				Main.Fillclb(clbGoodReceiptNoteNo, dtgoodReceiptNotes, "GoodReceiptNoteID", "GoodReceiptNoteNo");
				for (int i = 0; i < clbGoodReceiptNoteNo.Items.Count; i++)
				{
					clbGoodReceiptNoteNo.SetItemChecked(i, value: true);
				}
				((UltraToggleEditorBase)chkAll).Checked = true;
				((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
				clbGoodReceiptNoteNo.SelectedValueChanged += clbGoodReceiptNoteNo_SelectedValueChanged;
			}
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtInvoiceDetailsExpenses);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtInvoiceDetailsExpenses";
			ds.Relations.Add(ds.Tables[0].Columns["PSInvoiceDetailID"], ds.Tables[1].Columns["PSInvoiceDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			((UltraGridBase)ULGDataExpenses).DataSource = dtInvoiceExpenses;
			InitGrid();
			CalcTotalQty();
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
		rbIsDirectInvoice.Enabled = !NavMode && !Updating;
		rbIsPurchaseOrder.Enabled = !NavMode && !Updating;
		rbIsGoodReceiptNote.Enabled = !NavMode && !Updating;
		((Control)(object)chkTax).Visible = !NavMode;
		((Control)(object)cboTax).Visible = !NavMode;
		((EditorButtonControlBase)cboSupplier).ReadOnly = NavMode || (rbIsDirectInvoice.Checked ? NavMode : ((!Adding && rbIsPurchaseOrder.Checked) || rbIsGoodReceiptNote.Checked));
		((EditorButtonControlBase)cboPurchaseOrder).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)txtBranchBalance).Visible = !NavMode;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((Control)(object)btnSelectLenses).Visible = !NavMode && (Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItems'")[0]["OptionValue"].ToString()) || Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItemsXY'")[0]["OptionValue"].ToString()));
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStampValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGrowthFees).ReadOnly = NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnSupplierSearch).Visible = (rbIsDirectInvoice.Checked ? (!NavMode) : (Adding && rbIsPurchaseOrder.Checked));
		((Control)(object)btnPurchaseOrderSearch).Visible = Adding && rbIsPurchaseOrder.Checked;
		((Control)(object)btnPaymentMethodSearch).Visible = !NavMode;
		((Control)(object)btnGoodReceiptNoteSearch).Visible = Adding && rbIsGoodReceiptNote.Checked;
		clbGoodReceiptNoteNo.Enabled = Adding;
		((Control)(object)chkAll).Enabled = Adding;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
		((Control)(object)btnGetFromXL).Visible = Adding;
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
			DataTable dataTable2 = dataView2.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["ItemBarCode"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboPurchaseOrder, dtPSOrders, "PSOrderID", "PSOrderNo");
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
		if (cboSupplier.SelectedIndex > -1 && Updating && dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			dtItemPrices = ItemsPrices.GetPrice(dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
		}
		else
		{
			dtItemPrices = null;
		}
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
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
		((Control)(object)txtCode).Text = (Adding ? PSInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		cboSupplier.SelectedIndex = -1;
		((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboPurchaseOrder).ValueChanged -= cboPurchaseOrder_ValueChanged;
		cboPurchaseOrder.SelectedIndex = -1;
		((TextEditorControlBase)cboPurchaseOrder).ValueChanged += cboPurchaseOrder_ValueChanged;
		cboPaymentMethod.SelectedIndex = -1;
		rbIsPurchaseOrder.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsPurchaseOrder.Checked = false;
		rbIsPurchaseOrder.CheckedChanged += RadioButtons_CheckedChanged;
		rbIsGoodReceiptNote.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsGoodReceiptNote.Checked = false;
		rbIsGoodReceiptNote.CheckedChanged += RadioButtons_CheckedChanged;
		((TextEditorControlBase)txtBranchBalance).Clear();
		rbIsDirectInvoice.Checked = true;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtExchangeRate).Text = "0";
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((Control)(object)txtTableTaxTotalValue).Text = "0";
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtGrowthTaxTotalValue).Text = "0";
		((Control)(object)txtDiscAfterTaxValue).Text = "0";
		((Control)(object)txtDiscAfterTaxRatio).Text = "0";
		((Control)(object)txtCommercialTax).Text = "0";
		((Control)(object)txtStampValue).Text = "0";
		((Control)(object)txtGrowthFees).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataExpenses).DataSource).Rows.Clear();
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
		if (cboSupplier.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Supplier");
			((TextEditorControlBase)cboSupplier).Focus();
			cboSupplier.DropDown();
			return false;
		}
		if (rbIsPurchaseOrder.Checked && cboPurchaseOrder.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار أمر الشراء" : "Please Select Purchase order No");
			((TextEditorControlBase)cboPurchaseOrder).Focus();
			cboPurchaseOrder.DropDown();
			return false;
		}
		if (rbIsGoodReceiptNote.Checked && clbGoodReceiptNoteNo.CheckedItems.Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار اذونات اضافة" : "Please Select Good receipt note");
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("PS_PSInvoices", "PSInvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PSInvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = PSInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
			if (UsingColors && (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0 || (dataRow["ItemColorCategoryID"] != DBNull.Value && dtItemsColorCategorysDetails.Select(" ItemColorCategoryID = " + int.Parse(dataRow["ItemColorCategoryID"].ToString()) + " and ColorID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0)))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0 || (dataRow["ItemSizeCategoryID"] != DBNull.Value && dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID = " + int.Parse(dataRow["ItemSizeCategoryID"].ToString()) + " and ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0)))
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
			if (!rbIsGoodReceiptNote.Checked && (GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoice") || GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoiceEnforceCreation")) && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
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
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && (GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoice") || GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoiceEnforceCreation")) && (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value || dtBatchs.Select("BatchID=" + ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (PurchaseItemsAuditAlert && ((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"].Value.ToString()) < dtpDate.DateTime)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("تاريخ التنبيه قبل تاريخ الفاتورة", "Alert Date Is Before The Voucher Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExpenseID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExpenseID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Value"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Value"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExchangeRate"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExchangeRate"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال قيمة سعر الصرف  ", "Please Enter Exchange Rate ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExchangeRate"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CurrencyValue"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CurrencyValue"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CurrencyValue"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CurrencyID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار العملة  ", "Please Select Currency ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CurrencyID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExpenseID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense Name ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExpenseID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["CurrencyValue"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[k].Cells["CurrencyValue"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["CurrencyValue"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["CurrencyID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار العملة  ", "Please Select Currency ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["CurrencyID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExchangeRate"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExchangeRate"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الصرف  ", "Please Enter Exchange Rate ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExchangeRate"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='PurchaseAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المشتريات من حسابات النظام  ", "Please Select Purchase Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='PurchaseReturnsAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب مردودات المشتريات من حسابات النظام  ", "Please Select Purchase Returns Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='PurchaseDiscountAccount' ")[0]["AccountID"] == DBNull.Value && (decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) > 0m || decimal.Parse(((Control)(object)txtDiscAfterTaxValue).Text) > 0m))
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المكتسب من حسابات النظام  ", "Please Select Purchase Discount Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='DiscountTaxAccount-Purchase' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtCommercialTax).Text != "" && decimal.Parse(((Control)(object)txtCommercialTax).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الخصم من حسابات النظام  ", "Please Select Discount Tax Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='StampsTaxAccount' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtStampValue).Text != "" && decimal.Parse(((Control)(object)txtStampValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الدمغة من حسابات النظام  ", "Please Select Stamps Tax Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='AddTaxAccount-Purchase' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtGrowthFees).Text != "" && decimal.Parse(((Control)(object)txtGrowthFees).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الاضافة من حسابات النظام  ", "Please Select Add Tax Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Expected O, but got Unknown
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			((UltraGridBase)ULGData).UpdateData();
			string text = "";
			decimal invoiceExpense = CalculateInvoiceExpense();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CalculateGoss();
				CalculateTotalsTax();
				CalculateRowActualUnitPriceAndReturnedPrice(((UltraGridBase)ULGData).Rows[i], invoiceExpense);
				((UltraGridBase)ULGData).Rows[i].Cells["DeliverdQty"].Value = "0";
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			num = PSInvoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsGoodReceiptNote.Checked ? "1" : "0", ((TextEditorControlBase)cboSupplier).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (cboPurchaseOrder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseOrder).Value.ToString(), "Null", (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), "Null", "Null", "Null", "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTableTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtGrowthTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (rbIsPurchaseOrder.Checked && cboPurchaseOrder.SelectedIndex > -1)
			{
				if (GlobalFunctions.GetOption("ClosePurchaseOrderAfterPurchaseInvoice"))
				{
					Main.ExecuteNonQuery(" Update PS_PSOrders Set HasPSInvoice=1 where PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString());
				}
				else
				{
					Main.ExecuteNonQuery(" Update PS_PSOrders Set HasPSInvoice=1 where Closed=1 And PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString());
				}
				Main.ExecuteNonQuery(" Update SC_GoodReceiptNotes set PSInvoiceID = " + num + " Where PSInvoiceID Is Null And Deleted =0 And PSOrderID =" + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString());
			}
			if (rbIsGoodReceiptNote.Checked)
			{
				text = GetGoodReceiptNotesIds();
				if (text != "")
				{
					Main.ExecuteNonQuery(" Update SC_GoodReceiptNotes Set PSInvoiceID = " + num + " Where GoodReceiptNoteID in ( " + text + ")");
					Main.ExecuteNonQuery(" Insert Into Trans_Log Select " + GlobalVariables.UserID + ",'SC_GoodReceiptNotes',GoodReceiptNoteID,GetDate(),'Inv'  From SC_GoodReceiptNotes Where GoodReceiptNoteID in ( " + text + ")");
				}
			}
			DataSet dataSet = (DataSet)((UltraGridBase)ULGData).DataSource;
			dataSet.AcceptChanges();
			for (int j = 0; j < dataSet.Tables.Count; j++)
			{
				for (int k = 0; k < dataSet.Tables[j].Rows.Count; k++)
				{
					dataSet.Tables[j].Rows[k].SetAdded();
				}
			}
			PSInvoicesDetails.Insert_UpdateByDataset(dataSet, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, EnforceUpdate: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count > 0)
			{
				ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; l++)
				{
					((UltraGridBase)ULGDataExpenses).Rows[l].Cells["PSInvoiceExpenseID"].Value = -1;
					((UltraGridBase)ULGDataExpenses).Rows[l].Cells["PSInvoiceID"].Value = num;
					((UltraGridBase)ULGDataExpenses).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				PSInvoicesExpenses.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataExpenses).DataSource, GlobalVariables.UserID);
			}
			PSInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			if (GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoiceEnforceCreation") && (rbIsDirectInvoice.Checked || (rbIsPurchaseOrder.Checked = cboPurchaseOrder.SelectedIndex > -1 && !bool.Parse(dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["IsGRNFirst"].ToString()))))
			{
				GoodReceiptNotesDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (!GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoice") || GlobalFunctions.GetOption("CreateGoodReceiptNoteFromPurchaseInvoiceEnforceCreation") || (!rbIsDirectInvoice.Checked && !(rbIsPurchaseOrder.Checked = cboPurchaseOrder.SelectedIndex > -1 && !bool.Parse(dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["IsGRNFirst"].ToString()))) || dtDetails.Select(" UnitID Is not Null ").Length == 0)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد إنشاء إذن إضافة ؟", "Are You Sure You want to Create Good Receipt Note?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				GoodReceiptNotesDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطأ فى إنشاء إذن الاضافة لم يتم الحفظ " : "Error Occured When Creating Good Receipt Note");
			}
		}
	}

	public override void UpdateData()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Expected O, but got Unknown
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			((UltraGridBase)ULGData).UpdateData();
			decimal invoiceExpense = CalculateInvoiceExpense();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text);
				CalculateGoss();
				CalculateTotalsTax();
				CalculateRowActualUnitPriceAndReturnedPrice(((UltraGridBase)ULGData).Rows[i], invoiceExpense);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			((UltraGridBase)ULGData).UpdateData();
			int num = PSInvoices.Insert_Update(drMaster["PSInvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsGoodReceiptNote.Checked ? "1" : "0", ((TextEditorControlBase)cboSupplier).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (cboPurchaseOrder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseOrder).Value.ToString(), "Null", (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), (drMaster["TruckNo"] == DBNull.Value) ? "Null" : drMaster["TruckNo"].ToString(), (drMaster["SubTruckNo"] == DBNull.Value) ? "Null" : drMaster["SubTruckNo"].ToString(), (drMaster["TruckWeight"] == DBNull.Value) ? "Null" : drMaster["TruckWeight"].ToString(), bool.Parse(drMaster["IsTruckPolicy"].ToString()) ? "1" : "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTableTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtGrowthTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", bool.Parse(drMaster["Closed"].ToString()) ? "1" : "0", (drMaster["PurchaseJVID"] == DBNull.Value) ? "Null" : drMaster["PurchaseJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			bool enforceUpdate = false;
			if (drMaster != null && ((Control)(object)txtExchangeRate).Text != drMaster["ExchangeRate"].ToString())
			{
				enforceUpdate = true;
			}
			PSInvoicesDetails.Insert_UpdateByDataset((DataSet)((UltraGridBase)ULGData).DataSource, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, enforceUpdate);
			PSInvoicesExpenses.DeleteByPSInvoiceID(num.ToString(), GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count > 0)
			{
				ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["PSInvoiceExpenseID"].Value = -1;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["PSInvoiceID"].Value = num;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				PSInvoicesExpenses.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataExpenses).DataSource, GlobalVariables.UserID);
			}
			if (rbIsDirectInvoice.Checked && drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboSupplier).Value.ToString())
			{
				Main.ExecuteNonQuery(" Update SC_GoodReceiptNotes set SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value.ToString() + " Where PSInvoiceID=  " + drMaster["PSInvoiceID"].ToString());
			}
			PSInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_GoodReceiptNotes Where Deleted=0 And IsPSInvoice = 1 And PSInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تمت أضافتها فى المخازن", "Cannot Delete This Transaction Because It Added in The Store ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_SuppliersReturns Where Deleted=0 And PSInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم أرتجاعها من المخازن", "Cannot Delete This Transaction Because It Returned From The Store ");
			return;
		}
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DataSaved = true;
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
			if (rbIsGoodReceiptNote.Checked && drMaster["PSInvoiceID"] != DBNull.Value && clbGoodReceiptNoteNo.Items.Count > 0 && GoodReceiptNotes.ValidateClosedYearbyGoodReceiptNotesIds(GetGoodReceiptNotesIds()).Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة اذونات اضافة فى سنوات مغلقة", "Cannot Update This Transaction Because there Are Good receipt Notes In Closed Year   ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.DeleteVirtual(drMaster["PurchaseJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			PSInvoices.DeleteVirtual(drMaster["PSInvoiceID"].ToString(), GlobalVariables.UserID);
			PSInvoicesDetails.DeleteVirtualByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.UserID);
			PSInvoicesDetailsExpenses.DeleteVirtualByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.UserID);
			PSInvoicesExpenses.DeleteVirtualByPSInvoiceID(drMaster["PSInvoiceID"].ToString(), GlobalVariables.UserID);
			if (rbIsPurchaseOrder.Checked)
			{
				Main.ExecuteNonQuery(" Update PS_PSOrders Set HasPSInvoice=0 where PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString());
			}
			Main.ExecuteNonQuery(" Update SC_GoodReceiptNotes set PSInvoiceID=null Where PSInvoiceID =" + drMaster["PSInvoiceID"].ToString());
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PS_PSInvoices_A.rpt" : "Rep_PS_PSInvoices_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PSInvoiceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@PSInvoiceIDs", "," + RowID + ",", "Tax name");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PSInvoicesReport(-1, 0, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PSInvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboCurrency).Value;
		object value2 = ((TextEditorControlBase)cboItemBatchs).Value;
		object value3 = ((TextEditorControlBase)cboPaymentMethod).Value;
		object value4 = ((TextEditorControlBase)cboPurchaseOrder).Value;
		object value5 = ((TextEditorControlBase)cboSupplier).Value;
		object value6 = ((TextEditorControlBase)cboTax).Value;
		object value7 = ((TextEditorControlBase)cboTransactionBranch).Value;
		((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
		((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
		((TextEditorControlBase)cboPurchaseOrder).ValueChanged -= cboPurchaseOrder_ValueChanged;
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
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToPurchaseInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToPurchaseInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		DataView dataView = new DataView(dtPSOrders);
		dataView.RowFilter = " HasPSInvoice =0 And BranchID= " + GlobalVariables.CurrentBranchID;
		GlobalFunctions.FillCombo(cboPurchaseOrder, dataView.ToTable(), "PSOrderID", "PSOrderNo");
		DataView dataView2 = new DataView(dtStores);
		dataView2.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView2.ToTable();
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dataTable.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[l]["StoreID"], dataTable.Rows[l]["StoreName"].ToString());
		}
		if (Adding)
		{
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView3.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int m = 0; m < dataTable2.Rows.Count; m++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int n = 0; n < dtItems.Rows.Count; n++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["ItemBarCode"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceExpenses.ValueListItems.Clear();
		vlInvoiceDetailsExpenses.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtExpenses.Rows.Count; num2++)
		{
			vlInvoiceExpenses.ValueListItems.Add(dtExpenses.Rows[num2]["ExpenseID"], dtExpenses.Rows[num2]["ExpenseName"].ToString());
			vlInvoiceDetailsExpenses.ValueListItems.Add(dtExpenses.Rows[num2]["ExpenseID"], dtExpenses.Rows[num2]["ExpenseName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		vlGrowthDetailsTaxs.ValueListItems.Clear();
		vlTableDetailsTaxs.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtTaxs.Rows.Count; num3++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num3]["TaxID"], dtTaxs.Rows[num3]["TaxName"].ToString());
			vlGrowthDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num3]["TaxID"], dtTaxs.Rows[num3]["TaxName"].ToString());
			vlTableDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num3]["TaxID"], dtTaxs.Rows[num3]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		FillCurrencyDropDown();
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		((TextEditorControlBase)cboCurrency).Value = value;
		((TextEditorControlBase)cboItemBatchs).Value = value2;
		((TextEditorControlBase)cboPaymentMethod).Value = value3;
		((TextEditorControlBase)cboPurchaseOrder).Value = value4;
		((TextEditorControlBase)cboSupplier).Value = value5;
		((TextEditorControlBase)cboTax).Value = value6;
		((TextEditorControlBase)cboTransactionBranch).Value = value7;
		((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
		((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		((TextEditorControlBase)cboPurchaseOrder).ValueChanged += cboPurchaseOrder_ValueChanged;
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_1265: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Expected O, but got Unknown
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value)[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			string text = (dataRow["DefaultStoreID"].Equals(DBNull.Value) ? "-1" : dataRow["DefaultStoreID"].ToString());
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((dtStores.Select(" StoreID= " + text + " And BranchID =" + GlobalVariables.CurrentBranchID).Length != 0) ? text : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value)));
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboSupplier.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateGoss();
				CalculateTotalsTax();
			}
			else if ((rbIsDirectInvoice.Checked || rbIsGoodReceiptNote.Checked) && cboSupplier.SelectedIndex > -1 && dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] == DBNull.Value)
			{
				DataTable dataTable = Items.SelectLastPOPrice(e.Cell.Row.Cells["ItemID"].Value.ToString(), (e.Cell.Row.Cells["ColorID"].Value == DBNull.Value) ? "1" : e.Cell.Row.Cells["ColorID"].Value.ToString(), (e.Cell.Row.Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : e.Cell.Row.Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
				if (dataTable.Rows.Count > 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					CalculateGoss();
					CalculateTotalsTax();
				}
			}
			if (UsingBatchNoAndValidityPeriod)
			{
				int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
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
			if (AutomaticlyAddItemTaxToPurchaseInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["GrowthTaxID"].Value = dataRow["GrowthTaxID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["TableTaxID"].Value = dataRow["TableTaxID"];
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
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
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "GrowthTaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TableTaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num3);
				obj2.Value = value;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				int num4 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num4 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num4);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				}
				else
				{
					e.Cell.Row.Cells["UnitID"].ValueList = null;
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = 1;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
				}
			}
		}
		if (((GridItemBase)e.Cell).Band.Index == 1 && ((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyID")
		{
			if (e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				((UltraGridBase)ULGData).UpdateData();
				e.Cell.Row.Cells["ExchangeRate"].Value = dtCurrency.Select(" CurrencyID= " + e.Cell.Value.ToString())[0]["ExchangeRate"].ToString();
				if (((Control)(object)txtExchangeRate).Text != "" && ((Control)(object)txtExchangeRate).Text != "0")
				{
					e.Cell.Row.Cells["Value"].Value = decimal.Parse((e.Cell.Row.Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(e.Cell.Row.Cells["CurrencyValue"].Value.ToString());
				}
			}
			else
			{
				e.Cell.Row.Cells["ExchangeRate"].Value = 0;
			}
			CalculateRow(e.Cell.Row.ParentRow);
		}
		CalcTotalQty();
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID") && rbIsGoodReceiptNote.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			return;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID") && ShowSerial && rbIsPurchaseOrder.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID") && ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPurchasePrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (cboPurchaseOrder.SelectedIndex > -1 && bool.Parse(dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["IsGRNFirst"].ToString()) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "UnitPrice" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ExpenseID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Value" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "TaxID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "TableTaxID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "GrowthTaxID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if ((rbIsDirectInvoice.Checked || (rbIsGoodReceiptNote.Checked && !ShowSerial) || (rbIsPurchaseOrder.Checked && cboPurchaseOrder.SelectedIndex > -1 && !ShowSerial && !bool.Parse(dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["IsGRNFirst"].ToString()))) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
				{
					frmAddItemsSerial frmAddItemsSerial2 = new frmAddItemsSerial(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSerial2.WindowState = FormWindowState.Normal;
					if (frmAddItemsSerial2.ShowDialog() == DialogResult.OK)
					{
						dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						vlBatchs.ValueListItems.Clear();
						for (int i = 0; i < dtBatchs.Rows.Count; i++)
						{
							vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmAddItemsSerial2.BatchID;
					}
				}
				else if (rbIsDirectInvoice.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList == null)
				{
					frmAddItemsColor frmAddItemsColor2 = new frmAddItemsColor(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsColor2.WindowState = FormWindowState.Normal;
					frmAddItemsColor2.ShowDialog();
					dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					vlColors.ValueListItems.Clear();
					for (int j = 0; j < dtColors.Rows.Count; j++)
					{
						vlColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
					}
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmAddItemsColor2.ColorID;
				}
				else if (rbIsDirectInvoice.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList == null)
				{
					frmAddItemsSize frmAddItemsSize2 = new frmAddItemsSize(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSize2.WindowState = FormWindowState.Normal;
					frmAddItemsSize2.ShowDialog();
					dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					vlSizes.ValueListItems.Clear();
					for (int k = 0; k < dtSizes.Rows.Count; k++)
					{
						vlSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
					}
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmAddItemsSize2.SizeID;
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()) == 0m)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && cboSupplier.SelectedIndex > -1)
				{
					DataTable dthistory = PSInvoicesDetails.SelectTopPrices(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
					frmEnterValue frmEnterValue2 = new frmEnterValue(dthistory, GlobalVariables.IsArabic ? "السعر شامل الضريبه" : "Total Price with tax", _IsInt: false, _IsNumeric: true);
					frmEnterValue2.WindowState = FormWindowState.Normal;
					frmEnterValue2.ShowDialog();
					if (frmEnterValue2.Value != "" && decimal.Parse(frmEnterValue2.Value) > 0m)
					{
						DataRow dataRow = dtSuppliers.Select("SubAccountID =" + ((TextEditorControlBase)cboSupplier).Value.ToString())[0];
						double num = 0.0;
						if (((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value != DBNull.Value)
						{
							num = double.Parse(dtTaxs.Select(" TaxID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100.0;
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = double.Parse(frmEnterValue2.Value) / (1.0 - (bool.Parse(dataRow["IsDiscountTax"].ToString()) ? GlobalVariables.DiscountTax : 0.0) + (bool.Parse(dataRow["IsAddedTax"].ToString()) ? GlobalVariables.AddedTax : 0.0) + num);
					}
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
				{
					int num2 = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
					if (num2 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num2;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num2;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
				{
					int num3 = SearchFunctions.TaxsSearch(IsFromServer: false);
					if (num3 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = num3;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["EnforceBatchNo"].ToString()))
				{
					int num4 = SearchFunctions.ItemsBatchesSearch(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()), 0, IsFromServer: false);
					if (num4 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = num4;
					}
				}
				else if (((GridItemBase)ULGData.ActiveCell).Band.Index == 1 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ExpenseID")
				{
					int num5 = SearchFunctions.ExpensesSearch(IsFromServer: false);
					if (num5 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseID"].Value = num5;
					}
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

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["PSInvoiceDetailID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["DetailExpenseID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GrowthTaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TableTaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				if (((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" لايمكن نقص الكمية عن الكمية المستلمة فى المخازن و قدرها " + ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString(), "You Cannot Decrease The Quantity From The Deliverd Qty in the Store" + ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value;
				}
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
			if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GrowthTaxValue") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TableTaxValue") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
			{
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			}
		}
		else if (ULGData.ActiveCell != null && ((Control)(object)txtExchangeRate).Text != "" && ((Control)(object)txtExchangeRate).Text != "0" && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrencyValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ExchangeRate") && e.Cell.Value != DBNull.Value)
		{
			e.Cell.Row.Cells["Value"].Value = decimal.Parse((ULGData.ActiveCell.Row.Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : ULGData.ActiveCell.Row.Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(ULGData.ActiveCell.Row.Cells["CurrencyValue"].Value.ToString());
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
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((UltraGridBase)ULGData).ActiveRow != null && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لانه تمت أضافته فى المخازن" : "Cannot Delete This Item Because Added in The Store");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
			CalcTotalQty();
		}
	}

	private void CalculateGoss()
	{
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GrowthTaxValue"].Value.ToString());
			num3 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TableTaxValue"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtGrowthTaxTotalValue).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtTableTaxTotalValue).Text = decimal.Parse(num3.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
		}
		if (Row.Cells["TableTaxID"].Value != DBNull.Value)
		{
			Row.Cells["TableTaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TableTaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TableTaxValue"].Value = 0;
		}
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TableTaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		if (Row.Cells["GrowthTaxID"].Value != DBNull.Value)
		{
			Row.Cells["GrowthTaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["GrowthTaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["TableTaxValue"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()));
		}
		else
		{
			Row.Cells["GrowthTaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + CalculateDetailExpense(Row);
	}

	public decimal CalculateDetailExpense(UltraGridRow Row)
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; i++)
		{
			if (Row.ChildBands[0].Rows[i].Cells["Value"].Value != DBNull.Value)
			{
				result += decimal.Parse(Row.ChildBands[0].Rows[i].Cells["Value"].Value.ToString());
			}
		}
		return result;
	}

	public decimal CalculateInvoiceExpense()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; i++)
		{
			result += decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[i].Cells["Value"].Value.ToString());
		}
		return result;
	}

	private decimal GetTotalExpenses()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num2 += CalculateDetailExpense(((UltraGridBase)ULGData).Rows[i]);
		}
		return num2 + CalculateInvoiceExpense();
	}

	private void CalculateNetTotals()
	{
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscAfterTaxRatio).Text == "" || ((Control)(object)txtDiscAfterTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text) / 100m * (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (cboSupplier.SelectedIndex > -1)
		{
			if (bool.Parse(dtSuppliers.Select("SubAccountID =" + ((TextEditorControlBase)cboSupplier).Value.ToString())[0]["IsDiscountTax"].ToString()))
			{
				((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
				((Control)(object)txtCommercialTax).Text = decimal.Parse((double.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * GlobalVariables.DiscountTax).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
			}
			if (bool.Parse(dtSuppliers.Select("SubAccountID =" + ((TextEditorControlBase)cboSupplier).Value.ToString())[0]["IsAddedTax"].ToString()))
			{
				((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
				((Control)(object)txtGrowthFees).Text = decimal.Parse((double.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * GlobalVariables.AddedTax).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtGrowthFees).ValueChanged += txtAdditionalValues_ValueChanged;
			}
		}
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
	}

	private void CalculateRowActualUnitPriceAndReturnedPrice(UltraGridRow Row, decimal InvoiceExpense)
	{
		Row.Cells["ActualUnitPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + CalculateDetailExpense(Row) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * InvoiceExpense / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
		Row.Cells["ReturnPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscBeforeTaxValue).Text != "" && decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) > 0m)
		{
			Row.Cells["DiscountAfterTax"].Value = (decimal.Parse(Row.Cells["NetPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString())) / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text)) * decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text);
		}
		else
		{
			Row.Cells["DiscountAfterTax"].Value = 0;
		}
		Row.Cells["CommercialTaxValue"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text);
		Row.Cells["StampsValue"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text);
		Row.Cells["GrowthFees"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text);
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
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
				CalcTotalQty();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void AddItemInGid()
	{
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		//IL_0abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			if (text.StartsWith("000"))
			{
				text = "00" + text.TrimStart('0');
			}
			if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
			{
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				GlobalVariables.InformationMB.Show("هذا الباركود غير موجود", "This BarCode Does not Exists");
				return;
			}
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
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
		object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
		obj.Value = value;
		DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboSupplier.SelectedIndex > -1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateGoss();
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
		if (AutomaticlyAddItemTaxToPurchaseInvoice)
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
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		vlInvoiceExpensesCurrency.ValueListItems.Clear();
		vlInvoiceDetailsExpensesCurrency.ValueListItems.Clear();
		for (int i = 0; i < dtCurrency.Rows.Count; i++)
		{
			vlInvoiceExpensesCurrency.ValueListItems.Add(dtCurrency.Rows[i]["CurrencyID"], dtCurrency.Rows[i]["CurrencyName"].ToString());
			vlInvoiceDetailsExpensesCurrency.ValueListItems.Add(dtCurrency.Rows[i]["CurrencyID"], dtCurrency.Rows[i]["CurrencyName"].ToString());
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = PSInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			if (cboSupplier.SelectedIndex > -1)
			{
				((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboSupplier).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : drMaster["BranchID"].ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboPurchaseOrder_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0, -1);
			if (num != 0)
			{
				((TextEditorControlBase)cboPurchaseOrder).Value = num;
			}
		}
	}

	private void cboPurchaseOrder_ValueChanged(object sender, EventArgs e)
	{
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Expected O, but got Unknown
		if (cboPurchaseOrder.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged -= cboPurchaseOrder_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			object obj = dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["SubAccountID"];
			if (((DataTable)cboSupplier.DataSource).Select("SubAccountID = " + obj.ToString()).Length != 0)
			{
				((TextEditorControlBase)cboSupplier).Value = obj;
			}
			else
			{
				((TextEditorControlBase)cboSupplier).Value = null;
			}
			((TextEditorControlBase)cboCurrency).Value = dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboPurchaseOrder).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (((TextEditorControlBase)cboSupplier).Value != null)
			{
				((TextEditorControlBase)cboPaymentMethod).Value = dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["DefaultPaymentMethodID"];
				((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			dtPSOrderDetails = PSInvoicesDetails.FillByPSOrderID(((TextEditorControlBase)cboPurchaseOrder).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			CnsStoreID = "-1";
			if (CnsProjectsInstalled)
			{
				DataTable storeByPSOrder = Contracts.GetStoreByPSOrder(((TextEditorControlBase)cboPurchaseOrder).Value.ToString());
				if (storeByPSOrder.Rows.Count > 0)
				{
					CnsStoreID = storeByPSOrder.Rows[0]["StoreID"].ToString();
				}
			}
			dtDetails.Rows.Clear();
			for (int i = 0; i < dtPSOrderDetails.Rows.Count; i++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtPSOrderDetails.Rows[i]["ItemID"].ToString());
				obj2.Value = value;
				DataRow dataRow = dtItems.Select("ItemID=" + dtPSOrderDetails.Rows[i]["ItemID"].ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dtPSOrderDetails.Rows[i]["ColorID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dtPSOrderDetails.Rows[i]["ItemSizeID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dtPSOrderDetails.Rows[i]["BatchID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtPSOrderDetails.Rows[i]["UnitID"];
				string text = (dataRow["DefaultStoreID"].Equals(DBNull.Value) ? "-1" : dataRow["DefaultStoreID"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((CnsProjectsInstalled && CnsStoreID != "-1") ? CnsStoreID : ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((dtStores.Select(" StoreID= " + text + " And BranchID =" + GlobalVariables.CurrentBranchID).Length != 0) ? text : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value))));
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dtPSOrderDetails.Rows[i]["Qty"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtPSOrderDetails.Rows[i]["UnitPrice"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value = dtPSOrderDetails.Rows[i]["DeliverdQty"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(dtPSOrderDetails.Rows[i]["UnitPrice"].ToString()) * decimal.Parse(dtPSOrderDetails.Rows[i]["Qty"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = dtPSOrderDetails.Rows[i]["Notes"];
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				if (dtPSOrderDetails.Rows[i]["UnitID"] != DBNull.Value)
				{
					ValueList unitsValueList = getUnitsValueList(int.Parse(dtUnits.Select(" UnitID =" + dtPSOrderDetails.Rows[i]["UnitID"].ToString())[0]["UnitTypeID"].ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
				}
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(dtPSOrderDetails.Rows[i]["ItemID"].ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			CalculateGoss();
			CalculateTotalsTax();
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged += cboPurchaseOrder_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
	}

	private void btnPurchaseOrderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PSOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, -1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboPurchaseOrder).Value = num;
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblPurchaseOrder).Visible = rbIsPurchaseOrder.Checked;
		((Control)(object)cboPurchaseOrder).Visible = rbIsPurchaseOrder.Checked;
		((Control)(object)btnPurchaseOrderSearch).Visible = rbIsPurchaseOrder.Checked;
		((Control)(object)chkAll).Visible = rbIsGoodReceiptNote.Checked;
		clbGoodReceiptNoteNo.Visible = rbIsGoodReceiptNote.Checked;
		((Control)(object)btnGoodReceiptNoteSearch).Visible = rbIsGoodReceiptNote.Checked && Adding;
		((Control)(object)btnGetFromXL).Visible = rbIsDirectInvoice.Checked && Adding;
		if (Adding || Updating)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress -= textBox_KeyPress;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			if (rbIsPurchaseOrder.Checked)
			{
				dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
				DataView dataView = new DataView(dtPSOrders);
				dataView.RowFilter = " HasPSInvoice =0  And BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboPurchaseOrder, dataView.ToTable(), "PSOrderID", "PSOrderNo");
			}
			cboPurchaseOrder.SelectedIndex = -1;
			cboSupplier.SelectedIndex = -1;
			cboPaymentMethod.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress += textBox_KeyPress;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
	}

	private void cboSupplier_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Suppliers((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSupplier).Value = num;
			}
		}
	}

	private void btnSupplierSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Suppliers((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSupplier).Value = num;
		}
	}

	private void cboSupplier_ValueChanged(object sender, EventArgs e)
	{
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Expected O, but got Unknown
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Expected O, but got Unknown
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Expected O, but got Unknown
		if (cboSupplier.SelectedIndex <= -1)
		{
			return;
		}
		if (cboCurrency.SelectedIndex > -1)
		{
			((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboSupplier).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : drMaster["BranchID"].ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		if (rbIsPurchaseOrder.Checked)
		{
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged -= cboPurchaseOrder_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			DataView dataView = new DataView(dtPSOrders);
			dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboPurchaseOrder, dataView.ToTable(), "PSOrderID", "PSOrderNo");
			dtDetails.Rows.Clear();
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((TextEditorControlBase)cboPurchaseOrder).ValueChanged += cboPurchaseOrder_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		}
		else if (rbIsGoodReceiptNote.Checked && Adding)
		{
			clbGoodReceiptNoteNo.DataSource = null;
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
			dtgoodReceiptNotes = Main.ExecuteQuery_DataTable(" Select GoodReceiptNoteID,(GoodReceiptNoteNo+' - '+ Convert (varchar ,GoodReceiptNoteDate,103)+' '+Convert (varchar ,GoodReceiptNoteDate,108)) as  GoodReceiptNoteNo  from  SC_GoodReceiptNotes Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID + " And IsDirect=1 And PSInvoiceID is null And SubAccountID=" + ((TextEditorControlBase)cboSupplier).Value.ToString());
			clbGoodReceiptNoteNo.SelectedValueChanged -= clbGoodReceiptNoteNo_SelectedValueChanged;
			Main.Fillclb(clbGoodReceiptNoteNo, dtgoodReceiptNotes, "GoodReceiptNoteID", "GoodReceiptNoteNo");
			clbGoodReceiptNoteNo.SelectedValueChanged += clbGoodReceiptNoteNo_SelectedValueChanged;
		}
		if (rbIsDirectInvoice.Checked || rbIsGoodReceiptNote.Checked)
		{
			if (dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				dtItemPrices = ItemsPrices.GetPrice(dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
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
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					DataTable dataTable = Items.SelectLastPOPrice(((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
					if (dataTable.Rows.Count > 0)
					{
						((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
						((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).Rows[j]);
					}
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		((TextEditorControlBase)cboPaymentMethod).Value = dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["DefaultPaymentMethodID"];
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void cboPaymentMethod_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PaymentMethods(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboPaymentMethod).Value = num;
			}
		}
	}

	private void btnPaymentMethodSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PaymentMethods(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboPaymentMethod).Value = num;
		}
	}

	private void frmPSInvoices_Load(object sender, EventArgs e)
	{
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSuppliers);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + " ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboSupplier, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void btnStoreSearch_Click(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkStore).Checked && (Adding || Updating))
		{
			int num = SearchFunctions.Stores(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboStore).Value = num;
			}
		}
	}

	private void cboStore_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		if (cboStore.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = ((TextEditorControlBase)cboStore).Value;
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = ((TextEditorControlBase)cboStore).Value;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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

	private void btnGetFromXL_Click(object sender, EventArgs e)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Expected O, but got Unknown
		if (!Adding || !rbIsDirectInvoice.Checked)
		{
			return;
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "*.xls|*.xlsx";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		empty = openFileDialog.FileName;
		empty2 = Path.GetExtension(empty);
		DataTable dataTable = GlobalFunctions.ReadExcel(empty, empty2);
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		dtDetails.Rows.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			DataRow dataRow = null;
			DataRow dataRow2 = null;
			if (dtItems.Select("ItemBarCode = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'").Length == 0)
			{
				GlobalVariables.InformationMB.Show("هذا الباركود غير موجود", "This BarCode Does not Exists");
				return;
			}
			if (UsingColors)
			{
				if (dtColors.Select("ColorName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'").Length == 0)
				{
					GlobalVariables.InformationMB.Show("هذا اللون غير موجود", "This Color Does not Exists");
					return;
				}
				dataRow = dtColors.Select("ColorName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'")[0];
			}
			if (UsingSizes)
			{
				if (dtSizes.Select("ItemSizeName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'").Length == 0)
				{
					GlobalVariables.InformationMB.Show("هذا المقاس غير موجود", "This Size Does not Exists");
					return;
				}
				dataRow2 = dtSizes.Select("ItemSizeName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'")[0];
			}
			DataRow dataRow3 = dtItems.Select("ItemBarCode = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'")[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow3["ItemID"]);
			obj.Value = value;
			if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "الوحدة" : "Unit") && dtUnits.Select("UnitName = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'").Length == 0)
			{
				DataRow dataRow4 = dtUnits.Select("UnitName = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'")[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow4["UnitID"];
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable.Rows[i][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString();
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			if (AutomaticlyAddItemTaxToPurchaseInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataTable.Rows[i][GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateGoss();
			CalculateTotalsTax();
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataRow["ColorID"];
			}
			if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataRow2["ItemSizeID"];
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			CalcTotalQty();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		((UltraGridBase)ULGData).UpdateData();
	}

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSuppliers);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboSupplier, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
		dtDetails.Rows.Clear();
	}

	private void ULGDataExpenses_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGDataExpenses.ActiveCell.Column).Key == "ExpenseID")
		{
			int num = SearchFunctions.ExpensesSearch(IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGDataExpenses).ActiveRow.Cells["ExpenseID"].Value = num;
			}
		}
		e.Handled = true;
	}

	private void ULGDataExpenses_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataExpenses.ActiveCell != null && ((KeyedSubObjectBase)ULGDataExpenses.ActiveCell.Column).Key == "Value")
		{
			GlobalFunctions.CheckForNumbers(ULGDataExpenses.ActiveCell, e);
		}
	}

	private void ULGDataExpenses_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Value" || ((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyValue" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ExchangeRate")
		{
			if (((Control)(object)txtExchangeRate).Text != "" && ((Control)(object)txtExchangeRate).Text != "0")
			{
				e.Cell.Row.Cells["Value"].Value = decimal.Parse((e.Cell.Row.Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(e.Cell.Row.Cells["CurrencyValue"].Value.ToString());
			}
			CalculateNetTotals();
		}
		ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
	}

	private void ULGDataExpenses_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		ULGDataExpenses.CellListSelect -= new CellEventHandler(ULGDataExpenses_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "CurrencyID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataExpenses).UpdateData();
			e.Cell.Row.Cells["ExchangeRate"].Value = dtCurrency.Select(" CurrencyID= " + e.Cell.Value.ToString())[0]["ExchangeRate"].ToString();
			if (((Control)(object)txtExchangeRate).Text != "" && ((Control)(object)txtExchangeRate).Text != "0")
			{
				e.Cell.Row.Cells["Value"].Value = decimal.Parse((e.Cell.Row.Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : e.Cell.Row.Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(e.Cell.Row.Cells["CurrencyValue"].Value.ToString());
			}
		}
		else
		{
			e.Cell.Row.Cells["ExchangeRate"].Value = 0;
		}
		ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		ULGDataExpenses.CellListSelect += new CellEventHandler(ULGDataExpenses_CellListSelect);
	}

	private void ULGDataExpenses_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateNetTotals();
	}

	private void ULGDataExpenses_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "0") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void txtDiscAfterTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text) != 0m)
			{
				((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTableTaxTotalValue).Text == "" || ((Control)(object)txtTableTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTableTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtGrowthTaxTotalValue).Text == "" || ((Control)(object)txtGrowthTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtGrowthTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["PurchaseJVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void ULGDataExpenses_Enter(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		int num = -1;
		for (int num2 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns).Count - 1; num2 >= 0; num2--)
		{
			if (num == -1 && !((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns[num2].Hidden)
			{
				num = num2;
				break;
			}
		}
		((UltraGridBase)ULGDataExpenses).Rows.TemplateAddRow.Cells[num].Activate();
		ULGDataExpenses.PerformAction((UltraGridAction)24);
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Expected O, but got Unknown
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Expected O, but got Unknown
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Expected O, but got Unknown
		clbGoodReceiptNoteNo.SelectedValueChanged -= clbGoodReceiptNoteNo_SelectedValueChanged;
		for (int i = 0; i < clbGoodReceiptNoteNo.Items.Count; i++)
		{
			clbGoodReceiptNoteNo.SetItemChecked(i, ((UltraToggleEditorBase)chkAll).Checked);
		}
		string goodReceiptNotesIds = GetGoodReceiptNotesIds();
		if (goodReceiptNotesIds != "")
		{
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			dtDetails = PSInvoicesDetails.FillByGoodReceiptNoteIDs("," + goodReceiptNotesIds + ",", GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceDetailsExpenses = PSInvoicesDetailsExpenses.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtInvoiceDetailsExpenses);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtInvoiceDetailsExpenses";
			ds.Relations.Add(ds.Tables[0].Columns["PSInvoiceDetailID"], ds.Tables[1].Columns["PSInvoiceDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)6;
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboSupplier.SelectedIndex > -1)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
					((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[j]);
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			if ((rbIsDirectInvoice.Checked || rbIsGoodReceiptNote.Checked) && cboSupplier.SelectedIndex > -1 && dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] == DBNull.Value)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					DataTable dataTable = Items.SelectLastPOPrice(((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
					if (dataTable.Rows.Count > 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
						((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).Rows[k]);
					}
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		clbGoodReceiptNoteNo.SelectedValueChanged += clbGoodReceiptNoteNo_SelectedValueChanged;
	}

	private void clbGoodReceiptNoteNo_SelectedValueChanged(object sender, EventArgs e)
	{
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Expected O, but got Unknown
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Expected O, but got Unknown
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Expected O, but got Unknown
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll).Checked = clbGoodReceiptNoteNo.CheckedItems.Count == clbGoodReceiptNoteNo.Items.Count && clbGoodReceiptNoteNo.Items.Count > 0;
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		string goodReceiptNotesIds = GetGoodReceiptNotesIds();
		if (!(goodReceiptNotesIds != ""))
		{
			return;
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		dtDetails = PSInvoicesDetails.FillByGoodReceiptNoteIDs("," + goodReceiptNotesIds + ",", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceDetailsExpenses = PSInvoicesDetailsExpenses.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtInvoiceDetailsExpenses);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtInvoiceDetailsExpenses";
		ds.Relations.Add(ds.Tables[0].Columns["PSInvoiceDetailID"], ds.Tables[1].Columns["PSInvoiceDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboSupplier.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		if ((!rbIsDirectInvoice.Checked && !rbIsGoodReceiptNote.Checked) || cboSupplier.SelectedIndex <= -1 || dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			DataTable dataTable = Items.SelectLastPOPrice(((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[j]);
			}
		}
		CalculateGoss();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public string GetGoodReceiptNotesIds()
	{
		string text = "";
		for (int i = 0; i < clbGoodReceiptNoteNo.Items.Count; i++)
		{
			if (clbGoodReceiptNoteNo.GetItemChecked(i))
			{
				text = ((!(text == "")) ? (text + "," + dtgoodReceiptNotes.Rows[i]["GoodReceiptNoteID"].ToString()) : (text + dtgoodReceiptNotes.Rows[i]["GoodReceiptNoteID"].ToString()));
			}
		}
		return text;
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		((UltraGridBase)ULGData).UpdateData();
		DataView dataView = new DataView(((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0]);
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

	private void btnGoodReceiptNoteSearch_Click(object sender, EventArgs e)
	{
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Expected O, but got Unknown
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Expected O, but got Unknown
		if (cboSupplier.SelectedIndex <= -1)
		{
			return;
		}
		dtSearchResult = SearchFunctions.GoodReceiptNotesReportBySupplierID(((TextEditorControlBase)cboSupplier).Value.ToString());
		for (int i = 0; i < clbGoodReceiptNoteNo.Items.Count; i++)
		{
			if (dtSearchResult.Select(" GoodReceiptNoteID= " + dtgoodReceiptNotes.Rows[i]["GoodReceiptNoteID"].ToString()).Length != 0)
			{
				clbGoodReceiptNoteNo.SetItemChecked(i, value: true);
			}
		}
		clbGoodReceiptNoteNo.SelectedValueChanged -= clbGoodReceiptNoteNo_SelectedValueChanged;
		string goodReceiptNotesIds = GetGoodReceiptNotesIds();
		if (goodReceiptNotesIds != "")
		{
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			dtDetails = PSInvoicesDetails.FillByGoodReceiptNoteIDs("," + goodReceiptNotesIds + ",", GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceDetailsExpenses = PSInvoicesDetailsExpenses.SelectByPSInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtInvoiceDetailsExpenses);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtInvoiceDetailsExpenses";
			ds.Relations.Add(ds.Tables[0].Columns["PSInvoiceDetailID"], ds.Tables[1].Columns["PSInvoiceDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboSupplier.SelectedIndex > -1)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
					((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[j]);
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			if ((rbIsDirectInvoice.Checked || rbIsGoodReceiptNote.Checked) && cboSupplier.SelectedIndex > -1 && dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["PriceTypeID"] == DBNull.Value)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					DataTable dataTable = Items.SelectLastPOPrice(((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
					if (dataTable.Rows.Count > 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
						((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).Rows[k]);
					}
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		clbGoodReceiptNoteNo.SelectedValueChanged += clbGoodReceiptNoteNo_SelectedValueChanged;
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void txtExchangeRate_ValueChanged(object sender, EventArgs e)
	{
		if (!(((Control)(object)txtExchangeRate).Text != "") || !(decimal.Parse(((Control)(object)txtExchangeRate).Text) > 0m))
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataExpenses).Rows[i].Cells["Value"].Value = decimal.Parse((((UltraGridBase)ULGDataExpenses).Rows[i].Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataExpenses).Rows[i].Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[i].Cells["CurrencyValue"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
			{
				((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Value"].Value = decimal.Parse((((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ExchangeRate"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ExchangeRate"].Value.ToString()) / decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["CurrencyValue"].Value.ToString());
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[j]);
		}
		CalculateTotalsTax();
	}

	private void btnSelectLenses_Click(object sender, EventArgs e)
	{
		if (Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItems'")[0]["OptionValue"].ToString()))
		{
			frmColorSizeSelection frmColorSizeSelection2 = new frmColorSizeSelection(_ViewPrice: true);
			frmColorSizeSelection2.Tag = base.Tag;
			frmColorSizeSelection2.Width = base.Parent.Width;
			frmColorSizeSelection2.Height = base.Parent.Height;
			frmColorSizeSelection2.ShowDialog();
			if (frmColorSizeSelection2.dtColorSizeCrossStructure == null)
			{
				return;
			}
			for (int i = 0; i < frmColorSizeSelection2.dtColorSizeCrossStructure.Rows.Count; i++)
			{
				for (int j = 1; j < frmColorSizeSelection2.dtColorSizeCrossStructure.Columns.Count; j++)
				{
					if (frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString() != "" && decimal.Parse(frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString()) > 0m && ((DataSet)((UltraGridBase)ULGData).DataSource).Tables["dtDetails"].Select(" ItemID= " + frmColorSizeSelection2.ItemID + " And ColorID= " + frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i]["SPH/CYL"].ToString() + " And ItemSizeID= " + frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j].Caption).Length == 0)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmColorSizeSelection2.ItemID);
						obj.Value = value;
						DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
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
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i]["SPH/CYL"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j].Caption;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString());
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
						if (AutomaticlyAddItemTaxToPurchaseInvoice)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = frmColorSizeSelection2.Price;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = frmColorSizeSelection2.Price * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
						if (UsingBatchNoAndValidityPeriod)
						{
							ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
						int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
						ValueList unitsValueList = getUnitsValueList(unitTypeID);
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					}
					CalculateGoss();
					CalculateTotalsTax();
					CalcTotalQty();
				}
			}
			return;
		}
		frmXYSelection frmXYSelection2 = new frmXYSelection(_ViewPrice: true);
		frmXYSelection2.Tag = base.Tag;
		frmXYSelection2.Width = base.Parent.Width;
		frmXYSelection2.Height = base.Parent.Height;
		frmXYSelection2.ShowDialog();
		if (frmXYSelection2.dtXYItems == null || frmXYSelection2.dtXYItems.Rows.Count <= 0)
		{
			return;
		}
		for (int k = 0; k < frmXYSelection2.dtXYItems.Rows.Count; k++)
		{
			for (int l = 1; l < frmXYSelection2.dtXYItems.Columns.Count; l++)
			{
				if (frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString() != "" && decimal.Parse(frmXYSelection2.dtXYItems.Rows[k]["Qty"].ToString()) > 0m && ((DataSet)((UltraGridBase)ULGData).DataSource).Tables["dtDetails"].Select(" ItemID= " + frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString()).Length == 0)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString());
					obj2.Value = value;
					DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(frmXYSelection2.dtXYItems.Rows[k]["Qty"].ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					if (AutomaticlyAddItemTaxToPurchaseInvoice)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow2["TaxID"];
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = frmXYSelection2.Price;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = frmXYSelection2.Price * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList2 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
					int unitTypeID2 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList2 = getUnitsValueList(unitTypeID2);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList2;
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				}
				CalculateGoss();
				CalculateTotalsTax();
				CalcTotalQty();
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
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
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
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected O, but got Unknown
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Expected O, but got Unknown
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_093d: Expected O, but got Unknown
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Expected O, but got Unknown
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Expected O, but got Unknown
		//IL_0f7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Expected O, but got Unknown
		//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb4: Expected O, but got Unknown
		//IL_0fc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Purchasing.Transactions.frmPSInvoices3));
		UltraTab val = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataExpenses = new UltraGrid();
		this.pnlCheckType = new UltraPanel();
		this.rbIsGoodReceiptNote = new System.Windows.Forms.RadioButton();
		this.rbIsPurchaseOrder = new System.Windows.Forms.RadioButton();
		this.rbIsDirectInvoice = new System.Windows.Forms.RadioButton();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboSupplier = new UltraComboEditor();
		this.lblSupplier = new UltraLabel();
		this.txtBarCode = new UltraTextEditor();
		this.lblPurchaseOrder = new UltraLabel();
		this.cboPurchaseOrder = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnSupplierSearch = new UltraButton();
		this.btnPurchaseOrderSearch = new UltraButton();
		this.btnPaymentMethodSearch = new UltraButton();
		this.lblPaymentMethod = new UltraLabel();
		this.cboPaymentMethod = new UltraComboEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDiscAfterTaxRatio = new UltraLabel();
		this.txtDiscAfterTaxRatio = new UltraTextEditor();
		this.lblDiscAfterTaxValue = new UltraLabel();
		this.txtDiscAfterTaxValue = new UltraTextEditor();
		this.lblCommercialTax = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.lblStampValue = new UltraLabel();
		this.txtStampValue = new UltraTextEditor();
		this.lblGrowthFees = new UltraLabel();
		this.txtGrowthFees = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.cboTax = new UltraComboEditor();
		this.chkTax = new UltraCheckEditor();
		this.cboItemBatchs = new UltraComboEditor();
		this.btnJV = new UltraButton();
		this.chkAll = new UltraCheckEditor();
		this.clbGoodReceiptNoteNo = new System.Windows.Forms.CheckedListBox();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.btnGoodReceiptNoteSearch = new UltraButton();
		this.btnSelectLenses = new UltraButton();
		this.lblBalance = new UltraLabel();
		this.txtBranchBalance = new UltraTextEditor();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.btnStoreSearch = new UltraButton();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.btnGetFromXL = new UltraButton();
		this.lblTable = new UltraLabel();
		this.txtTableTaxTotalValue = new UltraTextEditor();
		this.lblGrowthTax = new UltraLabel();
		this.txtGrowthTaxTotalValue = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseOrder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemBatchs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthTaxTotalValue).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
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
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		this.ULGDataExpenses.AfterRowsDeleted += new System.EventHandler(ULGDataExpenses_AfterRowsDeleted);
		this.ULGDataExpenses.CellListSelect += new CellEventHandler(ULGDataExpenses_CellListSelect);
		this.ULGDataExpenses.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataExpenses_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Enter += new System.EventHandler(ULGDataExpenses_Enter);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataExpenses_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataExpenses_KeyPress);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val15, "appearance23");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsGoodReceiptNote);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsPurchaseOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirectInvoice);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsGoodReceiptNote, "rbIsGoodReceiptNote");
		this.rbIsGoodReceiptNote.BackColor = System.Drawing.Color.Transparent;
		this.rbIsGoodReceiptNote.Name = "rbIsGoodReceiptNote";
		this.rbIsGoodReceiptNote.TabStop = true;
		this.rbIsGoodReceiptNote.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsPurchaseOrder, "rbIsPurchaseOrder");
		this.rbIsPurchaseOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbIsPurchaseOrder.Name = "rbIsPurchaseOrder";
		this.rbIsPurchaseOrder.TabStop = true;
		this.rbIsPurchaseOrder.UseVisualStyleBackColor = false;
		this.rbIsPurchaseOrder.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDirectInvoice, "rbIsDirectInvoice");
		this.rbIsDirectInvoice.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirectInvoice.Name = "rbIsDirectInvoice";
		this.rbIsDirectInvoice.TabStop = true;
		this.rbIsDirectInvoice.UseVisualStyleBackColor = false;
		this.rbIsDirectInvoice.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		((TextEditorControlBase)this.cboSupplier).ValueChanged += new System.EventHandler(cboSupplier_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSupplier).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSupplier_KeyDown);
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblPurchaseOrder, "lblPurchaseOrder");
		this.lblPurchaseOrder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseOrder).Name = "lblPurchaseOrder";
		((ControlBase)this.lblPurchaseOrder).WrapText = false;
		resources.ApplyResources(this.cboPurchaseOrder, "cboPurchaseOrder");
		((TextEditorControlBase)this.cboPurchaseOrder).AlwaysInEditMode = true;
		this.cboPurchaseOrder.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPurchaseOrder).Name = "cboPurchaseOrder";
		((TextEditorControlBase)this.cboPurchaseOrder).ValueChanged += new System.EventHandler(cboPurchaseOrder_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboPurchaseOrder).KeyDown += new System.Windows.Forms.KeyEventHandler(cboPurchaseOrder_KeyDown);
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((TextEditorControlBase)this.txtExchangeRate).ValueChanged += new System.EventHandler(txtExchangeRate_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.btnSupplierSearch, "btnSupplierSearch");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance14");
		((ControlBase)this.btnSupplierSearch).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Name = "btnSupplierSearch";
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Click += new System.EventHandler(btnSupplierSearch_Click);
		resources.ApplyResources(this.btnPurchaseOrderSearch, "btnPurchaseOrderSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance24");
		((ControlBase)this.btnPurchaseOrderSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnPurchaseOrderSearch).Name = "btnPurchaseOrderSearch";
		((System.Windows.Forms.Control)(object)this.btnPurchaseOrderSearch).Click += new System.EventHandler(btnPurchaseOrderSearch_Click);
		resources.ApplyResources(this.btnPaymentMethodSearch, "btnPaymentMethodSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance25");
		((ControlBase)this.btnPaymentMethodSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Name = "btnPaymentMethodSearch";
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Click += new System.EventHandler(btnPaymentMethodSearch_Click);
		resources.ApplyResources(this.lblPaymentMethod, "lblPaymentMethod");
		this.lblPaymentMethod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentMethod).Name = "lblPaymentMethod";
		((ControlBase)this.lblPaymentMethod).WrapText = false;
		resources.ApplyResources(this.cboPaymentMethod, "cboPaymentMethod");
		((TextEditorControlBase)this.cboPaymentMethod).AlwaysInEditMode = true;
		this.cboPaymentMethod.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).Name = "cboPaymentMethod";
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).KeyDown += new System.Windows.Forms.KeyEventHandler(cboPaymentMethod_KeyDown);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
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
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		this.lblCommercialTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		((ControlBase)this.lblCommercialTax).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblStampValue, "lblStampValue");
		this.lblStampValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStampValue).Name = "lblStampValue";
		((ControlBase)this.lblStampValue).WrapText = false;
		resources.ApplyResources(this.txtStampValue, "txtStampValue");
		((System.Windows.Forms.Control)(object)this.txtStampValue).Name = "txtStampValue";
		((TextEditorControlBase)this.txtStampValue).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtStampValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrowthFees, "lblGrowthFees");
		this.lblGrowthFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthFees).Name = "lblGrowthFees";
		((ControlBase)this.lblGrowthFees).WrapText = false;
		resources.ApplyResources(this.txtGrowthFees, "txtGrowthFees");
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).Name = "txtGrowthFees";
		((TextEditorControlBase)this.txtGrowthFees).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		resources.ApplyResources(this.cboItemBatchs, "cboItemBatchs");
		((TextEditorControlBase)this.cboItemBatchs).AlwaysInEditMode = true;
		this.cboItemBatchs.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemBatchs).Name = "cboItemBatchs";
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance26");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.clbGoodReceiptNoteNo, "clbGoodReceiptNoteNo");
		this.clbGoodReceiptNoteNo.CheckOnClick = true;
		this.clbGoodReceiptNoteNo.FormattingEnabled = true;
		this.clbGoodReceiptNoteNo.Name = "clbGoodReceiptNoteNo";
		this.clbGoodReceiptNoteNo.SelectedValueChanged += new System.EventHandler(clbGoodReceiptNoteNo_SelectedValueChanged);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.btnGoodReceiptNoteSearch, "btnGoodReceiptNoteSearch");
		((AppearanceBase)val20).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val20, "appearance27");
		((ControlBase)this.btnGoodReceiptNoteSearch).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.btnGoodReceiptNoteSearch).Name = "btnGoodReceiptNoteSearch";
		((System.Windows.Forms.Control)(object)this.btnGoodReceiptNoteSearch).Click += new System.EventHandler(btnGoodReceiptNoteSearch_Click);
		resources.ApplyResources(this.btnSelectLenses, "btnSelectLenses");
		((AppearanceBase)val21).Image = resources.GetObject("appearance28.Image");
		resources.ApplyResources(val21, "appearance28");
		((ControlBase)this.btnSelectLenses).Appearance = (AppearanceBase)(object)val21;
		((UltraButtonBase)this.btnSelectLenses).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnSelectLenses).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSelectLenses).Name = "btnSelectLenses";
		((System.Windows.Forms.Control)(object)this.btnSelectLenses).Click += new System.EventHandler(btnSelectLenses_Click);
		resources.ApplyResources(this.lblBalance, "lblBalance");
		this.lblBalance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		resources.ApplyResources(this.txtBranchBalance, "txtBranchBalance");
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).Name = "txtBranchBalance";
		((EditorButtonControlBase)this.txtBranchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).TabStop = false;
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val22, "appearance29");
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		((UltraButtonBase)this.btnStoreSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val23, "appearance30");
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Click += new System.EventHandler(btnStoreSearch_Click);
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		resources.ApplyResources(this.cboStore, "cboStore");
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.btnGetFromXL, "btnGetFromXL");
		((System.Windows.Forms.Control)(object)this.btnGetFromXL).Name = "btnGetFromXL";
		((System.Windows.Forms.Control)(object)this.btnGetFromXL).Click += new System.EventHandler(btnGetFromXL_Click);
		resources.ApplyResources(this.lblTable, "lblTable");
		this.lblTable.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTable).Name = "lblTable";
		((ControlBase)this.lblTable).WrapText = false;
		resources.ApplyResources(this.txtTableTaxTotalValue, "txtTableTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTableTaxTotalValue).Name = "txtTableTaxTotalValue";
		((EditorButtonControlBase)this.txtTableTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblGrowthTax, "lblGrowthTax");
		this.lblGrowthTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthTax).Name = "lblGrowthTax";
		((ControlBase)this.lblGrowthTax).WrapText = false;
		resources.ApplyResources(this.txtGrowthTaxTotalValue, "txtGrowthTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtGrowthTaxTotalValue).Name = "txtGrowthTaxTotalValue";
		((EditorButtonControlBase)this.txtGrowthTaxTotalValue).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTable);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTableTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrowthTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrowthTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetFromXL);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectLenses);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGoodReceiptNoteSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add(this.clbGoodReceiptNoteNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseOrderSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSupplierSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemBatchs);
		base.Name = "frmPSInvoices3";
		base.Load += new System.EventHandler(frmPSInvoices_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemBatchs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPurchaseOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPurchaseOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSupplierSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPurchaseOrderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex(this.clbGoodReceiptNoteNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGoodReceiptNoteSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectLenses, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetFromXL, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrowthTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrowthTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTableTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTable, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseOrder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemBatchs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthTaxTotalValue).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
