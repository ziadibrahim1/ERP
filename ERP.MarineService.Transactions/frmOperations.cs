using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.MarineService.Transactions;

public class frmOperations : frmHeaderManyDetails
{
	private DataTable dtVessels;

	private DataTable dtItemsQuotations;

	private DataTable dtCurrency;

	private DataTable dtOwners;

	private DataTable dtCharters;

	private DataTable dtCaptains;

	private DataTable dtLoadType;

	private DataTable dtSeaPorts;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtSeaMenRanks;

	private DataTable dtSubAccountTypes;

	private DataTable dtSubAccounts;

	private DataTable dtAccounts;

	private DataTable dtServices;

	private DataTable dtCompanies;

	private DataTable dtUsers;

	private DataTable dtExpenses;

	private DataTable dtTaskPlaces;

	private DataTable dtSeaMen;

	private DataTable dtInvoicesCurrency;

	private DataTable dtReports;

	private DataTable dtCostCenters;

	private DataTable dtTaxs;

	private DataTable dtOperationsItems;

	private DataTable dtOperationsSeaMen;

	private DataTable dtSeaPortReports;

	private DataTable dtServicesReports;

	private DataTable dtOperationsExpenses;

	private DataTable dtOperationsInvoices;

	private DataTable dtOperationsPSOrders;

	private DataTable dtOperationsMaterialIssueVoucher;

	private DataTable dtShipChandler;

	private DataTable dtOperationsServicesStepsTasks;

	private DataTable dtOperationsRemarks;

	private DataTable dtServicesPrices;

	private DataTable dtItemsPrices;

	private bool CanOpenSeaportLetter = false;

	private bool CanOpenPSOrders = false;

	private DataTable dtItemsQuotationsDetails;

	private bool UsingColors;

	private bool UsingSizes = false;

	private ValueList vlItems = new ValueList();

	private ValueList vlItemsUnits = new ValueList();

	private ValueList vlSeaMenRanks = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlItemsSubAccountTypes = new ValueList();

	private ValueList vlItemsSubAccounts = new ValueList();

	private ValueList vlServices = new ValueList();

	private ValueList vlServicesCompanies = new ValueList();

	private ValueList vlServicesUnits = new ValueList();

	private ValueList vlServicesSubAccountTypes = new ValueList();

	private ValueList vlServicesSubAccounts = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlSeaMen = new ValueList();

	private ValueList vlReports = new ValueList();

	private ValueList vlOperationsExpensesCompanies = new ValueList();

	private ValueList vlOperationsExpenses = new ValueList();

	private ValueList vlOperationsExpensesAccounts = new ValueList();

	private ValueList vlOperationsExpensesSubAccounts = new ValueList();

	private ValueList vlInvoicesSubAccounts = new ValueList();

	private ValueList vlInvoicesCurrency = new ValueList();

	private ValueList vlTasksPlaces = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlToUsers = new ValueList();

	private ValueList vlServiceUsers = new ValueList();

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnVesselSearch;

	private UltraLabel lblVesselName;

	private UltraComboEditor cboVesselName;

	private UltraTextEditor txtVoyageNo;

	private UltraLabel lblVoyageNo;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblServiceQuotation;

	private UltraComboEditor cboServiceQuotation;

	private UltraLabel lblItemQuotation;

	private UltraComboEditor cboItemQuotation;

	private UltraTextEditor txtPassengersCount;

	private UltraLabel lblPassengersCount;

	private UltraLabel lblOwner;

	private UltraComboEditor cboOwners;

	private UltraLabel lblCharter;

	private UltraComboEditor cboCharterers;

	private UltraLabel lblCaptain;

	private UltraComboEditor cboCaptain;

	public UltraButton btnOwnerSearch;

	public UltraButton btnCharterer;

	public UltraButton btnCaptain;

	public UltraButton btnItemQuotationSearch;

	public UltraButton btnServiceQuotationSearch;

	private UltraLabel lblEntryDate;

	private UltraDateTimeEditor dtpEntryDate;

	private UltraTextEditor txtEntryLoad;

	private UltraLabel lblEntryLoad;

	private UltraLabel lblLoadType;

	private UltraComboEditor cboLoadType;

	private UltraLabel lblEntryReason;

	private UltraLabel lblEntrySeaPort;

	private UltraComboEditor cboEntrySeaPort;

	private UltraLabel lblDepartureDate;

	private UltraDateTimeEditor dtpDepartureDate;

	private UltraTextEditor txtDepartureLoad;

	private UltraLabel lblDepartureLoad;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraTabPageControl ultraTabPageControl5;

	private UltraTabPageControl ultraTabPageControl6;

	protected internal UltraGrid ULGDataExpenses;

	protected internal UltraGrid ULGDataItems;

	protected internal UltraGrid ULGDataInvoices;

	protected internal UltraGrid ULGDataSeaPortsReports;

	private UltraLabel lblFromSeaPort;

	private UltraComboEditor cboFromSeaPort;

	private UltraLabel lblToSeaPort;

	private UltraComboEditor cboToSeaPort;

	public UltraButton btnFromSeaPortSearch;

	public UltraButton btnEntrySeaPortSearch;

	public UltraButton btnToSeaPortSearch;

	public UltraButton btnCreateInvoices;

	public UltraButton btnCreateOrders;

	public UltraButton btnCreateMIV;

	private UltraTabPageControl ultraTabPageControl7;

	private UltraLabel lblQuayNo;

	private UltraTextEditor txtQuayNo;

	private UltraTextEditor txtExtendReason;

	private UltraLabel lblExtendReason;

	private UltraTextEditor txtRemainingFuel;

	private UltraLabel lblRemainingFuel;

	private UltraLabel lblWivesChildreCount;

	private UltraTextEditor txtWivesChildrenCount;

	private UltraLabel lblSeaMenCount;

	private UltraTextEditor txtSeaMenCount;

	private UltraTabPageControl ultraTabPageControl8;

	protected internal UltraGrid ULGDataOrders;

	private UltraTabPageControl ultraTabPageControl9;

	protected internal UltraGrid ULGDataTasks;

	private UltraTabPageControl ultraTabPageControl10;

	protected internal UltraGrid ULGDataRemarks;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraLabel lblImportManifestDate;

	private UltraDateTimeEditor dtpImportManifestDate;

	private UltraLabel lblImportManifestNo;

	private UltraTextEditor txtImportManifestNo;

	private UltraLabel lblExportManifestDate;

	private UltraDateTimeEditor dtpExportManifestDate;

	private UltraLabel lblExportManifestNo;

	private UltraTextEditor txtExportManifestNo;

	public UltraButton btnPrintImportManifest;

	public UltraButton btnPrintExportManifest;

	public UltraButton btnPrintEmptyManifest;

	private UltraTabPageControl ultraTabPageControl11;

	protected internal UltraGrid ULGDataSeaMen;

	private UltraTextEditor txtEntryReason;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtDepartureDraftAft;

	private UltraTextEditor txtDepartureDraftFwd;

	private UltraLabel lblArrivalDraftFwd;

	private UltraLabel lblDepartureDraft;

	private UltraLabel lblArrivalDraftAft;

	private UltraTextEditor txtArrivalDraftAft;

	private UltraLabel lblArrivalDraft;

	private UltraTextEditor txtArrivalDraftFwd;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel5;

	private UltraLabel lblCurrentCrew;

	private UltraLabel lblCurrentPassenger;

	private UltraLabel lblCurrentPassengerCount;

	public UltraButton btnCreateExpense;

	public UltraButton btnCostCenterSearch;

	private UltraComboEditor cboCostCenter;

	private UltraLabel lblCostCenter;

	protected internal UltraGrid ULGDataMaterialIssueVoucher;

	private UltraTabPageControl ultraTabPageControl12;

	private UltraCheckEditor chkItemsSubAccount;

	private UltraComboEditor cboItemsSubAccountTypeID;

	private UltraComboEditor cboItemsSubAccountID;

	public UltraButton btnGenerateExpManifestNo;

	public UltraButton btnGenerateImportManifestNo;

	private UltraCheckEditor chkIsActualDeparture;

	private UltraDateTimeEditor dtpActualDepartureDate;

	private UltraTabPageControl ultraTabPageControl13;

	protected internal UltraGrid ULGServicesReports;

	public UltraButton btnChangeVessel;

	public UltraButton btnChangeCostCenter;

	public UltraButton btnOpenOperationsSearch;

	private UltraLabel lblItemsCounterResult;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl ultraTabPageControl14;

	public UltraButton btnCreateShipChandler;

	protected internal UltraGrid ULGShipChandler;

	public frmOperations()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
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
		InitializeComponent();
		TableName = "MS_Operations";
		IDCol = "OperationID";
		NoCol = "OperationNo";
		DateCol = "OperationDate";
	}

	public frmOperations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		PrepareTabs();
		if (int.Parse(Operations.CheckVesselStayValidityPeriod(GlobalVariables.CurrentBranchID)) > 0)
		{
			((UltraControlBase)btnOpenOperationsSearch).UseAppStyling = false;
			((ControlBase)btnOpenOperationsSearch).Appearance.ThemedElementAlpha = (Alpha)3;
			((ControlBase)btnOpenOperationsSearch).Appearance.BackColor = Color.Red;
		}
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtpEntryDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtpDepartureDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVesselName, dtVessels, "VesselID", "VesselName");
		dtItemsQuotations = ItemsQuotations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboItemQuotation, dtItemsQuotations, "ItemQuotationID", "ItemQuotationNo");
		FillCurrencyDropDown();
		dtOwners = Owners.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOwners, dtOwners, "SubAccountID", "OwnerName");
		dtCharters = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCharterers, dtCharters, "SubAccountID", "SubAccountName");
		dtCaptains = Captains.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCaptain, dtCaptains, "SubAccountID", "CaptainName");
		dtLoadType = LoadTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLoadType, dtLoadType, "LoadTypeID", "LoadTypeName");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFromSeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
		GlobalFunctions.FillCombo(cboEntrySeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
		GlobalFunctions.FillCombo(cboToSeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
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
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsUnits.ValueListItems.Clear();
		vlServicesUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlItemsUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
			vlServicesUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int m = 0; m < dtTaxs.Rows.Count; m++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[m]["TaxID"], dtTaxs.Rows[m]["TaxName"].ToString());
		}
		dtSeaMenRanks = SeaMenRanks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSeaMenRanks.ValueListItems.Clear();
		for (int n = 0; n < dtSeaMenRanks.Rows.Count; n++)
		{
			vlSeaMenRanks.ValueListItems.Add(dtSeaMenRanks.Rows[n]["SeaManRankID"], dtSeaMenRanks.Rows[n]["SeaManRankName"].ToString());
		}
		dtSubAccountTypes = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.AgentSubAccountTypeIDs + GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs + GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboItemsSubAccountTypeID, dtSubAccountTypes, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		vlItemsSubAccountTypes.ValueListItems.Clear();
		vlServicesSubAccountTypes.ValueListItems.Clear();
		for (int num = 0; num < dtSubAccountTypes.Rows.Count; num++)
		{
			vlItemsSubAccountTypes.ValueListItems.Add(dtSubAccountTypes.Rows[num]["SubAccountTypeID"], GlobalVariables.IsArabic ? dtSubAccountTypes.Rows[num]["SubAccountTypeNameAr"].ToString() : dtSubAccountTypes.Rows[num]["SubAccountTypeNameEn"].ToString());
			vlServicesSubAccountTypes.ValueListItems.Add(dtSubAccountTypes.Rows[num]["SubAccountTypeID"], GlobalVariables.IsArabic ? dtSubAccountTypes.Rows[num]["SubAccountTypeNameAr"].ToString() : dtSubAccountTypes.Rows[num]["SubAccountTypeNameEn"].ToString());
		}
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlOperationsExpensesAccounts.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtAccounts.Rows.Count; num2++)
		{
			vlOperationsExpensesAccounts.ValueListItems.Add(dtAccounts.Rows[num2]["AccountID"], dtAccounts.Rows[num2]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountIDWithoutCode("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItemsSubAccountID, dtSubAccounts, "SubAccountID", "Name");
		vlItemsSubAccounts.ValueListItems.Clear();
		vlServicesSubAccounts.ValueListItems.Clear();
		vlOperationsExpensesSubAccounts.ValueListItems.Clear();
		vlInvoicesSubAccounts.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtSubAccounts.Rows.Count; num3++)
		{
			vlItemsSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num3]["SubAccountID"], dtSubAccounts.Rows[num3]["Name"].ToString());
			vlServicesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num3]["SubAccountID"], dtSubAccounts.Rows[num3]["Name"].ToString());
			vlOperationsExpensesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num3]["SubAccountID"], dtSubAccounts.Rows[num3]["Name"].ToString());
			vlInvoicesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num3]["SubAccountID"], dtSubAccounts.Rows[num3]["Name"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtServices.Rows.Count; num4++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[num4]["ServiceID"], dtServices.Rows[num4]["ServiceName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServicesCompanies.ValueListItems.Clear();
		vlOperationsExpensesCompanies.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtCompanies.Rows.Count; num5++)
		{
			vlServicesCompanies.ValueListItems.Add(dtCompanies.Rows[num5]["CompanyID"], dtCompanies.Rows[num5]["CompanyName"].ToString());
			vlOperationsExpensesCompanies.ValueListItems.Add(dtCompanies.Rows[num5]["CompanyID"], dtCompanies.Rows[num5]["CompanyName"].ToString());
		}
		dtTaskPlaces = TasksPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTasksPlaces.ValueListItems.Clear();
		for (int num6 = 0; num6 < dtTaskPlaces.Rows.Count; num6++)
		{
			vlTasksPlaces.ValueListItems.Add(dtTaskPlaces.Rows[num6]["TaskPlaceID"], dtTaskPlaces.Rows[num6]["TaskPlaceName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		vlToUsers.ValueListItems.Clear();
		vlServiceUsers.ValueListItems.Clear();
		for (int num7 = 0; num7 < dtUsers.Rows.Count; num7++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num7]["User_ID"], dtUsers.Rows[num7]["UserName"].ToString());
			vlToUsers.ValueListItems.Add(dtUsers.Rows[num7]["User_ID"], dtUsers.Rows[num7]["UserName"].ToString());
			vlServiceUsers.ValueListItems.Add(dtUsers.Rows[num7]["User_ID"], dtUsers.Rows[num7]["UserName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlOperationsExpenses.ValueListItems.Clear();
		for (int num8 = 0; num8 < dtExpenses.Rows.Count; num8++)
		{
			vlOperationsExpenses.ValueListItems.Add(dtExpenses.Rows[num8]["ExpenseID"], dtExpenses.Rows[num8]["ExpenseName"].ToString());
		}
		dtSeaMen = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSeaMen.ValueListItems.Clear();
		for (int num9 = 0; num9 < dtSeaMen.Rows.Count; num9++)
		{
			vlSeaMen.ValueListItems.Add(dtSeaMen.Rows[num9]["SubAccountID"], dtSeaMen.Rows[num9]["SubAccountName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("MarineService", "Reports", "frmPortReports"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlReports.ValueListItems.Clear();
		for (int num10 = 0; num10 < dtReports.Rows.Count; num10++)
		{
			vlReports.ValueListItems.Add(dtReports.Rows[num10]["ReportID"], dtReports.Rows[num10]["ReportName"].ToString());
		}
		dtInvoicesCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoicesCurrency.ValueListItems.Clear();
		for (int num11 = 0; num11 < dtInvoicesCurrency.Rows.Count; num11++)
		{
			vlInvoicesCurrency.ValueListItems.Add(dtInvoicesCurrency.Rows[num11]["CurrencyID"], dtInvoicesCurrency.Rows[num11]["CurrencyName"].ToString());
		}
		dtServicesReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("MarineService", "Reports", "frmOperationServicesRep"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGServicesReports).DataSource = dtServicesReports;
		InitGridServicesReports();
		dtDetails = OperationsServices.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		dtOperationsItems = OperationsItems.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataItems).DataSource = dtOperationsItems;
		InitGridItems();
		dtSeaPortReports = SeaPortsReports.SelectBySeaPortID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGDataSeaPortsReports).DataSource = dtSeaPortReports;
		InitGridSeaPortsReports();
		dtOperationsExpenses = OperationsExpenses.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataExpenses).DataSource = dtOperationsExpenses;
		InitGridExpenses();
		dtOperationsInvoices = OperationsInvoices.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataInvoices).DataSource = dtOperationsInvoices;
		InitGridInvoices();
		dtOperationsPSOrders = OperationsPSOrders.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataOrders).DataSource = dtOperationsPSOrders;
		InitGridPSOrders();
		dtShipChandler = ShipChandler.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
		InitGridShipChandler();
		dtOperationsMaterialIssueVoucher = OperationsMaterialIssueVouchers.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataMaterialIssueVoucher).DataSource = dtOperationsMaterialIssueVoucher;
		InitGridMaterialIssueVoucher();
		dtOperationsRemarks = OperationsRemarks.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataRemarks).DataSource = dtOperationsRemarks;
		InitGridRemarks();
		dtOperationsSeaMen = OperationsSeaMen.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataSeaMen).DataSource = dtOperationsSeaMen;
		InitGridSeaMen();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Operations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		((UltraToggleEditorBase)chkItemsSubAccount).Checked = false;
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)btnChangeVessel).Enabled = !bool.Parse(drMaster["Closed"].ToString());
			((Control)(object)btnChangeCostCenter).Enabled = !bool.Parse(drMaster["Closed"].ToString());
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OperationNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["OperationDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboVesselName).ValueChanged -= cboVesselName_ValueChanged;
			((TextEditorControlBase)cboVesselName).Value = drMaster["VesselID"];
			((TextEditorControlBase)cboVesselName).ValueChanged += cboVesselName_ValueChanged;
			((Control)(object)txtVoyageNo).Text = drMaster["VoyageNo"].ToString();
			((TextEditorControlBase)cboCostCenter).Value = drMaster["CostCenterID"];
			((TextEditorControlBase)cboItemQuotation).ValueChanged -= cboItemQuotation_ValueChanged;
			((TextEditorControlBase)cboItemQuotation).Value = drMaster["ItemQuotationID"];
			((TextEditorControlBase)cboItemQuotation).ValueChanged += cboItemQuotation_ValueChanged;
			((Control)(object)txtQuayNo).Text = drMaster["QuayNo"].ToString();
			((Control)(object)txtSeaMenCount).Text = drMaster["SeaMenCount"].ToString();
			((Control)(object)txtWivesChildrenCount).Text = drMaster["WivesChildrenCount"].ToString();
			((Control)(object)txtPassengersCount).Text = drMaster["PassengersCount"].ToString();
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboOwners).Value = drMaster["OwnerSubAccountID"];
			((TextEditorControlBase)cboCharterers).Value = drMaster["CharterSubAccountID"];
			((TextEditorControlBase)cboCaptain).Value = drMaster["CaptainSubAccountID"];
			dtpEntryDate.Value = (DateTime)drMaster["EntryDate"];
			((Control)(object)txtEntryLoad).Text = decimal.Parse(drMaster["EntryLoad"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboLoadType).Value = drMaster["LoadTypeID"];
			((TextEditorControlBase)txtEntryReason).Value = drMaster["EntryReason"].ToString();
			dtpDepartureDate.Value = drMaster["DepartureDate"];
			((TextEditorControlBase)cboFromSeaPort).Value = drMaster["FromSeaPortID"];
			dtpActualDepartureDate.Value = drMaster["ActualDepartureDate"];
			((UltraToggleEditorBase)chkIsActualDeparture).Checked = Convert.ToBoolean(drMaster["IsActualDeparture"]);
			((TextEditorControlBase)cboEntrySeaPort).ValueChanged -= cboEntrySeaPort_ValueChanged;
			((TextEditorControlBase)cboEntrySeaPort).Value = drMaster["EntrySeaPortID"];
			((TextEditorControlBase)cboEntrySeaPort).ValueChanged += cboEntrySeaPort_ValueChanged;
			((TextEditorControlBase)cboToSeaPort).Value = drMaster["ToSeaPortID"];
			((Control)(object)txtDepartureLoad).Text = decimal.Parse(drMaster["DepartureLoad"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRemainingFuel).Text = decimal.Parse(drMaster["RemainingFuel"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtExtendReason).Text = drMaster["ExtendReason"].ToString();
			((Control)(object)txtExportManifestNo).Text = drMaster["ExportManifestNo"].ToString();
			dtpExportManifestDate.Value = drMaster["ExportManifestDate"];
			((Control)(object)txtImportManifestNo).Text = drMaster["ImportManifestNo"].ToString();
			dtpImportManifestDate.Value = drMaster["ImportManifestDate"];
			((Control)(object)txtArrivalDraftFwd).Text = drMaster["ArrivalDraftFwd"].ToString();
			((Control)(object)txtArrivalDraftAft).Text = drMaster["ArrivalDraftAft"].ToString();
			((Control)(object)txtDepartureDraftFwd).Text = drMaster["DepartureDraftFwd"].ToString();
			((Control)(object)txtDepartureDraftAft).Text = drMaster["DepartureDraftAft"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			dtOperationsItems = OperationsItems.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataItems).DataSource = dtOperationsItems;
			InitGridItems();
			dtSeaPortReports = SeaPortsReports.SelectBySeaPortID((cboEntrySeaPort.SelectedIndex > -1) ? ((TextEditorControlBase)cboEntrySeaPort).Value.ToString() : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraGridBase)ULGDataSeaPortsReports).DataSource = dtSeaPortReports;
			InitGridSeaPortsReports();
			dtOperationsExpenses = OperationsExpenses.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationsExpenses;
			InitGridExpenses();
			dtOperationsInvoices = OperationsInvoices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataInvoices).DataSource = dtOperationsInvoices;
			InitGridInvoices();
			dtOperationsPSOrders = OperationsPSOrders.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataOrders).DataSource = dtOperationsPSOrders;
			InitGridPSOrders();
			dtShipChandler = ShipChandler.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
			InitGridShipChandler();
			dtOperationsMaterialIssueVoucher = OperationsMaterialIssueVouchers.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataMaterialIssueVoucher).DataSource = dtOperationsMaterialIssueVoucher;
			InitGridMaterialIssueVoucher();
			dtOperationsServicesStepsTasks = OperationsServicesStepsTasks.SelectInfoByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataTasks).DataSource = dtOperationsServicesStepsTasks;
			InitGridNewTasks();
			dtOperationsSeaMen = OperationsSeaMen.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataSeaMen).DataSource = dtOperationsSeaMen;
			InitGridSeaMen();
			dtOperationsRemarks = OperationsRemarks.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataRemarks).DataSource = dtOperationsRemarks;
			InitGridRemarks();
			if (drMaster["Approved"].Equals(true) || drMaster["Closed"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
			((TextEditorControlBase)txtSeaMenCount).ValueChanged -= txtSeaMenCount_ValueChanged;
			DataTable currentSeaMenCount = Operations.GetCurrentSeaMenCount(drMaster["OperationID"].ToString());
			((Control)(object)lblCurrentCrew).Text = currentSeaMenCount.Rows[0][0].ToString();
			((Control)(object)lblCurrentPassengerCount).Text = currentSeaMenCount.Rows[0][1].ToString();
			((TextEditorControlBase)txtSeaMenCount).ValueChanged += txtSeaMenCount_ValueChanged;
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void InitGrid()
	{
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "Serial No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البداية" : "Start Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ النهاية" : "End Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة" : "Percent");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Header).Caption = (GlobalVariables.IsArabic ? "إضافة للتكلفة" : "Cost Plus");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Unit Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة إضافية" : "Additional Fees");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Header).Caption = (GlobalVariables.IsArabic ? "المصروفات" : "Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحساب" : "SubAccount Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].ValueList = (IValueList)(object)vlServicesCompanies;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlServicesUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].ValueList = (IValueList)(object)vlServicesSubAccountTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].ValueList = (IValueList)(object)vlServicesSubAccounts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].DefaultCellValue = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Details"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(1, "Details");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Header).Caption = (GlobalVariables.IsArabic ? "تفاصيل" : "Details");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Header).VisiblePosition = ((!GlobalVariables.IsArabic) ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 2));
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Reports"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Reports");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Header).Caption = (GlobalVariables.IsArabic ? "الخطابات" : "Letters");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Header).VisiblePosition = ((!GlobalVariables.IsArabic) ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlServiceUsers;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "مستخدم" : "User");
		UpdateServiceDetailsButtons();
	}

	public void InitGridItems()
	{
		GlobalFunctions.PrepareGrid(ULGDataItems);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["OperationItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["RequiredQty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DeliverdQty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.075);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ReturnedQty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.075);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["RequiredQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المطلوبة" : "Req Qty");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DeliverdQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المستلمة" : "Deliverd Qty");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ReturnedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المرتجعه" : "Returned Qty");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة " : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحساب" : "SubAccount Type");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["RequiredQty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DeliverdQty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ReturnedQty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = !CanViewCostPrice;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlItemsUnits;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].ValueList = (IValueList)(object)vlItemsSubAccountTypes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].ValueList = (IValueList)(object)vlItemsSubAccounts;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["RequiredQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DeliverdQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].DefaultCellValue = GlobalVariables.OwnerSubAccountTypeIDs.Replace(",", "").Trim();
		if (drMaster != null)
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].DefaultCellValue = drMaster["OwnerSubAccountID"];
		}
	}

	public void InitGridSeaPortsReports()
	{
		GlobalFunctions.PrepareGrid(ULGDataSeaPortsReports);
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["ReportID"].Width = (int)((double)((Control)(object)ULGDataSeaPortsReports).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["ReportID"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["ReportID"].Hidden = false;
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["ReportID"].ValueList = (IValueList)(object)vlReports;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGDataSeaPortsReports).Width * 0.1);
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaPortsReports).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaPortsReports).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataSeaPortsReports).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridServicesReports()
	{
		GlobalFunctions.PrepareGrid(ULGServicesReports);
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["ReportName"].Width = (int)((double)((Control)(object)ULGServicesReports).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["ReportName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["ReportName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGServicesReports).Width * 0.1);
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGServicesReports).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGServicesReports).Rows).Count; i++)
		{
			((UltraGridBase)ULGServicesReports).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridExpenses()
	{
		GlobalFunctions.PrepareGrid(ULGDataExpenses);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CompanyID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseNo"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.14);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.12);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["PaidDate"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.14);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CompanyID"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المصروف" : "Expense No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الايصال" : "Receipt No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "صافي السعر" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["PaidDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CompanyID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseNo"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["PaidDate"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["CompanyID"].ValueList = (IValueList)(object)vlOperationsExpensesCompanies;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlOperationsExpensesAccounts;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlOperationsExpensesSubAccounts;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
	}

	public void InitGridInvoices()
	{
		GlobalFunctions.PrepareGrid(ULGDataInvoices);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationInvoiceNo"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationinvoiceDate"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsServiceInvoice"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.05);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsItemInvoice"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.05);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["CurrencyID"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["ExchangeRate"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["GrossValue"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["DiscountValue"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationinvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsServiceInvoice"].Header).Caption = (GlobalVariables.IsArabic ? "خدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsItemInvoice"].Header).Caption = (GlobalVariables.IsArabic ? "صنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["CurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["ExchangeRate"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الصرف" : "Exchange Rate");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["GrossValue"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["DiscountValue"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net Price");
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["OperationinvoiceDate"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsServiceInvoice"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["IsItemInvoice"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["CurrencyID"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["ExchangeRate"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["GrossValue"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["DiscountValue"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlInvoicesSubAccounts;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["CurrencyID"].ValueList = (IValueList)(object)vlInvoicesCurrency;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["ExchangeRate"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["GrossValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["DiscountValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGDataInvoices).Width * 0.05);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataInvoices).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataInvoices).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataInvoices).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridPSOrders()
	{
		GlobalFunctions.PrepareGrid(ULGDataOrders);
		((UltraGridBase)ULGDataOrders).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderNo"].Width = (int)((double)((Control)(object)ULGDataOrders).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderDate"].Width = (int)((double)((Control)(object)ULGDataOrders).Width * 0.45);
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGDataOrders).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم المورد" : "Supplier Name");
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderNo"].Hidden = false;
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["PSOrderDate"].Hidden = false;
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGDataOrders).Width * 0.05);
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataOrders).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataOrders).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataOrders).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridMaterialIssueVoucher()
	{
		GlobalFunctions.PrepareGrid(ULGDataMaterialIssueVoucher);
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Width = (int)((double)((Control)(object)ULGDataMaterialIssueVoucher).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Width = (int)((double)((Control)(object)ULGDataMaterialIssueVoucher).Width * 0.45);
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGDataMaterialIssueVoucher).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم العميل" : "Client Name");
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Hidden = false;
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Hidden = false;
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGDataMaterialIssueVoucher).Width * 0.05);
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataMaterialIssueVoucher).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataMaterialIssueVoucher).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataMaterialIssueVoucher).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridShipChandler()
	{
		GlobalFunctions.PrepareGrid(ULGShipChandler);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerNo"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerDate"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.15);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["GrossValue"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.1);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.1);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.1);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.1);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم إذن الصرف" : "ShipChandler No");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ إذن الصرف" : "ShipChandler Date");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["GrossValue"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Gross Value");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "خصم قبل الضريبة " : "Discount Before Tax");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerNo"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerDate"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["GrossValue"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["GrossValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["TaxTotalValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.05);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; i++)
		{
			((UltraGridBase)ULGShipChandler).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	public void InitGridNewTasks()
	{
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGDataTasks);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataTasks).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["ServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمه" : "Service");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StepName"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.15);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StepName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StepName"].Header).Caption = (GlobalVariables.IsArabic ? "المرحله" : "Step");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskName"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.15);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskName"].Header).Caption = (GlobalVariables.IsArabic ? "المهمه" : "Task");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["UserName"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.1);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["UserName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["UserName"].Header).Caption = (GlobalVariables.IsArabic ? "المندوب" : "PRO");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskPlaceID"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.15);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskPlaceID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskPlaceID"].Header).Caption = (GlobalVariables.IsArabic ? "الجهه" : "Place");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["TaskPlaceID"].ValueList = (IValueList)(object)vlTasksPlaces;
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.15);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تبدأ في" : "Starts at");
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["StartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGDataTasks).Width * 0.1);
		((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataTasks).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Done");
	}

	public void InitGridRemarks()
	{
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGDataRemarks);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["OperationRemarkID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Date"].DefaultCellValue = DateTime.Now;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.2);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Remarks");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.1);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["User_ID"].DefaultCellValue = GlobalVariables.UserID;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.1);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Hidden = false;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].ValueList = (IValueList)(object)vlToUsers;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Header).Caption = (GlobalVariables.IsArabic ? "الى مستخدم" : "To User");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Comment"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.25);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Comment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["Comment"].Header).Caption = (GlobalVariables.IsArabic ? "تعليق" : "Comment");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.1);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تام" : "Completed");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].DefaultCellValue = false;
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Width = (int)((double)((Control)(object)ULGDataRemarks).Width * 0.1);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Header).Caption = (GlobalVariables.IsArabic ? "متابعة" : "FollowUp");
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].DefaultCellValue = false;
	}

	public void InitGridSeaMen()
	{
		GlobalFunctions.PrepareGrid(ULGDataSeaMen);
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["OperationSeaManID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SeaManRankID"].Width = (int)((double)((Control)(object)ULGDataSeaMen).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SeaManRankID"].Hidden = false;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SeaManRankID"].ValueList = (IValueList)(object)vlSeaMenRanks;
		((HeaderBase)((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SeaManRankID"].Header).Caption = (GlobalVariables.IsArabic ? "الرتبه" : "Rank");
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SeaManRankID"].DefaultCellValue = GlobalVariables.UserID;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGDataSeaMen).Width * 0.5);
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSeaMen;
		((HeaderBase)((UltraGridBase)ULGDataSeaMen).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "SeaMan");
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVesselName).ReadOnly = !Adding;
		((EditorButtonControlBase)cboCostCenter).ReadOnly = !Adding;
		((Control)(object)btnVesselSearch).Visible = Adding;
		((Control)(object)btnCostCenterSearch).Visible = Adding;
		((EditorButtonControlBase)txtVoyageNo).ReadOnly = NavMode;
		((Control)(object)btnGenerateExpManifestNo).Visible = !NavMode;
		((Control)(object)btnGenerateImportManifestNo).Visible = !NavMode;
		((Control)(object)chkItemsSubAccount).Visible = !NavMode;
		((Control)(object)cboItemsSubAccountID).Visible = !NavMode;
		((Control)(object)cboItemsSubAccountTypeID).Visible = !NavMode;
		((Control)(object)btnChangeVessel).Visible = NavMode;
		((Control)(object)btnChangeCostCenter).Visible = NavMode;
		((Control)(object)chkIsActualDeparture).Enabled = !NavMode;
		((EditorButtonControlBase)dtpActualDepartureDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboItemQuotation).ReadOnly = NavMode;
		((EditorButtonControlBase)txtQuayNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeaMenCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWivesChildrenCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPassengersCount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOwners).ReadOnly = !Adding;
		((EditorButtonControlBase)cboCharterers).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCaptain).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpEntryDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEntryLoad).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLoadType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEntryReason).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDepartureDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFromSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEntrySeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboToSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepartureLoad).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRemainingFuel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExtendReason).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArrivalDraftFwd).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArrivalDraftAft).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepartureDraftFwd).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepartureDraftAft).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExportManifestNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpExportManifestDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtImportManifestNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpImportManifestDate).ReadOnly = NavMode;
		((Control)(object)btnPrintExportManifest).Visible = NavMode;
		((Control)(object)btnPrintImportManifest).Visible = NavMode;
		((Control)(object)btnPrintEmptyManifest).Visible = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboItemQuotation).ReadOnly = NavMode || dtOperationsItems.Select("DeliverdQty > 0 ").Length != 0 || ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataMaterialIssueVoucher).Rows).Count > 0;
		((Control)(object)btnItemQuotationSearch).Visible = Adding;
		((Control)(object)btnOwnerSearch).Visible = Adding;
		((Control)(object)btnCharterer).Visible = Adding;
		((Control)(object)btnCaptain).Visible = Adding;
		((Control)(object)btnFromSeaPortSearch).Visible = !NavMode;
		((Control)(object)btnEntrySeaPortSearch).Visible = !NavMode;
		((Control)(object)btnToSeaPortSearch).Visible = !NavMode;
		if (Updating)
		{
			((TextEditorControlBase)cboItemQuotation).ValueChanged -= cboItemQuotation_ValueChanged;
			int num = 0;
			if (cboItemQuotation.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboItemQuotation).Value.ToString());
			}
			DataView dataView = new DataView(dtItemsQuotations);
			dataView.RowFilter = " Approved =1  And Closed=0 And BranchID =" + GlobalVariables.CurrentBranchID + " And VesselID = " + ((TextEditorControlBase)cboVesselName).Value.ToString() + " And ItemQuotationValidTo  >=   '" + dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'";
			GlobalFunctions.FillCombo(cboItemQuotation, dataView.ToTable(), "ItemQuotationID", "ItemQuotationNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboItemQuotation).Value = num;
			}
			((TextEditorControlBase)cboItemQuotation).ValueChanged += cboItemQuotation_ValueChanged;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["InvoiceSubAccountTypeID"].Value != DBNull.Value)
				{
					int subAccountTypeID = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["InvoiceSubAccountTypeID"].Value.ToString());
					ValueList subAccountValueList = getSubAccountValueList(subAccountTypeID);
					((UltraGridBase)ULGData).Rows[i].Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
					if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["InvoiceSubAccountID"].Value = DBNull.Value;
					}
				}
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGDataItems).Rows[j].Cells["InvoiceSubAccountTypeID"].Value != DBNull.Value)
				{
					int subAccountTypeID2 = int.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["InvoiceSubAccountTypeID"].Value.ToString());
					ValueList subAccountValueList2 = getSubAccountValueList(subAccountTypeID2);
					((UltraGridBase)ULGDataItems).Rows[j].Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)subAccountValueList2;
					if (((DisposableObjectCollectionBase)subAccountValueList2.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGDataItems).Rows[j].Cells["InvoiceSubAccountID"].Value = DBNull.Value;
					}
				}
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataExpenses).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGDataExpenses).Rows[k].Cells["AccountID"].Value != DBNull.Value)
				{
					int accountID = int.Parse(((UltraGridBase)ULGDataExpenses).Rows[k].Cells["AccountID"].Value.ToString());
					ValueList subAccountValueListByAccountID = getSubAccountValueListByAccountID(accountID);
					((UltraGridBase)ULGDataExpenses).Rows[k].Cells["AccountID"].ValueList = (IValueList)(object)subAccountValueListByAccountID;
					if (((DisposableObjectCollectionBase)subAccountValueListByAccountID.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGDataExpenses).Rows[k].Cells["AccountID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataInvoices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataRemarks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataSeaMen).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		UpdateServiceDetailsButtons();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		UltraDateTimeEditor obj = dtpDate;
		DateTime dateTime = (dtpEntryDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		obj.DateTime = dateTime;
		((Control)(object)txtCode).Text = (Adding ? Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboVesselName).ValueChanged -= cboVesselName_ValueChanged;
		cboVesselName.SelectedIndex = -1;
		((TextEditorControlBase)cboVesselName).ValueChanged += cboVesselName_ValueChanged;
		((Control)(object)txtVoyageNo).Text = "";
		((Control)(object)lblCurrentCrew).Text = "";
		((Control)(object)lblCurrentPassengerCount).Text = "";
		cboCostCenter.SelectedIndex = -1;
		((TextEditorControlBase)cboItemQuotation).ValueChanged -= cboItemQuotation_ValueChanged;
		cboItemQuotation.SelectedIndex = -1;
		((TextEditorControlBase)cboItemQuotation).ValueChanged += cboItemQuotation_ValueChanged;
		((UltraToggleEditorBase)chkIsActualDeparture).Checked = false;
		dtpActualDepartureDate.Value = DBNull.Value;
		((TextEditorControlBase)txtQuayNo).Clear();
		((TextEditorControlBase)txtSeaMenCount).ValueChanged -= txtSeaMenCount_ValueChanged;
		((Control)(object)txtSeaMenCount).Text = "0";
		((TextEditorControlBase)txtSeaMenCount).ValueChanged += txtSeaMenCount_ValueChanged;
		((Control)(object)txtWivesChildrenCount).Text = "0";
		((Control)(object)txtPassengersCount).Text = "0";
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((Control)(object)txtExchangeRate).Text = "0";
		cboOwners.SelectedIndex = -1;
		cboCharterers.SelectedIndex = -1;
		cboCaptain.SelectedIndex = -1;
		((Control)(object)txtEntryLoad).Text = "0";
		cboLoadType.SelectedIndex = -1;
		((TextEditorControlBase)txtEntryReason).Clear();
		((Control)(object)txtArrivalDraftFwd).Text = "0";
		((Control)(object)txtArrivalDraftAft).Text = "0";
		((Control)(object)txtDepartureDraftFwd).Text = "0";
		((Control)(object)txtDepartureDraftAft).Text = "0";
		cboFromSeaPort.SelectedIndex = -1;
		((TextEditorControlBase)cboEntrySeaPort).ValueChanged -= cboEntrySeaPort_ValueChanged;
		cboEntrySeaPort.SelectedIndex = -1;
		((TextEditorControlBase)cboEntrySeaPort).ValueChanged += cboEntrySeaPort_ValueChanged;
		cboToSeaPort.SelectedIndex = -1;
		((Control)(object)txtDepartureLoad).Text = "0";
		((Control)(object)txtRemainingFuel).Text = "0";
		((TextEditorControlBase)txtExtendReason).Clear();
		((TextEditorControlBase)txtExportManifestNo).Clear();
		dtpExportManifestDate.Value = DBNull.Value;
		((TextEditorControlBase)txtImportManifestNo).Clear();
		dtpImportManifestDate.Value = DBNull.Value;
		dtpDepartureDate.Value = DBNull.Value;
		((TextEditorControlBase)txtNotes).Clear();
		((DataTable)((UltraGridBase)ULGDataItems).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataSeaPortsReports).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataExpenses).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataSeaMen).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataInvoices).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataRemarks).DataSource).Rows.Clear();
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
		if (cboVesselName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الباخرة" : "Please Select Vessel");
			((TextEditorControlBase)cboVesselName).Focus();
			cboVesselName.DropDown();
			return false;
		}
		if (cboCostCenter.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مركز التكلفة" : "Please Select Cost Center");
			((TextEditorControlBase)cboCostCenter).Focus();
			cboCostCenter.DropDown();
			return false;
		}
		if (((Control)(object)txtVoyageNo).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الرحلة" : "Please Enter Voyage No");
			((TextEditorControlBase)txtVoyageNo).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_Operations", "VoyageNo", ((Control)(object)txtVoyageNo).Text, Adding ? "0" : drMaster["VoyageNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), " and VesselID = " + ((TextEditorControlBase)cboVesselName).Value.ToString()) > 0)
		{
			string text = Operations.VoyageNoGetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذه الرحلة متواجد من قبل \n سوف يتم الحفظ برقم " + text, "The Voyage Number Already Exists It Will Be Saved With No. : " + text);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtVoyageNo).Focus();
				return false;
			}
			((Control)(object)txtVoyageNo).Text = text;
		}
		if (Adding && int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From MS_Operations Where " + (Adding ? " " : ("OperationID <> " + drMaster["OperationID"].ToString() + " And ")) + " VesselID = " + ((TextEditorControlBase)cboVesselName).Value.ToString() + " and Closed = 0 and Deleted = 0 And BranchID = " + GlobalVariables.CurrentBranchID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.QuestionMB.Show("توجد رحلة اخرى على نفس الباخرة. هل تريد الاستمرار؟", "There is another voyage on the same vessel. Do you want to continue?");
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				return false;
			}
		}
		if (((UltraTabControlBase)UTCDetails).Tabs["VoyageNo"].Visible)
		{
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
			if (cboOwners.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المالك" : "Please Select Owners");
				((TextEditorControlBase)cboOwners).Focus();
				cboOwners.DropDown();
				return false;
			}
			if (cboCharterers.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المستأجر" : "Please Select Charter");
				((TextEditorControlBase)cboCharterers).Focus();
				cboCharterers.DropDown();
				return false;
			}
			if (cboCaptain.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار القبطان" : "Please Select Captains");
				((TextEditorControlBase)cboCaptain).Focus();
				cboCaptain.DropDown();
				return false;
			}
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_Operations", "OperationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGDataItems).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGDataItems).Rows[i].Cells["InvoiceSubAccountID"].Value.ToString() == ((UltraGridBase)ULGDataItems).Rows[j].Cells["InvoiceSubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف مع نفس الحساب التحليلي ", "Cannot Duplicate The Same Item with Same SubAccount");
					ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"];
					return false;
				}
			}
		}
		if (((UltraTabControlBase)UTCDetails).Tabs["Details"].Visible)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[k].Cells["ServiceID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال اسم الخدمة  ", "Please Enter Service Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["ServiceID"];
					((UltraGridBase)ULGData).Rows[k].Cells["ServiceID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[k].Cells["CompanyID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال اسم الشركة  ", "Please Enter Company Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["CompanyID"];
					((UltraGridBase)ULGData).Rows[k].Cells["CompanyID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountTypeID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show(" برجاء ادخال نوع الحساب التحليلي  ", "Please Enter SubAccount Type");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountTypeID"];
					((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountTypeID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show(" برجاء ادخال اسم الحساب التحليلي", "Please Enter SubAccount Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountID"];
					((UltraGridBase)ULGData).Rows[k].Cells["InvoiceSubAccountID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
			}
		}
		if (((UltraTabControlBase)UTCDetails).Tabs["SeaMen"].Visible)
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count; l++)
			{
				if (((UltraGridBase)ULGDataSeaMen).Rows[l].Cells["SubAccountID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء اختيار البحار  ", "Please Select a SeaMan");
					ULGDataSeaMen.ActiveCell = ((UltraGridBase)ULGDataSeaMen).Rows[l].Cells["SubAccountID"];
					((UltraGridBase)ULGDataSeaMen).Rows[l].Cells["SubAccountID"].DroppedDown = true;
					return false;
				}
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count; m++)
				{
					if (l != m && ((UltraGridBase)ULGDataSeaMen).Rows[l].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGDataSeaMen).Rows[m].Cells["SubAccountID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار البحار ", "Can not Duplicate The Same SeaMan");
						ULGDataSeaMen.ActiveCell = ((UltraGridBase)ULGDataSeaMen).Rows[l].Cells["SubAccountID"];
						return false;
					}
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Expected O, but got Unknown
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Operations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), "Null", "Null", ((Control)(object)txtVoyageNo).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (cboOwners.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOwners).Value.ToString(), (cboCharterers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCharterers).Value.ToString(), (cboCaptain.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCaptain).Value.ToString(), (cboServiceQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceQuotation).Value.ToString(), (cboItemQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItemQuotation).Value.ToString(), (((Control)(object)txtQuayNo).Text == "") ? "Null" : ((Control)(object)txtQuayNo).Text, (((Control)(object)txtSeaMenCount).Text == "") ? "0" : ((Control)(object)txtSeaMenCount).Text, (((Control)(object)txtWivesChildrenCount).Text == "") ? "0" : ((Control)(object)txtWivesChildrenCount).Text, (((Control)(object)txtPassengersCount).Text == "") ? "Null" : ((Control)(object)txtPassengersCount).Text, dtpEntryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtEntryLoad).Text == "") ? "Null" : ((Control)(object)txtEntryLoad).Text, (cboLoadType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadType).Value.ToString(), (((Control)(object)txtEntryReason).Text == "") ? "Null" : ((Control)(object)txtEntryReason).Text, (cboFromSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFromSeaPort).Value.ToString(), (cboEntrySeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEntrySeaPort).Value.ToString(), (cboToSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboToSeaPort).Value.ToString(), (dtpDepartureDate.Value == null) ? "Null" : dtpDepartureDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsActualDeparture).Checked ? "1" : "0", (dtpActualDepartureDate.Value == null) ? "Null" : dtpActualDepartureDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtDepartureLoad).Text == "") ? "Null" : ((Control)(object)txtDepartureLoad).Text, (((Control)(object)txtRemainingFuel).Text == "") ? "0" : ((Control)(object)txtRemainingFuel).Text, (((Control)(object)txtExtendReason).Text == "") ? "Null" : ((Control)(object)txtExtendReason).Text, (((Control)(object)txtArrivalDraftFwd).Text == "") ? "0" : ((Control)(object)txtArrivalDraftFwd).Text, (((Control)(object)txtArrivalDraftAft).Text == "") ? "0" : ((Control)(object)txtArrivalDraftAft).Text, (((Control)(object)txtDepartureDraftFwd).Text == "") ? "0" : ((Control)(object)txtDepartureDraftFwd).Text, (((Control)(object)txtDepartureDraftAft).Text == "") ? "0" : ((Control)(object)txtDepartureDraftAft).Text, (((Control)(object)txtExportManifestNo).Text == "") ? "Null" : ((Control)(object)txtExportManifestNo).Text, (dtpExportManifestDate.Value == null) ? "Null" : dtpExportManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtImportManifestNo).Text == "") ? "Null" : ((Control)(object)txtImportManifestNo).Text, (dtpImportManifestDate.Value == null) ? "Null" : dtpImportManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			dtDetails.AcceptChanges();
			OperationsServices.Insert_UpdateXML(dtDetails, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count > 0)
			{
				ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
				{
					((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationItemID"].Value = -1;
					((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationID"].Value = num;
					((UltraGridBase)ULGDataItems).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
				OperationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataItems).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count > 0)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["OperationSeaManID"].Value = -1;
					((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["OperationID"].Value = num;
					((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				OperationsSeaMen.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataSeaMen).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRemarks).Rows).Count > 0)
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRemarks).Rows).Count; k++)
				{
					((UltraGridBase)ULGDataRemarks).Rows[k].Cells["OperationRemarkID"].Value = -1;
					((UltraGridBase)ULGDataRemarks).Rows[k].Cells["OperationID"].Value = num;
					((UltraGridBase)ULGDataRemarks).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				OperationsRemarks.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataRemarks).DataSource, GlobalVariables.UserID);
			}
			RowID = num.ToString();
			Main.EndBulkTrans(FromServer: false);
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
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Expected O, but got Unknown
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Operations.Insert_Update(drMaster["OperationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), "Null", "Null", ((Control)(object)txtVoyageNo).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, (cboOwners.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOwners).Value.ToString(), (cboCharterers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCharterers).Value.ToString(), (cboCaptain.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCaptain).Value.ToString(), (cboServiceQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceQuotation).Value.ToString(), (cboItemQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItemQuotation).Value.ToString(), (((Control)(object)txtQuayNo).Text == "") ? "Null" : ((Control)(object)txtQuayNo).Text, (((Control)(object)txtSeaMenCount).Text == "") ? "0" : ((Control)(object)txtSeaMenCount).Text, (((Control)(object)txtWivesChildrenCount).Text == "") ? "0" : ((Control)(object)txtWivesChildrenCount).Text, (((Control)(object)txtPassengersCount).Text == "") ? "Null" : ((Control)(object)txtPassengersCount).Text, dtpEntryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtEntryLoad).Text == "") ? "Null" : ((Control)(object)txtEntryLoad).Text, (cboLoadType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadType).Value.ToString(), (((Control)(object)txtEntryReason).Text == "") ? "Null" : ((Control)(object)txtEntryReason).Text, (cboFromSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFromSeaPort).Value.ToString(), (cboEntrySeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEntrySeaPort).Value.ToString(), (cboToSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboToSeaPort).Value.ToString(), (dtpDepartureDate.Value == null) ? "Null" : dtpDepartureDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsActualDeparture).Checked ? "1" : "0", (dtpActualDepartureDate.Value == null) ? "Null" : dtpActualDepartureDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtDepartureLoad).Text == "") ? "Null" : ((Control)(object)txtDepartureLoad).Text, (((Control)(object)txtRemainingFuel).Text == "") ? "0" : ((Control)(object)txtRemainingFuel).Text, (((Control)(object)txtExtendReason).Text == "") ? "Null" : ((Control)(object)txtExtendReason).Text, (((Control)(object)txtArrivalDraftFwd).Text == "") ? "0" : ((Control)(object)txtArrivalDraftFwd).Text, (((Control)(object)txtArrivalDraftAft).Text == "") ? "0" : ((Control)(object)txtArrivalDraftAft).Text, (((Control)(object)txtDepartureDraftFwd).Text == "") ? "0" : ((Control)(object)txtDepartureDraftFwd).Text, (((Control)(object)txtDepartureDraftAft).Text == "") ? "0" : ((Control)(object)txtDepartureDraftAft).Text, (((Control)(object)txtExportManifestNo).Text == "") ? "Null" : ((Control)(object)txtExportManifestNo).Text, (dtpExportManifestDate.Value == null) ? "Null" : dtpExportManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtImportManifestNo).Text == "") ? "Null" : ((Control)(object)txtImportManifestNo).Text, (dtpImportManifestDate.Value == null) ? "Null" : dtpImportManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			dtDetails.AcceptChanges();
			OperationsServices.Insert_UpdateXML(dtDetails, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string text = ",";
			ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
			{
				((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDataItems).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationItemID"].Value.ToString() + ",";
			}
			ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
			Main.DeleteForUpdate("MS_OperationsItems", "OperationID", drMaster["OperationID"].ToString(), "OperationItemID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count > 0)
			{
				OperationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataItems).DataSource, GlobalVariables.UserID);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text2 = text2 + ((UltraGridBase)ULGDataSeaMen).Rows[j].Cells["OperationSeaManID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsSeaMen", "OperationID", drMaster["OperationID"].ToString(), "OperationSeaManID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSeaMen).Rows).Count > 0)
			{
				OperationsSeaMen.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataSeaMen).DataSource, GlobalVariables.UserID);
			}
			string text3 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRemarks).Rows).Count; k++)
			{
				((UltraGridBase)ULGDataRemarks).Rows[k].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDataRemarks).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text3 = text3 + ((UltraGridBase)ULGDataRemarks).Rows[k].Cells["OperationRemarkID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsRemarks", "OperationID", drMaster["OperationID"].ToString(), "OperationRemarkID", text3);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRemarks).Rows).Count > 0)
			{
				OperationsRemarks.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataRemarks).DataSource, GlobalVariables.UserID);
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
			Operations.DeleteVirtualOperationDetails(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		if (Adding)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
			dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboVesselName, dtVessels, "VesselID", "VesselName");
			dtItemsQuotations = ItemsQuotations.FillCombo(GlobalVariables.BranchIDs);
			GlobalFunctions.FillCombo(cboItemQuotation, dtItemsQuotations, "ItemQuotationID", "ItemQuotationNo");
			FillCurrencyDropDown();
			dtOwners = Owners.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboOwners, dtOwners, "SubAccountID", "OwnerName");
			dtCharters = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboCharterers, dtCharters, "SubAccountID", "SubAccountName");
			dtCaptains = Captains.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboCaptain, dtCaptains, "SubAccountID", "CaptainName");
			dtLoadType = LoadTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboLoadType, dtLoadType, "LoadTypeID", "LoadTypeName");
			dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboFromSeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
			GlobalFunctions.FillCombo(cboEntrySeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
			GlobalFunctions.FillCombo(cboToSeaPort, dtSeaPorts, "SeaPortID", "SeaPortName");
		}
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
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsUnits.ValueListItems.Clear();
		vlServicesUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlItemsUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
			vlServicesUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int m = 0; m < dtTaxs.Rows.Count; m++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[m]["TaxID"], dtTaxs.Rows[m]["TaxName"].ToString());
		}
		dtSeaMenRanks = SeaMenRanks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSeaMenRanks.ValueListItems.Clear();
		for (int n = 0; n < dtSeaMenRanks.Rows.Count; n++)
		{
			vlSeaMenRanks.ValueListItems.Add(dtSeaMenRanks.Rows[n]["SeaManRankID"], dtSeaMenRanks.Rows[n]["SeaManRankName"].ToString());
		}
		dtSubAccountTypes = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.AgentSubAccountTypeIDs + GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs + GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboItemsSubAccountTypeID, dtSubAccountTypes, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		vlItemsSubAccountTypes.ValueListItems.Clear();
		vlServicesSubAccountTypes.ValueListItems.Clear();
		vlOperationsExpensesAccounts.ValueListItems.Clear();
		for (int num = 0; num < dtSubAccountTypes.Rows.Count; num++)
		{
			vlItemsSubAccountTypes.ValueListItems.Add(dtSubAccountTypes.Rows[num]["SubAccountTypeID"], GlobalVariables.IsArabic ? dtSubAccountTypes.Rows[num]["SubAccountTypeNameAr"].ToString() : dtSubAccountTypes.Rows[num]["SubAccountTypeNameEn"].ToString());
			vlServicesSubAccountTypes.ValueListItems.Add(dtSubAccountTypes.Rows[num]["SubAccountTypeID"], GlobalVariables.IsArabic ? dtSubAccountTypes.Rows[num]["SubAccountTypeNameAr"].ToString() : dtSubAccountTypes.Rows[num]["SubAccountTypeNameEn"].ToString());
			vlOperationsExpensesAccounts.ValueListItems.Add(dtSubAccountTypes.Rows[num]["SubAccountTypeID"], GlobalVariables.IsArabic ? dtSubAccountTypes.Rows[num]["SubAccountTypeNameAr"].ToString() : dtSubAccountTypes.Rows[num]["SubAccountTypeNameEn"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItemsSubAccountID, dtSubAccounts, "SubAccountID", "Name");
		vlItemsSubAccounts.ValueListItems.Clear();
		vlServicesSubAccounts.ValueListItems.Clear();
		vlOperationsExpensesSubAccounts.ValueListItems.Clear();
		vlInvoicesSubAccounts.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtSubAccounts.Rows.Count; num2++)
		{
			vlItemsSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num2]["SubAccountID"], dtSubAccounts.Rows[num2]["Name"].ToString());
			vlServicesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num2]["SubAccountID"], dtSubAccounts.Rows[num2]["Name"].ToString());
			vlOperationsExpensesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num2]["SubAccountID"], dtSubAccounts.Rows[num2]["Name"].ToString());
			vlInvoicesSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[num2]["SubAccountID"], dtSubAccounts.Rows[num2]["Name"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtServices.Rows.Count; num3++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[num3]["ServiceID"], dtServices.Rows[num3]["ServiceName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServicesCompanies.ValueListItems.Clear();
		vlOperationsExpensesCompanies.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtCompanies.Rows.Count; num4++)
		{
			vlServicesCompanies.ValueListItems.Add(dtCompanies.Rows[num4]["CompanyID"], dtCompanies.Rows[num4]["CompanyName"].ToString());
			vlOperationsExpensesCompanies.ValueListItems.Add(dtCompanies.Rows[num4]["CompanyID"], dtCompanies.Rows[num4]["CompanyName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlOperationsExpenses.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtExpenses.Rows.Count; num5++)
		{
			vlOperationsExpenses.ValueListItems.Add(dtExpenses.Rows[num5]["ExpenseID"], dtExpenses.Rows[num5]["ExpenseName"].ToString());
		}
		dtSeaMen = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSeaMen.ValueListItems.Clear();
		for (int num6 = 0; num6 < dtSeaMen.Rows.Count; num6++)
		{
			vlSeaMen.ValueListItems.Add(dtSeaMen.Rows[num6]["SubAccountID"], dtSeaMen.Rows[num6]["SubAccountName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("MarineService", "Reports", "frmMSLetters"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlReports.ValueListItems.Clear();
		for (int num7 = 0; num7 < dtReports.Rows.Count; num7++)
		{
			vlReports.ValueListItems.Add(dtReports.Rows[num7]["ReportID"], dtReports.Rows[num7]["ReportName"].ToString());
		}
		dtInvoicesCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoicesCurrency.ValueListItems.Clear();
		for (int num8 = 0; num8 < dtInvoicesCurrency.Rows.Count; num8++)
		{
			vlInvoicesCurrency.ValueListItems.Add(dtInvoicesCurrency.Rows[num8]["CurrencyID"], dtInvoicesCurrency.Rows[num8]["CurrencyName"].ToString());
		}
	}

	public override void btnPrintClick()
	{
	}

	public override void btnOKClick()
	{
		((Control)(object)btnOK).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		if (Adding)
		{
			AddData();
			if (DataSaved)
			{
				Adding = false;
				SetControls(NavMode: true);
				if (RowID != "" && TableName != "")
				{
					UsersTransactions.DeleteByRowID(TableName, RowID);
				}
				FillData();
			}
			return;
		}
		UpdateData();
		if (DataSaved)
		{
			Updating = false;
			SetControls(NavMode: true);
			if (RowID != "" && TableName != "")
			{
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
			FillData();
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.OperationsSearchReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationID"].ToString();
			FillData();
		}
	}

	private void btnVesselSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.VesselsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboVesselName).Value = num;
		}
	}

	private void btnOwnerSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.OwnersSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboOwners).Value = num;
		}
	}

	private void btnCharterer_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ChartersSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboCharterers).Value = num;
		}
	}

	private void btnCaptain_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CaptainsSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboCaptain).Value = num;
		}
	}

	private void btnFromSeaPortSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(-1, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboFromSeaPort).Value = num;
		}
	}

	private void btnEntrySeaPortSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(-1, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboEntrySeaPort).Value = num;
		}
	}

	private void btnToSeaPortSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(-1, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboToSeaPort).Value = num;
		}
	}

	private void btnItemQuotationSearch_Click(object sender, EventArgs e)
	{
		if (cboVesselName.SelectedIndex > -1)
		{
			int num = SearchFunctions.MSItemsQuotationsSearch("," + GlobalVariables.CurrentBranchID + ",", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), 1, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboItemQuotation).Value = num;
			}
		}
	}

	private void cboVesselName_ValueChanged(object sender, EventArgs e)
	{
		if (cboVesselName.SelectedIndex > -1)
		{
			DataRow dataRow = dtVessels.Select(" VesselID = " + ((TextEditorControlBase)cboVesselName).Value.ToString())[0];
			cboItemQuotation.SelectedIndex = -1;
			cboServiceQuotation.SelectedIndex = -1;
			DataView dataView = new DataView(dtItemsQuotations);
			dataView.RowFilter = " Approved =1  And Closed=0 And BranchID =" + GlobalVariables.CurrentBranchID + " And VesselID = " + ((TextEditorControlBase)cboVesselName).Value.ToString() + " And ItemQuotationValidTo  >=   '" + dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'";
			GlobalFunctions.FillCombo(cboItemQuotation, dataView.ToTable(), "ItemQuotationID", "ItemQuotationNo");
			((TextEditorControlBase)cboOwners).Value = dataRow["OwnerSubAccountID"];
			((TextEditorControlBase)cboCharterers).Value = dataRow["ClientSubAccountID"];
			if (Adding)
			{
				((Control)(object)txtVoyageNo).Text = Operations.VoyageNoGetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), GlobalVariables.CurrentBranchID);
			}
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

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding && ((TextEditorControlBase)cboVesselName).Value != null)
		{
			((Control)(object)txtCode).Text = Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			((Control)(object)txtVoyageNo).Text = Operations.VoyageNoGetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((TextEditorControlBase)cboVesselName).Value.ToString(), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboEntrySeaPort_ValueChanged(object sender, EventArgs e)
	{
		if (cboEntrySeaPort.SelectedIndex > -1)
		{
			dtSeaPortReports = SeaPortsReports.SelectBySeaPortID((cboEntrySeaPort.SelectedIndex > -1) ? ((TextEditorControlBase)cboEntrySeaPort).Value.ToString() : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraGridBase)ULGDataSeaPortsReports).DataSource = dtSeaPortReports;
			InitGridSeaPortsReports();
		}
		else
		{
			dtSeaPortReports.Rows.Clear();
		}
	}

	private void cboItemQuotation_ValueChanged(object sender, EventArgs e)
	{
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		if (cboItemQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboItemQuotation).ValueChanged -= cboItemQuotation_ValueChanged;
			dtItemsQuotationsDetails = OperationsItems.FillByItemQuotationID(((TextEditorControlBase)cboItemQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataItems).DataSource = dtItemsQuotationsDetails;
			InitGridItems();
			dtItemsPrices = ItemsPrices.GetPriceWithMSItemQuotations((cboItemQuotation.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboItemQuotation).Value.ToString(), (cboItemQuotation.SelectedIndex == -1 || dtItemsQuotations.Select(" ItemQuotationID=" + ((TextEditorControlBase)cboItemQuotation).Value.ToString())[0]["PriceTypeID"] == DBNull.Value) ? "1" : dtItemsQuotations.Select(" ItemQuotationID=" + ((TextEditorControlBase)cboItemQuotation).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			if (dtItemsPrices != null && dtItemsPrices.Rows.Count > 0)
			{
				ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
				{
					((UltraGridBase)ULGDataItems).Rows[i].Cells["UnitPrice"].Value = dtItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
					((UltraGridBase)ULGDataItems).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["DeliverdQty"].Value.ToString());
				}
				ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
			}
			((TextEditorControlBase)cboItemQuotation).ValueChanged += cboItemQuotation_ValueChanged;
		}
		else
		{
			dtItemsPrices = null;
		}
	}

	private void cboServiceQuotation_ValueChanged(object sender, EventArgs e)
	{
	}

	private void ULGShipChandler_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGShipChandler).ActiveRow != null && ULGShipChandler.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGShipChandler).ActiveRow.Cells["ShipChandlerID"].Value != DBNull.Value)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_ShipChandler_A.rpt" : "Rep_MS_ShipChandler_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ShipChandlerIDs", "," + ((UltraGridBase)ULGShipChandler).ActiveRow.Cells["ShipChandlerID"].Value.ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void ULGShipChandler_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGShipChandler).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGShipChandler).ActiveRow).Selected = true;
		}
	}

	private void ULGDataItems_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Expected O, but got Unknown
		ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "DeliverdQty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "TotalPrice") && ULGDataItems.ActiveCell.Value == DBNull.Value)
		{
			ULGDataItems.ActiveCell.Value = 0;
		}
		else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "DeliverdQty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitPrice")
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString());
		}
		else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString());
		}
		ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
	}

	private void ULGDataItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (ULGDataItems.ActiveCell != null && ((UltraGridBase)ULGDataItems).ActiveRow != null && (((UltraGridBase)ULGDataItems).ActiveRow.Cells["OperationInvoiceID"].Value != DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m))
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "DeliverdQty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "ReturnedQty")
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (ULGDataItems.ActiveCell != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "InvoiceSubAccountID" && ((UltraGridBase)ULGDataItems).ActiveRow.Cells["InvoiceSubAccountTypeID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (ULGDataItems.ActiveCell != null && (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "ColorID") && double.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString()) != 0.0)
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
	}

	private void ULGDataItems_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataItems.ActiveCell != null && (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "DeliverdQty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "TotalPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGDataItems.ActiveCell, e);
		}
	}

	private void ULGDataItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "ItemID")
		{
			int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
			if (num != 0)
			{
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = num;
			}
		}
		e.Handled = true;
	}

	private void ULGDataItems_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Expected O, but got Unknown
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Expected O, but got Unknown
		ULGDataItems.CellListSelect -= new CellEventHandler(ULGDataItems_CellListSelect);
		ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
		((UltraGridBase)ULGDataItems).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = e.Cell.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["RequiredQty"].Value = 1;
			if (dtItemsPrices == null || dtItemsPrices.Rows.Count == 0)
			{
				dtItemsPrices = ItemsPrices.GetPriceWithMSItemQuotations((cboItemQuotation.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboItemQuotation).Value.ToString(), (cboItemQuotation.SelectedIndex == -1 || dtItemsQuotations.Select(" ItemQuotationID=" + ((TextEditorControlBase)cboItemQuotation).Value.ToString())[0]["PriceTypeID"] == DBNull.Value) ? "1" : dtItemsQuotations.Select(" ItemQuotationID=" + ((TextEditorControlBase)cboItemQuotation).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			if (dtItemsPrices != null && dtItemsPrices.Rows.Count > 0)
			{
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitPrice"].Value = dtItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["DeliverdQty"].Value.ToString());
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "InvoiceSubAccountTypeID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			int num = ((vlItemsSubAccountTypes.SelectedItem != null) ? int.Parse(dtSubAccountTypes.Rows[vlItemsSubAccountTypes.SelectedIndex]["SubAccountTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["InvoiceSubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
				if ("," + num + "," == GlobalVariables.OwnerSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["OwnerSubAccountID"];
				}
				else if ("," + num + "," == GlobalVariables.CharterSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["CharterSubAccountID"];
				}
				else if ("," + num + "," == GlobalVariables.CaptainSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["CaptainSubAccountID"];
				}
			}
			else
			{
				e.Cell.Row.Cells["InvoiceSubAccountID"].ValueList = null;
				e.Cell.Row.Cells["InvoiceSubAccountID"].Value = DBNull.Value;
			}
		}
		ULGDataItems.CellListSelect += new CellEventHandler(ULGDataItems_CellListSelect);
		ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
	}

	private void ULGDataItems_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (e.Rows[i].Cells["OperationInvoiceID"].Value != DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لوجود فواتير على الصنف" : "Cannot Delete This Item Because There Are invoices On It");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
			if (double.Parse(e.Rows[i].Cells["DeliverdQty"].Value.ToString()) != 0.0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لاستلامه" : "Cannot Delete This Item Because It Was Delivered");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGData_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && e.Cell.Row.Cells["OperationServiceID"].Value != null && e.Cell.Row.Cells["OperationServiceID"].Value.ToString() != "-1" && OperationsServices.CheckForDetails(e.Cell.Row.Cells["OperationServiceID"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن تغيير هذه الخدمة لوجود تفاصيل" : "Cannot Change This Service Because It Has Details");
			((CancelEventArgs)(object)e).Cancel = true;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Expected O, but got Unknown
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Expected O, but got Unknown
		ULGData.BeforeCellUpdate -= new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
		{
			ULGData.ActiveCell.Value = 0;
		}
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalFees"))
		{
			if (bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsExpensePercent"].Value.ToString()))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) + 100m) / 100m * (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalExpenses"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalFees"].Value.ToString()));
			}
			else if (bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsCostPlus"].Value.ToString()))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalExpenses"].Value.ToString());
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			CalculateRowTax(((UltraGridBase)ULGData).ActiveRow);
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
		{
			CalculateRowTax(((UltraGridBase)ULGData).ActiveRow);
		}
		ULGData.BeforeCellUpdate += new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OperationInvoiceID"].Value != DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ServiceID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalExpenses"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsCostPlus" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsExpensePercent" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalExpenses"))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalFees" && !bool.Parse(ULGData.ActiveCell.Row.Cells["IsExpensePercent"].Value.ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && bool.Parse(ULGData.ActiveCell.Row.Cells["IsExpensePercent"].Value.ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !bool.Parse(dtServices.Select(" ServiceID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "InvoiceSubAccountID" && ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceSubAccountTypeID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "User_ID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OperationServiceNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Expected O, but got Unknown
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Expected O, but got Unknown
		//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3e: Expected O, but got Unknown
		ULGData.BeforeCellUpdate -= new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			if (e.Cell.Row.Cells["OperationServiceID"].Value != null && e.Cell.Row.Cells["OperationServiceID"].Value.ToString() != "-1" && OperationsServices.CheckForDetails(e.Cell.Row.Cells["OperationServiceID"].Value.ToString()))
			{
				return;
			}
			DataRow dataRow = dtServices.Select(" ServiceID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = e.Cell.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["IsExpensePercent"].Value = dataRow["IsExpensePercent"];
			((UltraGridBase)ULGData).ActiveRow.Cells["IsCostPlus"].Value = dataRow["IsCostPlus"];
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["StartDate"].Value = dtpDate.DateTime;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "InvoiceSubAccountTypeID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			int num = ((vlServicesSubAccountTypes.SelectedItem != null) ? int.Parse(dtSubAccountTypes.Rows[vlServicesSubAccountTypes.SelectedIndex]["SubAccountTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["InvoiceSubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
				if ("," + num + "," == GlobalVariables.OwnerSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["OwnerSubAccountID"];
				}
				else if ("," + num + "," == GlobalVariables.CharterSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["CharterSubAccountID"];
				}
				else if ("," + num + "," == GlobalVariables.CaptainSubAccountTypeIDs)
				{
					e.Cell.Row.Cells["InvoiceSubAccountID"].Value = drMaster["CaptainSubAccountID"];
				}
			}
			else
			{
				e.Cell.Row.Cells["InvoiceSubAccountID"].ValueList = null;
				e.Cell.Row.Cells["InvoiceSubAccountID"].Value = DBNull.Value;
			}
		}
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "InvoiceSubAccountTypeID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "InvoiceSubAccountID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID") && (e.Cell.Column.ValueList.SelectedItemIndex >= 0 || e.Cell.ValueList.SelectedItemIndex >= 0) && e.Cell.Row.Cells["InvoiceSubAccountID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value != DBNull.Value)
		{
			if (dtServicesPrices == null || dtServicesPrices.Rows.Count == 0 || e.Cell.Row.Cells["InvoiceSubAccountID"].Value.ToString() != ((TextEditorControlBase)cboOwners).Value.ToString())
			{
				dtServicesPrices = ServicesPrices.GetPrice(e.Cell.Row.Cells["InvoiceSubAccountID"].Value.ToString(), ((TextEditorControlBase)cboOwners).Value.ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			}
			if (dtServicesPrices != null && dtServicesPrices.Rows.Count > 0)
			{
				if (dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["TaxID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["TaxID"].ToString();
				}
				else
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["IsExpensePercent"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["IsExpensePercent"];
				((UltraGridBase)ULGData).ActiveRow.Cells["IsCostPlus"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["IsCostPlus"];
				((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalFees"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["AdditionalFees"];
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = dtServicesPrices.Select(" ServiceID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["TaxValue"];
				if (bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsExpensePercent"].Value.ToString()))
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) + 100m) / 100m * (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalExpenses"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalFees"].Value.ToString()));
				}
				else if (bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsCostPlus"].Value.ToString()))
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalExpenses"].Value.ToString());
				}
				else
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				CalculateRowTax(((UltraGridBase)ULGData).ActiveRow);
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID")
		{
			CalculateRowTax(e.Cell.Row);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.BeforeCellUpdate += new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ServiceID")
		{
			int num = (Adding ? SearchFunctions.ServicesSearch(IsFromServer: false) : SearchFunctions.ServicesSearch(IsFromServer: false));
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = num;
			}
		}
		e.Handled = true;
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		for (int i = 0; i < e.Rows.Length; i++)
		{
			int num = int.Parse(e.Rows[i].Cells["OperationServiceID"].Value.ToString());
			DataRow dataRow = dtServices.Select(" ServiceID = " + e.Rows[i].Cells["ServiceID"].Value.ToString())[0];
			if (num == -1)
			{
				continue;
			}
			if (OperationsServicesStepsTasksExpenses.CheckIsCompleted(num.ToString()) > 0 || e.Rows[i].Cells["OperationInvoiceID"].Value != DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الخدمة لوجود مصروفات مسددة" : "Cannot Delete This Service Because Expenses Was Paid");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
			if (OperationsExpensesDetails.SelectByOperationServiceID(e.Rows[i].Cells["OperationServiceID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الخدمة لوجود مصروفات " : "Cannot Delete This Service Because it has Expenses");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
			if (dataRow["ServiceTypeID"] != DBNull.Value && (dataRow["ServiceTypeID"].ToString() == "12" || dataRow["ServiceTypeID"].ToString() == "13"))
			{
				string text = "";
				DataRow[] array = dtServices.Select(" ServiceTypeID in (15,16,17)");
				foreach (DataRow dataRow2 in array)
				{
					text = text + dataRow2["ServiceID"].ToString() + ",";
				}
				if (text != "")
				{
					string text2 = OperationsServicesCargos.CheckForRelations("-1", "," + e.Rows[i].Cells["OperationServiceID"].Value.ToString() + ",", GlobalVariables.IsArabic ? "1" : "0").Rows[0]["Relations"].ToString().Replace("-", "\n");
					if (text2 != "")
					{
						GlobalVariables.InformationMB.Show(text2, text2);
						e.DisplayPromptMsg = false;
						((CancelEventArgs)(object)e).Cancel = true;
						return;
					}
				}
			}
			else
			{
				if (dataRow["ServiceTypeID"] == DBNull.Value || !(dataRow["ServiceTypeID"].ToString() == "2"))
				{
					continue;
				}
				string text3 = "";
				DataRow[] array2 = dtServices.Select(" ServiceTypeID = 22");
				foreach (DataRow dataRow3 in array2)
				{
					text3 = text3 + dataRow3["ServiceID"].ToString() + ",";
				}
				if (text3 != "")
				{
					string text4 = OperationsServicesVisas.CheckForRelations("-1", "," + e.Rows[i].Cells["OperationServiceID"].Value.ToString() + ",", GlobalVariables.IsArabic ? "1" : "0").Rows[0]["Relations"].ToString().Replace("-", "\n");
					if (text4 != "")
					{
						GlobalVariables.InformationMB.Show(text4, text4);
						e.DisplayPromptMsg = false;
						((CancelEventArgs)(object)e).Cancel = true;
						return;
					}
				}
			}
		}
		base.ULGData_BeforeRowsDeleted(sender, e);
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Reports")
		{
			frmPROTasksReports frmPROTasksReports2 = new frmPROTasksReports("-1", e.Cell.Row.Cells["OperationServiceID"].Value.ToString(), e.Cell.Row.Cells["CompanyID"].Value.ToString());
			frmPROTasksReports2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmPROTasksReports2.lblTitle).Text = (GlobalVariables.IsArabic ? "خطابات المهمه" : "Task Letters");
			frmPROTasksReports2.ShowDialog();
		}
		else
		{
			if (drMaster == null || ((UltraGridBase)ULGData).ActiveRow == null || ULGData.ActiveCell == null || !(((KeyedSubObjectBase)e.Cell.Column).Key == "Details") || ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value == DBNull.Value)
			{
				return;
			}
			DataRow dataRow = dtServices.Select(" ServiceID = " + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0];
			if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "2")
			{
				frmOperationsServicesVisas frmOperationsServicesVisas2 = new frmOperationsServicesVisas(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), drMaster["VesselID"].ToString(), READONLY: false);
				frmOperationsServicesVisas2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisas2.lblTitle).Text = (GlobalVariables.IsArabic ? "تأشيرات" : "Visa");
				frmOperationsServicesVisas2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "3")
			{
				frmOperationsServicesCrew frmOperationsServicesCrew2 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: true, READONLY: false);
				frmOperationsServicesCrew2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
				frmOperationsServicesCrew2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "6")
			{
				frmOperationsServicesTickets frmOperationsServicesTickets2 = new frmOperationsServicesTickets(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesTickets2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز الطيران" : "Ticketings");
				frmOperationsServicesTickets2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "5")
			{
				frmOperationsServicesCustoms frmOperationsServicesCustoms2 = new frmOperationsServicesCustoms(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesCustoms2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCustoms2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص جمركى" : "Custom Clearence");
				frmOperationsServicesCustoms2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "7")
			{
				frmOperationsServicesTransfers frmOperationsServicesTransfers2 = new frmOperationsServicesTransfers(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), ((TextEditorControlBase)cboVesselName).Value.ToString(), READONLY: false);
				frmOperationsServicesTransfers2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTransfers2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل من باخرة الى باخرة" : "Vessel Transfer");
				frmOperationsServicesTransfers2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "8")
			{
				frmOperationsServicesEquipmentsAndVechiles frmOperationsServicesEquipmentsAndVechiles2 = new frmOperationsServicesEquipmentsAndVechiles(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: false);
				frmOperationsServicesEquipmentsAndVechiles2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesEquipmentsAndVechiles2.lblTitle).Text = (GlobalVariables.IsArabic ? "المعدات و المركبات" : "Equipments And Vechiles");
				frmOperationsServicesEquipmentsAndVechiles2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "9")
			{
				frmOperationsServicesTransportations frmOperationsServicesTransportations2 = new frmOperationsServicesTransportations(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesTransportations2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesTransportations2.lblTitle).Text = (GlobalVariables.IsArabic ? "توصيل" : "Transportations");
				frmOperationsServicesTransportations2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "10")
			{
				frmOperationsServicesHotelsBooking frmOperationsServicesHotelsBooking2 = new frmOperationsServicesHotelsBooking(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesHotelsBooking2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesHotelsBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز فندق" : "Hotel Booking");
				frmOperationsServicesHotelsBooking2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "11")
			{
				frmOperationsServicesCrew frmOperationsServicesCrew3 = new frmOperationsServicesCrew(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), ISSIGNON: false, READONLY: false);
				frmOperationsServicesCrew3.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCrew3.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Crew");
				frmOperationsServicesCrew3.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "12")
			{
				frmOperationsServicesCargosExport frmOperationsServicesCargosExport2 = new frmOperationsServicesCargosExport(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesCargosExport2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCargosExport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الصادرة" : "Export Cargos");
				frmOperationsServicesCargosExport2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "13")
			{
				frmOperationsServicesCargosImport frmOperationsServicesCargosImport2 = new frmOperationsServicesCargosImport(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesCargosImport2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesCargosImport2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحمولة الواردة" : "Import Cargos");
				frmOperationsServicesCargosImport2.ShowDialog();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "14")
			{
				frmOperationsServicesSupply frmOperationsServicesSupply2 = new frmOperationsServicesSupply(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["SupplierSubAccountID"].Value, READONLY: false);
				frmOperationsServicesSupply2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSupply2.lblTitle).Text = (GlobalVariables.IsArabic ? "المهمات" : "Supply");
				frmOperationsServicesSupply2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "15")
			{
				frmOperationsServicesChangeVessel frmOperationsServicesChangeVessel2 = new frmOperationsServicesChangeVessel(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesChangeVessel2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesChangeVessel2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الباخرة" : "Change Vessel");
				frmOperationsServicesChangeVessel2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "16")
			{
				frmOperationsServicesAgentCorrection frmOperationsServicesAgentCorrection2 = new frmOperationsServicesAgentCorrection(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesAgentCorrection2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgentCorrection2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير الوكيل والباخرة" : "Correction Of Agent & Vessel");
				frmOperationsServicesAgentCorrection2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "17")
			{
				frmOperationsServicesShipToShip frmOperationsServicesShipToShip2 = new frmOperationsServicesShipToShip(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesShipToShip2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesShipToShip2.lblTitle).Text = (GlobalVariables.IsArabic ? "من باخرة لباخرة" : "Ship To Ship");
				frmOperationsServicesShipToShip2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "18")
			{
				frmOperationsServicesSkipReloading frmOperationsServicesSkipReloading2 = new frmOperationsServicesSkipReloading(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesSkipReloading2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSkipReloading2.lblTitle).Text = (GlobalVariables.IsArabic ? "إعادة تحميل" : "Skip Reloading");
				frmOperationsServicesSkipReloading2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "19")
			{
				frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer2 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: true, READONLY: false);
				frmOperationsServicesAgencyTransfer2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyTransfer2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكيل" : "Agency Transfer From another Agent");
				frmOperationsServicesAgencyTransfer2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "20")
			{
				frmOperationsServicesAgencyTransfer frmOperationsServicesAgencyTransfer3 = new frmOperationsServicesAgencyTransfer(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), FROMAGENT: false, READONLY: false);
				frmOperationsServicesAgencyTransfer3.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyTransfer3.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل وكالة من وكالتنا" : "Agency Transfer To another Agent");
				frmOperationsServicesAgencyTransfer3.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "21")
			{
				frmOperationsServicesShortPass frmOperationsServicesShortPass2 = new frmOperationsServicesShortPass(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesShortPass2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesShortPass2.lblTitle).Text = (GlobalVariables.IsArabic ? "تصريح مؤقت" : "Short Pass");
				frmOperationsServicesShortPass2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "22")
			{
				frmOperationsServicesVisasCancellation frmOperationsServicesVisasCancellation2 = new frmOperationsServicesVisasCancellation(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), drMaster["VesselID"].ToString(), READONLY: false);
				frmOperationsServicesVisasCancellation2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasCancellation2.lblTitle).Text = (GlobalVariables.IsArabic ? "التإشيرات الملغاة" : "Visa Cancellation");
				frmOperationsServicesVisasCancellation2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "23")
			{
				frmOperationsServicesVisasSubmissions frmOperationsServicesVisasSubmissions2 = new frmOperationsServicesVisasSubmissions(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesVisasSubmissions2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasSubmissions2.lblTitle).Text = (GlobalVariables.IsArabic ? "تقديم التأشيرات" : "Visa Submissions");
				frmOperationsServicesVisasSubmissions2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "24")
			{
				frmOperationsServicesVisasClearingOverStay frmOperationsServicesVisasClearingOverStay2 = new frmOperationsServicesVisasClearingOverStay(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesVisasClearingOverStay2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasClearingOverStay2.lblTitle).Text = (GlobalVariables.IsArabic ? " تمديد اقامة" : "Visa Clearing Over Stay");
				frmOperationsServicesVisasClearingOverStay2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "25")
			{
				frmOperationsServicesSeaManBooking frmOperationsServicesSeaManBooking2 = new frmOperationsServicesSeaManBooking(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesSeaManBooking2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesSeaManBooking2.lblTitle).Text = (GlobalVariables.IsArabic ? "حجز للبحار" : "SeaMan Booking");
				frmOperationsServicesSeaManBooking2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "26")
			{
				frmOperationsServicesMedicalAssistance frmOperationsServicesMedicalAssistance2 = new frmOperationsServicesMedicalAssistance(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesMedicalAssistance2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesMedicalAssistance2.lblTitle).Text = (GlobalVariables.IsArabic ? "رعاية صحية" : "Medical Assistance");
				frmOperationsServicesMedicalAssistance2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && (dataRow["ServiceTypeID"].ToString() == "27" || dataRow["ServiceTypeID"].ToString() == "30" || dataRow["ServiceTypeID"].ToString() == "31"))
			{
				frmOperationsServicesPassengersClearance frmOperationsServicesPassengersClearance2 = new frmOperationsServicesPassengersClearance(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesPassengersClearance2.WindowState = FormWindowState.Maximized;
				if (dataRow["ServiceTypeID"].ToString() == "27")
				{
					((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص الركاب " : "Passengers Clearance");
				}
				else if (dataRow["ServiceTypeID"].ToString() == "30")
				{
					((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص دخول فنيين " : "Technicians Inward Clearance");
				}
				else if (dataRow["ServiceTypeID"].ToString() == "31")
				{
					((Control)(object)frmOperationsServicesPassengersClearance2.lblTitle).Text = (GlobalVariables.IsArabic ? "تخليص خروج فنيين " : "Technicians Outward Clearance");
				}
				frmOperationsServicesPassengersClearance2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "28")
			{
				frmOperationsServicesVisasRejection frmOperationsServicesVisasRejection2 = new frmOperationsServicesVisasRejection(dtServices, ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), drMaster["VesselID"].ToString(), READONLY: false);
				frmOperationsServicesVisasRejection2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesVisasRejection2.lblTitle).Text = (GlobalVariables.IsArabic ? "التأشيرات المرفوضة" : "Visa Rejection");
				frmOperationsServicesVisasRejection2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "29")
			{
				frmOperationsServicesAgencyFromDateToDate frmOperationsServicesAgencyFromDateToDate2 = new frmOperationsServicesAgencyFromDateToDate(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesAgencyFromDateToDate2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesAgencyFromDateToDate2.lblTitle).Text = (GlobalVariables.IsArabic ? "وكالة من تاريخ لتاريخ" : "Agency From Date To Date");
				frmOperationsServicesAgencyFromDateToDate2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			else if (dataRow["ServiceTypeID"] != DBNull.Value && dataRow["ServiceTypeID"].ToString() == "100")
			{
				frmOperationsServicesOthers frmOperationsServicesOthers2 = new frmOperationsServicesOthers(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), drMaster["OperationID"].ToString(), dataRow["ServiceID"].ToString(), READONLY: false);
				frmOperationsServicesOthers2.WindowState = FormWindowState.Maximized;
				((Control)(object)frmOperationsServicesOthers2.lblTitle).Text = (GlobalVariables.IsArabic ? "خدمات أخرى" : "Other Services");
				frmOperationsServicesOthers2.ShowDialog();
				dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
				InitGrid();
			}
			DataTable currentSeaMenCount = Operations.GetCurrentSeaMenCount(drMaster["OperationID"].ToString());
			((Control)(object)lblCurrentCrew).Text = currentSeaMenCount.Rows[0][0].ToString();
			((Control)(object)lblCurrentPassengerCount).Text = currentSeaMenCount.Rows[0][1].ToString();
		}
	}

	private void ULGDataSeaPortsReports_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataSeaPortsReports).ActiveRow).Selected = true;
	}

	private void ULGDataSeaPortsReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGDataSeaPortsReports).ActiveRow != null && !CanOpenSeaportLetter)
		{
			CanOpenSeaportLetter = true;
			frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
			frmCompanySelector2.WindowState = FormWindowState.Normal;
			frmCompanySelector2.ShowDialog();
			if (frmCompanySelector2.Cancel || frmCompanySelector2.CompanyID == 0)
			{
				CanOpenSeaportLetter = false;
				return;
			}
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtReports.Select("ReportID = " + ((UltraGridBase)ULGDataSeaPortsReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtReports.Select("ReportID = " + ((UltraGridBase)ULGDataSeaPortsReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", frmCompanySelector2.CompanyID);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenSeaportLetter = false;
		}
	}

	private void ULGServicesReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster != null && e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGServicesReports).ActiveRow != null && !CanOpenSeaportLetter)
		{
			CanOpenSeaportLetter = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtServicesReports.Select("ReportID = " + ((UltraGridBase)ULGServicesReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtServicesReports.Select("ReportID = " + ((UltraGridBase)ULGServicesReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenSeaportLetter = false;
		}
	}

	private void ULGDataExpenses_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataExpenses).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataExpenses).ActiveRow).Selected = true;
		}
	}

	private void ULGDataExpenses_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataExpenses.ActiveCell != null && ((KeyedSubObjectBase)ULGDataExpenses.ActiveCell.Column).Key == "NetPrice")
		{
			GlobalFunctions.CheckForNumbers(ULGDataExpenses.ActiveCell, e);
		}
	}

	private void ULGDataExpenses_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		ULGDataExpenses.CellListSelect -= new CellEventHandler(ULGDataExpenses_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			int num = ((vlOperationsExpensesAccounts.SelectedItem != null) ? int.Parse(dtAccounts.Rows[vlOperationsExpensesAccounts.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)getSubAccountValueListByAccountID(num);
			}
			else
			{
				e.Cell.Row.Cells["AccountID"].ValueList = null;
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
			}
		}
		ULGDataExpenses.CellListSelect += new CellEventHandler(ULGDataExpenses_CellListSelect);
	}

	private void ULGDataExpenses_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGDataExpenses.ActiveCell.Column).Key == "ExpenseID")
		{
			int num = SearchFunctions.MS_ExpensesSearch(IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGDataExpenses).ActiveRow.Cells["ExpenseID"].Value = num;
			}
		}
		e.Handled = true;
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

	private void ULGDataInvoices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataInvoices).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataInvoices).ActiveRow).Selected = true;
		}
	}

	private void ULGDataOrders_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataOrders).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataOrders).ActiveRow).Selected = true;
		}
	}

	private void ULGDataOrders_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGDataOrders).ActiveRow != null && ULGDataOrders.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGDataOrders).ActiveRow.Cells["PSOrderID"].Value != DBNull.Value && !CanOpenPSOrders)
		{
			CanOpenPSOrders = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PS_PSOrders_A.rpt" : "Rep_PS_PSOrders_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PSOrderIDs", "," + ((UltraGridBase)ULGDataOrders).ActiveRow.Cells["PSOrderID"].Value.ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenPSOrders = false;
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.BeforeCellUpdate -= new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		e.Row.Cells["User_ID"].Value = GlobalVariables.UserID;
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 1)
		{
			e.Row.Cells["companyID"].Value = ((UltraGridBase)ULGData).Rows[0].Cells["companyID"].Value;
			e.Row.Cells["OperationServiceNo"].Value = Convert.ToInt32(((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["OperationServiceNo"].Value) + 1;
		}
		else
		{
			e.Row.Cells["OperationServiceNo"].Value = 1;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.BeforeCellUpdate += new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
	}

	private void ULGDataMaterialIssueVoucher_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataMaterialIssueVoucher).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataMaterialIssueVoucher).ActiveRow).Selected = true;
		}
	}

	private void ULGDataMaterialIssueVoucher_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGDataMaterialIssueVoucher).ActiveRow != null && ULGDataMaterialIssueVoucher.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGDataMaterialIssueVoucher).ActiveRow.Cells["MaterialIssueVoucherID"].Value != DBNull.Value && !CanOpenPSOrders)
		{
			CanOpenPSOrders = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SC_MaterialIssueVouchers_A.rpt" : "Rep_SC_MaterialIssueVouchers_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@MaterialIssueVoucherIDs", "," + ((UltraGridBase)ULGDataMaterialIssueVoucher).ActiveRow.Cells["MaterialIssueVoucherID"].Value.ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenPSOrders = false;
		}
	}

	private void ULGDataTasks_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataTasks).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataTasks).ActiveRow).Selected = true;
		}
	}

	private void ULGDataRemarks_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDataRemarks).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataRemarks.ActiveCell.Column).Key == "User_ID")
		{
			((GridItemBase)((UltraGridBase)ULGDataRemarks).ActiveRow).Selected = true;
		}
	}

	private void ULGDataSeaMen_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataSeaMen_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDataSeaMen).ActiveRow).Selected = true;
		}
	}

	private void ULGDataItems_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblItemsCounterResult).Text = ((UltraGridBase)ULGDataItems).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGDataItems_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblItemsCounterResult).Text = ((UltraGridBase)ULGDataItems).Rows.GetFilteredInNonGroupByRows().Length.ToString();
		int num = int.Parse(GlobalVariables.OwnerSubAccountTypeIDs.Replace(",", "").Trim());
		if (num != 0)
		{
			e.Row.Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			return;
		}
		e.Row.Cells["InvoiceSubAccountID"].ValueList = null;
		e.Row.Cells["InvoiceSubAccountID"].Value = DBNull.Value;
	}

	private void ULGDataItems_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblItemsCounterResult).Text = ((UltraGridBase)ULGDataItems).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGServicesReports_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGServicesReports).ActiveRow).Selected = true;
	}

	private void ULGDataInvoices_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster == null || e.Cell == null || !(((KeyedSubObjectBase)e.Cell.Column).Key == "Print") || ((UltraGridBase)ULGDataInvoices).ActiveRow == null)
		{
			return;
		}
		GlobalVariables.ReportDocument = new ReportDocument();
		if (bool.Parse(e.Cell.Row.Cells["IsServiceInvoice"].Value.ToString()))
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Services_A.rpt" : "Rep_MS_OperationsInvoices_Services_E.rpt"));
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Items_A.rpt" : "Rep_MS_OperationsInvoices_Items_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@OperationInvoiceIDs", "," + e.Cell.Row.Cells["OperationInvoiceID"].Value.ToString() + ",");
		DataRow[] array = ((DataTable)((UltraGridBase)ULGData).DataSource).Select(" OperationInvoiceID = " + e.Cell.Row.Cells["OperationInvoiceID"].Value.ToString());
		if (bool.Parse(e.Cell.Row.Cells["IsServiceInvoice"].Value.ToString()))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@WithTariff", "0");
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", array[0]["CompanyID"].ToString());
		}
		else
		{
			frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
			frmCompanySelector2.WindowState = FormWindowState.Normal;
			frmCompanySelector2.ShowDialog();
			if (frmCompanySelector2.Cancel || frmCompanySelector2.CompanyID == 0)
			{
				return;
			}
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", frmCompanySelector2.CompanyID);
		}
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
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

	private ValueList getSubAccountValueList(int SubAccountTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		string text = "";
		text = ((!("," + SubAccountTypeID + "," == GlobalVariables.CharterSubAccountTypeIDs)) ? ("SubAccountTypeID=" + SubAccountTypeID) : ("SubAccountTypeID in(" + SubAccountTypeID + GlobalVariables.OwnerSubAccountTypeIDs.Remove(GlobalVariables.OwnerSubAccountTypeIDs.Length - 1) + ")"));
		DataRow[] array = dtSubAccounts.Select(text);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
	}

	private ValueList getSubAccountValueListByAccountID(int AccountID)
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
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void UpdateServiceDetailsButtons()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.BeforeCellUpdate -= new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		if (!Adding && !Updating && !base.DesignMode)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Hidden = false;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value != DBNull.Value && bool.Parse(dtServices.Select(" ServiceID = " + ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString())[0]["HasDetails"].ToString()))
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Details"].Value = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
				}
				((UltraGridBase)ULGData).Rows[i].Cells["Reports"].Value = (GlobalVariables.IsArabic ? "الخطابات" : "Letters");
			}
		}
		else
		{
			if (((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Details"))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Hidden = true;
			}
			if (((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Reports"))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Reports"].Hidden = true;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.BeforeCellUpdate += new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
	}

	private void PrepareTabs()
	{
		((UltraTabControlBase)UTCDetails).Tabs["Details"].Text = (GlobalVariables.IsArabic ? "الخدمات" : "Services");
		((UltraTabControlBase)UTCDetails).Tabs["VoyageNo"].VisibleIndex = 0;
		((UltraTabControlBase)UTCDetails).Tabs["Details"].VisibleIndex = 1;
		((UltraTabControlBase)UTCDetails).Tabs["VoyageNo"].Selected = true;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraTabControlBase)UTCDetails).Tabs).Count; i++)
		{
			((UltraTabControlBase)UTCDetails).Tabs[i].FixedWidth = ((Control)(object)UTCDetails).Width / ((DisposableObjectCollectionBase)((UltraTabControlBase)UTCDetails).Tabs).Count;
			((UltraTabControlBase)UTCDetails).Tabs[i].Appearance.FontData.Bold = (DefaultableBoolean)1;
			((UltraTabControlBase)UTCDetails).Tabs[i].Appearance.FontData.SizeInPoints = 10f;
			((UltraTabControlBase)UTCDetails).TabButtonStyle = (UIElementButtonStyle)10;
		}
		((UltraTabControlBase)UTCDetails).VisibleTabs[0].Visible = Tap1;
		((UltraTabControlBase)UTCDetails).VisibleTabs[1].Visible = Tap2;
		((UltraTabControlBase)UTCDetails).VisibleTabs[2].Visible = Tap3;
		((UltraTabControlBase)UTCDetails).VisibleTabs[3].Visible = Tap4;
		((UltraTabControlBase)UTCDetails).VisibleTabs[4].Visible = Tap5;
		((UltraTabControlBase)UTCDetails).VisibleTabs[5].Visible = Tap6;
		((UltraTabControlBase)UTCDetails).VisibleTabs[6].Visible = Tap7;
		((UltraTabControlBase)UTCDetails).VisibleTabs[7].Visible = Tap8;
		((UltraTabControlBase)UTCDetails).VisibleTabs[13].Visible = Tap9;
		((UltraTabControlBase)UTCDetails).VisibleTabs["Remarks"].Selected = true;
	}

	private void btnInvoices_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmOperationInvoices frmOperationInvoices2 = new frmOperationInvoices(drMaster["OperationID"].ToString());
			frmOperationInvoices2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmOperationInvoices2.lblTitle).Text = (GlobalVariables.IsArabic ? "فواتير العمليات" : "Operation Invoices");
			frmOperationInvoices2.ShowDialog();
			dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			dtOperationsItems = OperationsItems.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataItems).DataSource = dtOperationsItems;
			InitGridItems();
			dtOperationsInvoices = OperationsInvoices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataInvoices).DataSource = dtOperationsInvoices;
			InitGridInvoices();
		}
	}

	private void btnCreateOrders_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmGeneratePSOrder frmGeneratePSOrder2 = new frmGeneratePSOrder(drMaster["OperationID"].ToString());
			frmGeneratePSOrder2.WindowState = FormWindowState.Maximized;
			frmGeneratePSOrder2.ShowDialog();
			dtOperationsPSOrders = OperationsPSOrders.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataOrders).DataSource = dtOperationsPSOrders;
			InitGridPSOrders();
		}
	}

	private void btnCreateMIV_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmMaterialIssueVouchers frmMaterialIssueVouchers2 = new frmMaterialIssueVouchers(drMaster["OperationID"].ToString(), ((TextEditorControlBase)cboOwners).Value.ToString());
			frmMaterialIssueVouchers2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmMaterialIssueVouchers2.lblTitle).Text = (GlobalVariables.IsArabic ? "صرف مخازن" : "Material Issue Vouchers");
			frmMaterialIssueVouchers2.ShowDialog();
			dtOperationsItems = OperationsItems.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataItems).DataSource = dtOperationsItems;
			InitGridItems();
			dtOperationsMaterialIssueVoucher = OperationsMaterialIssueVouchers.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataMaterialIssueVoucher).DataSource = dtOperationsMaterialIssueVoucher;
			InitGridMaterialIssueVoucher();
		}
	}

	private void btnPrintExportManifest_Click(object sender, EventArgs e)
	{
		frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
		frmCompanySelector2.WindowState = FormWindowState.Normal;
		frmCompanySelector2.ShowDialog();
		if (!frmCompanySelector2.Cancel && frmCompanySelector2.CompanyID != 0)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_Manifest_Op.rpt" : "Rep_MS_Manifest_Op.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", frmCompanySelector2.CompanyID);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void btnPrintImportManifest_Click(object sender, EventArgs e)
	{
		frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
		frmCompanySelector2.WindowState = FormWindowState.Normal;
		frmCompanySelector2.ShowDialog();
		if (!frmCompanySelector2.Cancel && frmCompanySelector2.CompanyID != 0)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_CargoManifest_Op.rpt" : "Rep_MS_CargoManifest_Op.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", frmCompanySelector2.CompanyID);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void btnPrintEmptyExportManifest_Click(object sender, EventArgs e)
	{
		frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
		frmCompanySelector2.WindowState = FormWindowState.Normal;
		frmCompanySelector2.ShowDialog();
		if (!frmCompanySelector2.Cancel && frmCompanySelector2.CompanyID != 0)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_EmptyManifest.rpt" : "Rep_MS_EmptyManifest.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", frmCompanySelector2.CompanyID);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void txtSeaMenCount_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnCreateExpense_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmOperationExpenses frmOperationExpenses2 = new frmOperationExpenses(drMaster["OperationID"].ToString());
			frmOperationExpenses2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmOperationExpenses2.lblTitle).Text = (GlobalVariables.IsArabic ? "مصروفات العمليات" : "Operation Expenses");
			frmOperationExpenses2.ShowDialog();
			dtOperationsExpenses = OperationsExpenses.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationsExpenses;
			InitGridExpenses();
		}
	}

	private void btnCostCenterSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboCostCenter).Value = SearchFunctions.CostCenter(IsFromServer: false);
	}

	private void cboCostCenter_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.CostCenter(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostCenter).Value = num;
			}
		}
	}

	private void chkItemsSubAccount_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboItemsSubAccountTypeID).ReadOnly = !((UltraToggleEditorBase)chkItemsSubAccount).Checked;
		((EditorButtonControlBase)cboItemsSubAccountID).ReadOnly = !((UltraToggleEditorBase)chkItemsSubAccount).Checked;
		if (!((UltraToggleEditorBase)chkItemsSubAccount).Checked)
		{
			((TextEditorControlBase)cboItemsSubAccountTypeID).Value = DBNull.Value;
			((TextEditorControlBase)cboItemsSubAccountID).Value = DBNull.Value;
		}
	}

	private void cboItemsSubAccountTypeID_ValueChanged(object sender, EventArgs e)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		if (cboItemsSubAccountTypeID.SelectedIndex <= -1)
		{
			return;
		}
		DataView dataView = new DataView(dtSubAccounts);
		dataView.RowFilter = "SubAccountTypeID =" + ((TextEditorControlBase)cboItemsSubAccountTypeID).Value.ToString();
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		cboItemsSubAccountID.DataSource = dataView;
		cboItemsSubAccountID.DisplayMember = "Name";
		cboItemsSubAccountID.ValueMember = "SubAccountID";
		ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationInvoiceID"].Value == DBNull.Value && decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["DeliverdQty"].Value.ToString()) == 0m)
			{
				((UltraGridBase)ULGDataItems).Rows[i].Cells["InvoiceSubAccountTypeID"].Value = ((TextEditorControlBase)cboItemsSubAccountTypeID).Value;
				((UltraGridBase)ULGDataItems).Rows[i].Cells["InvoiceSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(int.Parse(((TextEditorControlBase)cboItemsSubAccountTypeID).Value.ToString()));
				((UltraGridBase)ULGDataItems).Rows[i].Cells["InvoiceSubAccountID"].Value = DBNull.Value;
			}
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountTypeID"].DefaultCellValue = ((TextEditorControlBase)cboItemsSubAccountTypeID).Value;
		ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
	}

	private void cboItemsSubAccountID_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		if (cboItemsSubAccountID.SelectedIndex <= -1)
		{
			return;
		}
		ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataItems).Rows[i].Cells["OperationInvoiceID"].Value == DBNull.Value && decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["DeliverdQty"].Value.ToString()) == 0m)
			{
				((UltraGridBase)ULGDataItems).Rows[i].Cells["InvoiceSubAccountID"].Value = ((TextEditorControlBase)cboItemsSubAccountID).Value;
			}
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InvoiceSubAccountID"].DefaultCellValue = ((TextEditorControlBase)cboItemsSubAccountID).Value;
		ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
	}

	private void btnGenerateExpManifestNo_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtExportManifestNo).Text.Trim().Equals(""))
		{
			((Control)(object)txtExportManifestNo).Text = Operations.ExportManifestNoGetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnGenerateImportManifestNo_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtImportManifestNo).Text.Trim().Equals(""))
		{
			((Control)(object)txtImportManifestNo).Text = Operations.ImportManifestNoGetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void chkIsActualDeparture_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpActualDepartureDate).Enabled = ((UltraToggleEditorBase)chkIsActualDeparture).Checked;
		if (((UltraToggleEditorBase)chkIsActualDeparture).Checked)
		{
			dtpActualDepartureDate.Value = DateTime.Now;
		}
	}

	private void btnChangeVessel_Click(object sender, EventArgs e)
	{
		if (drMaster == null)
		{
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataInvoices).Rows).Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن تغيير الباخرة لوجود فواتير على العملية" : "Vessel Cannot Change Delete Invoices Before Change");
			return;
		}
		frmChangeVessel frmChangeVessel2 = new frmChangeVessel(dtVessels, ((TextEditorControlBase)cboVesselName).Value.ToString());
		frmChangeVessel2.ShowDialog();
		string newVesselID = frmChangeVessel2.NewVesselID;
		if (newVesselID != "" && drMaster["VesselID"].ToString() != frmChangeVessel2.NewVesselID)
		{
			Operations.UpdateVessel(drMaster["OperationID"].ToString(), GlobalVariables.UserID, newVesselID);
			FillData();
		}
	}

	private void btnChangeCostCenter_Click(object sender, EventArgs e)
	{
		if (drMaster == null)
		{
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataInvoices).Rows).Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن تغيير مركز التكلفة لوجود فواتير على العملية" : "Cost Center Cannot Change Delete Invoices Before Change");
			return;
		}
		frmChangeCostCenter frmChangeCostCenter2 = new frmChangeCostCenter(dtCostCenters, ((TextEditorControlBase)cboCostCenter).Value.ToString());
		frmChangeCostCenter2.ShowDialog();
		string newCostCenterID = frmChangeCostCenter2.NewCostCenterID;
		if (newCostCenterID != "" && drMaster["CostCenterID"].ToString() != frmChangeCostCenter2.NewCostCenterID)
		{
			Operations.UpdateCostCenter(drMaster["OperationID"].ToString(), GlobalVariables.UserID, newCostCenterID);
			FillData();
		}
	}

	private void btnOpenOperationsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.OperationsAlertSearchReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationID"].ToString();
			FillData();
		}
	}

	private void CalculateRowTax(UltraGridRow Row)
	{
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(Row.Cells["TotalPrice"].Value.ToString());
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
	}

	private void btnCreateShipChandler_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmShipChandler frmShipChandler2 = new frmShipChandler(drMaster["OperationID"].ToString());
			frmShipChandler2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmShipChandler2.lblTitle).Text = (GlobalVariables.IsArabic ? "ShipChandler" : "ShipChandler");
			frmShipChandler2.ShowDialog();
			dtOperationsItems = OperationsItems.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataItems).DataSource = dtOperationsItems;
			InitGridItems();
			dtShipChandler = ShipChandler.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
			InitGridShipChandler();
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (ShipChandler.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود صرف بضائع عليها", "Cannot Delete This Transaction Because there Are ShipChandler on it. ");
			return;
		}
		if (ShipChandlerReturns.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود مرتجع عليها", "Cannot Delete This Transaction Because there Are Return on it. ");
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
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Expected O, but got Unknown
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Expected O, but got Unknown
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Expected O, but got Unknown
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Expected O, but got Unknown
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Expected O, but got Unknown
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Expected O, but got Unknown
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Expected O, but got Unknown
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Expected O, but got Unknown
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Expected O, but got Unknown
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Expected O, but got Unknown
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Expected O, but got Unknown
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Expected O, but got Unknown
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Expected O, but got Unknown
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Expected O, but got Unknown
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Expected O, but got Unknown
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected O, but got Unknown
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Expected O, but got Unknown
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Expected O, but got Unknown
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected O, but got Unknown
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Expected O, but got Unknown
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Expected O, but got Unknown
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected O, but got Unknown
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Expected O, but got Unknown
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Expected O, but got Unknown
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Expected O, but got Unknown
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Expected O, but got Unknown
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Expected O, but got Unknown
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Expected O, but got Unknown
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected O, but got Unknown
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Expected O, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Expected O, but got Unknown
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Expected O, but got Unknown
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Expected O, but got Unknown
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Expected O, but got Unknown
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Expected O, but got Unknown
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Expected O, but got Unknown
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Expected O, but got Unknown
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Expected O, but got Unknown
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Expected O, but got Unknown
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Expected O, but got Unknown
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Expected O, but got Unknown
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Expected O, but got Unknown
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Expected O, but got Unknown
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Expected O, but got Unknown
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Expected O, but got Unknown
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Expected O, but got Unknown
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Expected O, but got Unknown
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Expected O, but got Unknown
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Expected O, but got Unknown
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Expected O, but got Unknown
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Expected O, but got Unknown
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Expected O, but got Unknown
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Expected O, but got Unknown
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Expected O, but got Unknown
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Expected O, but got Unknown
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Expected O, but got Unknown
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Expected O, but got Unknown
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Expected O, but got Unknown
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Expected O, but got Unknown
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Expected O, but got Unknown
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Expected O, but got Unknown
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Expected O, but got Unknown
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Expected O, but got Unknown
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Expected O, but got Unknown
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Expected O, but got Unknown
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Expected O, but got Unknown
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Expected O, but got Unknown
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Expected O, but got Unknown
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Expected O, but got Unknown
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Expected O, but got Unknown
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Expected O, but got Unknown
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Expected O, but got Unknown
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Expected O, but got Unknown
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Expected O, but got Unknown
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Expected O, but got Unknown
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Expected O, but got Unknown
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Expected O, but got Unknown
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Expected O, but got Unknown
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Expected O, but got Unknown
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Expected O, but got Unknown
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Expected O, but got Unknown
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Expected O, but got Unknown
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Expected O, but got Unknown
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Expected O, but got Unknown
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Expected O, but got Unknown
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Expected O, but got Unknown
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Expected O, but got Unknown
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Expected O, but got Unknown
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Expected O, but got Unknown
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Expected O, but got Unknown
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Expected O, but got Unknown
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Expected O, but got Unknown
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Expected O, but got Unknown
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Expected O, but got Unknown
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Expected O, but got Unknown
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Expected O, but got Unknown
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Expected O, but got Unknown
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Expected O, but got Unknown
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Expected O, but got Unknown
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f9: Expected O, but got Unknown
		//IL_1507: Unknown result type (might be due to invalid IL or missing references)
		//IL_1511: Expected O, but got Unknown
		//IL_151f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1529: Expected O, but got Unknown
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_1541: Expected O, but got Unknown
		//IL_154f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1559: Expected O, but got Unknown
		//IL_1bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be1: Expected O, but got Unknown
		//IL_1c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c29: Expected O, but got Unknown
		//IL_1c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c41: Expected O, but got Unknown
		//IL_1c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c59: Expected O, but got Unknown
		//IL_1c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c71: Expected O, but got Unknown
		//IL_24ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b7: Expected O, but got Unknown
		//IL_2889: Unknown result type (might be due to invalid IL or missing references)
		//IL_2893: Expected O, but got Unknown
		//IL_28a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ab: Expected O, but got Unknown
		//IL_2cad: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb7: Expected O, but got Unknown
		//IL_4835: Unknown result type (might be due to invalid IL or missing references)
		//IL_483f: Expected O, but got Unknown
		//IL_4c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c1b: Expected O, but got Unknown
		//IL_5636: Unknown result type (might be due to invalid IL or missing references)
		//IL_5640: Expected O, but got Unknown
		//IL_597e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5988: Expected O, but got Unknown
		//IL_5d5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d64: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperations));
		Appearance val = new Appearance();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
		UltraTab val4 = new UltraTab();
		UltraTab val5 = new UltraTab();
		UltraTab val6 = new UltraTab();
		UltraTab val7 = new UltraTab();
		Appearance val8 = new Appearance();
		UltraTab val9 = new UltraTab();
		UltraTab val10 = new UltraTab();
		UltraTab val11 = new UltraTab();
		UltraTab val12 = new UltraTab();
		UltraTab val13 = new UltraTab();
		UltraTab val14 = new UltraTab();
		UltraTab val15 = new UltraTab();
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
		Appearance val74 = new Appearance();
		Appearance val75 = new Appearance();
		Appearance val76 = new Appearance();
		Appearance val77 = new Appearance();
		Appearance val78 = new Appearance();
		Appearance val79 = new Appearance();
		Appearance val80 = new Appearance();
		Appearance val81 = new Appearance();
		Appearance val82 = new Appearance();
		Appearance val83 = new Appearance();
		Appearance val84 = new Appearance();
		Appearance val85 = new Appearance();
		Appearance val86 = new Appearance();
		Appearance val87 = new Appearance();
		Appearance val88 = new Appearance();
		Appearance val89 = new Appearance();
		Appearance val90 = new Appearance();
		Appearance val91 = new Appearance();
		Appearance val92 = new Appearance();
		Appearance val93 = new Appearance();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.lblItemsCounterResult = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.cboItemsSubAccountID = new UltraComboEditor();
		this.ULGDataItems = new UltraGrid();
		this.chkItemsSubAccount = new UltraCheckEditor();
		this.cboItemsSubAccountTypeID = new UltraComboEditor();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.btnGenerateImportManifestNo = new UltraButton();
		this.btnPrintImportManifest = new UltraButton();
		this.btnPrintEmptyManifest = new UltraButton();
		this.btnGenerateExpManifestNo = new UltraButton();
		this.btnPrintExportManifest = new UltraButton();
		this.lblImportManifestDate = new UltraLabel();
		this.dtpImportManifestDate = new UltraDateTimeEditor();
		this.lblImportManifestNo = new UltraLabel();
		this.txtImportManifestNo = new UltraTextEditor();
		this.lblExportManifestDate = new UltraLabel();
		this.dtpExportManifestDate = new UltraDateTimeEditor();
		this.lblExportManifestNo = new UltraLabel();
		this.txtExportManifestNo = new UltraTextEditor();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.ULGDataSeaPortsReports = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnCreateExpense = new UltraButton();
		this.ULGDataExpenses = new UltraGrid();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.btnCreateInvoices = new UltraButton();
		this.ULGDataInvoices = new UltraGrid();
		this.ultraTabPageControl7 = new UltraTabPageControl();
		this.btnChangeCostCenter = new UltraButton();
		this.chkIsActualDeparture = new UltraCheckEditor();
		this.dtpActualDepartureDate = new UltraDateTimeEditor();
		this.btnCostCenterSearch = new UltraButton();
		this.cboCostCenter = new UltraComboEditor();
		this.lblCostCenter = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.txtDepartureDraftAft = new UltraTextEditor();
		this.txtDepartureDraftFwd = new UltraTextEditor();
		this.lblArrivalDraftFwd = new UltraLabel();
		this.lblDepartureDraft = new UltraLabel();
		this.lblArrivalDraftAft = new UltraLabel();
		this.txtArrivalDraftAft = new UltraTextEditor();
		this.lblArrivalDraft = new UltraLabel();
		this.txtArrivalDraftFwd = new UltraTextEditor();
		this.txtEntryReason = new UltraTextEditor();
		this.txtExtendReason = new UltraTextEditor();
		this.lblExtendReason = new UltraLabel();
		this.txtRemainingFuel = new UltraTextEditor();
		this.lblRemainingFuel = new UltraLabel();
		this.lblWivesChildreCount = new UltraLabel();
		this.txtWivesChildrenCount = new UltraTextEditor();
		this.lblSeaMenCount = new UltraLabel();
		this.txtSeaMenCount = new UltraTextEditor();
		this.lblQuayNo = new UltraLabel();
		this.txtQuayNo = new UltraTextEditor();
		this.lblCaptain = new UltraLabel();
		this.btnToSeaPortSearch = new UltraButton();
		this.btnEntrySeaPortSearch = new UltraButton();
		this.btnFromSeaPortSearch = new UltraButton();
		this.lblToSeaPort = new UltraLabel();
		this.cboToSeaPort = new UltraComboEditor();
		this.lblFromSeaPort = new UltraLabel();
		this.cboFromSeaPort = new UltraComboEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblExchangeRate = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		this.cboServiceQuotation = new UltraComboEditor();
		this.lblServiceQuotation = new UltraLabel();
		this.cboItemQuotation = new UltraComboEditor();
		this.lblItemQuotation = new UltraLabel();
		this.lblPassengersCount = new UltraLabel();
		this.txtPassengersCount = new UltraTextEditor();
		this.cboOwners = new UltraComboEditor();
		this.lblOwner = new UltraLabel();
		this.cboCharterers = new UltraComboEditor();
		this.lblCharter = new UltraLabel();
		this.cboCaptain = new UltraComboEditor();
		this.btnOwnerSearch = new UltraButton();
		this.btnCharterer = new UltraButton();
		this.btnCaptain = new UltraButton();
		this.btnItemQuotationSearch = new UltraButton();
		this.btnServiceQuotationSearch = new UltraButton();
		this.dtpEntryDate = new UltraDateTimeEditor();
		this.lblEntryDate = new UltraLabel();
		this.lblEntryLoad = new UltraLabel();
		this.txtEntryLoad = new UltraTextEditor();
		this.cboLoadType = new UltraComboEditor();
		this.lblLoadType = new UltraLabel();
		this.lblEntryReason = new UltraLabel();
		this.txtDepartureLoad = new UltraTextEditor();
		this.cboEntrySeaPort = new UltraComboEditor();
		this.lblDepartureLoad = new UltraLabel();
		this.lblEntrySeaPort = new UltraLabel();
		this.lblDepartureDate = new UltraLabel();
		this.dtpDepartureDate = new UltraDateTimeEditor();
		this.ultraTabPageControl8 = new UltraTabPageControl();
		this.ULGDataOrders = new UltraGrid();
		this.btnCreateOrders = new UltraButton();
		this.ultraTabPageControl12 = new UltraTabPageControl();
		this.ULGDataMaterialIssueVoucher = new UltraGrid();
		this.btnCreateMIV = new UltraButton();
		this.ultraTabPageControl9 = new UltraTabPageControl();
		this.ULGDataTasks = new UltraGrid();
		this.ultraTabPageControl10 = new UltraTabPageControl();
		this.ULGDataRemarks = new UltraGrid();
		this.ultraTabPageControl11 = new UltraTabPageControl();
		this.ULGDataSeaMen = new UltraGrid();
		this.ultraTabPageControl13 = new UltraTabPageControl();
		this.ULGServicesReports = new UltraGrid();
		this.ultraTabPageControl14 = new UltraTabPageControl();
		this.btnCreateShipChandler = new UltraButton();
		this.ULGShipChandler = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboVesselName = new UltraComboEditor();
		this.lblVesselName = new UltraLabel();
		this.btnVesselSearch = new UltraButton();
		this.lblVoyageNo = new UltraLabel();
		this.txtVoyageNo = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.lblCurrentCrew = new UltraLabel();
		this.lblCurrentPassenger = new UltraLabel();
		this.lblCurrentPassengerCount = new UltraLabel();
		this.btnChangeVessel = new UltraButton();
		this.btnOpenOperationsSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboItemsSubAccountID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkItemsSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemsSubAccountTypeID).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpImportManifestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtImportManifestNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExportManifestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExportManifestNo).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataSeaPortsReports).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataInvoices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkIsActualDeparture).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualDepartureDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureDraftAft).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureDraftFwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArrivalDraftAft).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArrivalDraftFwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemainingFuel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWivesChildrenCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMenCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuayNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboToSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFromSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboServiceQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassengersCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCharterers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCaptain).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryLoad).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureLoad).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEntrySeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDepartureDate).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataOrders).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl12).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataMaterialIssueVoucher).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataTasks).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataRemarks).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl11).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataSeaMen).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl13).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGServicesReports).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl14).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandler).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyageNo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		resources.ApplyResources(val, "appearance1");
		((AppearanceBase)val).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)base.UTCDetails).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl7);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl8);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl9);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl10);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl11);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl12);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl13);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl14);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val2).Key = "Items";
		val2.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val2, "ultraTab3");
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "Manifest";
		val3.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val3, "ultraTab2");
		((SubObjectBase)val3).ForceApplyResources = "";
		((KeyedSubObjectBase)val4).Key = "PortReports";
		val4.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val4, "ultraTab5");
		((SubObjectBase)val4).ForceApplyResources = "";
		((KeyedSubObjectBase)val5).Key = "Expenses";
		val5.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val5, "ultraTab1");
		((SubObjectBase)val5).ForceApplyResources = "";
		((KeyedSubObjectBase)val6).Key = "Invoices";
		val6.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val6, "ultraTab4");
		((SubObjectBase)val6).ForceApplyResources = "";
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance71");
		val7.Appearance = (AppearanceBase)(object)val8;
		((KeyedSubObjectBase)val7).Key = "VoyageNo";
		val7.TabPage = this.ultraTabPageControl7;
		resources.ApplyResources(val7, "ultraTab6");
		((SubObjectBase)val7).ForceApplyResources = "";
		((KeyedSubObjectBase)val9).Key = "Orders";
		val9.TabPage = this.ultraTabPageControl8;
		resources.ApplyResources(val9, "ultraTab7");
		((SubObjectBase)val9).ForceApplyResources = "";
		((KeyedSubObjectBase)val10).Key = "IssueVoucher";
		val10.TabPage = this.ultraTabPageControl12;
		resources.ApplyResources(val10, "ultraTab11");
		val10.Visible = false;
		((SubObjectBase)val10).ForceApplyResources = "";
		((KeyedSubObjectBase)val11).Key = "Tasks";
		val11.TabPage = this.ultraTabPageControl9;
		resources.ApplyResources(val11, "ultraTab8");
		((SubObjectBase)val11).ForceApplyResources = "";
		((KeyedSubObjectBase)val12).Key = "Remarks";
		val12.TabPage = this.ultraTabPageControl10;
		resources.ApplyResources(val12, "ultraTab9");
		((SubObjectBase)val12).ForceApplyResources = "";
		((KeyedSubObjectBase)val13).Key = "SeaMen";
		val13.TabPage = this.ultraTabPageControl11;
		resources.ApplyResources(val13, "ultraTab10");
		((SubObjectBase)val13).ForceApplyResources = "";
		((KeyedSubObjectBase)val14).Key = "Reports";
		val14.TabPage = this.ultraTabPageControl13;
		resources.ApplyResources(val14, "ultraTab12");
		((SubObjectBase)val14).ForceApplyResources = "";
		((KeyedSubObjectBase)val15).Key = "ShipChandler";
		val15.TabPage = this.ultraTabPageControl14;
		resources.ApplyResources(val15, "ultraTab13");
		((SubObjectBase)val15).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[13]
		{
			val2, val3, val4, val5, val6, val7, val9, val10, val11, val12,
			val13, val14, val15
		});
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl14, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl13, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl12, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl11, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl10, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl9, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl8, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl7, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl6, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl5, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val16, "appearance72");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val16;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val17).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val17, "appearance73");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance74");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val19, "appearance75");
		((AppearanceBase)val19).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val20).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val20).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val20).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val20, "appearance76");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val21).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val21, "appearance77");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val22, "appearance78");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val22;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		base.ULGData.BeforeCellUpdate += new BeforeCellUpdateEventHandler(ULGData_BeforeCellUpdate);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
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
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblItemsCounterResult);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboItemsSubAccountID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkItemsSubAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboItemsSubAccountTypeID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.lblItemsCounterResult, "lblItemsCounterResult");
		this.lblItemsCounterResult.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsCounterResult).Name = "lblItemsCounterResult";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.cboItemsSubAccountID, "cboItemsSubAccountID");
		((TextEditorControlBase)this.cboItemsSubAccountID).AlwaysInEditMode = true;
		this.cboItemsSubAccountID.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemsSubAccountID).Name = "cboItemsSubAccountID";
		((EditorButtonControlBase)this.cboItemsSubAccountID).ReadOnly = true;
		((TextEditorControlBase)this.cboItemsSubAccountID).ValueChanged += new System.EventHandler(cboItemsSubAccountID_ValueChanged);
		resources.ApplyResources(this.ULGDataItems, "ULGDataItems");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance8");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val24).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val24).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val24, "appearance9");
		((AppearanceBase)val24).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val25).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val25, "appearance10");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val25;
		((AppearanceBase)val26).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val26).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val26, "appearance11");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val27).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val27).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val27).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val27, "appearance12");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataItems).Name = "ULGDataItems";
		((UltraControlBase)this.ULGDataItems).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
		this.ULGDataItems.AfterEnterEditMode += new System.EventHandler(ULGDataItems_AfterEnterEditMode);
		this.ULGDataItems.AfterRowsDeleted += new System.EventHandler(ULGDataItems_AfterRowsDeleted);
		this.ULGDataItems.AfterRowInsert += new RowEventHandler(ULGDataItems_AfterRowInsert);
		this.ULGDataItems.CellListSelect += new CellEventHandler(ULGDataItems_CellListSelect);
		this.ULGDataItems.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataItems_BeforeRowsDeleted);
		((UltraGridBase)this.ULGDataItems).FilterRow += new FilterRowEventHandler(ULGDataItems_FilterRow);
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataItems_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataItems_KeyPress);
		resources.ApplyResources(this.chkItemsSubAccount, "chkItemsSubAccount");
		((System.Windows.Forms.Control)(object)this.chkItemsSubAccount).Name = "chkItemsSubAccount";
		((UltraToggleEditorBase)this.chkItemsSubAccount).CheckedChanged += new System.EventHandler(chkItemsSubAccount_CheckedChanged);
		resources.ApplyResources(this.cboItemsSubAccountTypeID, "cboItemsSubAccountTypeID");
		((TextEditorControlBase)this.cboItemsSubAccountTypeID).AlwaysInEditMode = true;
		this.cboItemsSubAccountTypeID.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemsSubAccountTypeID).Name = "cboItemsSubAccountTypeID";
		((EditorButtonControlBase)this.cboItemsSubAccountTypeID).ReadOnly = true;
		((TextEditorControlBase)this.cboItemsSubAccountTypeID).ValueChanged += new System.EventHandler(cboItemsSubAccountTypeID_ValueChanged);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnGenerateImportManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintImportManifest);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintEmptyManifest);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnGenerateExpManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintExportManifest);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblImportManifestDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpImportManifestDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblImportManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtImportManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblExportManifestDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpExportManifestDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblExportManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtExportManifestNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.btnGenerateImportManifestNo, "btnGenerateImportManifestNo");
		((System.Windows.Forms.Control)(object)this.btnGenerateImportManifestNo).Name = "btnGenerateImportManifestNo";
		((System.Windows.Forms.Control)(object)this.btnGenerateImportManifestNo).Click += new System.EventHandler(btnGenerateImportManifestNo_Click);
		resources.ApplyResources(this.btnPrintImportManifest, "btnPrintImportManifest");
		((System.Windows.Forms.Control)(object)this.btnPrintImportManifest).Name = "btnPrintImportManifest";
		((System.Windows.Forms.Control)(object)this.btnPrintImportManifest).Click += new System.EventHandler(btnPrintImportManifest_Click);
		resources.ApplyResources(this.btnPrintEmptyManifest, "btnPrintEmptyManifest");
		((System.Windows.Forms.Control)(object)this.btnPrintEmptyManifest).Name = "btnPrintEmptyManifest";
		((System.Windows.Forms.Control)(object)this.btnPrintEmptyManifest).Click += new System.EventHandler(btnPrintEmptyExportManifest_Click);
		resources.ApplyResources(this.btnGenerateExpManifestNo, "btnGenerateExpManifestNo");
		((System.Windows.Forms.Control)(object)this.btnGenerateExpManifestNo).Name = "btnGenerateExpManifestNo";
		((System.Windows.Forms.Control)(object)this.btnGenerateExpManifestNo).Click += new System.EventHandler(btnGenerateExpManifestNo_Click);
		resources.ApplyResources(this.btnPrintExportManifest, "btnPrintExportManifest");
		((System.Windows.Forms.Control)(object)this.btnPrintExportManifest).Name = "btnPrintExportManifest";
		((System.Windows.Forms.Control)(object)this.btnPrintExportManifest).Click += new System.EventHandler(btnPrintExportManifest_Click);
		resources.ApplyResources(this.lblImportManifestDate, "lblImportManifestDate");
		this.lblImportManifestDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblImportManifestDate).Name = "lblImportManifestDate";
		((ControlBase)this.lblImportManifestDate).WrapText = false;
		resources.ApplyResources(this.dtpImportManifestDate, "dtpImportManifestDate");
		((UltraWinEditorMaskedControlBase)this.dtpImportManifestDate).AlwaysInEditMode = true;
		this.dtpImportManifestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpImportManifestDate).Name = "dtpImportManifestDate";
		resources.ApplyResources(this.lblImportManifestNo, "lblImportManifestNo");
		this.lblImportManifestNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblImportManifestNo).Name = "lblImportManifestNo";
		((ControlBase)this.lblImportManifestNo).WrapText = false;
		resources.ApplyResources(this.txtImportManifestNo, "txtImportManifestNo");
		((System.Windows.Forms.Control)(object)this.txtImportManifestNo).Name = "txtImportManifestNo";
		resources.ApplyResources(this.lblExportManifestDate, "lblExportManifestDate");
		this.lblExportManifestDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExportManifestDate).Name = "lblExportManifestDate";
		((ControlBase)this.lblExportManifestDate).WrapText = false;
		resources.ApplyResources(this.dtpExportManifestDate, "dtpExportManifestDate");
		((UltraWinEditorMaskedControlBase)this.dtpExportManifestDate).AlwaysInEditMode = true;
		this.dtpExportManifestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExportManifestDate).Name = "dtpExportManifestDate";
		resources.ApplyResources(this.lblExportManifestNo, "lblExportManifestNo");
		this.lblExportManifestNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExportManifestNo).Name = "lblExportManifestNo";
		((ControlBase)this.lblExportManifestNo).WrapText = false;
		resources.ApplyResources(this.txtExportManifestNo, "txtExportManifestNo");
		((System.Windows.Forms.Control)(object)this.txtExportManifestNo).Name = "txtExportManifestNo";
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataSeaPortsReports);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.ULGDataSeaPortsReports, "ULGDataSeaPortsReports");
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val28, "appearance19");
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val29).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val29).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val29, "appearance20");
		((AppearanceBase)val29).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val30, "appearance21");
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val30;
		((AppearanceBase)val31).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val31).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val31, "appearance22");
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val32).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val32).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val32).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val32, "appearance23");
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataSeaPortsReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataSeaPortsReports).Name = "ULGDataSeaPortsReports";
		((UltraControlBase)this.ULGDataSeaPortsReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataSeaPortsReports.AfterEnterEditMode += new System.EventHandler(ULGDataSeaPortsReports_AfterEnterEditMode);
		this.ULGDataSeaPortsReports.ClickCellButton += new CellEventHandler(ULGDataSeaPortsReports_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateExpense);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnCreateExpense, "btnCreateExpense");
		((AppearanceBase)val33).Image = resources.GetObject("appearance81.Image");
		resources.ApplyResources(val33, "appearance81");
		((ControlBase)this.btnCreateExpense).Appearance = (AppearanceBase)(object)val33;
		((ControlBase)this.btnCreateExpense).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateExpense).Name = "btnCreateExpense";
		((System.Windows.Forms.Control)(object)this.btnCreateExpense).Click += new System.EventHandler(btnCreateExpense_Click);
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val34, "appearance3");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val35).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val35).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val35).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val35, "appearance4");
		((AppearanceBase)val35).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val36).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val36, "appearance5");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val37).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val37).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val37, "appearance6");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val38).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val38).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val38, "appearance7");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterEnterEditMode += new System.EventHandler(ULGDataExpenses_AfterEnterEditMode);
		this.ULGDataExpenses.CellListSelect += new CellEventHandler(ULGDataExpenses_CellListSelect);
		this.ULGDataExpenses.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataExpenses_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataExpenses_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataExpenses_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateInvoices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataInvoices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.btnCreateInvoices, "btnCreateInvoices");
		((AppearanceBase)val39).Image = resources.GetObject("appearance82.Image");
		resources.ApplyResources(val39, "appearance82");
		((ControlBase)this.btnCreateInvoices).Appearance = (AppearanceBase)(object)val39;
		((ControlBase)this.btnCreateInvoices).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateInvoices).Name = "btnCreateInvoices";
		((System.Windows.Forms.Control)(object)this.btnCreateInvoices).Click += new System.EventHandler(btnInvoices_Click);
		resources.ApplyResources(this.ULGDataInvoices, "ULGDataInvoices");
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val40, "appearance14");
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val41).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val41).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val41, "appearance15");
		((AppearanceBase)val41).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val42).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val42, "appearance16");
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val42;
		((AppearanceBase)val43).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val43).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val43, "appearance17");
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val44).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val44).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val44, "appearance18");
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataInvoices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataInvoices).Name = "ULGDataInvoices";
		((UltraControlBase)this.ULGDataInvoices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataInvoices.AfterEnterEditMode += new System.EventHandler(ULGDataInvoices_AfterEnterEditMode);
		this.ULGDataInvoices.ClickCellButton += new CellEventHandler(ULGDataInvoices_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnChangeCostCenter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActualDeparture);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualDepartureDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCenterSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartureDraftAft);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartureDraftFwd);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblArrivalDraftFwd);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartureDraft);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblArrivalDraftAft);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtArrivalDraftAft);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblArrivalDraft);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtArrivalDraftFwd);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtEntryReason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtExtendReason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblExtendReason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtRemainingFuel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblRemainingFuel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblWivesChildreCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtWivesChildrenCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblSeaMenCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtSeaMenCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblQuayNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtQuayNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblCaptain);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnToSeaPortSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnEntrySeaPortSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnFromSeaPortSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblToSeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboToSeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblFromSeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboFromSeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboServiceQuotation);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceQuotation);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboItemQuotation);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblItemQuotation);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengersCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtPassengersCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboOwners);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblOwner);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboCharterers);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblCharter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboCaptain);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnOwnerSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnCharterer);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnCaptain);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemQuotationSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnServiceQuotationSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.dtpEntryDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryLoad);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtEntryLoad);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboLoadType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryReason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartureLoad);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboEntrySeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartureLoad);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntrySeaPort);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartureDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.dtpDepartureDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.btnChangeCostCenter, "btnChangeCostCenter");
		((System.Windows.Forms.Control)(object)this.btnChangeCostCenter).Name = "btnChangeCostCenter";
		((System.Windows.Forms.Control)(object)this.btnChangeCostCenter).Click += new System.EventHandler(btnChangeCostCenter_Click);
		resources.ApplyResources(this.chkIsActualDeparture, "chkIsActualDeparture");
		((System.Windows.Forms.Control)(object)this.chkIsActualDeparture).Name = "chkIsActualDeparture";
		((UltraToggleEditorBase)this.chkIsActualDeparture).CheckedChanged += new System.EventHandler(chkIsActualDeparture_CheckedChanged);
		resources.ApplyResources(this.dtpActualDepartureDate, "dtpActualDepartureDate");
		((UltraWinEditorMaskedControlBase)this.dtpActualDepartureDate).AlwaysInEditMode = true;
		this.dtpActualDepartureDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpActualDepartureDate).Name = "dtpActualDepartureDate";
		resources.ApplyResources(this.btnCostCenterSearch, "btnCostCenterSearch");
		((AppearanceBase)val45).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val45, "appearance83");
		((ControlBase)this.btnCostCenterSearch).Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Name = "btnCostCenterSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Click += new System.EventHandler(btnCostCenterSearch_Click);
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		((System.Windows.Forms.Control)(object)this.cboCostCenter).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCostCenter_KeyDown);
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtDepartureDraftAft, "txtDepartureDraftAft");
		((System.Windows.Forms.Control)(object)this.txtDepartureDraftAft).Name = "txtDepartureDraftAft";
		((System.Windows.Forms.Control)(object)this.txtDepartureDraftAft).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtDepartureDraftFwd, "txtDepartureDraftFwd");
		((System.Windows.Forms.Control)(object)this.txtDepartureDraftFwd).Name = "txtDepartureDraftFwd";
		((System.Windows.Forms.Control)(object)this.txtDepartureDraftFwd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblArrivalDraftFwd, "lblArrivalDraftFwd");
		this.lblArrivalDraftFwd.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArrivalDraftFwd).Name = "lblArrivalDraftFwd";
		((ControlBase)this.lblArrivalDraftFwd).WrapText = false;
		resources.ApplyResources(this.lblDepartureDraft, "lblDepartureDraft");
		this.lblDepartureDraft.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartureDraft).Name = "lblDepartureDraft";
		((ControlBase)this.lblDepartureDraft).WrapText = false;
		resources.ApplyResources(this.lblArrivalDraftAft, "lblArrivalDraftAft");
		this.lblArrivalDraftAft.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArrivalDraftAft).Name = "lblArrivalDraftAft";
		((ControlBase)this.lblArrivalDraftAft).WrapText = false;
		resources.ApplyResources(this.txtArrivalDraftAft, "txtArrivalDraftAft");
		((System.Windows.Forms.Control)(object)this.txtArrivalDraftAft).Name = "txtArrivalDraftAft";
		((System.Windows.Forms.Control)(object)this.txtArrivalDraftAft).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblArrivalDraft, "lblArrivalDraft");
		this.lblArrivalDraft.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArrivalDraft).Name = "lblArrivalDraft";
		((ControlBase)this.lblArrivalDraft).WrapText = false;
		resources.ApplyResources(this.txtArrivalDraftFwd, "txtArrivalDraftFwd");
		((System.Windows.Forms.Control)(object)this.txtArrivalDraftFwd).Name = "txtArrivalDraftFwd";
		((System.Windows.Forms.Control)(object)this.txtArrivalDraftFwd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtEntryReason, "txtEntryReason");
		((System.Windows.Forms.Control)(object)this.txtEntryReason).Name = "txtEntryReason";
		resources.ApplyResources(this.txtExtendReason, "txtExtendReason");
		((System.Windows.Forms.Control)(object)this.txtExtendReason).Name = "txtExtendReason";
		resources.ApplyResources(this.lblExtendReason, "lblExtendReason");
		this.lblExtendReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExtendReason).Name = "lblExtendReason";
		((ControlBase)this.lblExtendReason).WrapText = false;
		resources.ApplyResources(this.txtRemainingFuel, "txtRemainingFuel");
		((System.Windows.Forms.Control)(object)this.txtRemainingFuel).Name = "txtRemainingFuel";
		resources.ApplyResources(this.lblRemainingFuel, "lblRemainingFuel");
		this.lblRemainingFuel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemainingFuel).Name = "lblRemainingFuel";
		((ControlBase)this.lblRemainingFuel).WrapText = false;
		resources.ApplyResources(this.lblWivesChildreCount, "lblWivesChildreCount");
		this.lblWivesChildreCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWivesChildreCount).Name = "lblWivesChildreCount";
		((ControlBase)this.lblWivesChildreCount).WrapText = false;
		resources.ApplyResources(this.txtWivesChildrenCount, "txtWivesChildrenCount");
		((System.Windows.Forms.Control)(object)this.txtWivesChildrenCount).Name = "txtWivesChildrenCount";
		resources.ApplyResources(this.lblSeaMenCount, "lblSeaMenCount");
		this.lblSeaMenCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeaMenCount).Name = "lblSeaMenCount";
		((ControlBase)this.lblSeaMenCount).WrapText = false;
		resources.ApplyResources(this.txtSeaMenCount, "txtSeaMenCount");
		((System.Windows.Forms.Control)(object)this.txtSeaMenCount).Name = "txtSeaMenCount";
		((TextEditorControlBase)this.txtSeaMenCount).ValueChanged += new System.EventHandler(txtSeaMenCount_ValueChanged);
		resources.ApplyResources(this.lblQuayNo, "lblQuayNo");
		this.lblQuayNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuayNo).Name = "lblQuayNo";
		((ControlBase)this.lblQuayNo).WrapText = false;
		resources.ApplyResources(this.txtQuayNo, "txtQuayNo");
		((System.Windows.Forms.Control)(object)this.txtQuayNo).Name = "txtQuayNo";
		resources.ApplyResources(this.lblCaptain, "lblCaptain");
		this.lblCaptain.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCaptain).Name = "lblCaptain";
		((ControlBase)this.lblCaptain).WrapText = false;
		resources.ApplyResources(this.btnToSeaPortSearch, "btnToSeaPortSearch");
		((AppearanceBase)val46).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val46, "appearance84");
		((ControlBase)this.btnToSeaPortSearch).Appearance = (AppearanceBase)(object)val46;
		((System.Windows.Forms.Control)(object)this.btnToSeaPortSearch).Name = "btnToSeaPortSearch";
		((System.Windows.Forms.Control)(object)this.btnToSeaPortSearch).Click += new System.EventHandler(btnToSeaPortSearch_Click);
		resources.ApplyResources(this.btnEntrySeaPortSearch, "btnEntrySeaPortSearch");
		((AppearanceBase)val47).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val47, "appearance85");
		((ControlBase)this.btnEntrySeaPortSearch).Appearance = (AppearanceBase)(object)val47;
		((System.Windows.Forms.Control)(object)this.btnEntrySeaPortSearch).Name = "btnEntrySeaPortSearch";
		((System.Windows.Forms.Control)(object)this.btnEntrySeaPortSearch).Click += new System.EventHandler(btnEntrySeaPortSearch_Click);
		resources.ApplyResources(this.btnFromSeaPortSearch, "btnFromSeaPortSearch");
		((AppearanceBase)val48).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val48, "appearance86");
		((ControlBase)this.btnFromSeaPortSearch).Appearance = (AppearanceBase)(object)val48;
		((System.Windows.Forms.Control)(object)this.btnFromSeaPortSearch).Name = "btnFromSeaPortSearch";
		((System.Windows.Forms.Control)(object)this.btnFromSeaPortSearch).Click += new System.EventHandler(btnFromSeaPortSearch_Click);
		resources.ApplyResources(this.lblToSeaPort, "lblToSeaPort");
		this.lblToSeaPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToSeaPort).Name = "lblToSeaPort";
		((ControlBase)this.lblToSeaPort).WrapText = false;
		resources.ApplyResources(this.cboToSeaPort, "cboToSeaPort");
		((TextEditorControlBase)this.cboToSeaPort).AlwaysInEditMode = true;
		this.cboToSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboToSeaPort).Name = "cboToSeaPort";
		resources.ApplyResources(this.lblFromSeaPort, "lblFromSeaPort");
		this.lblFromSeaPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromSeaPort).Name = "lblFromSeaPort";
		((ControlBase)this.lblFromSeaPort).WrapText = false;
		resources.ApplyResources(this.cboFromSeaPort, "cboFromSeaPort");
		((TextEditorControlBase)this.cboFromSeaPort).AlwaysInEditMode = true;
		this.cboFromSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFromSeaPort).Name = "cboFromSeaPort";
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.cboServiceQuotation, "cboServiceQuotation");
		((TextEditorControlBase)this.cboServiceQuotation).AlwaysInEditMode = true;
		this.cboServiceQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboServiceQuotation).Name = "cboServiceQuotation";
		((TextEditorControlBase)this.cboServiceQuotation).ValueChanged += new System.EventHandler(cboServiceQuotation_ValueChanged);
		resources.ApplyResources(this.lblServiceQuotation, "lblServiceQuotation");
		this.lblServiceQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceQuotation).Name = "lblServiceQuotation";
		((ControlBase)this.lblServiceQuotation).WrapText = false;
		resources.ApplyResources(this.cboItemQuotation, "cboItemQuotation");
		((TextEditorControlBase)this.cboItemQuotation).AlwaysInEditMode = true;
		this.cboItemQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemQuotation).Name = "cboItemQuotation";
		((TextEditorControlBase)this.cboItemQuotation).ValueChanged += new System.EventHandler(cboItemQuotation_ValueChanged);
		resources.ApplyResources(this.lblItemQuotation, "lblItemQuotation");
		this.lblItemQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemQuotation).Name = "lblItemQuotation";
		((ControlBase)this.lblItemQuotation).WrapText = false;
		resources.ApplyResources(this.lblPassengersCount, "lblPassengersCount");
		this.lblPassengersCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassengersCount).Name = "lblPassengersCount";
		((ControlBase)this.lblPassengersCount).WrapText = false;
		resources.ApplyResources(this.txtPassengersCount, "txtPassengersCount");
		((System.Windows.Forms.Control)(object)this.txtPassengersCount).Name = "txtPassengersCount";
		resources.ApplyResources(this.cboOwners, "cboOwners");
		((TextEditorControlBase)this.cboOwners).AlwaysInEditMode = true;
		this.cboOwners.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOwners).Name = "cboOwners";
		resources.ApplyResources(this.lblOwner, "lblOwner");
		this.lblOwner.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOwner).Name = "lblOwner";
		((ControlBase)this.lblOwner).WrapText = false;
		resources.ApplyResources(this.cboCharterers, "cboCharterers");
		((TextEditorControlBase)this.cboCharterers).AlwaysInEditMode = true;
		this.cboCharterers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCharterers).Name = "cboCharterers";
		resources.ApplyResources(this.lblCharter, "lblCharter");
		this.lblCharter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCharter).Name = "lblCharter";
		((ControlBase)this.lblCharter).WrapText = false;
		resources.ApplyResources(this.cboCaptain, "cboCaptain");
		((TextEditorControlBase)this.cboCaptain).AlwaysInEditMode = true;
		this.cboCaptain.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCaptain).Name = "cboCaptain";
		resources.ApplyResources(this.btnOwnerSearch, "btnOwnerSearch");
		((AppearanceBase)val49).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val49, "appearance87");
		((ControlBase)this.btnOwnerSearch).Appearance = (AppearanceBase)(object)val49;
		((System.Windows.Forms.Control)(object)this.btnOwnerSearch).Name = "btnOwnerSearch";
		((System.Windows.Forms.Control)(object)this.btnOwnerSearch).Click += new System.EventHandler(btnOwnerSearch_Click);
		resources.ApplyResources(this.btnCharterer, "btnCharterer");
		((AppearanceBase)val50).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val50, "appearance88");
		((ControlBase)this.btnCharterer).Appearance = (AppearanceBase)(object)val50;
		((System.Windows.Forms.Control)(object)this.btnCharterer).Name = "btnCharterer";
		((System.Windows.Forms.Control)(object)this.btnCharterer).Click += new System.EventHandler(btnCharterer_Click);
		resources.ApplyResources(this.btnCaptain, "btnCaptain");
		((AppearanceBase)val51).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val51, "appearance89");
		((ControlBase)this.btnCaptain).Appearance = (AppearanceBase)(object)val51;
		((System.Windows.Forms.Control)(object)this.btnCaptain).Name = "btnCaptain";
		((System.Windows.Forms.Control)(object)this.btnCaptain).Click += new System.EventHandler(btnCaptain_Click);
		resources.ApplyResources(this.btnItemQuotationSearch, "btnItemQuotationSearch");
		((AppearanceBase)val52).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val52, "appearance90");
		((ControlBase)this.btnItemQuotationSearch).Appearance = (AppearanceBase)(object)val52;
		((System.Windows.Forms.Control)(object)this.btnItemQuotationSearch).Name = "btnItemQuotationSearch";
		((System.Windows.Forms.Control)(object)this.btnItemQuotationSearch).Click += new System.EventHandler(btnItemQuotationSearch_Click);
		resources.ApplyResources(this.btnServiceQuotationSearch, "btnServiceQuotationSearch");
		((AppearanceBase)val53).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val53, "appearance91");
		((ControlBase)this.btnServiceQuotationSearch).Appearance = (AppearanceBase)(object)val53;
		((System.Windows.Forms.Control)(object)this.btnServiceQuotationSearch).Name = "btnServiceQuotationSearch";
		resources.ApplyResources(this.dtpEntryDate, "dtpEntryDate");
		((UltraWinEditorMaskedControlBase)this.dtpEntryDate).AlwaysInEditMode = true;
		this.dtpEntryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpEntryDate).Name = "dtpEntryDate";
		resources.ApplyResources(this.lblEntryDate, "lblEntryDate");
		this.lblEntryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryDate).Name = "lblEntryDate";
		((ControlBase)this.lblEntryDate).WrapText = false;
		resources.ApplyResources(this.lblEntryLoad, "lblEntryLoad");
		this.lblEntryLoad.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryLoad).Name = "lblEntryLoad";
		((ControlBase)this.lblEntryLoad).WrapText = false;
		resources.ApplyResources(this.txtEntryLoad, "txtEntryLoad");
		((System.Windows.Forms.Control)(object)this.txtEntryLoad).Name = "txtEntryLoad";
		((System.Windows.Forms.Control)(object)this.txtEntryLoad).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.cboLoadType, "cboLoadType");
		((TextEditorControlBase)this.cboLoadType).AlwaysInEditMode = true;
		this.cboLoadType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLoadType).Name = "cboLoadType";
		resources.ApplyResources(this.lblLoadType, "lblLoadType");
		this.lblLoadType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadType).Name = "lblLoadType";
		((ControlBase)this.lblLoadType).WrapText = false;
		resources.ApplyResources(this.lblEntryReason, "lblEntryReason");
		this.lblEntryReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryReason).Name = "lblEntryReason";
		((ControlBase)this.lblEntryReason).WrapText = false;
		resources.ApplyResources(this.txtDepartureLoad, "txtDepartureLoad");
		((System.Windows.Forms.Control)(object)this.txtDepartureLoad).Name = "txtDepartureLoad";
		((System.Windows.Forms.Control)(object)this.txtDepartureLoad).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.cboEntrySeaPort, "cboEntrySeaPort");
		((TextEditorControlBase)this.cboEntrySeaPort).AlwaysInEditMode = true;
		this.cboEntrySeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEntrySeaPort).Name = "cboEntrySeaPort";
		((TextEditorControlBase)this.cboEntrySeaPort).ValueChanged += new System.EventHandler(cboEntrySeaPort_ValueChanged);
		resources.ApplyResources(this.lblDepartureLoad, "lblDepartureLoad");
		this.lblDepartureLoad.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartureLoad).Name = "lblDepartureLoad";
		((ControlBase)this.lblDepartureLoad).WrapText = false;
		resources.ApplyResources(this.lblEntrySeaPort, "lblEntrySeaPort");
		this.lblEntrySeaPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntrySeaPort).Name = "lblEntrySeaPort";
		((ControlBase)this.lblEntrySeaPort).WrapText = false;
		resources.ApplyResources(this.lblDepartureDate, "lblDepartureDate");
		this.lblDepartureDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartureDate).Name = "lblDepartureDate";
		((ControlBase)this.lblDepartureDate).WrapText = false;
		resources.ApplyResources(this.dtpDepartureDate, "dtpDepartureDate");
		((UltraWinEditorMaskedControlBase)this.dtpDepartureDate).AlwaysInEditMode = true;
		this.dtpDepartureDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDepartureDate).Name = "dtpDepartureDate";
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataOrders);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateOrders);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		resources.ApplyResources(this.ULGDataOrders, "ULGDataOrders");
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val54).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val54, "appearance33");
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val54;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val55).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val55).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val55).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val55, "appearance34");
		((AppearanceBase)val55).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val56).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val56, "appearance35");
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val56;
		((AppearanceBase)val57).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val57).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val57).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val57, "appearance36");
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val57;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val58).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val58).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val58).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val58).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val58, "appearance37");
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataOrders).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataOrders).Name = "ULGDataOrders";
		((UltraControlBase)this.ULGDataOrders).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataOrders.AfterEnterEditMode += new System.EventHandler(ULGDataOrders_AfterEnterEditMode);
		this.ULGDataOrders.ClickCellButton += new CellEventHandler(ULGDataOrders_ClickCellButton);
		resources.ApplyResources(this.btnCreateOrders, "btnCreateOrders");
		((AppearanceBase)val59).Image = resources.GetObject("appearance92.Image");
		resources.ApplyResources(val59, "appearance92");
		((ControlBase)this.btnCreateOrders).Appearance = (AppearanceBase)(object)val59;
		((ControlBase)this.btnCreateOrders).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateOrders).Name = "btnCreateOrders";
		((System.Windows.Forms.Control)(object)this.btnCreateOrders).Click += new System.EventHandler(btnCreateOrders_Click);
		resources.ApplyResources(this.ultraTabPageControl12, "ultraTabPageControl12");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl12).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataMaterialIssueVoucher);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl12).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateMIV);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl12).Name = "ultraTabPageControl12";
		resources.ApplyResources(this.ULGDataMaterialIssueVoucher, "ULGDataMaterialIssueVoucher");
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val60).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val60, "appearance54");
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val60;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val61).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val61).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val61).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val61, "appearance55");
		((AppearanceBase)val61).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val61;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val62).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val62, "appearance56");
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val62;
		((AppearanceBase)val63).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val63).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val63).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val63, "appearance57");
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val63;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val64).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val64).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val64).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val64).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val64, "appearance58");
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val64;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataMaterialIssueVoucher).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataMaterialIssueVoucher).Name = "ULGDataMaterialIssueVoucher";
		((UltraControlBase)this.ULGDataMaterialIssueVoucher).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataMaterialIssueVoucher.AfterEnterEditMode += new System.EventHandler(ULGDataMaterialIssueVoucher_AfterEnterEditMode);
		this.ULGDataMaterialIssueVoucher.ClickCellButton += new CellEventHandler(ULGDataMaterialIssueVoucher_ClickCellButton);
		resources.ApplyResources(this.btnCreateMIV, "btnCreateMIV");
		((AppearanceBase)val65).Image = resources.GetObject("appearance93.Image");
		resources.ApplyResources(val65, "appearance93");
		((ControlBase)this.btnCreateMIV).Appearance = (AppearanceBase)(object)val65;
		((ControlBase)this.btnCreateMIV).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateMIV).Name = "btnCreateMIV";
		((System.Windows.Forms.Control)(object)this.btnCreateMIV).Click += new System.EventHandler(btnCreateMIV_Click);
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataTasks);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		resources.ApplyResources(this.ULGDataTasks, "ULGDataTasks");
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val66).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val66, "appearance39");
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val66;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val67).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val67).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val67).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val67, "appearance40");
		((AppearanceBase)val67).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val67;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val68).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val68, "appearance41");
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val68;
		((AppearanceBase)val69).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val69).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val69).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val69, "appearance42");
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val69;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val70).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val70).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val70).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val70).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val70, "appearance43");
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val70;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataTasks).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataTasks).Name = "ULGDataTasks";
		((UltraControlBase)this.ULGDataTasks).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataTasks.AfterEnterEditMode += new System.EventHandler(ULGDataTasks_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl10, "ultraTabPageControl10");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataRemarks);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Name = "ultraTabPageControl10";
		resources.ApplyResources(this.ULGDataRemarks, "ULGDataRemarks");
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val71).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val71, "appearance44");
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val71;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val72).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val72).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val72).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val72, "appearance45");
		((AppearanceBase)val72).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val72;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val73).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val73, "appearance46");
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val73;
		((AppearanceBase)val74).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val74).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val74).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val74, "appearance47");
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val74;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val75).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val75).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val75).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val75).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val75, "appearance48");
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val75;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataRemarks).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataRemarks).Name = "ULGDataRemarks";
		((UltraControlBase)this.ULGDataRemarks).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataRemarks.AfterEnterEditMode += new System.EventHandler(ULGDataRemarks_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl11, "ultraTabPageControl11");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl11).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataSeaMen);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl11).Name = "ultraTabPageControl11";
		resources.ApplyResources(this.ULGDataSeaMen, "ULGDataSeaMen");
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val76).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val76, "appearance49");
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val76;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val77).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val77).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val77).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val77, "appearance50");
		((AppearanceBase)val77).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val77;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val78).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val78, "appearance51");
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val78;
		((AppearanceBase)val79).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val79).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val79).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val79, "appearance52");
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val79;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val80).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val80).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val80).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val80).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val80, "appearance53");
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val80;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataSeaMen).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataSeaMen).Name = "ULGDataSeaMen";
		((UltraControlBase)this.ULGDataSeaMen).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataSeaMen.AfterEnterEditMode += new System.EventHandler(ULGDataSeaMen_AfterEnterEditMode);
		this.ULGDataSeaMen.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataSeaMen_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl13, "ultraTabPageControl13");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl13).Controls.Add((System.Windows.Forms.Control)(object)this.ULGServicesReports);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl13).Name = "ultraTabPageControl13";
		resources.ApplyResources(this.ULGServicesReports, "ULGServicesReports");
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val81).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val81, "appearance60");
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val81;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val82).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val82).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val82).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val82, "appearance61");
		((AppearanceBase)val82).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val82;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val83).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val83, "appearance62");
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val83;
		((AppearanceBase)val84).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val84).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val84).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val84, "appearance63");
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val84;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val85).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val85).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val85).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val85).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val85, "appearance64");
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val85;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGServicesReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGServicesReports).Name = "ULGServicesReports";
		((UltraControlBase)this.ULGServicesReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGServicesReports.AfterEnterEditMode += new System.EventHandler(ULGServicesReports_AfterEnterEditMode);
		this.ULGServicesReports.ClickCellButton += new CellEventHandler(ULGServicesReports_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl14, "ultraTabPageControl14");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl14).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateShipChandler);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl14).Controls.Add((System.Windows.Forms.Control)(object)this.ULGShipChandler);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl14).Name = "ultraTabPageControl14";
		resources.ApplyResources(this.btnCreateShipChandler, "btnCreateShipChandler");
		((AppearanceBase)val86).Image = resources.GetObject("appearance94.Image");
		resources.ApplyResources(val86, "appearance94");
		((ControlBase)this.btnCreateShipChandler).Appearance = (AppearanceBase)(object)val86;
		((ControlBase)this.btnCreateShipChandler).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateShipChandler).Name = "btnCreateShipChandler";
		((System.Windows.Forms.Control)(object)this.btnCreateShipChandler).Click += new System.EventHandler(btnCreateShipChandler_Click);
		resources.ApplyResources(this.ULGShipChandler, "ULGShipChandler");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val87).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val87, "appearance66");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val87;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val88).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val88).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val88).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val88, "appearance67");
		((AppearanceBase)val88).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val88;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val89).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val89, "appearance68");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val89;
		((AppearanceBase)val90).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val90).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val90).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val90, "appearance69");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val90;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val91).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val91).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val91).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val91).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val91, "appearance70");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val91;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGShipChandler).Name = "ULGShipChandler";
		((UltraControlBase)this.ULGShipChandler).UseFlatMode = (DefaultableBoolean)1;
		this.ULGShipChandler.AfterEnterEditMode += new System.EventHandler(ULGShipChandler_AfterEnterEditMode);
		this.ULGShipChandler.ClickCellButton += new CellEventHandler(ULGShipChandler_ClickCellButton);
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
		resources.ApplyResources(this.cboVesselName, "cboVesselName");
		((TextEditorControlBase)this.cboVesselName).AlwaysInEditMode = true;
		this.cboVesselName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVesselName).Name = "cboVesselName";
		((TextEditorControlBase)this.cboVesselName).ValueChanged += new System.EventHandler(cboVesselName_ValueChanged);
		resources.ApplyResources(this.lblVesselName, "lblVesselName");
		this.lblVesselName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselName).Name = "lblVesselName";
		((ControlBase)this.lblVesselName).WrapText = false;
		resources.ApplyResources(this.btnVesselSearch, "btnVesselSearch");
		((AppearanceBase)val92).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val92, "appearance95");
		((ControlBase)this.btnVesselSearch).Appearance = (AppearanceBase)(object)val92;
		((System.Windows.Forms.Control)(object)this.btnVesselSearch).Name = "btnVesselSearch";
		((System.Windows.Forms.Control)(object)this.btnVesselSearch).Click += new System.EventHandler(btnVesselSearch_Click);
		resources.ApplyResources(this.lblVoyageNo, "lblVoyageNo");
		this.lblVoyageNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyageNo).Name = "lblVoyageNo";
		((ControlBase)this.lblVoyageNo).WrapText = false;
		resources.ApplyResources(this.txtVoyageNo, "txtVoyageNo");
		((System.Windows.Forms.Control)(object)this.txtVoyageNo).Name = "txtVoyageNo";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.lblCurrentCrew, "lblCurrentCrew");
		this.lblCurrentCrew.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrentCrew).Name = "lblCurrentCrew";
		resources.ApplyResources(this.lblCurrentPassenger, "lblCurrentPassenger");
		this.lblCurrentPassenger.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrentPassenger).Name = "lblCurrentPassenger";
		((ControlBase)this.lblCurrentPassenger).WrapText = false;
		resources.ApplyResources(this.lblCurrentPassengerCount, "lblCurrentPassengerCount");
		this.lblCurrentPassengerCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrentPassengerCount).Name = "lblCurrentPassengerCount";
		resources.ApplyResources(this.btnChangeVessel, "btnChangeVessel");
		((System.Windows.Forms.Control)(object)this.btnChangeVessel).Name = "btnChangeVessel";
		((System.Windows.Forms.Control)(object)this.btnChangeVessel).Click += new System.EventHandler(btnChangeVessel_Click);
		resources.ApplyResources(this.btnOpenOperationsSearch, "btnOpenOperationsSearch");
		((AppearanceBase)val93).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val93, "appearance96");
		((ControlBase)this.btnOpenOperationsSearch).Appearance = (AppearanceBase)(object)val93;
		((System.Windows.Forms.Control)(object)this.btnOpenOperationsSearch).Name = "btnOpenOperationsSearch";
		((UltraControlBase)this.btnOpenOperationsSearch).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.btnOpenOperationsSearch).Click += new System.EventHandler(btnOpenOperationsSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenOperationsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnChangeVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrentPassengerCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrentCrew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrentPassenger);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyageNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVesselSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyageNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Name = "frmOperations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyageNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVesselSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyageNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrentPassenger, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrentCrew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrentPassengerCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnChangeVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenOperationsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboItemsSubAccountID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkItemsSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemsSubAccountTypeID).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpImportManifestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtImportManifestNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExportManifestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExportManifestNo).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataSeaPortsReports).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataInvoices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkIsActualDeparture).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualDepartureDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureDraftAft).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureDraftFwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArrivalDraftAft).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArrivalDraftFwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemainingFuel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWivesChildrenCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMenCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuayNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboToSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFromSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboServiceQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassengersCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCharterers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCaptain).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryLoad).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartureLoad).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEntrySeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDepartureDate).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataOrders).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl12).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataMaterialIssueVoucher).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataTasks).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataRemarks).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl11).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataSeaMen).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl13).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGServicesReports).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl14).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandler).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyageNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
