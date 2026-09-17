using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.Production;
using BusinessLayer.Purchasing;
using BusinessLayer.Sales;
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

namespace ERP.Sales.Transactions;

public class frmSLInvoices2 : frmHeaderManyDetails
{
	private DataTable dtBranches;

	private DataTable dtItems;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtCostCenters;

	private DataTable dtUnits;

	private DataTable dtTaxs;

	private DataTable dtExpenses;

	private DataTable dtClients;

	private DataTable dtReports;

	private DataTable dtSLQuotation;

	private DataTable dtProductionRequest;

	private DataTable dtCurrency;

	private DataTable dtPaymentMethod;

	private DataTable dtSLQuotationDetails;

	private DataTable dtInvoiceExpenses;

	private DataTable dtMaterialIssueVouchers;

	private DataTable dtItemPrices;

	private DataTable dtPOSDefaultStore;

	private DataTable dtSalesMan;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlInvoiceExpenses = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UseCostCenters;

	private bool UseSalesSeasons = false;

	private bool HidePrice = false;

	private int rowIndex = -1;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private IContainer components = null;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblQuotationNo;

	private UltraComboEditor cboQuotation;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnClientSearch;

	public UltraButton btnQuotationSearch;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsQuotation;

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

	private RadioButton rbIsMaterialIssueVoucher;

	protected internal UltraCheckEditor chkAll;

	protected internal CheckedListBox clbMaterialIssueVoucherNo;

	public UltraButton btnJV;

	private UltraTextEditor txtSerial;

	private UltraLabel lblSerial;

	private UltraLabel lblCostCenter;

	private UltraComboEditor cboCostCenter;

	private RadioButton rbIsProductionRequest;

	public UltraButton btnRequestSearch;

	private UltraLabel lblRequestNo;

	private UltraComboEditor cboRequestNo;

	private UltraTextEditor txtSubTruckNo;

	private UltraLabel lblSubTruckNo;

	private UltraTextEditor txtTruckNo;

	private UltraLabel lblTruckNo;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	public UltraButton btnStockBalance;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMan;

	public UltraButton btnSalesManSearch;

	private UltraLabel lblBalance;

	public UltraButton btnClientBalance;

	private UltraTextEditor txtBranchBalance;

	public UltraButton btnCostCenterSearch;

	public UltraButton btnStoreSearch;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	private UltraTextEditor txtDiscount;

	private UltraLabel txtDisc;

	private UltraComboEditor cboClientCode;

	private UltraCheckEditor chkWithoutMIV;

	public frmSLInvoices2()
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
		InitializeComponent();
		TableName = "SL_SLInvoices";
		IDCol = "SLInvoiceID";
		NoCol = "SLInvoiceNo";
		DateCol = "SLInvoiceDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public frmSLInvoices2(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		rbIsQuotation.Text = GlobalFunctions.GetFormName("Sales", "Transactions", "frmQuotations");
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
		UseSalesSeasons = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from SL_SalesSeasons Where Deleted = 0").Rows[0][0].ToString()) > 0;
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseCostCenters)
		{
			UltraLabel obj = lblCostCenter;
			bool visible = (((Control)(object)cboCostCenter).Visible = true);
			((Control)(object)obj).Visible = visible;
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		}
		else
		{
			UltraLabel obj2 = lblCostCenter;
			bool visible = (((Control)(object)cboCostCenter).Visible = false);
			((Control)(object)obj2).Visible = visible;
		}
		dtProductionRequest = ProductionRequests.FillCombo(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboRequestNo, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
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
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtSLQuotation = BusinessLayer.Sales.Quotations.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
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
		dtExpenses = BusinessLayer.Purchasing.Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceExpenses.ValueListItems.Clear();
		for (int num = 0; num < dtExpenses.Rows.Count; num++)
		{
			vlInvoiceExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxs.Rows.Count; num2++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		FillCurrencyDropDown();
		dtDetails = SLInvoicesDetails.SelectBySLInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceExpenses = SLInvoicesExpenses.SelectBySLInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataExpenses).DataSource = dtInvoiceExpenses;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataExpenses);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].DefaultCellValue = -1;
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		if (GlobalFunctions.GetOption("SalesVirtualQuantity"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].Hidden = false;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية وهمية" : "Virtual Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الخصم" : "Discount Ratio");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Header).Caption = (GlobalVariables.IsArabic ? "الرصيد" : "Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IssuedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VirtualQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SLInvoiceExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlInvoiceExpenses;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = SLInvoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_0abb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)cboSalesMan).ValueChanged -= cboSalesMan_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
			GlobalFunctions.FillCombo(cboRequestNo, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["SLInvoiceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SLInvoiceDate"];
			((TextEditorControlBase)cboCostCenter).Value = drMaster["CostCenterID"];
			((Control)(object)txtTruckNo).Text = drMaster["TruckNo"].ToString();
			((Control)(object)txtSubTruckNo).Text = drMaster["SubTruckno"].ToString();
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((UltraToggleEditorBase)chkWithoutMIV).Checked = Convert.ToBoolean(drMaster["WithoutMIV"]);
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClientCode).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboQuotation).Value = drMaster["QuotationID"];
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboRequestNo).ValueChanged -= cboRequestNo_ValueChanged;
			((TextEditorControlBase)cboRequestNo).Value = drMaster["ProductionRequestID"];
			((TextEditorControlBase)cboRequestNo).ValueChanged += cboRequestNo_ValueChanged;
			rbIsDirectInvoice.Checked = bool.Parse(drMaster["IsDirectInvoice"].ToString());
			rbIsQuotation.Checked = bool.Parse(drMaster["IsQuotation"].ToString());
			rbIsMaterialIssueVoucher.Checked = bool.Parse(drMaster["IsMaterialIssueVoucher"].ToString());
			rbIsProductionRequest.Checked = bool.Parse(drMaster["IsProductionRequest"].ToString());
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboPaymentMethod).Value = drMaster["PaymentMethodID"];
			((TextEditorControlBase)cboSalesMan).ValueChanged -= cboSalesMan_ValueChanged;
			((TextEditorControlBase)cboSalesMan).Value = drMaster["EmployeeID"];
			((TextEditorControlBase)cboSalesMan).ValueChanged += cboSalesMan_ValueChanged;
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
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo"].ToString() + ")قيد") : ("No( " + drMaster["JVNo"].ToString() + " )"));
			((Control)(object)btnJV).Visible = ((drMaster["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = SLInvoicesDetails.SelectBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceExpenses = SLInvoicesExpenses.SelectBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (rbIsMaterialIssueVoucher.Checked)
			{
				((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
				clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
				dtMaterialIssueVouchers = Main.ExecuteQuery_DataTable(" Select MaterialIssueVoucherID , (MaterialIssueVoucherNo+' - '+ Convert (varchar ,MaterialIssueVoucherDate,103)+' '+Convert (varchar ,MaterialIssueVoucherDate,108))  as MaterialIssueVoucherNo from  SC_MaterialIssueVouchers Where SLInvoiceID =" + drMaster["SLInvoiceID"].ToString());
				Main.Fillclb(clbMaterialIssueVoucherNo, dtMaterialIssueVouchers, "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
				for (int i = 0; i < clbMaterialIssueVoucherNo.Items.Count; i++)
				{
					clbMaterialIssueVoucherNo.SetItemChecked(i, value: true);
				}
				((UltraToggleEditorBase)chkAll).Checked = true;
				((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
				clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
			}
			((UltraGridBase)ULGData).DataSource = dtDetails;
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
			((TextEditorControlBase)cboSalesMan).ValueChanged += cboSalesMan_ValueChanged;
			dtpDate.ValueChanged += dtpDate_ValueChanged;
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
		((EditorButtonControlBase)cboCostCenter).ReadOnly = NavMode;
		rbIsDirectInvoice.Enabled = !NavMode && !Updating;
		rbIsQuotation.Enabled = !NavMode && !Updating;
		rbIsMaterialIssueVoucher.Enabled = !NavMode && !Updating;
		rbIsProductionRequest.Enabled = !NavMode && !Updating;
		((Control)(object)chkTax).Visible = !NavMode;
		((Control)(object)cboTax).Visible = !NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode || (rbIsDirectInvoice.Checked ? NavMode : (!Adding && (rbIsMaterialIssueVoucher.Checked || rbIsQuotation.Checked || rbIsProductionRequest.Checked)));
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode || (rbIsDirectInvoice.Checked ? NavMode : (!Adding && (rbIsMaterialIssueVoucher.Checked || rbIsQuotation.Checked || rbIsProductionRequest.Checked)));
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((EditorButtonControlBase)cboQuotation).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboRequestNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTruckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSubTruckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode;
		((Control)(object)btnCostCenterSearch).Visible = !NavMode && UseCostCenters;
		((Control)(object)btnImport).Visible = Adding;
		((Control)(object)chkWithoutMIV).Enabled = !NavMode && (Adding || (drMaster != null && dtDetails.Rows.Count > 0 && dtDetails.Select(" IssuedQty > 0 ").Length != 0));
		((EditorButtonControlBase)txtDiscAfterTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStampValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGrowthFees).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscount).ReadOnly = NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnStockBalance).Visible = Adding || Updating;
		((Control)(object)btnClientSearch).Visible = !Updating && (rbIsDirectInvoice.Checked ? (!NavMode) : (Adding && (rbIsMaterialIssueVoucher.Checked || rbIsQuotation.Checked)));
		((Control)(object)btnQuotationSearch).Visible = Adding && rbIsQuotation.Checked;
		((Control)(object)btnRequestSearch).Visible = Adding && rbIsProductionRequest.Checked;
		((Control)(object)btnPaymentMethodSearch).Visible = !NavMode;
		((Control)(object)btnSalesManSearch).Visible = !NavMode;
		clbMaterialIssueVoucherNo.Enabled = Adding;
		((Control)(object)chkAll).Enabled = Adding;
		((Control)(object)txtBranchBalance).Visible = !NavMode;
		((Control)(object)lblBalance).Visible = !NavMode;
		((Control)(object)btnClientBalance).Visible = !NavMode;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
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
			DataView dataView3 = new DataView(dtSLQuotation);
			dataView3.RowFilter = " Closed= 0 And  BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView3.ToTable(), "QuotationID", "QuotationNo");
			object value = ((TextEditorControlBase)cboSalesMan).Value;
			DataView dataView4 = new DataView(dtSalesMan);
			dataView4.RowFilter = "BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView4.ToTable();
			GlobalFunctions.FillCombo(cboSalesMan, dt, "SubAccountID", "SubAccountName");
			((TextEditorControlBase)cboSalesMan).Value = value;
		}
		else
		{
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
			GlobalFunctions.FillCombo(cboRequestNo, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
			}
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
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
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(rbIsMaterialIssueVoucher.Checked ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtDiscount).ValueChanged -= txtDiscount_ValueChanged;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? SLInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtTruckNo).Clear();
		((TextEditorControlBase)txtSubTruckNo).Clear();
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
		cboQuotation.SelectedIndex = -1;
		((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
		((TextEditorControlBase)cboRequestNo).ValueChanged -= cboRequestNo_ValueChanged;
		cboRequestNo.SelectedIndex = -1;
		((TextEditorControlBase)cboRequestNo).ValueChanged += cboRequestNo_ValueChanged;
		cboPaymentMethod.SelectedIndex = -1;
		rbIsQuotation.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsQuotation.Checked = false;
		rbIsQuotation.CheckedChanged += RadioButtons_CheckedChanged;
		rbIsMaterialIssueVoucher.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsMaterialIssueVoucher.Checked = false;
		rbIsMaterialIssueVoucher.CheckedChanged += RadioButtons_CheckedChanged;
		rbIsProductionRequest.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsProductionRequest.Checked = false;
		rbIsProductionRequest.CheckedChanged += RadioButtons_CheckedChanged;
		rbIsDirectInvoice.Checked = true;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		cboCostCenter.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtExchangeRate).Text = "0";
		cboSalesMan.SelectedIndex = -1;
		((UltraToggleEditorBase)chkWithoutMIV).Checked = false;
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((Control)(object)txtDiscount).Text = "0";
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
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
		((TextEditorControlBase)txtBranchBalance).Clear();
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataExpenses).DataSource).Rows.Clear();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtStampValue).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtGrowthFees).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtDiscount).ValueChanged += txtDiscount_ValueChanged;
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
		if (rbIsQuotation.Checked && cboQuotation.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض السعر" : "Please Select Quotation No");
			((TextEditorControlBase)cboQuotation).Focus();
			cboQuotation.DropDown();
			return false;
		}
		if (rbIsProductionRequest.Checked && cboRequestNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم الطلبية" : "Please Select Production Request No");
			((TextEditorControlBase)cboRequestNo).Focus();
			cboRequestNo.DropDown();
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
		if (UseCostCenters && GlobalFunctions.GetOption("EnforceCostCentersUse") && cboCostCenter.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار مركز تكلفة", "Please choose Cost-Center");
			((TextEditorControlBase)cboCostCenter).Focus();
			cboCostCenter.DropDown();
			return false;
		}
		if (UseSalesSeasons && int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*)  AS Counter From SL_SalesSeasons Where    '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "' >=FromDate And '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'<= ToDate And Closed=0 And Deleted=0 ").Rows[0]["Counter"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("تاريخ الاذن غير موجود فى اى موسم مفتوح برجاء مراجعة المواسم", "Voucher Date Not Exsist With Any Season Please Check Sales Season ");
			return false;
		}
		if (UseSalesSeasons)
		{
			DataTable table = SLInvoices.SalesSeasonsValidation((DataTable)((UltraGridBase)ULGData).DataSource, Adding ? "-1" : drMaster["SLInvoiceID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
			DataView dataView = new DataView(table);
			dataView.RowFilter = " Diff >0 ";
			if (dataView.Count > 0)
			{
				frmSalesSeasonsValidation frmSalesSeasonsValidation2 = new frmSalesSeasonsValidation(dataView.ToTable());
				frmSalesSeasonsValidation2.WindowState = FormWindowState.Normal;
				frmSalesSeasonsValidation2.ShowDialog();
				return false;
			}
		}
		else if (Main.CheckForValueByBranchIDAndFiscalYearID("SL_SLInvoices", "SLInvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["SLInvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = SLInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		decimal num = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["CreditLimit"].ToString());
		if (decimal.Parse((((Control)(object)txtBranchBalance).Text == "") ? "0" : ((Control)(object)txtBranchBalance).Text) + decimal.Parse(((Control)(object)txtNetprice).Text) - ((Adding || (drMaster != null && drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboClient).Value.ToString())) ? 0m : ((drMaster != null) ? decimal.Parse(drMaster["NetPrice"].ToString()) : decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text))) > num && num != 0m)
		{
			GlobalVariables.InformationMB.Show("  الحد الاقصى لإئتمان العميل \n" + num, " Max Client Credit Limit \n" + num);
			return false;
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
			if ((GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoice") || GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation")) && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !rbIsMaterialIssueVoucher.Checked && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاءإختيار المخزن  ", "Please Select Store ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
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
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataExpenses).Rows[j].Cells["ExpenseID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense Name ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["ExpenseID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[j].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[j].Cells["Value"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[j].Cells["Value"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesReturnsAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب مردودات المبيعات من حسابات النظام  ", "Please Select Sales Returns Account From SystemAccounts ");
			return false;
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
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Expected O, but got Unknown
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Expected O, but got Unknown
		//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e13: Expected O, but got Unknown
		//IL_0ed4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ede: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			string text = "";
			decimal invoiceExpense = CalculateInvoiceExpense();
			num = SLInvoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", rbIsMaterialIssueVoucher.Checked ? "1" : "0", rbIsProductionRequest.Checked ? "1" : "0", "0", ((TextEditorControlBase)cboClient).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboRequestNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRequestNo).Value.ToString(), "Null", (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), ((Control)(object)txtTruckNo).Text, ((Control)(object)txtSubTruckNo).Text, "Null", "Null", "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, "Null", ((Control)(object)txtNotes).Text, "0", ((UltraToggleEditorBase)chkWithoutMIV).Checked ? "1" : "0", "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (rbIsMaterialIssueVoucher.Checked)
			{
				text = GetMaterialIssueVouchersIds();
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRowActualUnitSalesPriceAndReturnedPrice(((UltraGridBase)ULGData).Rows[i], invoiceExpense);
				int num2 = SLInvoicesDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Index.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["PackageCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["PackageCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["VirtualQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["VirtualQty"].Value.ToString(), "0", (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["DiscountAfterTax"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["DiscountAfterTax"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["StampsValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["StampsValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["GrowthFees"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["GrowthFees"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ReturnPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["ReturnPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				if (rbIsMaterialIssueVoucher.Checked && text != "")
				{
					Main.ExecuteNonQuery(" Update SC_MaterialIssueVouchersDetails Set SLInvoiceDetailID= " + num2 + " ,UnitPrice =" + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ActualUnitSalesPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text) + "Where MaterialIssueVoucherID in ( " + text + ") And ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + " And BatchID " + ((((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value) ? " is null" : ("= " + ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString())) + "  And  UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString());
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count > 0)
			{
				ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["SLInvoiceExpenseID"].Value = -1;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["SLInvoiceID"].Value = num;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				SLInvoicesExpenses.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataExpenses).DataSource, GlobalVariables.UserID);
			}
			if (rbIsMaterialIssueVoucher.Checked && text != "")
			{
				Main.ExecuteNonQuery(" Update SC_MaterialIssueVouchers Set SLInvoiceID = " + num + " Where MaterialIssueVoucherID in ( " + text + ")");
				Main.ExecuteNonQuery(" Insert Into Trans_Log Select " + GlobalVariables.UserID + ",'SC_MaterialIssueVouchers',MaterialIssueVoucherID,GetDate(),'Inv'  From SC_MaterialIssueVouchers Where MaterialIssueVoucherID in ( " + text + ")");
			}
			SLInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			string text2 = "";
			if (GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation") && !((UltraToggleEditorBase)chkWithoutMIV).Checked && !rbIsMaterialIssueVoucher.Checked)
			{
				text2 = MaterialIssueVouchersDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
			}
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2 + " لم يتم حفظ إذن الصرف ", text2 + " Erro Occured When Creating Material issue Voucher");
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				RowID = num.ToString();
				ItemsTransactions.ManageInThread();
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (!GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoice") || ((UltraToggleEditorBase)chkWithoutMIV).Checked || GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation") || rbIsMaterialIssueVoucher.Checked)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد إنشاء إذن صرف ؟", "Are You Sure You want to Create Material Issue Voucher?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			string text3 = MaterialIssueVouchersDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
			if (text3 != "")
			{
				GlobalVariables.InformationMB.Show(text3 + " لم يتم حفظ إذن الصرف ", text3 + " Erro Occured When Creating Material issue Voucher");
				Main.RollbackBulkTrans(FromServer: false);
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطأ فى إنشاء إذن الصرف لم يتم الحفظ " : "Error Occured When Creating Material Issue Voucher");
		}
	}

	public override void UpdateData()
	{
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Expected O, but got Unknown
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Expected O, but got Unknown
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Expected O, but got Unknown
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = SLInvoices.Insert_Update(drMaster["SLInvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", rbIsMaterialIssueVoucher.Checked ? "1" : "0", rbIsProductionRequest.Checked ? "1" : "0", "0", ((TextEditorControlBase)cboClient).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboRequestNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRequestNo).Value.ToString(), "Null", (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), ((Control)(object)txtTruckNo).Text, ((Control)(object)txtSubTruckNo).Text, "Null", "Null", bool.Parse(drMaster["IsTruckPolicy"].ToString()) ? "1" : "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, "Null", ((Control)(object)txtNotes).Text, "0", ((UltraToggleEditorBase)chkWithoutMIV).Checked ? "1" : "0", (drMaster["EInvoiceInternalCode"] == DBNull.Value) ? "Null" : drMaster["EInvoiceInternalCode"].ToString(), (drMaster["EInvoiceUUID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceUUID"].ToString(), (drMaster["EInvoiceSenDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceSenDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceSendUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceSendUserID"].ToString(), bool.Parse(drMaster["EInvoiceIsCanceled"].ToString()) ? "1" : "0", (drMaster["EInvoiceCanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceCanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceCanceledUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceCanceledUserID"].ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			decimal invoiceExpense = CalculateInvoiceExpense();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["RowIndex"].Value = ((UltraGridBase)ULGData).Rows[i].Index;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceDetailID"].Value.ToString() + ",";
				CalculateRowActualUnitSalesPriceAndReturnedPrice(((UltraGridBase)ULGData).Rows[i], invoiceExpense);
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("SL_SLInvoicesDetails", "SLInvoiceID", drMaster["SLInvoiceID"].ToString(), "SLInvoiceDetailID", text);
			SLInvoicesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			SLInvoicesExpenses.DeleteBySLInvoiceID(num.ToString(), GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count > 0)
			{
				ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["SLInvoiceExpenseID"].Value = -1;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["SLInvoiceID"].Value = num;
					((UltraGridBase)ULGDataExpenses).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
				SLInvoicesExpenses.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataExpenses).DataSource, GlobalVariables.UserID);
			}
			if ((rbIsDirectInvoice.Checked || rbIsQuotation.Checked) && drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboClient).Value.ToString())
			{
				Main.ExecuteNonQuery(" Update SC_MaterialIssueVouchers set SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString() + " Where SLInvoiceID=  " + drMaster["SLInvoiceID"].ToString());
			}
			SLInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch (Exception)
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
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_MaterialIssueVouchers Where Deleted=0 AND SLInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0 && !rbIsMaterialIssueVoucher.Checked)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تمت صرفها من المخازن", "Cannot Delete This Transaction Because It Added in The Store ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_ClientsDepartmentsReturns Where Deleted=0 AND SLInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم ارتجاعها فى المخازن", "Cannot Delete This Transaction Because It Returned in The Store ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SL_SLInvoicesGroupDetails Where Deleted=0 AND SLInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه موجوده في فاتورة مجمعه", "Cannot Delete This Transaction Because It Is On a Group Invoice ");
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			SLInvoices.DeleteVirtual(drMaster["SLInvoiceID"].ToString(), GlobalVariables.UserID);
			SLInvoicesDetails.DeleteVirtualBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.UserID);
			SLInvoicesExpenses.DeleteVirtualBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.UserID);
			if (rbIsMaterialIssueVoucher.Checked)
			{
				Main.ExecuteNonQuery(" Update SC_MaterialIssueVouchers set SLInvoiceID=null Where SLInvoiceID =" + drMaster["SLInvoiceID"].ToString());
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SL_SLInvoices_A.rpt" : "Rep_SL_SLInvoices_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", "," + RowID + ",", "Tax name");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLInvoicesReport(-1, 0, -1, 0, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SLInvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboCurrency).Value;
		object value2 = ((TextEditorControlBase)cboClient).Value;
		object value3 = ((TextEditorControlBase)cboCostCenter).Value;
		object value4 = ((TextEditorControlBase)cboItemBatchs).Value;
		object value5 = ((TextEditorControlBase)cboPaymentMethod).Value;
		object value6 = ((TextEditorControlBase)cboQuotation).Value;
		object value7 = ((TextEditorControlBase)cboRequestNo).Value;
		object value8 = ((TextEditorControlBase)cboSalesMan).Value;
		object value9 = ((TextEditorControlBase)cboTax).Value;
		object value10 = ((TextEditorControlBase)cboTransactionBranch).Value;
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
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseCostCenters)
		{
			UltraLabel obj = lblCostCenter;
			bool visible = (((Control)(object)cboCostCenter).Visible = true);
			((Control)(object)obj).Visible = visible;
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		}
		else
		{
			UltraLabel obj2 = lblCostCenter;
			bool visible = (((Control)(object)cboCostCenter).Visible = false);
			((Control)(object)obj2).Visible = visible;
		}
		dtPOSDefaultStore = Main.ExecuteQuery_DataTable(" Select DefaultStoreID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID);
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
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSLQuotation = BusinessLayer.Sales.Quotations.FillCombo(GlobalVariables.BranchIDs, "-1");
		dtProductionRequest = ProductionRequests.FillCombo(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboRequestNo, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		DataView dataView = new DataView(dtStores);
		dataView.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView.ToTable();
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dataTable.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[l]["StoreID"], dataTable.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dataTable, "StoreID", "StoreName");
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView2.ToTable();
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
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
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
		dtExpenses = BusinessLayer.Purchasing.Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceExpenses.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtExpenses.Rows.Count; num2++)
		{
			vlInvoiceExpenses.ValueListItems.Add(dtExpenses.Rows[num2]["ExpenseID"], dtExpenses.Rows[num2]["ExpenseName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtTaxs.Rows.Count; num3++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num3]["TaxID"], dtTaxs.Rows[num3]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		FillCurrencyDropDown();
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView3 = new DataView(dtClients);
			dataView3.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView3.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView3.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
		((TextEditorControlBase)cboCurrency).Value = value;
		((TextEditorControlBase)cboClient).Value = value2;
		((TextEditorControlBase)cboCostCenter).Value = value3;
		((TextEditorControlBase)cboItemBatchs).Value = value4;
		((TextEditorControlBase)cboPaymentMethod).Value = value5;
		((TextEditorControlBase)cboQuotation).Value = value6;
		((TextEditorControlBase)cboRequestNo).Value = value7;
		((TextEditorControlBase)cboSalesMan).Value = value8;
		((TextEditorControlBase)cboTax).Value = value9;
		((TextEditorControlBase)cboTransactionBranch).Value = value10;
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
		//IL_1243: Unknown result type (might be due to invalid IL or missing references)
		//IL_124d: Expected O, but got Unknown
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1265: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value = 0;
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : dataRow["DefaultStoreID"]);
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + e.Cell.Value.ToString()) : (" ItemID = " + e.Cell.Value.ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
				}
				else
				{
					UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"];
					object obj4 = (((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = 0);
					value = (obj3.Value = obj4);
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
			((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value = 0;
			if (e.Cell.Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				if (bool.Parse(dataRow2["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow[] array2 = dtItemPrices.Select(" UnitID = " + e.Cell.Value.ToString() + " and ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
					if (array2 != null && array2.Length != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array2[0]["Price"].ToString());
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((decimal.Parse(array2[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array2[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
					}
					else
					{
						UltraGridCell obj7 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
						UltraGridCell obj8 = ((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"];
						object obj4 = (((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = 0);
						object value = (obj8.Value = obj4);
						obj7.Value = value;
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
			((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value = 0;
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value = 0;
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow3 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num3);
				obj11.Value = value;
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StockBalance")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "UnitPrice" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "DiscountRatio" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Discount" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "TaxID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "TotalPrice" && rbIsMaterialIssueVoucher.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID") && ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
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
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
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
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !rbIsMaterialIssueVoucher.Checked)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					if (frmQuantityMultiUnit2.UnitID > 0)
					{
						DataRow dataRow = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmQuantityMultiUnit2.UnitID;
						if (bool.Parse(dataRow["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
						{
							DataRow[] array = dtItemPrices.Select(" UnitID = " + frmQuantityMultiUnit2.UnitID + " and ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
							if (array != null && array.Length != 0)
							{
								((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
							}
							else
							{
								((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = 0;
							}
						}
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && cboClient.SelectedIndex > -1)
				{
					DataTable dthistory = SLInvoicesDetails.SelectTopPrices(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
					frmEnterValue frmEnterValue2 = new frmEnterValue(dthistory, GlobalVariables.IsArabic ? "السعرالشامل" : "Total Price", _IsInt: false, _IsNumeric: true);
					frmEnterValue2.WindowState = FormWindowState.Normal;
					frmEnterValue2.ShowDialog();
					if (frmEnterValue2.Value != "" && decimal.Parse(frmEnterValue2.Value) > 0m)
					{
						DataRow dataRow2 = dtClients.Select("SubAccountID =" + ((TextEditorControlBase)cboClient).Value.ToString())[0];
						double num = 0.0;
						if (((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value != DBNull.Value)
						{
							num = double.Parse(dtTaxs.Select(" TaxID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100.0;
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = double.Parse(frmEnterValue2.Value) / (1.0 - (bool.Parse(dataRow2["IsDiscountTax"].ToString()) ? GlobalVariables.DiscountTax : 0.0) + (bool.Parse(dataRow2["IsAddedTax"].ToString()) ? GlobalVariables.AddedTax : 0.0) + num);
					}
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
					int num2 = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
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
				if (rbIsDirectInvoice.Checked && Adding && frmImageViewer2.Saved)
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
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DiscountRatio" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				if (((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" لايمكن نقص الكمية عن الكمية المنصرفة فى المخازن و قدرها " + ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString(), "You Cannot Decrease The Quantity From The Issued Qty in the Store" + ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value;
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
		if (((UltraGridBase)ULGData).ActiveRow != null && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString()) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لانه تم صرفه من المخازن" : "Cannot Delete This Item Because Issued From The Store");
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
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = (decimal.Parse(num2.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString(GlobalVariables.txtDecimalFormate);
		}
		CalculateNetTotals();
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (Row.Cells["DiscountRatio"].Value != DBNull.Value)
		{
			Row.Cells["DiscountRatio"].Value = ((decimal.Parse(Row.Cells["DiscountRatio"].Value.ToString()) > 100m) ? 100m : decimal.Parse(Row.Cells["DiscountRatio"].Value.ToString()));
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(Row.Cells["DiscountRatio"].Value.ToString()) / 100m;
		}
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateRowActualUnitSalesPriceAndReturnedPrice(UltraGridRow Row, decimal InvoiceExpense)
	{
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) != 0m)
		{
			Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * (InvoiceExpense + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text)) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
		}
		else
		{
			Row.Cells["ActualUnitSalesPrice"].Value = 0;
		}
		Row.Cells["ReturnPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscBeforeTaxValue).Text != "" && decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) > 0m)
		{
			Row.Cells["DiscountAfterTax"].Value = (decimal.Parse(Row.Cells["NetPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString())) / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)) * decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text);
		}
		else
		{
			Row.Cells["DiscountAfterTax"].Value = 0;
		}
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) != 0m)
		{
			Row.Cells["CommercialTaxValue"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text);
			Row.Cells["StampsValue"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text);
			Row.Cells["GrowthFees"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text);
		}
		else
		{
			Row.Cells["CommercialTaxValue"].Value = 0;
			Row.Cells["StampsValue"].Value = 0;
			Row.Cells["GrowthFees"].Value = 0;
		}
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
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text) + CalculateInvoiceExpense()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
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
		//IL_103e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Expected O, but got Unknown
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Expected O, but got Unknown
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Expected O, but got Unknown
		//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1007: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		//IL_18b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c1: Expected O, but got Unknown
		//IL_0d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Expected O, but got Unknown
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
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow dataRow2 = dtItemPrices.Select(" UnitID = " + text5 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((decimal.Parse(dataRow2["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(dataRow2["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
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
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow3["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow3["UnitID"].ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) : (" ItemID = " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			if (array != null && array.Length != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
			}
			else
			{
				UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
				UltraGridCell obj6 = ((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"];
				object obj7 = (((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = 0);
				value = (obj6.Value = obj7);
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
		if (AutomaticlyAddItemTaxToSalesInvoice)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
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

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Expected O, but got Unknown
		if (cboClient.SelectedIndex > -1 && dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
				}
				else
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
					UltraGridCell obj2 = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
					object obj3 = (((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = 0);
					object value = (obj2.Value = obj3);
					obj.Value = value;
				}
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
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SLInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboQuotation_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.SLQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num;
			}
		}
	}

	private void cboQuotation_ValueChanged(object sender, EventArgs e)
	{
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Expected O, but got Unknown
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Expected O, but got Unknown
		if (cboQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			object obj = dtSLQuotation.Select(" QuotationID= " + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["SubAccountID"];
			if (obj != null)
			{
				((TextEditorControlBase)cboClient).Value = obj.ToString();
			}
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null && dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), GlobalVariables.LocalCurrencyID.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			((TextEditorControlBase)cboCurrency).Value = dtSLQuotation.Select(" QuotationID= " + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				object obj2 = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DefaultPaymentMethodID"];
				if (obj2 != null)
				{
					((TextEditorControlBase)cboPaymentMethod).Value = obj2.ToString();
				}
				((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			dtSLQuotationDetails = SLInvoicesDetails.FillByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtDetails.Rows.Clear();
			((UltraGridBase)ULGData).DataSource = dtSLQuotationDetails;
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				((TextEditorControlBase)txtDiscount).ValueChanged -= txtDiscount_ValueChanged;
				((Control)(object)txtDiscount).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtDiscount).ValueChanged += txtDiscount_ValueChanged;
			}
			InitGrid();
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((Adding || Updating) ? 6 : 2);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text);
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				DataRow dataRow = dtItems.Select(" ItemID= " + dtSLQuotationDetails.Rows[i]["ItemID"].ToString())[0];
				if (!bool.Parse(dataRow["IsService"].ToString()))
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dtUnits.Select(" UnitID =" + dtSLQuotationDetails.Rows[i]["UnitID"].ToString())[0]["UnitTypeID"].ToString()));
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			CalculateGoss();
			CalculateTotalsTax();
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
			if (cboBranches.SelectedIndex > -1)
			{
				DataView dataView = new DataView(dtClients);
				dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
				GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
			}
			else
			{
				GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
			}
		}
	}

	private void btnQuotationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SLQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotation).Value = num;
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblQuotationNo;
		bool visible = (((Control)(object)cboQuotation).Visible = rbIsQuotation.Checked);
		((Control)(object)obj).Visible = visible;
		((Control)(object)btnQuotationSearch).Visible = rbIsQuotation.Checked && Adding;
		UltraLabel obj2 = lblRequestNo;
		UltraComboEditor obj3 = cboRequestNo;
		bool flag2 = (((Control)(object)btnRequestSearch).Visible = rbIsProductionRequest.Checked);
		visible = (((Control)(object)obj3).Visible = flag2);
		((Control)(object)obj2).Visible = visible;
		UltraCheckEditor obj4 = chkAll;
		visible = (clbMaterialIssueVoucherNo.Visible = rbIsMaterialIssueVoucher.Checked);
		((Control)(object)obj4).Visible = visible;
		((Control)(object)chkWithoutMIV).Visible = rbIsDirectInvoice.Checked;
		((Control)(object)btnImport).Visible = rbIsDirectInvoice.Checked && Adding;
		if (Adding)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress -= textBox_KeyPress;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			cboQuotation.SelectedIndex = -1;
			cboRequestNo.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			cboPaymentMethod.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "0";
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress += textBox_KeyPress;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void cboClientCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboClientCode.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboClientCode).Value;
			return;
		}
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Expected O, but got Unknown
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Expected O, but got Unknown
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboClient).Value;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		if (cboClient.SelectedIndex > -1)
		{
			if (rbIsQuotation.Checked)
			{
				((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
				DataView dataView = new DataView(dtSLQuotation);
				dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString() + " And  Closed= 0 And  BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
				dtDetails.Rows.Clear();
				cboCurrency.SelectedIndex = -1;
				((Control)(object)txtExchangeRate).Text = "";
				((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			}
			else if (rbIsProductionRequest.Checked)
			{
				((TextEditorControlBase)cboRequestNo).ValueChanged -= cboRequestNo_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
				DataView dataView2 = new DataView(dtProductionRequest);
				dataView2.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboRequestNo, dataView2.ToTable(), "ProductionRequestID", "ProductionRequestNo");
				dtDetails.Rows.Clear();
				cboCurrency.SelectedIndex = -1;
				((Control)(object)txtExchangeRate).Text = "";
				((TextEditorControlBase)cboRequestNo).ValueChanged += cboRequestNo_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			}
			else if (rbIsMaterialIssueVoucher.Checked && Adding)
			{
				clbMaterialIssueVoucherNo.DataSource = null;
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
				dtMaterialIssueVouchers = Main.ExecuteQuery_DataTable(" Select MaterialIssueVoucherID , (MaterialIssueVoucherNo+' - '+ Convert (varchar ,MaterialIssueVoucherDate,103)+' '+Convert (varchar ,MaterialIssueVoucherDate,108))  as MaterialIssueVoucherNo from  SC_MaterialIssueVouchers Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID + " And IsDirect=1 And SLInvoiceID is null And SubAccountID=" + ((TextEditorControlBase)cboClient).Value.ToString());
				Main.Fillclb(clbMaterialIssueVoucherNo, dtMaterialIssueVouchers, "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
			}
			((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), GlobalVariables.LocalCurrencyID.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboSalesMan).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"];
			((TextEditorControlBase)txtDiscount).ValueChanged -= txtDiscount_ValueChanged;
			((Control)(object)txtDiscount).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString();
			((TextEditorControlBase)txtDiscount).ValueChanged += txtDiscount_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboPaymentMethod).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DefaultPaymentMethodID"];
			if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value && (rbIsDirectInvoice.Checked || rbIsMaterialIssueVoucher.Checked || rbIsProductionRequest.Checked))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
					if (array != null && array.Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
						((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
					}
					else
					{
						UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
						UltraGridCell obj2 = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
						object obj3 = (((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = 0);
						object value = (obj2.Value = obj3);
						obj.Value = value;
					}
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
		else
		{
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
			GlobalFunctions.FillCombo(cboRequestNo, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
		}
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		ULGDataExpenses.AfterCellUpdate -= new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Value")
		{
			CalculateNetTotals();
		}
		ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
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

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
		ClearData();
	}

	public void ClearData()
	{
		dtDetails.Rows.Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void cboSalesMan_ValueChanged(object sender, EventArgs e)
	{
		if (cboSalesMan.SelectedIndex <= -1)
		{
			return;
		}
		DataRow dataRow = dtSalesMan.Select("SubAccountID = " + ((TextEditorControlBase)cboSalesMan).Value.ToString())[0];
		if (dataRow["SalesManDefaultStoreID"] == DBNull.Value || dtStores.Select(" Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID + " and StoreID = " + dataRow["SalesManDefaultStoreID"].ToString()).Length == 0)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceDetailID"].Value.ToString() == "-1")
			{
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = dataRow["SalesManDefaultStoreID"];
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = dataRow["SalesManDefaultStoreID"];
	}

	private void txtDiscount_ValueChanged(object sender, EventArgs e)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		((TextEditorControlBase)txtDiscount).ValueChanged -= txtDiscount_ValueChanged;
		decimal result = default(decimal);
		if (decimal.TryParse(((Control)(object)txtDiscount).Text, out result))
		{
			((Control)(object)txtDiscount).Text = ((decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text) > 100m) ? "100" : ((Control)(object)txtDiscount).Text);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text);
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].DefaultCellValue = ((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		((TextEditorControlBase)txtDiscount).ValueChanged += txtDiscount_ValueChanged;
		CalculateGoss();
		CalculateTotalsTax();
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + " ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
	}

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	private void btnGetFromXL_Click(object sender, EventArgs e)
	{
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_0b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Expected O, but got Unknown
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
		StringBuilder stringBuilder = new StringBuilder();
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		dtDetails.Rows.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i]["الكود"].ToString().Length == 0 || !(decimal.Parse(dataTable.Rows[i]["عدد القطع"].ToString()) > 0m))
			{
				continue;
			}
			if (dtItems.Select("ItemBarCode = '" + dataTable.Rows[i]["الكود"].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? ("هذا الباركود " + dataTable.Rows[i]["الكود"].ToString() + "غير موجود") : "This BarCode Does not Exists");
				stringBuilder.Append("\n");
				continue;
			}
			if (UsingColors && dtColors.Select("ColorName = '" + dataTable.Rows[i]["اللون"].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" لون هذا الصنف  " + dataTable.Rows[i]["الكود"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
			if (UsingSizes && dtSizes.Select("ItemSizeName = '" + dataTable.Rows[i]["المقاس"].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" مقاس هذا الصنف  " + dataTable.Rows[i]["الكود"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
		}
		if (stringBuilder.ToString() != "")
		{
			GlobalVariables.InformationMB.Show(stringBuilder.ToString(), stringBuilder.ToString());
			return;
		}
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			if (dataTable.Rows[j]["الكود"].ToString().Length == 0 || !(decimal.Parse(dataTable.Rows[j]["عدد القطع"].ToString()) > 0m))
			{
				continue;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			DataRow dataRow = null;
			DataRow dataRow2 = null;
			if (UsingColors)
			{
				dataRow = dtColors.Select("ColorName = '" + dataTable.Rows[j]["اللون"].ToString() + "'")[0];
			}
			if (UsingSizes)
			{
				dataRow2 = dtSizes.Select("ItemSizeName = '" + dataTable.Rows[j]["المقاس"].ToString() + "'")[0];
			}
			DataRow dataRow3 = dtItems.Select("ItemBarCode = '" + dataTable.Rows[j]["الكود"].ToString() + "'")[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow3["ItemID"]);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : dataRow3["DefaultStoreID"]);
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable.Rows[j]["عدد القطع"].ToString();
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow3["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow3["UnitID"].ToString() + " and  ItemID= " + dataRow3["ItemID"].ToString()) : (" ItemID = " + dataRow3["ItemID"].ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj3.Value = value;
				}
			}
			if (AutomaticlyAddItemTaxToSalesInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
			}
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
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = ((!UsingColors || dataRow == null) ? ((object)1) : dataRow["ColorID"]);
			if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = ((!UsingSizes || dataRow2 == null) ? ((object)1) : dataRow2["ItemSizeID"]);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			CalcTotalQty();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		((UltraGridBase)ULGData).UpdateData();
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

	private void txtDiscAfterTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtStampValue).Text == "" || ((Control)(object)txtStampValue).Text == ".") ? "0" : ((Control)(object)txtStampValue).Text) + decimal.Parse((((Control)(object)txtGrowthFees).Text == "" || ((Control)(object)txtGrowthFees).Text == ".") ? "0" : ((Control)(object)txtGrowthFees).Text) + CalculateInvoiceExpense()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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

	private void clbMaterialIssueVoucherNo_SelectedValueChanged(object sender, EventArgs e)
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Expected O, but got Unknown
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll).Checked = clbMaterialIssueVoucherNo.CheckedItems.Count == clbMaterialIssueVoucherNo.Items.Count && clbMaterialIssueVoucherNo.Items.Count > 0;
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		string materialIssueVouchersIds = GetMaterialIssueVouchersIds();
		if (materialIssueVouchersIds != "")
		{
			((UltraGridBase)ULGData).DataSource = SLInvoicesDetails.FillByMaterialIssueVoucherIDs("," + materialIssueVouchersIds + ",", GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					DataRow[] array = (bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString()) ? ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null) : dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		else
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			CalculateGoss();
			CalculateTotalsTax();
		}
	}

	public string GetMaterialIssueVouchersIds()
	{
		string text = "";
		for (int i = 0; i < clbMaterialIssueVoucherNo.Items.Count; i++)
		{
			if (clbMaterialIssueVoucherNo.GetItemChecked(i))
			{
				text = ((!(text == "")) ? (text + "," + dtMaterialIssueVouchers.Rows[i]["MaterialIssueVoucherID"].ToString()) : (text + dtMaterialIssueVouchers.Rows[i]["MaterialIssueVoucherID"].ToString()));
			}
		}
		return text;
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Expected O, but got Unknown
		clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		for (int i = 0; i < clbMaterialIssueVoucherNo.Items.Count; i++)
		{
			clbMaterialIssueVoucherNo.SetItemChecked(i, ((UltraToggleEditorBase)chkAll).Checked);
		}
		string materialIssueVouchersIds = GetMaterialIssueVouchersIds();
		if (materialIssueVouchersIds != "")
		{
			((UltraGridBase)ULGData).DataSource = SLInvoicesDetails.FillByMaterialIssueVoucherIDs("," + materialIssueVouchersIds + ",", GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					DataRow[] array = (bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString()) ? ((((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString()) : null) : dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
					((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
					((UltraGridBase)ULGData).Rows[j].Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
					CalculateRow(((UltraGridBase)ULGData).Rows[j]);
				}
				CalculateGoss();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["SalesJVID"].ToString()));
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

	private void cboRequestNo_ValueChanged(object sender, EventArgs e)
	{
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Expected O, but got Unknown
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Expected O, but got Unknown
		if (cboRequestNo.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			object obj = dtProductionRequest.Select(" ProductionRequestID= " + ((TextEditorControlBase)cboRequestNo).Value.ToString())[0]["SubAccountID"];
			if (obj != null)
			{
				((TextEditorControlBase)cboClient).Value = obj.ToString();
			}
			if (cboClient.SelectedIndex > -1 && dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				((TextEditorControlBase)cboPaymentMethod).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DefaultPaymentMethodID"];
			}
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
				((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			}
			((UltraGridBase)ULGData).DataSource = SLInvoicesDetails.FillByProductionRequestID(((TextEditorControlBase)cboRequestNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
			if (cboClient.SelectedIndex > -1 && ((TextEditorControlBase)cboClient).Value != null)
			{
				decimal num = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString());
				((TextEditorControlBase)txtDiscount).ValueChanged -= txtDiscount_ValueChanged;
				((Control)(object)txtDiscount).Text = num.ToString();
				((TextEditorControlBase)txtDiscount).ValueChanged += txtDiscount_ValueChanged;
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
					if (array != null && array.Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString());
						((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m) ? decimal.Parse(array[0]["DiscountPercentage"].ToString()) : decimal.Parse((((Control)(object)txtDiscount).Text == "" || ((Control)(object)txtDiscount).Text == ".") ? "0" : ((Control)(object)txtDiscount).Text));
					}
					else
					{
						UltraGridCell obj2 = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
						UltraGridCell obj3 = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
						object obj4 = (((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = 0);
						object value = (obj3.Value = obj4);
						obj2.Value = value;
					}
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
					CalculateGoss();
					CalculateTotalsTax();
				}
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()));
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtDetails.Rows.Clear();
			if (cboBranches.SelectedIndex > -1)
			{
				DataView dataView = new DataView(dtClients);
				dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
				GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
			}
			else
			{
				GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
			}
		}
	}

	private void cboRequestNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.ProductionRequests("," + GlobalVariables.CurrentBranchID + ",", 1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboRequestNo).Value = num;
			}
		}
	}

	private void btnRequestSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ProductionRequests("," + GlobalVariables.CurrentBranchID + ",", 1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboRequestNo).Value = num;
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

	public override void ImportGridData()
	{
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Expected O, but got Unknown
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Expected O, but got Unknown
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
		StringBuilder stringBuilder = new StringBuilder();
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		dtDetails.Rows.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString().Length == 0 || !(decimal.Parse(dataTable.Rows[i][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString()) > 0m))
			{
				continue;
			}
			if (dtItems.Select("ItemBarCode = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? ("هذا الباركود " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود") : "This BarCode Does not Exists");
				stringBuilder.Append("\n");
				continue;
			}
			if (UsingColors && dtColors.Select("ColorName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" لون هذا الصنف  " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
			if (UsingSizes && dtSizes.Select("ItemSizeName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" مقاس هذا الصنف  " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
		}
		if (stringBuilder.ToString() != "")
		{
			GlobalVariables.InformationMB.Show(stringBuilder.ToString(), stringBuilder.ToString());
			return;
		}
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			if (dataTable.Rows[j][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString().Length == 0 || !(decimal.Parse(dataTable.Rows[j][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString()) > 0m))
			{
				continue;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			DataRow dataRow = null;
			DataRow dataRow2 = null;
			if (UsingColors)
			{
				dataRow = dtColors.Select("ColorName = '" + dataTable.Rows[j][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'")[0];
			}
			if (UsingSizes)
			{
				dataRow2 = dtSizes.Select("ItemSizeName = '" + dataTable.Rows[j][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'")[0];
			}
			DataRow dataRow3 = dtItems.Select("ItemBarCode = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'")[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow3["ItemID"]);
			obj.Value = value;
			if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "الوحدة" : "Unit") && dtUnits.Select("UnitName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'").Length != 0)
			{
				DataRow dataRow4 = dtUnits.Select("UnitName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'")[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow4["UnitID"];
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
			}
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : dataRow3["DefaultStoreID"]);
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable.Rows[j][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString();
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow3["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow3["UnitID"].ToString() + " and  ItemID= " + dataRow3["ItemID"].ToString()) : (" ItemID = " + dataRow3["ItemID"].ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj3.Value = value;
				}
			}
			if (AutomaticlyAddItemTaxToSalesInvoice)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
			}
			if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "ملاحظات" : "Notes"))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = dataTable.Rows[j][GlobalVariables.IsArabic ? "ملاحظات" : "Notes"].ToString();
			}
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateGoss();
			CalculateTotalsTax();
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + dataRow3["UnitID"].ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = ((!UsingColors || dataRow == null) ? ((object)1) : dataRow["ColorID"]);
			if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = ((!UsingSizes || dataRow2 == null) ? ((object)1) : dataRow2["ItemSizeID"]);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			CalcTotalQty();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		((UltraGridBase)ULGData).UpdateData();
	}

	public void CalcTotalTotalDiscount()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((UltraGridBase)ULGData).UpdateData();
		DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
		if (dataTable.Rows.Count <= 0 || dataTable.Select(" Discount > 0 ").Length == 0)
		{
			return;
		}
		object obj = dataTable.Compute(" Sum(Discount) ", "");
		if (obj != DBNull.Value)
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (decimal.Parse(((Control)(object)txtGrossValue).Text) > 0m)
			{
				((Control)(object)txtDiscBeforeTaxRatio).Text = (decimal.Parse(((Control)(object)txtGrossValue).Text) / decimal.Parse(obj.ToString())).ToString();
			}
		}
	}

	private void btnStockBalance_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاءإختيار المخزن  ", "Please Select Store ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value)
			{
				if (UsingColors)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = "1";
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value)
			{
				if (UsingSizes)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = "1";
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
		}
		StringWriter stringWriter = new StringWriter();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
		dataView.ToTable("Table", false, "ItemID", "ColorID", "ItemSizeID", "BatchID", "UnitID", "StoreID").WriteXml((TextWriter)stringWriter);
		DataTable itemAllowedQtyByItemIDs = Items.GetItemAllowedQtyByItemIDs(stringWriter.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			DataRow[] array = itemAllowedQtyByItemIDs.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() + " And StoreID= " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() + " And ColorID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() + " And ItemSizeID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() + " And BatchID" + ((((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value == DBNull.Value) ? " is null " : ("=" + ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString())) + " And UnitID=" + ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString());
			if (array.Length != 0)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["StockBalance"].Value = array[0]["Balance"].ToString();
			}
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

	private void btnClientBalance_Click(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex != -1)
		{
			try
			{
				frmSubAccountBalanceWithCurrency frmSubAccountBalanceWithCurrency2 = new frmSubAccountBalanceWithCurrency(int.Parse(((TextEditorControlBase)cboClient).Value.ToString()), "-1");
				frmSubAccountBalanceWithCurrency2.WindowState = FormWindowState.Normal;
				frmSubAccountBalanceWithCurrency2.ShowDialog();
			}
			catch
			{
				GlobalVariables.InformationMB.Show("لايمكن الإستعلام عن رصيد العميل لتعزر الإتصال بالخادم", "Client Balance not Available ");
			}
		}
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void btnCostCenterSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CostCenter(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboCostCenter).Value = num;
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

	public override void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching)
		{
			return;
		}
		if (e.KeyCode == Keys.Return && TableName.Length > 0 && NoCol.Length > 0 && ((Control)(object)txtCode).Text.Length > 0)
		{
			if (Adding || Updating)
			{
				e.Handled = true;
				SendKeys.Send("{tab}");
				return;
			}
			DataTable comboData = Main.GetComboData(TableName, "*", NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And EInvoiceInternalCode is null Order by year(" + DateCol + ") Desc");
			if (comboData.Rows.Count > 0)
			{
				RowID = comboData.Rows[0][IDCol].ToString();
				dtSearchResult = null;
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
		if (e.KeyCode == Keys.F8)
		{
			btnSearch_Click(null, null);
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode is null", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode is null ", "1");
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
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected O, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected O, but got Unknown
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Expected O, but got Unknown
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Expected O, but got Unknown
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Expected O, but got Unknown
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Expected O, but got Unknown
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Expected O, but got Unknown
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Expected O, but got Unknown
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Expected O, but got Unknown
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Expected O, but got Unknown
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Expected O, but got Unknown
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Expected O, but got Unknown
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Expected O, but got Unknown
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Expected O, but got Unknown
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Expected O, but got Unknown
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Expected O, but got Unknown
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Expected O, but got Unknown
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Expected O, but got Unknown
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Expected O, but got Unknown
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Expected O, but got Unknown
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Expected O, but got Unknown
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Expected O, but got Unknown
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Expected O, but got Unknown
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Expected O, but got Unknown
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Expected O, but got Unknown
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Expected O, but got Unknown
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Expected O, but got Unknown
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Expected O, but got Unknown
		//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Expected O, but got Unknown
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Expected O, but got Unknown
		//IL_101f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Expected O, but got Unknown
		//IL_104f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1059: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.Transactions.frmSLInvoices2));
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
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		this.pnlCheckType = new UltraPanel();
		this.rbIsProductionRequest = new System.Windows.Forms.RadioButton();
		this.rbIsMaterialIssueVoucher = new System.Windows.Forms.RadioButton();
		this.rbIsQuotation = new System.Windows.Forms.RadioButton();
		this.rbIsDirectInvoice = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataExpenses = new UltraGrid();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.txtBarCode = new UltraTextEditor();
		this.lblQuotationNo = new UltraLabel();
		this.cboQuotation = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnClientSearch = new UltraButton();
		this.btnQuotationSearch = new UltraButton();
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
		this.chkAll = new UltraCheckEditor();
		this.clbMaterialIssueVoucherNo = new System.Windows.Forms.CheckedListBox();
		this.btnJV = new UltraButton();
		this.txtSerial = new UltraTextEditor();
		this.lblSerial = new UltraLabel();
		this.lblCostCenter = new UltraLabel();
		this.cboCostCenter = new UltraComboEditor();
		this.btnRequestSearch = new UltraButton();
		this.lblRequestNo = new UltraLabel();
		this.cboRequestNo = new UltraComboEditor();
		this.txtSubTruckNo = new UltraTextEditor();
		this.lblSubTruckNo = new UltraLabel();
		this.txtTruckNo = new UltraTextEditor();
		this.lblTruckNo = new UltraLabel();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.btnStockBalance = new UltraButton();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMan = new UltraComboEditor();
		this.btnSalesManSearch = new UltraButton();
		this.lblBalance = new UltraLabel();
		this.btnClientBalance = new UltraButton();
		this.txtBranchBalance = new UltraTextEditor();
		this.btnCostCenterSearch = new UltraButton();
		this.btnStoreSearch = new UltraButton();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.txtDiscount = new UltraTextEditor();
		this.txtDisc = new UltraLabel();
		this.cboClientCode = new UltraComboEditor();
		this.chkWithoutMIV = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.txtSerial).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRequestNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubTruckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTruckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutMIV).BeginInit();
		base.SuspendLayout();
		base.lblTitle2.AutoEllipsis = false;
		((ControlBase)base.lblTitle2).WrapText = false;
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnImport, "btnImport");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsProductionRequest);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsMaterialIssueVoucher);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsQuotation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirectInvoice);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsProductionRequest, "rbIsProductionRequest");
		this.rbIsProductionRequest.BackColor = System.Drawing.Color.Transparent;
		this.rbIsProductionRequest.Name = "rbIsProductionRequest";
		this.rbIsProductionRequest.TabStop = true;
		this.rbIsProductionRequest.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsMaterialIssueVoucher, "rbIsMaterialIssueVoucher");
		this.rbIsMaterialIssueVoucher.BackColor = System.Drawing.Color.Transparent;
		this.rbIsMaterialIssueVoucher.Name = "rbIsMaterialIssueVoucher";
		this.rbIsMaterialIssueVoucher.TabStop = true;
		this.rbIsMaterialIssueVoucher.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsQuotation, "rbIsQuotation");
		this.rbIsQuotation.BackColor = System.Drawing.Color.Transparent;
		this.rbIsQuotation.Name = "rbIsQuotation";
		this.rbIsQuotation.TabStop = true;
		this.rbIsQuotation.UseVisualStyleBackColor = false;
		this.rbIsQuotation.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDirectInvoice, "rbIsDirectInvoice");
		this.rbIsDirectInvoice.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirectInvoice.Name = "rbIsDirectInvoice";
		this.rbIsDirectInvoice.TabStop = true;
		this.rbIsDirectInvoice.UseVisualStyleBackColor = false;
		this.rbIsDirectInvoice.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		this.ULGDataExpenses.AfterRowsDeleted += new System.EventHandler(ULGDataExpenses_AfterRowsDeleted);
		this.ULGDataExpenses.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataExpenses_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Enter += new System.EventHandler(ULGDataExpenses_Enter);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataExpenses_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataExpenses_KeyPress);
		this.lblBarCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		this.lblClient.AutoEllipsis = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		this.lblQuotationNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblQuotationNo, "lblQuotationNo");
		((System.Windows.Forms.Control)(object)this.lblQuotationNo).Name = "lblQuotationNo";
		((ControlBase)this.lblQuotationNo).WrapText = false;
		((TextEditorControlBase)this.cboQuotation).AlwaysInEditMode = true;
		this.cboQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboQuotation, "cboQuotation");
		((System.Windows.Forms.Control)(object)this.cboQuotation).Name = "cboQuotation";
		((TextEditorControlBase)this.cboQuotation).ValueChanged += new System.EventHandler(cboQuotation_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboQuotation).KeyDown += new System.Windows.Forms.KeyEventHandler(cboQuotation_KeyDown);
		this.lblCurrency.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val16;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnQuotationSearch).Appearance = (AppearanceBase)(object)val17;
		resources.ApplyResources(this.btnQuotationSearch, "btnQuotationSearch");
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Name = "btnQuotationSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Click += new System.EventHandler(btnQuotationSearch_Click);
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPaymentMethodSearch).Appearance = (AppearanceBase)(object)val18;
		resources.ApplyResources(this.btnPaymentMethodSearch, "btnPaymentMethodSearch");
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Name = "btnPaymentMethodSearch";
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Click += new System.EventHandler(btnPaymentMethodSearch_Click);
		this.lblPaymentMethod.AutoEllipsis = false;
		resources.ApplyResources(this.lblPaymentMethod, "lblPaymentMethod");
		((System.Windows.Forms.Control)(object)this.lblPaymentMethod).Name = "lblPaymentMethod";
		((ControlBase)this.lblPaymentMethod).WrapText = false;
		((TextEditorControlBase)this.cboPaymentMethod).AlwaysInEditMode = true;
		this.cboPaymentMethod.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPaymentMethod, "cboPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).Name = "cboPaymentMethod";
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).KeyDown += new System.Windows.Forms.KeyEventHandler(cboPaymentMethod_KeyDown);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((EditorButtonControlBase)this.txtDiscBeforeTaxValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((EditorButtonControlBase)this.txtDiscBeforeTaxRatio).ReadOnly = true;
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
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		((TextEditorControlBase)this.cboItemBatchs).AlwaysInEditMode = true;
		this.cboItemBatchs.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboItemBatchs, "cboItemBatchs");
		((System.Windows.Forms.Control)(object)this.cboItemBatchs).Name = "cboItemBatchs";
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val19;
		resources.ApplyResources(this.chkAll, "chkAll");
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		this.clbMaterialIssueVoucherNo.CheckOnClick = true;
		this.clbMaterialIssueVoucherNo.FormattingEnabled = true;
		resources.ApplyResources(this.clbMaterialIssueVoucherNo, "clbMaterialIssueVoucherNo");
		this.clbMaterialIssueVoucherNo.Name = "clbMaterialIssueVoucherNo";
		this.clbMaterialIssueVoucherNo.SelectedValueChanged += new System.EventHandler(clbMaterialIssueVoucherNo_SelectedValueChanged);
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.txtSerial, "txtSerial");
		((System.Windows.Forms.Control)(object)this.txtSerial).Name = "txtSerial";
		((System.Windows.Forms.Control)(object)this.txtSerial).KeyUp += new System.Windows.Forms.KeyEventHandler(txtSerial_KeyUp);
		this.lblSerial.AutoEllipsis = false;
		resources.ApplyResources(this.lblSerial, "lblSerial");
		((System.Windows.Forms.Control)(object)this.lblSerial).Name = "lblSerial";
		((ControlBase)this.lblSerial).WrapText = false;
		this.lblCostCenter.AutoEllipsis = false;
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		((TextEditorControlBase)this.cboCostCenter).AlwaysInEditMode = true;
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		((AppearanceBase)val20).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnRequestSearch).Appearance = (AppearanceBase)(object)val20;
		resources.ApplyResources(this.btnRequestSearch, "btnRequestSearch");
		((System.Windows.Forms.Control)(object)this.btnRequestSearch).Name = "btnRequestSearch";
		((System.Windows.Forms.Control)(object)this.btnRequestSearch).Click += new System.EventHandler(btnRequestSearch_Click);
		this.lblRequestNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblRequestNo, "lblRequestNo");
		((System.Windows.Forms.Control)(object)this.lblRequestNo).Name = "lblRequestNo";
		((ControlBase)this.lblRequestNo).WrapText = false;
		((TextEditorControlBase)this.cboRequestNo).AlwaysInEditMode = true;
		this.cboRequestNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboRequestNo, "cboRequestNo");
		((System.Windows.Forms.Control)(object)this.cboRequestNo).Name = "cboRequestNo";
		((TextEditorControlBase)this.cboRequestNo).ValueChanged += new System.EventHandler(cboRequestNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboRequestNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboRequestNo_KeyDown);
		resources.ApplyResources(this.txtSubTruckNo, "txtSubTruckNo");
		((System.Windows.Forms.Control)(object)this.txtSubTruckNo).Name = "txtSubTruckNo";
		this.lblSubTruckNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubTruckNo, "lblSubTruckNo");
		((System.Windows.Forms.Control)(object)this.lblSubTruckNo).Name = "lblSubTruckNo";
		((ControlBase)this.lblSubTruckNo).WrapText = false;
		resources.ApplyResources(this.txtTruckNo, "txtTruckNo");
		((System.Windows.Forms.Control)(object)this.txtTruckNo).Name = "txtTruckNo";
		this.lblTruckNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblTruckNo, "lblTruckNo");
		((System.Windows.Forms.Control)(object)this.lblTruckNo).Name = "lblTruckNo";
		((ControlBase)this.lblTruckNo).WrapText = false;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.btnStockBalance, "btnStockBalance");
		((System.Windows.Forms.Control)(object)this.btnStockBalance).Name = "btnStockBalance";
		((System.Windows.Forms.Control)(object)this.btnStockBalance).Click += new System.EventHandler(btnStockBalance_Click);
		this.lblSalesMan.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((TextEditorControlBase)this.cboSalesMan).ValueChanged += new System.EventHandler(cboSalesMan_ValueChanged);
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val21;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		this.lblBalance.AutoEllipsis = false;
		resources.ApplyResources(this.lblBalance, "lblBalance");
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		((AppearanceBase)val22).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val22).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((ControlBase)this.btnClientBalance).Appearance = (AppearanceBase)(object)val22;
		resources.ApplyResources(this.btnClientBalance, "btnClientBalance");
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Name = "btnClientBalance";
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Click += new System.EventHandler(btnClientBalance_Click);
		resources.ApplyResources(this.txtBranchBalance, "txtBranchBalance");
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).Name = "txtBranchBalance";
		((EditorButtonControlBase)this.txtBranchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).TabStop = false;
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnCostCenterSearch).Appearance = (AppearanceBase)(object)val23;
		resources.ApplyResources(this.btnCostCenterSearch, "btnCostCenterSearch");
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Name = "btnCostCenterSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Click += new System.EventHandler(btnCostCenterSearch_Click);
		((UltraButtonBase)this.btnStoreSearch).AcceptsFocus = false;
		((AppearanceBase)val24).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val24;
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Click += new System.EventHandler(btnStoreSearch_Click);
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboStore, "cboStore");
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		((AppearanceBase)val25).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val25;
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		resources.ApplyResources(this.txtDiscount, "txtDiscount");
		((System.Windows.Forms.Control)(object)this.txtDiscount).Name = "txtDiscount";
		((TextEditorControlBase)this.txtDiscount).ValueChanged += new System.EventHandler(txtDiscount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		this.txtDisc.AutoEllipsis = false;
		resources.ApplyResources(this.txtDisc, "txtDisc");
		((System.Windows.Forms.Control)(object)this.txtDisc).Name = "txtDisc";
		((ControlBase)this.txtDisc).WrapText = false;
		((TextEditorControlBase)this.cboClientCode).AlwaysInEditMode = true;
		this.cboClientCode.AutoCompleteMode = (AutoCompleteMode)3;
		resources.ApplyResources(this.cboClientCode, "cboClientCode");
		((System.Windows.Forms.Control)(object)this.cboClientCode).Name = "cboClientCode";
		((TextEditorControlBase)this.cboClientCode).ValueChanged += new System.EventHandler(cboClientCode_ValueChanged);
		resources.ApplyResources(this.chkWithoutMIV, "chkWithoutMIV");
		((System.Windows.Forms.Control)(object)this.chkWithoutMIV).Name = "chkWithoutMIV";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithoutMIV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDisc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCenterSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStockBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRequestSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRequestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRequestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add(this.clbMaterialIssueVoucherNo);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemBatchs);
		base.Name = "frmSLInvoices2";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemBatchs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrowthFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrowthFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex(this.clbMaterialIssueVoucherNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRequestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRequestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRequestSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStockBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCostCenterSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDisc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithoutMIV, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.txtSerial).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRequestNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubTruckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTruckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutMIV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
