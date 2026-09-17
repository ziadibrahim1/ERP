using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Export;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Export.Transactions;

public class frmExpOperations : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtClientsBanks;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtUnits;

	private DataTable dtSeaPorts;

	private DataTable dtPackingTypes;

	private DataTable dtDeliveryTerms;

	private DataTable dtPaymentsMethods;

	private DataTable dtContainersSizes;

	private DataTable dtContainersTypes;

	private DataTable dtShippers;

	private DataTable dtShippersBanks;

	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtOperationDocumentsRequired;

	private DataTable X;

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlPackingTypes = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool CanOpenLetter = false;

	private IContainer components = null;

	private UltraLabel lblContainersType;

	private UltraComboEditor cboContainersTypes;

	private UltraTextEditor txtTotalNetWeight;

	private UltraLabel lblTotalNetWeight;

	private UltraLabel lblTotalPrice;

	private UltraLabel lblContainersCount;

	private UltraTextEditor txtContainersCount;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraLabel lblShipper;

	private UltraComboEditor cboShippers;

	private UltraLabel lblConsignee;

	private UltraLabel lblPaidPercentage;

	private UltraTextEditor txtPaidPercentage;

	private UltraLabel ultraLabel6;

	private UltraLabel lblExpectedDeliveryDate;

	private UltraDateTimeEditor dtpExpectedDeliveryDate;

	private UltraLabel lblPaymentMethod;

	private UltraComboEditor cboPaymentMethod;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblNotify;

	private UltraComboEditor cboBuyer;

	private UltraLabel lblBuyer;

	public UltraButton btnPODSearch;

	public UltraButton btnPOLSearch;

	private UltraLabel lblLoadingSeaPort;

	private UltraComboEditor cboLoadingSeaPort;

	private UltraComboEditor cboDischargeSeaPort;

	private UltraLabel lblDischargeSeaPort;

	private UltraComboEditor cboContainersSizes;

	private UltraLabel lblContainersSize;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel lblCustomsTotalPrice;

	private UltraTextEditor txtCustomsTotalPrice;

	private UltraTextEditor txtTotalGrossWeight;

	private UltraLabel lblTotalGrossWeight;

	private UltraTextEditor txtSticker;

	private UltraLabel lblSticker;

	private UltraTextEditor txtStickerNotes;

	private UltraLabel lblStickerNotes;

	private UltraComboEditor cboDeliveryTerms;

	private UltraLabel ultraLabel1;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGReports;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDocumentsRequired;

	private UltraLabel lblAdvancePaymentPercentage;

	private UltraTextEditor txtAdvancePaymentPercentage;

	private UltraLabel ultraLabel3;

	private UltraLabel lblAdvancePaymentAmount;

	private UltraTextEditor txtAdvancePaymentAmount;

	private UltraLabel lblTTPercentage;

	private UltraTextEditor txtTTPercentage;

	private UltraLabel ultraLabel7;

	private UltraLabel lblTTAmount;

	private UltraTextEditor txtTTAmount;

	private UltraLabel lblLCPercentage;

	private UltraTextEditor txtLCPercentage;

	private UltraLabel ultraLabel10;

	private UltraLabel lblLCAmount;

	private UltraTextEditor txtLCAmount;

	private UltraLabel lblCADPercentage;

	private UltraTextEditor txtCADPercentage;

	private UltraLabel ultraLabel13;

	private UltraLabel lblCADAmount;

	private UltraTextEditor txtCADAmount;

	private UltraTextEditor txtDeliverySchedule;

	private UltraLabel lblDeliverySchedule;

	private UltraTextEditor txtContractNo;

	private UltraLabel lblContractNo;

	private UltraComboEditor cboBuyerBank;

	private UltraLabel lblBuyerBank;

	private UltraComboEditor cboShipperBank;

	private UltraLabel lblShipperBank;

	private RichTextBox txtConsignee;

	private RichTextBox txtNotify;

	private UltraComboEditor cboBroker;

	private UltraLabel lblBroker;

	private UltraTextEditor txtLogisticsNotes;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtFinanceNotes;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtDeclaredTotalPrice;

	private UltraCheckEditor chkFinanceMemo;

	private UltraCheckEditor chkLogisticsMemo;

	private UltraCheckEditor chkProductionMemo;

	private UltraDateTimeEditor dtpFinanceMemoDate;

	private UltraDateTimeEditor dtpLogisticsMemoDate;

	private UltraDateTimeEditor dtpProductionMemoDate;

	private UltraLabel ultraLabel8;

	private UltraTextEditor txtBrokercommissionValue;

	private UltraButton btnConsignee;

	private UltraButton btnNotify;

	private UltraCheckEditor chkPartialShipment;

	public frmExpOperations()
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
		TableName = "EXP_Operations";
		IDCol = "OperationID";
		NoCol = "OperationNo";
		DateCol = "OperationDate";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
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
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtPackingTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackingTypes.ValueListItems.Clear();
		for (int n = 0; n < dtPackingTypes.Rows.Count; n++)
		{
			vlPackingTypes.ValueListItems.Add(dtPackingTypes.Rows[n]["PackingTypeID"], dtPackingTypes.Rows[n]["PackingTypeName"].ToString());
		}
		dtShippers = Shippers.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboShippers, dtShippers, "ShipperID", "ShipperName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBroker, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboBuyer, dtClients, "SubAccountID", "SubAccountName");
		dtPaymentsMethods = BusinessLayer.Export.PaymentMethods.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentsMethods, "PaymentMethodID", "PaymentMethodName");
		dtClientsBanks = SubAccountsClientSupplierBanks.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboBuyerBank, dtClientsBanks, "ClientSupplierBankID", "BankName");
		dtShippersBanks = ShippersBanks.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboShipperBank, dtShippersBanks, "ShipperBankID", "BankName");
		dtDeliveryTerms = DeliveryTerms.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDeliveryTerms, dtDeliveryTerms, "DeliveryTermID", "DeliveryTermName");
		dtContainersSizes = ContainersSizes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersSizes, dtContainersSizes, "ContainerSizeID", "ContainerSizeName");
		dtContainersTypes = BusinessLayer.Export.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersTypes, dtContainersTypes, "ContainerTypeID", "ContainerTypeName");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtSeaPorts);
		dataView.RowFilter = "IsLoadingPort = 1";
		cboLoadingSeaPort.DataSource = dataView;
		cboLoadingSeaPort.DisplayMember = "SeaPortName";
		cboLoadingSeaPort.ValueMember = "SeaPortID";
		DataView dataView2 = new DataView(dtSeaPorts);
		dataView2.RowFilter = "IsLoadingPort = 0";
		cboDischargeSeaPort.DataSource = dataView2;
		cboDischargeSeaPort.DisplayMember = "SeaPortName";
		cboDischargeSeaPort.ValueMember = "SeaPortID";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("Export", "Reports", "frmExpOperationsRep"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGReports).DataSource = dtReports;
		InitGridReports();
		FillCurrencyDropDown();
		dtDetails = OperationsDetails.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtOperationDocumentsRequired = OperationsDocumentsRequired.GetSelectedByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDocumentsRequired).DataSource = dtOperationDocumentsRequired;
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		InitGrid();
		InitGridDocuments();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف " : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarcode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarcode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "Item Barcode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarcode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarcode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "السريل " : "Batch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية " : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة " : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingTypeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع التغليف" : "Packing Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingTypeID"].ValueList = (IValueList)(object)vlPackingTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingWeight"].Header).Caption = (GlobalVariables.IsArabic ? "وزن العبوة بالكجم" : "Package Weight in KG");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PackingWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الطرود " : "Bags Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة " : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "إجمالي السعر" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر المعلن للوحدة" : "DeclaredUnit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredTotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي السعر المعلن" : "Declared Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeclaredTotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "جمرك الوحدة " : "Customs Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي الجمارك " : "Customs Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FreightUnit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FreightUnit"].Header).Caption = (GlobalVariables.IsArabic ? "نولون الوحدة" : "Freight Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FreightUnit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FreightUnit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضاعة" : "Goods Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BusinessLayer.Export.Operations.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboShippers).ValueChanged -= cboShippers_ValueChanged;
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged -= txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged -= txtAdvancePaymentPercentage_ValueChanged;
			((TextEditorControlBase)txtTTAmount).ValueChanged -= txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged -= txtTTPercentage_ValueChanged;
			((TextEditorControlBase)txtLCAmount).ValueChanged -= txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged -= txtLCPercentage_ValueChanged;
			((TextEditorControlBase)txtCADAmount).ValueChanged -= txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged -= txtCADPercentage_ValueChanged;
			((TextEditorControlBase)cboBuyer).ValueChanged -= cboBuyer_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["OperationNo"].ToString();
			((Control)(object)txtContainersCount).Text = drMaster["ContainersCount"].ToString();
			((Control)(object)txtCustomsTotalPrice).Text = decimal.Parse(drMaster["CustomsTotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtPaidPercentage).Text = decimal.Parse(drMaster["PaymentPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSticker).Text = drMaster["Sticker"].ToString();
			((Control)(object)txtStickerNotes).Text = drMaster["StickerNote"].ToString();
			((Control)(object)txtTotalGrossWeight).Text = decimal.Parse(drMaster["TotalGrossWeight"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalNetWeight).Text = decimal.Parse(drMaster["TotalNetWeight"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtContractNo).Text = drMaster["ContractNo"].ToString();
			((Control)(object)txtDeliverySchedule).Text = drMaster["DeliverySchedule"].ToString();
			((Control)(object)txtAdvancePaymentAmount).Text = decimal.Parse(drMaster["AdvancePaymentAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtAdvancePaymentPercentage).Text = decimal.Parse(drMaster["AdvancePaymentPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTTAmount).Text = decimal.Parse(drMaster["TTAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTTPercentage).Text = decimal.Parse(drMaster["TTPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLCAmount).Text = decimal.Parse(drMaster["LCAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLCPercentage).Text = decimal.Parse(drMaster["LCPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtCADAmount).Text = decimal.Parse(drMaster["CADAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtCADPercentage).Text = decimal.Parse(drMaster["CADPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			txtConsignee.Text = drMaster["Consignee"].ToString();
			txtNotify.Text = drMaster["Notify"].ToString();
			((Control)(object)txtLogisticsNotes).Text = drMaster["LogisticsNotes"].ToString();
			((Control)(object)txtFinanceNotes).Text = drMaster["FinanceNotes"].ToString();
			dtpFinanceMemoDate.Value = drMaster["FinanceMemoDate"];
			dtpLogisticsMemoDate.Value = drMaster["LogisticsMemoDate"];
			dtpProductionMemoDate.Value = drMaster["ProductionMemoDate"];
			((UltraToggleEditorBase)chkFinanceMemo).Checked = Convert.ToBoolean(drMaster["IsFinanceMemo"]);
			((UltraToggleEditorBase)chkLogisticsMemo).Checked = Convert.ToBoolean(drMaster["IsLogisticsMemo"]);
			((UltraToggleEditorBase)chkProductionMemo).Checked = Convert.ToBoolean(drMaster["IsProductionMemo"]);
			((UltraToggleEditorBase)chkPartialShipment).Checked = Convert.ToBoolean(drMaster["IsPartialShipment"]);
			((Control)(object)txtDeclaredTotalPrice).Text = Convert.ToDecimal(drMaster["DeclaredTotalPrice"]).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtBrokercommissionValue).Text = decimal.Parse(drMaster["BrokercommissionValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboBuyerBank).Value = drMaster["ClientSupplierBankID"];
			((TextEditorControlBase)cboShipperBank).Value = drMaster["ShipperBankID"];
			((TextEditorControlBase)cboBuyer).Value = drMaster["BuyerSubAccountID"];
			((TextEditorControlBase)cboBroker).Value = drMaster["BrokerSubAccountID"];
			((TextEditorControlBase)cboContainersSizes).Value = drMaster["ContainerSizeID"];
			((TextEditorControlBase)cboContainersTypes).Value = drMaster["ContainerTypeID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboDischargeSeaPort).Value = drMaster["DischargeSeaPortID"];
			((TextEditorControlBase)cboLoadingSeaPort).Value = drMaster["LoadingSeaPortID"];
			((TextEditorControlBase)cboPaymentMethod).Value = drMaster["PaymentMethodID"];
			((TextEditorControlBase)cboDeliveryTerms).Value = drMaster["DeliveryTermID"];
			((TextEditorControlBase)cboShippers).Value = drMaster["ShipperID"];
			dtpExpectedDeliveryDate.Value = drMaster["ExpectedDeliveryDate"];
			dtpDate.Value = drMaster["OperationDate"];
			DataView dataView = new DataView(dtClientsBanks);
			dataView.RowFilter = "SubAccountID = " + ((((TextEditorControlBase)cboBuyer).Value == null) ? "-1" : ((TextEditorControlBase)cboBuyer).Value.ToString());
			GlobalFunctions.FillCombo(cboBuyerBank, dataView.ToTable(), "ClientSupplierBankID", "BankName");
			DataView dataView2 = new DataView(dtShippersBanks);
			dataView2.RowFilter = "ShipperID = " + ((((TextEditorControlBase)cboShippers).Value == null) ? "-1" : ((TextEditorControlBase)cboShippers).Value.ToString());
			GlobalFunctions.FillCombo(cboShipperBank, dataView2.ToTable(), "ShipperBankID", "BankName");
			((TextEditorControlBase)cboBuyer).ValueChanged += cboBuyer_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged += txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged += txtAdvancePaymentPercentage_ValueChanged;
			((TextEditorControlBase)txtTTAmount).ValueChanged += txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged += txtTTPercentage_ValueChanged;
			((TextEditorControlBase)txtLCAmount).ValueChanged += txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged += txtLCPercentage_ValueChanged;
			((TextEditorControlBase)txtCADAmount).ValueChanged += txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged += txtCADPercentage_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboShippers).ValueChanged += cboShippers_ValueChanged;
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			dtDetails = OperationsDetails.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			dtOperationDocumentsRequired = OperationsDocumentsRequired.GetSelectedByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDocumentsRequired).DataSource = dtOperationDocumentsRequired;
			InitGrid();
			InitGridDocuments();
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
		((EditorButtonControlBase)cboBuyer).ReadOnly = NavMode;
		((EditorButtonControlBase)cboContainersSizes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboContainersTypes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDischargeSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLoadingSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDeliveryTerms).ReadOnly = NavMode;
		((EditorButtonControlBase)cboShippers).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBuyerBank).ReadOnly = NavMode;
		((EditorButtonControlBase)cboShipperBank).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBroker).ReadOnly = NavMode;
		((Control)(object)btnConsignee).Enabled = !NavMode;
		((Control)(object)btnNotify).Enabled = !NavMode;
		((EditorButtonControlBase)dtpExpectedDeliveryDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		txtConsignee.ReadOnly = NavMode;
		txtNotify.ReadOnly = NavMode;
		((EditorButtonControlBase)txtLogisticsNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFinanceNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpFinanceMemoDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpLogisticsMemoDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpProductionMemoDate).ReadOnly = NavMode;
		((Control)(object)chkPartialShipment).Enabled = !NavMode;
		((Control)(object)chkFinanceMemo).Enabled = !NavMode;
		((Control)(object)chkLogisticsMemo).Enabled = !NavMode;
		((Control)(object)chkProductionMemo).Enabled = !NavMode;
		((EditorButtonControlBase)txtBrokercommissionValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtContractNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)txtContainersCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSticker).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStickerNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDeliverySchedule).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdvancePaymentAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdvancePaymentPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTTAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTTPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLCAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLCPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCADAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCADPercentage).ReadOnly = NavMode;
		((Control)(object)btnPOLSearch).Visible = !NavMode;
		((Control)(object)btnPODSearch).Visible = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? BusinessLayer.Export.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((Control)(object)txtContractNo).Text = (Adding ? BusinessLayer.Export.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboBuyer.SelectedIndex = -1;
		cboContainersSizes.SelectedIndex = -1;
		cboContainersTypes.SelectedIndex = -1;
		cboCurrency.SelectedIndex = -1;
		cboDischargeSeaPort.SelectedIndex = -1;
		cboLoadingSeaPort.SelectedIndex = -1;
		cboBroker.SelectedIndex = -1;
		cboPaymentMethod.SelectedIndex = -1;
		cboShippers.SelectedIndex = -1;
		cboDeliveryTerms.SelectedIndex = -1;
		cboBuyerBank.SelectedIndex = -1;
		cboShipperBank.SelectedIndex = -1;
		((UltraToggleEditorBase)chkPartialShipment).Checked = false;
		txtNotify.Clear();
		txtConsignee.Clear();
		((TextEditorControlBase)txtFinanceNotes).Clear();
		((TextEditorControlBase)txtLogisticsNotes).Clear();
		((TextEditorControlBase)txtContainersCount).Clear();
		((TextEditorControlBase)txtCustomsTotalPrice).Clear();
		((TextEditorControlBase)txtExchangeRate).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtPaidPercentage).Text = "0";
		((TextEditorControlBase)txtSticker).Clear();
		((TextEditorControlBase)txtStickerNotes).Clear();
		((TextEditorControlBase)txtTotalGrossWeight).Clear();
		((TextEditorControlBase)txtTotalNetWeight).Clear();
		((TextEditorControlBase)txtContractNo).Clear();
		((TextEditorControlBase)txtDeliverySchedule).Clear();
		((Control)(object)txtAdvancePaymentAmount).Text = "0";
		((Control)(object)txtAdvancePaymentPercentage).Text = "0";
		((Control)(object)txtTTAmount).Text = "0";
		((Control)(object)txtTTPercentage).Text = "0";
		((Control)(object)txtLCAmount).Text = "0";
		((Control)(object)txtLCPercentage).Text = "0";
		((Control)(object)txtCADAmount).Text = "0";
		((Control)(object)txtCADPercentage).Text = "0";
		((Control)(object)txtBrokercommissionValue).Text = "0";
		((Control)(object)txtDeclaredTotalPrice).Text = "0";
		((UltraToggleEditorBase)chkFinanceMemo).Checked = false;
		((UltraToggleEditorBase)chkLogisticsMemo).Checked = false;
		((UltraToggleEditorBase)chkProductionMemo).Checked = false;
		dtpFinanceMemoDate.Value = DBNull.Value;
		dtpLogisticsMemoDate.Value = DBNull.Value;
		dtpProductionMemoDate.Value = DBNull.Value;
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		dtOperationDocumentsRequired = OperationsDocumentsRequired.GetSelectedByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDocumentsRequired).DataSource = dtOperationDocumentsRequired;
		((TextEditorControlBase)txtTotalPrice).Clear();
		dtpExpectedDeliveryDate.Value = DBNull.Value;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		dtpDate.ValueChanged += dtpDate_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value || dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ العملية" : "Please Enter The Operation Date");
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم العملية" : "Please Enter The Operation No");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العملة " : "Please Select The Currency");
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
		if (cboShippers.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار اسم الشاحن" : "Please Select The Shipper");
			((TextEditorControlBase)cboShippers).Focus();
			cboShippers.DropDown();
			return false;
		}
		if (cboShipperBank.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار بنك الشاحن " : "Please Select The Shipper Bank");
			((TextEditorControlBase)cboShipperBank).Focus();
			cboShipperBank.DropDown();
			return false;
		}
		if (cboDischargeSeaPort.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار ميناء التفريغ" : "Please Select POD");
			((TextEditorControlBase)cboDischargeSeaPort).Focus();
			cboDischargeSeaPort.DropDown();
			return false;
		}
		if (cboBuyer.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل " : "Please Select The Buyer");
			((TextEditorControlBase)cboBuyer).Focus();
			cboBuyer.DropDown();
			return false;
		}
		if (cboContainersTypes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار أنواع الحاويات " : "Please Select The Containers Types");
			((TextEditorControlBase)cboContainersTypes).Focus();
			cboContainersTypes.DropDown();
			return false;
		}
		if (cboContainersSizes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مقاسات الحاويات " : "Please Select The Containers Sizes");
			((TextEditorControlBase)cboContainersSizes).Focus();
			cboContainersSizes.DropDown();
			return false;
		}
		if (cboDeliveryTerms.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار شروط الشحن " : "Please Select The Delivery Terms");
			((TextEditorControlBase)cboDeliveryTerms).Focus();
			cboDeliveryTerms.DropDown();
			return false;
		}
		if (cboPaymentMethod.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار طريقة الدفع " : "Please Select The Payment Method");
			((TextEditorControlBase)cboPaymentMethod).Focus();
			cboPaymentMethod.DropDown();
			return false;
		}
		if (((Control)(object)txtContainersCount).Text.Trim() == "" || int.Parse(((Control)(object)txtContainersCount).Text) <= 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الحاويات" : "Please Enter The Containers Count");
			((TextEditorControlBase)txtContainersCount).Focus();
			return false;
		}
		if (Main.CheckForValue("EXP_Operations", "ContractNo", ((Control)(object)txtContractNo).Text, Adding ? "0" : drMaster["ContractNo"].ToString(), IsFromServer: true) > 0)
		{
			string contractNo = BusinessLayer.Export.Operations.GetContractNo(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboBuyer).Value.ToString(), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا التعاقد متواجد من قبل \n سوف يتم الحفظ برقم " + contractNo, "The Contract No. Already Exists It Will Be Saved With No. : " + contractNo);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtContractNo).Focus();
				return false;
			}
			((Control)(object)txtContractNo).Text = contractNo;
		}
		if (Main.CheckForValue("EXP_Operations", "OperationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationNo"].ToString(), IsFromServer: true) > 0)
		{
			string codeByBranchID = BusinessLayer.Export.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذه العملية متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Operation No. Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذه العملية", "Please insert Details For This Operation");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "إسم الصنف متواجد من قبل" : "Item Already Exists");
					return false;
				}
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال نوع التغليف  ", "Please Enter Packing Type");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال عدد الطرود  ", "Please Enter Bags Count ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"];
				ULGData.PerformAction((UltraGridAction)24);
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
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			bool flag = false;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["DeclaredUnitPrice"].Value == null)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["DeclaredUnitPrice"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value;
					((UltraGridBase)ULGData).Rows[i].Cells["DeclaredTotalPrice"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value;
					flag = true;
				}
			}
			((UltraGridBase)ULGData).UpdateData();
			if (flag)
			{
				DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
				if (dataTable.Rows.Count > 0)
				{
					object obj = dataTable.Compute(" Sum(DeclaredTotalPrice) ", "");
					if (obj != DBNull.Value)
					{
						((Control)(object)txtDeclaredTotalPrice).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
					}
				}
			}
			int num = BusinessLayer.Export.Operations.Insert_Update("-1", ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtContractNo).Text, (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (cboShippers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShippers).Value.ToString(), txtConsignee.Text, txtNotify.Text, (cboBuyer.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuyer).Value.ToString(), (cboBroker.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBroker).Value.ToString(), (((Control)(object)txtBrokercommissionValue).Text == "") ? "0" : ((Control)(object)txtBrokercommissionValue).Text, (cboLoadingSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingSeaPort).Value.ToString(), (cboDischargeSeaPort.SelectedIndex == -15) ? "Null" : ((TextEditorControlBase)cboDischargeSeaPort).Value.ToString(), (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboDeliveryTerms.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryTerms).Value.ToString(), ((Control)(object)txtDeliverySchedule).Text, (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), (((Control)(object)txtPaidPercentage).Text == "") ? "0" : ((Control)(object)txtPaidPercentage).Text, (((Control)(object)txtAdvancePaymentAmount).Text == "") ? "0" : ((Control)(object)txtAdvancePaymentAmount).Text, (((Control)(object)txtAdvancePaymentPercentage).Text == "") ? "0" : ((Control)(object)txtAdvancePaymentPercentage).Text, (((Control)(object)txtTTAmount).Text == "") ? "0" : ((Control)(object)txtTTAmount).Text, (((Control)(object)txtTTPercentage).Text == "") ? "0" : ((Control)(object)txtTTPercentage).Text, (((Control)(object)txtLCAmount).Text == "") ? "0" : ((Control)(object)txtLCAmount).Text, (((Control)(object)txtLCPercentage).Text == "") ? "0" : ((Control)(object)txtLCPercentage).Text, (((Control)(object)txtCADAmount).Text == "") ? "0" : ((Control)(object)txtCADAmount).Text, (((Control)(object)txtCADPercentage).Text == "") ? "0" : ((Control)(object)txtCADPercentage).Text, (cboShipperBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShipperBank).Value.ToString(), (cboBuyerBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuyerBank).Value.ToString(), (((Control)(object)txtContainersCount).Text == "") ? "0" : ((Control)(object)txtContainersCount).Text, (cboContainersTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersTypes).Value.ToString(), (cboContainersSizes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersSizes).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtCustomsTotalPrice).Text == "") ? "0" : ((Control)(object)txtCustomsTotalPrice).Text, (((Control)(object)txtDeclaredTotalPrice).Text == "") ? "0" : ((Control)(object)txtDeclaredTotalPrice).Text, (((Control)(object)txtTotalNetWeight).Text == "") ? "0" : ((Control)(object)txtTotalNetWeight).Text, (((Control)(object)txtTotalGrossWeight).Text == "") ? "0" : ((Control)(object)txtTotalGrossWeight).Text, (((Control)(object)txtSticker).Text == "") ? "Null" : ((Control)(object)txtSticker).Text, (((Control)(object)txtStickerNotes).Text == "") ? "Null" : ((Control)(object)txtStickerNotes).Text, (((Control)(object)txtLogisticsNotes).Text == "") ? "Null" : ((Control)(object)txtLogisticsNotes).Text, (((Control)(object)txtFinanceNotes).Text == "") ? "Null" : ((Control)(object)txtFinanceNotes).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkFinanceMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkFinanceMemo).Checked) ? "Null" : ((dtpFinanceMemoDate.Value == null) ? "Null" : dtpFinanceMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkLogisticsMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkLogisticsMemo).Checked) ? "Null" : ((dtpLogisticsMemoDate.Value == null) ? "Null" : dtpLogisticsMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkProductionMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkProductionMemo).Checked) ? "Null" : ((dtpProductionMemoDate.Value == null) ? "Null" : dtpProductionMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkPartialShipment).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["OperationDetailID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[j].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				if (((UltraGridBase)ULGData).Rows[j].Cells["DeclaredUnitPrice"].Value == null)
				{
					((UltraGridBase)ULGData).Rows[j].Cells["DeclaredUnitPrice"].Value = ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value;
					((UltraGridBase)ULGData).Rows[j].Cells["DeclaredTotalPrice"].Value = ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value;
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDocumentsRequired).Rows).Count; k++)
			{
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["OperationDocumentRequiredID"].Value = "-1";
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["Deleted"].Value = false;
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			dtOperationDocumentsRequired.AcceptChanges();
			X = dtOperationDocumentsRequired.Clone();
			DataRow[] array = dtOperationDocumentsRequired.Select("Selected = 1 ");
			if (array.Length != 0)
			{
				DataRow[] array2 = array;
				foreach (DataRow row in array2)
				{
					X.ImportRow(row);
				}
				OperationsDocumentsRequired.Insert_UpdateByTable(X, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		Main.StartBulkTrans(FromServer: true);
		try
		{
			bool flag = false;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["DeclaredUnitPrice"].Value == null || Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["DeclaredUnitPrice"].Value) == 0m)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["DeclaredUnitPrice"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value;
					((UltraGridBase)ULGData).Rows[i].Cells["DeclaredTotalPrice"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value;
					flag = true;
				}
			}
			((UltraGridBase)ULGData).UpdateData();
			if (flag)
			{
				DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
				if (dataTable.Rows.Count > 0)
				{
					object obj = dataTable.Compute(" Sum(DeclaredTotalPrice) ", "");
					if (obj != DBNull.Value)
					{
						((Control)(object)txtDeclaredTotalPrice).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
					}
				}
			}
			int num = BusinessLayer.Export.Operations.Insert_Update(drMaster["OperationID"].ToString(), ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtContractNo).Text, (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (cboShippers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShippers).Value.ToString(), txtConsignee.Text, txtNotify.Text, (cboBuyer.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuyer).Value.ToString(), (cboBroker.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBroker).Value.ToString(), (((Control)(object)txtBrokercommissionValue).Text == "") ? "0" : ((Control)(object)txtBrokercommissionValue).Text, (cboLoadingSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingSeaPort).Value.ToString(), (cboDischargeSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDischargeSeaPort).Value.ToString(), (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboDeliveryTerms.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryTerms).Value.ToString(), ((Control)(object)txtDeliverySchedule).Text, (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), (((Control)(object)txtPaidPercentage).Text == "") ? "0" : ((Control)(object)txtPaidPercentage).Text, (((Control)(object)txtAdvancePaymentAmount).Text == "") ? "0" : ((Control)(object)txtAdvancePaymentAmount).Text, (((Control)(object)txtAdvancePaymentPercentage).Text == "") ? "0" : ((Control)(object)txtAdvancePaymentPercentage).Text, (((Control)(object)txtTTAmount).Text == "") ? "0" : ((Control)(object)txtTTAmount).Text, (((Control)(object)txtTTPercentage).Text == "") ? "0" : ((Control)(object)txtTTPercentage).Text, (((Control)(object)txtLCAmount).Text == "") ? "0" : ((Control)(object)txtLCAmount).Text, (((Control)(object)txtLCPercentage).Text == "") ? "0" : ((Control)(object)txtLCPercentage).Text, (((Control)(object)txtCADAmount).Text == "") ? "0" : ((Control)(object)txtCADAmount).Text, (((Control)(object)txtCADPercentage).Text == "") ? "0" : ((Control)(object)txtCADPercentage).Text, (cboShipperBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShipperBank).Value.ToString(), (cboBuyerBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuyerBank).Value.ToString(), (((Control)(object)txtContainersCount).Text == "") ? "0" : ((Control)(object)txtContainersCount).Text, (cboContainersTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersTypes).Value.ToString(), (cboContainersSizes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersSizes).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtCustomsTotalPrice).Text == "") ? "0" : ((Control)(object)txtCustomsTotalPrice).Text, (((Control)(object)txtDeclaredTotalPrice).Text == "") ? "0" : ((Control)(object)txtDeclaredTotalPrice).Text, (((Control)(object)txtTotalNetWeight).Text == "") ? "0" : ((Control)(object)txtTotalNetWeight).Text, (((Control)(object)txtTotalGrossWeight).Text == "") ? "0" : ((Control)(object)txtTotalGrossWeight).Text, (((Control)(object)txtSticker).Text == "") ? "Null" : ((Control)(object)txtSticker).Text, (((Control)(object)txtStickerNotes).Text == "") ? "Null" : ((Control)(object)txtStickerNotes).Text, (((Control)(object)txtLogisticsNotes).Text == "") ? "Null" : ((Control)(object)txtLogisticsNotes).Text, (((Control)(object)txtFinanceNotes).Text == "") ? "Null" : ((Control)(object)txtFinanceNotes).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkFinanceMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkFinanceMemo).Checked) ? "Null" : ((dtpFinanceMemoDate.Value == null) ? "Null" : dtpFinanceMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkLogisticsMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkLogisticsMemo).Checked) ? "Null" : ((dtpLogisticsMemoDate.Value == null) ? "Null" : dtpLogisticsMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkProductionMemo).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkProductionMemo).Checked) ? "Null" : ((dtpProductionMemoDate.Value == null) ? "Null" : dtpProductionMemoDate.DateTime.ToString(GlobalVariables.DateLongFormate)), ((UltraToggleEditorBase)chkPartialShipment).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[j].Cells["OperationDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[j].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("EXP_OperationsDetails", "OperationID", num.ToString(), "OperationDetailID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDocumentsRequired).Rows).Count; k++)
			{
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["OperationDocumentRequiredID"].Value = "-1";
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["Deleted"].Value = false;
				((UltraGridBase)ULGDocumentsRequired).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			OperationsDocumentsRequired.DeleteByOperationID(num.ToString(), GlobalVariables.UserID);
			dtOperationDocumentsRequired.AcceptChanges();
			X = dtOperationDocumentsRequired.Clone();
			DataRow[] array = dtOperationDocumentsRequired.Select("Selected = 1 ");
			if (array.Length != 0)
			{
				DataRow[] array2 = array;
				foreach (DataRow row in array2)
				{
					X.ImportRow(row);
				}
				OperationsDocumentsRequired.Insert_UpdateByTable(X, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void DeleteData()
	{
		base.DeleteData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			OperationsDocumentsRequired.DeleteVirtualByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			OperationsDetails.DeleteVirtualByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			BusinessLayer.Export.Operations.DeleteVirtual(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
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
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtPackingTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackingTypes.ValueListItems.Clear();
		for (int n = 0; n < dtPackingTypes.Rows.Count; n++)
		{
			vlPackingTypes.ValueListItems.Add(dtPackingTypes.Rows[n]["PackingTypeID"], dtPackingTypes.Rows[n]["PackingTypeName"].ToString());
		}
		dtShippers = Shippers.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboShippers, dtShippers, "ShipperID", "ShipperName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBroker, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboBuyer, dtClients, "SubAccountID", "SubAccountName");
		dtClientsBanks = SubAccountsClientSupplierBanks.FillCombo(IsFromServer: false);
		if (cboBuyer.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClientsBanks);
			dataView.RowFilter = "SubAccountID = " + ((TextEditorControlBase)cboBuyer).Value.ToString();
			GlobalFunctions.FillCombo(cboBuyerBank, dataView.ToTable(), "ClientSupplierBankID", "BankName");
		}
		dtShippersBanks = ShippersBanks.FillCombo(IsFromServer: false);
		if (cboShippers.SelectedIndex > -1)
		{
			DataView dataView2 = new DataView(dtShippersBanks);
			dataView2.RowFilter = "ShipperID = " + ((TextEditorControlBase)cboShippers).Value.ToString();
			GlobalFunctions.FillCombo(cboShipperBank, dataView2.ToTable(), "ShipperBankID", "BankName");
		}
		dtPaymentsMethods = BusinessLayer.Export.PaymentMethods.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentsMethods, "PaymentMethodID", "PaymentMethodName");
		dtDeliveryTerms = DeliveryTerms.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDeliveryTerms, dtDeliveryTerms, "DeliveryTermID", "DeliveryTermName");
		dtContainersSizes = ContainersSizes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersSizes, dtContainersSizes, "ContainerSizeID", "ContainerSizeName");
		dtContainersTypes = BusinessLayer.Export.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersTypes, dtContainersTypes, "ContainerTypeID", "ContainerTypeName");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView3 = new DataView(dtSeaPorts);
		dataView3.RowFilter = "IsLoadingPort = 1";
		cboLoadingSeaPort.DataSource = dataView3;
		cboLoadingSeaPort.DisplayMember = "SeaPortName";
		cboLoadingSeaPort.ValueMember = "SeaPortID";
		DataView dataView4 = new DataView(dtSeaPorts);
		dataView4.RowFilter = "IsLoadingPort = 0";
		cboDischargeSeaPort.DataSource = dataView4;
		cboDischargeSeaPort.DisplayMember = "SeaPortName";
		cboDischargeSeaPort.ValueMember = "SeaPortID";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("Export", "Reports", "frmExpOperationsRep"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGReports).DataSource = dtReports;
		FillCurrencyDropDown();
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ExpOperationsReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationID"].ToString();
			FillData();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = BusinessLayer.Export.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
		if (Adding && cboBuyer.SelectedIndex > -1)
		{
			((Control)(object)txtContractNo).Text = BusinessLayer.Export.Operations.GetContractNo(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboBuyer).Value.ToString(), GlobalVariables.CurrentBranchID);
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Expected O, but got Unknown
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
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
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "PackingTypeID" && ULGData.ActiveCell.Value != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value = decimal.Parse(dtPackingTypes.Select("PackingTypeID = " + ULGData.ActiveCell.Value.ToString())[0]["WeightKG"].ToString());
			if (((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value != null && ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != null)
			{
				if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value.ToString()) != 0m)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["BagsCount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) * 1000m / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value.ToString());
				}
				else
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["BagsCount"].Value = 0;
				}
			}
			CalcTotalGrossWeight();
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		CalculateCustomsTotals();
		CalculateTotals();
		CalcTotalQty();
		CalcTotalGrossWeight();
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
				frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
				frmQuantityMultiUnit2.ShowDialog();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if ((Adding || Updating) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FreightUnit" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PackingWeight" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FreightUnit" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsTotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredUnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["DeclaredTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeclaredUnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeclaredTotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["DeclaredUnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeclaredTotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsUnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["CustomsTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CustomsUnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsTotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["CustomsUnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CustomsTotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PackingWeight" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty") && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["BagsCount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) * 1000m / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value.ToString());
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty") && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BagsCount"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["PackingWeight"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) * 1000m / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BagsCount"].Value.ToString());
			}
			CalculateTotals();
			CalculateCustomsTotals();
			CalcTotalQty();
			CalcTotalGrossWeight();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
		CalculateCustomsTotals();
		CalcTotalQty();
		CalcTotalGrossWeight();
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
			num2 += Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["DeclaredTotalPrice"].Value);
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDeclaredTotalPrice).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged -= txtAdvancePaymentAmount_ValueChanged;
		((TextEditorControlBase)txtCADAmount).ValueChanged -= txtCADAmount_ValueChanged;
		((TextEditorControlBase)txtLCAmount).ValueChanged -= txtLCAmount_ValueChanged;
		((TextEditorControlBase)txtTTAmount).ValueChanged -= txtTTAmount_ValueChanged;
		if (num > 0m)
		{
			((Control)(object)txtAdvancePaymentAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtAdvancePaymentPercentage).Text == "" || ((Control)(object)txtAdvancePaymentPercentage).Text == ".") ? "0" : ((Control)(object)txtAdvancePaymentPercentage).Text) / 100m * decimal.Parse(num.ToString()), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLCAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtLCPercentage).Text == "" || ((Control)(object)txtLCPercentage).Text == ".") ? "0" : ((Control)(object)txtLCPercentage).Text) / 100m * decimal.Parse(num.ToString()), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtCADAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtCADPercentage).Text == "" || ((Control)(object)txtCADPercentage).Text == ".") ? "0" : ((Control)(object)txtCADPercentage).Text) / 100m * decimal.Parse(num.ToString()), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTTAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtTTPercentage).Text == "" || ((Control)(object)txtTTPercentage).Text == ".") ? "0" : ((Control)(object)txtTTPercentage).Text) / 100m * decimal.Parse(num.ToString()), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtAdvancePaymentAmount).Text = "0";
			((Control)(object)txtCADAmount).Text = "0";
			((Control)(object)txtLCAmount).Text = "0";
			((Control)(object)txtTTAmount).Text = "0";
		}
		((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged += txtAdvancePaymentAmount_ValueChanged;
		((TextEditorControlBase)txtCADAmount).ValueChanged += txtCADAmount_ValueChanged;
		((TextEditorControlBase)txtLCAmount).ValueChanged += txtLCAmount_ValueChanged;
		((TextEditorControlBase)txtTTAmount).ValueChanged += txtTTAmount_ValueChanged;
	}

	private void CalculateCustomsTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CustomsTotalPrice"].Value.ToString());
		}
		((Control)(object)txtCustomsTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalNetWeight).Text = "0";
		((UltraGridBase)ULGData).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
		dataView.RowFilter = " UnitID is not null";
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0 && dataTable.Select(" UnitID<> " + dataTable.Rows[0]["UnitID"].ToString()).Length == 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalNetWeight).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	public void CalcTotalGrossWeight()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"].Value != null && ((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"].Value != DBNull.Value)
			{
				num += decimal.Parse(dtPackingTypes.Select("PackingTypeID = " + ((UltraGridBase)ULGData).Rows[i].Cells["PackingTypeID"].Value.ToString())[0]["CoverWeightKG"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value.ToString());
			}
		}
		((Control)(object)txtTotalGrossWeight).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTotalNetWeight).Text == "") ? "0" : ((Control)(object)txtTotalNetWeight).Text) + num / 1000m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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

	private void btnPOLSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(1, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLoadingSeaPort).Value = num;
		}
	}

	private void btnPODSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboDischargeSeaPort).Value = num;
		}
	}

	private void btnConsignee_Click(object sender, EventArgs e)
	{
		frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "المستلم" : "Consignee", _IsInt: false, _IsNumeric: false, txtConsignee.Text);
		frmEnterValue2.WindowState = FormWindowState.Normal;
		if (frmEnterValue2.ShowDialog() == DialogResult.OK)
		{
			txtConsignee.Text = frmEnterValue2.Value;
		}
	}

	private void btnNotify_Click(object sender, EventArgs e)
	{
		frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "Notify" : "Notify", _IsInt: false, _IsNumeric: false, txtNotify.Text);
		frmEnterValue2.WindowState = FormWindowState.Normal;
		if (frmEnterValue2.ShowDialog() == DialogResult.OK)
		{
			txtNotify.Text = frmEnterValue2.Value;
		}
	}

	private void cboShippers_ValueChanged(object sender, EventArgs e)
	{
		cboShipperBank.SelectedIndex = -1;
		((Control)(object)cboShipperBank).Text = "";
		if (cboShippers.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtShippersBanks);
			dataView.RowFilter = "ShipperID = " + ((TextEditorControlBase)cboShippers).Value.ToString();
			GlobalFunctions.FillCombo(cboShipperBank, dataView.ToTable(), "ShipperBankID", "BankName");
		}
	}

	private void frmExpOperations_Load(object sender, EventArgs e)
	{
	}

	private void richTextBox2_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtContainersCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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
		if (OperationsDeclarations.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم عمل طلب شحن عليها", "Cannot Delete This Transaction Because there Are Operations Declarations Made on It ");
			return;
		}
		if (OperationsDeclarationsInvoices.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فواتير عليها", "Cannot Delete This Transaction Because There Are Invoices For It");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
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
			if (OperationsDeclarationsInvoices.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فواتير عليها", "Cannot Update This Transaction Because There Are Invoices For It");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_EXP_Operations_E.rpt" : "Rep_EXP_Operations_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void ULGReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster != null && e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGReports).ActiveRow != null && !CanOpenLetter)
		{
			CanOpenLetter = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenLetter = false;
		}
	}

	private void ULGReports_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGReports).ActiveRow).Selected = true;
	}

	public void InitGridReports()
	{
		GlobalFunctions.PrepareGrid(ULGReports);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.1);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).Rows).Count; i++)
		{
			((UltraGridBase)ULGReports).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridDocuments()
	{
		GlobalFunctions.PrepareGrid(ULGDocumentsRequired);
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["DocumentRequiredName"].Width = (int)((double)((Control)(object)ULGDocumentsRequired).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["DocumentRequiredName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الوثيقة" : "Document Name");
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["DocumentRequiredName"].Hidden = false;
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGDocumentsRequired).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = (GlobalVariables.IsArabic ? "تم الاختيار" : "Selected");
		((UltraGridBase)ULGDocumentsRequired).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
	}

	private void ULGDocumentsRequired_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (((KeyedSubObjectBase)ULGDocumentsRequired.ActiveCell.Column).Key != "Selected")
			{
				((GridItemBase)((UltraGridBase)ULGDocumentsRequired).ActiveRow).Selected = true;
			}
		}
		else
		{
			((GridItemBase)((UltraGridBase)ULGDocumentsRequired).ActiveRow).Selected = true;
		}
	}

	private void txtAdvancePaymentAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged -= txtAdvancePaymentPercentage_ValueChanged;
				((Control)(object)txtAdvancePaymentPercentage).Text = decimal.Parse((decimal.Parse((((Control)(object)txtAdvancePaymentAmount).Text == "" || ((Control)(object)txtAdvancePaymentAmount).Text == "0" || ((Control)(object)txtAdvancePaymentAmount).Text == ".") ? "0" : ((Control)(object)txtAdvancePaymentAmount).Text) / decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged += txtAdvancePaymentPercentage_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged -= txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged -= txtAdvancePaymentPercentage_ValueChanged;
			((Control)(object)txtAdvancePaymentPercentage).Text = "0";
			((Control)(object)txtAdvancePaymentAmount).Text = "0";
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged += txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged += txtAdvancePaymentPercentage_ValueChanged;
		}
	}

	private void txtAdvancePaymentPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged -= txtAdvancePaymentAmount_ValueChanged;
				((Control)(object)txtAdvancePaymentAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtAdvancePaymentPercentage).Text == "" || ((Control)(object)txtAdvancePaymentPercentage).Text == ".") ? "0" : ((Control)(object)txtAdvancePaymentPercentage).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged += txtAdvancePaymentAmount_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged -= txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged -= txtAdvancePaymentPercentage_ValueChanged;
			((Control)(object)txtAdvancePaymentAmount).Text = "0";
			((Control)(object)txtAdvancePaymentPercentage).Text = "0";
			((TextEditorControlBase)txtAdvancePaymentAmount).ValueChanged += txtAdvancePaymentAmount_ValueChanged;
			((TextEditorControlBase)txtAdvancePaymentPercentage).ValueChanged += txtAdvancePaymentPercentage_ValueChanged;
		}
	}

	private void txtTTAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtTTPercentage).ValueChanged -= txtTTPercentage_ValueChanged;
				((Control)(object)txtTTPercentage).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTTAmount).Text == "" || ((Control)(object)txtTTAmount).Text == "0" || ((Control)(object)txtTTAmount).Text == ".") ? "0" : ((Control)(object)txtTTAmount).Text) / decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtTTPercentage).ValueChanged += txtTTPercentage_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtTTAmount).ValueChanged -= txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged -= txtTTPercentage_ValueChanged;
			((Control)(object)txtTTPercentage).Text = "0";
			((Control)(object)txtTTAmount).Text = "0";
			((TextEditorControlBase)txtTTAmount).ValueChanged += txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged += txtTTPercentage_ValueChanged;
		}
	}

	private void txtTTPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtTTAmount).ValueChanged -= txtTTAmount_ValueChanged;
				((Control)(object)txtTTAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtTTPercentage).Text == "" || ((Control)(object)txtTTPercentage).Text == ".") ? "0" : ((Control)(object)txtTTPercentage).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtTTAmount).ValueChanged += txtTTAmount_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtTTAmount).ValueChanged -= txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged -= txtTTPercentage_ValueChanged;
			((Control)(object)txtTTPercentage).Text = "0";
			((Control)(object)txtTTAmount).Text = "0";
			((TextEditorControlBase)txtTTAmount).ValueChanged += txtTTAmount_ValueChanged;
			((TextEditorControlBase)txtTTPercentage).ValueChanged += txtTTPercentage_ValueChanged;
		}
	}

	private void txtLCAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtLCPercentage).ValueChanged -= txtLCPercentage_ValueChanged;
				((Control)(object)txtLCPercentage).Text = decimal.Parse((decimal.Parse((((Control)(object)txtLCAmount).Text == "" || ((Control)(object)txtLCAmount).Text == "0" || ((Control)(object)txtLCAmount).Text == ".") ? "0" : ((Control)(object)txtLCAmount).Text) / decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtLCPercentage).ValueChanged += txtLCPercentage_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtLCAmount).ValueChanged -= txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged -= txtLCPercentage_ValueChanged;
			((Control)(object)txtLCPercentage).Text = "0";
			((Control)(object)txtLCAmount).Text = "0";
			((TextEditorControlBase)txtLCAmount).ValueChanged += txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged += txtLCPercentage_ValueChanged;
		}
	}

	private void txtLCPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtLCAmount).ValueChanged -= txtLCAmount_ValueChanged;
				((Control)(object)txtLCAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtLCPercentage).Text == "" || ((Control)(object)txtLCPercentage).Text == ".") ? "0" : ((Control)(object)txtLCPercentage).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtLCAmount).ValueChanged += txtLCAmount_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtLCAmount).ValueChanged -= txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged -= txtLCPercentage_ValueChanged;
			((Control)(object)txtLCPercentage).Text = "0";
			((Control)(object)txtLCAmount).Text = "0";
			((TextEditorControlBase)txtLCAmount).ValueChanged += txtLCAmount_ValueChanged;
			((TextEditorControlBase)txtLCPercentage).ValueChanged += txtLCPercentage_ValueChanged;
		}
	}

	private void txtCADAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtCADPercentage).ValueChanged -= txtCADPercentage_ValueChanged;
				((Control)(object)txtCADPercentage).Text = decimal.Parse((decimal.Parse((((Control)(object)txtCADAmount).Text == "" || ((Control)(object)txtCADAmount).Text == "0" || ((Control)(object)txtCADAmount).Text == ".") ? "0" : ((Control)(object)txtCADAmount).Text) / decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtCADPercentage).ValueChanged += txtCADPercentage_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtCADAmount).ValueChanged -= txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged -= txtCADPercentage_ValueChanged;
			((Control)(object)txtCADPercentage).Text = "0";
			((Control)(object)txtCADAmount).Text = "0";
			((TextEditorControlBase)txtCADAmount).ValueChanged += txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged += txtCADPercentage_ValueChanged;
		}
	}

	private void txtCADPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
			{
				((TextEditorControlBase)txtCADAmount).ValueChanged -= txtCADAmount_ValueChanged;
				((Control)(object)txtCADAmount).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtCADPercentage).Text == "" || ((Control)(object)txtCADPercentage).Text == ".") ? "0" : ((Control)(object)txtCADPercentage).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == "0" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtCADAmount).ValueChanged += txtCADAmount_ValueChanged;
				return;
			}
			((TextEditorControlBase)txtCADAmount).ValueChanged -= txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged -= txtCADPercentage_ValueChanged;
			((Control)(object)txtCADPercentage).Text = "0";
			((Control)(object)txtCADAmount).Text = "0";
			((TextEditorControlBase)txtCADAmount).ValueChanged += txtCADAmount_ValueChanged;
			((TextEditorControlBase)txtCADPercentage).ValueChanged += txtCADPercentage_ValueChanged;
		}
	}

	private void cboBuyer_ValueChanged(object sender, EventArgs e)
	{
		cboBuyerBank.SelectedIndex = -1;
		((Control)(object)cboBuyerBank).Text = "";
		if (cboBuyer.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClientsBanks);
			dataView.RowFilter = "SubAccountID = " + ((TextEditorControlBase)cboBuyer).Value.ToString();
			GlobalFunctions.FillCombo(cboBuyerBank, dataView.ToTable(), "ClientSupplierBankID", "BankName");
			if (Adding && cboBuyer.SelectedIndex > -1)
			{
				((Control)(object)txtContractNo).Text = BusinessLayer.Export.Operations.GetContractNo(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboBuyer).Value.ToString(), GlobalVariables.CurrentBranchID);
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
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Expected O, but got Unknown
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Expected O, but got Unknown
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Expected O, but got Unknown
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Expected O, but got Unknown
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Expected O, but got Unknown
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Expected O, but got Unknown
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Expected O, but got Unknown
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Expected O, but got Unknown
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Expected O, but got Unknown
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Expected O, but got Unknown
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Expected O, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Expected O, but got Unknown
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Expected O, but got Unknown
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Expected O, but got Unknown
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Expected O, but got Unknown
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Expected O, but got Unknown
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Expected O, but got Unknown
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Expected O, but got Unknown
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Expected O, but got Unknown
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Expected O, but got Unknown
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Expected O, but got Unknown
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Expected O, but got Unknown
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Expected O, but got Unknown
		//IL_0b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Expected O, but got Unknown
		//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Expected O, but got Unknown
		//IL_12f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Export.Transactions.frmExpOperations));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
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
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDocumentsRequired = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGReports = new UltraGrid();
		this.lblContainersType = new UltraLabel();
		this.cboContainersTypes = new UltraComboEditor();
		this.txtTotalNetWeight = new UltraTextEditor();
		this.lblTotalNetWeight = new UltraLabel();
		this.lblTotalPrice = new UltraLabel();
		this.lblContainersCount = new UltraLabel();
		this.txtContainersCount = new UltraTextEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.lblShipper = new UltraLabel();
		this.cboShippers = new UltraComboEditor();
		this.lblConsignee = new UltraLabel();
		this.lblPaidPercentage = new UltraLabel();
		this.txtPaidPercentage = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.lblExpectedDeliveryDate = new UltraLabel();
		this.dtpExpectedDeliveryDate = new UltraDateTimeEditor();
		this.lblPaymentMethod = new UltraLabel();
		this.cboPaymentMethod = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.lblNotify = new UltraLabel();
		this.cboBuyer = new UltraComboEditor();
		this.lblBuyer = new UltraLabel();
		this.btnPODSearch = new UltraButton();
		this.btnPOLSearch = new UltraButton();
		this.lblLoadingSeaPort = new UltraLabel();
		this.cboLoadingSeaPort = new UltraComboEditor();
		this.cboDischargeSeaPort = new UltraComboEditor();
		this.lblDischargeSeaPort = new UltraLabel();
		this.cboContainersSizes = new UltraComboEditor();
		this.lblContainersSize = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.lblCustomsTotalPrice = new UltraLabel();
		this.txtCustomsTotalPrice = new UltraTextEditor();
		this.txtTotalGrossWeight = new UltraTextEditor();
		this.lblTotalGrossWeight = new UltraLabel();
		this.txtSticker = new UltraTextEditor();
		this.lblSticker = new UltraLabel();
		this.txtStickerNotes = new UltraTextEditor();
		this.lblStickerNotes = new UltraLabel();
		this.cboDeliveryTerms = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblAdvancePaymentPercentage = new UltraLabel();
		this.txtAdvancePaymentPercentage = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.lblAdvancePaymentAmount = new UltraLabel();
		this.txtAdvancePaymentAmount = new UltraTextEditor();
		this.lblTTPercentage = new UltraLabel();
		this.txtTTPercentage = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		this.lblTTAmount = new UltraLabel();
		this.txtTTAmount = new UltraTextEditor();
		this.lblLCPercentage = new UltraLabel();
		this.txtLCPercentage = new UltraTextEditor();
		this.ultraLabel10 = new UltraLabel();
		this.lblLCAmount = new UltraLabel();
		this.txtLCAmount = new UltraTextEditor();
		this.lblCADPercentage = new UltraLabel();
		this.txtCADPercentage = new UltraTextEditor();
		this.ultraLabel13 = new UltraLabel();
		this.lblCADAmount = new UltraLabel();
		this.txtCADAmount = new UltraTextEditor();
		this.txtDeliverySchedule = new UltraTextEditor();
		this.lblDeliverySchedule = new UltraLabel();
		this.txtContractNo = new UltraTextEditor();
		this.lblContractNo = new UltraLabel();
		this.cboBuyerBank = new UltraComboEditor();
		this.lblBuyerBank = new UltraLabel();
		this.cboShipperBank = new UltraComboEditor();
		this.lblShipperBank = new UltraLabel();
		this.txtConsignee = new System.Windows.Forms.RichTextBox();
		this.txtNotify = new System.Windows.Forms.RichTextBox();
		this.cboBroker = new UltraComboEditor();
		this.lblBroker = new UltraLabel();
		this.txtLogisticsNotes = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.txtFinanceNotes = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.txtDeclaredTotalPrice = new UltraTextEditor();
		this.chkFinanceMemo = new UltraCheckEditor();
		this.chkLogisticsMemo = new UltraCheckEditor();
		this.chkProductionMemo = new UltraCheckEditor();
		this.dtpFinanceMemoDate = new UltraDateTimeEditor();
		this.dtpLogisticsMemoDate = new UltraDateTimeEditor();
		this.dtpProductionMemoDate = new UltraDateTimeEditor();
		this.ultraLabel8 = new UltraLabel();
		this.txtBrokercommissionValue = new UltraTextEditor();
		this.btnConsignee = new UltraButton();
		this.btnNotify = new UltraButton();
		this.chkPartialShipment = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDocumentsRequired).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainersCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShippers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuyer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDischargeSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSticker).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStickerNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryTerms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvancePaymentPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvancePaymentAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTTPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTTAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLCPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLCAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCADPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCADAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliverySchedule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContractNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuyerBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShipperBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBroker).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLogisticsNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinanceNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeclaredTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkFinanceMemo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkLogisticsMemo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkProductionMemo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFinanceMemoDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpLogisticsMemoDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionMemoDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrokercommissionValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPartialShipment).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "RequiredDocuments";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab2");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Reports";
		val2.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val2, "ultraTab1");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDocumentsRequired);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDocumentsRequired).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDocumentsRequired, "ULGDocumentsRequired");
		((System.Windows.Forms.Control)(object)this.ULGDocumentsRequired).Name = "ULGDocumentsRequired";
		((UltraControlBase)this.ULGDocumentsRequired).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDocumentsRequired.AfterEnterEditMode += new System.EventHandler(ULGDocumentsRequired_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGReports);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((UltraGridBase)this.ULGReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGReports, "ULGReports");
		((System.Windows.Forms.Control)(object)this.ULGReports).Name = "ULGReports";
		((UltraControlBase)this.ULGReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGReports.AfterEnterEditMode += new System.EventHandler(ULGReports_AfterEnterEditMode);
		this.ULGReports.ClickCellButton += new CellEventHandler(ULGReports_ClickCellButton);
		this.lblContainersType.AutoEllipsis = false;
		resources.ApplyResources(this.lblContainersType, "lblContainersType");
		((System.Windows.Forms.Control)(object)this.lblContainersType).Name = "lblContainersType";
		((ControlBase)this.lblContainersType).WrapText = false;
		this.cboContainersTypes.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboContainersTypes, "cboContainersTypes");
		((System.Windows.Forms.Control)(object)this.cboContainersTypes).Name = "cboContainersTypes";
		resources.ApplyResources(this.txtTotalNetWeight, "txtTotalNetWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalNetWeight).Name = "txtTotalNetWeight";
		((EditorButtonControlBase)this.txtTotalNetWeight).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalNetWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblTotalNetWeight, "lblTotalNetWeight");
		this.lblTotalNetWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalNetWeight).Name = "lblTotalNetWeight";
		((ControlBase)this.lblTotalNetWeight).WrapText = false;
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		this.lblContainersCount.AutoEllipsis = false;
		resources.ApplyResources(this.lblContainersCount, "lblContainersCount");
		((System.Windows.Forms.Control)(object)this.lblContainersCount).Name = "lblContainersCount";
		((ControlBase)this.lblContainersCount).WrapText = false;
		resources.ApplyResources(this.txtContainersCount, "txtContainersCount");
		((System.Windows.Forms.Control)(object)this.txtContainersCount).Name = "txtContainersCount";
		((System.Windows.Forms.Control)(object)this.txtContainersCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtContainersCount_KeyPress);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		this.lblShipper.AutoEllipsis = false;
		resources.ApplyResources(this.lblShipper, "lblShipper");
		((System.Windows.Forms.Control)(object)this.lblShipper).Name = "lblShipper";
		((ControlBase)this.lblShipper).WrapText = false;
		this.cboShippers.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboShippers, "cboShippers");
		((System.Windows.Forms.Control)(object)this.cboShippers).Name = "cboShippers";
		((TextEditorControlBase)this.cboShippers).ValueChanged += new System.EventHandler(cboShippers_ValueChanged);
		this.lblConsignee.AutoEllipsis = false;
		resources.ApplyResources(this.lblConsignee, "lblConsignee");
		((System.Windows.Forms.Control)(object)this.lblConsignee).Name = "lblConsignee";
		((ControlBase)this.lblConsignee).WrapText = false;
		this.lblPaidPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblPaidPercentage, "lblPaidPercentage");
		((System.Windows.Forms.Control)(object)this.lblPaidPercentage).Name = "lblPaidPercentage";
		((ControlBase)this.lblPaidPercentage).WrapText = false;
		resources.ApplyResources(this.txtPaidPercentage, "txtPaidPercentage");
		((System.Windows.Forms.Control)(object)this.txtPaidPercentage).Name = "txtPaidPercentage";
		((System.Windows.Forms.Control)(object)this.txtPaidPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.ultraLabel6.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		this.lblExpectedDeliveryDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExpectedDeliveryDate, "lblExpectedDeliveryDate");
		((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate).Name = "lblExpectedDeliveryDate";
		((ControlBase)this.lblExpectedDeliveryDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpExpectedDeliveryDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpExpectedDeliveryDate, "dtpExpectedDeliveryDate");
		this.dtpExpectedDeliveryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate).Name = "dtpExpectedDeliveryDate";
		this.lblPaymentMethod.AutoEllipsis = false;
		resources.ApplyResources(this.lblPaymentMethod, "lblPaymentMethod");
		((System.Windows.Forms.Control)(object)this.lblPaymentMethod).Name = "lblPaymentMethod";
		((ControlBase)this.lblPaymentMethod).WrapText = false;
		this.cboPaymentMethod.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPaymentMethod, "cboPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).Name = "cboPaymentMethod";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
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
		this.lblNotify.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotify, "lblNotify");
		((System.Windows.Forms.Control)(object)this.lblNotify).Name = "lblNotify";
		((ControlBase)this.lblNotify).WrapText = false;
		this.cboBuyer.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBuyer, "cboBuyer");
		((System.Windows.Forms.Control)(object)this.cboBuyer).Name = "cboBuyer";
		((TextEditorControlBase)this.cboBuyer).ValueChanged += new System.EventHandler(cboBuyer_ValueChanged);
		this.lblBuyer.AutoEllipsis = false;
		resources.ApplyResources(this.lblBuyer, "lblBuyer");
		((System.Windows.Forms.Control)(object)this.lblBuyer).Name = "lblBuyer";
		((ControlBase)this.lblBuyer).WrapText = false;
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPODSearch).Appearance = (AppearanceBase)(object)val21;
		resources.ApplyResources(this.btnPODSearch, "btnPODSearch");
		((System.Windows.Forms.Control)(object)this.btnPODSearch).Name = "btnPODSearch";
		((System.Windows.Forms.Control)(object)this.btnPODSearch).Click += new System.EventHandler(btnPODSearch_Click);
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPOLSearch).Appearance = (AppearanceBase)(object)val22;
		resources.ApplyResources(this.btnPOLSearch, "btnPOLSearch");
		((System.Windows.Forms.Control)(object)this.btnPOLSearch).Name = "btnPOLSearch";
		((System.Windows.Forms.Control)(object)this.btnPOLSearch).Click += new System.EventHandler(btnPOLSearch_Click);
		this.lblLoadingSeaPort.AutoEllipsis = false;
		resources.ApplyResources(this.lblLoadingSeaPort, "lblLoadingSeaPort");
		((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort).Name = "lblLoadingSeaPort";
		((ControlBase)this.lblLoadingSeaPort).WrapText = false;
		this.cboLoadingSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboLoadingSeaPort, "cboLoadingSeaPort");
		((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort).Name = "cboLoadingSeaPort";
		this.cboDischargeSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboDischargeSeaPort, "cboDischargeSeaPort");
		((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort).Name = "cboDischargeSeaPort";
		this.lblDischargeSeaPort.AutoEllipsis = false;
		resources.ApplyResources(this.lblDischargeSeaPort, "lblDischargeSeaPort");
		((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort).Name = "lblDischargeSeaPort";
		((ControlBase)this.lblDischargeSeaPort).WrapText = false;
		this.cboContainersSizes.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboContainersSizes, "cboContainersSizes");
		((System.Windows.Forms.Control)(object)this.cboContainersSizes).Name = "cboContainersSizes";
		this.lblContainersSize.AutoEllipsis = false;
		resources.ApplyResources(this.lblContainersSize, "lblContainersSize");
		((System.Windows.Forms.Control)(object)this.lblContainersSize).Name = "lblContainersSize";
		((ControlBase)this.lblContainersSize).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblCustomsTotalPrice, "lblCustomsTotalPrice");
		this.lblCustomsTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice).Name = "lblCustomsTotalPrice";
		((ControlBase)this.lblCustomsTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtCustomsTotalPrice, "txtCustomsTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice).Name = "txtCustomsTotalPrice";
		((EditorButtonControlBase)this.txtCustomsTotalPrice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtTotalGrossWeight, "txtTotalGrossWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight).Name = "txtTotalGrossWeight";
		((EditorButtonControlBase)this.txtTotalGrossWeight).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblTotalGrossWeight, "lblTotalGrossWeight");
		this.lblTotalGrossWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight).Name = "lblTotalGrossWeight";
		((ControlBase)this.lblTotalGrossWeight).WrapText = false;
		resources.ApplyResources(this.txtSticker, "txtSticker");
		((System.Windows.Forms.Control)(object)this.txtSticker).Name = "txtSticker";
		this.lblSticker.AutoEllipsis = false;
		resources.ApplyResources(this.lblSticker, "lblSticker");
		((System.Windows.Forms.Control)(object)this.lblSticker).Name = "lblSticker";
		((ControlBase)this.lblSticker).WrapText = false;
		resources.ApplyResources(this.txtStickerNotes, "txtStickerNotes");
		((System.Windows.Forms.Control)(object)this.txtStickerNotes).Name = "txtStickerNotes";
		this.lblStickerNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblStickerNotes, "lblStickerNotes");
		((System.Windows.Forms.Control)(object)this.lblStickerNotes).Name = "lblStickerNotes";
		((ControlBase)this.lblStickerNotes).WrapText = false;
		this.cboDeliveryTerms.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboDeliveryTerms, "cboDeliveryTerms");
		((System.Windows.Forms.Control)(object)this.cboDeliveryTerms).Name = "cboDeliveryTerms";
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.lblAdvancePaymentPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblAdvancePaymentPercentage, "lblAdvancePaymentPercentage");
		((System.Windows.Forms.Control)(object)this.lblAdvancePaymentPercentage).Name = "lblAdvancePaymentPercentage";
		((ControlBase)this.lblAdvancePaymentPercentage).WrapText = false;
		resources.ApplyResources(this.txtAdvancePaymentPercentage, "txtAdvancePaymentPercentage");
		((System.Windows.Forms.Control)(object)this.txtAdvancePaymentPercentage).Name = "txtAdvancePaymentPercentage";
		((TextEditorControlBase)this.txtAdvancePaymentPercentage).ValueChanged += new System.EventHandler(txtAdvancePaymentPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAdvancePaymentPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		this.lblAdvancePaymentAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblAdvancePaymentAmount, "lblAdvancePaymentAmount");
		((System.Windows.Forms.Control)(object)this.lblAdvancePaymentAmount).Name = "lblAdvancePaymentAmount";
		((ControlBase)this.lblAdvancePaymentAmount).WrapText = false;
		resources.ApplyResources(this.txtAdvancePaymentAmount, "txtAdvancePaymentAmount");
		((System.Windows.Forms.Control)(object)this.txtAdvancePaymentAmount).Name = "txtAdvancePaymentAmount";
		((TextEditorControlBase)this.txtAdvancePaymentAmount).ValueChanged += new System.EventHandler(txtAdvancePaymentAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAdvancePaymentAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.lblTTPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblTTPercentage, "lblTTPercentage");
		((System.Windows.Forms.Control)(object)this.lblTTPercentage).Name = "lblTTPercentage";
		((ControlBase)this.lblTTPercentage).WrapText = false;
		resources.ApplyResources(this.txtTTPercentage, "txtTTPercentage");
		((System.Windows.Forms.Control)(object)this.txtTTPercentage).Name = "txtTTPercentage";
		((TextEditorControlBase)this.txtTTPercentage).ValueChanged += new System.EventHandler(txtTTPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTTPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.ultraLabel7.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		this.lblTTAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblTTAmount, "lblTTAmount");
		((System.Windows.Forms.Control)(object)this.lblTTAmount).Name = "lblTTAmount";
		((ControlBase)this.lblTTAmount).WrapText = false;
		resources.ApplyResources(this.txtTTAmount, "txtTTAmount");
		((System.Windows.Forms.Control)(object)this.txtTTAmount).Name = "txtTTAmount";
		((TextEditorControlBase)this.txtTTAmount).ValueChanged += new System.EventHandler(txtTTAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTTAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.lblLCPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblLCPercentage, "lblLCPercentage");
		((System.Windows.Forms.Control)(object)this.lblLCPercentage).Name = "lblLCPercentage";
		((ControlBase)this.lblLCPercentage).WrapText = false;
		resources.ApplyResources(this.txtLCPercentage, "txtLCPercentage");
		((System.Windows.Forms.Control)(object)this.txtLCPercentage).Name = "txtLCPercentage";
		((TextEditorControlBase)this.txtLCPercentage).ValueChanged += new System.EventHandler(txtLCPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLCPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.ultraLabel10.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		this.lblLCAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblLCAmount, "lblLCAmount");
		((System.Windows.Forms.Control)(object)this.lblLCAmount).Name = "lblLCAmount";
		((ControlBase)this.lblLCAmount).WrapText = false;
		resources.ApplyResources(this.txtLCAmount, "txtLCAmount");
		((System.Windows.Forms.Control)(object)this.txtLCAmount).Name = "txtLCAmount";
		((TextEditorControlBase)this.txtLCAmount).ValueChanged += new System.EventHandler(txtLCAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLCAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.lblCADPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblCADPercentage, "lblCADPercentage");
		((System.Windows.Forms.Control)(object)this.lblCADPercentage).Name = "lblCADPercentage";
		((ControlBase)this.lblCADPercentage).WrapText = false;
		resources.ApplyResources(this.txtCADPercentage, "txtCADPercentage");
		((System.Windows.Forms.Control)(object)this.txtCADPercentage).Name = "txtCADPercentage";
		((TextEditorControlBase)this.txtCADPercentage).ValueChanged += new System.EventHandler(txtCADPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCADPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.ultraLabel13.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		this.lblCADAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblCADAmount, "lblCADAmount");
		((System.Windows.Forms.Control)(object)this.lblCADAmount).Name = "lblCADAmount";
		((ControlBase)this.lblCADAmount).WrapText = false;
		resources.ApplyResources(this.txtCADAmount, "txtCADAmount");
		((System.Windows.Forms.Control)(object)this.txtCADAmount).Name = "txtCADAmount";
		((TextEditorControlBase)this.txtCADAmount).ValueChanged += new System.EventHandler(txtCADAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCADAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtDeliverySchedule, "txtDeliverySchedule");
		((System.Windows.Forms.Control)(object)this.txtDeliverySchedule).Name = "txtDeliverySchedule";
		this.lblDeliverySchedule.AutoEllipsis = false;
		resources.ApplyResources(this.lblDeliverySchedule, "lblDeliverySchedule");
		((System.Windows.Forms.Control)(object)this.lblDeliverySchedule).Name = "lblDeliverySchedule";
		((ControlBase)this.lblDeliverySchedule).WrapText = false;
		resources.ApplyResources(this.txtContractNo, "txtContractNo");
		((System.Windows.Forms.Control)(object)this.txtContractNo).Name = "txtContractNo";
		this.lblContractNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblContractNo, "lblContractNo");
		((System.Windows.Forms.Control)(object)this.lblContractNo).Name = "lblContractNo";
		((ControlBase)this.lblContractNo).WrapText = false;
		this.cboBuyerBank.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBuyerBank, "cboBuyerBank");
		((System.Windows.Forms.Control)(object)this.cboBuyerBank).Name = "cboBuyerBank";
		this.lblBuyerBank.AutoEllipsis = false;
		resources.ApplyResources(this.lblBuyerBank, "lblBuyerBank");
		((System.Windows.Forms.Control)(object)this.lblBuyerBank).Name = "lblBuyerBank";
		((ControlBase)this.lblBuyerBank).WrapText = false;
		this.cboShipperBank.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboShipperBank, "cboShipperBank");
		((System.Windows.Forms.Control)(object)this.cboShipperBank).Name = "cboShipperBank";
		this.lblShipperBank.AutoEllipsis = false;
		resources.ApplyResources(this.lblShipperBank, "lblShipperBank");
		((System.Windows.Forms.Control)(object)this.lblShipperBank).Name = "lblShipperBank";
		((ControlBase)this.lblShipperBank).WrapText = false;
		resources.ApplyResources(this.txtConsignee, "txtConsignee");
		this.txtConsignee.Name = "txtConsignee";
		resources.ApplyResources(this.txtNotify, "txtNotify");
		this.txtNotify.Name = "txtNotify";
		this.txtNotify.TextChanged += new System.EventHandler(richTextBox2_TextChanged);
		this.cboBroker.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBroker, "cboBroker");
		((System.Windows.Forms.Control)(object)this.cboBroker).Name = "cboBroker";
		this.lblBroker.AutoEllipsis = false;
		resources.ApplyResources(this.lblBroker, "lblBroker");
		((System.Windows.Forms.Control)(object)this.lblBroker).Name = "lblBroker";
		((ControlBase)this.lblBroker).WrapText = false;
		resources.ApplyResources(this.txtLogisticsNotes, "txtLogisticsNotes");
		((System.Windows.Forms.Control)(object)this.txtLogisticsNotes).Name = "txtLogisticsNotes";
		this.ultraLabel2.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtFinanceNotes, "txtFinanceNotes");
		((System.Windows.Forms.Control)(object)this.txtFinanceNotes).Name = "txtFinanceNotes";
		this.ultraLabel4.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtDeclaredTotalPrice, "txtDeclaredTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtDeclaredTotalPrice).Name = "txtDeclaredTotalPrice";
		((EditorButtonControlBase)this.txtDeclaredTotalPrice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtDeclaredTotalPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkFinanceMemo, "chkFinanceMemo");
		((System.Windows.Forms.Control)(object)this.chkFinanceMemo).Name = "chkFinanceMemo";
		resources.ApplyResources(this.chkLogisticsMemo, "chkLogisticsMemo");
		((System.Windows.Forms.Control)(object)this.chkLogisticsMemo).Name = "chkLogisticsMemo";
		resources.ApplyResources(this.chkProductionMemo, "chkProductionMemo");
		((System.Windows.Forms.Control)(object)this.chkProductionMemo).Name = "chkProductionMemo";
		((UltraWinEditorMaskedControlBase)this.dtpFinanceMemoDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpFinanceMemoDate, "dtpFinanceMemoDate");
		this.dtpFinanceMemoDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpFinanceMemoDate).Name = "dtpFinanceMemoDate";
		((UltraWinEditorMaskedControlBase)this.dtpLogisticsMemoDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpLogisticsMemoDate, "dtpLogisticsMemoDate");
		this.dtpLogisticsMemoDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpLogisticsMemoDate).Name = "dtpLogisticsMemoDate";
		((UltraWinEditorMaskedControlBase)this.dtpProductionMemoDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpProductionMemoDate, "dtpProductionMemoDate");
		this.dtpProductionMemoDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpProductionMemoDate).Name = "dtpProductionMemoDate";
		this.ultraLabel8.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.txtBrokercommissionValue, "txtBrokercommissionValue");
		((System.Windows.Forms.Control)(object)this.txtBrokercommissionValue).Name = "txtBrokercommissionValue";
		((TextEditorControlBase)this.txtBrokercommissionValue).ValueChanged += new System.EventHandler(txtAdvancePaymentPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtBrokercommissionValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		((AppearanceBase)val23).Image = ERP.Properties.Resources.Update1;
		((ControlBase)this.btnConsignee).Appearance = (AppearanceBase)(object)val23;
		resources.ApplyResources(this.btnConsignee, "btnConsignee");
		((System.Windows.Forms.Control)(object)this.btnConsignee).Name = "btnConsignee";
		((System.Windows.Forms.Control)(object)this.btnConsignee).Click += new System.EventHandler(btnConsignee_Click);
		((AppearanceBase)val24).Image = ERP.Properties.Resources.Update1;
		((ControlBase)this.btnNotify).Appearance = (AppearanceBase)(object)val24;
		resources.ApplyResources(this.btnNotify, "btnNotify");
		((System.Windows.Forms.Control)(object)this.btnNotify).Name = "btnNotify";
		((System.Windows.Forms.Control)(object)this.btnNotify).Click += new System.EventHandler(btnNotify_Click);
		resources.ApplyResources(this.chkPartialShipment, "chkPartialShipment");
		((System.Windows.Forms.Control)(object)this.chkPartialShipment).Name = "chkPartialShipment";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPartialShipment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNotify);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpProductionMemoDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpLogisticsMemoDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFinanceMemoDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkProductionMemo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkLogisticsMemo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkFinanceMemo);
		base.Controls.Add(this.txtNotify);
		base.Controls.Add(this.txtConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShipperBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuyerBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShipperBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBuyerBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDeliveryTerms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPODSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPOLSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCADAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLCAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCADAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTTAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLCAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdvancePaymentAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTTAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdvancePaymentAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBroker);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuyer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBroker);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBuyer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotify);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShipper);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShippers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStickerNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContractNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSticker);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliverySchedule);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFinanceNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLogisticsNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStickerNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContractNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSticker);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCADPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliverySchedule);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLCPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainersCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTTPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCADPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLCPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBrokercommissionValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdvancePaymentPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTTPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdvancePaymentPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeclaredTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContainersSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContainersTypes);
		base.Name = "frmExpOperations";
		base.Load += new System.EventHandler(frmExpOperations_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContainersTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContainersSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeclaredTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdvancePaymentPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTTPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdvancePaymentPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBrokercommissionValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLCPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCADPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTTPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainersCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLCPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliverySchedule, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCADPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSticker, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContractNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStickerNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLogisticsNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFinanceNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliverySchedule, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSticker, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContractNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStickerNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShippers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShipper, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotify, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBuyer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBroker, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuyer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBroker, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel13, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdvancePaymentAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTTAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdvancePaymentAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLCAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTTAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCADAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLCAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCADAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPOLSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPODSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDeliveryTerms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBuyerBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShipperBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuyerBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShipperBank, 0);
		base.Controls.SetChildIndex(this.txtConsignee, 0);
		base.Controls.SetChildIndex(this.txtNotify, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkFinanceMemo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkLogisticsMemo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkProductionMemo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFinanceMemoDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpLogisticsMemoDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpProductionMemoDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNotify, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPartialShipment, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDocumentsRequired).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainersCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShippers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuyer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDischargeSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSticker).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStickerNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryTerms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvancePaymentPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdvancePaymentAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTTPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTTAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLCPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLCAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCADPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCADAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliverySchedule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContractNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuyerBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShipperBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBroker).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLogisticsNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinanceNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeclaredTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkFinanceMemo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkLogisticsMemo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkProductionMemo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFinanceMemoDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpLogisticsMemoDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionMemoDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrokercommissionValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPartialShipment).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
