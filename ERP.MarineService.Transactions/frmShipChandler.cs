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
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.StockControl.Transactions;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmShipChandler : frmHeaderDetails
{
	private DataTable dtOperationItems;

	private DataTable dtOperationsNo;

	private DataTable dtSubAccounts;

	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtItemPrices;

	private DataTable dtCurrency;

	private DataTable dtTaxs;

	private DataTable dtMinAllowedTransDate;

	private ValueList vlItems = new ValueList();

	private ValueList vlItemsCopy = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private string OperationID = "0";

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool UsingSalesDiscountLevels = false;

	private IContainer components = null;

	private UltraLabel lblCommercialTax;

	private UltraTextEditor txtCommercialTax;

	private UltraLabel lblDiscAfterTaxRatio;

	private UltraTextEditor txtDiscAfterTaxRatio;

	private UltraLabel lblDiscAfterTaxValue;

	private UltraTextEditor txtDiscAfterTaxValue;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboTax;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraCheckEditor chkTax;

	private UltraTextEditor txtTotalQty;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblOnAccountOF;

	private UltraComboEditor cboOnAccountOF;

	private UltraLabel lblExchangeRate;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	public UltraButton btnClear;

	public frmShipChandler()
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
		TableName = "MS_ShipChandler";
		IDCol = "ShipChandlerID";
		NoCol = "ShipChandlerNo";
		DateCol = "ShipChandlerDate";
	}

	public frmShipChandler(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmShipChandler(string OPERATIONID)
		: this()
	{
		OperationID = OPERATIONID;
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
		dtOperationsNo = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		vlItemsCopy.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlItemsCopy.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = ShipChandlerDetails.SelectByShipChandlerID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (OperationID != "0")
		{
			drMaster = null;
			btnAddClick();
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = ShipChandler.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ShipChandlerNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["ShipChandlerDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
			((TextEditorControlBase)cboOnAccountOF).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
			((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ShipChandlerDetails.SelectByShipChandlerID(drMaster["ShipChandlerID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			CalculateGross();
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipChandlerDetailID"].DefaultCellValue = -1;
		if (UsingSizes)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		}
		if (UsingColors)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		}
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		((EditorButtonControlBase)cboOnAccountOF).ReadOnly = NavMode || Updating;
		((Control)(object)btnOperationNoSearch).Visible = Adding;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		DiscountUserID = 0;
		((Control)(object)btnClear).Visible = Adding;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
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
			if (num != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num;
			}
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
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
		if (Adding || Updating)
		{
			DataView dataView2 = new DataView(dtStores);
			dataView2.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView2.ToTable();
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
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView3.ToTable();
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
		DiscountUserID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? ShipChandler.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
		cboOnAccountOF.SelectedIndex = -1;
		((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((Control)(object)txtExchangeRate).Text = "0";
		((Control)(object)txtGrossValue).Text = "0";
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((Control)(object)txtDiscAfterTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtCommercialTax).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
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

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_ShipChandler_A.rpt" : "Rep_MS_ShipChandler_E.rpt"));
			}
			frmCompanySelector frmCompanySelector2 = new frmCompanySelector();
			frmCompanySelector2.WindowState = FormWindowState.Normal;
			frmCompanySelector2.ShowDialog();
			if (!frmCompanySelector2.Cancel && frmCompanySelector2.CompanyID != 0)
			{
				frmReporViwer frmReporViwer2 = new frmReporViwer();
				GlobalVariables.ReportDocument.SetParameterValue("@ShipChandlerIDs", "," + RowID + ",");
				GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				frmReporViwer2.ShowDialog();
				frmReporViwer2 = null;
			}
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ShipChandlerSearchReport("-1", -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ShipChandlerID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		((TextEditorControlBase)cboOnAccountOF).ValueChanged -= cboOnAccountOF_ValueChanged;
		object value = ((TextEditorControlBase)cboOperationNo).Value;
		object value2 = ((TextEditorControlBase)cboOnAccountOF).Value;
		object value3 = ((TextEditorControlBase)cboCurrency).Value;
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
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxs.Rows.Count; num2++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num2]["TaxID"], dtTaxs.Rows[num2]["TaxName"].ToString());
		}
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		((TextEditorControlBase)cboOperationNo).Value = value;
		((TextEditorControlBase)cboOnAccountOF).Value = value2;
		((TextEditorControlBase)cboCurrency).Value = value3;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((TextEditorControlBase)cboOnAccountOF).ValueChanged += cboOnAccountOF_ValueChanged;
	}

	public override bool ValidateData()
	{
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Expected O, but got Unknown
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Expected O, but got Unknown
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
		((UltraGridBase)ULGData).UpdateData();
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select(" Qty =0 ").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("يوجد أصناف كميتها بصفر هل تريد الحذف؟ ", "There Are Items Quantity Equal Zero Are you Sure To Delete?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				for (int num = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1; num >= 0; num--)
				{
					if (((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value.ToString()) == 0m)
					{
						((UltraGridBase)ULGData).Rows[num].Delete(false);
					}
				}
				((UltraGridBase)ULGData).UpdateData();
				ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		DataTable dataTable2 = OperationsItems.Diff((DataTable)((UltraGridBase)ULGData).DataSource, Adding ? "-1" : drMaster["ShipChandlerID"].ToString(), ((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dataTable2.Rows.Count > 0)
		{
			frmAuditorState frmAuditorState2 = new frmAuditorState(dataTable2, vlItemsCopy);
			frmAuditorState2.WindowState = FormWindowState.Normal;
			frmAuditorState2.ShowDialog();
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				if (int.Parse(dataTable2.Rows[i]["Flag"].ToString()) == 2 || (int.Parse(dataTable2.Rows[i]["Flag"].ToString()) == 1 && decimal.Parse(dataTable2.Rows[i]["Diff"].ToString()) > 0m))
				{
					return false;
				}
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingColors && (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"];
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"];
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["MaxQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["MaxQty"].Value.ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  لايمكن زيادة الكمية عن الكمية المطلوبة و قدرها " + Math.Round(decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["MaxQty"].Value.ToString()), 3)) : (" Qty Cannot be More than  MaxQty " + decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["MaxQty"].Value.ToString())));
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع قبل أخر إعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n  على مخزن   " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Text, string.Concat("\n  The Date you choosed Before Last Store Taking With Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Text));
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_ShipChandler", "ShipChandlerNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ShipChandlerNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ShipChandler.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			CalculateGross();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			num = ShipChandler.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), "Null", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ShipChandlerID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ShipChandlerDetailID"].Value = -1;
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
			ShipChandlerDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			OperationsItems.UpdateDeliverdQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			ShipChandler.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SHPMIV", "SHPMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SHPMIV", "SHPMIV");
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
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			CalculateGross();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = ShipChandler.Insert_Update(drMaster["ShipChandlerID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (drMaster["OperationInvoiceID"] == DBNull.Value) ? "Null" : drMaster["OperationInvoiceID"].ToString(), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ShipChandlerID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[j].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[j].Cells["ShipChandlerDetailID"].Value.ToString() + ",";
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("MS_ShipChandlerDetails", "ShipChandlerID", drMaster["ShipChandlerID"].ToString(), "ShipChandlerDetailID", text);
			ShipChandlerDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			OperationsItems.UpdateDeliverdQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			ShipChandler.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SHPMIV", "SHPMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SHPMIV", "SHPMIV");
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
			ShipChandler.DeleteVirtual(drMaster["ShipChandlerID"].ToString(), GlobalVariables.UserID);
			ShipChandlerDetails.DeleteVirtualByShipChandlerID(drMaster["ShipChandlerID"].ToString(), GlobalVariables.UserID);
			OperationsItems.UpdateDeliverdQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JVDetails.DeleteVirtualByJVID(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JVDetails.DeleteVirtualByJVID(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["ShipChandlerID"].ToString(), "SHPMIV", "SHPMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["ShipChandlerID"].ToString(), "SHPMIV", "SHPMIV");
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

	private void CalculateGross()
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
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != ".")
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
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxRatio).Text == "" || ((Control)(object)txtDiscAfterTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text) / 100m * (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * -decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				result += 0m;
				continue;
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		return result;
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
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
		if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ReturnedQty"].Value.ToString()) > 0m)
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
		//IL_0c3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Expected O, but got Unknown
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c60: Expected O, but got Unknown
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
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.ItemCount > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Column.ValueList.GetValue(0) : DBNull.Value);
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboOnAccountOF.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGross();
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
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected O, but got Unknown
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
					if (((UltraGridBase)ULGData).ActiveRow.Index != i && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() == val.Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString() == val.Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString() == val.Cells["ItemSizeID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString() == val.Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString() == val.Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString() == val.Cells["UnitID"].Value.ToString())
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
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		if (ULGData.ActiveCell != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
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
				CalculateGross();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGross();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
				CalculateGross();
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
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGross();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((cboOnAccountOF.SelectedIndex > -1 && decimal.Parse(dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOnAccountOF).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOnAccountOF).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
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
			if (cboOnAccountOF.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount)
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
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (cboOnAccountOF.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount)
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

	private void txtDiscAfterTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && decimal.Parse((((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (Adding)
		{
			((Control)(object)txtCode).Text = ShipChandler.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			DataTable dataTable = ShipChandler.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (cboOperationNo.SelectedIndex > -1)
			{
				((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
				dtDetails.Rows.Clear();
				CalculateGross();
				DataTable dt = OperationsInvoices.SelectSubAccountsFillCombo(((TextEditorControlBase)cboOperationNo).Value.ToString(), Adding ? "NULL" : "-1", GlobalVariables.IsArabic ? "1" : "0");
				GlobalFunctions.FillCombo(cboOnAccountOF, dt, "SubAccountID", "Name");
				((TextEditorControlBase)cboCurrency).Value = dtOperationsNo.Select(" OperationID= " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0]["CurrencyID"];
				((TextEditorControlBase)txtExchangeRate).Value = dtOperationsNo.Select(" OperationID= " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0]["ExchangeRate"];
				((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			}
			else
			{
				dtDetails.Rows.Clear();
				GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
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
			if (drMaster["OperationInvoiceID"].ToString() != "")
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة", "Cannot Update This Transaction Because There Is an Invoice For It");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
		if (ShipChandlerReturns.SelectByShipChandlerID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود مرتجع عليها", "Cannot Delete This Transaction Because there Are Return on it. ");
			return;
		}
		if (drMaster["OperationInvoiceID"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة", "Cannot Delete This Transaction Because There Is an Invoice For It");
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

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
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
		if (cboOnAccountOF.SelectedIndex <= -1 || cboOperationNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		dtOperationItems = ShipChandlerDetails.FillByOperationSubAccountID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtOperationItems;
		if (UsingBatchNoAndValidityPeriod)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()));
				((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value = DBNull.Value;
				}
			}
		}
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		InitGrid();
		CalculateGross();
		CalculateTotalsTax();
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

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
	}

	public void AddItemInGid()
	{
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Expected O, but got Unknown
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
				((UltraGridBase)ULGData).Rows[i].Cells["MaxQty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateGross();
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
		object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
		obj.Value = value;
		DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (dtOperationItems.Rows.Count == 0)
		{
			dtOperationItems = ShipChandlerDetails.FillByOperationSubAccountID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		}
		if (dtOperationItems.Rows.Count > 0)
		{
			DataRow[] array = dtOperationItems.Select("ItemID = " + dataRow["ItemID"].ToString());
			if (array.Length != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["unitPrice"].Value = array[0]["UnitPrice"];
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = array[0]["TaxID"];
				CalculateGross();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalsTax();
			}
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
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((Control)(object)txtDiscAfterTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtCommercialTax).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
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
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Expected O, but got Unknown
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmShipChandler));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblCommercialTax = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.lblDiscAfterTaxRatio = new UltraLabel();
		this.txtDiscAfterTaxRatio = new UltraTextEditor();
		this.lblDiscAfterTaxValue = new UltraLabel();
		this.txtDiscAfterTaxValue = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboTax = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.chkTax = new UltraCheckEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.lblOnAccountOF = new UltraLabel();
		this.cboOnAccountOF = new UltraComboEditor();
		this.lblExchangeRate = new UltraLabel();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.btnClear = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
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
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		this.lblCommercialTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		((ControlBase)this.lblCommercialTax).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
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
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtGrossValue).TabStop = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtNetprice).TabStop = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
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
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		resources.ApplyResources(this.cboStore, "cboStore");
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.lblOperationsNo, "lblOperationsNo");
		this.lblOperationsNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperationsNo).Name = "lblOperationsNo";
		((ControlBase)this.lblOperationsNo).WrapText = false;
		resources.ApplyResources(this.cboOperationNo, "cboOperationNo");
		((TextEditorControlBase)this.cboOperationNo).AlwaysInEditMode = true;
		this.cboOperationNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOperationNo).Name = "cboOperationNo";
		((TextEditorControlBase)this.cboOperationNo).ValueChanged += new System.EventHandler(cboOperationNo_ValueChanged);
		resources.ApplyResources(this.btnOperationNoSearch, "btnOperationNoSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.lblOnAccountOF, "lblOnAccountOF");
		this.lblOnAccountOF.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOnAccountOF).Name = "lblOnAccountOF";
		((ControlBase)this.lblOnAccountOF).WrapText = false;
		resources.ApplyResources(this.cboOnAccountOF, "cboOnAccountOF");
		((TextEditorControlBase)this.cboOnAccountOF).AlwaysInEditMode = true;
		this.cboOnAccountOF.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOnAccountOF).Name = "cboOnAccountOF";
		((TextEditorControlBase)this.cboOnAccountOF).ValueChanged += new System.EventHandler(cboOnAccountOF_ValueChanged);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnAccountOF);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOnAccountOF);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Name = "frmShipChandler";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOnAccountOF, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnAccountOF, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
