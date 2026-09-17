using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.Purchasing;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SafesAndBanks.SafeTransactions;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Purchasing.Transactions;

public class frmPSOrders : frmHeaderDetails
{
	private DataTable dtBatchs;

	private DataTable dtBranches;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtReports;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtSuppliers;

	private DataTable dtQuotations;

	private DataTable dtPurchaseRequest;

	private DataTable dtCurrency;

	private DataTable dtPaymentMethod;

	private DataTable dtQuotationDetails;

	private DataTable dtPurchaseRequestDetails;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool ShowSerial = false;

	private int rowIndex = -1;

	private IContainer components = null;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboSupplier;

	private UltraLabel lblSupplier;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblQuotation;

	private UltraComboEditor cboQuotation;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblTotalPrice;

	private UltraTextEditor txtTotalPrice;

	public UltraButton btnSupplierSearch;

	public UltraButton btnQuotationSearch;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsQuotation;

	private RadioButton rbIsPurchaseRequest;

	private RadioButton rbIsDirectOrder;

	public UltraButton btnPaymentMethodSearch;

	private UltraLabel lblPaymentMethod;

	private UltraComboEditor cboPaymentMethod;

	public UltraButton btnPurchaseRequestSearch;

	private UltraLabel lblPurchaseRequest;

	private UltraComboEditor cboPurchaseRequest;

	private UltraComboEditor cboQuotationNoFilterd;

	private UltraCheckEditor chkQuotationNoFilterd;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	public UltraButton btnSafeOutRequest;

	public frmPSOrders()
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
		InitializeComponent();
		TableName = "PS_PSOrders";
		IDCol = "PSOrderID";
		NoCol = "PSOrderNo";
		DateCol = "PSOrderDate";
		((Control)(object)btnSafeOutRequest).Text = (GlobalVariables.IsArabic ? "إنشاء طلب صرف خزينة" : "Create Safe Out Request");
	}

	public frmPSOrders(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		ShowSerial = GlobalFunctions.GetOption("ShowBatchNoInPurchaseOrder");
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
		if (ShowSerial)
		{
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
		}
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtQuotations = Quotations.FillCombo("-1", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
		dtPurchaseRequest = PSRequest.FillCombo("1", "0", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboPurchaseRequest, dtPurchaseRequest, "PSRequestID", "PSRequestNo");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtItems = Items.FillComboWithNotes("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		FillCurrencyDropDown();
		dtDetails = PSOrdersDetails.SelectByPSOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdQty"].DefaultCellValue = 0;
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
		if (ShowSerial)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "السريل" : "Batch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = PSOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged -= cboPurchaseRequest_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["PSOrderNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["PSOrderDate"];
			((TextEditorControlBase)cboSupplier).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboPaymentMethod).Value = drMaster["PaymentMethodID"];
			((TextEditorControlBase)cboQuotation).Value = drMaster["QuotationID"];
			((TextEditorControlBase)cboPurchaseRequest).Value = drMaster["PSRequestID"];
			rbIsDirectOrder.Checked = bool.Parse(drMaster["IsDirectOrder"].ToString());
			rbIsPurchaseRequest.Checked = cboPurchaseRequest.SelectedIndex != -1;
			rbIsQuotation.Checked = cboQuotation.SelectedIndex != -1;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			if (drMaster["SafeOutRequestID"] == DBNull.Value)
			{
				((Control)(object)btnSafeOutRequest).Text = (GlobalVariables.IsArabic ? "إنشاء طلب صرف خزينة" : "Create Safe Out Request");
			}
			else
			{
				((Control)(object)btnSafeOutRequest).Text = (GlobalVariables.IsArabic ? "تعديل طلب صرف خزينة" : "Update Safe Out Request");
			}
			dtDetails = PSOrdersDetails.SelectByPSOrderID(drMaster["PSOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged += cboPurchaseRequest_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
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
		rbIsDirectOrder.Enabled = !NavMode;
		rbIsPurchaseRequest.Enabled = !NavMode;
		rbIsQuotation.Enabled = !NavMode;
		((EditorButtonControlBase)cboSupplier).ReadOnly = NavMode;
		((EditorButtonControlBase)cboQuotation).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPurchaseRequest).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)btnSafeOutRequest).Enabled = NavMode;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((Control)(object)btnSupplierSearch).Visible = !NavMode;
		((Control)(object)btnQuotationSearch).Visible = !NavMode && rbIsQuotation.Checked;
		((Control)(object)btnPurchaseRequestSearch).Visible = !NavMode && rbIsPurchaseRequest.Checked;
		((Control)(object)btnPaymentMethodSearch).Visible = !NavMode;
		if (Adding)
		{
			DataView dataView = new DataView(dtPurchaseRequest);
			dataView.RowFilter = " Closed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboPurchaseRequest, dataView.ToTable(), "PSRequestID", "PSRequestNo");
			DataView dataView2 = new DataView(dtQuotations);
			dataView2.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView2.ToTable(), "QuotationID", "QuotationNo");
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView3.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			int num = 0;
			int num2 = 0;
			if (cboPurchaseRequest.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboPurchaseRequest).Value.ToString());
			}
			if (cboQuotation.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboQuotation).Value.ToString());
			}
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged -= cboPurchaseRequest_ValueChanged;
			GlobalFunctions.FillCombo(cboPurchaseRequest, dtPurchaseRequest, "PSRequestID", "PSRequestNo");
			GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboPurchaseRequest).Value = num;
			}
			if (num2 > 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num2;
			}
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged += cboPurchaseRequest_ValueChanged;
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		if (cboQuotation.SelectedIndex > -1)
		{
			dtQuotationDetails = QuotationsDetails.SelectByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		}
		if (cboPurchaseRequest.SelectedIndex > -1)
		{
			DataView dataView4 = new DataView(dtQuotations);
			dataView4.RowFilter = "  PSRequestID=" + ((TextEditorControlBase)cboPurchaseRequest).Value.ToString();
			DataTable dt = dataView4.ToTable();
			GlobalFunctions.FillCombo(cboQuotationNoFilterd, dt, "QuotationID", "QuotationNo");
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
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
			if (ShowSerial && UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
				((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].Value = DBNull.Value;
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

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? PSOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		cboSupplier.SelectedIndex = -1;
		cboCurrency.SelectedIndex = -1;
		cboQuotation.SelectedIndex = -1;
		cboPurchaseRequest.SelectedIndex = -1;
		cboQuotationNoFilterd.SelectedIndex = -1;
		((UltraToggleEditorBase)chkQuotationNoFilterd).Checked = false;
		cboPaymentMethod.SelectedIndex = -1;
		rbIsQuotation.Checked = false;
		rbIsPurchaseRequest.Checked = false;
		rbIsDirectOrder.Checked = true;
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtExchangeRate).Clear();
		((TextEditorControlBase)txtTotalPrice).Clear();
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
		if (cboSupplier.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Supplier");
			((TextEditorControlBase)cboSupplier).Focus();
			cboSupplier.DropDown();
			return false;
		}
		if (rbIsPurchaseRequest.Checked && cboPurchaseRequest.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار طلب الشراء" : "Please Select Purchase Request");
			((TextEditorControlBase)cboPurchaseRequest).Focus();
			cboPurchaseRequest.DropDown();
			return false;
		}
		if (rbIsQuotation.Checked && cboQuotation.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض السعر" : "Please Select Quotation No");
			((TextEditorControlBase)cboQuotation).Focus();
			cboQuotation.DropDown();
			return false;
		}
		if (!rbIsQuotation.Checked && !rbIsPurchaseRequest.Checked && !rbIsDirectOrder.Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع امر الشراء" : "Please Select Purchase Order Type");
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("PS_PSOrders", "PSOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PSOrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = PSOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
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
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = PSOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectOrder.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboPurchaseRequest.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseRequest).Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), "0", "0", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["PSOrderDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["PSOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			PSOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = PSOrders.Insert_Update(drMaster["PSOrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectOrder.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), (cboPurchaseRequest.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseRequest).Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, (cboPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPaymentMethod).Value.ToString(), bool.Parse(drMaster["IsGRNFirst"].ToString()) ? "1" : "0", bool.Parse(drMaster["HasPSInvoice"].ToString()) ? "1" : "0", "Null", bool.Parse(drMaster["Closed"].ToString()) ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["PSOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["PSOrderDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("PS_PSOrdersDetails", "PSOrderID", drMaster["PSOrderID"].ToString(), "PSOrderDetailID", text);
			PSOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			PSOrders.DeleteVirtual(drMaster["PSOrderID"].ToString(), GlobalVariables.UserID);
			PSOrdersDetails.DeleteVirtualByPSOrderID(drMaster["PSOrderID"].ToString(), GlobalVariables.UserID);
			if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='MarineService'")[0]["Installed"]))
			{
				OperationsPSOrders.DeleteVirtualByPSOrderID(drMaster["PSOrderID"].ToString(), GlobalVariables.UserID);
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
		if (bool.Parse(drMaster["HasPSInvoice"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة مشتريات على أمر الشراء", "Cannot Delete This Transaction Because there Are Purchase Invoice On This Purchase Order");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
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
			if (bool.Parse(drMaster["HasPSInvoice"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة مشتريات على إذن أمر الشراء برجاء حذف الفاتورة اولا", "Cannot Update This Transaction Because purchase Invoice Was Made on Purchase order Please Delete Purchase Invoice First   ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PS_PSOrders_A.rpt" : "Rep_PS_PSOrders_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PSOrderIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PSOrdersReport(-1, 0, -1, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PSOrderID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboCurrency).Value;
		object value2 = ((TextEditorControlBase)cboPaymentMethod).Value;
		object value3 = ((TextEditorControlBase)cboPurchaseRequest).Value;
		object value4 = ((TextEditorControlBase)cboQuotation).Value;
		object value5 = ((TextEditorControlBase)cboQuotationNoFilterd).Value;
		object value6 = ((TextEditorControlBase)cboSupplier).Value;
		object value7 = ((TextEditorControlBase)cboTransactionBranch).Value;
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
		if (ShowSerial)
		{
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
		}
		dtItems = Items.FillComboWithNotes("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPurchaseRequest = PSRequest.FillCombo("1", "0", GlobalVariables.BranchIDs);
		dtQuotations = Quotations.FillCombo("-1", GlobalVariables.BranchIDs);
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		if (Adding)
		{
			DataView dataView = new DataView(dtPurchaseRequest);
			dataView.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboPurchaseRequest, dataView.ToTable(), "PSRequestID", "PSRequestNo");
			DataView dataView2 = new DataView(dtPurchaseRequest);
			dataView2.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView2.ToTable(), "QuotationID", "QuotationNo");
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView3.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dataTable.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[l]["ItemID"], dataTable.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[l]["ItemID"], dataTable.Rows[l]["ItemBarCode"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboPurchaseRequest, dtPurchaseRequest, "PSRequestID", "PSRequestNo");
			GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int m = 0; m < dtItems.Rows.Count; m++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
		if (cboQuotation.SelectedIndex > -1)
		{
			DataTable dt = QuotationsRequestSuppliers.SelectSuppliersByQuotationRequestID_FillCombo(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			GlobalFunctions.FillCombo(cboSupplier, dt, "SubAccountID", "SubAccountName");
		}
		else
		{
			dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboSupplier).Value = value6;
		((TextEditorControlBase)cboCurrency).Value = value;
		((TextEditorControlBase)cboPaymentMethod).Value = value2;
		((TextEditorControlBase)cboPurchaseRequest).Value = value3;
		((TextEditorControlBase)cboQuotation).Value = value4;
		((TextEditorControlBase)cboQuotationNoFilterd).Value = value5;
		((TextEditorControlBase)cboTransactionBranch).Value = value7;
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_1131: Unknown result type (might be due to invalid IL or missing references)
		//IL_113b: Expected O, but got Unknown
		//IL_1149: Unknown result type (might be due to invalid IL or missing references)
		//IL_1153: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			e.Cell.Row.Cells["UnitPrice"].Value = 0;
			e.Cell.Row.Cells["TotalPrice"].Value = 0;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = dataRow["Notes"];
			int num = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
			if (ShowSerial && UsingBatchNoAndValidityPeriod)
			{
				int num2 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
				if (num2 != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num2);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
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
			if (!((UltraToggleEditorBase)chkQuotationNoFilterd).Checked)
			{
				if (cboQuotation.SelectedIndex > -1 && dtQuotationDetails != null)
				{
					DataRow[] array = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Value, e.Cell.Row.Cells["ColorID"].Value.Equals(DBNull.Value) ? "" : (" And ColorID=" + e.Cell.Row.Cells["ColorID"].Value), e.Cell.Row.Cells["ItemSizeID"].Value.Equals(DBNull.Value) ? "" : (" And ItemSizeID=" + e.Cell.Row.Cells["ItemSizeID"].Value)));
					if (array.Length != 0)
					{
						e.Cell.Row.Cells["UnitPrice"].Value = array[0]["UnitPrice"].ToString();
						e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
					}
				}
				else if (cboSupplier.SelectedIndex > -1 && !rbIsDirectOrder.Checked)
				{
					DataTable dataTable = Items.SelectLastPSPrice(e.Cell.Value.ToString(), e.Cell.Row.Cells["ColorID"].Value.Equals(DBNull.Value) ? "1" : e.Cell.Row.Cells["ColorID"].Value.ToString(), e.Cell.Row.Cells["ItemSizeID"].Value.Equals(DBNull.Value) ? "1" : e.Cell.Row.Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
					if (dataTable.Rows.Count > 0)
					{
						e.Cell.Row.Cells["UnitPrice"].Value = dataTable.Rows[0]["UnitPrice"].ToString();
						e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
					}
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ColorID" && !e.Cell.Row.Cells["ItemSizeID"].Value.Equals(DBNull.Value) && !e.Cell.Row.Cells["ItemID"].Value.Equals(DBNull.Value) && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			if (!((UltraToggleEditorBase)chkQuotationNoFilterd).Checked)
			{
				if (cboQuotation.SelectedIndex > -1)
				{
					DataRow[] array2 = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Row.Cells["ItemID"].Value, " And ColorID=", e.Cell.Value, " And ItemSizeID=", e.Cell.Row.Cells["ItemSizeID"].Value));
					if (array2.Length != 0)
					{
						e.Cell.Row.Cells["UnitPrice"].Value = array2[0]["UnitPrice"].ToString();
						e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
					}
					else
					{
						e.Cell.Row.Cells["UnitPrice"].Value = 0;
						e.Cell.Row.Cells["TotalPrice"].Value = 0;
					}
				}
				else if (cboSupplier.SelectedIndex > -1 && !rbIsDirectOrder.Checked)
				{
					DataTable dataTable2 = Items.SelectLastPSPrice(e.Cell.Row.Cells["ItemID"].Value.ToString(), e.Cell.Value.ToString(), e.Cell.Row.Cells["ItemSizeID"].Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
					if (dataTable2.Rows.Count > 0)
					{
						e.Cell.Row.Cells["UnitPrice"].Value = dataTable2.Rows[0]["UnitPrice"].ToString();
						e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
					}
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemSizeID" && !e.Cell.Row.Cells["ColorID"].Value.Equals(DBNull.Value) && !e.Cell.Row.Cells["ItemID"].Value.Equals(DBNull.Value) && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && !((UltraToggleEditorBase)chkQuotationNoFilterd).Checked)
		{
			if (cboQuotation.SelectedIndex > -1)
			{
				DataRow[] array3 = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Row.Cells["ItemID"].Value, " And ColorID=", e.Cell.Row.Cells["ColorID"].Value, " And ItemSizeID=", e.Cell.Value));
				if (array3.Length != 0)
				{
					e.Cell.Row.Cells["UnitPrice"].Value = array3[0]["UnitPrice"].ToString();
					e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
				}
				else
				{
					e.Cell.Row.Cells["UnitPrice"].Value = 0;
					e.Cell.Row.Cells["TotalPrice"].Value = 0;
				}
			}
			else if (cboSupplier.SelectedIndex > -1 && !rbIsDirectOrder.Checked)
			{
				DataTable dataTable3 = Items.SelectLastPSPrice(e.Cell.Row.Cells["ItemID"].Value.ToString(), e.Cell.Row.Cells["ItemSizeID"].Value.ToString(), e.Cell.Value.ToString(), ((TextEditorControlBase)cboSupplier).Value.ToString(), IsFromServer: false);
				if (dataTable3.Rows.Count > 0)
				{
					e.Cell.Row.Cells["UnitPrice"].Value = dataTable3.Rows[0]["UnitPrice"].ToString();
					e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
				}
			}
		}
		CalculateTotals();
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
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID") && ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !CanModifyPriceType && !rbIsDirectOrder.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && !CanModifyQty && !rbIsDirectOrder.Checked)
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
					frmAddItemsSerial frmAddItemsSerial2 = new frmAddItemsSerial(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSerial2.WindowState = FormWindowState.Normal;
					frmAddItemsSerial2.ShowDialog();
					dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					vlBatchs.ValueListItems.Clear();
					for (int i = 0; i < dtBatchs.Rows.Count; i++)
					{
						vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmAddItemsSerial2.BatchID;
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList == null)
				{
					frmAddItemsColor frmAddItemsColor2 = new frmAddItemsColor(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsColor2.WindowState = FormWindowState.Normal;
					frmAddItemsColor2.ShowDialog();
					dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					vlColors.ValueListItems.Clear();
					for (int j = 0; j < dtColors.Rows.Count; j++)
					{
						vlColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
					}
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmAddItemsColor2.ColorID;
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList == null)
				{
					frmAddItemsSize frmAddItemsSize2 = new frmAddItemsSize(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSize2.WindowState = FormWindowState.Normal;
					frmAddItemsSize2.ShowDialog();
					dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					vlSizes.ValueListItems.Clear();
					for (int k = 0; k < dtSizes.Rows.Count; k++)
					{
						vlSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
					}
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmAddItemsSize2.SizeID;
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
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected O, but got Unknown
		if (ULGData.ActiveCell == null)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
		{
			ULGData.ActiveCell.Value = 0;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(" لايمكن نقص الكمية عن الكمية المستلمة فى المخازن و قدرها " + ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString(), "You Cannot Decrease The Quantity From The Deliverd Qty in the Store" + ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value;
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		CalculateTotals();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdQty"].Value.ToString()) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لانه تمت أضافته فى المخازن" : "Cannot Delete This Item Because Added in The Store");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
		rowIndex = -1;
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
		else if (rowIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > rowIndex)
		{
			frmEnterQuantity frmEnterQuantity2 = new frmEnterQuantity(((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Text.Split('-')[0].ToString(), ((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			frmEnterQuantity2.WindowState = FormWindowState.Normal;
			if (frmEnterQuantity2.ShowDialog() == DialogResult.OK)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) + decimal.Parse(frmEnterQuantity2.Value);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
				CalculateTotals();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void AddItemInGid()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && (!ShowSerial || (ShowSerial && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4)) && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				rowIndex = i;
				CalculateTotals();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				return;
			}
		}
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
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
		ValueList unitsValueList = getUnitsValueList(unitTypeID);
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
		if (ShowSerial && UsingBatchNoAndValidityPeriod)
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
			((Control)(object)txtCode).Text = PSOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void frmPSOrders_Load(object sender, EventArgs e)
	{
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSuppliers);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + " ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboSupplier, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
	}

	private void btnSafeOutRequest_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && bool.Parse(drMaster["Approved"].ToString()) && !bool.Parse(drMaster["Closed"].ToString()))
		{
			frmSafeOutRequest frmSafeOutRequest2 = new frmSafeOutRequest((drMaster["SafeOutRequestID"] == DBNull.Value) ? (-1) : int.Parse(drMaster["SafeOutRequestID"].ToString()), drMaster["PSOrderID"].ToString(), int.Parse(drMaster["CurrencyID"].ToString()));
			frmSafeOutRequest2.Size = new Size(base.Width, base.Height);
			frmSafeOutRequest2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmSafeOutRequest2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب صرف خزينة" : "Safe Out Request");
			frmSafeOutRequest2.ShowDialog();
			if (frmSafeOutRequest2._SafeOutRequestID > 0)
			{
				FillData();
				((Control)(object)btnSafeOutRequest).Text = (GlobalVariables.IsArabic ? "تعديل طلب صرف خزينة" : "Update Safe Out Request");
			}
		}
	}

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSuppliers);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboSupplier, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
		dtDetails.Rows.Clear();
	}

	private void cboQuotation_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num;
			}
		}
	}

	private void cboQuotation_ValueChanged(object sender, EventArgs e)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Expected O, but got Unknown
		if (cboQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			dtQuotationDetails = QuotationsDetails.SelectByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			dtDetails.Rows.Clear();
			GlobalVariables.QuestionMB.Show("هل تريد إضافة كل أصناف العرض ؟", "Are You Sure You want to Generate All Quotation Items?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				for (int i = 0; i < dtQuotationDetails.Rows.Count; i++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtQuotationDetails.Rows[i]["ItemID"].ToString());
					obj.Value = value;
					DataRow dataRow = dtItems.Select(" ItemID= " + dtQuotationDetails.Rows[i]["ItemID"].ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dtQuotationDetails.Rows[i]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dtQuotationDetails.Rows[i]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtQuotationDetails.Rows[i]["UnitID"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dtQuotationDetails.Rows[i]["Qty"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtQuotationDetails.Rows[i]["UnitPrice"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = dtQuotationDetails.Rows[i]["TotalPrice"].ToString();
					int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList = getUnitsValueList(unitTypeID);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					}
				}
			}
			((TextEditorControlBase)cboSupplier).Value = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["SubAccountID"];
			if (((TextEditorControlBase)cboSupplier).Value == null)
			{
				((Control)(object)cboSupplier).Text = "";
			}
			((TextEditorControlBase)cboCurrency).Value = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			CalculateTotals();
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		}
	}

	private void btnQuotationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PSQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotation).Value = num;
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblPurchaseRequest).Visible = rbIsPurchaseRequest.Checked;
		((Control)(object)chkQuotationNoFilterd).Visible = rbIsPurchaseRequest.Checked;
		((Control)(object)lblQuotation).Visible = rbIsQuotation.Checked;
		((Control)(object)cboPurchaseRequest).Visible = rbIsPurchaseRequest.Checked;
		((Control)(object)cboQuotationNoFilterd).Visible = rbIsPurchaseRequest.Checked;
		((Control)(object)cboQuotation).Visible = rbIsQuotation.Checked;
		if (Adding || Updating)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress -= txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			cboPurchaseRequest.SelectedIndex = -1;
			cboQuotation.SelectedIndex = -1;
			cboSupplier.SelectedIndex = -1;
			cboPaymentMethod.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((Control)(object)btnPurchaseRequestSearch).Visible = rbIsPurchaseRequest.Checked;
			((Control)(object)btnQuotationSearch).Visible = rbIsQuotation.Checked;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress += txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
	}

	private void cboPurchaseRequest_ValueChanged(object sender, EventArgs e)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Expected O, but got Unknown
		if (cboPurchaseRequest.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged -= cboPurchaseRequest_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			dtPurchaseRequestDetails = PSOrdersDetails.FillByPSRequestID(((TextEditorControlBase)cboPurchaseRequest).Value.ToString(), (cboSupplier.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboSupplier).Value.ToString());
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			dtDetails.Rows.Clear();
			for (int i = 0; i < dtPurchaseRequestDetails.Rows.Count; i++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtPurchaseRequestDetails.Rows[i]["ItemID"].ToString());
				obj.Value = value;
				DataRow dataRow = dtItems.Select(" ItemID= " + dtPurchaseRequestDetails.Rows[i]["ItemID"].ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dtPurchaseRequestDetails.Rows[i]["ColorID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dtPurchaseRequestDetails.Rows[i]["ItemSizeID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtPurchaseRequestDetails.Rows[i]["UnitID"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dtPurchaseRequestDetails.Rows[i]["Qty"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtPurchaseRequestDetails.Rows[i]["UnitPrice"].ToString();
				if (!dtPurchaseRequestDetails.Rows[i]["UnitPrice"].Equals(DBNull.Value))
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = Convert.ToDecimal(dtPurchaseRequestDetails.Rows[i]["Qty"]) * Convert.ToDecimal(dtPurchaseRequestDetails.Rows[i]["UnitPrice"]);
				}
				int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList = getUnitsValueList(unitTypeID);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				}
			}
			GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			CalculateTotals();
			((TextEditorControlBase)cboPurchaseRequest).ValueChanged += cboPurchaseRequest_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
		}
	}

	private void cboPurchaseRequest_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSRequestSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboPurchaseRequest).Value = num;
			}
		}
	}

	private void btnPurchaseRequestSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PSRequestSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboPurchaseRequest).Value = num;
		}
	}

	private void cboSupplier_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Suppliers((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSupplier).Value = num;
			}
		}
	}

	private void btnSupplierSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Suppliers((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSupplier).Value = num;
		}
	}

	private void cboSupplier_ValueChanged(object sender, EventArgs e)
	{
		if (cboSupplier.SelectedIndex > -1)
		{
			if (rbIsQuotation.Checked)
			{
				((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
				DataView dataView = new DataView(dtQuotations);
				dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
				dtDetails.Rows.Clear();
				cboCurrency.SelectedIndex = -1;
				((Control)(object)txtExchangeRate).Text = "";
				((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
				((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			}
			else if (rbIsPurchaseRequest.Checked && ((UltraToggleEditorBase)chkQuotationNoFilterd).Checked)
			{
				DataView dataView2 = new DataView(dtQuotations);
				dataView2.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboQuotationNoFilterd, dataView2.ToTable(), "QuotationID", "QuotationNo");
			}
			((TextEditorControlBase)cboPaymentMethod).Value = dtSuppliers.Select(" SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value)[0]["DefaultPaymentMethodID"];
		}
	}

	private void cboPaymentMethod_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PaymentMethods(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboPaymentMethod).Value = num;
			}
		}
	}

	private void btnPaymentMethodSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PaymentMethods(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboPaymentMethod).Value = num;
		}
	}

	private void cboQuotationNoFilterd_ValueChanged(object sender, EventArgs e)
	{
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Expected O, but got Unknown
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Expected O, but got Unknown
		if (cboQuotationNoFilterd.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotationNoFilterd).ValueChanged -= cboQuotationNoFilterd_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			dtQuotationDetails = QuotationsDetails.SelectByQuotationID(((TextEditorControlBase)cboQuotationNoFilterd).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				DataRow[] array = dtQuotationDetails.Select(string.Concat(" ItemID= ", ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value, " And ColorID=", ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value, " And ItemSizeID=", ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value, " And UnitID=", ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value));
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = ((array.Length != 0) ? array[0]["UnitPrice"].ToString() : "0");
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
			((TextEditorControlBase)cboSupplier).Value = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotationNoFilterd).Value.ToString())[0]["SubAccountID"];
			((TextEditorControlBase)cboCurrency).Value = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotationNoFilterd).Value.ToString())[0]["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotationNoFilterd).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			CalculateTotals();
			((TextEditorControlBase)cboQuotationNoFilterd).ValueChanged += cboQuotationNoFilterd_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboQuotationNoFilterd).ValueChanged -= cboQuotationNoFilterd_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged -= cboSupplier_ValueChanged;
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = 0;
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = 0;
			}
			CalculateTotals();
			((TextEditorControlBase)cboQuotationNoFilterd).ValueChanged += cboQuotationNoFilterd_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSupplier).ValueChanged += cboSupplier_ValueChanged;
		}
	}

	private void chkQuotationNoFilterd_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboQuotationNoFilterd).ReadOnly = !((UltraToggleEditorBase)chkQuotationNoFilterd).Checked;
		if (cboSupplier.SelectedIndex > -1 && rbIsPurchaseRequest.Checked && ((UltraToggleEditorBase)chkQuotationNoFilterd).Checked)
		{
			DataView dataView = new DataView(dtQuotations);
			dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboSupplier).Value.ToString() + " And  BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotationNoFilterd, dataView.ToTable(), "QuotationID", "QuotationNo");
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected O, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Expected O, but got Unknown
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Purchasing.Transactions.frmPSOrders));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsQuotation = new System.Windows.Forms.RadioButton();
		this.rbIsPurchaseRequest = new System.Windows.Forms.RadioButton();
		this.rbIsDirectOrder = new System.Windows.Forms.RadioButton();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboSupplier = new UltraComboEditor();
		this.lblSupplier = new UltraLabel();
		this.txtBarCode = new UltraTextEditor();
		this.lblQuotation = new UltraLabel();
		this.cboQuotation = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblTotalPrice = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.btnSupplierSearch = new UltraButton();
		this.btnQuotationSearch = new UltraButton();
		this.btnPaymentMethodSearch = new UltraButton();
		this.lblPaymentMethod = new UltraLabel();
		this.cboPaymentMethod = new UltraComboEditor();
		this.btnPurchaseRequestSearch = new UltraButton();
		this.lblPurchaseRequest = new UltraLabel();
		this.cboPurchaseRequest = new UltraComboEditor();
		this.cboQuotationNoFilterd = new UltraComboEditor();
		this.chkQuotationNoFilterd = new UltraCheckEditor();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.btnSafeOutRequest = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseRequest).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotationNoFilterd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkQuotationNoFilterd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
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
		resources.ApplyResources(val9, "appearance15");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsQuotation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsPurchaseRequest);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirectOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsQuotation, "rbIsQuotation");
		this.rbIsQuotation.BackColor = System.Drawing.Color.Transparent;
		this.rbIsQuotation.Name = "rbIsQuotation";
		this.rbIsQuotation.TabStop = true;
		this.rbIsQuotation.UseVisualStyleBackColor = false;
		this.rbIsQuotation.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsPurchaseRequest, "rbIsPurchaseRequest");
		this.rbIsPurchaseRequest.BackColor = System.Drawing.Color.Transparent;
		this.rbIsPurchaseRequest.Name = "rbIsPurchaseRequest";
		this.rbIsPurchaseRequest.TabStop = true;
		this.rbIsPurchaseRequest.UseVisualStyleBackColor = false;
		this.rbIsPurchaseRequest.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDirectOrder, "rbIsDirectOrder");
		this.rbIsDirectOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirectOrder.Name = "rbIsDirectOrder";
		this.rbIsDirectOrder.TabStop = true;
		this.rbIsDirectOrder.UseVisualStyleBackColor = false;
		this.rbIsDirectOrder.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
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
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		((TextEditorControlBase)this.cboSupplier).ValueChanged += new System.EventHandler(cboSupplier_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSupplier).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSupplier_KeyDown);
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblQuotation, "lblQuotation");
		this.lblQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotation).Name = "lblQuotation";
		((ControlBase)this.lblQuotation).WrapText = false;
		resources.ApplyResources(this.cboQuotation, "cboQuotation");
		((TextEditorControlBase)this.cboQuotation).AlwaysInEditMode = true;
		this.cboQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotation).Name = "cboQuotation";
		((TextEditorControlBase)this.cboQuotation).ValueChanged += new System.EventHandler(cboQuotation_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboQuotation).KeyDown += new System.Windows.Forms.KeyEventHandler(cboQuotation_KeyDown);
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
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.btnSupplierSearch, "btnSupplierSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance9");
		((ControlBase)this.btnSupplierSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Name = "btnSupplierSearch";
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Click += new System.EventHandler(btnSupplierSearch_Click);
		resources.ApplyResources(this.btnQuotationSearch, "btnQuotationSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance16");
		((ControlBase)this.btnQuotationSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Name = "btnQuotationSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Click += new System.EventHandler(btnQuotationSearch_Click);
		resources.ApplyResources(this.btnPaymentMethodSearch, "btnPaymentMethodSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance17");
		((ControlBase)this.btnPaymentMethodSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Name = "btnPaymentMethodSearch";
		((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch).Click += new System.EventHandler(btnPaymentMethodSearch_Click);
		resources.ApplyResources(this.lblPaymentMethod, "lblPaymentMethod");
		this.lblPaymentMethod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentMethod).Name = "lblPaymentMethod";
		((ControlBase)this.lblPaymentMethod).WrapText = false;
		resources.ApplyResources(this.cboPaymentMethod, "cboPaymentMethod");
		((TextEditorControlBase)this.cboPaymentMethod).AlwaysInEditMode = true;
		this.cboPaymentMethod.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).Name = "cboPaymentMethod";
		((System.Windows.Forms.Control)(object)this.cboPaymentMethod).KeyDown += new System.Windows.Forms.KeyEventHandler(cboPaymentMethod_KeyDown);
		((UltraButtonBase)this.btnPurchaseRequestSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnPurchaseRequestSearch, "btnPurchaseRequestSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance18");
		((ControlBase)this.btnPurchaseRequestSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnPurchaseRequestSearch).Name = "btnPurchaseRequestSearch";
		((System.Windows.Forms.Control)(object)this.btnPurchaseRequestSearch).Click += new System.EventHandler(btnPurchaseRequestSearch_Click);
		resources.ApplyResources(this.lblPurchaseRequest, "lblPurchaseRequest");
		this.lblPurchaseRequest.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseRequest).Name = "lblPurchaseRequest";
		((ControlBase)this.lblPurchaseRequest).WrapText = false;
		resources.ApplyResources(this.cboPurchaseRequest, "cboPurchaseRequest");
		((TextEditorControlBase)this.cboPurchaseRequest).AlwaysInEditMode = true;
		this.cboPurchaseRequest.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPurchaseRequest).Name = "cboPurchaseRequest";
		((TextEditorControlBase)this.cboPurchaseRequest).ValueChanged += new System.EventHandler(cboPurchaseRequest_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboPurchaseRequest).KeyDown += new System.Windows.Forms.KeyEventHandler(cboPurchaseRequest_KeyDown);
		resources.ApplyResources(this.cboQuotationNoFilterd, "cboQuotationNoFilterd");
		((TextEditorControlBase)this.cboQuotationNoFilterd).AlwaysInEditMode = true;
		this.cboQuotationNoFilterd.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotationNoFilterd).Name = "cboQuotationNoFilterd";
		((EditorButtonControlBase)this.cboQuotationNoFilterd).ReadOnly = true;
		((TextEditorControlBase)this.cboQuotationNoFilterd).ValueChanged += new System.EventHandler(cboQuotationNoFilterd_ValueChanged);
		resources.ApplyResources(this.chkQuotationNoFilterd, "chkQuotationNoFilterd");
		((System.Windows.Forms.Control)(object)this.chkQuotationNoFilterd).Name = "chkQuotationNoFilterd";
		((UltraToggleEditorBase)this.chkQuotationNoFilterd).CheckedChanged += new System.EventHandler(chkQuotationNoFilterd_CheckedChanged);
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance19");
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		resources.ApplyResources(this.btnSafeOutRequest, "btnSafeOutRequest");
		((System.Windows.Forms.Control)(object)this.btnSafeOutRequest).Name = "btnSafeOutRequest";
		((System.Windows.Forms.Control)(object)this.btnSafeOutRequest).Click += new System.EventHandler(btnSafeOutRequest_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSafeOutRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkQuotationNoFilterd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotationNoFilterd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseRequestSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPaymentMethod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSupplierSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotation);
		base.Name = "frmPSOrders";
		base.Load += new System.EventHandler(frmPSOrders_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPurchaseRequest, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPurchaseRequest, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSupplierSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentMethod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPaymentMethodSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPurchaseRequestSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotationNoFilterd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkQuotationNoFilterd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSafeOutRequest, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseRequest).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotationNoFilterd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkQuotationNoFilterd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
