using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
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

public class frmSLTruckInvoices : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtSLQuotation;

	private DataTable dtSLQuotationDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtTaxs;

	private DataTable dtExpenses;

	private DataTable dtClients;

	private DataTable dtReports;

	private DataTable dtInvoiceExpenses;

	private DataTable dtItemPrices;

	private DataTable dtPOSDefaultStore;

	private DataTable dtTruckScales;

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

	private bool UseSalesSeasons = false;

	private bool UsingTruckScale = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private TruckScale TS;

	private IContainer components = null;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnClientSearch;

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

	private UltraComboEditor cboItemBatchs;

	public UltraButton btnJV;

	private UltraTextEditor txtSerial;

	private UltraLabel lblSerial;

	private UltraTextEditor txtTruckNo;

	private UltraLabel lblTruckNo;

	private UltraTextEditor txtSubTruckNo;

	private UltraLabel lblSubTruckNo;

	private UltraTextEditor txtEmptyWeight;

	private UltraLabel lblEmptyWeight;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsQuotation;

	private RadioButton rbIsDirectInvoice;

	public UltraButton btnQuotationSearch;

	private UltraLabel lblQuotationNo;

	private UltraComboEditor cboQuotation;

	private UltraLabel lblScaleWeight;

	private UltraTextEditor txtScaleWeight;

	public UltraButton btnEmptyWeight;

	private UltraComboEditor cboTruckScale;

	private UltraLabel lblTruckScale;

	public UltraButton btnTruckScaleSearch;

	private UltraCheckEditor chkWithoutMIV;

	public frmSLTruckInvoices()
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

	public frmSLTruckInvoices(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		TS = new TruckScale(txtScaleWeight);
		if (GlobalFunctions.GetDefault("TruckScalePort") != "")
		{
			UltraTextEditor obj = txtScaleWeight;
			bool flag = (((Control)(object)lblScaleWeight).Visible = true);
			bool usingTruckScale = (((Control)(object)obj).Visible = flag);
			UsingTruckScale = usingTruckScale;
			try
			{
				TS.SetPort(GlobalFunctions.GetDefault("TruckScalePort"));
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show(ex.Message);
			}
		}
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
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtTruckScales = TruckScales.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtTruckScales.Rows.Count > 0)
		{
			UltraTextEditor obj2 = txtScaleWeight;
			bool flag = (((Control)(object)lblScaleWeight).Visible = true);
			bool usingTruckScale = (((Control)(object)obj2).Visible = flag);
			UsingTruckScale = usingTruckScale;
			UltraLabel obj3 = lblTruckScale;
			UltraComboEditor obj4 = cboTruckScale;
			flag = (((Control)(object)btnTruckScaleSearch).Visible = true);
			usingTruckScale = (((Control)(object)obj4).Visible = flag);
			((Control)(object)obj3).Visible = usingTruckScale;
			GlobalFunctions.FillCombo(cboTruckScale, dtTruckScales, "Port", "TruckScaleName");
			if (dtTruckScales.Rows.Count == 1)
			{
				cboTruckScale.SelectedIndex = 0;
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtSLQuotation = BusinessLayer.Sales.Quotations.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		if (GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoice"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackageCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackageCount"].Header).Caption = (GlobalVariables.IsArabic ? "العدد" : "Count");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن قائم" : "Truck Weight");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackageCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IssuedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckWeight"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SLInvoiceExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlInvoiceExpenses;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Value"].DefaultCellValue = 0;
		if (UsingTruckScale)
		{
			if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("InsertScaleValue"))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "InsertScaleValue");
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].Header).Caption = (GlobalVariables.IsArabic ? "ميزان" : "Scale");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].Style = (ColumnStyle)8;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].Header).VisiblePosition = ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckWeight"].Index - 1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InsertScaleValue"].DefaultCellValue = ">>";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["InsertScaleValue"].Value = ">>";
			}
		}
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
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["SLInvoiceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SLInvoiceDate"];
			((Control)(object)txtTruckNo).Text = drMaster["TruckNo"].ToString();
			((Control)(object)txtSubTruckNo).Text = drMaster["SubtruckNo"].ToString();
			((Control)(object)txtEmptyWeight).Text = ((drMaster["EmptyWeight"] == DBNull.Value) ? "0" : decimal.Parse(drMaster["EmptyWeight"].ToString()).ToString(GlobalVariables.txtDecimalFormate));
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboQuotation).Value = drMaster["QuotationID"];
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			rbIsDirectInvoice.Checked = bool.Parse(drMaster["IsDirectInvoice"].ToString());
			rbIsQuotation.Checked = bool.Parse(drMaster["IsQuotation"].ToString());
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((UltraToggleEditorBase)chkWithoutMIV).Checked = Convert.ToBoolean(drMaster["WithoutMIV"]);
			((TextEditorControlBase)cboTruckScale).Value = drMaster["TruckScaleID"];
			((Control)(object)txtGrossValue).Text = drMaster["GrossValue"].ToString();
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
			dtDetails = SLInvoicesDetails.SelectBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceExpenses = SLInvoicesExpenses.SelectBySLInvoiceID(drMaster["SLInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		rbIsQuotation.Enabled = !NavMode && !Updating;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode || (rbIsDirectInvoice.Checked ? NavMode : (!Adding && rbIsQuotation.Checked));
		((EditorButtonControlBase)cboQuotation).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)txtTruckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSubTruckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEmptyWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = (rbIsDirectInvoice.Checked ? (!NavMode) : (Adding && rbIsQuotation.Checked));
		((Control)(object)btnQuotationSearch).Visible = Adding && rbIsQuotation.Checked;
		((Control)(object)btnEmptyWeight).Visible = !NavMode && UsingTruckScale;
		((Control)(object)chkWithoutMIV).Enabled = !NavMode && (Adding || (drMaster != null && dtDetails.Rows.Count > 0 && dtDetails.Select(" IssuedQty > 0 ").Length != 0));
		if (Adding)
		{
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = " IsActive =1 ";
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			DataTable dataTable = dataView.ToTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboQuotation, dtSLQuotation, "QuotationID", "QuotationNo");
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
		if (Updating)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
					((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].Value = DBNull.Value;
					}
				}
				if (!bool.Parse(dataRow["IsService"].ToString()))
				{
					int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList = getUnitsValueList(unitTypeID);
					((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value = DBNull.Value;
					}
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtStampValue).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtGrowthFees).ValueChanged -= txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? SLInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
		cboQuotation.SelectedIndex = -1;
		((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
		rbIsQuotation.CheckedChanged -= RadioButtons_CheckedChanged;
		rbIsQuotation.Checked = false;
		rbIsQuotation.CheckedChanged += RadioButtons_CheckedChanged;
		((UltraToggleEditorBase)chkWithoutMIV).Checked = false;
		rbIsDirectInvoice.Checked = true;
		if (dtTruckScales.Rows.Count > 0)
		{
			cboTruckScale.SelectedIndex = 0;
		}
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtTruckNo).Clear();
		((TextEditorControlBase)txtSubTruckNo).Clear();
		((Control)(object)txtEmptyWeight).Text = "0";
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
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataExpenses).DataSource).Rows.Clear();
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
		if (rbIsQuotation.Checked && cboQuotation.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض السعر" : "Please Select Quotation No");
			((TextEditorControlBase)cboQuotation).Focus();
			cboQuotation.DropDown();
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
			if ((GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoice") || GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation")) && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاءإختيار المخزن  ", "Please Select Store ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
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
				if (UsingBatchNoAndValidityPeriod && i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString())
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف مع رقم التشيغلة مع نفس المخزن", "Cannot Duplicate The Same Item With the Same Batch No With Same Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (!UsingBatchNoAndValidityPeriod && i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("  لا يمكن تكرار الصنف مع نفس المخزن ", "Cannot Duplicate The Same Item with Same Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["ExpenseID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense Name ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["ExpenseID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				ULGDataExpenses.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة المصروف  ", "Please Enter Expense Amount ");
				ULGDataExpenses.ActiveCell = ((UltraGridBase)ULGDataExpenses).Rows[k].Cells["Value"];
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
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c05: Expected O, but got Unknown
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3a: Expected O, but got Unknown
		//IL_0cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			decimal invoiceExpense = CalculateInvoiceExpense();
			num = SLInvoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", "0", "0", "0", ((TextEditorControlBase)cboClient).Value.ToString(), "1", "1", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), "Null", "Null", "Null", "Null", "Null", ((Control)(object)txtTruckNo).Text, ((Control)(object)txtSubTruckNo).Text, (cboTruckScale.SelectedIndex == -1) ? "Null" : dtTruckScales.Select(" Port= '" + ((TextEditorControlBase)cboTruckScale).Value.ToString() + "'")[0]["TruckScaleID"].ToString(), (((Control)(object)txtEmptyWeight).Text == "") ? "0" : ((Control)(object)txtEmptyWeight).Text, "1", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, "Null", ((Control)(object)txtNotes).Text, "0", ((UltraToggleEditorBase)chkWithoutMIV).Checked ? "1" : "0", "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRowActualUnitSalesPriceAndReturnedPrice(((UltraGridBase)ULGData).Rows[i], invoiceExpense);
				int num2 = SLInvoicesDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Index.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["PackageCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["PackageCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["VirtualQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["VirtualQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["IssuedQty"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["IssuedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["DiscountAfterTax"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["DiscountAfterTax"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["StampsValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["StampsValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["GrowthFees"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["GrowthFees"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ReturnPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["ReturnPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			SLInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			string text = "";
			if (GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation") && !((UltraToggleEditorBase)chkWithoutMIV).Checked)
			{
				text = MaterialIssueVouchersDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
			}
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text + " لم يتم حفظ إذن الصرف ", text + " Erro Occured When Creating Material issue Voucher");
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
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (!GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoice") || ((UltraToggleEditorBase)chkWithoutMIV).Checked || GlobalFunctions.GetOption("CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation"))
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
			string text2 = MaterialIssueVouchersDetails.GenerateByInvoiceID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.UserID);
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2 + " لم يتم حفظ إذن الصرف ", text2 + " Erro Occured When Creating Material issue Voucher");
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
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Expected O, but got Unknown
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Expected O, but got Unknown
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Expected O, but got Unknown
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = SLInvoices.Insert_Update(drMaster["SLInvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectInvoice.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", "0", "0", "0", ((TextEditorControlBase)cboClient).Value.ToString(), "1", "1", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), "Null", "Null", "Null", "Null", "Null", ((Control)(object)txtTruckNo).Text, ((Control)(object)txtSubTruckNo).Text, (cboTruckScale.SelectedIndex == -1) ? "Null" : dtTruckScales.Select(" Port= '" + ((TextEditorControlBase)cboTruckScale).Value.ToString() + "'")[0]["TruckScaleID"].ToString(), (((Control)(object)txtEmptyWeight).Text == "") ? "0" : ((Control)(object)txtEmptyWeight).Text, bool.Parse(drMaster["IsTruckPolicy"].ToString()) ? "1" : "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtStampValue).Text == "") ? "0" : ((Control)(object)txtStampValue).Text, (((Control)(object)txtGrowthFees).Text == "") ? "0" : ((Control)(object)txtGrowthFees).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, "Null", ((Control)(object)txtNotes).Text, "0", ((UltraToggleEditorBase)chkWithoutMIV).Checked ? "1" : "0", (drMaster["EInvoiceInternalCode"] == DBNull.Value) ? "Null" : drMaster["EInvoiceInternalCode"].ToString(), (drMaster["EInvoiceUUID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceUUID"].ToString(), (drMaster["EInvoiceSenDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceSenDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceSendUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceSendUserID"].ToString(), bool.Parse(drMaster["EInvoiceIsCanceled"].ToString()) ? "1" : "0", (drMaster["EInvoiceCanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceCanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceCanceledUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceCanceledUserID"].ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			decimal invoiceExpense = CalculateInvoiceExpense();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["RowIndex"].Value = ((UltraGridBase)ULGData).Rows[i].Index;
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = ((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text);
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
			if (drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboClient).Value.ToString())
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
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_MaterialIssueVouchers Where Deleted=0 AND SLInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تمت صرفها من المخازن", "Cannot Delete This Transaction Because It Added in The Store ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From SC_ClientsDepartmentsReturns Where Deleted=0 AND SLInvoiceID = " + RowID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم ارتجاعها فى المخازن", "Cannot Delete This Transaction Because It Returned in The Store ");
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SL_SLInvoicesTruck_A.rpt" : "Rep_SL_SLInvoicesTruck_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLInvoicesTruckReport(-1, 0, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SLInvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
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
		DataView dataView = new DataView(dtStores);
		dataView.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView.ToTable();
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dataTable.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[l]["StoreID"], dataTable.Rows[l]["StoreName"].ToString());
		}
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
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtTruckScales = TruckScales.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTruckScale, dtTruckScales, "TruckScaleID", "TruckScaleName");
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3a: Expected O, but got Unknown
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Expected O, but got Unknown
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultStore.Rows.Count > 0 && dtPOSDefaultStore.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultStore.Rows[0]["DefaultStoreID"] : dtItems.Select(" ItemID= " + e.Cell.Value)[0]["DefaultStoreID"]);
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
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
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
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
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + num3)[0]["UnitID"].ToString();
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
		CalcTotalQty();
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
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
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount")
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
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && cboClient.SelectedIndex > -1)
				{
					frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "السعرالشامل" : "Total Price", _IsInt: false, _IsNumeric: true);
					frmEnterValue2.WindowState = FormWindowState.Normal;
					frmEnterValue2.ShowDialog();
					if (frmEnterValue2.Value != "" && decimal.Parse(frmEnterValue2.Value) > 0m)
					{
						DataRow dataRow = dtClients.Select("SubAccountID =" + ((TextEditorControlBase)cboClient).Value.ToString())[0];
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
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TruckWeight"].Value = CalculateWeightByIndex(((UltraGridBase)ULGData).ActiveRow.Index);
				CalculateWeight();
				if (((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" لايمكن نقص الكمية عن الكمية المنصرفة فى المخازن و قدرها " + ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString(), "You Cannot Decrease The Quantity From The Issued Qty in the Store" + ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value.ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["IssuedQty"].Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
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
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TruckWeight")
			{
				CalculateWeight();
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
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateWeight();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != ".")
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
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
		((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxRatio).Text == "" || ((Control)(object)txtDiscAfterTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text) / 100m * (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (e.KeyCode == Keys.Return && ((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
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
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
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
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
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
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = text4;
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
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		if (cboClient.SelectedIndex <= -1)
		{
			return;
		}
		if (rbIsQuotation.Checked)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			DataView dataView = new DataView(dtSLQuotation);
			dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
			dtDetails.Rows.Clear();
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPrice(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
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

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "0") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void cboTruckScale_ValueChanged(object sender, EventArgs e)
	{
		if (cboTruckScale.SelectedIndex != -1)
		{
			try
			{
				TS.SetPort(((TextEditorControlBase)cboTruckScale).Value.ToString());
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show(ex.Message);
			}
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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

	public void CalculateWeight()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (i == 0)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value.ToString()) - decimal.Parse((((Control)(object)txtEmptyWeight).Text == "") ? "0" : ((Control)(object)txtEmptyWeight).Text);
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value.ToString()) - decimal.Parse((((UltraGridBase)ULGData).Rows[i - 1].Cells["TruckWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i - 1].Cells["TruckWeight"].Value.ToString());
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
		}
	}

	public decimal CalculateWeightByIndex(int index)
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (i <= index)
			{
				num += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		return num += decimal.Parse((((Control)(object)txtEmptyWeight).Text == "") ? "0" : ((Control)(object)txtEmptyWeight).Text);
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
			((Control)(object)txtCode).Text = SLInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
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
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Expected O, but got Unknown
		if (cboQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = dtSLQuotation.Select(" QuotationID= " + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["SubAccountID"];
			if (cboClient.SelectedIndex > -1 && dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				dtItemPrices = ItemsPrices.GetPrice(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentageAfterTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			dtSLQuotationDetails = SLInvoicesDetails.FillByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtDetails.Rows.Clear();
			((UltraGridBase)ULGData).DataSource = dtSLQuotationDetails;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			InitGrid();
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
			((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((Adding || Updating) ? 6 : 2);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				DataRow dataRow = dtItems.Select(" ItemID= " + dtSLQuotationDetails.Rows[i]["ItemID"].ToString())[0];
				((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value = CalculateWeightByIndex(((UltraGridBase)ULGData).Rows[i].Index);
				if (!bool.Parse(dataRow["IsService"].ToString()))
				{
					ValueList unitsValueList = getUnitsValueList(int.Parse(dtUnits.Select(" UnitID =" + dtSLQuotationDetails.Rows[i]["UnitID"].ToString())[0]["UnitTypeID"].ToString()));
					((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
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
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
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
		((Control)(object)lblQuotationNo).Visible = rbIsQuotation.Checked;
		((Control)(object)cboQuotation).Visible = rbIsQuotation.Checked;
		((Control)(object)btnQuotationSearch).Visible = rbIsQuotation.Checked && Adding;
		((Control)(object)chkWithoutMIV).Visible = rbIsDirectInvoice.Checked;
		if (Adding)
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			cboQuotation.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
	}

	private void txtEmptyWeight_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtEmptyWeight).Text != "")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TruckWeight"].Value = CalculateWeightByIndex(((UltraGridBase)ULGData).Rows[i].Index);
			}
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (Adding || Updating)
		{
			e.Cell.Row.Cells["TruckWeight"].Value = ((Control)(object)txtScaleWeight).Text;
			CalculateWeight();
		}
	}

	private void btnEmptyWeight_Click(object sender, EventArgs e)
	{
		((Control)(object)txtEmptyWeight).Text = decimal.Parse(((Control)(object)txtScaleWeight).Text).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		if (e.Row.IsAddRow && UsingTruckScale)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			e.Row.Cells["InsertScaleValue"].Value = ">>";
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
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
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Expected O, but got Unknown
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Expected O, but got Unknown
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Expected O, but got Unknown
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Expected O, but got Unknown
		//IL_102e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Expected O, but got Unknown
		//IL_105e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1068: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.Transactions.frmSLTruckInvoices));
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
		this.pnlCheckType = new UltraPanel();
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
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnClientSearch = new UltraButton();
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
		this.cboItemBatchs = new UltraComboEditor();
		this.btnJV = new UltraButton();
		this.txtSerial = new UltraTextEditor();
		this.lblSerial = new UltraLabel();
		this.txtTruckNo = new UltraTextEditor();
		this.lblTruckNo = new UltraLabel();
		this.txtSubTruckNo = new UltraTextEditor();
		this.lblSubTruckNo = new UltraLabel();
		this.txtEmptyWeight = new UltraTextEditor();
		this.lblEmptyWeight = new UltraLabel();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.btnQuotationSearch = new UltraButton();
		this.lblQuotationNo = new UltraLabel();
		this.cboQuotation = new UltraComboEditor();
		this.lblScaleWeight = new UltraLabel();
		this.txtScaleWeight = new UltraTextEditor();
		this.btnEmptyWeight = new UltraButton();
		this.cboTruckScale = new UltraComboEditor();
		this.lblTruckScale = new UltraLabel();
		this.btnTruckScaleSearch = new UltraButton();
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
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemBatchs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTruckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubTruckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtScaleWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTruckScale).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutMIV).BeginInit();
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
		base.ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
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
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance14");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsQuotation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirectInvoice);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
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
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance1");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val12, "appearance2");
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val13, "appearance3");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance4");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance5");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterCellUpdate += new CellEventHandler(ULGDataExpenses_AfterCellUpdate);
		this.ULGDataExpenses.AfterRowsDeleted += new System.EventHandler(ULGDataExpenses_AfterRowsDeleted);
		this.ULGDataExpenses.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataExpenses_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Enter += new System.EventHandler(ULGDataExpenses_Enter);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataExpenses_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataExpenses_KeyPress);
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
		this.dtpDate.DateTime = new System.DateTime(2019, 10, 17, 0, 0, 0, 0);
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.Value = new System.DateTime(2019, 10, 17, 0, 0, 0, 0);
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance15");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDiscAfterTaxRatio, "lblDiscAfterTaxRatio");
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio).Name = "lblDiscAfterTaxRatio";
		resources.ApplyResources(this.txtDiscAfterTaxRatio, "txtDiscAfterTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).Name = "txtDiscAfterTaxRatio";
		((TextEditorControlBase)this.txtDiscAfterTaxRatio).ValueChanged += new System.EventHandler(txtDiscAfterTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscAfterTaxValue, "lblDiscAfterTaxValue");
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue).Name = "lblDiscAfterTaxValue";
		resources.ApplyResources(this.txtDiscAfterTaxValue, "txtDiscAfterTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).Name = "txtDiscAfterTaxValue";
		((TextEditorControlBase)this.txtDiscAfterTaxValue).ValueChanged += new System.EventHandler(txtDiscAfterTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblStampValue, "lblStampValue");
		((System.Windows.Forms.Control)(object)this.lblStampValue).Name = "lblStampValue";
		resources.ApplyResources(this.txtStampValue, "txtStampValue");
		((System.Windows.Forms.Control)(object)this.txtStampValue).Name = "txtStampValue";
		((TextEditorControlBase)this.txtStampValue).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtStampValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrowthFees, "lblGrowthFees");
		((System.Windows.Forms.Control)(object)this.lblGrowthFees).Name = "lblGrowthFees";
		resources.ApplyResources(this.txtGrowthFees, "txtGrowthFees");
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).Name = "txtGrowthFees";
		((TextEditorControlBase)this.txtGrowthFees).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtGrowthFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.cboItemBatchs, "cboItemBatchs");
		((TextEditorControlBase)this.cboItemBatchs).AlwaysInEditMode = true;
		this.cboItemBatchs.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemBatchs).Name = "cboItemBatchs";
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.txtSerial, "txtSerial");
		((System.Windows.Forms.Control)(object)this.txtSerial).Name = "txtSerial";
		((System.Windows.Forms.Control)(object)this.txtSerial).KeyUp += new System.Windows.Forms.KeyEventHandler(txtSerial_KeyUp);
		resources.ApplyResources(this.lblSerial, "lblSerial");
		this.lblSerial.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSerial).Name = "lblSerial";
		((ControlBase)this.lblSerial).WrapText = false;
		resources.ApplyResources(this.txtTruckNo, "txtTruckNo");
		((System.Windows.Forms.Control)(object)this.txtTruckNo).Name = "txtTruckNo";
		resources.ApplyResources(this.lblTruckNo, "lblTruckNo");
		this.lblTruckNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTruckNo).Name = "lblTruckNo";
		((ControlBase)this.lblTruckNo).WrapText = false;
		resources.ApplyResources(this.txtSubTruckNo, "txtSubTruckNo");
		((System.Windows.Forms.Control)(object)this.txtSubTruckNo).Name = "txtSubTruckNo";
		resources.ApplyResources(this.lblSubTruckNo, "lblSubTruckNo");
		this.lblSubTruckNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubTruckNo).Name = "lblSubTruckNo";
		((ControlBase)this.lblSubTruckNo).WrapText = false;
		resources.ApplyResources(this.txtEmptyWeight, "txtEmptyWeight");
		((System.Windows.Forms.Control)(object)this.txtEmptyWeight).Name = "txtEmptyWeight";
		((TextEditorControlBase)this.txtEmptyWeight).ValueChanged += new System.EventHandler(txtEmptyWeight_ValueChanged);
		resources.ApplyResources(this.lblEmptyWeight, "lblEmptyWeight");
		this.lblEmptyWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmptyWeight).Name = "lblEmptyWeight";
		((ControlBase)this.lblEmptyWeight).WrapText = false;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.btnQuotationSearch, "btnQuotationSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance16");
		((ControlBase)this.btnQuotationSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Name = "btnQuotationSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Click += new System.EventHandler(btnQuotationSearch_Click);
		resources.ApplyResources(this.lblQuotationNo, "lblQuotationNo");
		this.lblQuotationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotationNo).Name = "lblQuotationNo";
		((ControlBase)this.lblQuotationNo).WrapText = false;
		resources.ApplyResources(this.cboQuotation, "cboQuotation");
		((TextEditorControlBase)this.cboQuotation).AlwaysInEditMode = true;
		this.cboQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotation).Name = "cboQuotation";
		((TextEditorControlBase)this.cboQuotation).ValueChanged += new System.EventHandler(cboQuotation_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboQuotation).KeyDown += new System.Windows.Forms.KeyEventHandler(cboQuotation_KeyDown);
		resources.ApplyResources(this.lblScaleWeight, "lblScaleWeight");
		this.lblScaleWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblScaleWeight).Name = "lblScaleWeight";
		((ControlBase)this.lblScaleWeight).WrapText = false;
		resources.ApplyResources(this.txtScaleWeight, "txtScaleWeight");
		((System.Windows.Forms.Control)(object)this.txtScaleWeight).Name = "txtScaleWeight";
		((EditorButtonControlBase)this.txtScaleWeight).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtScaleWeight).TabStop = false;
		resources.ApplyResources(this.btnEmptyWeight, "btnEmptyWeight");
		((System.Windows.Forms.Control)(object)this.btnEmptyWeight).Name = "btnEmptyWeight";
		((System.Windows.Forms.Control)(object)this.btnEmptyWeight).Click += new System.EventHandler(btnEmptyWeight_Click);
		resources.ApplyResources(this.cboTruckScale, "cboTruckScale");
		((TextEditorControlBase)this.cboTruckScale).AlwaysInEditMode = true;
		this.cboTruckScale.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTruckScale).Name = "cboTruckScale";
		((TextEditorControlBase)this.cboTruckScale).ValueChanged += new System.EventHandler(cboTruckScale_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboTruckScale).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblTruckScale, "lblTruckScale");
		this.lblTruckScale.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTruckScale).Name = "lblTruckScale";
		((ControlBase)this.lblTruckScale).WrapText = false;
		resources.ApplyResources(this.btnTruckScaleSearch, "btnTruckScaleSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance17");
		((ControlBase)this.btnTruckScaleSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnTruckScaleSearch).Name = "btnTruckScaleSearch";
		((System.Windows.Forms.Control)(object)this.btnTruckScaleSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.chkWithoutMIV, "chkWithoutMIV");
		((System.Windows.Forms.Control)(object)this.chkWithoutMIV).Name = "chkWithoutMIV";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithoutMIV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtScaleWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblScaleWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEmptyWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEmptyWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTruckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTruckScaleSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTruckScale);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTruckScale);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemBatchs);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEmptyWeight);
		base.Name = "frmSLTruckInvoices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEmptyWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemBatchs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTruckScale, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTruckScale, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTruckScaleSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubTruckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEmptyWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEmptyWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblScaleWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtScaleWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStampValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemBatchs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTruckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubTruckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtScaleWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTruckScale).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutMIV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
