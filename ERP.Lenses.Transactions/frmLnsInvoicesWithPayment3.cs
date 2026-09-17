using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Properties;
using ERP.Sales.MasterData;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmLnsInvoicesWithPayment3 : frmHeaderDetails
{
	private InputLanguage Language;

	private DataTable dtSalesMan1;

	private DataTable dtSalesMan2;

	private DataTable dtLines;

	private DataTable dtBranches;

	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtSalesMan;

	private DataTable dtClientsGroup;

	private DataTable dtItemPrices;

	private DataTable dtPriceType;

	private DataTable dtPOSDefaultData;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtShiftDetails;

	private DataTable dtTaxs;

	private DataTable dtVisaType;

	private DataTable dtCurrency;

	private DataTable dtOffers;

	private DataTable dtAllOffers;

	private DataTable dtReservations;

	private DataTable dtReservationDetails;

	private DataTable dtCashBackOffers;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private int CashBackOfferID = 0;

	private int rowIndex = -1;

	private bool UsingColors;

	private bool UsingSizes;

	private bool IsDisplayData = false;

	private bool usingUnits = false;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int NewPriceUserID = 0;

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private ArrayList ArOfferIDs = new ArrayList();

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool UsingSalesDiscountLevels = false;

	private IContainer components = null;

	private UltraLabel lblCashAmount;

	private UltraTextEditor txtCashAmount;

	private UltraTextEditor txtAccountAmount;

	private UltraLabel lblAccountAmount;

	public UltraButton btnPriceTypeSearch;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	public UltraButton btnClientSearch;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblClient;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraComboEditor cboTax;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraCheckEditor chkTax;

	public UltraButton btnClientBalance;

	private UltraComboEditor cboClientCode;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBrabnchBalance;

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblShiftDate;

	private UltraDateTimeEditor dtpShiftDate;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMan;

	public UltraButton btnSalesManSearch;

	private UltraTextEditor txtClientCardNo;

	private UltraLabel lblClientCard;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblNetPrice;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblTaxTotalValue;

	private UltraLabel lblVisaType;

	private UltraComboEditor cboVisaType;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraTextEditor txtVisaAmount;

	private UltraLabel lblVisaAmount;

	private UltraTextEditor txtRestAmountPaid;

	private UltraLabel lblRestAmountPaid;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboMobile;

	private UltraComboEditor cboSalesMan2;

	private UltraLabel lblSalesMan2;

	public UltraButton btnSalesMan2Search;

	public UltraButton btnOffers;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsDirect;

	private RadioButton rbIsReservation;

	public UltraButton btnReservationSearch;

	private UltraLabel lblReservationNo;

	private UltraComboEditor cboReservationNo;

	public UltraButton btnClientAdd;

	private UltraButton btnStoreTransfer;

	private UltraLabel lblSerial;

	private UltraTextEditor txtSerial;

	private UltraLabel lblDiscInvRatio;

	private UltraTextEditor txtDiscInvRatio;

	private UltraLabel lblDiscInvValue;

	private UltraTextEditor txtDiscInvValue;

	private UltraTextEditor txtClientDisc;

	private UltraLabel lblClientDisc;

	private UltraCheckEditor chkApplyTax;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	public UltraButton btnLineSearch;

	private UltraLabel lblLine;

	private UltraComboEditor cboLine;

	public frmLnsInvoicesWithPayment3()
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
		TableName = "Lns_Invoices";
		IDCol = "InvoiceID";
		NoCol = "InvoiceNo";
		DateCol = "InvoiceDate";
	}

	public frmLnsInvoicesWithPayment3(int ID)
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
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		UsingSalesDiscountLevels = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from G_DiscountSettings ").Rows[0][0].ToString()) > 0;
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
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "SubAccountID", "Mobile");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		usingUnits = dtItemsUnitsBarCode.Rows.Count > 0;
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		dtAllOffers = Offers.FillCombo("Null", "-1", "-1", "-1", "-1", "1", IsFromServer: false);
		dtReservations = Reservations.FillCombo("0", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Invoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["InvoiceNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["InvoiceDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboBranches).Value = drMaster["SubAccountBranchID"];
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			IsDisplayData = true;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			cboClient_ValueChanged(null, null);
			IsDisplayData = false;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClientCode).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)cboSalesMan).Value = drMaster["EmployeeID"];
			((TextEditorControlBase)cboSalesMan2).ValueChanged -= cboSalesMan2_ValueChanged;
			((TextEditorControlBase)cboSalesMan2).Value = drMaster["EmployeeID2"];
			((TextEditorControlBase)cboSalesMan2).ValueChanged += cboSalesMan2_ValueChanged;
			((TextEditorControlBase)cboReservationNo).ValueChanged -= cboReservationNo_ValueChanged;
			((TextEditorControlBase)cboReservationNo).Value = drMaster["ReservationID"];
			rbIsDirect.Checked = bool.Parse(drMaster["IsDirect"].ToString());
			rbIsReservation.Checked = bool.Parse(drMaster["IsReservation"].ToString());
			((UltraToggleEditorBase)chkApplyTax).CheckedChanged -= chkApplyTax_CheckedChanged;
			((UltraToggleEditorBase)chkApplyTax).Checked = bool.Parse(drMaster["ApplyTax"].ToString());
			((UltraToggleEditorBase)chkApplyTax).CheckedChanged += chkApplyTax_CheckedChanged;
			((TextEditorControlBase)cboReservationNo).ValueChanged += cboReservationNo_ValueChanged;
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCashAmount).ValueChanged -= txtCashAmount_ValueChanged;
			((Control)(object)txtCashAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCashAmount).ValueChanged += txtCashAmount_ValueChanged;
			((TextEditorControlBase)txtAccountAmount).ValueChanged -= txtAccountAmount_ValueChanged;
			((Control)(object)txtAccountAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtAccountAmount).ValueChanged += txtAccountAmount_ValueChanged;
			((Control)(object)txtDiscInvValue).Text = decimal.Parse(drMaster["DiscountInvoiceValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscInvRatio).Text = decimal.Parse(drMaster["DiscountInvoiceRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboVisaType).Value = drMaster["VisaTypeID"];
			((Control)(object)txtVisaNo).Text = drMaster["VisaNo"].ToString();
			((TextEditorControlBase)txtVisaAmount).ValueChanged -= txtVisaAmount_ValueChanged;
			((Control)(object)txtVisaAmount).Text = decimal.Parse(drMaster["VisaAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtVisaAmount).ValueChanged += txtVisaAmount_ValueChanged;
			if (rbIsDirect.Checked)
			{
				((Control)(object)txtRestAmountPaid).Text = decimal.Parse(drMaster["RestAmountPaid"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtRestAmountPaid).Text = "0";
			}
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = InvoicesDetails.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (dtDetails.Select("OfferID Is not Null and DiscountInvoice > 0").Length != 0)
			{
				CashBackOfferID = int.Parse(dtDetails.Select("OfferID Is not Null and DiscountInvoice > 0")[0]["OfferID"].ToString());
			}
			dtDetails.AcceptChanges();
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || !CanEditFromServer)
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			CalcQuantity();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	private void CalcQuantity()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
		}
		((Control)(object)txtTotalQty).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDetailID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingColors)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		if (UsingSizes)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		}
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			if (usingUnits)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			}
			else
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = true;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			if (usingUnits)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			}
			else
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = true;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Header).Caption = (GlobalVariables.IsArabic ? "%الخصم" : "Discount Ratio");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OfferDiscountRatio"].DefaultCellValue = decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountInvoice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OfferDiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Hidden = !bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && !Adding && !Updating;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Header).Caption = (GlobalVariables.IsArabic ? "الرصيد" : "Stock Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((Control)(object)chkApplyTax).Visible = dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["AllowEditTax"].ToString());
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode || rbIsReservation.Checked || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode || rbIsReservation.Checked || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboMobile).ReadOnly = NavMode || rbIsReservation.Checked || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((Control)(object)lblClientCard).Visible = UsingSalesDiscountLevels && rbIsDirect.Checked;
		((Control)(object)txtClientCardNo).Visible = UsingSalesDiscountLevels && rbIsDirect.Checked;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)chkBranches).Enabled = Adding;
		((EditorButtonControlBase)cboPriceType).ReadOnly = !CanModifyPriceType || rbIsReservation.Checked;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)btnStoreTransfer).Visible = !NavMode;
		((EditorButtonControlBase)cboReservationNo).ReadOnly = NavMode || Updating;
		rbIsDirect.Enabled = !NavMode && !Updating;
		rbIsReservation.Enabled = !NavMode && !Updating;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)cboReservationNo).Visible = rbIsReservation.Checked;
		((Control)(object)lblReservationNo).Visible = rbIsReservation.Checked;
		((Control)(object)btnReservationSearch).Visible = rbIsReservation.Checked;
		((Control)(object)txtBrabnchBalance).Visible = !NavMode;
		((Control)(object)lblBalance).Visible = !NavMode;
		((EditorButtonControlBase)txtAccountAmount).ReadOnly = NavMode || rbIsReservation.Checked;
		((EditorButtonControlBase)txtCashAmount).ReadOnly = NavMode || rbIsReservation.Checked;
		((EditorButtonControlBase)cboVisaType).ReadOnly = NavMode || rbIsReservation.Checked;
		((EditorButtonControlBase)txtVisaNo).ReadOnly = NavMode || rbIsReservation.Checked;
		((EditorButtonControlBase)txtVisaAmount).ReadOnly = NavMode || rbIsReservation.Checked;
		((EditorButtonControlBase)txtRestAmountPaid).ReadOnly = NavMode || rbIsReservation.Checked;
		((Control)(object)btnClientAdd).Visible = !NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode && rbIsDirect.Checked;
		((Control)(object)btnReservationSearch).Visible = !NavMode && rbIsReservation.Checked && Adding;
		((Control)(object)btnClientBalance).Visible = !NavMode && rbIsDirect.Checked;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && !CanModifyPriceType && rbIsDirect.Checked;
		((Control)(object)btnSalesManSearch).Visible = !NavMode;
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((Control)(object)dtpShiftDate).Visible = !Adding;
		((Control)(object)txtShiftNo).Visible = !Adding;
		((Control)(object)lblShiftDate).Visible = !Adding;
		((Control)(object)lblShiftNo).Visible = !Adding;
		((Control)(object)btnOffers).Visible = ((Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows.Count > 0 && !NavMode) ? true : false);
		object value = ((TextEditorControlBase)cboSalesMan).Value;
		object value2 = ((TextEditorControlBase)cboSalesMan2).Value;
		if (Updating || Adding)
		{
			if ((Updating && drMaster != null && drMaster["SubAccountID"] != DBNull.Value && dtClients.Select("SubAccountID= " + drMaster["SubAccountID"].ToString())[0]["EmployeeID"] != DBNull.Value) || (Adding && cboClient.SelectedIndex > -1 && dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"] != DBNull.Value))
			{
				DataView dataView = new DataView(dtSalesMan);
				dataView.RowFilter = " SubAccountID=" + dtClients.Select("SubAccountID= " + (Adding ? ((TextEditorControlBase)cboClient).Value.ToString() : drMaster["SubAccountID"].ToString()))[0]["EmployeeID"].ToString();
				GlobalFunctions.FillCombo(cboSalesMan2, dataView.ToTable(), "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = false;
			}
			else if ((Updating && drMaster != null && drMaster["LineID"] != DBNull.Value) || (Adding && cboLine.SelectedIndex > -1))
			{
				dtSalesMan2 = SubAccounts.SelectSalesMan2ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, Adding ? ((TextEditorControlBase)cboLine).Value.ToString() : drMaster["LineID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan2, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = false;
			}
			else
			{
				DataView dataView2 = new DataView(dtSalesMan);
				dataView2.RowFilter = " BranchID = " + GlobalVariables.CurrentBranchID + " Or ForAllBranches = 1";
				GlobalFunctions.FillCombo(cboSalesMan2, dataView2.ToTable(), "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = Adding || Updating;
			}
			if ((Updating && drMaster != null && drMaster["LineID"] != DBNull.Value) || (Adding && cboLine.SelectedIndex > -1))
			{
				dtSalesMan1 = SubAccounts.SelectSalesMan1ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, Adding ? ((TextEditorControlBase)cboLine).Value.ToString() : drMaster["LineID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan1, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesManSearch).Visible = false;
			}
			else
			{
				DataView dataView3 = new DataView(dtSalesMan);
				dataView3.RowFilter = " BranchID = " + GlobalVariables.CurrentBranchID + " Or ForAllBranches = 1";
				GlobalFunctions.FillCombo(cboSalesMan, dataView3.ToTable(), "SubAccountID", "SubAccountName");
			}
		}
		else
		{
			DataView dataView4 = new DataView(dtSalesMan);
			dataView4.RowFilter = " BranchID = " + GlobalVariables.CurrentBranchID + " Or ForAllBranches = 1";
			GlobalFunctions.FillCombo(cboSalesMan, dataView4.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboSalesMan2, dataView4.ToTable(), "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboSalesMan).Value = value;
		((TextEditorControlBase)cboSalesMan2).Value = value2;
		if (Adding || Updating)
		{
			if (Adding)
			{
				DataView dataView5 = new DataView(dtReservations);
				dataView5.RowFilter = " IsDeliverd = 0 And BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboReservationNo, dataView5.ToTable(), "ReservationID", "ReservationNo");
			}
			else
			{
				GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
			}
			DataView dataView6 = new DataView(dtStores);
			dataView6.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView6.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		if (Adding)
		{
			DataView dataView7 = new DataView(dtItems);
			dataView7.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView7.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["ItemBarCode"].ToString());
			}
			DataView dataView8 = new DataView(dtVisaType);
			dataView8.RowFilter = " IsActive =1 ";
			DataTable dt = dataView8.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
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
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
		if (!Updating)
		{
			return;
		}
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

	public override void ClearControls()
	{
		base.ClearControls();
		NewPriceUserID = 0;
		DiscountUserID = 0;
		CashBackOfferID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		cboLine.SelectedIndex = -1;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)cboReservationNo).ValueChanged -= cboReservationNo_ValueChanged;
		cboReservationNo.SelectedIndex = -1;
		((TextEditorControlBase)cboReservationNo).ValueChanged += cboReservationNo_ValueChanged;
		rbIsReservation.Checked = false;
		rbIsDirect.Checked = true;
		cboSalesMan.SelectedIndex = -1;
		((TextEditorControlBase)cboSalesMan2).ValueChanged -= cboSalesMan2_ValueChanged;
		cboSalesMan2.SelectedIndex = -1;
		((TextEditorControlBase)cboSalesMan2).ValueChanged += cboSalesMan2_ValueChanged;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkApplyTax).Checked = dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString());
		((Control)(object)txtDiscInvValue).Text = "0";
		((Control)(object)txtDiscInvRatio).Text = "0";
		((TextEditorControlBase)txtBrabnchBalance).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((Control)(object)txtDiscInvValue).Text = "0";
		((Control)(object)txtDiscInvRatio).Text = "0";
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtCashAmount).ValueChanged -= txtCashAmount_ValueChanged;
		((Control)(object)txtCashAmount).Text = "0";
		((TextEditorControlBase)txtCashAmount).ValueChanged += txtCashAmount_ValueChanged;
		((TextEditorControlBase)txtAccountAmount).ValueChanged -= txtAccountAmount_ValueChanged;
		((Control)(object)txtAccountAmount).Text = "0";
		((TextEditorControlBase)txtAccountAmount).ValueChanged += txtAccountAmount_ValueChanged;
		cboVisaType.SelectedIndex = -1;
		((TextEditorControlBase)txtVisaNo).Clear();
		((TextEditorControlBase)txtVisaAmount).ValueChanged -= txtVisaAmount_ValueChanged;
		((Control)(object)txtVisaAmount).Text = "0";
		((TextEditorControlBase)txtVisaAmount).ValueChanged += txtVisaAmount_ValueChanged;
		((Control)(object)txtRestAmountPaid).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)txtClientDisc).ValueChanged -= txtClientDisc_ValueChanged;
		((Control)(object)txtClientDisc).Text = "0";
		((TextEditorControlBase)txtClientDisc).ValueChanged += txtClientDisc_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		if (dtCashBackOffers == null || dtCashBackOffers.Rows.Count == 0)
		{
			dtCashBackOffers = OffersCashBack.FillCombo(dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] != DBNull.Value && Adding)
		{
			((TextEditorControlBase)cboClient).Value = dtPOSDefaultData.Rows[0]["DefaultSubAccountID"];
		}
	}

	public override void CallButtons(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1 && ((Control)(object)btnAdd).Enabled && ((Control)(object)btnAdd).Visible)
		{
			SendKeys.Send("{tab}");
			btnAddClick();
		}
		else if (e.KeyCode == Keys.F2 && ((Control)(object)btnUpdate).Enabled && ((Control)(object)btnUpdate).Visible)
		{
			btnUpdateClick();
		}
		else if (e.KeyCode == Keys.F3 && ((Control)(object)btnDelete).Enabled && ((Control)(object)btnDelete).Visible)
		{
			btnDeleteClick();
		}
		else if (e.KeyCode == Keys.F4 && ((Control)(object)btnPrint).Enabled && ((Control)(object)btnPrint).Visible)
		{
			btnPrint_Click(null, null);
		}
		else if (e.KeyCode == Keys.F5 && ((Control)(object)btnRefreshData).Enabled && ((Control)(object)btnRefreshData).Visible)
		{
			btnRefreshDataClick();
		}
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void btnSaveClose_Click(object sender, EventArgs e)
	{
		if (ValidateForShift())
		{
			base.btnSaveClose_Click(sender, e);
		}
	}

	public override void btnOKClick()
	{
		if (ValidateForShift())
		{
			base.btnOKClick();
		}
	}

	public override void btnUpdateClick()
	{
		if (Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && Invoices.SyncCanUpdate(RowID) == 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان تم تعديله في المركز الرئيسي برجاء الانتظار حتي الانتهاء من تحديث البيانات من الخادم  ", "Can not Update this Data. This data is Modified in The Central Point Please wait for Syncronization ");
		}
		else
		{
			base.btnUpdateClick();
		}
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		string val = "";
		ReportDocument reportDocument = new ReportDocument();
		if (dtReports.Rows.Count > 0)
		{
			if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_InvoicesFastPrint")
			{
				FastPrint();
				return;
			}
			reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
		}
		else
		{
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_Invoices_A.rpt" : "Rep_Lns_Invoices_E.rpt"));
		}
		if (dtReports.Rows.Count > 0 && dtReports.Rows[0]["ProcedureName"].ToString().Trim() == "QRBarcode")
		{
			GlobalFunctions.GenerateVoucherQR("," + RowID + ",", "Lns_Invoices");
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@InvoiceID", RowID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
		}
		else
		{
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@InvoiceID", RowID);
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			if (dtReports.Rows.Count > 0 && dtReports.Rows[0]["ProcedureName"].ToString().Trim() != "UnitPriceWithTaxes")
			{
				reportDocument.SetParameterValue("@InvoiceID", RowID, "Tax name");
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
			}
			reportDocument.SetParameterValue("@Balance", val);
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
		}
		try
		{
			reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
		}
		finally
		{
			if (dtReports.Rows.Count > 0 && dtReports.Rows[0]["ProcedureName"].ToString().Trim() == "QRBarcode")
			{
				GlobalFunctions.DeleteVoucherQR("," + RowID + ",", "Lns_Invoices");
			}
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public void FastPrint()
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_Lns_Invoices " + RowID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable2 = Main.ExecuteQuery_DataTable(" Rep_POS_Settings_SelectByBranchID " + GlobalVariables.CurrentBranchID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable3 = Main.ExecuteQuery_DataTable(" Rep_Lns_InvoicesByTaxGrouped " + RowID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		Color black = Color.Black;
		Color darkBlue = Color.DarkBlue;
		Color black2 = Color.Black;
		float lineWidth = 0.03f;
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 6f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 7f, FontStyle.Regular);
		Font font5 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font6 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font7 = new Font("Times New Roman", 8f, FontStyle.Bold);
		Font font8 = new Font("Free 3 of 9 Extended", 18f, FontStyle.Regular);
		FastPrint instance = ERP.Classes.DirectPrinting.FastPrint.Instance;
		instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
		instance.GraphicsUnit = GraphicsUnit.Millimeter;
		instance.Margins = new Margins(0, 0, 0, 0);
		instance.OverallWidth = 70f;
		try
		{
			if (dataTable2.Rows[0]["Logo"] != null)
			{
				Image image = GlobalFunctions.BinaryToImage((byte[])dataTable2.Rows[0]["Logo"]);
				float num = 20f;
				float value = 20f;
				instance.AddEmptyCell((1f - num / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
				instance.AddImageCell(image, num / instance.OverallWidth, value, DrawRectangle: false);
				instance.AddEmptyCell((1f - num / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
				instance.AcceptChanges();
			}
		}
		catch
		{
		}
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AddTextCell(dataTable2.Rows[0]["CompanyName"].ToString(), font, 44f / instance.OverallWidth, 7f, StringAlignment.Center, black, DrawRectangle: false);
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("*" + dataRow["InvoiceNo"].ToString() + "*", font8, 0.5f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell(dataRow["InvoiceNo"].ToString(), font7, 0.2f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("فاتورة مبيعات برقم ", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(((DateTime)dataRow["InvoiceDate"]).ToString("dd/MM/yyyy hh:mm:ss tt"), font7, 0.7f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("بتاريخ", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["BranchName"].ToString(), font7, 0.7f, 5f, StringAlignment.Near, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("فرع", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["UserName"].ToString(), font7, 0.7f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اسم البائع", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["ClientName"].ToString(), font7, 0.7f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("العميل", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		if (dataRow["SalesManName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["SalesManName"].ToString(), font5, 0.7f, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("اسم مندوب ", font5, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 3f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font2, 18f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("سعر الوحده", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الوحده", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الكميه", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الصنف", font2, 28f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		foreach (DataRow row in dataTable.Rows)
		{
			instance.AddTextCell(decimal.Parse(row["TotalPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 18f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(decimal.Parse(row["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["UnitName"].ToString(), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(decimal.Parse(row["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["ItemName"].ToString(), font3, 28f / instance.OverallWidth, 4f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(decimal.Parse(dataRow["GrossValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الإجمالي", font6, 8f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell(decimal.Parse(((Control)(object)txtTotalQty).Text.ToString(), NumberStyles.Float).ToString("0.##"), font3, float.Parse((16f / instance.OverallWidth).ToString()), 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("إجمالي الكميه", font6, float.Parse((28f / instance.OverallWidth).ToString()), 5f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("خصم قبل الضرائب", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["DiscountInvoiceValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["DiscountInvoiceValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("خصم الكاش باك", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) != 0m)
		{
			try
			{
				if (dataTable3 != null)
				{
					foreach (DataRow row2 in dataTable3.Rows)
					{
						instance.AddTextCell(decimal.Parse(row2["TaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
						instance.AddTextCell(row2["TaxName"].ToString(), font6, 52f / instance.OverallWidth, 4f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
						instance.AcceptChanges();
					}
				}
			}
			catch
			{
			}
		}
		if (decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("خصم بعد الضرائب", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["CommercialTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["CommercialTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("ضريبة ارباح تجارية وصناعية", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["NetPrice"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["NetPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("صافى الفاتورة", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["PaidAmount"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["PaidAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("المدفوع", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RestAmount"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["RestAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell("المتبقى", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(decimal.Parse(dataRow["CurrentBalance"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 42f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الرصيد عند الحالى بالفرع", font6, 28f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) != 0m)
		{
			instance.AddTextCell(dataTable2.Rows[0]["TaxNo"].ToString(), font3, 0.7f, 6f, StringAlignment.Far, black, DrawRectangle: false, black2, lineWidth);
			instance.AddTextCell("الرقم الضريبي : ", font5, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: false, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dataTable2.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataTable2.Rows[0]["Message"].ToString().Trim(), font2, 1f, float.Parse((Math.Ceiling((double)dataTable2.Rows[0]["Message"].ToString().Trim().Length / 85.0) * 3.0).ToString()), StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		instance.AddTextCell("Powered by Future Solutions : www.fs-scs.com", font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		instance.PrinterSettings.Copies = 1;
		instance.Print();
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsInvoicesReport(-1, 0, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["InvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboVisaType).Value;
		object value2 = ((TextEditorControlBase)cboTax).Value;
		object value3 = ((TextEditorControlBase)cboPriceType).Value;
		object value4 = ((TextEditorControlBase)cboReservationNo).Value;
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
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		usingUnits = dtItemsUnitsBarCode.Rows.Count > 0;
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
			DataView dataView3 = new DataView(dtVisaType);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dt = dataView3.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
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
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxs.Rows.Count; num2++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtReservations = Reservations.FillCombo("0", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
		((TextEditorControlBase)cboVisaType).Value = value;
		((TextEditorControlBase)cboTax).Value = value2;
		((TextEditorControlBase)cboPriceType).Value = value3;
		((TextEditorControlBase)cboReservationNo).Value = value4;
	}

	public override void btnDeleteClick()
	{
		if (ValidateForShift())
		{
			base.btnDeleteClick();
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

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		frmPricesTypesChange frmPricesTypesChange2 = new frmPricesTypesChange(base.Name);
		frmPricesTypesChange2.WindowState = FormWindowState.Normal;
		frmPricesTypesChange2.ShowDialog();
		((TextEditorControlBase)cboPriceType).Value = ((frmPricesTypesChange2.PriceTypeID > 0) ? ((object)frmPricesTypesChange2.PriceTypeID) : ((TextEditorControlBase)cboPriceType).Value);
		NewPriceUserID = frmPricesTypesChange2.UserID;
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

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الأذن", "Please Enter The Voucher Date");
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
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم  الأذن", "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء نوع السعر", "Please Select Price Type");
			((TextEditorControlBase)cboPriceType).Focus();
			return false;
		}
		if (Convert.ToBoolean(dtPOSDefaultData.Rows[0]["EnforceSalesManSelection"]) && cboSalesMan.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار رجل البيع ", "Please Select SalesMan");
			((TextEditorControlBase)cboSalesMan).Focus();
			return false;
		}
		if (decimal.Parse(((Control)(object)txtRestAmountPaid).Text) < 0m)
		{
			GlobalVariables.QuestionMB.Show("القيمة المدفوعة اكبر من إجمالى الفاتورة هل تريد الحفظ؟", "Paid Amount Greater than Invoice Total Amount Are you Sure ?");
			if (GlobalVariables.MessageBoxResult == 'N')
			{
				((TextEditorControlBase)txtCashAmount).Focus();
				return false;
			}
		}
		if (rbIsReservation.Checked)
		{
			if (decimal.Parse(dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["PaidAmount"].ToString()) < decimal.Parse(((Control)(object)txtNetprice).Text))
			{
				GlobalVariables.InformationMB.Show("القيمة المدفوعة أقل من إجمالى الفاتورة", "Paid Amount Less than Invoice Total Amount");
				return false;
			}
		}
		else if (decimal.Parse(((Control)(object)txtRestAmountPaid).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("القيمة المدفوعة أقل من إجمالى الفاتورة", "Paid Amount Less than Invoice Total Amount");
			((TextEditorControlBase)txtCashAmount).Focus();
			return false;
		}
		if (((Control)(object)txtVisaAmount).Text != "" && decimal.Parse(((Control)(object)txtVisaAmount).Text) > 0m && cboVisaType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال نوع الفيزا", "Please Enter Visa Type");
			((TextEditorControlBase)cboVisaType).Focus();
			return false;
		}
		if (((Control)(object)txtVisaAmount).Text != "" && decimal.Parse(((Control)(object)txtVisaAmount).Text) > 0m && ((Control)(object)txtVisaNo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم الفيزا", "Please Enter Visa No");
			((TextEditorControlBase)txtVisaNo).Focus();
			return false;
		}
		((UltraGridBase)ULGData).UpdateData();
		decimal num = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["CreditLimit"].ToString());
		if (decimal.Parse(((Control)(object)txtBrabnchBalance).Text) + decimal.Parse((((Control)(object)txtAccountAmount).Text == "" || ((Control)(object)txtAccountAmount).Text == ".") ? "0" : ((Control)(object)txtAccountAmount).Text) - ((Adding || (drMaster != null && drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboClient).Value.ToString())) ? 0m : ((drMaster != null) ? decimal.Parse(drMaster["RestAmount"].ToString()) : decimal.Parse((((Control)(object)txtAccountAmount).Text == "" || ((Control)(object)txtAccountAmount).Text == ".") ? "0" : ((Control)(object)txtAccountAmount).Text))) > num && num != 0m)
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع قبل أخر إعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n  على مخزن   " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n  The Date you choosed Before Last Store Taking With Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_Invoices", "InvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["InvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		return true;
	}

	public bool ValidateForShift()
	{
		DataTable dataTable = BusinessLayer.POS.Settings.ValidateCashierData(GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		if (dataTable.Rows[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد الحساب التحليلى للمستخدم  ", "Please Set user SubAccount");
			return false;
		}
		if (dataTable.Rows[0]["CashierAccount"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الكاشير من إعدادات البيع المباشر  ", "Please Set Cashier Account From POS Setting");
			return false;
		}
		if (int.Parse(dataTable.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		DataTable dataTable2 = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return false;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Second);
		if (GlobalFunctions.GetServerDateTimeNow() < dateTime)
		{
			GlobalVariables.InformationMB.Show(dateTime.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime.ToShortTimeString());
			return false;
		}
		if (GlobalFunctions.GetServerDateTimeNow() > dateTime2.AddHours(2.0))
		{
			GlobalVariables.InformationMB.Show(dateTime2.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime2.ToShortTimeString());
			return false;
		}
		if (dtpDate.DateTime < DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return false;
		}
		ShiftDetailID = dataTable2.Rows[0]["ShiftDetailID"].ToString();
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (!Updating)
		{
			DataTable dataTable3 = ShiftsDetailsUsers.SelectNotClosed(ShiftDetailID, GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
			if (dataTable3.Rows.Count == 0)
			{
				ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
			}
			else
			{
				ShiftDetailUserID = dataTable3.Rows[0]["ShiftDetailUserID"].ToString();
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Expected O, but got Unknown
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
				if (CashBackOfferID != 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value = CashBackOfferID;
				}
			}
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			num = Invoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsReservation.Checked ? "1" : "0", (cboReservationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReservationNo).Value.ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), "Null", (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "Null", (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), "Null", "Null", "0", ((UltraToggleEditorBase)chkApplyTax).Checked ? "1" : "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscInvValue).Text == "") ? "0" : ((Control)(object)txtDiscInvValue).Text, (((Control)(object)txtDiscInvRatio).Text == "") ? "0" : ((Control)(object)txtDiscInvRatio).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", "0", (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtCashAmount).Text == "") ? "0" : ((Control)(object)txtCashAmount).Text, (((Control)(object)txtAccountAmount).Text == "") ? "0" : ((Control)(object)txtAccountAmount).Text, (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, (((Control)(object)txtRestAmountPaid).Text == "") ? "0" : ((Control)(object)txtRestAmountPaid).Text, ((Control)(object)txtNotes).Text, "0", "Null", ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "Null", "1", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["InvoiceID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["InvoiceDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[j].Cells["ReturnedQty"].Value = 0;
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[j].Cells["VoucherDate"].Value = dtpDate.DateTime;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
			dataTable.AcceptChanges();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				dataTable.Rows[k].SetAdded();
			}
			InvoicesDetails.Insert_UpdateByTableXML(dataTable, "InvoiceDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.SalesJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
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
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		if (DataSaved)
		{
			ItemsTransactions.ManageInThread();
			RowID = num.ToString();
		}
		if (!DataSaved || !(GlobalVariables.POSPrinter != ""))
		{
			return;
		}
		string val = "";
		GlobalVariables.QuestionMB.Show("هل تريد طباعة الفاتورة ؟", "Are You Sure You want to Print This Invoice?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_InvoicesFastPrint")
				{
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_Invoices_A.rpt" : "Rep_Lns_Invoices_E.rpt"));
			}
			if (dtReports.Rows.Count > 0 && dtReports.Rows[0]["ProcedureName"].ToString().Trim() == "QRBarcode")
			{
				GlobalFunctions.GenerateVoucherQR("," + RowID + ",", "Lns_Invoices");
				GlobalFunctions.ConfigureReport(reportDocument);
				reportDocument.SetParameterValue("@InvoiceID", RowID);
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			}
			else
			{
				GlobalFunctions.ConfigureReport(reportDocument);
				reportDocument.SetParameterValue("@InvoiceID", num);
				reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				reportDocument.SetParameterValue("@InvoiceID", num, "Tax name");
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
				reportDocument.SetParameterValue("@Balance", val);
				reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			}
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
			finally
			{
				if (dtReports.Rows.Count > 0 && dtReports.Rows[0]["ProcedureName"].ToString().Trim() == "QRBarcode")
				{
					GlobalFunctions.DeleteVoucherQR("," + RowID + ",", "Lns_Invoices");
				}
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void UpdateData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Expected O, but got Unknown
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
				if (CashBackOfferID != 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value = CashBackOfferID;
				}
			}
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Invoices.Insert_Update(drMaster["InvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsReservation.Checked ? "1" : "0", (cboReservationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReservationNo).Value.ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), (drMaster["ClientSupplierContactID"] == DBNull.Value) ? "Null" : drMaster["ClientSupplierContactID"].ToString(), (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "Null", (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), "Null", "Null", "0", ((UltraToggleEditorBase)chkApplyTax).Checked ? "1" : "0", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscInvValue).Text == "") ? "0" : ((Control)(object)txtDiscInvValue).Text, (((Control)(object)txtDiscInvRatio).Text == "") ? "0" : ((Control)(object)txtDiscInvRatio).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", "0", (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtCashAmount).Text == "") ? "0" : ((Control)(object)txtCashAmount).Text, (((Control)(object)txtAccountAmount).Text == "") ? "0" : ((Control)(object)txtAccountAmount).Text, (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, (((Control)(object)txtRestAmountPaid).Text == "") ? "0" : ((Control)(object)txtRestAmountPaid).Text, ((Control)(object)txtNotes).Text, "0", "Null", drMaster["ShiftDetailID"].ToString(), drMaster["ShiftDetailUserID"].ToString(), drMaster["User_ID"].ToString(), (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), (drMaster["EInvoiceInternalCode"] == DBNull.Value) ? "Null" : drMaster["EInvoiceInternalCode"].ToString(), (drMaster["EInvoiceUUID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceUUID"].ToString(), (drMaster["EInvoiceSenDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceSenDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceSendUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceSendUserID"].ToString(), bool.Parse(drMaster["EInvoiceIsCanceled"].ToString()) ? "1" : "0", (drMaster["EInvoiceCanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceCanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceCanceledUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceCanceledUserID"].ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "Null", "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["InvoiceID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[j].Cells["VoucherDate"].Value = dtpDate.DateTime;
			}
			((UltraGridBase)ULGData).UpdateData();
			InvoicesDetails.Insert_UpdateByTableXML((DataTable)((UltraGridBase)ULGData).DataSource, "InvoiceDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ShiftsDetails.SalesJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.RevenueJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
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
			Invoices.DeleteVirtual(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			InvoicesDetails.DeleteVirtualByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			ShiftsDetails.SalesJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.RevenueJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTotalQty).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		Row.Cells["Discount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(Row.Cells["DiscountRatio"].Value.ToString()) / 100m;
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) - decimal.Parse(Row.Cells["DiscountInvoice"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalcTotalDiscountBeforeTax()
	{
		bool flag = false;
		if (decimal.Parse(((Control)(object)txtGrossValue).Text.ToString()) != 0m)
		{
			decimal num = default(decimal);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString());
				if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) > 0m)
				{
					flag = true;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["offerID"].Value != DBNull.Value && decimal.Parse((((Control)(object)txtDiscInvValue).Text == "" || ((Control)(object)txtDiscInvValue).Text == ".") ? "0" : ((Control)(object)txtDiscInvValue).Text) == 0m)
				{
					flag = true;
				}
			}
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = (num / decimal.Parse(((Control)(object)txtGrossValue).Text.ToString()) * 100m).ToString(GlobalVariables.txtDecimalFormate);
			if (!flag)
			{
				decimal num2 = default(decimal);
				num2 = (bool.Parse(dtPOSDefaultData.Rows[0]["ApplyCashBackWithDiscount"].ToString()) ? (decimal.Parse(((Control)(object)txtGrossValue).Text.ToString()) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text.ToString())) : CalculateTotalValueForCashBack());
				DataRow[] array = dtCashBackOffers.Select("FromAmount <= " + num2 + " And ToAmount >= " + num2, "FromAmount");
				if (array.Length != 0)
				{
					if (bool.Parse(array[0]["IsRatio"].ToString()))
					{
						((Control)(object)txtDiscInvValue).Text = (decimal.Parse(array[0]["Value"].ToString()) * num2 / 100m).ToString(GlobalVariables.txtDecimalFormate);
						((Control)(object)txtDiscInvRatio).Text = array[0]["Value"].ToString();
					}
					else
					{
						((Control)(object)txtDiscInvValue).Text = array[0]["Value"].ToString();
						((Control)(object)txtDiscInvRatio).Text = (decimal.Parse(array[0]["Value"].ToString()) / num2 * 100m).ToString(GlobalVariables.txtDecimalFormate);
					}
					CashBackOfferID = int.Parse(array[0]["OfferID"].ToString());
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
					{
						((UltraGridBase)ULGData).Rows[j].Cells["DiscountInvoice"].Value = decimal.Parse(((Control)(object)txtDiscInvRatio).Text) * (decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString())) / 100m;
						CalculateRow(((UltraGridBase)ULGData).Rows[j]);
					}
				}
				else
				{
					((Control)(object)txtDiscInvValue).Text = "0";
					((Control)(object)txtDiscInvRatio).Text = "0";
					CashBackOfferID = 0;
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["DiscountInvoice"].Value = 0;
						CalculateRow(((UltraGridBase)ULGData).Rows[k]);
					}
					CheckForCashBackOffer();
				}
			}
			else
			{
				((Control)(object)txtDiscInvValue).Text = "0";
				((Control)(object)txtDiscInvRatio).Text = "0";
				CashBackOfferID = 0;
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["DiscountInvoice"].Value = 0;
					CalculateRow(((UltraGridBase)ULGData).Rows[l]);
				}
			}
		}
		else
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
			((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
			((Control)(object)txtDiscInvValue).Text = "0";
			((Control)(object)txtDiscInvRatio).Text = "0";
			CashBackOfferID = 0;
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
			{
				((UltraGridBase)ULGData).Rows[m].Cells["DiscountInvoice"].Value = 0;
				CalculateRow(((UltraGridBase)ULGData).Rows[m]);
			}
		}
		CalculateTotalsTax();
	}

	private decimal CalculateTotalValueForCashBack()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			DataRow dataRow = null;
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0)
			{
				dataRow = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
			}
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value.ToString()) > 0m || (dataRow != null && decimal.Parse(dataRow["DiscountPercentage"].ToString()) != 0m))
			{
				result += 0m;
			}
			else
			{
				result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
			}
		}
		return result;
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscInvValue).Text == "" || ((Control)(object)txtDiscInvValue).Text == ".") ? "0" : ((Control)(object)txtDiscInvValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (rbIsDirect.Checked)
		{
			((Control)(object)txtRestAmountPaid).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtCashAmount).Text == "" || ((Control)(object)txtCashAmount).Text == ".") ? "0" : ((Control)(object)txtCashAmount).Text) - decimal.Parse((((Control)(object)txtAccountAmount).Text == "" || ((Control)(object)txtAccountAmount).Text == ".") ? "0" : ((Control)(object)txtAccountAmount).Text) - decimal.Parse((((Control)(object)txtVisaAmount).Text == "" || ((Control)(object)txtVisaAmount).Text == ".") ? "0" : ((Control)(object)txtVisaAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtRestAmountPaid).Text = "0";
		}
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) - decimal.Parse(Row.Cells["DiscountInvoice"].Value.ToString())) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
		bool flag = false;
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select("OfferDiscountRatio > 0").Length != 0)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && (decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m || ((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value))
				{
					result += 0m;
					continue;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
				result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
			}
		}
		else
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value != DBNull.Value && (decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m || decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["DiscountInvoice"].Value.ToString()) > 0m))
				{
					result += 0m;
					continue;
				}
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString());
				result += decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString());
			}
		}
		return result;
	}

	public void AddItemInGid()
	{
		//IL_17a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ae: Expected O, but got Unknown
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Expected O, but got Unknown
		//IL_146b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1475: Expected O, but got Unknown
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_1763: Unknown result type (might be due to invalid IL or missing references)
		//IL_176d: Expected O, but got Unknown
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Expected O, but got Unknown
		//IL_2360: Unknown result type (might be due to invalid IL or missing references)
		//IL_236a: Expected O, but got Unknown
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Expected O, but got Unknown
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
		string text5 = ((!flag) ? dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString() : dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + text + "'")[0]["ItemID"].ToString());
		if (GlobalFunctions.GetOption("UseScaleBarCode") && dtItems.Select(" ItemID = '" + text5 + "'")[0]["IsWeight"] != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + text5 + "'")[0]["IsWeight"].ToString()))
		{
			s = (decimal.Parse(s) / 1000m).ToString();
		}
		object value;
		if (flag)
		{
			DataRow dataRow = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + text + "'")[0];
			text = dtItems.Select("ItemID = " + text5)[0]["ItemBarCode"].ToString();
			string text6 = dataRow["UnitID"].ToString();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (!(((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text) || !(((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4) || !(((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2) || !(((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3) || !(((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() == text6))
				{
					continue;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
				{
					decimal num = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["StockBalance"].Value.ToString());
					if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > num)
					{
						GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num, 3)) : ("Maximum Allowed Qty " + num));
						((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = num;
					}
				}
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				rowIndex = i;
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = text5);
			obj.Value = value;
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'")[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = text6;
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["IsBarcodeRead"].Value = true;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
			if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
				decimal num2 = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value.ToString());
				if (decimal.Parse(s) > num2)
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num2, 3)) : ("Maximum Allowed Qty " + num2));
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2;
				}
			}
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(" UnitID = " + text6 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
				if (array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					if (decimal.Parse(array[0]["DiscountPercentage"].ToString()) != 0m)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(array[0]["DiscountPercentage"].ToString());
						((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(((Control)(object)txtClientDisc).Text);
						((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtClientDisc).Text) / 100m;
					}
					if (decimal.Parse(array[0]["DiscountPercentage"].ToString()) > 0m)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(array[0]["DiscountPercentage"].ToString());
					}
				}
				else
				{
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj3.Value = value;
				}
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalcTotalDiscountBeforeTax();
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
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			return;
		}
		DataRow dataRow2 = dtItems.Select(" ItemBarcode = '" + text + "'")[0];
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (!(((UltraGridBase)ULGData).Rows[j].Cells["ItemBarCode"].Text == text) || !(((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Text == text4) || !(((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() == text2) || !(((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() == text3) || ((UltraGridBase)ULGData).Rows[j].Cells["OfferID"].Value != DBNull.Value || (bool.Parse(dataRow2["IsUnitPrice"].ToString()) && !((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString().Equals(dataRow2["UnitID"].ToString())))
			{
				continue;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
			if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				decimal num3 = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["StockBalance"].Value.ToString());
				if (decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) > num3)
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num3, 3)) : ("Maximum Allowed Qty " + num3));
					((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = num3;
				}
			}
			((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
			rowIndex = j;
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).Rows[j]);
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow2["ItemID"].ToString());
		obj5.Value = value;
		if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dataRow2["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
		}
		else
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["IsBarcodeRead"].Value = true;
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
			decimal num4 = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value.ToString());
			if (decimal.Parse(s) > num4)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num4, 3)) : ("Maximum Allowed Qty " + num4));
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num4;
			}
		}
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			DataRow dataRow3 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
			if (decimal.Parse(dataRow3["DiscountPercentage"].ToString()) != 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(dataRow3["DiscountPercentage"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(((Control)(object)txtClientDisc).Text);
				((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtClientDisc).Text) / 100m;
			}
			if (decimal.Parse(dataRow3["DiscountPercentage"].ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(dataRow3["DiscountPercentage"].ToString());
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalcTotalDiscountBeforeTax();
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
		if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		}
		if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && rbIsReservation.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ColorID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ItemSizeID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "BatchID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StockBalance")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ReturnedQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DiscountRatio" && (decimal.Parse(ULGData.ActiveCell.Row.Cells["OfferDiscountRatio"].Value.ToString()) > 0m || (ULGData.ActiveCell.Row.Cells["ItemID"].Value != DBNull.Value && dtItemPrices.Select("ItemID = " + ULGData.ActiveCell.Row.Cells["ItemID"].Value.ToString()).Length != 0 && decimal.Parse(dtItemPrices.Select("ItemID = " + ULGData.ActiveCell.Row.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)))
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
		//IL_1460: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Expected O, but got Unknown
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_1482: Expected O, but got Unknown
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
			if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			}
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + e.Cell.Value)[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + e.Cell.Value.ToString()) : (" ItemID = " + e.Cell.Value.ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (decimal.Parse(array[0]["DiscountPercentage"].ToString()) != 0m)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = Math.Round(decimal.Parse(array[0]["DiscountPercentage"].ToString()), 7);
						((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * Math.Round(decimal.Parse(array[0]["DiscountPercentage"].ToString()), 7) / 100m;
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = decimal.Parse(((Control)(object)txtClientDisc).Text);
						((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtClientDisc).Text) / 100m;
					}
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
				CalcTotalDiscountBeforeTax();
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
			int num2 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + e.Cell.Value)[0]["UnitID"];
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
			if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
			{
				((UltraGridBase)ULGData).UpdateData();
				GetItemStockBlance(e.Cell.Row);
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
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array2[0]["Price"].ToString()) - decimal.Parse(array2[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					}
					else
					{
						UltraGridCell obj4 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
						obj4.Value = value;
					}
					CalculateRow(e.Cell.Row);
					CalcTotalDiscountBeforeTax();
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
		if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && (((KeyedSubObjectBase)e.Cell.Column).Key == "UnitID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ColorID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "StoreID"))
		{
			((UltraGridBase)ULGData).UpdateData();
			GetItemStockBlance(e.Cell.Row);
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
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
			if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
			{
				((UltraGridBase)ULGData).UpdateData();
				GetItemStockBlance(e.Cell.Row);
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if (ULGData.ActiveCell != null && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				try
				{
					UltraGridRow val = ((UltraGridBase)ULGData).Rows[i];
					if (((UltraGridBase)ULGData).ActiveRow.Index != i && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value == DBNull.Value && val.Cells["OfferID"].Value == DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() == val.Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString() == val.Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString() == val.Cells["ItemSizeID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString() == val.Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString() == val.Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString() == val.Cells["UnitID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف  مع نفس المخزن", "Cannot Duplicate The Same Item With  Same Store");
						ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
						((UltraGridBase)ULGData).ActiveRow.Delete(false);
						ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
						((GridItemBase)val).Selected = true;
						val.Activate();
					}
				}
				catch
				{
				}
			}
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyValue == 38 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)19);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else if (e.KeyValue == 40 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)20);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.F6)
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
					if (frmQuantityMultiUnit2.UnitID > 0)
					{
						DataRow dataRow = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmQuantityMultiUnit2.UnitID;
						if (bool.Parse(dataRow["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
						{
							DataRow[] array = dtItemPrices.Select(" UnitID = " + frmQuantityMultiUnit2.UnitID + " and ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
							if (array != null && array.Length != 0)
							{
								((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
							}
							else
							{
								((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = 0;
							}
						}
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
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
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DiscountRatio" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Expected O, but got Unknown
		if (ULGData.ActiveCell == null)
		{
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
		{
			ULGData_CellListSelect(ULGData, e);
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DiscountRatio") && ULGData.ActiveCell.Value == DBNull.Value)
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
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DiscountRatio" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value.ToString()) > 0m)
		{
			decimal grossWithoutItemUnderDiscount = CalculateGrossWithoutItemUnderDiscount();
			if ((cboClient.SelectedIndex > -1 && decimal.Parse(ULGData.ActiveCell.Value.ToString()) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse(ULGData.ActiveCell.Value.ToString()) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse(ULGData.ActiveCell.Value.ToString()) > UserSalesDiscount))
			{
				decimal num = OpenChangeDiscountForm(grossWithoutItemUnderDiscount, decimal.Parse(ULGData.ActiveCell.Value.ToString()), 0m);
				if (num != decimal.Parse(ULGData.ActiveCell.Value.ToString()))
				{
					ULGData.ActiveCell.Value = num;
				}
			}
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
			CalculateGoss();
		}
		if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ULGData.ActiveCell.Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ULGData.ActiveCell.Row.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			decimal num2 = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["StockBalance"].Value.ToString());
			if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > num2)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num2, 3)) : ("Maximum Allowed Qty " + num2));
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
		}
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalcTotalDiscountBeforeTax();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterRowsDeleted -= ULGData_AfterRowsDeleted;
		((UltraGridBase)ULGData).UpdateData();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value && ArOfferIDs.Contains(((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value))
			{
				ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				((UltraGridBase)ULGData).Rows[i].Delete(false);
				ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				i--;
			}
		}
		CalculateGoss();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[j]);
		}
		CalcTotalDiscountBeforeTax();
		CalculateTotalsTax();
		ULGData.AfterRowsDeleted += ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		rowIndex = -1;
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		ArOfferIDs.Clear();
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (e.Rows[i].Cells["OfferID"].Value != DBNull.Value)
			{
				ArOfferIDs.Add(e.Rows[i].Cells["OfferID"].Value);
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
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

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Expected O, but got Unknown
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboClient).Value;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
		((TextEditorControlBase)cboMobile).Value = ((TextEditorControlBase)cboClient).Value;
		((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		if (cboClient.SelectedIndex <= -1)
		{
			return;
		}
		DataRow dataRow = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0];
		if (dataRow["Notes"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show(dataRow["Notes"].ToString());
		}
		string text = "," + ((dataRow["AccountID"] != DBNull.Value) ? string.Concat(dataRow["AccountID"], ",") : "");
		if (dataRow["SupplierAccountID"] != DBNull.Value)
		{
			text = text + dataRow["SupplierAccountID"].ToString() + ",";
		}
		((Control)(object)txtBrabnchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), text, Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtClientDisc).Text = decimal.Parse(dataRow["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			if (!IsDisplayData)
			{
				((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
				((TextEditorControlBase)cboPriceType).Value = dataRow["PriceTypeID"];
				((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount((!IsDisplayData) ? dataRow["PriceTypeID"].ToString() : ((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value && dtAllOffers != null && dtAllOffers.Select(" OfferID= " + ((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value.ToString())[0]["IsPackage"].Equals(true)))
				{
					continue;
				}
				DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
				if (array != null && array.Length != 0)
				{
					if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == array[0]["OfferID"])
					{
						((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value = array[0]["OfferDiscountRatio"];
					}
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (decimal.Parse(array[0]["DiscountPercentage"].ToString()) != 0m)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = Math.Round(decimal.Parse(array[0]["DiscountPercentage"].ToString()), 7);
						((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * Math.Round(decimal.Parse(array[0]["DiscountPercentage"].ToString()), 7) / 100m;
					}
					else
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = decimal.Parse(((Control)(object)txtClientDisc).Text);
						((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtClientDisc).Text) / 100m;
					}
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
					obj.Value = value;
				}
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			cboPriceType.SelectedIndex = -1;
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		}
		if (dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboLine).Value = dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"];
		}
		if (Adding || Updating)
		{
			if (dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"] != DBNull.Value)
			{
				dtSalesMan1 = SubAccounts.SelectSalesMan1ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan1, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesManSearch).Visible = false;
			}
			else
			{
				cboLine.SelectedIndex = -1;
				GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesManSearch).Visible = Adding || Updating;
			}
			if (dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"] != DBNull.Value)
			{
				DataView dataView = new DataView(dtSalesMan);
				dataView.RowFilter = " SubAccountID=" + dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"].ToString();
				GlobalFunctions.FillCombo(cboSalesMan2, dataView.ToTable(), "SubAccountID", "SubAccountName");
				((TextEditorControlBase)cboSalesMan2).Value = dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["EmployeeID"].ToString();
				((Control)(object)btnSalesMan2Search).Visible = false;
			}
			else if (dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"] != DBNull.Value)
			{
				dtSalesMan2 = SubAccounts.SelectSalesMan2ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan2, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = false;
			}
			else
			{
				GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = Adding || Updating;
			}
		}
	}

	private void cboClient_Leave(object sender, EventArgs e)
	{
		InputLanguage.CurrentInputLanguage = Language;
	}

	private void cboClient_Enter(object sender, EventArgs e)
	{
		Language = InputLanguage.CurrentInputLanguage;
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ar-Eg"));
	}

	private void cboClientCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboClientCode.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboClientCode).Value;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = ((TextEditorControlBase)cboClientCode).Value;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			cboMobile.SelectedIndex = -1;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		}
	}

	private void cboMobile_ValueChanged(object sender, EventArgs e)
	{
		if (cboMobile.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboMobile).Value;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboMobile).Value;
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			cboClientCode.SelectedIndex = -1;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		}
	}

	private void cboSalesMan2_ValueChanged(object sender, EventArgs e)
	{
		if (cboSalesMan2.SelectedIndex <= -1)
		{
			return;
		}
		DataTable dataTable = (DataTable)cboSalesMan2.DataSource;
		DataRow dataRow = dataTable.Select("SubAccountID = " + ((TextEditorControlBase)cboSalesMan2).Value.ToString())[0];
		if (dataRow["SalesManDefaultStoreID"] == DBNull.Value || dtStores.Select(" Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID + " and StoreID = " + dataRow["SalesManDefaultStoreID"].ToString()).Length == 0)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value.ToString() == "-1")
			{
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = dataRow["SalesManDefaultStoreID"];
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = dataRow["SalesManDefaultStoreID"];
	}

	private void chkApplyTax_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = DBNull.Value;
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void txtClientDisc_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((Control)(object)txtClientDisc).Text != null)
		{
			decimal discountRatio = decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text);
			decimal grossWithoutItemUnderDiscount = CalculateGrossWithoutItemUnderDiscount();
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text) > UserSalesDiscount))
			{
				((Control)(object)txtClientDisc).Text = OpenChangeDiscountForm(grossWithoutItemUnderDiscount, discountRatio, 0m).ToString(GlobalVariables.txtDecimalFormate);
			}
			discountRatio = decimal.Parse((((Control)(object)txtClientDisc).Text == "" || ((Control)(object)txtClientDisc).Text == ".") ? "0" : ((Control)(object)txtClientDisc).Text);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != null)
				{
					if (dtItemPrices == null || dtItemPrices.Rows.Count == 0)
					{
						if (cboPriceType.SelectedIndex > -1)
						{
							dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
						}
						else if (cboClient.SelectedIndex > -1)
						{
							dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
						}
					}
					if (dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()).Length != 0)
					{
						DataRow dataRow = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
						if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) == 0m && decimal.Parse(dataRow["DiscountPercentage"].ToString()) == 0m)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = discountRatio;
						}
					}
					else
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = discountRatio;
					}
				}
				else
				{
					((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = discountRatio;
				}
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].DefaultCellValue = discountRatio;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		CalcTotalDiscountBeforeTax();
	}

	private void btnStoreTransfer_Click(object sender, EventArgs e)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		int num = SearchFunctions.StoreTransferVouchersLnsSearch(FromServer: false);
		if (num == 0)
		{
			return;
		}
		((UltraGridBase)ULGData).DataSource = InvoicesDetails.SelectByStoreTransferVoucherID(num.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			string text = "";
			if (Adding || Updating)
			{
				text = " IsActive = 1 and ";
			}
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = text + (((UltraToggleEditorBase)chkBranches).Checked ? ("BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString()) : ("( ForAllBranches=1 or BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + ")"));
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else if (Adding || Updating)
		{
			DataView dataView2 = new DataView(dtClients);
			dataView2.RowFilter = "IsActive = 1";
			DataTable dt = dataView2.ToTable();
			GlobalFunctions.FillCombo(cboClient, dt, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dt, "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			string text = "";
			if (Adding || Updating)
			{
				text = " IsActive = 1 and ";
			}
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = text + (((UltraToggleEditorBase)chkBranches).Checked ? ("BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString()) : ("( ForAllBranches=1 or BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + ")"));
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else if (Adding || Updating)
		{
			DataView dataView2 = new DataView(dtClients);
			dataView2.RowFilter = "IsActive = 1";
			DataTable dt = dataView2.ToTable();
			GlobalFunctions.FillCombo(cboClient, dt, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dt, "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
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

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == DBNull.Value || dtAllOffers == null || !dtAllOffers.Select(" OfferID= " + ((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value.ToString())[0]["IsPackage"].Equals(true))
				{
					DataRow dataRow = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
					if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == dataRow["OfferID"])
					{
						((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value = dataRow["OfferDiscountRatio"];
					}
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (decimal.Parse(dataRow["DiscountPercentage"].ToString()) != 0m)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = Math.Round(decimal.Parse(dataRow["DiscountPercentage"].ToString()), 7);
						((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(dataRow["DiscountPercentage"].ToString()) / 100m;
					}
					else
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value = decimal.Parse(((Control)(object)txtClientDisc).Text);
						((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtClientDisc).Text) / 100m;
					}
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
			}
			CalculateGoss();
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
	}

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	public decimal OpenChangeDiscountForm(decimal GrossWithoutItemUnderDiscount, decimal discountRatio, decimal discountValue)
	{
		decimal num = discountRatio;
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(GrossWithoutItemUnderDiscount, discountValue, discountRatio);
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			return decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString());
		}
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
		return frmChangeDiscount2.DiscountRatio;
	}

	private void txtAccountAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtCashAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtVisaAmount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Expected O, but got Unknown
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
		else
		{
			if (rowIndex <= -1 || ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= rowIndex)
			{
				return;
			}
			frmEnterQuantity frmEnterQuantity2 = new frmEnterQuantity(((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Text, ((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			frmEnterQuantity2.WindowState = FormWindowState.Normal;
			if (frmEnterQuantity2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) + decimal.Parse(frmEnterQuantity2.Value);
			if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()) && ((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				decimal num = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["StockBalance"].Value.ToString());
				if (decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) > num)
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num, 3)) : ("Maximum Allowed Qty " + num));
					((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = num;
				}
			}
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).Rows[rowIndex]);
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void GetItemStockBlance(UltraGridRow Row)
	{
		if (Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + Row.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()) && Row.Cells["UnitID"].Value != DBNull.Value && Row.Cells["StoreID"].Value != DBNull.Value && (!UsingColors || (UsingColors && Row.Cells["ColorID"].Value != DBNull.Value)) && (!UsingSizes || (UsingSizes && Row.Cells["ItemSizeID"].Value != DBNull.Value)) && (!UsingBatchNoAndValidityPeriod || !bool.Parse(dtItems.Select("ItemID =" + Row.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) || (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + Row.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && Row.Cells["BatchID"].Value != DBNull.Value)))
		{
			string colorID = (UsingColors ? Row.Cells["ColorID"].Value.ToString() : "1");
			string itemSizeID = (UsingSizes ? Row.Cells["ItemSizeID"].Value.ToString() : "1");
			string batchIDs = ((!UsingBatchNoAndValidityPeriod || !bool.Parse(dtItems.Select("ItemID =" + Row.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString())) ? "-1" : ("," + Row.Cells["BatchID"].Value.ToString() + ","));
			DataTable balanceByColorAndSize = Items.GetBalanceByColorAndSize(Row.Cells["ItemID"].Value.ToString(), Row.Cells["StoreID"].Value.ToString(), batchIDs, colorID, itemSizeID, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
			Row.Cells["StockBalance"].Value = ((balanceByColorAndSize.Rows.Count > 0) ? balanceByColorAndSize.Rows[0]["Qty"].ToString() : "0");
		}
		else
		{
			Row.Cells["StockBalance"].Value = "0";
		}
		decimal num = decimal.Parse(Row.Cells["StockBalance"].Value.ToString());
		if (Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + Row.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()) && decimal.Parse(Row.Cells["Qty"].Value.ToString()) > num)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية  " + Math.Round(num, 3)) : ("Maximum Allowed Qty " + num));
			Row.Cells["Qty"].Value = num;
			Row.Cells["TotalPrice"].Value = decimal.Parse(Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(Row.Cells["Qty"].Value.ToString());
			CalculateGoss();
			CalculateRow(Row);
			CalculateTotalsTax();
		}
	}

	private void txtSerial_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Expected O, but got Unknown
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
				CalcTotalDiscountBeforeTax();
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
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + dataRow["ItemID"].ToString()) : (" ItemID = " + dataRow["ItemID"].ToString()));
			if (array != null && array.Length != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else
			{
				UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
				obj3.Value = value;
			}
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
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

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void textBox_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		((Control)(UltraTextEditor)sender).Select();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
				if (array != null && array.Length != 0)
				{
					if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == array[0]["OfferID"])
					{
						((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value = array[0]["OfferDiscountRatio"];
					}
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
					obj.Value = value;
				}
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalcTotalDiscountBeforeTax();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
		if (Adding)
		{
			((Control)(object)txtCode).Text = Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnSalesManSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan).Value = num;
		}
	}

	private void btnSalesMan2Search_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan2).Value = num;
		}
	}

	private void txtClientCardNo_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		if (cboClient.SelectedIndex > -1)
		{
			if (e.KeyCode != Keys.Return || !(((Control)(object)txtClientCardNo).Text != ""))
			{
				return;
			}
			DataTable discountRatio = Invoices.GetDiscountRatio(((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtClientCardNo).Text);
			if (discountRatio.Rows.Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((TextEditorControlBase)txtClientDisc).ValueChanged -= txtClientDisc_ValueChanged;
				((Control)(object)txtClientDisc).Text = decimal.Parse(discountRatio.Rows[0]["Percentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				((TextEditorControlBase)txtClientCardNo).Clear();
				((TextEditorControlBase)txtClientDisc).ValueChanged += txtClientDisc_ValueChanged;
			}
			else
			{
				GlobalVariables.InformationMB.Show("لا يوجد خصم لهذا العميل", "There is no Discount For this SubAccount");
				((TextEditorControlBase)txtClientCardNo).Clear();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)txtClientCardNo).Clear();
		}
	}

	private void CheckForCashBackOffer()
	{
		if (CashBackOfferID != 0 || ((DataTable)((UltraGridBase)ULGData).DataSource).Select(" OfferID Is Not Null  And OfferDiscountRatio = 0 And DiscountInvoice <> 0").Length != 0)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).Rows[i].Cells["DiscountInvoice"].Value = 0;
			}
			((Control)(object)txtDiscInvValue).Text = "0";
			((Control)(object)txtDiscInvRatio).Text = "0";
		}
	}

	private void btnOffers_Click(object sender, EventArgs e)
	{
		//IL_2060: Unknown result type (might be due to invalid IL or missing references)
		//IL_206a: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_2549: Unknown result type (might be due to invalid IL or missing references)
		//IL_2553: Expected O, but got Unknown
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Expected O, but got Unknown
		//IL_2677: Unknown result type (might be due to invalid IL or missing references)
		//IL_2681: Expected O, but got Unknown
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Expected O, but got Unknown
		//IL_352a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3534: Expected O, but got Unknown
		//IL_14dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e7: Expected O, but got Unknown
		//IL_33fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3404: Expected O, but got Unknown
		//IL_13fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1405: Expected O, but got Unknown
		//IL_4010: Unknown result type (might be due to invalid IL or missing references)
		//IL_401a: Expected O, but got Unknown
		//IL_1fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbe: Expected O, but got Unknown
		dtOffers = Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtOffers.Rows.Count > 1)
		{
			frmSelectOffers frmSelectOffers2 = new frmSelectOffers(dtOffers);
			frmSelectOffers2.Location = new Point(0, 0);
			frmSelectOffers2.ShowDialog();
			if (frmSelectOffers2.OfferID != 0 && !frmSelectOffers2.Cancel && dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsPackage"].Equals(true))
			{
				int offerID = frmSelectOffers2.OfferID;
				DataTable dataTable = OffersPackages.SelectByOfferID(offerID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CheckForCashBackOffer();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataTable.Rows[i]["ItemID"]);
					obj.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + dataTable.Rows[i]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable.Rows[i]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataTable.Rows[i]["UnitID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataTable.Rows[i]["Price"];
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			else if (frmSelectOffers2.OfferID != 0 && !frmSelectOffers2.Cancel && dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsPackage"].Equals(false) && dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsQtyDiscount"].Equals(false))
			{
				if (dtItemPrices == null || dtItemPrices.Rows.Count <= 0 || cboClient.SelectedIndex <= -1)
				{
					return;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CheckForCashBackOffer();
				for (int j = 0; j < frmSelectOffers2.dtOffersItems.Rows.Count; j++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["ItemID"]);
					obj3.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + frmSelectOffers2.dtOffersItems.Rows[j]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmSelectOffers2.dtOffersItems.Rows[j]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = 0;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				for (int k = 0; k < frmSelectOffers2.dtOffersGifts.Rows.Count; k++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["ItemID"]);
					obj5.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + frmSelectOffers2.dtOffersGifts.Rows[k]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmSelectOffers2.dtOffersGifts.Rows[k]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			else
			{
				if (frmSelectOffers2.OfferID == 0 || frmSelectOffers2.Cancel || !dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsPackage"].Equals(false) || !dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsQtyDiscount"].Equals(true) || dtItemPrices == null || dtItemPrices.Rows.Count <= 0 || cboClient.SelectedIndex <= -1)
				{
					return;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("ItemID", typeof(int));
				dataTable2.Columns.Add("UnitID", typeof(int));
				dataTable2.Columns.Add("BatchID", typeof(int));
				dataTable2.Columns.Add("ColorID", typeof(int));
				dataTable2.Columns.Add("ItemSizeID", typeof(int));
				dataTable2.Columns.Add("Qty", typeof(decimal));
				dataTable2.Columns.Add("OfferID", typeof(int));
				dataTable2.Columns.Add("Price", typeof(decimal));
				for (int l = 0; l < frmSelectOffers2.dtOffersItems.Rows.Count; l++)
				{
					dataTable2.Rows.Add(frmSelectOffers2.dtOffersItems.Rows[l]["ItemID"], frmSelectOffers2.dtOffersItems.Rows[l]["UnitID"], frmSelectOffers2.dtOffersItems.Rows[l]["BatchID"], frmSelectOffers2.dtOffersItems.Rows[l]["ColorID"], frmSelectOffers2.dtOffersItems.Rows[l]["ItemSizeID"], frmSelectOffers2.dtOffersItems.Rows[l]["Qty"], frmSelectOffers2.dtOffersItems.Rows[l]["OfferID"], dtItemPrices.Select(" ItemID= " + frmSelectOffers2.dtOffersItems.Rows[l]["ItemID"].ToString())[0]["Price"]);
				}
				DataView dataView = new DataView(dataTable2);
				if (frmSelectOffers2.LowestPrice)
				{
					dataView.Sort = "Price ASC";
				}
				else if (frmSelectOffers2.HighestPrice)
				{
					dataView.Sort = "Price Desc";
				}
				CheckForCashBackOffer();
				decimal num = default(decimal);
				for (int m = 0; m < dataView.Count; m++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj7 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataView[m]["ItemID"]);
					obj7.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + dataView[m]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataView[m]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataView[m]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataView[m]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataView[m]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dataView[m]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
					if (frmSelectOffers2.ForAll)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
					}
					else if (frmSelectOffers2.HighestPrice && num < frmSelectOffers2.QtyDiscount && dataView.Count > 0)
					{
						if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[m]["ItemID"].ToString()))
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
							++num;
						}
					}
					else if (frmSelectOffers2.LowestPrice && num < frmSelectOffers2.QtyDiscount && dataView.Count > 0 && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[m]["ItemID"].ToString()))
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
						++num;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString();
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		else
		{
			if (dtOffers.Rows.Count != 1)
			{
				return;
			}
			if (dtOffers.Rows[0]["IsPackage"].Equals(true))
			{
				string text = dtOffers.Rows[0]["OfferID"].ToString();
				DataTable dataTable3 = OffersPackages.SelectByOfferID(text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CheckForCashBackOffer();
				for (int n = 0; n < dataTable3.Rows.Count; n++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj9 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataTable3.Rows[n]["ItemID"]);
					obj9.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + dataTable3.Rows[n]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable3.Rows[n]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataTable3.Rows[n]["UnitID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = text;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataTable3.Rows[n]["Price"];
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			else if (dtOffers.Rows[0]["IsQtyDiscount"].Equals(false))
			{
				frmLnsInvoicesGiftsoffers frmLnsInvoicesGiftsoffers2 = new frmLnsInvoicesGiftsoffers(int.Parse(dtOffers.Rows[0]["OfferID"].ToString()));
				frmLnsInvoicesGiftsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				frmLnsInvoicesGiftsoffers2.Location = new Point(0, 0);
				((Control)(object)frmLnsInvoicesGiftsoffers2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار عرض" : "Select Offer");
				frmLnsInvoicesGiftsoffers2.ShowDialog();
				if (frmLnsInvoicesGiftsoffers2.Cancel || dtItemPrices == null || dtItemPrices.Rows.Count <= 0 || cboClient.SelectedIndex <= -1)
				{
					return;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CheckForCashBackOffer();
				for (int num2 = 0; num2 < frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows.Count; num2++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["ItemID"]);
					obj11.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num2]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesGiftsoffers2.OfferID;
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = 0;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				for (int num3 = 0; num3 < frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows.Count; num3++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj13 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["ItemID"]);
					obj13.Value = value;
					UltraGridCell obj15 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[num3]["ItemID"]);
					obj15.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[num3]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesGiftsoffers2.OfferID;
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesGiftsoffers2.DiscountRatio;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			else
			{
				if (!dtOffers.Rows[0]["IsQtyDiscount"].Equals(true))
				{
					return;
				}
				frmLnsInvoicesQtyDiscountsoffers frmLnsInvoicesQtyDiscountsoffers2 = new frmLnsInvoicesQtyDiscountsoffers(int.Parse(dtOffers.Rows[0]["OfferID"].ToString()));
				frmLnsInvoicesQtyDiscountsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				frmLnsInvoicesQtyDiscountsoffers2.Location = new Point(0, 0);
				((Control)(object)frmLnsInvoicesQtyDiscountsoffers2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار عرض" : "Select Offer");
				frmLnsInvoicesQtyDiscountsoffers2.ShowDialog();
				if (frmLnsInvoicesQtyDiscountsoffers2.Cancel || dtItemPrices == null || dtItemPrices.Rows.Count <= 0 || cboClient.SelectedIndex <= -1)
				{
					return;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				CheckForCashBackOffer();
				DataTable dataTable4 = new DataTable();
				dataTable4.Columns.Add("ItemID", typeof(int));
				dataTable4.Columns.Add("UnitID", typeof(int));
				dataTable4.Columns.Add("BatchID", typeof(int));
				dataTable4.Columns.Add("ColorID", typeof(int));
				dataTable4.Columns.Add("ItemSizeID", typeof(int));
				dataTable4.Columns.Add("Qty", typeof(decimal));
				dataTable4.Columns.Add("OfferID", typeof(int));
				dataTable4.Columns.Add("Price", typeof(decimal));
				for (int num4 = 0; num4 < frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows.Count; num4++)
				{
					dataTable4.Rows.Add(frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["ItemID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["UnitID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["BatchID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["ColorID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["ItemSizeID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["Qty"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["OfferID"], dtItemPrices.Select(" ItemID= " + frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num4]["ItemID"].ToString())[0]["Price"]);
				}
				DataView dataView2 = new DataView(dataTable4);
				if (frmLnsInvoicesQtyDiscountsoffers2.LowestPrice)
				{
					dataView2.Sort = "Price ASC";
				}
				else if (frmLnsInvoicesQtyDiscountsoffers2.HighestPrice)
				{
					dataView2.Sort = "Price Desc";
				}
				decimal num5 = default(decimal);
				for (int num6 = 0; num6 < dataView2.Count; num6++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj17 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataView2[num6]["ItemID"]);
					obj17.Value = value;
					if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + dataView2[num6]["ItemID"].ToString())[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
					}
					else
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataView2[num6]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataView2[num6]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataView2[num6]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataView2[num6]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dataView2[num6]["BatchID"];
					if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == DBNull.Value || ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue == null)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesQtyDiscountsoffers2.OfferID;
					if (frmLnsInvoicesQtyDiscountsoffers2.ForAll)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
					}
					else if (frmLnsInvoicesQtyDiscountsoffers2.HighestPrice && num5 < frmLnsInvoicesQtyDiscountsoffers2.QtyDiscount && dataView2.Count > 0)
					{
						if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView2[num6]["ItemID"].ToString()))
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
							++num5;
						}
					}
					else if (frmLnsInvoicesQtyDiscountsoffers2.LowestPrice && num5 < frmLnsInvoicesQtyDiscountsoffers2.QtyDiscount && dataView2.Count > 0 && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView2[num6]["ItemID"].ToString()))
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
						++num5;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString());
					((UltraGridBase)ULGData).ActiveRow.Cells["Discount"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["DiscountRatio"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value;
					if (bool.Parse(dtPOSDefaultData.Rows[0]["GetItemBalanceAutomatically"].ToString()))
					{
						GetItemStockBlance(((UltraGridBase)ULGData).ActiveRow);
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
				CalcTotalDiscountBeforeTax();
				CalculateTotalsTax();
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void RadioButton_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)btnOffers).Visible = rbIsDirect.Checked;
		((Control)(object)cboReservationNo).Visible = !rbIsDirect.Checked;
		((Control)(object)lblReservationNo).Visible = !rbIsDirect.Checked;
		((Control)(object)btnReservationSearch).Visible = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtCashAmount).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtAccountAmount).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtVisaAmount).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)cboVisaType).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtVisaNo).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)txtRestAmountPaid).ReadOnly = !rbIsDirect.Checked;
		((EditorButtonControlBase)cboClient).ReadOnly = !Adding || !rbIsDirect.Checked;
		((EditorButtonControlBase)cboMobile).ReadOnly = !Adding || !rbIsDirect.Checked;
		((EditorButtonControlBase)cboClientCode).ReadOnly = !Adding || !rbIsDirect.Checked;
		((EditorButtonControlBase)cboPriceType).ReadOnly = !Adding || !rbIsDirect.Checked;
		((Control)(object)btnClientSearch).Enabled = Adding && rbIsDirect.Checked;
		((Control)(object)btnPriceTypeSearch).Enabled = Adding && rbIsDirect.Checked;
		if (rbIsReservation.Checked && Adding)
		{
			((TextEditorControlBase)cboReservationNo).ValueChanged -= cboReservationNo_ValueChanged;
			dtReservations = Reservations.FillCombo("0", GlobalVariables.BranchIDs);
			DataView dataView = new DataView(dtReservations);
			dataView.RowFilter = " IsDeliverd = 0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboReservationNo, dataView.ToTable(), "ReservationID", "ReservationNo");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboReservationNo.SelectedIndex = -1;
			((TextEditorControlBase)cboReservationNo).ValueChanged += cboReservationNo_ValueChanged;
		}
	}

	private void btnReservationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Reservations("-1", "0", "0", "-1", "0");
		if (num != 0)
		{
			((TextEditorControlBase)cboReservationNo).Value = num;
		}
	}

	private void cboReservationNo_ValueChanged(object sender, EventArgs e)
	{
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Expected O, but got Unknown
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		if (!rbIsReservation.Checked || cboReservationNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboReservationNo).ValueChanged -= cboReservationNo_ValueChanged;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		UltraComboEditor obj = cboClient;
		UltraComboEditor obj2 = cboClientCode;
		object obj3 = (((TextEditorControlBase)cboMobile).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["SubAccountID"]);
		object value = (((TextEditorControlBase)obj2).Value = obj3);
		((TextEditorControlBase)obj).Value = value;
		((TextEditorControlBase)cboPriceType).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["PriceTypeID"];
		DataRow dataRow = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0];
		string text = "," + ((dataRow["AccountID"] != DBNull.Value) ? string.Concat(dataRow["AccountID"], ",") : "");
		if (dataRow["SupplierAccountID"] != DBNull.Value)
		{
			text = text + dataRow["SupplierAccountID"].ToString() + ",";
		}
		((Control)(object)txtBrabnchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), text, Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), GlobalVariables.LocalCurrencyID.ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dataRow["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		dtReservationDetails = InvoicesDetails.FillByReservationID(((TextEditorControlBase)cboReservationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		dtDetails.Rows.Clear();
		((UltraGridBase)ULGData).DataSource = dtReservationDetails;
		InitGrid();
		CalcQuantity();
		((TextEditorControlBase)txtCashAmount).Value = 0;
		((TextEditorControlBase)txtVisaAmount).Value = 0;
		((TextEditorControlBase)txtGrossValue).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["GrossValue"];
		((TextEditorControlBase)txtDiscBeforeTaxValue).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["DiscountBeforeTaxValue"];
		((TextEditorControlBase)txtDiscBeforeTaxRatio).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["DiscountBeforeTaxRatio"];
		((TextEditorControlBase)txtTaxTotalValue).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["TaxTotalValue"];
		((TextEditorControlBase)txtNetprice).Value = dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["NetPrice"];
		((TextEditorControlBase)txtRestAmountPaid).Value = 0;
		((TextEditorControlBase)txtAccountAmount).Value = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			DataRow dataRow2 = dtItems.Select(" ItemID= " + dtReservationDetails.Rows[i]["ItemID"].ToString())[0];
			((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dtUnits.Select(" UnitID =" + dtReservationDetails.Rows[i]["UnitID"].ToString())[0]["UnitTypeID"].ToString()));
			if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
			}
			if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		((TextEditorControlBase)cboReservationNo).ValueChanged += cboReservationNo_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
	}

	private void cboReservationNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Reservations("," + GlobalVariables.CurrentBranchID + ",", "0", "0", "0", "0");
			if (num != 0)
			{
				((TextEditorControlBase)cboReservationNo).Value = num;
			}
		}
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.dtForms.Select("IsFullName = 1 and FormFullName = 'ERP.Sales.MasterData.frmClientsTree'").Length != 0 && GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("IsFullName = 1 and FormFullName = 'ERP.Sales.MasterData.frmClientsTree'")[0]["FormID"].ToString(), "Adding"))
		{
			frmClientsTree frmClientsTree2 = new frmClientsTree(_AddFromAnotherForm: true);
			frmClientsTree2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmClientsTree2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Clients");
			frmClientsTree2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.Sales.MasterData.frmClientsTree'")[0];
			frmClientsTree2.ShowDialog();
			if (!(frmClientsTree2.SubAccountID != 0m))
			{
				return;
			}
			GlobalFunctions.SyncMasterData("frmSubAccountsTree");
			dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (cboBranches.SelectedIndex > -1)
			{
				string text = "";
				if (Adding || Updating)
				{
					text = " IsActive = 1 and ";
				}
				DataView dataView = new DataView(dtClients);
				dataView.RowFilter = text + (((UltraToggleEditorBase)chkBranches).Checked ? ("BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString()) : ("( ForAllBranches=1 or BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + ")"));
				GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
			}
			else if (Adding || Updating)
			{
				DataView dataView2 = new DataView(dtClients);
				dataView2.RowFilter = "IsActive = 1";
				DataTable dt = dataView2.ToTable();
				GlobalFunctions.FillCombo(cboClient, dt, "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dt, "SubAccountID", "ClientSupplierNo");
			}
			else
			{
				GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
			}
			((TextEditorControlBase)cboClient).Value = frmClientsTree2.SubAccountID;
		}
		else
		{
			GlobalVariables.InformationMB.Show("لا توجد صلاحية لهذا المستخدم فى إضافة عميل جديد", "User Cannot Add New Client");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is  Null ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is  Null ", "1");
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

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		UltraTextEditor obj = txtCashAmount;
		UltraTextEditor obj2 = txtVisaAmount;
		string text = (((Control)(object)txtAccountAmount).Text = "0");
		string text3 = (((Control)(object)obj2).Text = text);
		((Control)(object)obj).Text = text3;
		cboVisaType.SelectedIndex = -1;
		((Control)(object)txtVisaNo).Text = "";
		drMaster = null;
		SetControls(NavMode: false);
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
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
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
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Expected O, but got Unknown
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsInvoicesWithPayment3));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsDirect = new System.Windows.Forms.RadioButton();
		this.rbIsReservation = new System.Windows.Forms.RadioButton();
		this.lblCashAmount = new UltraLabel();
		this.txtCashAmount = new UltraTextEditor();
		this.txtAccountAmount = new UltraTextEditor();
		this.lblAccountAmount = new UltraLabel();
		this.btnPriceTypeSearch = new UltraButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.btnClientSearch = new UltraButton();
		this.txtBarCode = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.cboTax = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.chkTax = new UltraCheckEditor();
		this.btnClientBalance = new UltraButton();
		this.cboClientCode = new UltraComboEditor();
		this.lblBalance = new UltraLabel();
		this.txtBrabnchBalance = new UltraTextEditor();
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblShiftDate = new UltraLabel();
		this.dtpShiftDate = new UltraDateTimeEditor();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMan = new UltraComboEditor();
		this.btnSalesManSearch = new UltraButton();
		this.txtClientCardNo = new UltraTextEditor();
		this.lblClientCard = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.lblVisaType = new UltraLabel();
		this.cboVisaType = new UltraComboEditor();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.txtVisaAmount = new UltraTextEditor();
		this.lblVisaAmount = new UltraLabel();
		this.txtRestAmountPaid = new UltraTextEditor();
		this.lblRestAmountPaid = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.cboMobile = new UltraComboEditor();
		this.cboSalesMan2 = new UltraComboEditor();
		this.lblSalesMan2 = new UltraLabel();
		this.btnSalesMan2Search = new UltraButton();
		this.btnOffers = new UltraButton();
		this.btnReservationSearch = new UltraButton();
		this.lblReservationNo = new UltraLabel();
		this.cboReservationNo = new UltraComboEditor();
		this.btnClientAdd = new UltraButton();
		this.btnStoreTransfer = new UltraButton();
		this.lblSerial = new UltraLabel();
		this.txtSerial = new UltraTextEditor();
		this.lblDiscInvRatio = new UltraLabel();
		this.txtDiscInvRatio = new UltraTextEditor();
		this.lblDiscInvValue = new UltraLabel();
		this.txtDiscInvValue = new UltraTextEditor();
		this.txtClientDisc = new UltraTextEditor();
		this.lblClientDisc = new UltraLabel();
		this.chkApplyTax = new UltraCheckEditor();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.btnLineSearch = new UltraButton();
		this.lblLine = new UltraLabel();
		this.cboLine = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccountAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientCardNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmountPaid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReservationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscInvRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscInvValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientDisc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)base.txtCode).TabStop = false;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
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
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance9");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirect);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsReservation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsDirect, "rbIsDirect");
		this.rbIsDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirect.Checked = true;
		this.rbIsDirect.Name = "rbIsDirect";
		this.rbIsDirect.TabStop = true;
		this.rbIsDirect.UseVisualStyleBackColor = false;
		this.rbIsDirect.CheckedChanged += new System.EventHandler(RadioButton_CheckedChanged);
		resources.ApplyResources(this.rbIsReservation, "rbIsReservation");
		this.rbIsReservation.BackColor = System.Drawing.Color.Transparent;
		this.rbIsReservation.Name = "rbIsReservation";
		this.rbIsReservation.UseVisualStyleBackColor = false;
		this.rbIsReservation.CheckedChanged += new System.EventHandler(RadioButton_CheckedChanged);
		resources.ApplyResources(this.lblCashAmount, "lblCashAmount");
		this.lblCashAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCashAmount).Name = "lblCashAmount";
		((ControlBase)this.lblCashAmount).WrapText = false;
		resources.ApplyResources(this.txtCashAmount, "txtCashAmount");
		((System.Windows.Forms.Control)(object)this.txtCashAmount).Name = "txtCashAmount";
		((TextEditorControlBase)this.txtCashAmount).ValueChanged += new System.EventHandler(txtCashAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCashAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtCashAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtAccountAmount, "txtAccountAmount");
		((System.Windows.Forms.Control)(object)this.txtAccountAmount).Name = "txtAccountAmount";
		((System.Windows.Forms.Control)(object)this.txtAccountAmount).TabStop = false;
		((TextEditorControlBase)this.txtAccountAmount).ValueChanged += new System.EventHandler(txtAccountAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAccountAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblAccountAmount, "lblAccountAmount");
		this.lblAccountAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountAmount).Name = "lblAccountAmount";
		((ControlBase)this.lblAccountAmount).WrapText = false;
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((System.Windows.Forms.Control)(object)this.cboPriceType).TabStop = false;
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
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
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).Enter += new System.EventHandler(cboClient_Enter);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		((System.Windows.Forms.Control)(object)this.cboClient).Leave += new System.EventHandler(cboClient_Leave);
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		resources.ApplyResources(this.btnClientBalance, "btnClientBalance");
		((AppearanceBase)val12).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val12).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val12).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val12).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val12).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnClientBalance).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Name = "btnClientBalance";
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Click += new System.EventHandler(btnClientBalance_Click);
		resources.ApplyResources(this.cboClientCode, "cboClientCode");
		((TextEditorControlBase)this.cboClientCode).AlwaysInEditMode = true;
		this.cboClientCode.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClientCode).Name = "cboClientCode";
		((TextEditorControlBase)this.cboClientCode).ValueChanged += new System.EventHandler(cboClientCode_ValueChanged);
		resources.ApplyResources(this.lblBalance, "lblBalance");
		this.lblBalance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		resources.ApplyResources(this.txtBrabnchBalance, "txtBrabnchBalance");
		((System.Windows.Forms.Control)(object)this.txtBrabnchBalance).Name = "txtBrabnchBalance";
		((EditorButtonControlBase)this.txtBrabnchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBrabnchBalance).TabStop = false;
		resources.ApplyResources(this.lblShiftNo, "lblShiftNo");
		this.lblShiftNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShiftNo).Name = "lblShiftNo";
		((ControlBase)this.lblShiftNo).WrapText = false;
		resources.ApplyResources(this.txtShiftNo, "txtShiftNo");
		((System.Windows.Forms.Control)(object)this.txtShiftNo).Name = "txtShiftNo";
		((EditorButtonControlBase)this.txtShiftNo).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtShiftNo).TabStop = false;
		resources.ApplyResources(this.lblShiftDate, "lblShiftDate");
		this.lblShiftDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShiftDate).Name = "lblShiftDate";
		((ControlBase)this.lblShiftDate).WrapText = false;
		resources.ApplyResources(this.dtpShiftDate, "dtpShiftDate");
		((UltraWinEditorMaskedControlBase)this.dtpShiftDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).Name = "dtpShiftDate";
		((EditorButtonControlBase)this.dtpShiftDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).TabStop = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		this.lblSalesMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((System.Windows.Forms.Control)(object)this.cboSalesMan).TabStop = false;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		resources.ApplyResources(this.txtClientCardNo, "txtClientCardNo");
		((System.Windows.Forms.Control)(object)this.txtClientCardNo).Name = "txtClientCardNo";
		((System.Windows.Forms.Control)(object)this.txtClientCardNo).KeyUp += new System.Windows.Forms.KeyEventHandler(txtClientCardNo_KeyUp);
		resources.ApplyResources(this.lblClientCard, "lblClientCard");
		this.lblClientCard.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientCard).Name = "lblClientCard";
		((ControlBase)this.lblClientCard).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtNetprice).TabStop = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtGrossValue).TabStop = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((EditorButtonControlBase)this.txtDiscBeforeTaxValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((EditorButtonControlBase)this.txtDiscBeforeTaxRatio).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.lblVisaType, "lblVisaType");
		this.lblVisaType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaType).Name = "lblVisaType";
		((ControlBase)this.lblVisaType).WrapText = false;
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((TextEditorControlBase)this.cboVisaType).AlwaysInEditMode = true;
		this.cboVisaType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		((System.Windows.Forms.Control)(object)this.cboVisaType).TabStop = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.txtVisaAmount, "txtVisaAmount");
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).Name = "txtVisaAmount";
		((TextEditorControlBase)this.txtVisaAmount).ValueChanged += new System.EventHandler(txtVisaAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblVisaAmount, "lblVisaAmount");
		this.lblVisaAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaAmount).Name = "lblVisaAmount";
		((ControlBase)this.lblVisaAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmountPaid, "txtRestAmountPaid");
		((System.Windows.Forms.Control)(object)this.txtRestAmountPaid).Name = "txtRestAmountPaid";
		((EditorButtonControlBase)this.txtRestAmountPaid).ReadOnly = true;
		resources.ApplyResources(this.lblRestAmountPaid, "lblRestAmountPaid");
		this.lblRestAmountPaid.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmountPaid).Name = "lblRestAmountPaid";
		((ControlBase)this.lblRestAmountPaid).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboMobile, "cboMobile");
		((TextEditorControlBase)this.cboMobile).AlwaysInEditMode = true;
		this.cboMobile.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMobile).Name = "cboMobile";
		((TextEditorControlBase)this.cboMobile).ValueChanged += new System.EventHandler(cboMobile_ValueChanged);
		resources.ApplyResources(this.cboSalesMan2, "cboSalesMan2");
		((TextEditorControlBase)this.cboSalesMan2).AlwaysInEditMode = true;
		this.cboSalesMan2.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).Name = "cboSalesMan2";
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).TabStop = false;
		((TextEditorControlBase)this.cboSalesMan2).ValueChanged += new System.EventHandler(cboSalesMan2_ValueChanged);
		resources.ApplyResources(this.lblSalesMan2, "lblSalesMan2");
		this.lblSalesMan2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan2).Name = "lblSalesMan2";
		((ControlBase)this.lblSalesMan2).WrapText = false;
		resources.ApplyResources(this.btnSalesMan2Search, "btnSalesMan2Search");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.btnSalesMan2Search).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Name = "btnSalesMan2Search";
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Click += new System.EventHandler(btnSalesMan2Search_Click);
		resources.ApplyResources(this.btnOffers, "btnOffers");
		((System.Windows.Forms.Control)(object)this.btnOffers).Name = "btnOffers";
		((System.Windows.Forms.Control)(object)this.btnOffers).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOffers).Click += new System.EventHandler(btnOffers_Click);
		((UltraButtonBase)this.btnReservationSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnReservationSearch, "btnReservationSearch");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.btnReservationSearch).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnReservationSearch).Name = "btnReservationSearch";
		((System.Windows.Forms.Control)(object)this.btnReservationSearch).Click += new System.EventHandler(btnReservationSearch_Click);
		resources.ApplyResources(this.lblReservationNo, "lblReservationNo");
		this.lblReservationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReservationNo).Name = "lblReservationNo";
		((ControlBase)this.lblReservationNo).WrapText = false;
		resources.ApplyResources(this.cboReservationNo, "cboReservationNo");
		((TextEditorControlBase)this.cboReservationNo).AlwaysInEditMode = true;
		this.cboReservationNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboReservationNo).Name = "cboReservationNo";
		((TextEditorControlBase)this.cboReservationNo).ValueChanged += new System.EventHandler(cboReservationNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboReservationNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboReservationNo_KeyDown);
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.btnClientAdd).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		resources.ApplyResources(this.btnStoreTransfer, "btnStoreTransfer");
		((System.Windows.Forms.Control)(object)this.btnStoreTransfer).Name = "btnStoreTransfer";
		((System.Windows.Forms.Control)(object)this.btnStoreTransfer).Click += new System.EventHandler(btnStoreTransfer_Click);
		resources.ApplyResources(this.lblSerial, "lblSerial");
		this.lblSerial.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSerial).Name = "lblSerial";
		((ControlBase)this.lblSerial).WrapText = false;
		resources.ApplyResources(this.txtSerial, "txtSerial");
		((System.Windows.Forms.Control)(object)this.txtSerial).Name = "txtSerial";
		((System.Windows.Forms.Control)(object)this.txtSerial).KeyUp += new System.Windows.Forms.KeyEventHandler(txtSerial_KeyUp);
		resources.ApplyResources(this.lblDiscInvRatio, "lblDiscInvRatio");
		this.lblDiscInvRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscInvRatio).Name = "lblDiscInvRatio";
		((ControlBase)this.lblDiscInvRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscInvRatio, "txtDiscInvRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscInvRatio).Name = "txtDiscInvRatio";
		((EditorButtonControlBase)this.txtDiscInvRatio).ReadOnly = true;
		resources.ApplyResources(this.lblDiscInvValue, "lblDiscInvValue");
		this.lblDiscInvValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscInvValue).Name = "lblDiscInvValue";
		((ControlBase)this.lblDiscInvValue).WrapText = false;
		resources.ApplyResources(this.txtDiscInvValue, "txtDiscInvValue");
		((System.Windows.Forms.Control)(object)this.txtDiscInvValue).Name = "txtDiscInvValue";
		((EditorButtonControlBase)this.txtDiscInvValue).ReadOnly = true;
		resources.ApplyResources(this.txtClientDisc, "txtClientDisc");
		((System.Windows.Forms.Control)(object)this.txtClientDisc).Name = "txtClientDisc";
		((TextEditorControlBase)this.txtClientDisc).ValueChanged += new System.EventHandler(txtClientDisc_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtClientDisc).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblClientDisc, "lblClientDisc");
		this.lblClientDisc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientDisc).Name = "lblClientDisc";
		resources.ApplyResources(this.chkApplyTax, "chkApplyTax");
		((System.Windows.Forms.Control)(object)this.chkApplyTax).Name = "chkApplyTax";
		((UltraToggleEditorBase)this.chkApplyTax).CheckedChanged += new System.EventHandler(chkApplyTax_CheckedChanged);
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		resources.ApplyResources(this.btnLineSearch, "btnLineSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.btnLineSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnLineSearch).Name = "btnLineSearch";
		((System.Windows.Forms.Control)(object)this.btnLineSearch).TabStop = false;
		resources.ApplyResources(this.lblLine, "lblLine");
		this.lblLine.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		resources.ApplyResources(this.cboLine, "cboLine");
		((TextEditorControlBase)this.cboLine).AlwaysInEditMode = true;
		this.cboLine.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		((EditorButtonControlBase)this.cboLine).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.cboLine).TabStop = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLineSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplyTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientDisc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscInvRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscInvRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscInvValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscInvValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreTransfer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReservationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReservationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOffers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmountPaid);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmountPaid);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientDisc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientCard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientCardNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesMan2Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBrabnchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAccountAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountAmount);
		base.Name = "frmLnsInvoicesWithPayment3";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBrabnchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesMan2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientCardNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientCard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientDisc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmountPaid, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmountPaid, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOffers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReservationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReservationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReservationSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreTransfer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscInvValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscInvValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscInvRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscInvRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientDisc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplyTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLineSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccountAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientCardNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmountPaid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReservationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscInvRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscInvValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientDisc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
