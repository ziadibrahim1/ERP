using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using BusinessLayer.Export;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.CustomsClearence.MasterData;
using ERP.Properties;
using ERP.Sales.MasterData;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.CustomsClearence.Transactions;

public class frmOperations : frmHeaderManyDetails
{
	private DataTable dtServicesQuotations;

	private DataTable dtReports;

	private DataTable dtTaxes;

	private DataTable dtExpenses;

	private DataTable dtServices;

	private DataTable dtServicesPrices;

	private DataTable dtPriceType;

	private DataTable dtExportTypes;

	private DataTable dtCurrency;

	private DataTable dtCountries;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtEmployees;

	private DataTable dtSeaPorts;

	private DataTable dtPackageTypes;

	private DataTable dtBotanicalNames;

	private DataTable dtContainersTypes;

	private DataTable dtOperationExpenses;

	private DataTable dtBillsOfLading;

	private DataTable dtCertificatesOfOrigin;

	private DataTable dtPhytosanitaryCertificates;

	private DataTable dtOperationItems;

	private DataTable dtFreightForwarder;

	private DataTable dtExporter;

	private DataTable dtCustomsBrokers;

	private ValueList vlBotanicalNames = new ValueList();

	private ValueList vlPackageTypes = new ValueList();

	private ValueList vlContainerTypes = new ValueList();

	private ValueList vlExpenses = new ValueList();

	private ValueList vlServices = new ValueList();

	private ValueList vlAccounts = new ValueList();

	private ValueList vlEmployees = new ValueList();

	private ValueList vlTaxes = new ValueList();

	private string CustodyAccountID = "";

	private string SubAccountID;

	private bool CanOpenLetter = false;

	private bool CanAddNewOperationExpenses = false;

	private bool CanAddBill = false;

	private bool CanAddCertOfOrigin = false;

	private bool CanAddPhyCert = false;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnSeaPortSearch;

	private UltraLabel lblSeaPort;

	private UltraComboEditor cboLoadingPorts;

	private UltraTextEditor txtClientInvoiceNo;

	private UltraTextEditor txtCertificateNo;

	private UltraLabel lblCertificateNo;

	private UltraDateTimeEditor dtpCertificateDate;

	private UltraLabel lblCertificateDate;

	private UltraCheckEditor chkIsOriginCountry;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraTabPageControl ultraTabPageControl4;

	protected internal UltraGrid ULGDataItems;

	private UltraTabPageControl ultraTabPageControl5;

	protected internal UltraGrid ULGDataExpenses;

	private UltraLabel lblClientName;

	private UltraTextEditor txtClientName;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraCheckEditor chkIsCompass;

	private UltraLabel lblSubAccount;

	private UltraComboEditor cboSubAccountName;

	public UltraButton btnPriceTypeSearch;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboExportType;

	private UltraLabel lblExportType;

	private UltraCheckEditor chkIsCopies;

	private UltraCheckEditor chkClosed;

	private UltraCheckEditor chkIsCanceled;

	private UltraDateTimeEditor dtpCancelDate;

	private UltraComboEditor cboCertificateOpeningSeaPort;

	private UltraLabel lblCertificateOpeningSeaPort;

	public UltraButton btnCertificateOpeningSeaPortSearch;

	public UltraButton btnCreateExpense;

	private UltraTabPageControl ultraTabPageControl6;

	protected internal UltraGrid ULGReports;

	private UltraLabel lblFreightForwarder;

	private UltraComboEditor cboFreightForwarder;

	private UltraComboEditor cboExporter;

	private UltraLabel lblExporter;

	private UltraComboEditor cboSeaPortOfDischarge;

	private UltraLabel lblSeaPortOfDischarge;

	private UltraDateTimeEditor dtpOriginCertificateDate;

	private UltraLabel lblOriginCertificateDate;

	private UltraDateTimeEditor dtpShippingDate;

	private UltraLabel lblShippingDate;

	private UltraLabel lblVesselName;

	private UltraTextEditor txtVesselName;

	private UltraLabel lblClientInvoiceNo;

	private UltraCheckEditor chkIsPrePaid;

	private UltraLabel lblPOLCountry;

	private UltraComboEditor cboPOLCountries;

	public UltraButton btnSeaPortOfDischargeSearch;

	public UltraButton btnClientAdd;

	public UltraButton btnAddExporter;

	private UltraLabel lblTotalQty;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalGrossWeight;

	private UltraTextEditor txtTotalGrossWeight;

	private UltraLabel lblTotalNetWeight;

	private UltraTextEditor txtTotalNetWeight;

	private UltraTabPageControl ultraTabPageControl7;

	private UltraTabPageControl ultraTabPageControl8;

	private UltraTabPageControl ultraTabPageControl9;

	protected internal UltraGrid ULGBillsOFLading;

	protected internal UltraGrid ULGPhytosanitaryCertificates;

	protected internal UltraGrid ULGCertificatesOFOrigin;

	public UltraButton btnAddPhyCert;

	public UltraButton btnAddCertOfOrigin;

	public UltraButton btnAddBL;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsQuotation;

	private RadioButton rbIsDirect;

	private UltraLabel lblQuotation;

	private UltraComboEditor cboQuotation;

	private UltraComboEditor cboCustomsBrokers;

	private UltraLabel lblCustomBroker;

	private UltraCheckEditor chkRevised;

	private UltraLabel lblBrokerFinishedDate;

	private UltraDateTimeEditor dtpBrokerFinishedDate;

	private UltraCheckEditor chkBrokerFinished;

	private UltraLabel lblRevised;

	private UltraDateTimeEditor dtpRevisedDate;

	public frmOperations()
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
		TableName = "CST_Operations";
		IDCol = "OperationID";
		NoCol = "OperationNo";
		DateCol = "OperationDate";
	}

	public frmOperations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public void CalcTotalQty()
	{
		((Control)(object)txtTotalQty).Text = "0";
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count <= 0)
		{
			return;
		}
		((UltraGridBase)ULGDataItems).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGDataItems).DataSource);
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	public void CalcTotalNetWeight()
	{
		((Control)(object)txtTotalNetWeight).Text = "0";
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count <= 0)
		{
			return;
		}
		((UltraGridBase)ULGDataItems).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGDataItems).DataSource);
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0)
		{
			object obj = dataTable.Compute(" Sum(NetWeight) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalNetWeight).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	public void CalcTotalGrossWeight()
	{
		((Control)(object)txtTotalGrossWeight).Text = "0";
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count <= 0)
		{
			return;
		}
		((UltraGridBase)ULGDataItems).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGDataItems).DataSource);
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0)
		{
			object obj = dataTable.Compute(" Sum(GrossWeight) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalGrossWeight).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	public override void PrepareData()
	{
		base.PrepareData();
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmOperationExpenses'").Length != 0)
		{
			CanAddNewOperationExpenses = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmOperationExpenses'")[0]["FormID"].ToString(), "Adding");
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmBillsOfLading'").Length != 0)
		{
			CanAddBill = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmBillsOfLading'")[0]["FormID"].ToString(), "Adding");
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmCertificatesOfOrigin'").Length != 0)
		{
			CanAddCertOfOrigin = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmCertificatesOfOrigin'")[0]["FormID"].ToString(), "Adding");
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmPhytosanitaryCertificates'").Length != 0)
		{
			CanAddPhyCert = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CustomsClearence.Transactions.frmPhytosanitaryCertificates'")[0]["FormID"].ToString(), "Adding");
		}
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		GlobalFunctions.FillCombo(cboPOLCountries, dtCountries, "CountryID", "CountryName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtExportTypes = ExportsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboExportType, dtExportTypes, "ExportTypeID", "ExportTypeName");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int i = 0; i < dtTaxes.Rows.Count; i++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[i]["TaxID"], dtTaxes.Rows[i]["TaxName"].ToString());
		}
		FillCurrencyDropDown();
		dtBotanicalNames = BotanicalNames.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBotanicalNames.ValueListItems.Clear();
		for (int j = 0; j < dtBotanicalNames.Rows.Count; j++)
		{
			vlBotanicalNames.ValueListItems.Add(dtBotanicalNames.Rows[j]["BotanicalNameID"], dtBotanicalNames.Rows[j]["BotanicalName"].ToString());
		}
		dtPackageTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackageTypes.ValueListItems.Clear();
		for (int k = 0; k < dtPackageTypes.Rows.Count; k++)
		{
			vlPackageTypes.ValueListItems.Add(dtPackageTypes.Rows[k]["PackingTypeID"], dtPackageTypes.Rows[k]["PackingTypeName"].ToString());
		}
		dtContainersTypes = BusinessLayer.CustomsClearence.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlContainerTypes.ValueListItems.Clear();
		for (int l = 0; l < dtContainersTypes.Rows.Count; l++)
		{
			vlContainerTypes.ValueListItems.Add(dtContainersTypes.Rows[l]["ContainerTypeID"], dtContainersTypes.Rows[l]["ContainerTypeName"].ToString());
		}
		dtServicesQuotations = ServicesQuotations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboQuotation, dtServicesQuotations, "ServiceQuotationID", "ServiceQuotationNo");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int m = 0; m < dtServices.Rows.Count; m++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[m]["ServiceID"], dtServices.Rows[m]["ServiceName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlExpenses.ValueListItems.Clear();
		for (int n = 0; n < dtExpenses.Rows.Count; n++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[n]["ExpenseID"], dtExpenses.Rows[n]["ExpenseName"].ToString());
		}
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int num = 0; num < dtAccounts.Rows.Count; num++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[num]["AccountID"], dtAccounts.Rows[num]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtEmployees = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlEmployees.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtEmployees.Rows.Count; num2++)
			{
				vlEmployees.ValueListItems.Add(dtEmployees.Rows[num2]["SubAccountID"], dtEmployees.Rows[num2]["Name"].ToString());
			}
		}
		dtFreightForwarder = FreightForwarders.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFreightForwarder, dtFreightForwarder, "FreightForwarderID", "FreightForwarderName");
		dtExporter = Exporters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboExporter, dtExporter, "ExporterID", "ExporterName");
		dtCustomsBrokers = CustomsBrokers.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("CustomsClearence", "Reports", "frmOperationsDocuments"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDetails = OperationsServices.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtOperationItems = OperationsItems.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtOperationExpenses = OperationsExpenses.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBillsOfLading = BillsOfLading.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtCertificatesOfOrigin = CertificatesOfOrigin.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtPhytosanitaryCertificates = PhytosanitaryCertificates.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
		InitGridExpenses();
		((UltraGridBase)ULGBillsOFLading).DataSource = dtBillsOfLading;
		InitGridBills();
		((UltraGridBase)ULGCertificatesOFOrigin).DataSource = dtCertificatesOfOrigin;
		InitGridCertificates();
		((UltraGridBase)ULGPhytosanitaryCertificates).DataSource = dtPhytosanitaryCertificates;
		InitGridPhytosanitary();
		((UltraGridBase)ULGDataItems).DataSource = dtOperationItems;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGReports).DataSource = dtReports;
		InitGridReports();
		InitGrid();
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

	private void ULGReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster != null && e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGReports).ActiveRow != null && !CanOpenLetter)
		{
			CanOpenLetter = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", string.Concat(",", drMaster["OperationID"], ","));
			if (e.Cell.Row.Cells["ProcedureName"].Value.ToString() == "BillsOfLading")
			{
				GlobalVariables.ReportDocument.SetParameterValue("@BillOfLadingIDs", "-1");
			}
			if (e.Cell.Row.Cells["ProcedureName"].Value.ToString() == "CertificatesOfOrigin")
			{
				GlobalVariables.ReportDocument.SetParameterValue("@CertificateOfOriginIDs", "-1");
			}
			if (e.Cell.Row.Cells["ProcedureName"].Value.ToString() == "PhytosanitaryCertificates")
			{
				GlobalVariables.ReportDocument.SetParameterValue("@PhytosanitaryCertificateIDs", "-1");
			}
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

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataItems);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlTaxes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["OperationItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BotanicalNameID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemName"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PackingTypeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitQty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerTypeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerNumber"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitGrossWeight"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["GrossWeight"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitNetWeight"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["NetWeight"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceDate"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DataLoggerInfo"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["FarmCode"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["StationCode"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["LotNo"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PermitNo"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemDescription"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.052);
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BotanicalNameID"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم العلمي" : "Botanical Name");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الصنف" : "Item Name");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PackingTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الطرد" : "Package");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المبدئية" : "Init Qty");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? " الكمية الفعلية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاوية" : "Container Type");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوية" : "Container No.");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitGrossWeight"].Header).Caption = (GlobalVariables.IsArabic ? " الوزن الاجمالي المبدئي" : "Init Gross Weight");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["GrossWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الاجمالي الفعلي" : "Gross Weight");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitNetWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الصافي المبدئي" : "Init Net Weight");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["NetWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الصافي الفعلي" : "Net Weight");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفاتورة" : "Client Invoice No");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الفاتورة" : "Client Invoice Date");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DataLoggerInfo"].Header).Caption = (GlobalVariables.IsArabic ? "قياس الحرارة" : "Data Logger Info");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["FarmCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود المزرعة" : "Farm Code");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["StationCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود المحطة" : "Station Code");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["LotNo"].Header).Caption = (GlobalVariables.IsArabic ? "LotNo" : "Lot No.");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PermitNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التصريح" : "Permit No.");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضاعة" : "Description");
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BotanicalNameID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PackingTypeID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitQty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerTypeID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerNumber"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitGrossWeight"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["GrossWeight"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitNetWeight"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["NetWeight"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ClientInvoiceDate"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["DataLoggerInfo"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["FarmCode"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["StationCode"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["LotNo"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PermitNo"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemDescription"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BotanicalNameID"].ValueList = (IValueList)(object)vlBotanicalNames;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["PackingTypeID"].ValueList = (IValueList)(object)vlPackageTypes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ContainerTypeID"].ValueList = (IValueList)(object)vlContainerTypes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitGrossWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["GrossWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["InitNetWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["NetWeight"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BusinessLayer.CustomsClearence.Operations.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboLoadingPorts).ValueChanged -= cboLoadingPorts_ValueChanged;
			((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["OperationNo"].ToString();
			((Control)(object)txtClientInvoiceNo).Text = drMaster["ClientInvoiceNo"].ToString();
			((Control)(object)txtClientName).Text = drMaster["ClientName"].ToString();
			((Control)(object)txtCertificateNo).Text = drMaster["CertificateNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtVesselName).Text = drMaster["VesselName"].ToString();
			rbIsDirect.Checked = Convert.ToBoolean(drMaster["IsDirect"]);
			rbIsQuotation.Checked = Convert.ToBoolean(drMaster["IsQuotation"]);
			((TextEditorControlBase)cboQuotation).Value = drMaster["ServiceQuotationID"];
			((TextEditorControlBase)cboCountry).Value = drMaster["CountryID"];
			((TextEditorControlBase)cboPOLCountries).Value = drMaster["POLCountryID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboExportType).Value = drMaster["ExportTypeID"];
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboLoadingPorts).Value = drMaster["SeaPortID"];
			((TextEditorControlBase)cboSeaPortOfDischarge).Value = drMaster["SeaPortOfDischargeID"];
			((TextEditorControlBase)cboCertificateOpeningSeaPort).Value = drMaster["CertificateOpeningSeaPortID"];
			((TextEditorControlBase)cboSubAccountName).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboFreightForwarder).Value = drMaster["FreightForwarderID"];
			((TextEditorControlBase)cboExporter).Value = drMaster["ExporterID"];
			((TextEditorControlBase)cboCustomsBrokers).Value = drMaster["CustomsBrokerID"];
			dtpDate.Value = drMaster["OperationDate"];
			dtpCertificateDate.Value = drMaster["CertificateDate"];
			dtpCancelDate.Value = drMaster["CancelledDate"];
			dtpShippingDate.Value = drMaster["ShippingDate"];
			dtpRevisedDate.Value = drMaster["RevisedDate"];
			dtpBrokerFinishedDate.Value = drMaster["BrokerFinishedDate"];
			dtpOriginCertificateDate.Value = drMaster["OriginCertificateDate"];
			((UltraToggleEditorBase)chkIsOriginCountry).Checked = bool.Parse(drMaster["IsOriginCountry"].ToString());
			((UltraToggleEditorBase)chkClosed).Checked = bool.Parse(drMaster["Closed"].ToString());
			((UltraToggleEditorBase)chkIsCanceled).Checked = bool.Parse(drMaster["IsCancelled"].ToString());
			((UltraToggleEditorBase)chkIsCompass).Checked = bool.Parse(drMaster["IsCompass"].ToString());
			((UltraToggleEditorBase)chkIsCopies).Checked = bool.Parse(drMaster["IsCopies"].ToString());
			((UltraToggleEditorBase)chkIsPrePaid).Checked = bool.Parse(drMaster["IsPrePaid"].ToString());
			((UltraToggleEditorBase)chkRevised).Checked = bool.Parse(drMaster["Revised"].ToString());
			((UltraToggleEditorBase)chkBrokerFinished).Checked = bool.Parse(drMaster["BrokerFinished"].ToString());
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboLoadingPorts).ValueChanged += cboLoadingPorts_ValueChanged;
			((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
			dtOperationItems = OperationsItems.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtDetails = OperationsServices.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataItems).DataSource = dtOperationItems;
			CalcTotalQty();
			CalcTotalNetWeight();
			CalcTotalGrossWeight();
			dtOperationExpenses = OperationsExpenses.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
			InitGridExpenses();
			dtBillsOfLading = BillsOfLading.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGBillsOFLading).DataSource = dtBillsOfLading;
			InitGridBills();
			dtCertificatesOfOrigin = CertificatesOfOrigin.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGCertificatesOFOrigin).DataSource = dtCertificatesOfOrigin;
			InitGridCertificates();
			dtPhytosanitaryCertificates = PhytosanitaryCertificates.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGPhytosanitaryCertificates).DataSource = dtPhytosanitaryCertificates;
			InitGridPhytosanitary();
			if (!(bool)drMaster["Approved"])
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
				{
					if ((decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["GrossWeight"].Value > (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitGrossWeight"].Value || (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["NetWeight"].Value > (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitNetWeight"].Value || (int)((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value > (int)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitQty"].Value)
					{
						((AppearanceBase)((UltraGridBase)ULGDataItems).Rows[i].Appearance).BackColor = Color.Yellow;
					}
					else
					{
						((AppearanceBase)((UltraGridBase)ULGDataItems).Rows[i].Appearance).BackColor = Control.DefaultBackColor;
					}
				}
			}
			InitGrid();
			if (drMaster["Approved"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
				UltraButton obj = btnCreateExpense;
				UltraButton obj2 = btnAddBL;
				UltraButton obj3 = btnAddCertOfOrigin;
				bool flag = (((Control)(object)btnAddPhyCert).Enabled = false);
				bool flag3 = (((Control)(object)obj3).Enabled = flag);
				bool enabled = (((Control)(object)obj2).Enabled = flag3);
				((Control)(object)obj).Enabled = enabled;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
				UltraButton obj4 = btnCreateExpense;
				UltraButton obj5 = btnAddBL;
				UltraButton obj6 = btnAddCertOfOrigin;
				bool flag = (((Control)(object)btnAddPhyCert).Enabled = true);
				bool flag3 = (((Control)(object)obj6).Enabled = flag);
				bool enabled = (((Control)(object)obj5).Enabled = flag3);
				((Control)(object)obj4).Enabled = enabled;
			}
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
		((Control)(object)btnCreateExpense).Visible = CanAddNewOperationExpenses && NavMode;
		((Control)(object)btnAddBL).Visible = CanAddBill && NavMode;
		((Control)(object)btnAddPhyCert).Visible = CanAddPhyCert && NavMode;
		((Control)(object)btnAddCertOfOrigin).Visible = CanAddCertOfOrigin && NavMode;
		((EditorButtonControlBase)txtClientInvoiceNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCertificateNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtClientName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVesselName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLoadingPorts).ReadOnly = NavMode || (Updating && drMaster != null && int.Parse(drMaster["InvoicesCount"].ToString()) > 0);
		((EditorButtonControlBase)cboCertificateOpeningSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPOLCountries).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)cboExportType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = true;
		((EditorButtonControlBase)cboSubAccountName).ReadOnly = NavMode || (Updating && drMaster != null && int.Parse(drMaster["InvoicesCount"].ToString()) > 0);
		((EditorButtonControlBase)cboExporter).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFreightForwarder).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSeaPortOfDischarge).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCustomsBrokers).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode || (drMaster != null && int.Parse(drMaster["InvoicesCount"].ToString()) > 0);
		((EditorButtonControlBase)dtpCertificateDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCancelDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpOriginCertificateDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpShippingDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpRevisedDate).ReadOnly = NavMode || Adding || Updating;
		((EditorButtonControlBase)dtpBrokerFinishedDate).ReadOnly = NavMode || Adding || Updating;
		rbIsDirect.Enabled = !NavMode;
		rbIsQuotation.Enabled = !NavMode;
		((Control)(object)chkIsOriginCountry).Enabled = !NavMode;
		((Control)(object)chkClosed).Enabled = !NavMode;
		((Control)(object)chkIsCanceled).Enabled = !NavMode;
		((Control)(object)chkIsCompass).Enabled = !NavMode && (drMaster == null || int.Parse(drMaster["InvoicesCount"].ToString()) <= 0);
		((Control)(object)chkIsCopies).Enabled = !NavMode;
		((Control)(object)chkIsPrePaid).Enabled = !NavMode;
		((Control)(object)chkRevised).Enabled = false;
		((Control)(object)chkBrokerFinished).Enabled = false;
		((Control)(object)btnSeaPortSearch).Visible = !NavMode;
		((Control)(object)btnCertificateOpeningSeaPortSearch).Visible = !NavMode;
		((Control)(object)btnSeaPortOfDischargeSearch).Visible = !NavMode;
		((Control)(object)btnClientAdd).Visible = !NavMode;
		((Control)(object)btnAddExporter).Visible = !NavMode;
		if (Adding || Updating)
		{
			int num = 0;
			if (cboQuotation.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboQuotation).Value.ToString());
			}
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			DataView dataView = new DataView(dtServicesQuotations);
			dataView.RowFilter = " Approved = 1 and BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "ServiceQuotationID", "ServiceQuotationNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num;
			}
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			DataView dataView2 = new DataView(dtCustomsBrokers);
			dataView2.RowFilter = "SeaPortID = " + ((((TextEditorControlBase)cboLoadingPorts).Value == null) ? "-1" : ((TextEditorControlBase)cboLoadingPorts).Value.ToString()) + "And IsActive = 1";
			GlobalFunctions.FillCombo(cboCustomsBrokers, dataView2.ToTable(), "CustomsBrokerID", "CustomsBrokerName");
			((TextEditorControlBase)cboCustomsBrokers).Value = (Adding ? ((object)(-1)) : drMaster["CustomsBrokerID"]);
		}
		else
		{
			int num2 = 0;
			if (cboQuotation.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboQuotation).Value.ToString());
			}
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			GlobalFunctions.FillCombo(cboQuotation, dtServicesQuotations, "ServiceQuotationID", "ServiceQuotationNo");
			if (num2 > 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num2;
			}
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			GlobalFunctions.FillCombo(cboCustomsBrokers, dtCustomsBrokers, "CustomsBrokerID", "CustomsBrokerName");
			((TextEditorControlBase)cboCustomsBrokers).Value = ((drMaster != null) ? drMaster["CustomsBrokerID"] : ((object)(-1)));
		}
		((UltraTabControlBase)UTCDetails).Tabs["Documents"].Visible = NavMode;
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((Adding || Updating) ? 6 : 2);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		((TextEditorControlBase)cboLoadingPorts).ValueChanged -= cboLoadingPorts_ValueChanged;
		((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		rbIsDirect.Checked = true;
		((Control)(object)txtCode).Text = (Adding ? BusinessLayer.CustomsClearence.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboCountry.SelectedIndex = -1;
		cboPOLCountries.SelectedIndex = -1;
		if (dtCountries.Rows.Count > 0)
		{
			((TextEditorControlBase)cboPOLCountries).Value = 1;
		}
		cboExportType.SelectedIndex = -1;
		cboPriceType.SelectedIndex = -1;
		cboLoadingPorts.SelectedIndex = -1;
		cboCertificateOpeningSeaPort.SelectedIndex = -1;
		cboSubAccountName.SelectedIndex = -1;
		cboSeaPortOfDischarge.SelectedIndex = -1;
		cboExporter.SelectedIndex = -1;
		cboFreightForwarder.SelectedIndex = -1;
		cboCustomsBrokers.SelectedIndex = -1;
		((TextEditorControlBase)txtClientInvoiceNo).Clear();
		((TextEditorControlBase)txtCertificateNo).Clear();
		((TextEditorControlBase)txtClientName).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		cboCurrency.SelectedIndex = ((((DisposableObjectCollectionBase)cboCurrency.Items).Count <= 0) ? (-1) : 0);
		((Control)(object)txtExchangeRate).Text = ((((TextEditorControlBase)cboCurrency).Value != null && dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString()).Length != 0) ? dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString() : "");
		((TextEditorControlBase)txtVesselName).Clear();
		dtpCertificateDate.Value = DBNull.Value;
		dtpCancelDate.Value = DBNull.Value;
		dtpShippingDate.Value = DBNull.Value;
		dtpOriginCertificateDate.Value = DBNull.Value;
		dtpBrokerFinishedDate.Value = DBNull.Value;
		dtpRevisedDate.Value = DBNull.Value;
		((UltraToggleEditorBase)chkClosed).Checked = false;
		((UltraToggleEditorBase)chkIsCanceled).Checked = false;
		((UltraToggleEditorBase)chkIsCompass).Checked = false;
		((UltraToggleEditorBase)chkIsCopies).Checked = false;
		((UltraToggleEditorBase)chkIsOriginCountry).Checked = false;
		((UltraToggleEditorBase)chkBrokerFinished).Checked = false;
		((UltraToggleEditorBase)chkRevised).Checked = false;
		((UltraToggleEditorBase)chkIsPrePaid).Checked = true;
		((DataTable)((UltraGridBase)ULGDataExpenses).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataItems).DataSource).Rows.Clear();
		((Control)(object)txtTotalQty).Text = "0";
		((Control)(object)txtTotalNetWeight).Text = "0";
		((Control)(object)txtTotalGrossWeight).Text = "0";
		((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboLoadingPorts).ValueChanged += cboLoadingPorts_ValueChanged;
		dtpDate.ValueChanged += dtpDate_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value || dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال التاريخ " : "Please Enter The Date");
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الرقم " : "Please Enter No.");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboSubAccountName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار اسم العميل" : "Please Select Client");
			((TextEditorControlBase)cboSubAccountName).Focus();
			cboSubAccountName.DropDown();
			return false;
		}
		if (rbIsQuotation.Checked && cboQuotation.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض السعر" : "Please Select Quotation No");
			((TextEditorControlBase)cboQuotation).Focus();
			cboQuotation.DropDown();
			return false;
		}
		if (cboLoadingPorts.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار ميناء التحميل " : "Please Select SeaPort");
			((TextEditorControlBase)cboLoadingPorts).Focus();
			cboLoadingPorts.DropDown();
			return false;
		}
		if (cboCertificateOpeningSeaPort.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار  ميناء فتح الشهادة " : "Please Select The Certificate Opening SeaPort");
			((TextEditorControlBase)cboCertificateOpeningSeaPort).Focus();
			cboCertificateOpeningSeaPort.DropDown();
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع السعر" : "Please Select Price Type");
			((TextEditorControlBase)cboPriceType).Focus();
			cboPriceType.DropDown();
			return false;
		}
		if (cboExportType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار  نظام التصدير" : "Please Select The Export Type");
			((TextEditorControlBase)cboExportType).Focus();
			cboExportType.DropDown();
			return false;
		}
		if (cboCountry.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الدولة " : "Please Select Country");
			((TextEditorControlBase)cboCountry).Focus();
			cboCountry.DropDown();
			return false;
		}
		if (((Control)(object)txtClientName).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال  اسم المصدر  " : "Please Enter The Exporter");
			((TextEditorControlBase)txtClientName).Focus();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_Operations", "OperationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BusinessLayer.CustomsClearence.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الخدمة  ", "Please Enter Service Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].DroppedDown = true;
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
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["InvoiceQty"].Value.ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("الكميه أقل من الكمية المفوترة", "Quantity Shouldn't be Less the Invoice Quantity");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["PackingTypeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال نوع الطرد  ", "Please Enter Packing Type");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["PackingTypeID"];
				((UltraGridBase)ULGDataItems).Rows[j].Cells["PackingTypeID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerTypeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال نوع الحاوية  ", "Please Enter Container Type");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerTypeID"];
				((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerTypeID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["Qty"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["Qty"].Value.ToString()) <= 0)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Qty ");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["Qty"];
				ULGDataItems.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["NetWeight"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["NetWeight"].Value.ToString()), 8) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الوزن الصافي  ", "Please Enter Net Weight");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["NetWeight"];
				ULGDataItems.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["GrossWeight"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["GrossWeight"].Value.ToString()), 8) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الوزن الكلي   ", "Please Enter Gross Weight");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["GrossWeight"];
				ULGDataItems.PerformAction((UltraGridAction)24);
				return false;
			}
			if (decimal.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["GrossWeight"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGDataItems).Rows[j].Cells["NetWeight"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("الوزن الكلي اقل من الوزن الصافي", "Gross Weight Is Less Than Net Weight");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["GrossWeight"];
				ULGDataItems.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerNumber"].Value.ToString().Trim() != "" && ((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerNumber"].Value.ToString().Trim().Length != 11)
			{
				GlobalVariables.InformationMB.Show("رقم الحاوية غير صحيح", "Container Number Is Not Correct");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[j].Cells["ContainerNumber"];
				ULGDataItems.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Expected O, but got Unknown
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Expected O, but got Unknown
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Expected O, but got Unknown
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Expected O, but got Unknown
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BusinessLayer.CustomsClearence.Operations.Insert_Update("-1", ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboFreightForwarder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFreightForwarder).Value.ToString(), (cboCustomsBrokers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustomsBrokers).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, ((UltraToggleEditorBase)chkIsCompass).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPrePaid).Checked ? "1" : "0", (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), (cboExporter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExporter).Value.ToString(), (((Control)(object)txtClientName).Text == "") ? "Null" : ((Control)(object)txtClientName).Text, (((Control)(object)txtVesselName).Text == "") ? "Null" : ((Control)(object)txtVesselName).Text, (dtpOriginCertificateDate.Value == null) ? "Null" : dtpOriginCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpShippingDate.Value == null) ? "Null" : dtpShippingDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPOLCountries.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPOLCountries).Value.ToString(), (cboLoadingPorts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingPorts).Value.ToString(), (cboCertificateOpeningSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCertificateOpeningSeaPort).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboSeaPortOfDischarge.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSeaPortOfDischarge).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (((Control)(object)txtCertificateNo).Text == "") ? "Null" : ((Control)(object)txtCertificateNo).Text, (dtpCertificateDate.Value == null) ? "Null" : dtpCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtClientInvoiceNo).Text == "") ? "Null" : ((Control)(object)txtClientInvoiceNo).Text, (cboExportType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExportType).Value.ToString(), ((UltraToggleEditorBase)chkIsCopies).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsOriginCountry).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCanceled).Checked ? "1" : "0", (dtpCancelDate.Value == null) ? "Null" : dtpCancelDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkBrokerFinished).Checked ? "1" : "0", (dtpBrokerFinishedDate.Value == null) ? "Null" : dtpBrokerFinishedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkRevised).Checked ? "1" : "0", (dtpRevisedDate.Value == null) ? "Null" : dtpRevisedDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).UpdateData();
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = -1;
					((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				OperationsServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count > 0)
			{
				ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
				((UltraGridBase)ULGDataItems).UpdateData();
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataItems).Rows[j].Cells["OperationItemID"].Value = -1;
					((UltraGridBase)ULGDataItems).Rows[j].Cells["OperationID"].Value = num;
					((UltraGridBase)ULGDataItems).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				OperationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataItems).DataSource, GlobalVariables.UserID);
				ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		if (OperationsExpenses.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم عمل مصروف عليها", "Cannot Delete This Transaction Because there Are Expenses Made on It ");
			return;
		}
		if (Invoices.SelectByOperationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تم عمل فاتورة عليها", "Cannot Delete This Transaction Because there Are Invoice Made on It ");
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

	public override void UpdateData()
	{
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Expected O, but got Unknown
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Expected O, but got Unknown
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Expected O, but got Unknown
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BusinessLayer.CustomsClearence.Operations.Insert_Update(drMaster["OperationID"].ToString(), ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsQuotation.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboFreightForwarder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFreightForwarder).Value.ToString(), (cboCustomsBrokers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustomsBrokers).Value.ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, ((UltraToggleEditorBase)chkIsCompass).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPrePaid).Checked ? "1" : "0", (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), (cboExporter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExporter).Value.ToString(), (((Control)(object)txtClientName).Text == "") ? "Null" : ((Control)(object)txtClientName).Text, (((Control)(object)txtVesselName).Text == "") ? "Null" : ((Control)(object)txtVesselName).Text, (dtpOriginCertificateDate.Value == null) ? "Null" : dtpOriginCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpShippingDate.Value == null) ? "Null" : dtpShippingDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPOLCountries.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPOLCountries).Value.ToString(), (cboLoadingPorts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingPorts).Value.ToString(), (cboCertificateOpeningSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCertificateOpeningSeaPort).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboSeaPortOfDischarge.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSeaPortOfDischarge).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (((Control)(object)txtCertificateNo).Text == "") ? "Null" : ((Control)(object)txtCertificateNo).Text, (dtpCertificateDate.Value == null) ? "Null" : dtpCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtClientInvoiceNo).Text == "") ? "Null" : ((Control)(object)txtClientInvoiceNo).Text, (cboExportType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExportType).Value.ToString(), ((UltraToggleEditorBase)chkIsCopies).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsOriginCountry).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCanceled).Checked ? "1" : "0", (dtpCancelDate.Value == null) ? "Null" : dtpCancelDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkBrokerFinished).Checked ? "1" : "0", (dtpBrokerFinishedDate.Value == null) ? "Null" : dtpBrokerFinishedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkRevised).Checked ? "1" : "0", (dtpRevisedDate.Value == null) ? "Null" : dtpRevisedDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value.ToString() + ",";
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("CST_OperationsServices", "OperationID", drMaster["OperationID"].ToString(), "OperationServiceID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			string text2 = ",";
			ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
			((UltraGridBase)ULGDataItems).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataItems).Rows[j].Cells["OperationID"].Value = num;
				((UltraGridBase)ULGDataItems).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text2 = text2 + ((UltraGridBase)ULGDataItems).Rows[j].Cells["OperationItemID"].Value.ToString() + ",";
			}
			((UltraGridBase)ULGDataItems).UpdateData();
			ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
			Main.DeleteForUpdate("CST_OperationsItems", "OperationID", drMaster["OperationID"].ToString(), "OperationItemID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count > 0)
			{
				OperationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataItems).DataSource, GlobalVariables.UserID);
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
		base.DeleteData();
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsItems.DeleteVirtualByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			OperationsServices.DeleteVirtualByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
			BusinessLayer.CustomsClearence.Operations.DeleteVirtual(drMaster["OperationID"].ToString(), GlobalVariables.UserID);
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
		object value = ((TextEditorControlBase)cboSubAccountName).Value;
		object value2 = ((TextEditorControlBase)cboPOLCountries).Value;
		object value3 = ((TextEditorControlBase)cboCountry).Value;
		object value4 = ((TextEditorControlBase)cboPriceType).Value;
		object value5 = ((TextEditorControlBase)cboExportType).Value;
		object value6 = ((TextEditorControlBase)cboCurrency).Value;
		object value7 = ((TextEditorControlBase)cboQuotation).Value;
		object value8 = ((TextEditorControlBase)cboFreightForwarder).Value;
		object value9 = ((TextEditorControlBase)cboExporter).Value;
		((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
		((TextEditorControlBase)cboPOLCountries).ValueChanged -= cboPOLCountries_ValueChanged;
		((TextEditorControlBase)cboCountry).ValueChanged -= cboCountry_ValueChanged;
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		GlobalFunctions.FillCombo(cboPOLCountries, dtCountries, "CountryID", "CountryName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtExportTypes = ExportsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboExportType, dtExportTypes, "ExportTypeID", "ExportTypeName");
		dtBotanicalNames = BotanicalNames.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBotanicalNames.ValueListItems.Clear();
		for (int i = 0; i < dtBotanicalNames.Rows.Count; i++)
		{
			vlBotanicalNames.ValueListItems.Add(dtBotanicalNames.Rows[i]["BotanicalNameID"], dtBotanicalNames.Rows[i]["BotanicalName"].ToString());
		}
		dtPackageTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackageTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPackageTypes.Rows.Count; j++)
		{
			vlPackageTypes.ValueListItems.Add(dtPackageTypes.Rows[j]["PackingTypeID"], dtPackageTypes.Rows[j]["PackingTypeName"].ToString());
		}
		dtContainersTypes = BusinessLayer.CustomsClearence.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlContainerTypes.ValueListItems.Clear();
		for (int k = 0; k < dtContainersTypes.Rows.Count; k++)
		{
			vlContainerTypes.ValueListItems.Add(dtContainersTypes.Rows[k]["ContainerTypeID"], dtContainersTypes.Rows[k]["ContainerTypeName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int l = 0; l < dtTaxes.Rows.Count; l++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[l]["TaxID"], dtTaxes.Rows[l]["TaxName"].ToString());
		}
		FillCurrencyDropDown();
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlExpenses.ValueListItems.Clear();
		for (int m = 0; m < dtExpenses.Rows.Count; m++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[m]["ExpenseID"], dtExpenses.Rows[m]["ExpenseName"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int n = 0; n < dtServices.Rows.Count; n++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[n]["ServiceID"], dtServices.Rows[n]["ServiceName"].ToString());
		}
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int num = 0; num < dtAccounts.Rows.Count; num++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[num]["AccountID"], dtAccounts.Rows[num]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtEmployees = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlEmployees.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtEmployees.Rows.Count; num2++)
			{
				vlEmployees.ValueListItems.Add(dtEmployees.Rows[num2]["SubAccountID"], dtEmployees.Rows[num2]["Name"].ToString());
			}
		}
		dtServicesQuotations = ServicesQuotations.FillCombo(GlobalVariables.BranchIDs);
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtFreightForwarder = FreightForwarders.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFreightForwarder, dtFreightForwarder, "FreightForwarderID", "FreightForwarderName");
		dtExporter = Exporters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboExporter, dtExporter, "ExporterID", "ExporterName");
		((TextEditorControlBase)cboSubAccountName).Value = value;
		((TextEditorControlBase)cboPOLCountries).Value = value2;
		((TextEditorControlBase)cboCountry).Value = value3;
		((TextEditorControlBase)cboPriceType).Value = value4;
		((TextEditorControlBase)cboExportType).Value = value5;
		((TextEditorControlBase)cboCurrency).Value = value6;
		((TextEditorControlBase)cboQuotation).Value = value7;
		((TextEditorControlBase)cboFreightForwarder).Value = value8;
		((TextEditorControlBase)cboExporter).Value = value9;
		((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
		((TextEditorControlBase)cboPOLCountries).ValueChanged += cboPOLCountries_ValueChanged;
		((TextEditorControlBase)cboCountry).ValueChanged += cboCountry_ValueChanged;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTOperationsReport(-1, -1, IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationID"].ToString();
			FillData();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && decimal.Parse(ULGData.ActiveCell.Row.Cells["InvoiceQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && (!bool.Parse(dtServices.Select(" ServiceID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["CanModifyPrice"].ToString()) || (rbIsQuotation.Checked && dtServicesPrices != null && (dtServicesPrices.Select(" ServiceID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString()).Length == 0 || !bool.Parse(dtServicesPrices.Select(" ServiceID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value.ToString())[0]["CanModifyPrice"].ToString())))))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString() != "-1" && InvoicesServices.SelectByOperationServiceID(((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الخدمة لانه تم عمل فاتورة عليها" : "Cannot Delete This Service Because There's an Invoice Made On It");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CST_Operations_E.rpt" : "Rep_CST_Operations_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Expected O, but got Unknown
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtServices.Select(" ServiceID= " + e.Cell.Value.ToString())[0];
			if (dtServicesPrices != null && dtServicesPrices.Rows.Count > 0 && cboPriceType.SelectedIndex > -1)
			{
				DataRow[] array = dtServicesPrices.Select(" ServiceID = " + e.Cell.Value.ToString());
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) + (((UltraToggleEditorBase)chkIsCompass).Checked ? decimal.Parse(array[0]["AdditionalCompassPrice"].ToString()) : 0m);
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj.Value = value;
				}
			}
			e.Cell.Row.Cells["TaxID"].Value = dataRow["TaxID"];
			if (e.Cell.Row.Cells["TaxID"].Value != DBNull.Value)
			{
				e.Cell.Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + e.Cell.Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(e.Cell.Row.Cells["TotalPrice"].Value.ToString());
			}
			else
			{
				e.Cell.Row.Cells["TaxValue"].Value = 0;
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			if (e.Cell.Row.Cells["TaxID"].Value != DBNull.Value)
			{
				e.Cell.Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + e.Cell.Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(e.Cell.Row.Cells["TotalPrice"].Value.ToString());
			}
			else
			{
				e.Cell.Row.Cells["TaxValue"].Value = 0;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSeaPorts);
			dataView.RowFilter = "CountryID = " + ((((TextEditorControlBase)cboCountry).Value == null) ? "-1" : ((TextEditorControlBase)cboCountry).Value.ToString());
			cboSeaPortOfDischarge.DataSource = dataView;
			cboSeaPortOfDischarge.DisplayMember = "SeaPortName";
			cboSeaPortOfDischarge.ValueMember = "SeaPortID";
		}
	}

	private void cboPOLCountries_ValueChanged(object sender, EventArgs e)
	{
		if (cboPOLCountries.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSeaPorts);
			dataView.RowFilter = "CountryID = " + ((((TextEditorControlBase)cboPOLCountries).Value == null) ? "-1" : ((TextEditorControlBase)cboPOLCountries).Value.ToString());
			cboLoadingPorts.DataSource = dataView;
			cboLoadingPorts.DisplayMember = "SeaPortName";
			cboLoadingPorts.ValueMember = "SeaPortID";
			cboCertificateOpeningSeaPort.DataSource = dataView;
			cboCertificateOpeningSeaPort.DisplayMember = "SeaPortName";
			cboCertificateOpeningSeaPort.ValueMember = "SeaPortID";
		}
	}

	private void lblShippingDate_Click(object sender, EventArgs e)
	{
	}

	private void lblExporter_Click(object sender, EventArgs e)
	{
	}

	private void chkIsCompass_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (dtServicesPrices != null && dtServicesPrices.Rows.Count > 0)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow[] array = dtServicesPrices.Select("ServiceID = " + ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString());
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) + (((UltraToggleEditorBase)chkIsCompass).Checked ? decimal.Parse(array[0]["AdditionalCompassPrice"].ToString()) : 0m);
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
					obj.Value = value;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + ((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
				}
				else
				{
					((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = 0;
				}
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.dtForms.Select("IsFullName = 1 and FormFullName = 'ERP.Sales.MasterData.frmClientsTree'").Length == 0 || !GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("IsFullName = 1 and FormFullName = 'ERP.Sales.MasterData.frmClientsTree'")[0]["FormID"].ToString(), "Adding"))
		{
			return;
		}
		frmClientsTree frmClientsTree2 = new frmClientsTree(_AddFromAnotherForm: true);
		frmClientsTree2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmClientsTree2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Clients");
		frmClientsTree2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.Sales.MasterData.frmClientsTree'")[0];
		frmClientsTree2.ShowDialog();
		if (frmClientsTree2.SubAccountID != 0m)
		{
			GlobalFunctions.SyncMasterData("frmSubAccountsTree");
			dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (Adding || Updating)
			{
				DataView dataView = new DataView(dtSubAccounts);
				dataView.RowFilter = "IsActive = 1";
				DataTable dt = dataView.ToTable();
				GlobalFunctions.FillCombo(cboSubAccountName, dt, "SubAccountID", "SubAccountName");
			}
			else
			{
				GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
			}
			((TextEditorControlBase)cboSubAccountName).Value = frmClientsTree2.SubAccountID;
		}
	}

	private void frmOperations_Load(object sender, EventArgs e)
	{
	}

	private void btnAddExporter_Click(object sender, EventArgs e)
	{
		if (cboSubAccountName.SelectedIndex > -1)
		{
			AddExporter();
			return;
		}
		GlobalVariables.InformationMB.Show(" قم باختيار عميل أولا ", "Choose a Client first");
		((TextEditorControlBase)cboSubAccountName).Focus();
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
	}

	private void lblPriceType_Click(object sender, EventArgs e)
	{
	}

	private void lblCertificateOpeningSeaPort_Click(object sender, EventArgs e)
	{
	}

	private void cboCertificateOpeningSeaPort_ValueChanged(object sender, EventArgs e)
	{
	}

	private void lblOriginCertificateDate_Click(object sender, EventArgs e)
	{
	}

	private void dtpShippingDate_ValueChanged(object sender, EventArgs e)
	{
	}

	private void ULGDataItems_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataItems_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalcTotalQty();
		CalcTotalNetWeight();
		CalcTotalGrossWeight();
	}

	private void txtCreateCertificate_Click(object sender, EventArgs e)
	{
	}

	private void txtCreateCertificateOfOrigin_Click(object sender, EventArgs e)
	{
	}

	private void btnAddBL_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmBillsOfLading frmBillsOfLading2 = new frmBillsOfLading(drMaster["OperationID"].ToString());
			frmBillsOfLading2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBillsOfLading2.lblTitle).Text = (GlobalVariables.IsArabic ? "بوالص الشحن" : "Bills Of Lading");
			frmBillsOfLading2.ShowDialog();
			dtBillsOfLading = BillsOfLading.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGBillsOFLading).DataSource = dtBillsOfLading;
			InitGridBills();
		}
	}

	private void btnAddPhyCert_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmPhytosanitaryCertificates frmPhytosanitaryCertificates2 = new frmPhytosanitaryCertificates(drMaster["OperationID"].ToString());
			frmPhytosanitaryCertificates2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmPhytosanitaryCertificates2.lblTitle).Text = (GlobalVariables.IsArabic ? "الشهادات الزراعية" : "Phytosanitary Certificates");
			frmPhytosanitaryCertificates2.ShowDialog();
			dtPhytosanitaryCertificates = PhytosanitaryCertificates.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGPhytosanitaryCertificates).DataSource = dtPhytosanitaryCertificates;
			InitGridPhytosanitary();
		}
	}

	private void btnAddCertOfOrigin_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmCertificatesOfOrigin frmCertificatesOfOrigin2 = new frmCertificatesOfOrigin(drMaster["OperationID"].ToString());
			frmCertificatesOfOrigin2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmCertificatesOfOrigin2.lblTitle).Text = (GlobalVariables.IsArabic ? "شهادات المنشأ" : "Certificates Of Origin");
			frmCertificatesOfOrigin2.ShowDialog();
			dtCertificatesOfOrigin = CertificatesOfOrigin.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGCertificatesOFOrigin).DataSource = dtCertificatesOfOrigin;
			InitGridCertificates();
		}
	}

	private void radionButton_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblQuotation).Visible = rbIsQuotation.Checked;
		((Control)(object)cboQuotation).Visible = rbIsQuotation.Checked;
		if (Adding || Updating)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress -= txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
			cboQuotation.SelectedIndex = -1;
			cboSubAccountName.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress += txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
		}
	}

	private void ULGBillsOFLading_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGBillsOFLading).ActiveRow).Selected = true;
	}

	private void ULGPhytosanitaryCertificates_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGPhytosanitaryCertificates).ActiveRow).Selected = true;
	}

	private void ULGCertificatesOFOrigin_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGCertificatesOFOrigin).ActiveRow).Selected = true;
	}

	private void btnQuotationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SLQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotation).Value = num;
		}
	}

	private void ULGDataItems_AfterRowUpdate(object sender, RowEventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			if ((decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["GrossWeight"].Value > (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitGrossWeight"].Value || (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["NetWeight"].Value > (decimal)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitNetWeight"].Value || (int)((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value > (int)((UltraGridBase)ULGDataItems).Rows[i].Cells["InitQty"].Value)
			{
				((AppearanceBase)((UltraGridBase)ULGDataItems).Rows[i].Appearance).BackColor = Color.Yellow;
			}
			else
			{
				((AppearanceBase)((UltraGridBase)ULGDataItems).Rows[i].Appearance).BackColor = Control.DefaultBackColor;
			}
		}
	}

	private void ULGDataItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F6 || (!Adding && !Updating))
		{
			return;
		}
		if (((UltraGridBase)ULGDataItems).ActiveRow != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "FarmCode")
		{
			frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "كود المزرعة" : "Farm Code", _IsInt: false, _IsNumeric: false, ULGDataItems.ActiveCell.Value.ToString());
			frmEnterValue2.WindowState = FormWindowState.Normal;
			if (frmEnterValue2.ShowDialog() == DialogResult.OK)
			{
				ULGDataItems.ActiveCell.Value = frmEnterValue2.Value;
			}
		}
		if (((UltraGridBase)ULGDataItems).ActiveRow != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "StationCode")
		{
			frmEnterValue frmEnterValue3 = new frmEnterValue(GlobalVariables.IsArabic ? "كود المحطة" : "Station Code", _IsInt: false, _IsNumeric: false, ULGDataItems.ActiveCell.Value.ToString());
			frmEnterValue3.WindowState = FormWindowState.Normal;
			if (frmEnterValue3.ShowDialog() == DialogResult.OK)
			{
				ULGDataItems.ActiveCell.Value = frmEnterValue3.Value;
			}
		}
		if (((UltraGridBase)ULGDataItems).ActiveRow != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "LotNo")
		{
			frmEnterValue frmEnterValue4 = new frmEnterValue(GlobalVariables.IsArabic ? "LotNo" : "Lot No.", _IsInt: false, _IsNumeric: false, ULGDataItems.ActiveCell.Value.ToString());
			frmEnterValue4.WindowState = FormWindowState.Normal;
			if (frmEnterValue4.ShowDialog() == DialogResult.OK)
			{
				ULGDataItems.ActiveCell.Value = frmEnterValue4.Value;
			}
		}
		if (((UltraGridBase)ULGDataItems).ActiveRow != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "PermitNo")
		{
			frmEnterValue frmEnterValue5 = new frmEnterValue(GlobalVariables.IsArabic ? "رقم التصريح" : "Permit No.", _IsInt: false, _IsNumeric: false, ULGDataItems.ActiveCell.Value.ToString());
			frmEnterValue5.WindowState = FormWindowState.Normal;
			if (frmEnterValue5.ShowDialog() == DialogResult.OK)
			{
				ULGDataItems.ActiveCell.Value = frmEnterValue5.Value;
			}
		}
	}

	private void cboQuotation_ValueChanged(object sender, EventArgs e)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Expected O, but got Unknown
		if (cboQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			DataRow dataRow = dtServicesQuotations.Select(" ServiceQuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0];
			if (cboPriceType.SelectedIndex > -1)
			{
				dtServicesPrices = ServicesQuotationsDetailsPrices.FillByServiceQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), ((TextEditorControlBase)cboPriceType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			else
			{
				dtServicesPrices = null;
			}
			((TextEditorControlBase)cboCurrency).Value = dataRow["CurrencyID"];
			if (dtServicesPrices != null && dtServicesPrices.Rows.Count > 0)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					DataRow[] array = dtServicesPrices.Select("ServiceID = " + ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString());
					if (array != null && array.Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) + (((UltraToggleEditorBase)chkIsCompass).Checked ? decimal.Parse(array[0]["AdditionalCompassPrice"].ToString()) : 0m);
						((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					}
					else
					{
						UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
						object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
						obj.Value = value;
					}
					if (((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + ((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
					}
					else
					{
						((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = 0;
					}
				}
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtDetails.Rows.Clear();
		}
	}

	private void AddExporter()
	{
		if (Main.CheckForValue("CST_Exporters", "SubAccountID", ((TextEditorControlBase)cboSubAccountName).Value.ToString(), "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" اسم المصدر متواجد من قبل ", "Exporter Name Already Exist");
			((TextEditorControlBase)cboSubAccountName).Focus();
			return;
		}
		frmAttachExporters frmAttachExporters2 = new frmAttachExporters();
		((Control)(object)frmAttachExporters2.lblTitle).Text = (GlobalVariables.IsArabic ? "المصدرين" : "Exporters");
		if (frmAttachExporters2.ShowDialog() != DialogResult.Cancel)
		{
			string parentID = frmAttachExporters2.ParentID;
			string exportNo = frmAttachExporters2.ExportNo;
			int num = Exporters.Insert_Client_Exporter(((TextEditorControlBase)cboSubAccountName).Value.ToString(), parentID, exportNo, GlobalVariables.UserID, IsFromServer: true);
			dtExporter = Exporters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboExporter, dtExporter, "ExporterID", "ExporterName");
			((TextEditorControlBase)cboExporter).Value = num;
		}
	}

	private void cboLoadingPorts_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (cboLoadingPorts.SelectedIndex > -1)
		{
			if (Adding)
			{
				((TextEditorControlBase)cboCertificateOpeningSeaPort).Value = ((TextEditorControlBase)cboLoadingPorts).Value;
			}
			DataRow dataRow = dtSeaPorts.Select("SeaPortID = " + ((TextEditorControlBase)cboLoadingPorts).Value.ToString())[0];
			if (dataRow["PriceTypeID"] != DBNull.Value)
			{
				((TextEditorControlBase)cboPriceType).Value = dataRow["PriceTypeID"];
				if (rbIsQuotation.Checked)
				{
					if (cboQuotation.SelectedIndex > -1)
					{
						dtServicesPrices = ServicesQuotationsDetailsPrices.FillByServiceQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), ((TextEditorControlBase)cboPriceType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
					}
					else
					{
						dtServicesPrices = null;
					}
				}
				else
				{
					dtServicesPrices = ServicesPrices.GetPrices(dataRow["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
				}
				if (dtServicesPrices != null && dtServicesPrices.Rows.Count > 0)
				{
					for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
					{
						DataRow[] array = dtServicesPrices.Select("ServiceID = " + ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString());
						if (array != null && array.Length != 0)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) + (((UltraToggleEditorBase)chkIsCompass).Checked ? decimal.Parse(array[0]["AdditionalCompassPrice"].ToString()) : 0m);
							((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
						}
						else
						{
							UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
							object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
							obj.Value = value;
						}
						if (((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value != DBNull.Value)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + ((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
						}
						else
						{
							((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value = 0;
						}
					}
				}
				else if (rbIsQuotation.Checked)
				{
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
					{
						UltraGridCell obj3 = ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"];
						UltraGridCell obj4 = ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"];
						object obj5 = (((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value = 0);
						object value = (obj4.Value = obj5);
						obj3.Value = value;
					}
				}
			}
			else
			{
				cboPriceType.SelectedIndex = -1;
				dtServicesPrices = null;
				if (rbIsQuotation.Checked)
				{
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
					{
						UltraGridCell obj8 = ((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"];
						UltraGridCell obj9 = ((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"];
						object obj5 = (((UltraGridBase)ULGData).Rows[k].Cells["TaxValue"].Value = 0);
						object value = (obj9.Value = obj5);
						obj8.Value = value;
					}
				}
			}
			DataView dataView = new DataView(dtCustomsBrokers);
			dataView.RowFilter = ((Updating || Adding) ? ("SeaPortID = " + ((((TextEditorControlBase)cboLoadingPorts).Value == null) ? "-1" : ((TextEditorControlBase)cboLoadingPorts).Value.ToString()) + "And IsActive = 1") : ("SeaPortID = " + ((((TextEditorControlBase)cboLoadingPorts).Value == null) ? "-1" : ((TextEditorControlBase)cboLoadingPorts).Value.ToString())));
			cboCustomsBrokers.DataSource = dataView;
			cboCustomsBrokers.DisplayMember = "CustomsBrokerName";
			cboCustomsBrokers.ValueMember = "CustomsBrokerID";
		}
		else
		{
			if (Adding)
			{
				cboCertificateOpeningSeaPort.SelectedIndex = -1;
			}
			cboPriceType.SelectedIndex = -1;
			dtServicesPrices = null;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnCertificateOpeningSeaPort_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTSeaPortsSearch((((TextEditorControlBase)cboPOLCountries).Value == null) ? (-1) : int.Parse(((TextEditorControlBase)cboPOLCountries).Value.ToString()), IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboCertificateOpeningSeaPort).Value = num;
		}
	}

	private void btnSeaPortSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTSeaPortsSearch((((TextEditorControlBase)cboPOLCountries).Value == null) ? (-1) : int.Parse(((TextEditorControlBase)cboPOLCountries).Value.ToString()), IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLoadingPorts).Value = num;
		}
	}

	private void btnSeaPortOfDischargeSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTSeaPortsSearch((((TextEditorControlBase)cboCountry).Value == null) ? (-1) : int.Parse(((TextEditorControlBase)cboCountry).Value.ToString()), IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSeaPortOfDischarge).Value = num;
		}
	}

	private void btnCreateExpense_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating && drMaster != null && drMaster["OperationID"] != DBNull.Value)
		{
			frmOperationExpenses frmOperationExpenses2 = new frmOperationExpenses(drMaster["OperationID"].ToString());
			frmOperationExpenses2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmOperationExpenses2.lblTitle).Text = (GlobalVariables.IsArabic ? "مصروفات العمليات" : "Operation Expenses");
			frmOperationExpenses2.ShowDialog();
			dtOperationExpenses = OperationsExpenses.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
			InitGridExpenses();
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

	private void cboSubAccountName_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (rbIsQuotation.Checked)
		{
			DataView dataView = new DataView(dtServicesQuotations);
			dataView.RowFilter = " Approved = 1 and BranchID= " + GlobalVariables.CurrentBranchID + ((cboSubAccountName.SelectedIndex > -1) ? (" And SubAccountID = " + ((TextEditorControlBase)cboSubAccountName).Value.ToString()) : "  And SubAccountID = 0 ") + ((dtpDate.Value == null) ? "" : (" And ServiceQuotationDate <= '" + dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "' And ServiceQuotationValidTo >= '" + dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate) + "'"));
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboQuotation, dataTable, "ServiceQuotationID", "ServiceQuotationNo");
			if (dataTable.Rows.Count > 0)
			{
				cboQuotation.SelectedIndex = 0;
			}
		}
		if (cboSubAccountName.SelectedIndex > -1)
		{
			SubAccountID = ((TextEditorControlBase)cboSubAccountName).Value.ToString();
			DataRow[] array = dtExporter.Select("SubAccountID = " + SubAccountID);
			if (array.Length != 0)
			{
				((TextEditorControlBase)cboExporter).Value = array[0]["ExporterID"];
				((Control)(object)btnAddExporter).Visible = false;
			}
			else
			{
				((Control)(object)btnAddExporter).Visible = true;
				cboExporter.SelectedIndex = -1;
			}
			if (((Control)(object)txtClientName).Text == "")
			{
				((Control)(object)txtClientName).Text = ((Control)(object)cboSubAccountName).Text;
			}
			if (cboLoadingPorts.SelectedIndex > -1 && cboPriceType.SelectedIndex == -1)
			{
				dtServicesPrices = null;
			}
		}
		else
		{
			dtServicesPrices = null;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = BusinessLayer.CustomsClearence.Operations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (rbIsQuotation.Checked)
		{
			DataView dataView = new DataView(dtServicesQuotations);
			dataView.RowFilter = " Approved = 1 and BranchID= " + GlobalVariables.CurrentBranchID + ((cboSubAccountName.SelectedIndex > -1) ? (" And SubAccountID = " + ((TextEditorControlBase)cboSubAccountName).Value.ToString()) : "  And SubAccountID = 0 ") + ((dtpDate.Value == null) ? "" : (" And ServiceQuotationDate <= '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "' And ServiceQuotationValidTo >= '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"));
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboQuotation, dataTable, "ServiceQuotationID", "ServiceQuotationNo");
			if (dataTable.Rows.Count > 0)
			{
				cboQuotation.SelectedIndex = 0;
			}
		}
		if (cboLoadingPorts.SelectedIndex > -1)
		{
			if (cboPriceType.SelectedIndex != -1)
			{
				if (!rbIsQuotation.Checked)
				{
				}
			}
			else
			{
				dtServicesPrices = null;
			}
		}
		else
		{
			cboPriceType.SelectedIndex = -1;
			dtServicesPrices = null;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		frmPricesTypesChange frmPricesTypesChange2 = new frmPricesTypesChange(base.Name);
		frmPricesTypesChange2.WindowState = FormWindowState.Normal;
		frmPricesTypesChange2.ShowDialog();
		((TextEditorControlBase)cboPriceType).Value = ((frmPricesTypesChange2.PriceTypeID > 0) ? ((object)frmPricesTypesChange2.PriceTypeID) : ((TextEditorControlBase)cboPriceType).Value);
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void ULGDataItems_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataItems.ActiveCell != null && (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "GrossWeight" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "NetWeight"))
		{
			GlobalFunctions.CheckForNumbers(ULGDataItems.ActiveCell, e);
		}
	}

	private void ULGDataItems_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Expected O, but got Unknown
		ULGDataItems.AfterCellUpdate -= new CellEventHandler(ULGDataItems_AfterCellUpdate);
		if (ULGDataItems.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "GrossWeight" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "NetWeight") && ULGDataItems.ActiveCell.Value == DBNull.Value)
			{
				ULGDataItems.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "Qty")
			{
				CalcTotalQty();
			}
			else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "GrossWeight")
			{
				CalcTotalGrossWeight();
			}
			else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "NetWeight")
			{
				CalcTotalNetWeight();
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "InitQty" && int.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value.ToString()) <= 0)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGDataItems).ActiveRow.Cells["InitQty"].Value;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "InitGrossWeight" && (decimal)((UltraGridBase)ULGDataItems).ActiveRow.Cells["GrossWeight"].Value == 0m)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["GrossWeight"].Value = ((UltraGridBase)ULGDataItems).ActiveRow.Cells["InitGrossWeight"].Value;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "InitNetWeight" && (decimal)((UltraGridBase)ULGDataItems).ActiveRow.Cells["NetWeight"].Value <= 0m)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["NetWeight"].Value = ((UltraGridBase)ULGDataItems).ActiveRow.Cells["InitNetWeight"].Value;
		}
		ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
			{
				GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
			{
				GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
			}
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
			int num = SearchFunctions.CSTServicesSearch(IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = num;
			}
		}
		e.Handled = true;
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			if (((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString());
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGDataExpenses_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataExpenses).ActiveRow).Selected = true;
	}

	public void InitGridBills()
	{
		GlobalFunctions.PrepareGrid(ULGBillsOFLading);
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingID"].DefaultCellValue = -1;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNo"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingDate"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.1);
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.2);
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.3);
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeVATNo"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.1);
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["NotifyName"].Width = (int)((double)((Control)(object)ULGBillsOFLading).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود البوليصة" : "Bill Code");
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البوليصة" : "Bill Date");
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم بوليصة الشحن" : "Bill Of Lading No");
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المستلم" : "Consignee Name");
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeVATNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم الضريبى للمستلم" : "Consignee VAT No");
		((HeaderBase)((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["NotifyName"].Header).Caption = (GlobalVariables.IsArabic ? "Notify" : "Notify Name");
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNo"].Hidden = false;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingDate"].Hidden = false;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Hidden = false;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["ConsigneeVATNo"].Hidden = false;
		((UltraGridBase)ULGBillsOFLading).DisplayLayout.Bands[0].Columns["NotifyName"].Hidden = false;
	}

	public void InitGridCertificates()
	{
		GlobalFunctions.PrepareGrid(ULGCertificatesOFOrigin);
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginNo"].Width = (int)((double)((Control)(object)ULGCertificatesOFOrigin).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginDate"].Width = (int)((double)((Control)(object)ULGCertificatesOFOrigin).Width * 0.2);
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateNumber"].Width = (int)((double)((Control)(object)ULGCertificatesOFOrigin).Width * 0.2);
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCertificatesOFOrigin).Width * 0.5);
		((HeaderBase)((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود الشهادة" : "No");
		((HeaderBase)((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ شهادة المنشأ" : "Date");
		((HeaderBase)((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم شهادة المنشأ" : "CertificateNumber");
		((HeaderBase)((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginNo"].Hidden = false;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateOfOriginDate"].Hidden = false;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["CertificateNumber"].Hidden = false;
		((UltraGridBase)ULGCertificatesOFOrigin).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public void InitGridPhytosanitary()
	{
		GlobalFunctions.PrepareGrid(ULGPhytosanitaryCertificates);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateID"].DefaultCellValue = -1;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateNo"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.05);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectionDate"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.05);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectorsNames"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.15);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["TreatmentDate"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.05);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Concentration"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.1);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.1);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.1);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.1);
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGPhytosanitaryCertificates).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود الشهادة الزراعية" : "Code");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإصدار" : "IssueDate");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الفحص" : "InspectionDate");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectorsNames"].Header).Caption = (GlobalVariables.IsArabic ? "اسماء الفاحصين" : "Inspectors Names");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["TreatmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المعالجة" : "Treatment Date");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Concentration"].Header).Caption = (GlobalVariables.IsArabic ? "التركيز" : "Concentration");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Header).Caption = (GlobalVariables.IsArabic ? "مدة التعرض ودرجة الحرارة" : "Duration And Temperature");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Header).Caption = (GlobalVariables.IsArabic ? "معلومات أخرى" : "Additional Information");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Header).Caption = (GlobalVariables.IsArabic ? "إقرار إضافي" : "Additional Declaration");
		((HeaderBase)((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateNo"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectionDate"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["InspectorsNames"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["TreatmentDate"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Concentration"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Hidden = false;
		((UltraGridBase)ULGPhytosanitaryCertificates).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		UltraCheckEditor obj = chkIsCanceled;
		bool flag = (((UltraToggleEditorBase)chkClosed).Checked = false);
		((UltraToggleEditorBase)obj).Checked = flag;
		dtpCancelDate.Value = null;
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		dtOperationExpenses.Clear();
		dtBillsOfLading.Clear();
		dtPhytosanitaryCertificates.Clear();
		dtCertificatesOfOrigin.Clear();
		drMaster = null;
		SetControls(NavMode: false);
	}

	public void InitGridExpenses()
	{
		GlobalFunctions.PrepareGrid(ULGDataExpenses);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.12);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.12);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.16);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المصروف" : "Expense No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الايصال" : "Receipt No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Header).Caption = (GlobalVariables.IsArabic ? "على حساب العميل" : "On Client Account");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].DefaultCellValue = 0;
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
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected O, but got Unknown
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Expected O, but got Unknown
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Expected O, but got Unknown
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Expected O, but got Unknown
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Expected O, but got Unknown
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Expected O, but got Unknown
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Expected O, but got Unknown
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Expected O, but got Unknown
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Expected O, but got Unknown
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Expected O, but got Unknown
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Expected O, but got Unknown
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Expected O, but got Unknown
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Expected O, but got Unknown
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Expected O, but got Unknown
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Expected O, but got Unknown
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Expected O, but got Unknown
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Expected O, but got Unknown
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Expected O, but got Unknown
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Expected O, but got Unknown
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Expected O, but got Unknown
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Expected O, but got Unknown
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Expected O, but got Unknown
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Expected O, but got Unknown
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Expected O, but got Unknown
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Expected O, but got Unknown
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Expected O, but got Unknown
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Expected O, but got Unknown
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Expected O, but got Unknown
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Expected O, but got Unknown
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Expected O, but got Unknown
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected O, but got Unknown
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Expected O, but got Unknown
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected O, but got Unknown
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Expected O, but got Unknown
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Expected O, but got Unknown
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Expected O, but got Unknown
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Expected O, but got Unknown
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8b: Expected O, but got Unknown
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da3: Expected O, but got Unknown
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbc: Expected O, but got Unknown
		//IL_1173: Unknown result type (might be due to invalid IL or missing references)
		//IL_117d: Expected O, but got Unknown
		//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ad: Expected O, but got Unknown
		//IL_11bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c5: Expected O, but got Unknown
		//IL_2229: Unknown result type (might be due to invalid IL or missing references)
		//IL_2233: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmOperations));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
		UltraTab val4 = new UltraTab();
		UltraTab val5 = new UltraTab();
		UltraTab val6 = new UltraTab();
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
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGDataItems = new UltraGrid();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.btnCreateExpense = new UltraButton();
		this.ULGDataExpenses = new UltraGrid();
		this.ultraTabPageControl7 = new UltraTabPageControl();
		this.btnAddBL = new UltraButton();
		this.ULGBillsOFLading = new UltraGrid();
		this.ultraTabPageControl8 = new UltraTabPageControl();
		this.btnAddPhyCert = new UltraButton();
		this.ULGPhytosanitaryCertificates = new UltraGrid();
		this.ultraTabPageControl9 = new UltraTabPageControl();
		this.btnAddCertOfOrigin = new UltraButton();
		this.ULGCertificatesOFOrigin = new UltraGrid();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.ULGReports = new UltraGrid();
		this.pnlCheckType = new UltraPanel();
		this.rbIsQuotation = new System.Windows.Forms.RadioButton();
		this.rbIsDirect = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnSeaPortSearch = new UltraButton();
		this.lblSeaPort = new UltraLabel();
		this.cboLoadingPorts = new UltraComboEditor();
		this.txtClientInvoiceNo = new UltraTextEditor();
		this.txtCertificateNo = new UltraTextEditor();
		this.lblCertificateNo = new UltraLabel();
		this.dtpCertificateDate = new UltraDateTimeEditor();
		this.lblCertificateDate = new UltraLabel();
		this.chkIsOriginCountry = new UltraCheckEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblClientName = new UltraLabel();
		this.txtClientName = new UltraTextEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.chkIsCompass = new UltraCheckEditor();
		this.lblSubAccount = new UltraLabel();
		this.cboSubAccountName = new UltraComboEditor();
		this.btnPriceTypeSearch = new UltraButton();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboExportType = new UltraComboEditor();
		this.lblExportType = new UltraLabel();
		this.chkIsCopies = new UltraCheckEditor();
		this.chkClosed = new UltraCheckEditor();
		this.chkIsCanceled = new UltraCheckEditor();
		this.dtpCancelDate = new UltraDateTimeEditor();
		this.cboCertificateOpeningSeaPort = new UltraComboEditor();
		this.lblCertificateOpeningSeaPort = new UltraLabel();
		this.btnCertificateOpeningSeaPortSearch = new UltraButton();
		this.lblFreightForwarder = new UltraLabel();
		this.cboFreightForwarder = new UltraComboEditor();
		this.cboExporter = new UltraComboEditor();
		this.lblExporter = new UltraLabel();
		this.cboSeaPortOfDischarge = new UltraComboEditor();
		this.lblSeaPortOfDischarge = new UltraLabel();
		this.dtpOriginCertificateDate = new UltraDateTimeEditor();
		this.lblOriginCertificateDate = new UltraLabel();
		this.dtpShippingDate = new UltraDateTimeEditor();
		this.lblShippingDate = new UltraLabel();
		this.lblVesselName = new UltraLabel();
		this.txtVesselName = new UltraTextEditor();
		this.lblClientInvoiceNo = new UltraLabel();
		this.chkIsPrePaid = new UltraCheckEditor();
		this.lblPOLCountry = new UltraLabel();
		this.cboPOLCountries = new UltraComboEditor();
		this.btnSeaPortOfDischargeSearch = new UltraButton();
		this.btnClientAdd = new UltraButton();
		this.btnAddExporter = new UltraButton();
		this.lblTotalQty = new UltraLabel();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalGrossWeight = new UltraLabel();
		this.txtTotalGrossWeight = new UltraTextEditor();
		this.lblTotalNetWeight = new UltraLabel();
		this.txtTotalNetWeight = new UltraTextEditor();
		this.lblQuotation = new UltraLabel();
		this.cboQuotation = new UltraComboEditor();
		this.cboCustomsBrokers = new UltraComboEditor();
		this.lblCustomBroker = new UltraLabel();
		this.chkRevised = new UltraCheckEditor();
		this.lblBrokerFinishedDate = new UltraLabel();
		this.dtpBrokerFinishedDate = new UltraDateTimeEditor();
		this.chkBrokerFinished = new UltraCheckEditor();
		this.lblRevised = new UltraLabel();
		this.dtpRevisedDate = new UltraDateTimeEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBillsOFLading).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPhytosanitaryCertificates).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGCertificatesOFOrigin).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGReports).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingPorts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCertificateDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOriginCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCompass).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCopies).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCertificateOpeningSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFreightForwarder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExporter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeaPortOfDischarge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpOriginCertificateDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShippingDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrePaid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPOLCountries).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustomsBrokers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRevised).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBrokerFinishedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBrokerFinished).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpRevisedDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl7);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl8);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl9);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Items";
		val.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Expenses";
		val2.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "BillsOfLading";
		val3.TabPage = this.ultraTabPageControl7;
		resources.ApplyResources(val3, "ultraTab4");
		((SubObjectBase)val3).ForceApplyResources = "";
		((KeyedSubObjectBase)val4).Key = "PhytosanitaryCertificates";
		val4.TabPage = this.ultraTabPageControl8;
		resources.ApplyResources(val4, "ultraTab5");
		((SubObjectBase)val4).ForceApplyResources = "";
		((KeyedSubObjectBase)val5).Key = "CertificatesOfOrigin";
		val5.TabPage = this.ultraTabPageControl9;
		resources.ApplyResources(val5, "ultraTab6");
		((SubObjectBase)val5).ForceApplyResources = "";
		((KeyedSubObjectBase)val6).Key = "Documents";
		val6.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val6, "ultraTab3");
		((SubObjectBase)val6).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[6] { val, val2, val3, val4, val5, val6 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl9, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl8, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl7, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl6, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl5, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val10).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val14).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val14).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val14).Image = resources.GetObject("appearance38.Image");
		resources.ApplyResources(val14, "appearance38");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataItems);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		((UltraGridBase)this.ULGDataItems).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val16).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDataItems, "ULGDataItems");
		((System.Windows.Forms.Control)(object)this.ULGDataItems).Name = "ULGDataItems";
		this.ULGDataItems.AfterCellUpdate += new CellEventHandler(ULGDataItems_AfterCellUpdate);
		this.ULGDataItems.AfterRowsDeleted += new System.EventHandler(ULGDataItems_AfterRowsDeleted);
		this.ULGDataItems.AfterRowUpdate += new RowEventHandler(ULGDataItems_AfterRowUpdate);
		this.ULGDataItems.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataItems_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataItems_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataItems_KeyPress);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateExpense);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.btnCreateExpense, "btnCreateExpense");
		((ControlBase)this.btnCreateExpense).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCreateExpense).Name = "btnCreateExpense";
		((System.Windows.Forms.Control)(object)this.btnCreateExpense).Click += new System.EventHandler(btnCreateExpense_Click);
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val22).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val22;
		((AppearanceBase)val23).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val23).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val24).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val24).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterEnterEditMode += new System.EventHandler(ULGDataExpenses_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnAddBL);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBillsOFLading);
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.btnAddBL, "btnAddBL");
		((ControlBase)this.btnAddBL).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnAddBL).Name = "btnAddBL";
		((System.Windows.Forms.Control)(object)this.btnAddBL).Click += new System.EventHandler(btnAddBL_Click);
		resources.ApplyResources(this.ULGBillsOFLading, "ULGBillsOFLading");
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val26).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val26).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val26).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val27;
		((AppearanceBase)val28).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val28).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val29).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val29).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGBillsOFLading).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGBillsOFLading).Name = "ULGBillsOFLading";
		((UltraControlBase)this.ULGBillsOFLading).UseFlatMode = (DefaultableBoolean)1;
		this.ULGBillsOFLading.AfterEnterEditMode += new System.EventHandler(ULGBillsOFLading_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.btnAddPhyCert);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPhytosanitaryCertificates);
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		resources.ApplyResources(this.btnAddPhyCert, "btnAddPhyCert");
		((ControlBase)this.btnAddPhyCert).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnAddPhyCert).Name = "btnAddPhyCert";
		((System.Windows.Forms.Control)(object)this.btnAddPhyCert).Click += new System.EventHandler(btnAddPhyCert_Click);
		resources.ApplyResources(this.ULGPhytosanitaryCertificates, "ULGPhytosanitaryCertificates");
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val31).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val31).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val31).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val32;
		((AppearanceBase)val33).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val33).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val34).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val34).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val34).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGPhytosanitaryCertificates).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGPhytosanitaryCertificates).Name = "ULGPhytosanitaryCertificates";
		((UltraControlBase)this.ULGPhytosanitaryCertificates).UseFlatMode = (DefaultableBoolean)1;
		this.ULGPhytosanitaryCertificates.AfterEnterEditMode += new System.EventHandler(ULGPhytosanitaryCertificates_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.btnAddCertOfOrigin);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCertificatesOFOrigin);
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		resources.ApplyResources(this.btnAddCertOfOrigin, "btnAddCertOfOrigin");
		((ControlBase)this.btnAddCertOfOrigin).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnAddCertOfOrigin).Name = "btnAddCertOfOrigin";
		((System.Windows.Forms.Control)(object)this.btnAddCertOfOrigin).Click += new System.EventHandler(btnAddCertOfOrigin_Click);
		resources.ApplyResources(this.ULGCertificatesOFOrigin, "ULGCertificatesOFOrigin");
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val36).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val36).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val36).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val36).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val37).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val37;
		((AppearanceBase)val38).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val38).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val39).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val39).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val39).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGCertificatesOFOrigin).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGCertificatesOFOrigin).Name = "ULGCertificatesOFOrigin";
		((UltraControlBase)this.ULGCertificatesOFOrigin).UseFlatMode = (DefaultableBoolean)1;
		this.ULGCertificatesOFOrigin.AfterEnterEditMode += new System.EventHandler(ULGCertificatesOFOrigin_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ULGReports);
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		((UltraGridBase)this.ULGReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val41).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val41).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val41).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val42).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val42;
		((AppearanceBase)val43).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val43).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val44).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val44).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGReports, "ULGReports");
		((System.Windows.Forms.Control)(object)this.ULGReports).Name = "ULGReports";
		((UltraControlBase)this.ULGReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGReports.AfterEnterEditMode += new System.EventHandler(ULGReports_AfterEnterEditMode);
		this.ULGReports.ClickCellButton += new CellEventHandler(ULGReports_ClickCellButton);
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsQuotation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirect);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsQuotation, "rbIsQuotation");
		this.rbIsQuotation.BackColor = System.Drawing.Color.Transparent;
		this.rbIsQuotation.Name = "rbIsQuotation";
		this.rbIsQuotation.TabStop = true;
		this.rbIsQuotation.UseVisualStyleBackColor = false;
		this.rbIsQuotation.CheckedChanged += new System.EventHandler(radionButton_CheckedChanged);
		resources.ApplyResources(this.rbIsDirect, "rbIsDirect");
		this.rbIsDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirect.Name = "rbIsDirect";
		this.rbIsDirect.TabStop = true;
		this.rbIsDirect.UseVisualStyleBackColor = false;
		this.rbIsDirect.CheckedChanged += new System.EventHandler(radionButton_CheckedChanged);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((AppearanceBase)val46).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSeaPortSearch).Appearance = (AppearanceBase)(object)val46;
		resources.ApplyResources(this.btnSeaPortSearch, "btnSeaPortSearch");
		((System.Windows.Forms.Control)(object)this.btnSeaPortSearch).Name = "btnSeaPortSearch";
		((System.Windows.Forms.Control)(object)this.btnSeaPortSearch).Click += new System.EventHandler(btnSeaPortSearch_Click);
		this.lblSeaPort.AutoEllipsis = false;
		resources.ApplyResources(this.lblSeaPort, "lblSeaPort");
		((System.Windows.Forms.Control)(object)this.lblSeaPort).Name = "lblSeaPort";
		((ControlBase)this.lblSeaPort).WrapText = false;
		((TextEditorControlBase)this.cboLoadingPorts).AlwaysInEditMode = true;
		this.cboLoadingPorts.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboLoadingPorts, "cboLoadingPorts");
		((System.Windows.Forms.Control)(object)this.cboLoadingPorts).Name = "cboLoadingPorts";
		((TextEditorControlBase)this.cboLoadingPorts).ValueChanged += new System.EventHandler(cboLoadingPorts_ValueChanged);
		resources.ApplyResources(this.txtClientInvoiceNo, "txtClientInvoiceNo");
		((System.Windows.Forms.Control)(object)this.txtClientInvoiceNo).Name = "txtClientInvoiceNo";
		resources.ApplyResources(this.txtCertificateNo, "txtCertificateNo");
		((System.Windows.Forms.Control)(object)this.txtCertificateNo).Name = "txtCertificateNo";
		this.lblCertificateNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblCertificateNo, "lblCertificateNo");
		((System.Windows.Forms.Control)(object)this.lblCertificateNo).Name = "lblCertificateNo";
		((ControlBase)this.lblCertificateNo).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpCertificateDate).AlwaysInEditMode = true;
		this.dtpCertificateDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpCertificateDate, "dtpCertificateDate");
		this.dtpCertificateDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCertificateDate).Name = "dtpCertificateDate";
		this.dtpCertificateDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		this.lblCertificateDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblCertificateDate, "lblCertificateDate");
		((System.Windows.Forms.Control)(object)this.lblCertificateDate).Name = "lblCertificateDate";
		((ControlBase)this.lblCertificateDate).WrapText = false;
		resources.ApplyResources(this.chkIsOriginCountry, "chkIsOriginCountry");
		((System.Windows.Forms.Control)(object)this.chkIsOriginCountry).Name = "chkIsOriginCountry";
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblClientName.AutoEllipsis = false;
		resources.ApplyResources(this.lblClientName, "lblClientName");
		((System.Windows.Forms.Control)(object)this.lblClientName).Name = "lblClientName";
		((ControlBase)this.lblClientName).WrapText = false;
		resources.ApplyResources(this.txtClientName, "txtClientName");
		((System.Windows.Forms.Control)(object)this.txtClientName).Name = "txtClientName";
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		this.lblCurrency.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.chkIsCompass, "chkIsCompass");
		((System.Windows.Forms.Control)(object)this.chkIsCompass).Name = "chkIsCompass";
		((UltraToggleEditorBase)this.chkIsCompass).CheckedChanged += new System.EventHandler(chkIsCompass_CheckedChanged);
		this.lblSubAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		((TextEditorControlBase)this.cboSubAccountName).AlwaysInEditMode = true;
		this.cboSubAccountName.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSubAccountName, "cboSubAccountName");
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).Name = "cboSubAccountName";
		((TextEditorControlBase)this.cboSubAccountName).ValueChanged += new System.EventHandler(cboSubAccountName_ValueChanged);
		((AppearanceBase)val47).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val47;
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Click += new System.EventHandler(lblPriceType_Click);
		((TextEditorControlBase)this.cboCountry).AlwaysInEditMode = true;
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val48).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val48;
		this.lblCountry.AutoEllipsis = false;
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		((TextEditorControlBase)this.cboExportType).AlwaysInEditMode = true;
		this.cboExportType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboExportType, "cboExportType");
		((System.Windows.Forms.Control)(object)this.cboExportType).Name = "cboExportType";
		this.lblExportType.AutoEllipsis = false;
		resources.ApplyResources(this.lblExportType, "lblExportType");
		((System.Windows.Forms.Control)(object)this.lblExportType).Name = "lblExportType";
		((ControlBase)this.lblExportType).WrapText = false;
		resources.ApplyResources(this.chkIsCopies, "chkIsCopies");
		((System.Windows.Forms.Control)(object)this.chkIsCopies).Name = "chkIsCopies";
		resources.ApplyResources(this.chkClosed, "chkClosed");
		((System.Windows.Forms.Control)(object)this.chkClosed).Name = "chkClosed";
		resources.ApplyResources(this.chkIsCanceled, "chkIsCanceled");
		((System.Windows.Forms.Control)(object)this.chkIsCanceled).Name = "chkIsCanceled";
		((UltraWinEditorMaskedControlBase)this.dtpCancelDate).AlwaysInEditMode = true;
		this.dtpCancelDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpCancelDate, "dtpCancelDate");
		this.dtpCancelDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCancelDate).Name = "dtpCancelDate";
		this.dtpCancelDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		((TextEditorControlBase)this.cboCertificateOpeningSeaPort).AlwaysInEditMode = true;
		this.cboCertificateOpeningSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCertificateOpeningSeaPort, "cboCertificateOpeningSeaPort");
		((System.Windows.Forms.Control)(object)this.cboCertificateOpeningSeaPort).Name = "cboCertificateOpeningSeaPort";
		((TextEditorControlBase)this.cboCertificateOpeningSeaPort).ValueChanged += new System.EventHandler(cboCertificateOpeningSeaPort_ValueChanged);
		this.lblCertificateOpeningSeaPort.AutoEllipsis = false;
		resources.ApplyResources(this.lblCertificateOpeningSeaPort, "lblCertificateOpeningSeaPort");
		((System.Windows.Forms.Control)(object)this.lblCertificateOpeningSeaPort).Name = "lblCertificateOpeningSeaPort";
		((ControlBase)this.lblCertificateOpeningSeaPort).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblCertificateOpeningSeaPort).Click += new System.EventHandler(lblCertificateOpeningSeaPort_Click);
		((AppearanceBase)val49).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnCertificateOpeningSeaPortSearch).Appearance = (AppearanceBase)(object)val49;
		resources.ApplyResources(this.btnCertificateOpeningSeaPortSearch, "btnCertificateOpeningSeaPortSearch");
		((System.Windows.Forms.Control)(object)this.btnCertificateOpeningSeaPortSearch).Name = "btnCertificateOpeningSeaPortSearch";
		((System.Windows.Forms.Control)(object)this.btnCertificateOpeningSeaPortSearch).Click += new System.EventHandler(btnCertificateOpeningSeaPort_Click);
		this.lblFreightForwarder.AutoEllipsis = false;
		resources.ApplyResources(this.lblFreightForwarder, "lblFreightForwarder");
		((System.Windows.Forms.Control)(object)this.lblFreightForwarder).Name = "lblFreightForwarder";
		((ControlBase)this.lblFreightForwarder).WrapText = false;
		((TextEditorControlBase)this.cboFreightForwarder).AlwaysInEditMode = true;
		this.cboFreightForwarder.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboFreightForwarder, "cboFreightForwarder");
		((System.Windows.Forms.Control)(object)this.cboFreightForwarder).Name = "cboFreightForwarder";
		((TextEditorControlBase)this.cboExporter).AlwaysInEditMode = true;
		this.cboExporter.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboExporter, "cboExporter");
		((System.Windows.Forms.Control)(object)this.cboExporter).Name = "cboExporter";
		this.lblExporter.AutoEllipsis = false;
		resources.ApplyResources(this.lblExporter, "lblExporter");
		((System.Windows.Forms.Control)(object)this.lblExporter).Name = "lblExporter";
		((ControlBase)this.lblExporter).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblExporter).Click += new System.EventHandler(lblExporter_Click);
		((TextEditorControlBase)this.cboSeaPortOfDischarge).AlwaysInEditMode = true;
		this.cboSeaPortOfDischarge.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSeaPortOfDischarge, "cboSeaPortOfDischarge");
		((System.Windows.Forms.Control)(object)this.cboSeaPortOfDischarge).Name = "cboSeaPortOfDischarge";
		this.lblSeaPortOfDischarge.AutoEllipsis = false;
		resources.ApplyResources(this.lblSeaPortOfDischarge, "lblSeaPortOfDischarge");
		((System.Windows.Forms.Control)(object)this.lblSeaPortOfDischarge).Name = "lblSeaPortOfDischarge";
		((ControlBase)this.lblSeaPortOfDischarge).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpOriginCertificateDate).AlwaysInEditMode = true;
		this.dtpOriginCertificateDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpOriginCertificateDate, "dtpOriginCertificateDate");
		this.dtpOriginCertificateDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpOriginCertificateDate).Name = "dtpOriginCertificateDate";
		this.dtpOriginCertificateDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		this.lblOriginCertificateDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblOriginCertificateDate, "lblOriginCertificateDate");
		((System.Windows.Forms.Control)(object)this.lblOriginCertificateDate).Name = "lblOriginCertificateDate";
		((ControlBase)this.lblOriginCertificateDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblOriginCertificateDate).Click += new System.EventHandler(lblOriginCertificateDate_Click);
		((UltraWinEditorMaskedControlBase)this.dtpShippingDate).AlwaysInEditMode = true;
		this.dtpShippingDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpShippingDate, "dtpShippingDate");
		this.dtpShippingDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpShippingDate).Name = "dtpShippingDate";
		this.dtpShippingDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		this.dtpShippingDate.ValueChanged += new System.EventHandler(dtpShippingDate_ValueChanged);
		this.lblShippingDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblShippingDate, "lblShippingDate");
		((System.Windows.Forms.Control)(object)this.lblShippingDate).Name = "lblShippingDate";
		((ControlBase)this.lblShippingDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblShippingDate).Click += new System.EventHandler(lblShippingDate_Click);
		this.lblVesselName.AutoEllipsis = false;
		resources.ApplyResources(this.lblVesselName, "lblVesselName");
		((System.Windows.Forms.Control)(object)this.lblVesselName).Name = "lblVesselName";
		((ControlBase)this.lblVesselName).WrapText = false;
		resources.ApplyResources(this.txtVesselName, "txtVesselName");
		((System.Windows.Forms.Control)(object)this.txtVesselName).Name = "txtVesselName";
		this.lblClientInvoiceNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblClientInvoiceNo, "lblClientInvoiceNo");
		((System.Windows.Forms.Control)(object)this.lblClientInvoiceNo).Name = "lblClientInvoiceNo";
		((ControlBase)this.lblClientInvoiceNo).WrapText = false;
		resources.ApplyResources(this.chkIsPrePaid, "chkIsPrePaid");
		((System.Windows.Forms.Control)(object)this.chkIsPrePaid).Name = "chkIsPrePaid";
		((AppearanceBase)val50).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val50).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPOLCountry).Appearance = (AppearanceBase)(object)val50;
		this.lblPOLCountry.AutoEllipsis = false;
		resources.ApplyResources(this.lblPOLCountry, "lblPOLCountry");
		((System.Windows.Forms.Control)(object)this.lblPOLCountry).Name = "lblPOLCountry";
		((ControlBase)this.lblPOLCountry).WrapText = false;
		((TextEditorControlBase)this.cboPOLCountries).AlwaysInEditMode = true;
		this.cboPOLCountries.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPOLCountries, "cboPOLCountries");
		((System.Windows.Forms.Control)(object)this.cboPOLCountries).Name = "cboPOLCountries";
		((TextEditorControlBase)this.cboPOLCountries).ValueChanged += new System.EventHandler(cboPOLCountries_ValueChanged);
		((AppearanceBase)val51).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSeaPortOfDischargeSearch).Appearance = (AppearanceBase)(object)val51;
		resources.ApplyResources(this.btnSeaPortOfDischargeSearch, "btnSeaPortOfDischargeSearch");
		((System.Windows.Forms.Control)(object)this.btnSeaPortOfDischargeSearch).Name = "btnSeaPortOfDischargeSearch";
		((System.Windows.Forms.Control)(object)this.btnSeaPortOfDischargeSearch).Click += new System.EventHandler(btnSeaPortOfDischargeSearch_Click);
		((AppearanceBase)val52).Image = ERP.Properties.Resources.New;
		((ControlBase)this.btnClientAdd).Appearance = (AppearanceBase)(object)val52;
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		((AppearanceBase)val53).Image = ERP.Properties.Resources.add;
		((ControlBase)this.btnAddExporter).Appearance = (AppearanceBase)(object)val53;
		resources.ApplyResources(this.btnAddExporter, "btnAddExporter");
		((System.Windows.Forms.Control)(object)this.btnAddExporter).Name = "btnAddExporter";
		((System.Windows.Forms.Control)(object)this.btnAddExporter).Click += new System.EventHandler(btnAddExporter_Click);
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		resources.ApplyResources(this.lblTotalGrossWeight, "lblTotalGrossWeight");
		this.lblTotalGrossWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight).Name = "lblTotalGrossWeight";
		((ControlBase)this.lblTotalGrossWeight).WrapText = false;
		resources.ApplyResources(this.txtTotalGrossWeight, "txtTotalGrossWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight).Name = "txtTotalGrossWeight";
		((EditorButtonControlBase)this.txtTotalGrossWeight).ReadOnly = true;
		resources.ApplyResources(this.lblTotalNetWeight, "lblTotalNetWeight");
		this.lblTotalNetWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalNetWeight).Name = "lblTotalNetWeight";
		((ControlBase)this.lblTotalNetWeight).WrapText = false;
		resources.ApplyResources(this.txtTotalNetWeight, "txtTotalNetWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalNetWeight).Name = "txtTotalNetWeight";
		((EditorButtonControlBase)this.txtTotalNetWeight).ReadOnly = true;
		this.lblQuotation.AutoEllipsis = false;
		resources.ApplyResources(this.lblQuotation, "lblQuotation");
		((System.Windows.Forms.Control)(object)this.lblQuotation).Name = "lblQuotation";
		((ControlBase)this.lblQuotation).WrapText = false;
		((TextEditorControlBase)this.cboQuotation).AlwaysInEditMode = true;
		this.cboQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboQuotation, "cboQuotation");
		((System.Windows.Forms.Control)(object)this.cboQuotation).Name = "cboQuotation";
		((EditorButtonControlBase)this.cboQuotation).ReadOnly = true;
		((TextEditorControlBase)this.cboQuotation).ValueChanged += new System.EventHandler(cboQuotation_ValueChanged);
		((TextEditorControlBase)this.cboCustomsBrokers).AlwaysInEditMode = true;
		this.cboCustomsBrokers.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCustomsBrokers, "cboCustomsBrokers");
		((System.Windows.Forms.Control)(object)this.cboCustomsBrokers).Name = "cboCustomsBrokers";
		((EditorButtonControlBase)this.cboCustomsBrokers).ReadOnly = true;
		this.lblCustomBroker.AutoEllipsis = false;
		resources.ApplyResources(this.lblCustomBroker, "lblCustomBroker");
		((System.Windows.Forms.Control)(object)this.lblCustomBroker).Name = "lblCustomBroker";
		((ControlBase)this.lblCustomBroker).WrapText = false;
		resources.ApplyResources(this.chkRevised, "chkRevised");
		((System.Windows.Forms.Control)(object)this.chkRevised).Name = "chkRevised";
		this.lblBrokerFinishedDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblBrokerFinishedDate, "lblBrokerFinishedDate");
		((System.Windows.Forms.Control)(object)this.lblBrokerFinishedDate).Name = "lblBrokerFinishedDate";
		((ControlBase)this.lblBrokerFinishedDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpBrokerFinishedDate).AlwaysInEditMode = true;
		this.dtpBrokerFinishedDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpBrokerFinishedDate, "dtpBrokerFinishedDate");
		this.dtpBrokerFinishedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpBrokerFinishedDate).Name = "dtpBrokerFinishedDate";
		this.dtpBrokerFinishedDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.chkBrokerFinished, "chkBrokerFinished");
		((System.Windows.Forms.Control)(object)this.chkBrokerFinished).Name = "chkBrokerFinished";
		this.lblRevised.AutoEllipsis = false;
		resources.ApplyResources(this.lblRevised, "lblRevised");
		((System.Windows.Forms.Control)(object)this.lblRevised).Name = "lblRevised";
		((ControlBase)this.lblRevised).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpRevisedDate).AlwaysInEditMode = true;
		this.dtpRevisedDate.DateTime = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpRevisedDate, "dtpRevisedDate");
		this.dtpRevisedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpRevisedDate).Name = "dtpRevisedDate";
		this.dtpRevisedDate.Value = new System.DateTime(2022, 9, 11, 0, 0, 0, 0);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomBroker);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCustomsBrokers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddExporter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeaPortOfDischarge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSeaPortOfDischarge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExporter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExporter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFreightForwarder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFreightForwarder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCancelDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPrePaid);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBrokerFinished);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRevised);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPOLCountries);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPOLCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCompass);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCopies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsOriginCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCertificateOpeningSeaPortSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSeaPortOfDischargeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSeaPortSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCertificateOpeningSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCertificateOpeningSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLoadingPorts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShippingDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOriginCertificateDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShippingDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpOriginCertificateDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpRevisedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBrokerFinishedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRevised);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBrokerFinishedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCertificateDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCertificateDate);
		base.Name = "frmOperations";
		base.Load += new System.EventHandler(frmOperations_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCertificateDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCertificateDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBrokerFinishedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRevised, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBrokerFinishedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpRevisedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpOriginCertificateDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShippingDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOriginCertificateDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShippingDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLoadingPorts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCertificateOpeningSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCertificateOpeningSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSeaPortSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSeaPortOfDischargeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCertificateOpeningSeaPortSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsOriginCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCopies, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCompass, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPOLCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPOLCountries, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClosed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRevised, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBrokerFinished, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsPrePaid, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCancelDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFreightForwarder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFreightForwarder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExporter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExporter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSeaPortOfDischarge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeaPortOfDischarge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddExporter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCustomsBrokers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomBroker, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGBillsOFLading).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPhytosanitaryCertificates).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGCertificatesOFOrigin).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGReports).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingPorts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCertificateDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOriginCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCompass).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCopies).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCertificateOpeningSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFreightForwarder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExporter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeaPortOfDischarge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpOriginCertificateDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShippingDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrePaid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPOLCountries).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustomsBrokers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRevised).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBrokerFinishedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBrokerFinished).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpRevisedDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
