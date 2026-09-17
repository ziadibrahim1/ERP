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
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmShipChandlerReturns : frmHeaderDetails
{
	private DataTable dtStores;

	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtOperations;

	private DataTable dtSubAccounts;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtUnits;

	private DataTable dtBatchs;

	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtSalesInvoices;

	private DataTable dtShipChandlers;

	private DataTable dtDepartments;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtTaxs;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboOnAccountOF;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblClient;

	private UltraLabel lblShipChandlerNo;

	private UltraComboEditor cboShipChandlersNo;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnShipChandlersSearch;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public UltraButton btnJV;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	public frmShipChandlerReturns()
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
		TableName = "MS_ShipChandlerReturns";
		IDCol = "ShipChandlerReturnID";
		NoCol = "ShipChandlerReturnNo";
		DateCol = "ShipChandlerReturnDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public frmShipChandlerReturns(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
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
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
		}
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
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
		dtShipChandlers = ShipChandler.FillCombo(GlobalVariables.BranchIDs);
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = ShipChandlerReturnsDetails.SelectByShipChandlerReturnID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipChandlerReturnDetailID"].DefaultCellValue = -1;
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصى" : "Max limit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة" : "Tax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Amount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ShipChandlerReturns.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboShipChandlersNo).ValueChanged -= cboShipChandlersNo_ValueChanged;
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ShipChandlerReturnNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ShipChandlerReturnDate"];
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboShipChandlersNo).Value = drMaster["ShipChandlerID"];
			((TextEditorControlBase)cboOnAccountOF).Value = drMaster["SubAccountID"];
			UltraLabel obj = lblTotal;
			bool visible = (((Control)(object)txtTotal).Visible = true);
			((Control)(object)obj).Visible = visible;
			((TextEditorControlBase)cboCurrency).Value = dtShipChandlers.Select("ShipChandlerID = " + drMaster["ShipChandlerID"])[0]["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtShipChandlers.Select("ShipChandlerID = " + drMaster["ShipChandlerID"])[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ShipChandlerReturnsDetails.SelectByShipChandlerReturnID(drMaster["ShipChandlerReturnID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			((TextEditorControlBase)cboShipChandlersNo).ValueChanged += cboShipChandlersNo_ValueChanged;
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
		((EditorButtonControlBase)cboShipChandlersNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode;
		((Control)(object)btnOperationNoSearch).Visible = !NavMode;
		((Control)(object)btnShipChandlersSearch).Visible = !NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged -= cboShipChandlersNo_ValueChanged;
		int num = 0;
		if (cboShipChandlersNo.SelectedIndex > -1)
		{
			num = int.Parse(((TextEditorControlBase)cboShipChandlersNo).Value.ToString());
		}
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtShipChandlers);
			if (cboOperationNo.SelectedIndex > -1)
			{
				dataView.RowFilter = " OperationID = " + ((TextEditorControlBase)cboOperationNo).Value.ToString();
			}
			else
			{
				dataView.RowFilter = " OperationID = -1";
			}
			GlobalFunctions.FillCombo(cboShipChandlersNo, dataView.ToTable(), "ShipChandlerID", "ShipChandlerNo");
			DataView dataView2 = new DataView(dtStores);
			dataView2.RowFilter = " Locked =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView2.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboShipChandlersNo, dtShipChandlers, "ShipChandlerID", "ShipChandlerNo");
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		if (num > 0)
		{
			((TextEditorControlBase)cboShipChandlersNo).Value = num;
		}
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged += cboShipChandlersNo_ValueChanged;
		if (Adding)
		{
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView3.ToTable();
			vlItems.ValueListItems.Clear();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)(Adding ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? ShipChandlerReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		cboOperationNo.SelectedIndex = -1;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged -= cboShipChandlersNo_ValueChanged;
		cboShipChandlersNo.SelectedIndex = -1;
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged += cboShipChandlersNo_ValueChanged;
		cboOnAccountOF.SelectedIndex = -1;
		cboCurrency.SelectedIndex = -1;
		((Control)(object)txtExchangeRate).Text = "0";
		((Control)(object)txtTotal).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		dtDetails.Rows.Clear();
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
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
		if (cboOperationNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم العملية" : "Please Select Operation No");
			cboOperationNo.DropDown();
			return false;
		}
		if (cboShipChandlersNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم إذن الصرف" : "Please Select ShipChandler No");
			cboShipChandlersNo.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_ShipChandlerReturns", "ShipChandlerReturnNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ShipChandlerReturnNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ShipChandlerReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				return false;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع قبل أخر إعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n  على مخزن   " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n  The Date you choosed Before Last Store Taking With Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesReturnsAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب مردودات المبيعات من حسابات النظام  ", "Please Select Sales Returns Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ShipChandlerReturns.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), "Null", (cboShipChandlersNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShipChandlersNo).Value.ToString(), (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ShipChandlerReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ShipChandlerReturnDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ShipChandlerReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			OperationsItems.UpdateReturnedQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			ShipChandlerReturns.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ShipChandlerReturns.Insert_Update(drMaster["ShipChandlerReturnID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (drMaster["OperationInvoiceID"] == DBNull.Value) ? "Null" : drMaster["OperationInvoiceID"].ToString(), (cboShipChandlersNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShipChandlersNo).Value.ToString(), (cboOnAccountOF.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RSHPMIV", "RSHPMIV");
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ShipChandlerReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ShipChandlerReturnDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("MS_ShipChandlerReturnsDetails", "ShipChandlerReturnID", drMaster["ShipChandlerReturnID"].ToString(), "ShipChandlerReturnDetailID", text);
			ShipChandlerReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			OperationsItems.UpdateReturnedQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "RSHPMIV", "RSHPMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RSHPMIV", "RSHPMIV");
			}
			else
			{
				ShipChandlerReturns.GenerateJvs("," + num + ",", GlobalVariables.UserID);
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			MessageLog.DeleteByVoucherIDAndTransType(drMaster["ShipChandlerReturnID"].ToString(), "RSHPMIV", "RSHPMIV");
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JVDetails.DeleteVirtualByJVID(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JVDetails.DeleteVirtualByJVID(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			ShipChandlerReturns.DeleteVirtual(drMaster["ShipChandlerReturnID"].ToString(), GlobalVariables.UserID);
			ShipChandlerReturnsDetails.DeleteVirtualByShipChandlerReturnID(drMaster["ShipChandlerReturnID"].ToString(), GlobalVariables.UserID);
			OperationsItems.UpdateReturnedQty(((TextEditorControlBase)cboOperationNo).Value.ToString(), ((TextEditorControlBase)cboOnAccountOF).Value.ToString(), GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["ShipChandlerReturnID"].ToString(), "RSHPMIV", "RSHPMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["ShipChandlerReturnID"].ToString(), "RSHPMIV", "RSHPMIV");
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_ShipChandlerReturns_A.rpt" : "Rep_MS_ShipChandlerReturns_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ShipChandlerReturnIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ShipChandlerReturnsSearchReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ShipChandlerReturnID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged -= cboShipChandlersNo_ValueChanged;
		object value = ((TextEditorControlBase)cboOperationNo).Value;
		object value2 = ((TextEditorControlBase)cboShipChandlersNo).Value;
		object value3 = ((TextEditorControlBase)cboOnAccountOF).Value;
		object value4 = ((TextEditorControlBase)cboCurrency).Value;
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
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
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtShipChandlers = ShipChandler.FillCombo(GlobalVariables.BranchIDs);
		DataView dataView = new DataView(dtShipChandlers);
		if (cboOperationNo.SelectedIndex > -1)
		{
			dataView.RowFilter = " OperationID = " + ((TextEditorControlBase)cboOperationNo).Value.ToString();
		}
		else
		{
			dataView.RowFilter = " OperationID = -1";
		}
		GlobalFunctions.FillCombo(cboShipChandlersNo, dataView.ToTable(), "ShipChandlerID", "ShipChandlerNo");
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
			for (int m = 0; m < dataTable2.Rows.Count; m++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["Name"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			for (int n = 0; n < dtItems.Rows.Count; n++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOnAccountOF, dtSubAccounts, "SubAccountID", "Name");
		((TextEditorControlBase)cboOperationNo).Value = value;
		((TextEditorControlBase)cboShipChandlersNo).Value = value2;
		((TextEditorControlBase)cboOnAccountOF).Value = value3;
		((TextEditorControlBase)cboCurrency).Value = value4;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		((TextEditorControlBase)cboShipChandlersNo).ValueChanged += cboShipChandlersNo_ValueChanged;
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
		if (drMaster != null && drMaster["OperationInvoiceID"] != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة", "Cannot Update This Transaction Because There Is an Invoice For It");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(dtStores.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString())[0]["Locked"].ToString()))
			{
				GlobalVariables.InformationMB.Show(" لايمكن تعديل هذه الحركة لوجود المخزن \n" + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " مغلق ", "Cannot Update This Transaction Because Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " Locked ");
				return;
			}
			DataTable minAllowedTransDateByStoreIDs = Stores.GetMinAllowedTransDateByStoreIDs("," + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + ",", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
			if (minAllowedTransDateByStoreIDs.Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show(" لا يمكن تعديل هذه الحركة لوجود جرد او اعادة تقييم بتاريخ \n " + minAllowedTransDateByStoreIDs.Rows[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), "\n Cannot Update This Transaction Because Store Taking Or Store Revaluation on Date" + minAllowedTransDateByStoreIDs.Rows[0]["Date"].ToString() + " \n  On Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString());
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
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n Cannot Delete This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return;
			}
		}
		if (drMaster["OperationInvoiceID"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة", "Cannot Delete This Transaction Because There Is an Invoice For It");
			return;
		}
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: true);
				frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
				frmQuantityMultiUnit2.ShowDialog();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
			}
			e.Handled = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show(" الحد الاقصى للكمية المرتجعة " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString(), " Max Allowed Returned Quantity " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString());
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			CalculateTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalDiscount"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value.ToString()));
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void cboShipChandlersNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboShipChandlersNo.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboOnAccountOF).Value = dtShipChandlers.Select("ShipChandlerID = " + ((TextEditorControlBase)cboShipChandlersNo).Value.ToString())[0]["SubAccountID"];
			((TextEditorControlBase)cboCurrency).Value = dtShipChandlers.Select("ShipChandlerID = " + ((TextEditorControlBase)cboShipChandlersNo).Value.ToString())[0]["CurrencyID"];
			((TextEditorControlBase)txtExchangeRate).Value = dtShipChandlers.Select("ShipChandlerID = " + ((TextEditorControlBase)cboShipChandlersNo).Value.ToString())[0]["ExchangeRate"];
			((UltraGridBase)ULGData).DataSource = ShipChandlerReturnsDetails.SelectByShipChandlerID(((TextEditorControlBase)cboShipChandlersNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
		else
		{
			cboOnAccountOF.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "0";
			dtDetails.Rows.Clear();
		}
	}

	private void btnShipChandlersSearch_Click(object sender, EventArgs e)
	{
		if (cboOperationNo.SelectedIndex > -1)
		{
			int num = SearchFunctions.ShipChandlerSearch(((TextEditorControlBase)cboOperationNo).Value.ToString(), -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboShipChandlersNo).Value = num;
			}
		}
	}

	private void cboMaterialIssueVoucherNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.MaterialIssueVouchersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboShipChandlersNo).Value = num;
			}
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

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = ShipChandlerReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtShipChandlers);
			if (cboOperationNo.SelectedIndex > -1)
			{
				dataView.RowFilter = " OperationID = " + ((TextEditorControlBase)cboOperationNo).Value.ToString();
			}
			else
			{
				dataView.RowFilter = " OperationID = -1";
				cboOnAccountOF.SelectedIndex = -1;
				cboShipChandlersNo.SelectedIndex = -1;
			}
			dtDetails.Rows.Clear();
			GlobalFunctions.FillCombo(cboShipChandlersNo, dataView.ToTable(), "ShipChandlerID", "ShipChandlerNo");
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmShipChandlerReturns));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboOnAccountOF = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblShipChandlerNo = new UltraLabel();
		this.cboShipChandlersNo = new UltraComboEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnShipChandlersSearch = new UltraButton();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.btnJV = new UltraButton();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShipChandlersNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
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
		((EditorButtonControlBase)this.cboOnAccountOF).ReadOnly = true;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblShipChandlerNo, "lblShipChandlerNo");
		this.lblShipChandlerNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShipChandlerNo).Name = "lblShipChandlerNo";
		((ControlBase)this.lblShipChandlerNo).WrapText = false;
		resources.ApplyResources(this.cboShipChandlersNo, "cboShipChandlersNo");
		((TextEditorControlBase)this.cboShipChandlersNo).AlwaysInEditMode = true;
		this.cboShipChandlersNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboShipChandlersNo).Name = "cboShipChandlersNo";
		((TextEditorControlBase)this.cboShipChandlersNo).ValueChanged += new System.EventHandler(cboShipChandlersNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboShipChandlersNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboMaterialIssueVoucherNo_KeyDown);
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		((UltraButtonBase)this.btnShipChandlersSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnShipChandlersSearch, "btnShipChandlersSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnShipChandlersSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnShipChandlersSearch).Name = "btnShipChandlersSearch";
		((System.Windows.Forms.Control)(object)this.btnShipChandlersSearch).Click += new System.EventHandler(btnShipChandlersSearch_Click);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((EditorButtonControlBase)this.txtExchangeRate).ReadOnly = true;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((EditorButtonControlBase)this.cboCurrency).ReadOnly = true;
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
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
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnShipChandlersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShipChandlerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShipChandlersNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOnAccountOF);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmShipChandlerReturns";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOnAccountOF, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShipChandlersNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShipChandlerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnShipChandlersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOnAccountOF).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShipChandlersNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
