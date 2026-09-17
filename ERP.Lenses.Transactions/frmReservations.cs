using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
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
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Lenses.Transactions;

public class frmReservations : frmHeaderManyDetails
{
	private InputLanguage Language;

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

	private DataTable dtClientsGroup;

	private DataTable dtItemPrices;

	private DataTable dtPriceType;

	private DataTable dtPOSDefaultData;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtShiftDetails;

	private DataTable dtTaxs;

	private DataTable dtCurrency;

	private DataTable dtOffers;

	private DataTable dtVisaType;

	private DataTable dtUsers;

	private DataTable dtReservationsPayments;

	private DataTable dtReservationsPaymentsReturns;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlUsers1 = new ValueList();

	private ValueList vlVisaType = new ValueList();

	private ValueList vlVisaType1 = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int NewPriceUserID = 0;

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private ArrayList ArOfferIDs = new ArrayList();

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool UsingSalesDiscountLevels = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private IContainer components = null;

	private UltraLabel lblCashAmount;

	private UltraTextEditor txtPaidAmount;

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

	private UltraComboEditor cboClientGroup;

	private UltraCheckEditor chkGroup;

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

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblRestAmountPaid;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboMobile;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataPayments;

	public UltraButton btnOffers;

	private UltraDateTimeEditor dtpDeliverdDate;

	private UltraCheckEditor chkIsDeliverd;

	private UltraButton btnReservationPayments;

	private UltraCheckEditor chkIsCanceled;

	private UltraDateTimeEditor dtpCancelledDate;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGPaymentsReturns;

	private UltraButton btnPaymentsReturns;

	public frmReservations()
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
		InitializeComponent();
		TableName = "Lns_Reservations";
		IDCol = "ReservationID";
		NoCol = "ReservationNo";
		DateCol = "ReservationDate";
	}

	public frmReservations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UsingSalesDiscountLevels = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from G_DiscountSettings ").Rows[0][0].ToString()) > 0;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "SubAccountID", "Mobile");
		dtClientsGroup = SubAccounts.GroupsFillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClientGroup, dtClientsGroup, "SubAccountID", "SubAccountName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
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
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtUsers.Rows.Count; num2++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num2]["User_ID"], dtUsers.Rows[num2]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlVisaType.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtVisaType.Rows.Count; num3++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[num3]["VisaTypeID"], dtVisaType.Rows[num3]["VisaTypeName"].ToString());
		}
		vlUsers1.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtUsers.Rows.Count; num4++)
		{
			vlUsers1.ValueListItems.Add(dtUsers.Rows[num4]["User_ID"], dtUsers.Rows[num4]["UserName"].ToString());
		}
		vlVisaType1.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtVisaType.Rows.Count; num5++)
		{
			vlVisaType1.ValueListItems.Add(dtVisaType.Rows[num5]["VisaTypeID"], dtVisaType.Rows[num5]["VisaTypeName"].ToString());
		}
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = ReservationsDetails.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtReservationsPayments = ReservationsPayments.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtReservationsPaymentsReturns = ReservationsPaymentsReturns.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataPayments).DataSource = dtReservationsPayments;
		((UltraGridBase)ULGPaymentsReturns).DataSource = dtReservationsPaymentsReturns;
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
			DataTable dataTable = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ReservationNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["ReservationDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClientCode).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			((UltraToggleEditorBase)chkIsDeliverd).Checked = Convert.ToBoolean(drMaster["IsDeliverd"]);
			dtpDeliverdDate.Value = drMaster["DeliverdDate"];
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtCashAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtCashAmount_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkIsCanceled).Checked = Convert.ToBoolean(drMaster["IsCancelled"]);
			dtpCancelledDate.Value = drMaster["CancelledDate"];
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ReservationsDetails.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			dtReservationsPayments = ReservationsPayments.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataPayments).DataSource = dtReservationsPayments;
			dtReservationsPaymentsReturns = ReservationsPaymentsReturns.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGPaymentsReturns).DataSource = dtReservationsPaymentsReturns;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || !CanEditFromServer || drMaster["IsDeliverd"].Equals(true))
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
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		GlobalFunctions.PrepareGrid(ULGPaymentsReturns);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationDetailID"].DefaultCellValue = -1;
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].DefaultCellValue = dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString();
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OfferDiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].Header).Caption = (GlobalVariables.IsArabic ? "الفيزا" : "Visa");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الفيزا" : "Visa Amount");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].Header).Caption = (GlobalVariables.IsArabic ? "نقدي" : "Cash");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة النقدي" : "Cash Amount");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsOnAccount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsOnAccount"].Header).Caption = (GlobalVariables.IsArabic ? "على الحساب" : "OnAccount");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsOnAccount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsOnAccount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة على الحساب" : "OnAccount Amount");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["OnAccountAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnID"].DefaultCellValue = -1;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnNo"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnNo"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnDate"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["ReservationPaymentReturnDate"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsVisa"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsVisa"].Header).Caption = (GlobalVariables.IsArabic ? "الفيزا" : "Visa");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsVisa"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsVisa"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType1;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaNo"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaNo"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaAmount"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الفيزا" : "Visa Amount");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaAmount"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["VisaAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsCash"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsCash"].Header).Caption = (GlobalVariables.IsArabic ? "نقدي" : "Cash");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsCash"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsCash"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["CashAmount"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["CashAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة النقدي" : "Cash Amount");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["CashAmount"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["CashAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsOnAccount"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsOnAccount"].Header).Caption = (GlobalVariables.IsArabic ? "على الحساب" : "OnAccount");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsOnAccount"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["IsOnAccount"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة على الحساب" : "OnAccount Amount");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["OnAccountAmount"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["OnAccountAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGPaymentsReturns).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGPaymentsReturns).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers1;
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMobile).ReadOnly = NavMode;
		((UltraToggleEditorBase)chkGroup).Checked = Adding;
		((Control)(object)chkGroup).Enabled = Adding;
		((EditorButtonControlBase)cboPriceType).ReadOnly = !CanModifyPriceType;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((Control)(object)txtBrabnchBalance).Visible = !NavMode;
		((Control)(object)lblBalance).Visible = !NavMode;
		((Control)(object)chkIsDeliverd).Enabled = false;
		((EditorButtonControlBase)dtpDeliverdDate).ReadOnly = true;
		((Control)(object)chkIsCanceled).Enabled = !NavMode;
		((EditorButtonControlBase)dtpCancelledDate).ReadOnly = NavMode;
		((Control)(object)btnReservationPayments).Visible = !NavMode;
		((Control)(object)btnPaymentsReturns).Visible = !NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)btnClientBalance).Visible = !NavMode;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && !CanModifyPriceType;
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((Control)(object)dtpShiftDate).Visible = !Adding;
		((Control)(object)txtShiftNo).Visible = !Adding;
		((Control)(object)lblShiftDate).Visible = !Adding;
		((Control)(object)lblShiftNo).Visible = !Adding;
		((Control)(object)btnOffers).Visible = !NavMode && ((Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "-1", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows.Count > 0 && !NavMode) ? true : false);
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
			DataView dataView3 = new DataView(dtVisaType);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable3 = dataView3.ToTable();
			vlVisaType.ValueListItems.Clear();
			for (int l = 0; l < dataTable3.Rows.Count; l++)
			{
				vlVisaType.ValueListItems.Add(dataTable3.Rows[l]["VisaTypeID"], dataTable3.Rows[l]["VisaTypeName"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int m = 0; m < dtItems.Rows.Count; m++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
			}
			vlVisaType.ValueListItems.Clear();
			for (int n = 0; n < dtVisaType.Rows.Count; n++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[n]["VisaTypeID"], dtVisaType.Rows[n]["VisaTypeName"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		for (int num = 0; num < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num++)
		{
			if (((UltraGridBase)ULGData).Rows[num].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[num].Cells["ItemID"].Value.ToString())[0];
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
				((UltraGridBase)ULGData).Rows[num].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["BatchID"].Value = DBNull.Value;
				}
			}
			if (!bool.Parse(dataRow["IsService"].ToString()))
			{
				int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList = getUnitsValueList(unitTypeID);
				((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
				if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].Value = DBNull.Value;
				}
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].Value = DBNull.Value;
				}
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].Value = DBNull.Value;
				}
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? Reservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		((UltraToggleEditorBase)chkIsCanceled).Checked = false;
		dtpCancelledDate.Value = null;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.Value = null;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtBrabnchBalance).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtCashAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtCashAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
		UltraComboEditor obj = cboClientCode;
		UltraComboEditor obj2 = cboClient;
		int num = (cboMobile.SelectedIndex = -1);
		int selectedIndex = (obj2.SelectedIndex = num);
		obj.SelectedIndex = selectedIndex;
		((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] != DBNull.Value && Adding)
		{
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			UltraComboEditor obj3 = cboClientCode;
			UltraComboEditor obj4 = cboMobile;
			object obj5 = (((TextEditorControlBase)cboClient).Value = dtPOSDefaultData.Rows[0]["DefaultSubAccountID"]);
			object value = (((TextEditorControlBase)obj4).Value = obj5);
			((TextEditorControlBase)obj3).Value = value;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		}
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGPaymentsReturns).DataSource).Rows.Clear();
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

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			string val = "";
			ReportDocument reportDocument = new ReportDocument();
			if (dtReports.Rows.Count > 0)
			{
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_Reservations_A.rpt" : "Rep_Lns_Reservations_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ReservationIDs", "," + RowID + ",");
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.SetParameterValue("@ReservationIDs", "," + RowID + ",", "Tax name");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
			reportDocument.SetParameterValue("@Balance", val);
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
			reportDocument.Dispose();
			GC.Collect();
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ReservationsReport(GlobalVariables.BranchIDs, "-1", "-1", "-1", "0");
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ReservationID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboClient).Value;
		object value2 = ((TextEditorControlBase)cboClientCode).Value;
		object value3 = ((TextEditorControlBase)cboClientGroup).Value;
		object value4 = ((TextEditorControlBase)cboMobile).Value;
		object value5 = ((TextEditorControlBase)cboPriceType).Value;
		object value6 = ((TextEditorControlBase)cboTax).Value;
		object value7 = ((TextEditorControlBase)cboTransactionBranch).Value;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientGroup).ValueChanged -= cboClientGroup_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
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
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
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
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtStores);
		dataView.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView.ToTable();
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dataTable.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[l]["StoreID"], dataTable.Rows[l]["StoreName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int m = 0; m < dtUsers.Rows.Count; m++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[m]["User_ID"], dtUsers.Rows[m]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView2.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int n = 0; n < dataTable2.Rows.Count; n++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[n]["ItemID"], dataTable2.Rows[n]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[n]["ItemID"], dataTable2.Rows[n]["ItemBarCode"].ToString());
			}
			DataView dataView3 = new DataView(dtVisaType);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable3 = dataView3.ToTable();
			vlVisaType.ValueListItems.Clear();
			for (int num = 0; num < dataTable3.Rows.Count; num++)
			{
				vlVisaType.ValueListItems.Add(dataTable3.Rows[num]["VisaTypeID"], dataTable3.Rows[num]["VisaTypeName"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtItems.Rows.Count; num2++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["ItemBarCode"].ToString());
			}
			vlVisaType.ValueListItems.Clear();
			for (int num3 = 0; num3 < dtVisaType.Rows.Count; num3++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[num3]["VisaTypeID"], dtVisaType.Rows[num3]["VisaTypeName"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtUnits.Rows.Count; num4++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num4]["UnitID"], dtUnits.Rows[num4]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtTaxs.Rows.Count; num5++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num5]["TaxID"], dtTaxs.Rows[num5]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "SubAccountID", "Mobile");
		dtClientsGroup = SubAccounts.GroupsFillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClientGroup, dtClientsGroup, "SubAccountID", "SubAccountName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		((TextEditorControlBase)cboClientGroup).Value = value3;
		((TextEditorControlBase)cboClient).Value = value;
		((TextEditorControlBase)cboClientCode).Value = value2;
		((TextEditorControlBase)cboMobile).Value = value4;
		((TextEditorControlBase)cboPriceType).Value = value5;
		((TextEditorControlBase)cboTax).Value = value6;
		((TextEditorControlBase)cboTransactionBranch).Value = value7;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientGroup).ValueChanged += cboClientGroup_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
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
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((UltraToggleEditorBase)chkGroup).Checked = false;
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
		if (((UltraToggleEditorBase)chkIsCanceled).Checked && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("لا يمكن الالغاء لوجود قيمة مسدده", "Cannot Cancel Because There's Paid Amount");
			((Control)(object)chkIsCanceled).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsCanceled).Checked && dtpCancelledDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء ادخال تاريخ الإلغاء", "Please Select Cancelation Date");
			((Control)(object)dtpCancelledDate).Focus();
			return false;
		}
		if (dtPOSDefaultData.Rows.Count == 0 || dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		((UltraGridBase)ULGData).UpdateData();
		dtDetails.AcceptChanges();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_Reservations", "ReservationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReservationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Reservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		if (Adding)
		{
			DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
			if (dataTable.Rows.Count != 1)
			{
				GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
				return false;
			}
			DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
			DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
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
			if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()))
			{
				GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date Less Than Shift End Date Check Your pc ");
				return false;
			}
			ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
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
				DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(ShiftDetailID, GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
				if (dataTable2.Rows.Count == 0)
				{
					ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
				}
				else
				{
					ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Expected O, but got Unknown
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Expected O, but got Unknown
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
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			num = Reservations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((Control)(object)txtNotes).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", ((UltraToggleEditorBase)chkIsCanceled).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCanceled).Checked ? dtpCancelledDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ReservationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
	}

	public override void UpdateData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Reservations.Insert_Update(drMaster["ReservationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((Control)(object)txtNotes).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, drMaster["ShiftDetailID"].ToString(), drMaster["ShiftDetailUserID"].ToString(), drMaster["User_ID"].ToString(), (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", ((UltraToggleEditorBase)chkIsCanceled).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCanceled).Checked ? dtpCancelledDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV");
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[j].Cells["ReservationDetailID"].Value.ToString() + ",";
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("Lns_ReservationsDetails", "ReservationID", drMaster["ReservationID"].ToString(), "ReservationDetailID", text);
			ReservationsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "LnsMIV", "LnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
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
			Reservations.DeleteVirtual(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsDetails.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsPayments.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsPaymentsReturns.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
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
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.Cells["ItemID"].Value != DBNull.Value && (decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m || Row.Cells["OfferID"].Value != DBNull.Value))
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
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

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
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
		return result;
	}

	public void AddItemInGid()
	{
		//IL_0fd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_0e44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Expected O, but got Unknown
		//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9e: Expected O, but got Unknown
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Expected O, but got Unknown
		//IL_17c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d3: Expected O, but got Unknown
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Expected O, but got Unknown
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
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3 && ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() == text6)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					CalculateGoss();
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
					CalculateTotalsTax();
					((TextEditorControlBase)txtBarCode).Clear();
					((TextEditorControlBase)txtBarCode).Focus();
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
					return;
				}
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = text5);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = text6;
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["IsBarcodeRead"].Value = true;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(" UnitID = " + text6 + " and  ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString());
				if (array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = array[0]["OfferID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = array[0]["OfferDiscountRatio"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
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
			if (((UltraGridBase)ULGData).Rows[j].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() == text3 && (!bool.Parse(dataRow2["IsUnitPrice"].ToString()) || ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString().Equals(dataRow2["UnitID"].ToString())))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[j]);
				CalculateTotalsTax();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow2["ItemID"].ToString());
		obj5.Value = value;
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
		((UltraGridBase)ULGData).ActiveRow.Cells["IsBarcodeRead"].Value = true;
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
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
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID")
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
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
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
		//IL_10db: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Expected O, but got Unknown
		//IL_10f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Expected O, but got Unknown
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
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + e.Cell.Value.ToString()) : (" ItemID = " + e.Cell.Value.ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array2[0]["Price"].ToString()) - decimal.Parse(array2[0]["Price"].ToString()) * decimal.Parse(array2[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array2[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
								((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
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
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		if (ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
			{
				ULGData_CellListSelect(ULGData, e);
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
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
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
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
		CalculateTotalsTax();
		ULGData.AfterRowsDeleted += ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
			int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
			if (num != 0)
			{
				((UltraToggleEditorBase)chkGroup).Checked = false;
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Expected O, but got Unknown
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
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["Notes"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["Notes"].ToString());
		}
		((Control)(object)txtBrabnchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : drMaster["BranchID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value)
				{
					DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
					if (array != null && array.Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
			}
			CalculateGoss();
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
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		}
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
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	public void OpenChangeDiscountForm()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse(((Control)(object)txtGrossValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscBeforeTaxValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "0" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / CalculateGrossWithoutItemUnderDiscount() * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
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

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
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
		if (e.KeyCode == Keys.Return && ((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
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

	private void chkGroup_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboClientGroup).Enabled = ((UltraToggleEditorBase)chkGroup).Checked;
		if (!((UltraToggleEditorBase)chkGroup).Checked)
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
			GlobalFunctions.FillCombo(cboMobile, dtClients, "SubAccountID", "Mobile");
		}
		else if (((UltraToggleEditorBase)chkGroup).Checked && cboClientGroup.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = " ParentID=" + ((TextEditorControlBase)cboClientGroup).Value.ToString();
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboClient, dt, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dt, "SubAccountID", "ClientSupplierNo");
			GlobalFunctions.FillCombo(cboMobile, dt, "SubAccountID", "Mobile");
		}
	}

	private void cboClientGroup_ValueChanged(object sender, EventArgs e)
	{
		if (Adding && ((UltraToggleEditorBase)chkGroup).Checked && cboClientGroup.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = " ParentID=" + ((TextEditorControlBase)cboClientGroup).Value.ToString();
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboClient, dt, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dt, "SubAccountID", "ClientSupplierNo");
			GlobalFunctions.FillCombo(cboMobile, dt, "SubAccountID", "Mobile");
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow[] array = ((!bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString())) ? dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : ((((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value != DBNull.Value) ? dtItemPrices.Select(" UnitID = " + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + " and  ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) : null));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
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
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
		if (Adding)
		{
			((Control)(object)txtCode).Text = Reservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnOffers_Click(object sender, EventArgs e)
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_1760: Unknown result type (might be due to invalid IL or missing references)
		//IL_176a: Expected O, but got Unknown
		//IL_2385: Unknown result type (might be due to invalid IL or missing references)
		//IL_238f: Expected O, but got Unknown
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbd: Expected O, but got Unknown
		//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Expected O, but got Unknown
		//IL_2255: Unknown result type (might be due to invalid IL or missing references)
		//IL_225f: Expected O, but got Unknown
		//IL_2cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d03: Expected O, but got Unknown
		//IL_1613: Unknown result type (might be due to invalid IL or missing references)
		//IL_161d: Expected O, but got Unknown
		dtOffers = Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "-1", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtOffers.Rows.Count > 1)
		{
			frmSelectOffers frmSelectOffers2 = new frmSelectOffers(dtOffers);
			frmSelectOffers2.Location = new Point(0, 0);
			frmSelectOffers2.ShowDialog();
			if (frmSelectOffers2.OfferID != 0 && !frmSelectOffers2.Cancel && dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsQtyDiscount"].Equals(false))
			{
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					for (int i = 0; i < frmSelectOffers2.dtOffersItems.Rows.Count; i++)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["ItemID"]);
						obj.Value = value;
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmSelectOffers2.dtOffersItems.Rows[i]["BatchID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = 0;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					for (int j = 0; j < frmSelectOffers2.dtOffersGifts.Rows.Count; j++)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["ItemID"]);
						obj3.Value = value;
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmSelectOffers2.dtOffersGifts.Rows[j]["BatchID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					CalculateGoss();
					CalculateTotalsTax();
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
			else
			{
				if (frmSelectOffers2.OfferID == 0 || frmSelectOffers2.Cancel || !dtOffers.Select(" OfferID= " + frmSelectOffers2.OfferID)[0]["IsQtyDiscount"].Equals(true) || dtItemPrices == null || dtItemPrices.Rows.Count <= 0 || cboClient.SelectedIndex <= -1)
				{
					return;
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ItemID", typeof(int));
				dataTable.Columns.Add("UnitID", typeof(int));
				dataTable.Columns.Add("BatchID", typeof(int));
				dataTable.Columns.Add("ColorID", typeof(int));
				dataTable.Columns.Add("ItemSizeID", typeof(int));
				dataTable.Columns.Add("Qty", typeof(decimal));
				dataTable.Columns.Add("OfferID", typeof(int));
				dataTable.Columns.Add("Price", typeof(decimal));
				for (int k = 0; k < frmSelectOffers2.dtOffersItems.Rows.Count; k++)
				{
					dataTable.Rows.Add(frmSelectOffers2.dtOffersItems.Rows[k]["ItemID"], frmSelectOffers2.dtOffersItems.Rows[k]["UnitID"], frmSelectOffers2.dtOffersItems.Rows[k]["BatchID"], frmSelectOffers2.dtOffersItems.Rows[k]["ColorID"], frmSelectOffers2.dtOffersItems.Rows[k]["ItemSizeID"], frmSelectOffers2.dtOffersItems.Rows[k]["Qty"], frmSelectOffers2.dtOffersItems.Rows[k]["OfferID"], dtItemPrices.Select(" ItemID= " + frmSelectOffers2.dtOffersItems.Rows[k]["ItemID"].ToString())[0]["Price"]);
				}
				DataView dataView = new DataView(dataTable);
				if (frmSelectOffers2.LowestPrice)
				{
					dataView.Sort = "Price ASC";
				}
				else if (frmSelectOffers2.HighestPrice)
				{
					dataView.Sort = "Price Desc";
				}
				decimal num = default(decimal);
				for (int l = 0; l < dataView.Count; l++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataView[l]["ItemID"]);
					obj5.Value = value;
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataView[l]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataView[l]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataView[l]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataView[l]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dataView[l]["BatchID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmSelectOffers2.OfferID;
					if (frmSelectOffers2.ForAll)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
					}
					else if (frmSelectOffers2.HighestPrice && num < frmSelectOffers2.QtyDiscount && dataView.Count > 0)
					{
						if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[l]["ItemID"].ToString()))
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
							++num;
						}
					}
					else if (frmSelectOffers2.LowestPrice && num < frmSelectOffers2.QtyDiscount && dataView.Count > 0 && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[l]["ItemID"].ToString()))
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmSelectOffers2.DiscountRatio;
						++num;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
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
			if (dtOffers.Rows[0]["IsQtyDiscount"].Equals(false))
			{
				frmLnsInvoicesGiftsoffers frmLnsInvoicesGiftsoffers2 = new frmLnsInvoicesGiftsoffers(int.Parse(dtOffers.Rows[0]["OfferID"].ToString()));
				frmLnsInvoicesGiftsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				frmLnsInvoicesGiftsoffers2.Location = new Point(0, 0);
				((Control)(object)frmLnsInvoicesGiftsoffers2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار عرض" : "Select Offer");
				frmLnsInvoicesGiftsoffers2.ShowDialog();
				if (!frmLnsInvoicesGiftsoffers2.Cancel && dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					for (int m = 0; m < frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows.Count; m++)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj7 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["ItemID"]);
						obj7.Value = value;
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersItems.Rows[m]["BatchID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesGiftsoffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = 0;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					for (int n = 0; n < frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows.Count; n++)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj9 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["ItemID"]);
						obj9.Value = value;
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmLnsInvoicesGiftsoffers2.dtOffersGifts.Rows[n]["BatchID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesGiftsoffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesGiftsoffers2.DiscountRatio;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					CalculateGoss();
					CalculateTotalsTax();
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
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
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("ItemID", typeof(int));
				dataTable2.Columns.Add("UnitID", typeof(int));
				dataTable2.Columns.Add("BatchID", typeof(int));
				dataTable2.Columns.Add("ColorID", typeof(int));
				dataTable2.Columns.Add("ItemSizeID", typeof(int));
				dataTable2.Columns.Add("Qty", typeof(decimal));
				dataTable2.Columns.Add("OfferID", typeof(int));
				dataTable2.Columns.Add("Price", typeof(decimal));
				for (int num2 = 0; num2 < frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows.Count; num2++)
				{
					dataTable2.Rows.Add(frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["ItemID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["UnitID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["BatchID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["ColorID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["ItemSizeID"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["Qty"], frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["OfferID"], dtItemPrices.Select(" ItemID= " + frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems.Rows[num2]["ItemID"].ToString())[0]["Price"]);
				}
				DataView dataView2 = new DataView(dataTable2);
				if (frmLnsInvoicesQtyDiscountsoffers2.LowestPrice)
				{
					dataView2.Sort = "Price ASC";
				}
				else if (frmLnsInvoicesQtyDiscountsoffers2.HighestPrice)
				{
					dataView2.Sort = "Price Desc";
				}
				decimal num3 = default(decimal);
				for (int num4 = 0; num4 < dataView2.Count; num4++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataView2[num4]["ItemID"]);
					obj11.Value = value;
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataView2[num4]["Qty"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataView2[num4]["UnitID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataView2[num4]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataView2[num4]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = dataView2[num4]["BatchID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultStoreID"] != DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultStoreID"] : ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value));
					((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmLnsInvoicesQtyDiscountsoffers2.OfferID;
					if (frmLnsInvoicesQtyDiscountsoffers2.ForAll)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
					}
					else if (frmLnsInvoicesQtyDiscountsoffers2.HighestPrice && num3 < frmLnsInvoicesQtyDiscountsoffers2.QtyDiscount && dataView2.Count > 0)
					{
						if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView2[num4]["ItemID"].ToString()))
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
							++num3;
						}
					}
					else if (frmLnsInvoicesQtyDiscountsoffers2.LowestPrice && num3 < frmLnsInvoicesQtyDiscountsoffers2.QtyDiscount && dataView2.Count > 0 && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView2[num4]["ItemID"].ToString()))
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
						++num3;
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				}
				CalculateGoss();
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

	private void btnSalesMan2Search_Click(object sender, EventArgs e)
	{
	}

	private void ULGDataPayments_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Selected = true;
	}

	private void ULGDataPayments_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		GlobalVariables.InformationMB.Show("لا يمكن حذف السداد لوجود خزينة المستخدم مغلقة؟", " Data Cannot be Deleted User Safe Is Closed");
		GlobalVariables.InformationMB.Show("لا يمكن حذف السداد", "Data Cannot be Deleted");
		((CancelEventArgs)(object)e).Cancel = true;
	}

	private void btnReservationPayments_Click(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkIsCanceled).Checked)
		{
			SaveClose(Close: false);
			if (Updating)
			{
				frmReservationPayments frmReservationPayments2 = new frmReservationPayments(int.Parse(drMaster["ReservationID"].ToString()), decimal.Parse(((Control)(object)txtRestAmount).Text.ToString()));
				frmReservationPayments2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmReservationPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "مدفوعات الحجز" : "Reservation Payments");
				frmReservationPayments2.CanAdd = CanAdd;
				frmReservationPayments2.CanUpdate = CanUpdate;
				frmReservationPayments2.CanDelete = CanDelete;
				frmReservationPayments2.CanDiscount = CanDiscount;
				frmReservationPayments2.CanSearching = CanSearching;
				frmReservationPayments2.CanExport = CanExport;
				frmReservationPayments2.CanPrint = CanPrint;
				frmReservationPayments2.CanPrintReport = CanPrintReport;
				frmReservationPayments2.CanViewReport = CanViewReport;
				frmReservationPayments2.ShowDialog();
				dtReservationsPayments = ReservationsPayments.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				object obj = dtReservationsPaymentsReturns.Compute(" Sum(CashAmount) ", "");
				object obj2 = dtReservationsPaymentsReturns.Compute(" Sum(OnAccountAmount) ", "");
				object obj3 = dtReservationsPaymentsReturns.Compute(" Sum(VisaAmount) ", "");
				decimal num = decimal.Parse((decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()) + decimal.Parse((obj2 == DBNull.Value) ? "0" : obj2.ToString()) + decimal.Parse((obj3 == DBNull.Value) ? "0" : obj3.ToString())).ToString());
				object obj4 = dtReservationsPayments.Compute(" Sum(CashAmount) ", "");
				object obj5 = dtReservationsPayments.Compute(" Sum(OnAccountAmount) ", "");
				object obj6 = dtReservationsPayments.Compute(" Sum(VisaAmount) ", "");
				((Control)(object)txtPaidAmount).Text = decimal.Parse((decimal.Parse((obj4 == DBNull.Value) ? "0" : obj4.ToString()) + decimal.Parse((obj5 == DBNull.Value) ? "0" : obj5.ToString()) + decimal.Parse((obj6 == DBNull.Value) ? "0" : obj6.ToString()) - num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((UltraGridBase)ULGDataPayments).DataSource = dtReservationsPayments;
				InitGrid();
				base.btnOKClick();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("تم إلغاء الحجز", "This is A Canceled Reservation");
		}
	}

	public bool SaveClose(bool Close)
	{
		DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return false;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
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
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date Less Than Shift End Date Check Your pc ");
			return false;
		}
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
		}
		if (ValidateData())
		{
			DataSaved = true;
			if (Adding)
			{
				AddData();
				if (DataSaved)
				{
					Adding = false;
					Updating = true;
					if (!Close)
					{
						DataTable dataTable3 = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
						if (dataTable3.Rows.Count > 0)
						{
							drMaster = dataTable3.Rows[0];
						}
						else
						{
							drMaster = null;
						}
					}
				}
			}
			else
			{
				UpdateData();
				if (!Close)
				{
					DataTable dataTable4 = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
					if (dataTable4.Rows.Count > 0)
					{
						drMaster = dataTable4.Rows[0];
					}
					else
					{
						drMaster = null;
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				}
			}
			DisplayData();
			if (DataSaved && Close)
			{
				base.Close();
			}
			if (!DataSaved)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	private void chkIsCanceled_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpCancelledDate).Enabled = ((UltraToggleEditorBase)chkIsCanceled).Checked;
	}

	private void btnPaymentsReturns_Click(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkIsCanceled).Checked)
		{
			SaveClose(Close: false);
			if (Updating)
			{
				frmReservationPaymentsReturns frmReservationPaymentsReturns2 = new frmReservationPaymentsReturns(int.Parse(drMaster["ReservationID"].ToString()), decimal.Parse(((Control)(object)txtRestAmount).Text.ToString()));
				frmReservationPaymentsReturns2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmReservationPaymentsReturns2.lblTitle).Text = (GlobalVariables.IsArabic ? "مردودات الحجز" : "Reservation Payments Returns");
				frmReservationPaymentsReturns2.CanAdd = CanAdd;
				frmReservationPaymentsReturns2.CanUpdate = CanUpdate;
				frmReservationPaymentsReturns2.CanDelete = CanDelete;
				frmReservationPaymentsReturns2.CanDiscount = CanDiscount;
				frmReservationPaymentsReturns2.CanSearching = CanSearching;
				frmReservationPaymentsReturns2.CanExport = CanExport;
				frmReservationPaymentsReturns2.CanPrint = CanPrint;
				frmReservationPaymentsReturns2.CanPrintReport = CanPrintReport;
				frmReservationPaymentsReturns2.CanViewReport = CanViewReport;
				frmReservationPaymentsReturns2.ShowDialog();
				dtReservationsPaymentsReturns = ReservationsPaymentsReturns.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				object obj = dtReservationsPaymentsReturns.Compute(" Sum(CashAmount) ", "");
				object obj2 = dtReservationsPaymentsReturns.Compute(" Sum(OnAccountAmount) ", "");
				object obj3 = dtReservationsPaymentsReturns.Compute(" Sum(VisaAmount) ", "");
				decimal num = decimal.Parse((decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()) + decimal.Parse((obj2 == DBNull.Value) ? "0" : obj2.ToString()) + decimal.Parse((obj3 == DBNull.Value) ? "0" : obj3.ToString())).ToString());
				object obj4 = dtReservationsPayments.Compute(" Sum(CashAmount) ", "");
				object obj5 = dtReservationsPayments.Compute(" Sum(OnAccountAmount) ", "");
				object obj6 = dtReservationsPayments.Compute(" Sum(VisaAmount) ", "");
				decimal num2 = decimal.Parse((decimal.Parse((obj4 == DBNull.Value) ? "0" : obj4.ToString()) + decimal.Parse((obj5 == DBNull.Value) ? "0" : obj5.ToString()) + decimal.Parse((obj6 == DBNull.Value) ? "0" : obj6.ToString())).ToString());
				((Control)(object)txtPaidAmount).Text = decimal.Parse((num2 - num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((UltraGridBase)ULGPaymentsReturns).DataSource = dtReservationsPaymentsReturns;
				InitGrid();
				base.btnOKClick();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("تم إلغاء الحجز", "This is A Canceled Reservation");
		}
	}

	private void ULGPaymentsReturns_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGPaymentsReturns).ActiveRow).Selected = true;
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
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Expected O, but got Unknown
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Expected O, but got Unknown
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmReservations));
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGPaymentsReturns = new UltraGrid();
		this.lblCashAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
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
		this.cboClientGroup = new UltraComboEditor();
		this.chkGroup = new UltraCheckEditor();
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
		this.txtRestAmount = new UltraTextEditor();
		this.lblRestAmountPaid = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.cboMobile = new UltraComboEditor();
		this.btnOffers = new UltraButton();
		this.dtpDeliverdDate = new UltraDateTimeEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.btnReservationPayments = new UltraButton();
		this.chkIsCanceled = new UltraCheckEditor();
		this.dtpCancelledDate = new UltraDateTimeEditor();
		this.btnPaymentsReturns = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPaymentsReturns).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.cboClientGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelledDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Payments";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "PaymentsReturns";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
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
		((System.Windows.Forms.Control)(object)base.txtCode).TabStop = false;
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataPayments);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataPayments, "ULGDataPayments");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance1");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val12, "appearance2");
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val13, "appearance3");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance4");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance5");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPayments.AfterEnterEditMode += new System.EventHandler(ULGDataPayments_AfterEnterEditMode);
		this.ULGDataPayments.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataPayments_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPaymentsReturns);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGPaymentsReturns, "ULGPaymentsReturns");
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance6");
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val17, "appearance7");
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val18, "appearance8");
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance9");
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance10");
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGPaymentsReturns).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGPaymentsReturns).Name = "ULGPaymentsReturns";
		((UltraControlBase)this.ULGPaymentsReturns).UseFlatMode = (DefaultableBoolean)1;
		this.ULGPaymentsReturns.AfterEnterEditMode += new System.EventHandler(ULGPaymentsReturns_AfterEnterEditMode);
		resources.ApplyResources(this.lblCashAmount, "lblCashAmount");
		this.lblCashAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCashAmount).Name = "lblCashAmount";
		((ControlBase)this.lblCashAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((EditorButtonControlBase)this.txtPaidAmount).ReadOnly = true;
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtCashAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance19");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val21;
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
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val22, "appearance20");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val22;
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
		((AppearanceBase)val23).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val23).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val23).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val23).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val23).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val23, "appearance21");
		((ControlBase)this.btnClientBalance).Appearance = (AppearanceBase)(object)val23;
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
		resources.ApplyResources(this.cboClientGroup, "cboClientGroup");
		((TextEditorControlBase)this.cboClientGroup).AlwaysInEditMode = true;
		this.cboClientGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClientGroup).Name = "cboClientGroup";
		((TextEditorControlBase)this.cboClientGroup).ValueChanged += new System.EventHandler(cboClientGroup_ValueChanged);
		resources.ApplyResources(this.chkGroup, "chkGroup");
		((System.Windows.Forms.Control)(object)this.chkGroup).Name = "chkGroup";
		((UltraToggleEditorBase)this.chkGroup).CheckedChanged += new System.EventHandler(chkGroup_CheckedChanged);
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
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
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
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
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
		resources.ApplyResources(this.btnOffers, "btnOffers");
		((System.Windows.Forms.Control)(object)this.btnOffers).Name = "btnOffers";
		((System.Windows.Forms.Control)(object)this.btnOffers).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOffers).Click += new System.EventHandler(btnOffers_Click);
		resources.ApplyResources(this.dtpDeliverdDate, "dtpDeliverdDate");
		((UltraWinEditorMaskedControlBase)this.dtpDeliverdDate).AlwaysInEditMode = true;
		this.dtpDeliverdDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpDeliverdDate).Name = "dtpDeliverdDate";
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		resources.ApplyResources(this.btnReservationPayments, "btnReservationPayments");
		((System.Windows.Forms.Control)(object)this.btnReservationPayments).Name = "btnReservationPayments";
		((System.Windows.Forms.Control)(object)this.btnReservationPayments).Click += new System.EventHandler(btnReservationPayments_Click);
		resources.ApplyResources(this.chkIsCanceled, "chkIsCanceled");
		((System.Windows.Forms.Control)(object)this.chkIsCanceled).Name = "chkIsCanceled";
		((UltraToggleEditorBase)this.chkIsCanceled).CheckedChanged += new System.EventHandler(chkIsCanceled_CheckedChanged);
		resources.ApplyResources(this.dtpCancelledDate, "dtpCancelledDate");
		((UltraWinEditorMaskedControlBase)this.dtpCancelledDate).AlwaysInEditMode = true;
		this.dtpCancelledDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpCancelledDate).Name = "dtpCancelledDate";
		resources.ApplyResources(this.btnPaymentsReturns, "btnPaymentsReturns");
		((System.Windows.Forms.Control)(object)this.btnPaymentsReturns).Name = "btnPaymentsReturns";
		((System.Windows.Forms.Control)(object)this.btnPaymentsReturns).Click += new System.EventHandler(btnPaymentsReturns_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPaymentsReturns);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCancelledDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReservationPayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliverdDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOffers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmountPaid);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkGroup);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Name = "frmReservations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmountPaid, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOffers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliverdDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReservationPayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCancelledDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPaymentsReturns, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPaymentsReturns).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.cboClientGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelledDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
