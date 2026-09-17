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
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Transactions;

public class frmSLNPreQuotations : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtUsers;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtItemPrices;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtCurrency;

	private DataTable dtClientContacts;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ArrayList ArDetailIDs = new ArrayList();

	private bool UsingColors;

	private bool UsingSizes = false;

	public DataTable dtPreQuotationsDetailsSlings = new DataTable();

	public DataTable dtPreQuotationsDetailsSlingsMaterials = new DataTable();

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private int newID = -100000;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblTotalPrice;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel lblTotalCost;

	private UltraTextEditor txtTotalCost;

	public UltraButton btnClientSearch;

	private UltraLabel lblClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtProfitPercentage;

	private UltraLabel lblProfitPercentage;

	private UltraLabel lblClientOrderNo;

	private UltraTextEditor txtClientOrderNo;

	private UltraTextEditor txtDeliveryPlace;

	private UltraLabel lblDeliveryPlace;

	private UltraDateTimeEditor dtpExpectedDeliveryDate;

	private UltraLabel lblExpectedDeliveryDate;

	private UltraLabel lblValidityDays;

	private UltraTextEditor txtValidityDays;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	private UltraLabel lblUser;

	private UltraComboEditor cboUsers;

	private UltraLabel lblTermsOfPayment;

	private UltraTextEditor txtTermsOfPayment;

	private UltraLabel lblClientContact;

	private UltraComboEditor cboClientContacts;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	public frmSLNPreQuotations()
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
		TableName = "SLN_PreQuotations";
		IDCol = "PreQuotationID";
		NoCol = "PreQuotationNo";
		DateCol = "PreQuotationDate";
	}

	public frmSLNPreQuotations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "UserID", "UserName");
		dtClientContacts = SubAccountsClientSupplierContacts.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboClientContacts, dtClientContacts, "ClientSupplierContactID", "ContactName");
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
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
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
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
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = PreQuotationsDetails.SelectByPreQuotationID("0", GlobalVariables.IsArabic ? "1" : "0");
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
			DataTable dataTable = PreQuotations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["PreQuotationNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["PreQuotationDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalCost).Text = decimal.Parse(drMaster["TotalCost"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtClientOrderNo).Text = drMaster["ClientOrderNo"].ToString();
			((Control)(object)txtDeliveryPlace).Text = drMaster["DeliveryPlace"].ToString();
			((TextEditorControlBase)txtExchangeRate).ValueChanged -= txtExchangeRate_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtExchangeRate).ValueChanged += txtExchangeRate_ValueChanged;
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTermsOfPayment).Text = drMaster["TermsOfPayment"].ToString();
			((TextEditorControlBase)txtProfitPercentage).ValueChanged -= txtProfitPercentage_ValueChanged;
			((Control)(object)txtProfitPercentage).Text = decimal.Parse(drMaster["ProfitPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtProfitPercentage).ValueChanged += txtProfitPercentage_ValueChanged;
			((Control)(object)txtValidityDays).Text = drMaster["ValidityDays"].ToString();
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["ClientSubAccountID"];
			if (cboClient.SelectedIndex > -1)
			{
				DataView dataView = new DataView(dtClientContacts);
				dataView.RowFilter = "SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString();
				GlobalFunctions.FillCombo(cboClientContacts, dataView.ToTable(), "ClientSupplierContactID", "ContactName");
				cboClientContacts.SelectedIndex = -1;
			}
			((TextEditorControlBase)cboClientContacts).Value = drMaster["ClientContactID"];
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			dtpExpectedDeliveryDate.Value = drMaster["ExpectedDeliveryDate"];
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = PreQuotationsDetails.SelectByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtPreQuotationsDetailsSlings = PreQuotationsDetailsSlings.SelectByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtPreQuotationsDetailsSlingsMaterials = PreQuotationsDetailsSlingsMaterials.SelectByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			CalcTotalQty();
			CalculateTotalPrice();
			CalculateTotalCostPrice();
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PreQuotationDetailID"].DefaultCellValue = -1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Details"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Details");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Header).Caption = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Details"].Value = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Header).VisiblePosition = (GlobalVariables.IsArabic ? (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1) : ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Details"].Index);
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
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Header).Caption = (GlobalVariables.IsArabic ? "المسلسل" : "Serial No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSling"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSling"].Header).Caption = (GlobalVariables.IsArabic ? "Sling" : "Sling");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSling"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSling"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProfitPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProfitPercentage"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الربح" : "Profit Percentage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProfitPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProfitPercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة الوحدة" : "Unit Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي التكلفة" : "Total Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManufacturingNotes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManufacturingNotes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات التصنيع" : "Manufacturing Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManufacturingNotes"].Hidden = false;
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClientContacts).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUsers).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotalCost).ReadOnly = true;
		((EditorButtonControlBase)txtTotalPrice).ReadOnly = true;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtValidityDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtProfitPercentage).ReadOnly = !Adding;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDeliveryPlace).ReadOnly = NavMode;
		((EditorButtonControlBase)txtClientOrderNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTermsOfPayment).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpExpectedDeliveryDate).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
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
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		DiscountUserID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? PreQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)txtClientOrderNo).Clear();
		((TextEditorControlBase)txtDeliveryPlace).Clear();
		((TextEditorControlBase)txtExchangeRate).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtTermsOfPayment).Clear();
		((TextEditorControlBase)txtProfitPercentage).ValueChanged -= txtProfitPercentage_ValueChanged;
		((Control)(object)txtProfitPercentage).Text = "0";
		((TextEditorControlBase)txtProfitPercentage).ValueChanged += txtProfitPercentage_ValueChanged;
		((Control)(object)txtTotalPrice).Text = "0";
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalCost).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((Control)(object)txtValidityDays).Text = "0";
		cboClient.SelectedIndex = -1;
		cboClientContacts.SelectedIndex = -1;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		dtPreQuotationsDetailsSlings.Rows.Clear();
		dtPreQuotationsDetailsSlingsMaterials.Rows.Clear();
		dtPreQuotationsDetailsSlingsMaterials = PreQuotationsDetailsSlingsMaterials.SelectByPreQuotationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtPreQuotationsDetailsSlings = PreQuotationsDetailsSlings.SelectByPreQuotationID("0", GlobalVariables.IsArabic ? "1" : "0");
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			string val = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SLN_PreQuotations_A.rpt" : "Rep_SLN_PreQuotations_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PreQuotationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLNPreQuotationsReport(-1, 0, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PreQuotationID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		object value = ((TextEditorControlBase)cboCurrency).Value;
		object value2 = ((TextEditorControlBase)cboClient).Value;
		object value3 = ((TextEditorControlBase)cboClientContacts).Value;
		object value4 = ((TextEditorControlBase)cboTransactionBranch).Value;
		object value5 = ((TextEditorControlBase)cboUsers).Value;
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "UserID", "UserName");
		dtClientContacts = SubAccountsClientSupplierContacts.FillCombo(IsFromServer: false);
		DataView dataView = new DataView(dtClientContacts);
		dataView.RowFilter = "SubAccountID = " + ((((TextEditorControlBase)cboClient).Value == null) ? "-1" : ((TextEditorControlBase)cboClient).Value.ToString());
		GlobalFunctions.FillCombo(cboClientContacts, dataView.ToTable(), "ClientSupplierContactID", "ContactName");
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
		FillCurrencyDropDown();
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		((TextEditorControlBase)cboCurrency).Value = value;
		((TextEditorControlBase)cboClient).Value = value2;
		((TextEditorControlBase)cboClientContacts).Value = value3;
		((TextEditorControlBase)cboTransactionBranch).Value = value4;
		((TextEditorControlBase)cboUsers).Value = value5;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم عرض السعر" : "Please Enter The Quotation No");
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SLN_PreQuotations", "PreQuotationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PreQuotationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = PreQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم عرض السعر متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Quotaion No. Already Exists It Will Be Saved With No. : " + codeByBranchID);
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
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSling"].Value.ToString()))
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
				if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
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
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value.ToString() == "")
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال المسلسل  ", "Please Enter Serial No. ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"];
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
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='CostOfSalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب تكلفة البضاعة المباعة من حسابات النظام  ", "Please Select Cost Of Sales Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1194: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = PreQuotations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (((TextEditorControlBase)cboClientContacts).Value == null) ? "Null" : ((TextEditorControlBase)cboClientContacts).Value.ToString(), (((Control)(object)txtClientOrderNo).Text == "") ? "Null" : ((Control)(object)txtClientOrderNo).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtProfitPercentage).Text == "" || ((Control)(object)txtProfitPercentage).Text == ".") ? "0" : ((Control)(object)txtProfitPercentage).Text, (((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtTotalCost).Text == "" || ((Control)(object)txtTotalCost).Text == ".") ? "0" : ((Control)(object)txtTotalCost).Text, ((Control)(object)txtDeliveryPlace).Text, (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtValidityDays).Text, ((Control)(object)txtTermsOfPayment).Text, ((Control)(object)txtNotes).Text, GlobalVariables.UserID, (((TextEditorControlBase)cboUsers).Value == null) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSling"].Value.ToString()))
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				}
				int num2 = PreQuotationsDetails.Insert_Update("-1", num.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["IsSling"].Value == DBNull.Value) ? "Null" : (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSling"].Value.ToString()) ? "1" : "0"), (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ManufacturingNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["ManufacturingNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["NetPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["TotalCostPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["TotalCostPrice"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				DataView dataView = new DataView(dtPreQuotationsDetailsSlings);
				dataView.RowFilter = " PreQuotationDetailID= " + ((UltraGridBase)ULGData).Rows[i].Cells["PreQuotationDetailID"].Value.ToString();
				int num3 = 0;
				if (dataView.Count > 0)
				{
					num3 = PreQuotationsDetailsSlings.Insert_Update("-1", num2.ToString(), num.ToString(), dataView[0]["SlingLegsCount"].ToString(), (dataView[0]["WireTypeID"] == DBNull.Value) ? "Null" : dataView[0]["WireTypeID"].ToString(), dataView[0]["WireLengthMt"].ToString(), dataView[0]["WireLengthFt"].ToString(), dataView[0]["FifthWireLengthMt"].ToString(), dataView[0]["FifthWireLengthFt"].ToString(), dataView[0]["Diameter"].ToString(), (dataView[0]["FirstTerminationTypeID"] == DBNull.Value) ? "Null" : dataView[0]["FirstTerminationTypeID"].ToString(), (dataView[0]["SecondTerminationTypeID"] == DBNull.Value) ? "Null" : dataView[0]["SecondTerminationTypeID"].ToString(), (dataView[0]["ShackleTypeID"] == DBNull.Value) ? "Null" : dataView[0]["ShackleTypeID"].ToString(), (dataView[0]["HookTypeID"] == DBNull.Value) ? "Null" : dataView[0]["HookTypeID"].ToString(), (dataView[0]["MasterLinkTypeID"] == DBNull.Value) ? "Null" : dataView[0]["MasterLinkTypeID"].ToString(), dataView[0]["EyeSizeFirstTerminal"].ToString(), dataView[0]["EyeSizeSecondTerminal"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				DataView dataView2 = new DataView(dtPreQuotationsDetailsSlingsMaterials);
				dataView2.RowFilter = " PreQuotationDetailID= " + ((UltraGridBase)ULGData).Rows[i].Cells["PreQuotationDetailID"].Value.ToString();
				for (int j = 0; j < dataView2.Count; j++)
				{
					PreQuotationsDetailsSlingsMaterials.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), (dataView2[j]["MaterialTypeID"] == DBNull.Value) ? "Null" : dataView2[j]["MaterialTypeID"].ToString(), (dataView2[j]["ItemID"] == DBNull.Value) ? "Null" : dataView2[j]["ItemID"].ToString(), (dataView2[j]["ColorID"] == DBNull.Value) ? "1" : dataView2[j]["ColorID"].ToString(), (dataView2[j]["ItemSizeID"] == DBNull.Value) ? "1" : dataView2[j]["ItemSizeID"].ToString(), (dataView2[j]["BatchID"] == DBNull.Value) ? "Null" : dataView2[j]["BatchID"].ToString(), (dataView2[j]["Count"] == DBNull.Value) ? "Null" : dataView2[j]["Count"].ToString(), (dataView2[j]["WireLength"] == DBNull.Value) ? "Null" : dataView2[j]["WireLength"].ToString(), (dataView2[j]["WireAllowance"] == DBNull.Value) ? "Null" : dataView2[j]["WireAllowance"].ToString(), (dataView2[j]["Qty"] == DBNull.Value) ? "Null" : dataView2[j]["Qty"].ToString(), (dataView2[j]["UnitID"] == DBNull.Value) ? "Null" : dataView2[j]["UnitID"].ToString(), (dataView2[j]["StoreID"] == DBNull.Value) ? "Null" : dataView2[j]["StoreID"].ToString(), (dataView2[j]["UnitCostPrice"] == DBNull.Value) ? "Null" : dataView2[j]["UnitCostPrice"].ToString(), (dataView2[j]["TotalCostPrice"] == DBNull.Value) ? "Null" : dataView2[j]["TotalCostPrice"].ToString(), (dataView2[j]["Notes"] == DBNull.Value) ? "Null" : dataView2[j]["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SlnMIV", "SlnMMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SlnMIV", "SlnMMIV");
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

	public override void UpdateData()
	{
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = PreQuotations.Insert_Update(drMaster["PreQuotationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), (((TextEditorControlBase)cboClientContacts).Value == null) ? "Null" : ((TextEditorControlBase)cboClientContacts).Value.ToString(), (((Control)(object)txtClientOrderNo).Text == "") ? "Null" : ((Control)(object)txtClientOrderNo).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtProfitPercentage).Text == "" || ((Control)(object)txtProfitPercentage).Text == ".") ? "0" : ((Control)(object)txtProfitPercentage).Text, (((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtTotalCost).Text == "" || ((Control)(object)txtTotalCost).Text == ".") ? "0" : ((Control)(object)txtTotalCost).Text, ((Control)(object)txtDeliveryPlace).Text, (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtValidityDays).Text, ((Control)(object)txtTermsOfPayment).Text, ((Control)(object)txtNotes).Text, drMaster["User_ID"].ToString(), (((TextEditorControlBase)cboUsers).Value == null) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), bool.Parse(drMaster["Closed"].ToString()) ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			dtPreQuotationsDetailsSlingsMaterials.AcceptChanges();
			for (int i = 0; i < dtPreQuotationsDetailsSlingsMaterials.Rows.Count; i++)
			{
				text = text + dtPreQuotationsDetailsSlingsMaterials.Rows[i]["PreQuotationDetailSlingMaterialD"].ToString() + ",";
			}
			Main.DeleteForUpdate("SLN_PreQuotationsDetailsSlingsMaterials", "PreQuotationID", drMaster["PreQuotationID"].ToString(), "PreQuotationDetailSlingMaterialD", text);
			string text2 = ",";
			for (int j = 0; j < dtPreQuotationsDetailsSlings.Rows.Count; j++)
			{
				text2 = text2 + dtPreQuotationsDetailsSlings.Rows[j]["PreQuotationDetailSlingID"].ToString() + ",";
			}
			Main.DeleteForUpdate("SLN_PreQuotationsDetailsSlings", "PreQuotationID", drMaster["PreQuotationID"].ToString(), "PreQuotationDetailSlingID", text2);
			string text3 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				text3 = text3 + ((UltraGridBase)ULGData).Rows[k].Cells["PreQuotationDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("SLN_PreQuotationsDetails", "PreQuotationID", drMaster["PreQuotationID"].ToString(), "PreQuotationDetailID", text3);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (!bool.Parse(((UltraGridBase)ULGData).Rows[l].Cells["IsSling"].Value.ToString()))
				{
					((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value);
					((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value);
				}
				int num2 = PreQuotationsDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["PreQuotationDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["PreQuotationDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[l].Cells["PreQuotationDetailID"].Value.ToString(), num.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["SerialNo"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["SerialNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["IsSling"].Value == DBNull.Value) ? "Null" : (bool.Parse(((UltraGridBase)ULGData).Rows[l].Cells["IsSling"].Value.ToString()) ? "1" : "0"), (((UltraGridBase)ULGData).Rows[l].Cells["ItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["ManufacturingNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["ManufacturingNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["Qty"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["ProfitPercentage"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["ProfitPercentage"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["UnitPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["UnitCostPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["UnitCostPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["TotalPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["Discount"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["NetPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["TotalCostPrice"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["TotalCostPrice"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				DataView dataView = new DataView(dtPreQuotationsDetailsSlings);
				dataView.RowFilter = " PreQuotationDetailID= " + ((UltraGridBase)ULGData).Rows[l].Cells["PreQuotationDetailID"].Value.ToString();
				int num3 = 0;
				if (dataView.Count > 0)
				{
					num3 = PreQuotationsDetailsSlings.Insert_Update(dataView[0]["PreQuotationDetailSlingID"].ToString(), num2.ToString(), num.ToString(), dataView[0]["SlingLegsCount"].ToString(), (dataView[0]["WireTypeID"] == DBNull.Value) ? "Null" : dataView[0]["WireTypeID"].ToString(), dataView[0]["WireLengthMt"].ToString(), dataView[0]["WireLengthFt"].ToString(), dataView[0]["FifthWireLengthMt"].ToString(), dataView[0]["FifthWireLengthFt"].ToString(), dataView[0]["Diameter"].ToString(), (dataView[0]["FirstTerminationTypeID"] == DBNull.Value) ? "Null" : dataView[0]["FirstTerminationTypeID"].ToString(), (dataView[0]["SecondTerminationTypeID"] == DBNull.Value) ? "Null" : dataView[0]["SecondTerminationTypeID"].ToString(), (dataView[0]["ShackleTypeID"] == DBNull.Value) ? "Null" : dataView[0]["ShackleTypeID"].ToString(), (dataView[0]["HookTypeID"] == DBNull.Value) ? "Null" : dataView[0]["HookTypeID"].ToString(), (dataView[0]["MasterLinkTypeID"] == DBNull.Value) ? "Null" : dataView[0]["MasterLinkTypeID"].ToString(), dataView[0]["EyeSizeFirstTerminal"].ToString(), dataView[0]["EyeSizeSecondTerminal"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				DataView dataView2 = new DataView(dtPreQuotationsDetailsSlingsMaterials);
				dataView2.RowFilter = " PreQuotationDetailID= " + ((UltraGridBase)ULGData).Rows[l].Cells["PreQuotationDetailID"].Value.ToString();
				for (int m = 0; m < dataView2.Count; m++)
				{
					PreQuotationsDetailsSlingsMaterials.Insert_Update(dataView2[m]["PreQuotationDetailSlingMaterialD"].ToString(), num3.ToString(), num2.ToString(), num.ToString(), (dataView2[m]["MaterialTypeID"] == DBNull.Value) ? "Null" : dataView2[m]["MaterialTypeID"].ToString(), (dataView2[m]["ItemID"] == DBNull.Value) ? "Null" : dataView2[m]["ItemID"].ToString(), (dataView2[m]["ColorID"] == DBNull.Value) ? "1" : dataView2[m]["ColorID"].ToString(), (dataView2[m]["ItemSizeID"] == DBNull.Value) ? "1" : dataView2[m]["ItemSizeID"].ToString(), (dataView2[m]["BatchID"] == DBNull.Value) ? "Null" : dataView2[m]["BatchID"].ToString(), (dataView2[m]["Count"] == DBNull.Value) ? "Null" : dataView2[m]["Count"].ToString(), (dataView2[m]["WireLength"] == DBNull.Value) ? "Null" : dataView2[m]["WireLength"].ToString(), (dataView2[m]["WireAllowance"] == DBNull.Value) ? "Null" : dataView2[m]["WireAllowance"].ToString(), (dataView2[m]["Qty"] == DBNull.Value) ? "Null" : dataView2[m]["Qty"].ToString(), (dataView2[m]["UnitID"] == DBNull.Value) ? "Null" : dataView2[m]["UnitID"].ToString(), (dataView2[m]["StoreID"] == DBNull.Value) ? "Null" : dataView2[m]["StoreID"].ToString(), (dataView2[m]["UnitCostPrice"] == DBNull.Value) ? "Null" : dataView2[m]["UnitCostPrice"].ToString(), (dataView2[m]["TotalCostPrice"] == DBNull.Value) ? "Null" : dataView2[m]["TotalCostPrice"].ToString(), (dataView2[m]["Notes"] == DBNull.Value) ? "Null" : dataView2[m]["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			string text4 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SlnMIV", "SlnMMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text4 != "")
			{
				GlobalVariables.InformationMB.Show(text4);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SlnMIV", "SlnMMIV");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
		}
		catch (Exception)
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
			PreQuotations.DeleteVirtual(drMaster["PreQuotationID"].ToString(), GlobalVariables.UserID);
			PreQuotationsDetails.DeleteVirtualByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.UserID);
			PreQuotationsDetailsSlings.DeleteVirtualByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.UserID);
			PreQuotationsDetailsSlingsMaterials.DeleteVirtualByPreQuotationID(drMaster["PreQuotationID"].ToString(), GlobalVariables.UserID);
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

	private void CalculateTotalPrice()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
			object obj = dataTable.Compute(" Sum(TotalPrice) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalPrice).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		}
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		CalculateNetTotals();
	}

	private void CalculateTotalCostPrice()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
			object obj = dataTable.Compute(" Sum(TotalCostPrice) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalCost).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["PreQuotationDetailID"].Value = ++newID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 1)
		{
			e.Row.Cells["SerialNo"].Value = Convert.ToInt32(((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["SerialNo"].Value) + 1;
		}
		else
		{
			e.Row.Cells["SerialNo"].Value = 1;
		}
		e.Row.Cells["ProfitPercentage"].Value = decimal.Parse((((Control)(object)txtProfitPercentage).Text == "" || ((Control)(object)txtProfitPercentage).Text == ".") ? "0" : ((Control)(object)txtProfitPercentage).Text);
		e.Row.Cells["Details"].Value = (GlobalVariables.IsArabic ? "التفاصيل" : "Details");
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode") && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSling"].Value.ToString()))
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
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
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
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9b: Expected O, but got Unknown
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = dataRow["Name"];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dataRow["DefaultStoreID"] != DBNull.Value && dtStores.Select(" StoreID = " + dataRow["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["DefaultStoreID"];
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString();
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
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
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"] != DBNull.Value)
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

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Expected O, but got Unknown
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if (((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Notes")
				{
					frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "الوصف" : "Description", _IsInt: false, _IsNumeric: false, ULGData.ActiveCell.Value.ToString());
					frmEnterValue2.WindowState = FormWindowState.Normal;
					if (frmEnterValue2.ShowDialog() == DialogResult.OK)
					{
						ULGData.ActiveCell.Value = frmEnterValue2.Value;
					}
				}
				else if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value != DBNull.Value)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					frmGetItemAllowedQty frmGetItemAllowedQty2 = new frmGetItemAllowedQty(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
					frmGetItemAllowedQty2.WindowState = FormWindowState.Normal;
					frmGetItemAllowedQty2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = ((frmGetItemAllowedQty2.ColorID > 0) ? ((object)frmGetItemAllowedQty2.ColorID) : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = ((frmGetItemAllowedQty2.ItemSizeID > 0) ? ((object)frmGetItemAllowedQty2.ItemSizeID) : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = ((frmGetItemAllowedQty2.BatchID > 0) ? ((object)frmGetItemAllowedQty2.BatchID) : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value = ((frmGetItemAllowedQty2.Avg > 0m) ? ((object)frmGetItemAllowedQty2.Avg) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
					{
						UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
						obj.Value = value;
					}
					else
					{
						decimal num = (((((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * num / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					}
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					CalculateTotalCostPrice();
					CalculateTotalPrice();
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
			if ((Adding || Updating) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num2 = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
				if (num2 != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num2;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num2;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ProfitPercentage")
			{
				if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
				{
					UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj.Value = value;
				}
				else
				{
					decimal num = (((((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * num / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
				CalculateTotalCostPrice();
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
				CalculateTotalCostPrice();
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
				CalculateTotalCostPrice();
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
				{
					UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj3.Value = value;
				}
				else
				{
					decimal num2 = (((((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * num2 / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
				CalculateTotalCostPrice();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
				{
					UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj5.Value = value;
				}
				else
				{
					decimal num3 = (((((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * num3 / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				}
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPrice();
				CalculateTotalCostPrice();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsSling" && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSling"].Value.ToString()))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = "";
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		ArDetailIDs.Clear();
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (e.Rows[i].Cells["PreQuotationDetailID"].Value != DBNull.Value)
			{
				ArDetailIDs.Add(e.Rows[i].Cells["PreQuotationDetailID"].Value);
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterRowsDeleted -= ULGData_AfterRowsDeleted;
		((UltraGridBase)ULGData).UpdateData();
		for (int i = 0; i < dtPreQuotationsDetailsSlings.Rows.Count; i++)
		{
			if (dtPreQuotationsDetailsSlings.Rows[i]["PreQuotationDetailID"] != DBNull.Value && ArDetailIDs.Contains(dtPreQuotationsDetailsSlings.Rows[i]["PreQuotationDetailID"]))
			{
				dtPreQuotationsDetailsSlings.Rows[i].Delete();
				dtPreQuotationsDetailsSlings.Rows[i].AcceptChanges();
				i--;
			}
		}
		for (int j = 0; j < dtPreQuotationsDetailsSlingsMaterials.Rows.Count; j++)
		{
			if (dtPreQuotationsDetailsSlingsMaterials.Rows[j]["PreQuotationDetailID"] != DBNull.Value && ArDetailIDs.Contains(dtPreQuotationsDetailsSlingsMaterials.Rows[j]["PreQuotationDetailID"]))
			{
				dtPreQuotationsDetailsSlingsMaterials.Rows[j].Delete();
				dtPreQuotationsDetailsSlingsMaterials.Rows[j].AcceptChanges();
				j--;
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[k]);
		}
		CalcTotalQty();
		CalculateTotalPrice();
		CalculateTotalCostPrice();
		ULGData.AfterRowsDeleted += ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Expected O, but got Unknown
		if (ULGData.ActiveCell == null || ((UltraGridBase)ULGData).ActiveRow == null || !bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSling"].Value.ToString()) || !(((KeyedSubObjectBase)e.Cell.Column).Key == "Details"))
		{
			return;
		}
		frmSLNPreQuotationsSlings frmSLNPreQuotationsSlings2 = new frmSLNPreQuotationsSlings(Adding ? (-1) : int.Parse(drMaster["PreQuotationID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PreQuotationDetailID"].Value.ToString()), decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()), ((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value.ToString(), !Adding && !Updating, dtpDate.DateTime, dtPreQuotationsDetailsSlings, dtPreQuotationsDetailsSlingsMaterials);
		frmSLNPreQuotationsSlings2.WindowState = FormWindowState.Maximized;
		((Control)(object)frmSLNPreQuotationsSlings2.lblTitle).Text = (GlobalVariables.IsArabic ? "Sling Manufacturing" : "Sling Manufacturing");
		DialogResult dialogResult = frmSLNPreQuotationsSlings2.ShowDialog();
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmSLNPreQuotationsSlings2.TotalQty;
		((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = frmSLNPreQuotationsSlings2.SlingDesc;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		DataView dataView = new DataView(dtPreQuotationsDetailsSlingsMaterials);
		dataView.RowFilter = " PreQuotationDetailID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["PreQuotationDetailID"].Value.ToString();
		object obj = dataView.ToTable().Compute(" Sum(TotalCostPrice) ", "");
		if (frmSLNPreQuotationsSlings2.Saved && obj != DBNull.Value)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value = obj.ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value = ((frmSLNPreQuotationsSlings2.TotalQty == 0m) ? 0m : (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString())));
			if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
			{
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = 0);
				obj2.Value = value;
			}
			else
			{
				decimal num = (((((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * num / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalPrice();
			CalculateTotalCostPrice();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
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

	private void txtTotalPrice_ValueChanged(object sender, EventArgs e)
	{
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
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = PreQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void txtValidityDays_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
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

	private void txtProfitPercentage_ValueChanged(object sender, EventArgs e)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		if (!Adding)
		{
			return;
		}
		((TextEditorControlBase)txtProfitPercentage).ValueChanged -= txtProfitPercentage_ValueChanged;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		decimal num = (((((Control)(object)txtProfitPercentage).Text == "" || ((Control)(object)txtProfitPercentage).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtProfitPercentage).Text)) + 100m) / 100m;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value = ((Control)(object)txtProfitPercentage).Text;
			if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
			{
				UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
				obj.Value = value;
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value.ToString()) * num / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalCostPrice();
		CalculateTotalPrice();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((TextEditorControlBase)txtProfitPercentage).ValueChanged += txtProfitPercentage_ValueChanged;
	}

	private void txtExchangeRate_ValueChanged(object sender, EventArgs e)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Expected O, but got Unknown
		if (!(((Control)(object)txtExchangeRate).Text != ""))
		{
			return;
		}
		((TextEditorControlBase)txtExchangeRate).ValueChanged -= txtExchangeRate_ValueChanged;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (decimal.Parse((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text) == 0m)
			{
				UltraGridCell obj = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				object value = (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = 0);
				obj.Value = value;
			}
			else
			{
				decimal num = (((((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value == null || ((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value.ToString() == "") ? 0m : decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ProfitPercentage"].Value.ToString())) + 100m) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value.ToString()) * num / ((((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtExchangeRate).Text));
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalCostPrice();
		CalculateTotalPrice();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((TextEditorControlBase)txtExchangeRate).ValueChanged += txtExchangeRate_ValueChanged;
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClientContacts);
			dataView.RowFilter = "SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString();
			GlobalFunctions.FillCombo(cboClientContacts, dataView.ToTable(), "ClientSupplierContactID", "ContactName");
			cboClientContacts.SelectedIndex = -1;
		}
	}

	public void OpenChangeDiscountForm()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse(((Control)(object)txtTotalPrice).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscBeforeTaxValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Close();
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "0" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateNetTotals();
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
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateNetTotals();
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

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtTotalPrice).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtTotalPrice).Text != ".")
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.Cells["ItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Expected O, but got Unknown
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Expected O, but got Unknown
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Expected O, but got Unknown
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Transactions.frmSLNPreQuotations));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblTotalPrice = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.lblTotalCost = new UltraLabel();
		this.txtTotalCost = new UltraTextEditor();
		this.btnClientSearch = new UltraButton();
		this.lblClient = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.ultraLabel6 = new UltraLabel();
		this.txtProfitPercentage = new UltraTextEditor();
		this.lblProfitPercentage = new UltraLabel();
		this.lblClientOrderNo = new UltraLabel();
		this.txtClientOrderNo = new UltraTextEditor();
		this.txtDeliveryPlace = new UltraTextEditor();
		this.lblDeliveryPlace = new UltraLabel();
		this.dtpExpectedDeliveryDate = new UltraDateTimeEditor();
		this.lblExpectedDeliveryDate = new UltraLabel();
		this.lblValidityDays = new UltraLabel();
		this.txtValidityDays = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.lblUser = new UltraLabel();
		this.cboUsers = new UltraComboEditor();
		this.lblTermsOfPayment = new UltraLabel();
		this.txtTermsOfPayment = new UltraTextEditor();
		this.lblClientContact = new UltraLabel();
		this.cboClientContacts = new UltraComboEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientOrderNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTermsOfPayment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientContacts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
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
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
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
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).TabStop = false;
		((TextEditorControlBase)this.txtTotalPrice).ValueChanged += new System.EventHandler(txtTotalPrice_ValueChanged);
		resources.ApplyResources(this.lblTotalCost, "lblTotalCost");
		this.lblTotalCost.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalCost).Name = "lblTotalCost";
		((ControlBase)this.lblTotalCost).WrapText = false;
		resources.ApplyResources(this.txtTotalCost, "txtTotalCost");
		((System.Windows.Forms.Control)(object)this.txtTotalCost).Name = "txtTotalCost";
		((EditorButtonControlBase)this.txtTotalCost).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalCost).TabStop = false;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboClient, "cboClient");
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
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
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((TextEditorControlBase)this.txtExchangeRate).ValueChanged += new System.EventHandler(txtExchangeRate_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.txtProfitPercentage, "txtProfitPercentage");
		((System.Windows.Forms.Control)(object)this.txtProfitPercentage).Name = "txtProfitPercentage";
		((TextEditorControlBase)this.txtProfitPercentage).ValueChanged += new System.EventHandler(txtProfitPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtProfitPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblProfitPercentage, "lblProfitPercentage");
		this.lblProfitPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProfitPercentage).Name = "lblProfitPercentage";
		((ControlBase)this.lblProfitPercentage).WrapText = false;
		resources.ApplyResources(this.lblClientOrderNo, "lblClientOrderNo");
		this.lblClientOrderNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientOrderNo).Name = "lblClientOrderNo";
		((ControlBase)this.lblClientOrderNo).WrapText = false;
		resources.ApplyResources(this.txtClientOrderNo, "txtClientOrderNo");
		((System.Windows.Forms.Control)(object)this.txtClientOrderNo).Name = "txtClientOrderNo";
		resources.ApplyResources(this.txtDeliveryPlace, "txtDeliveryPlace");
		((System.Windows.Forms.Control)(object)this.txtDeliveryPlace).Name = "txtDeliveryPlace";
		resources.ApplyResources(this.lblDeliveryPlace, "lblDeliveryPlace");
		this.lblDeliveryPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryPlace).Name = "lblDeliveryPlace";
		((ControlBase)this.lblDeliveryPlace).WrapText = false;
		resources.ApplyResources(this.dtpExpectedDeliveryDate, "dtpExpectedDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpExpectedDeliveryDate).AlwaysInEditMode = true;
		this.dtpExpectedDeliveryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate).Name = "dtpExpectedDeliveryDate";
		resources.ApplyResources(this.lblExpectedDeliveryDate, "lblExpectedDeliveryDate");
		this.lblExpectedDeliveryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate).Name = "lblExpectedDeliveryDate";
		((ControlBase)this.lblExpectedDeliveryDate).WrapText = false;
		resources.ApplyResources(this.lblValidityDays, "lblValidityDays");
		this.lblValidityDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValidityDays).Name = "lblValidityDays";
		((ControlBase)this.lblValidityDays).WrapText = false;
		resources.ApplyResources(this.txtValidityDays, "txtValidityDays");
		((System.Windows.Forms.Control)(object)this.txtValidityDays).Name = "txtValidityDays";
		((System.Windows.Forms.Control)(object)this.txtValidityDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtValidityDays_KeyPress);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.lblUser, "lblUser");
		this.lblUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUser).Name = "lblUser";
		((ControlBase)this.lblUser).WrapText = false;
		resources.ApplyResources(this.cboUsers, "cboUsers");
		this.cboUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUsers).Name = "cboUsers";
		resources.ApplyResources(this.lblTermsOfPayment, "lblTermsOfPayment");
		this.lblTermsOfPayment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTermsOfPayment).Name = "lblTermsOfPayment";
		((ControlBase)this.lblTermsOfPayment).WrapText = false;
		resources.ApplyResources(this.txtTermsOfPayment, "txtTermsOfPayment");
		((System.Windows.Forms.Control)(object)this.txtTermsOfPayment).Name = "txtTermsOfPayment";
		resources.ApplyResources(this.lblClientContact, "lblClientContact");
		this.lblClientContact.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientContact).Name = "lblClientContact";
		((ControlBase)this.lblClientContact).WrapText = false;
		resources.ApplyResources(this.cboClientContacts, "cboClientContacts");
		this.cboClientContacts.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClientContacts).Name = "cboClientContacts";
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
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientContact);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientContacts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTermsOfPayment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTermsOfPayment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValidityDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValidityDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtProfitPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProfitPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalCost);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalCost);
		base.Name = "frmSLNPreQuotations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalCost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalCost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProfitPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtProfitPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValidityDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValidityDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTermsOfPayment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTermsOfPayment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientContacts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientContact, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientOrderNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTermsOfPayment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientContacts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
