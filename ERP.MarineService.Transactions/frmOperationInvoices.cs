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
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.MarineService.Transactions;

public class frmOperationInvoices : frmHeaderManyDetails
{
	private DataTable dtServices;

	private DataTable dtReports;

	private DataTable dtSettings;

	private DataTable dtCurrency;

	private DataTable dtSubAccounts;

	private DataTable dtOperationsNo;

	private DataTable dtCompanies;

	private DataTable dtShipChandler;

	private DataTable dtShipChandlerReturn;

	private DataTable dtTaxs;

	private ValueList vlServices = new ValueList();

	private ValueList vlCompanies = new ValueList();

	private ValueList vlServicesUnits = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private string OperationID = "0";

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboOnAccountOF;

	private UltraLabel lblOnAccountOF;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnOperationNoSearch;

	private UltraLabel lblDiscountValue;

	private UltraTextEditor txtDiscountValue;

	private UltraLabel lblDiscountRatio;

	private UltraTextEditor txtDiscountRatio;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	private RadioButton rbIsItem;

	private RadioButton rbIsService;

	private UltraCheckEditor chkInvoiceMessage;

	private UltraCheckEditor chkPrintSummary;

	private UltraCheckEditor chkPrintWithTariff;

	private UltraCheckEditor chkWithLogo;

	private UltraLabel lblGrowthFees;

	private UltraTextEditor txtAddedTax;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGShipChandler;

	private UltraTabPageControl ultraTabPageControl4;

	protected internal UltraGrid ULGShipChandlerReturn;

	public frmOperationInvoices()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "MS_OperationsInvoices";
		IDCol = "OperationInvoiceID";
		NoCol = "OperationInvoiceNo";
		DateCol = "OperationinvoiceDate";
	}

	public frmOperationInvoices(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmOperationInvoices(string OPERATIONID)
		: this()
	{
		OperationID = OPERATIONID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		((UltraTabControlBase)UTCDetails).Tabs["Details"].Text = (GlobalVariables.IsArabic ? "الخدمات" : "Services");
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtOperationsNo = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int i = 0; i < dtServices.Rows.Count; i++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[i]["ServiceID"], dtServices.Rows[i]["ServiceName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCompanies.ValueListItems.Clear();
		for (int j = 0; j < dtCompanies.Rows.Count; j++)
		{
			vlCompanies.ValueListItems.Add(dtCompanies.Rows[j]["CompanyID"], dtCompanies.Rows[j]["CompanyName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int k = 0; k < dtTaxs.Rows.Count; k++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[k]["TaxID"], dtTaxs.Rows[k]["TaxName"].ToString());
		}
		dtSettings = BusinessLayer.MarineService.Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((Control)(object)chkInvoiceMessage).Text = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString()).Length != 0) ? dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["InvoiceMessage"].ToString() : "");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		FillCurrencyDropDown();
		dtDetails = OperationsServices.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtShipChandler = ShipChandler.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtShipChandlerReturn = ShipChandlerReturns.SelectByOperationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
		((UltraGridBase)ULGShipChandlerReturn).DataSource = dtShipChandlerReturn;
		InitGrid();
		InitGridShipChandler();
		InitGridShipChandlerReturn();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PONumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة" : "Percent");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Header).Caption = (GlobalVariables.IsArabic ? "إضافة للتكلفة" : "Cost Plus");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Unit Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة إضافية" : "Additional Fees");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالى المصروفات" : "Total Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service #");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PONumber"].Header).Caption = (GlobalVariables.IsArabic ? "PO#" : "PO#");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبه" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبه" : "Tax Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PONumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyID"].ValueList = (IValueList)(object)vlCompanies;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlServicesUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalFees"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalExpenses"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExpensePercent"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCostPlus"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
	}

	public void InitGridShipChandler()
	{
		GlobalFunctions.PrepareGrid(ULGShipChandler);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerID"].DefaultCellValue = -1;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerNo"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGShipChandler).DisplayLayout.Bands[0].Columns["ShipChandlerDate"].Width = (int)((double)((Control)(object)ULGShipChandler).Width * 0.2);
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
	}

	public void InitGridShipChandlerReturn()
	{
		GlobalFunctions.PrepareGrid(ULGShipChandlerReturn);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnID"].DefaultCellValue = -1;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnNo"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnDate"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.2);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["GrossValue"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.1);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.1);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.1);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.1);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGShipChandlerReturn).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المرتجع" : "Return No");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المرتجع" : "Return Date");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["GrossValue"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Gross Value");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "خصم قبل الضريبة " : "Discount Before Tax");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnNo"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["ShipChandlerReturnDate"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["GrossValue"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["TaxTotalValue"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["GrossValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["DiscountBeforeTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["TaxTotalValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (OperationID != "0")
		{
			drMaster = null;
			btnAddClick();
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = OperationsInvoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		if (dataTable.Rows.Count > 0)
		{
			drMaster = dataTable.Rows[0];
		}
		else
		{
			drMaster = null;
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OperationInvoiceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["OperationinvoiceDate"];
			((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
			((TextEditorControlBase)cboOnAccountOF).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			((Control)(object)txtAddedTax).Text = decimal.Parse(drMaster["AddedTax"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			rbIsService.Checked = bool.Parse(drMaster["IsServiceInvoice"].ToString());
			rbIsItem.Checked = bool.Parse(drMaster["IsItemInvoice"].ToString());
			((UltraToggleEditorBase)chkInvoiceMessage).Checked = bool.Parse(drMaster["ViewInvoiceMessage"].ToString());
			((Control)(object)chkInvoiceMessage).Text = dtSettings.Select("BranchID = " + drMaster["BranchID"].ToString())[0]["InvoiceMessage"].ToString();
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountValue).Text = decimal.Parse(drMaster["DiscountValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(drMaster["DiscountRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = OperationsServices.SelectByOperationInvoiceID(drMaster["OperationInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtShipChandler = ShipChandler.SelectByOperationInvoiceID(drMaster["OperationInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtShipChandlerReturn = ShipChandlerReturns.SelectByOperationInvoiceID(drMaster["OperationInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
			((UltraGridBase)ULGShipChandlerReturn).DataSource = dtShipChandlerReturn;
			InitGrid();
			InitGridShipChandler();
			InitGridShipChandlerReturn();
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
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
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
		((EditorButtonControlBase)cboOnAccountOF).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		rbIsService.Enabled = Adding;
		rbIsItem.Enabled = Adding;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkPrintSummary).Enabled = NavMode;
		((Control)(object)chkPrintWithTariff).Enabled = NavMode;
		((Control)(object)chkWithLogo).Enabled = NavMode;
		((EditorButtonControlBase)txtDiscountValue).ReadOnly = NavMode || rbIsItem.Checked;
		((EditorButtonControlBase)txtDiscountRatio).ReadOnly = NavMode || rbIsItem.Checked;
		((Control)(object)chkInvoiceMessage).Enabled = !NavMode;
		((Control)(object)btnOperationNoSearch).Visible = Adding;
		if (Adding)
		{
			DataView dataView = new DataView(dtOperationsNo);
			dataView.RowFilter = " BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView.ToTable();
			int num = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			GlobalFunctions.FillCombo(cboOperationNo, dt, "OperationID", "OperationNo");
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			if (num != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num;
			}
		}
		else
		{
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			int num2 = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
			if (num2 != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num2;
			}
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		}
		if (Updating || Adding)
		{
			if (cboOperationNo.SelectedIndex > -1)
			{
				DataTable dt2 = OperationsInvoices.SelectSubAccountsFillCombo(((TextEditorControlBase)cboOperationNo).Value.ToString(), Adding ? "NULL" : "-1", GlobalVariables.IsArabic ? "1" : "0");
				((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
				int num3 = 0;
				if (cboOnAccountOF.SelectedIndex > -1)
				{
					num3 = int.Parse(((TextEditorControlBase)cboOnAccountOF).Value.ToString());
				}
				GlobalFunctions.FillCombo(cboOnAccountOF, dt2, "SubAccountID", "Name");
				if (num3 != 0)
				{
					((TextEditorControlBase)cboOnAccountOF).Value = num3;
				}
				((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
			}
		}
		else
		{
			int num4 = 0;
			if (cboOnAccountOF.SelectedIndex > -1)
			{
				num4 = int.Parse(((TextEditorControlBase)cboOnAccountOF).Value.ToString());
			}
			((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
			GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
			if (num4 != 0)
			{
				((TextEditorControlBase)cboOnAccountOF).Value = num4;
			}
			((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
		}
		((UltraGridBase)ULGShipChandler).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGShipChandler).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGShipChandlerReturn).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? OperationsInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		rbIsService.Checked = true;
		((UltraToggleEditorBase)chkInvoiceMessage).Checked = false;
		((Control)(object)chkInvoiceMessage).Text = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString()).Length != 0) ? dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["InvoiceMessage"].ToString() : "");
		((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
		cboOnAccountOF.SelectedIndex = -1;
		((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		if (OperationID != "0")
		{
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		else
		{
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			cboOperationNo.SelectedIndex = -1;
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		}
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtExchangeRate).Text = "0";
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		((Control)(object)txtDiscountValue).Text = "0";
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		((Control)(object)txtAddedTax).Text = "0";
		((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
		((Control)(object)txtDiscountRatio).Text = "0";
		((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((DataTable)((UltraGridBase)ULGShipChandler).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGShipChandlerReturn).DataSource).Rows.Clear();
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
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			string text = ((UltraGridBase)ULGData).Rows[0].Cells["CompanyID"].Value.ToString();
			if (dtDetails.Select("CompanyID <> " + text).Length != 0)
			{
				GlobalVariables.InformationMB.Show("هذه الخدمات موجوده على اكثر من شركة", "These Services are made on more than one company");
				return false;
			}
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboOnAccountOF.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب" : "Please Select Account");
			((TextEditorControlBase)cboOnAccountOF).Focus();
			cboOnAccountOF.DropDown();
			return false;
		}
		DataTable dataTable = (DataTable)cboOnAccountOF.DataSource;
		if (dataTable.Select("SubAccountID = " + ((TextEditorControlBase)cboOnAccountOF).Value.ToString())[0]["DefaultClientAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار حساب العميل الافتراضي" : "Please Select Default Client Account");
			((TextEditorControlBase)cboOnAccountOF).Focus();
			cboOnAccountOF.DropDown();
			return false;
		}
		if (cboOperationNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم العملية" : "Please Select Operation No");
			((TextEditorControlBase)cboOperationNo).Focus();
			cboOperationNo.DropDown();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_OperationsInvoices", "OperationInvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationInvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = OperationsInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count == 0 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsCostPlus"].Value.ToString()) && !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsExpensePercent"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الخدمة  ", "Please Enter Service Price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && decimal.Parse(((Control)(object)txtDiscountValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		if (rbIsService.Checked)
		{
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[j].Cells["OperationServiceID"].Value != DBNull.Value)
				{
					text2 = text2 + ((UltraGridBase)ULGData).Rows[j].Cells["OperationServiceID"].Value.ToString() + ",";
				}
			}
			if (!text2.Equals(",") && OperationsServices.SelectTotalOperationServiceIDs(text2) != double.Parse(((Control)(object)txtGrossValue).Text))
			{
				GlobalVariables.InformationMB.Show(" اجمالي قيمة الخدمات مختلف عن اجمالي الفاتورة", "Total Services Price is different from the Invoice Total Price.");
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateGross();
			int num = OperationsInvoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), rbIsService.Checked ? "1" : "0", rbIsItem.Checked ? "1" : "0", (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text == "") ? "0" : ((Control)(object)txtDiscountValue).Text, (((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text, "Null", (((Control)(object)txtAddedTax).Text == "") ? "0" : ((Control)(object)txtAddedTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((UltraToggleEditorBase)chkInvoiceMessage).Checked ? "1" : "0", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = "";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + "Exec MS_OperationsServices_UpdateInvoiceData " + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value.ToString() + "," + num + ",'" + ((UltraGridBase)ULGData).Rows[i].Cells["PONumber"].Value.ToString() + "', " + GlobalVariables.UserID + ";";
			}
			if (text != "")
			{
				Main.ExecuteNonQuery(text);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; j++)
			{
				text2 = text2 + ((UltraGridBase)ULGShipChandler).Rows[j].Cells["ShipChandlerID"].Value.ToString() + ",";
			}
			ShipChandler.UpdateOperationInvoiceID(text2, num.ToString(), GlobalVariables.UserID);
			string text3 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count; k++)
			{
				text3 = text3 + ((UltraGridBase)ULGShipChandlerReturn).Rows[k].Cells["ShipChandlerReturnID"].Value.ToString() + ",";
			}
			ShipChandlerReturns.UpdateOperationInvoiceID(text3, num.ToString(), GlobalVariables.UserID);
			if (rbIsService.Checked)
			{
				OperationsInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			}
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

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGShipChandler).UpdateData();
			((UltraGridBase)ULGShipChandlerReturn).UpdateData();
			CalculateGross();
			int num = OperationsInvoices.Insert_Update(drMaster["OperationInvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), rbIsService.Checked ? "1" : "0", rbIsItem.Checked ? "1" : "0", (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text == "") ? "0" : ((Control)(object)txtDiscountValue).Text, (((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text, "Null", (((Control)(object)txtAddedTax).Text == "") ? "0" : ((Control)(object)txtAddedTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((UltraToggleEditorBase)chkInvoiceMessage).Checked ? "1" : "0", (drMaster["JVID"] == DBNull.Value) ? "Null" : drMaster["JVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			string text2 = "update MS_OperationsServices Set OperationInvoiceID=Null Where OperationInvoiceID = " + num + ";";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text2 = text2 + "Exec MS_OperationsServices_UpdateInvoiceData " + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value.ToString() + "," + num + ",'" + ((UltraGridBase)ULGData).Rows[i].Cells["PONumber"].Value.ToString() + "', " + GlobalVariables.UserID + ";";
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value.ToString() + ",";
			}
			if (!text2.Equals(""))
			{
				Main.ExecuteNonQuery(text2);
			}
			string text3 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; j++)
			{
				text3 = text3 + ((UltraGridBase)ULGShipChandler).Rows[j].Cells["ShipChandlerID"].Value.ToString() + ",";
			}
			ShipChandler.UpdateOperationInvoiceID(text3, num.ToString(), GlobalVariables.UserID);
			string text4 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count; k++)
			{
				text4 = text4 + ((UltraGridBase)ULGShipChandlerReturn).Rows[k].Cells["ShipChandlerReturnID"].Value.ToString() + ",";
			}
			ShipChandlerReturns.UpdateOperationInvoiceID(text4, num.ToString(), GlobalVariables.UserID);
			if (rbIsService.Checked)
			{
				OperationsInvoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
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

	public override void btnUpdateClick()
	{
		if (drMaster == null)
		{
			return;
		}
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
		if (drMaster != null)
		{
			string strQuery = " Select COUNT(*) as RowsCount From MS_OperationsItems Where OperationInvoiceID = " + drMaster["OperationInvoiceID"].ToString();
			if (int.Parse(Main.ExecuteQuery_DataTable(strQuery).Rows[0][0].ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لأنها فاتورة قديمه", "Cannot Update This Transaction Because It Is an Old Invoice");
				return;
			}
		}
		Updating = true;
		SetControls(NavMode: false);
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
			OperationsInvoices.DeleteVirtual(drMaster["OperationInvoiceID"].ToString(), GlobalVariables.UserID);
			if (drMaster["JVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JVDetails.DeleteVirtualByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			OperationsServices.UpdateOperationInvoiceID(drMaster["OperationInvoiceID"].ToString(), GlobalVariables.UserID);
			ShipChandler.UpdateOperationInvoiceID(",,", drMaster["OperationInvoiceID"].ToString(), GlobalVariables.UserID);
			ShipChandlerReturns.UpdateOperationInvoiceID(",,", drMaster["OperationInvoiceID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnCancelClick()
	{
		if (Updating && RowID != "" && TableName != "")
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID, TableName, RowID);
		}
		Adding = false;
		Updating = false;
		if (OperationID != "0")
		{
			drMaster = null;
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		else if (RowID == "")
		{
			drMaster = null;
			DisplayData();
		}
		else
		{
			DataTable dataTable = OperationsInvoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
			DisplayData();
		}
		SetControls(NavMode: true);
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		if (rbIsItem.Checked)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Items_A.rpt" : "Rep_MS_OperationsInvoices_Items_E.rpt"));
		}
		else if (((UltraToggleEditorBase)chkPrintSummary).Checked)
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_ServicesSummary_A.rpt" : "Rep_MS_OperationsInvoices_ServicesSummary_E.rpt"));
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_ServicesSummary_A_nologo.rpt" : "Rep_MS_OperationsInvoices_ServicesSummary_E_nologo.rpt"));
			}
		}
		else if (dtDetails.Select("PONumber <> '' ").Length != 0)
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Services3_A.rpt" : "Rep_MS_OperationsInvoices_Services3_E.rpt"));
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Services3_A_nologo.rpt" : "Rep_MS_OperationsInvoices_Services3_E_nologo.rpt"));
			}
		}
		else if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Services_A.rpt" : "Rep_MS_OperationsInvoices_Services_E.rpt"));
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsInvoices_Services_A_nologo.rpt" : "Rep_MS_OperationsInvoices_Services_E_nologo.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@OperationInvoiceIDs", "," + RowID + ",");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		if (rbIsService.Checked)
		{
			GlobalVariables.ReportDocument.SetParameterValue("@WithTariff", ((UltraToggleEditorBase)chkPrintWithTariff).Checked ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@CompanyID", ((UltraGridBase)ULGData).Rows[0].Cells["CompanyID"].Value.ToString());
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
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.OperationsInvoicesReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationInvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtOperationsNo = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int i = 0; i < dtServices.Rows.Count; i++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[i]["ServiceID"], dtServices.Rows[i]["ServiceName"].ToString());
		}
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCompanies.ValueListItems.Clear();
		for (int j = 0; j < dtCompanies.Rows.Count; j++)
		{
			vlCompanies.ValueListItems.Add(dtCompanies.Rows[j]["CompanyID"], dtCompanies.Rows[j]["CompanyName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int k = 0; k < dtTaxs.Rows.Count; k++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[k]["TaxID"], dtTaxs.Rows[k]["TaxName"].ToString());
		}
		dtSettings = BusinessLayer.MarineService.Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		FillCurrencyDropDown();
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "PONumber")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGross();
	}

	private void ULGShipChandlerReturn_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGShipChandlerReturn).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGShipChandlerReturn).ActiveRow).Selected = true;
		}
	}

	private void ULGShipChandlerReturn_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGShipChandlerReturn_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGross();
	}

	private void ULGShipChandler_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGross();
	}

	private void ULGShipChandler_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGShipChandler_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGShipChandler).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGShipChandler).ActiveRow).Selected = true;
		}
	}

	private void CalculateGross()
	{
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; j++)
		{
			num3 += decimal.Parse(((UltraGridBase)ULGShipChandler).Rows[j].Cells["GrossValue"].Value.ToString());
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count; k++)
		{
			num4 += decimal.Parse(((UltraGridBase)ULGShipChandlerReturn).Rows[k].Cells["GrossValue"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse((num + num2 + num3 - num4).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (rbIsService.Checked)
		{
			((Control)(object)txtDiscountValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			decimal num5 = default(decimal);
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; l++)
			{
				num5 += decimal.Parse(((UltraGridBase)ULGShipChandler).Rows[l].Cells["DiscountBeforeTaxValue"].Value.ToString());
			}
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count; m++)
			{
				num5 -= decimal.Parse(((UltraGridBase)ULGShipChandlerReturn).Rows[m].Cells["DiscountBeforeTaxValue"].Value.ToString());
			}
			((Control)(object)txtDiscountValue).Text = decimal.Parse(num5.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			if (decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "." || ((Control)(object)txtDiscountValue).Text == "0") ? "0" : ((Control)(object)txtDiscountValue).Text) == 0m || decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == "." || ((Control)(object)txtGrossValue).Text == "0") ? "0" : ((Control)(object)txtGrossValue).Text) == 0m)
			{
				((Control)(object)txtDiscountRatio).Text = "0";
			}
			else
			{
				((Control)(object)txtDiscountRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "." || ((Control)(object)txtDiscountValue).Text == "0") ? "0" : ((Control)(object)txtDiscountValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
		}
		CalculateTotalsTax();
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
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
			((Control)(object)txtCode).Text = OperationsInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (cboOperationNo.SelectedIndex > -1)
			{
				((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
				((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
				dtDetails.Rows.Clear();
				dtShipChandler.Rows.Clear();
				dtShipChandlerReturn.Rows.Clear();
				CalculateGross();
				DataTable dt = OperationsInvoices.SelectSubAccountsFillCombo(((TextEditorControlBase)cboOperationNo).Value.ToString(), Adding ? "NULL" : "-1", GlobalVariables.IsArabic ? "1" : "0");
				GlobalFunctions.FillCombo(cboOnAccountOF, dt, "SubAccountID", "Name");
				((TextEditorControlBase)cboCurrency).Value = dtOperationsNo.Select(" OperationID= " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0]["CurrencyID"];
				((TextEditorControlBase)txtExchangeRate).Value = dtOperationsNo.Select(" OperationID= " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0]["ExchangeRate"];
				((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
				((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
			}
			else
			{
				dtDetails.Rows.Clear();
				dtShipChandler.Rows.Clear();
				dtShipChandlerReturn.Rows.Clear();
				CalculateGross();
				GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
			}
		}
	}

	private void btnOperationNoSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.OperationsSearch(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboOperationNo).Value = num;
		}
	}

	private void cboOnAccountOF_ValueChanged(object sender, EventArgs e)
	{
		if (cboOnAccountOF.SelectedIndex > -1 && cboOperationNo.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
			dtDetails.Rows.Clear();
			dtShipChandler.Rows.Clear();
			dtShipChandlerReturn.Rows.Clear();
			if (rbIsService.Checked)
			{
				dtDetails = OperationsServices.SelectByInvoiceSubAccountID(((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtDetails;
			}
			if (rbIsItem.Checked)
			{
				dtShipChandler = ShipChandler.SelectBySubAccountID(((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGShipChandler).DataSource = dtShipChandler;
				dtShipChandlerReturn = ShipChandlerReturns.SelectBySubAccountID(((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGShipChandlerReturn).DataSource = dtShipChandlerReturn;
			}
			InitGrid();
			InitGridShipChandler();
			InitGridShipChandlerReturn();
			CalculateGross();
			((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
		}
	}

	private void txtDiscountValue_ValueChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) != 0m)
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "." || ((Control)(object)txtDiscountValue).Text == "0") ? "0" : ((Control)(object)txtDiscountValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			CalculateNetTotals();
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
		}
	}

	private void txtDiscountRatio_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			CalculateNetTotals();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		UltraCheckEditor obj = chkPrintWithTariff;
		bool visible = (((Control)(object)chkPrintSummary).Visible = rbIsService.Checked);
		((Control)(object)obj).Visible = visible;
		((UltraTabControlBase)UTCDetails).Tabs["ShipChandler"].Visible = rbIsItem.Checked;
		((UltraTabControlBase)UTCDetails).Tabs["ShipChandlerReturn"].Visible = rbIsItem.Checked;
		((UltraTabControlBase)UTCDetails).Tabs["Details"].Visible = rbIsService.Checked;
		((EditorButtonControlBase)txtDiscountValue).ReadOnly = !rbIsService.Checked || Adding || Updating;
		((EditorButtonControlBase)txtDiscountRatio).ReadOnly = !rbIsService.Checked || Adding || Updating;
		if (Adding)
		{
			dtDetails.Rows.Clear();
			dtShipChandler.Rows.Clear();
			dtShipChandlerReturn.Rows.Clear();
			cboOnAccountOF.SelectedIndex = -1;
			CalculateGross();
			CalculateTotalsTax();
		}
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtAddedTax).Text == "" || ((Control)(object)txtAddedTax).Text == ".") ? "0" : ((Control)(object)txtAddedTax).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandler).Rows).Count; j++)
		{
			num += decimal.Parse(((UltraGridBase)ULGShipChandler).Rows[j].Cells["TaxTotalValue"].Value.ToString());
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGShipChandlerReturn).Rows).Count; k++)
		{
			num -= decimal.Parse(((UltraGridBase)ULGShipChandlerReturn).Rows[k].Cells["TaxTotalValue"].Value.ToString());
		}
		((Control)(object)txtAddedTax).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
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
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d73: Expected O, but got Unknown
		//IL_10c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationInvoices));
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
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGShipChandler = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGShipChandlerReturn = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboOnAccountOF = new UltraComboEditor();
		this.lblOnAccountOF = new UltraLabel();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.lblDiscountValue = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.rbIsItem = new System.Windows.Forms.RadioButton();
		this.rbIsService = new System.Windows.Forms.RadioButton();
		this.chkInvoiceMessage = new UltraCheckEditor();
		this.chkPrintSummary = new UltraCheckEditor();
		this.chkPrintWithTariff = new UltraCheckEditor();
		this.chkWithLogo = new UltraCheckEditor();
		this.lblGrowthFees = new UltraLabel();
		this.txtAddedTax = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandler).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandlerReturn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInvoiceMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintSummary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintWithTariff).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedTax).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "ShipChandler";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab2");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "ShipChandlerReturn";
		val2.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val2, "ultraTab3");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val3, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val4, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance13");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance14");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance15");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance16");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val9, "appearance17");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
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
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGShipChandler);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGShipChandler, "ULGShipChandler");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance1");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val12, "appearance2");
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val13, "appearance3");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance4");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance5");
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGShipChandler).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGShipChandler).Name = "ULGShipChandler";
		((UltraControlBase)this.ULGShipChandler).UseFlatMode = (DefaultableBoolean)1;
		this.ULGShipChandler.AfterEnterEditMode += new System.EventHandler(ULGShipChandler_AfterEnterEditMode);
		this.ULGShipChandler.AfterRowsDeleted += new System.EventHandler(ULGShipChandler_AfterRowsDeleted);
		this.ULGShipChandler.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGShipChandler_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGShipChandlerReturn);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGShipChandlerReturn, "ULGShipChandlerReturn");
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance6");
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val17, "appearance7");
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val18, "appearance8");
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance9");
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance10");
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGShipChandlerReturn).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGShipChandlerReturn).Name = "ULGShipChandlerReturn";
		((UltraControlBase)this.ULGShipChandlerReturn).UseFlatMode = (DefaultableBoolean)1;
		this.ULGShipChandlerReturn.AfterEnterEditMode += new System.EventHandler(ULGShipChandlerReturn_AfterEnterEditMode);
		this.ULGShipChandlerReturn.AfterRowsDeleted += new System.EventHandler(ULGShipChandlerReturn_AfterRowsDeleted);
		this.ULGShipChandlerReturn.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGShipChandlerReturn_BeforeRowsDeleted);
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
		resources.ApplyResources(this.cboOnAccountOF, "cboOnAccountOF");
		((TextEditorControlBase)this.cboOnAccountOF).AlwaysInEditMode = true;
		this.cboOnAccountOF.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOnAccountOF).Name = "cboOnAccountOF";
		((TextEditorControlBase)this.cboOnAccountOF).ValueChanged += new System.EventHandler(cboOnAccountOF_ValueChanged);
		resources.ApplyResources(this.lblOnAccountOF, "lblOnAccountOF");
		this.lblOnAccountOF.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOnAccountOF).Name = "lblOnAccountOF";
		((ControlBase)this.lblOnAccountOF).WrapText = false;
		resources.ApplyResources(this.lblOperationsNo, "lblOperationsNo");
		this.lblOperationsNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperationsNo).Name = "lblOperationsNo";
		((ControlBase)this.lblOperationsNo).WrapText = false;
		resources.ApplyResources(this.cboOperationNo, "cboOperationNo");
		((TextEditorControlBase)this.cboOperationNo).AlwaysInEditMode = true;
		this.cboOperationNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOperationNo).Name = "cboOperationNo";
		((TextEditorControlBase)this.cboOperationNo).ValueChanged += new System.EventHandler(cboOperationNo_ValueChanged);
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
		resources.ApplyResources(this.btnOperationNoSearch, "btnOperationNoSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance20");
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		resources.ApplyResources(this.lblDiscountValue, "lblDiscountValue");
		this.lblDiscountValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue).Name = "lblDiscountValue";
		((ControlBase)this.lblDiscountValue).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue, "txtDiscountValue");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).Name = "txtDiscountValue";
		((TextEditorControlBase)this.txtDiscountValue).ValueChanged += new System.EventHandler(txtDiscountValue_ValueChanged);
		resources.ApplyResources(this.lblDiscountRatio, "lblDiscountRatio");
		this.lblDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio).Name = "lblDiscountRatio";
		((ControlBase)this.lblDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio, "txtDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).Name = "txtDiscountRatio";
		((TextEditorControlBase)this.txtDiscountRatio).ValueChanged += new System.EventHandler(txtDiscountRatio_ValueChanged);
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.rbIsItem, "rbIsItem");
		this.rbIsItem.BackColor = System.Drawing.Color.Transparent;
		this.rbIsItem.Name = "rbIsItem";
		this.rbIsItem.TabStop = true;
		this.rbIsItem.UseVisualStyleBackColor = false;
		this.rbIsItem.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbIsService, "rbIsService");
		this.rbIsService.BackColor = System.Drawing.Color.Transparent;
		this.rbIsService.Name = "rbIsService";
		this.rbIsService.TabStop = true;
		this.rbIsService.UseVisualStyleBackColor = false;
		this.rbIsService.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.chkInvoiceMessage, "chkInvoiceMessage");
		((System.Windows.Forms.Control)(object)this.chkInvoiceMessage).Name = "chkInvoiceMessage";
		resources.ApplyResources(this.chkPrintSummary, "chkPrintSummary");
		((System.Windows.Forms.Control)(object)this.chkPrintSummary).Name = "chkPrintSummary";
		resources.ApplyResources(this.chkPrintWithTariff, "chkPrintWithTariff");
		((System.Windows.Forms.Control)(object)this.chkPrintWithTariff).Name = "chkPrintWithTariff";
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((UltraToggleEditorBase)this.chkWithLogo).Checked = true;
		((UltraToggleEditorBase)this.chkWithLogo).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.lblGrowthFees, "lblGrowthFees");
		this.lblGrowthFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthFees).Name = "lblGrowthFees";
		((ControlBase)this.lblGrowthFees).WrapText = false;
		resources.ApplyResources(this.txtAddedTax, "txtAddedTax");
		((System.Windows.Forms.Control)(object)this.txtAddedTax).Name = "txtAddedTax";
		((EditorButtonControlBase)this.txtAddedTax).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrowthFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddedTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPrintWithTariff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPrintSummary);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInvoiceMessage);
		base.Controls.Add(this.rbIsItem);
		base.Controls.Add(this.rbIsService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnAccountOF);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOnAccountOF);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Name = "frmOperationInvoices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOnAccountOF, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnAccountOF, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex(this.rbIsService, 0);
		base.Controls.SetChildIndex(this.rbIsItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInvoiceMessage, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPrintSummary, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPrintWithTariff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddedTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrowthFees, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandler).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGShipChandlerReturn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInvoiceMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintSummary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintWithTariff).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
