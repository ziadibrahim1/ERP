using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CnsProjects;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Purchasing;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.StockControl.Reports;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Transactions;

public class frmGoodReceiptNotes : frmHeaderDetails
{
	private DataTable dtBranches;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtStores;

	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtBatchs;

	private DataTable dtSuppliers;

	private DataTable dtPSInvoices;

	private DataTable dtPSInvoiceDetails;

	private DataTable dtPSOrders;

	private DataTable dtPSOrderDetails;

	private DataTable dtMinAllowedTransDate;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool CnsProjectsInstalled = false;

	private bool GoodReceiptNoteItemsAuditAlert = false;

	private string CnsStoreID = "-1";

	private int rowIndex = -1;

	private bool ShowSerial = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboSupplier;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsPsOrder;

	private RadioButton rbIsPsInvoice;

	private UltraLabel lblSupplier;

	private UltraLabel lblInvoiceNo;

	private UltraComboEditor cboInvoiceNo;

	private UltraLabel lblOrderNo;

	private UltraComboEditor cboOrderNo;

	public UltraButton btnVouchersSearch;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private RadioButton rbIsDirect;

	public UltraButton btnPrintBarCode;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	public UltraButton btnStoreSearch;

	public UltraButton btnSelectLenses;

	private UltraTextEditor txtTotalQty;

	public UltraLabel lblTotalQty;

	public UltraButton btnSupplierSearch;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	public frmGoodReceiptNotes()
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
		TableName = "SC_GoodReceiptNotes";
		IDCol = "GoodReceiptNoteID";
		NoCol = "GoodReceiptNoteNo";
		DateCol = "GoodReceiptNoteDate";
	}

	public frmGoodReceiptNotes(int ID)
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
		GoodReceiptNoteItemsAuditAlert = GlobalFunctions.GetOption("GoodReceiptNoteItemsAuditAlert");
		ShowSerial = GlobalFunctions.GetOption("ShowBatchNoInPurchaseOrder");
		CnsProjectsInstalled = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='CnsProjects'")[0]["Installed"]);
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
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtPSInvoices = PSInvoices.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboInvoiceNo, dtPSInvoices, "PSInvoiceID", "PSInvoiceNo");
		dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboOrderNo, dtPSOrders, "PSOrderID", "PSOrderNo");
		dtDetails = GoodReceiptNotesDetails.SelectByGoodReceiptNoteID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteDetailID"].DefaultCellValue = -1;
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Hidden = !GoodReceiptNoteItemsAuditAlert;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AlertDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التنبيه" : "Alert Date");
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
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
			DataTable dataTable = GoodReceiptNotes.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			GlobalFunctions.FillCombo(cboInvoiceNo, dtPSInvoices, "PSInvoiceID", "PSInvoiceNo");
			GlobalFunctions.FillCombo(cboOrderNo, dtPSOrders, "PSOrderID", "PSOrderNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["GoodReceiptNoteNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["GoodReceiptNoteDate"];
			((TextEditorControlBase)cboSupplier).Value = drMaster["SubAccountID"];
			rbIsPsInvoice.Checked = bool.Parse(drMaster["IsPSInvoice"].ToString());
			rbIsPsOrder.Checked = !bool.Parse(drMaster["IsPSInvoice"].ToString()) && !bool.Parse(drMaster["IsDirect"].ToString());
			rbIsDirect.Checked = bool.Parse(drMaster["IsDirect"].ToString());
			((TextEditorControlBase)cboInvoiceNo).Value = drMaster["PSInvoiceID"];
			((TextEditorControlBase)cboOrderNo).Value = drMaster["PSOrderID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = GoodReceiptNotesDetails.SelectByGoodReceiptNoteID(drMaster["GoodReceiptNoteID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalcTotalQty();
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
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
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
		rbIsPsInvoice.Enabled = !NavMode && !Updating;
		rbIsPsOrder.Enabled = !NavMode && !Updating;
		rbIsDirect.Enabled = !NavMode && !Updating && CanDirect;
		((EditorButtonControlBase)cboInvoiceNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboOrderNo).ReadOnly = NavMode || Updating;
		((Control)(object)btnSelectLenses).Visible = rbIsDirect.Checked && Adding && (Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItems'")[0]["OptionValue"].ToString()) || Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItemsXY'")[0]["OptionValue"].ToString()));
		((Control)(object)cboSupplier).Enabled = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)btnSupplierSearch).Visible = (Adding || Updating) && rbIsDirect.Checked;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnVouchersSearch).Visible = Adding;
		((Control)(object)btnStoreSearch).Visible = !NavMode;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
		((Control)(object)btnPrintBarCode).Visible = NavMode;
		((Control)(object)btnImport).Visible = Adding;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataView.ToTable().Rows.Count; i++)
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
			DataView dataView2 = new DataView(dtPSInvoices);
			dataView2.RowFilter = " Closed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboInvoiceNo, dataView2.ToTable(), "PSInvoiceID", "PSInvoiceNo");
			DataView dataView3 = new DataView(dtPSOrders);
			dataView3.RowFilter = "Closed=0 And  HasPSInvoice =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboOrderNo, dataView3.ToTable(), "PSOrderID", "PSOrderNo");
		}
		if (!Adding)
		{
			int num = 0;
			int num2 = 0;
			if (cboInvoiceNo.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboInvoiceNo).Value.ToString());
			}
			if (cboOrderNo.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			GlobalFunctions.FillCombo(cboInvoiceNo, dtPSInvoices, "PSInvoiceID", "PSInvoiceNo");
			GlobalFunctions.FillCombo(cboOrderNo, dtPSOrders, "PSOrderID", "PSOrderNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num;
			}
			if (num2 > 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num2;
			}
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int k = 0; k < dtItems.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["ItemBarCode"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
		{
			if (((UltraGridBase)ULGData).Rows[l].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[l].Cells["ItemID"].Value.ToString())[0];
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
				((UltraGridBase)ULGData).Rows[l].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["BatchID"].Value = DBNull.Value;
				}
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["ColorID"].Value = DBNull.Value;
				}
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["ItemSizeID"].Value = DBNull.Value;
				}
			}
		}
		if (rbIsPsInvoice.Checked && cboInvoiceNo.SelectedIndex > -1)
		{
			dtPSInvoiceDetails = GoodReceiptNotesDetails.FillByPSInvoiceID(RowID, ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			getVoucherItemsValueList(dtPSInvoiceDetails);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		}
		else if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1)
		{
			dtPSOrderDetails = GoodReceiptNotesDetails.FillByPSOrderID(RowID, ((TextEditorControlBase)cboOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			getVoucherItemsValueList(dtPSOrderDetails);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? GoodReceiptNotes.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboInvoiceNo.SelectedIndex = -1;
		cboOrderNo.SelectedIndex = -1;
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		cboSupplier.SelectedIndex = -1;
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		if (rbIsPsInvoice.Checked && Adding)
		{
			dtPSInvoices = PSInvoices.FillCombo(GlobalVariables.BranchIDs);
			DataView dataView = new DataView(dtPSInvoices);
			dataView.RowFilter = " Closed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboInvoiceNo, dataView.ToTable(), "PSInvoiceID", "PSInvoiceNo");
		}
		((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
		if (((UltraGridBase)ULGData).Rows[0].Cells["UnitID"].Value != DBNull.Value && dataTable.Select(" UnitID<> " + ((UltraGridBase)ULGData).Rows[0].Cells["UnitID"].Value.ToString()).Length == 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
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
		if (rbIsPsInvoice.Checked && cboInvoiceNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم  فاتورة النشتريات" : "Please Select The Purchase Invoice Number");
			((TextEditorControlBase)cboInvoiceNo).Focus();
			return false;
		}
		if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم  أمر الشراء" : "Please Select The Purchase Order Number");
			((TextEditorControlBase)cboOrderNo).Focus();
			return false;
		}
		if (cboSupplier.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Supplier");
			((TextEditorControlBase)cboSupplier).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SC_GoodReceiptNotes", "GoodReceiptNoteNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["GoodReceiptNoteNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = GoodReceiptNotes.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
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
			if (cboInvoiceNo.SelectedIndex > -1)
			{
				ValidatePSInvoiceDetailAllowedQty(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["PSInvoiceDetailID"].Value.ToString()));
			}
			else if (cboOrderNo.SelectedIndex > -1)
			{
				ValidatePSOrderDetailAllowedQty(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["PSOrderDetailID"].Value.ToString()));
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (GoodReceiptNoteItemsAuditAlert && ((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"].Value.ToString()) < dtpDate.DateTime)
			{
				GlobalVariables.InformationMB.Show("تاريخ التنبيه قبل تاريخ الفاتورة", "Alert Date Is Before The Voucher Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["AlertDate"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() && !rbIsPsInvoice.Checked)
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف  مع نفس المخزن", "Cannot Duplicate The Same Item With the Same Batch No With Same Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = GoodReceiptNotes.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSupplier).Value.ToString(), rbIsPsInvoice.Checked ? ((TextEditorControlBase)cboInvoiceNo).Value.ToString() : "Null", rbIsPsOrder.Checked ? ((TextEditorControlBase)cboOrderNo).Value.ToString() : "Null", rbIsPsInvoice.Checked ? "1" : "0", rbIsDirect.Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			DataTable dataTable = ((DataTable)((UltraGridBase)ULGData).DataSource).Copy();
			dataTable.AcceptChanges();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				dataTable.Rows[i].SetAdded();
			}
			if (dataTable.Columns.Contains("ItemName"))
			{
				dataTable.Columns.Remove("ItemName");
			}
			GoodReceiptNotesDetails.Insert_UpdateByTableXML(dataTable, "GoodReceiptNoteDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				Main.ExecuteNonQuery(" Update PS_PSOrders Set IsGRNFirst=1 where PSOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = GoodReceiptNotes.Insert_Update(drMaster["GoodReceiptNoteID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSupplier).Value.ToString(), rbIsPsInvoice.Checked ? ((TextEditorControlBase)cboInvoiceNo).Value.ToString() : "Null", rbIsPsOrder.Checked ? ((TextEditorControlBase)cboOrderNo).Value.ToString() : "Null", rbIsPsInvoice.Checked ? "1" : "0", rbIsDirect.Checked ? "1" : "0", ((Control)(object)txtNotes).Text, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "GRN", "GRN");
			((UltraGridBase)ULGData).UpdateData();
			GoodReceiptNotesDetails.Insert_UpdateByTableXML((DataTable)((UltraGridBase)ULGData).DataSource, "GoodReceiptNoteDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "GRN", "GRN", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "GRN", "GRN");
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			MessageLog.DeleteByVoucherIDAndTransType(drMaster["GoodReceiptNoteID"].ToString(), "GRN", "GRN");
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			GoodReceiptNotes.DeleteVirtual(drMaster["GoodReceiptNoteID"].ToString(), GlobalVariables.UserID);
			GoodReceiptNotesDetails.DeleteVirtualByGoodReceiptNoteID(drMaster["GoodReceiptNoteID"].ToString(), GlobalVariables.UserID);
			if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				Main.ExecuteNonQuery(" Update PS_PSOrders Set IsGRNFirst=0 where (Select Count(1) From SC_GoodReceiptNotes Where Deleted = 0 And PSOrderID = " + ((TextEditorControlBase)cboOrderNo).Value.ToString() + " And GoodReceiptNoteID <> " + drMaster["GoodReceiptNoteID"].ToString() + ") = 0  And PSOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["GoodReceiptNoteID"].ToString(), "GRN", "GRN", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["GoodReceiptNoteID"].ToString(), "GRN", "GRN");
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
			string val = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SC_GoodReceiptNotes_A.rpt" : "Rep_SC_GoodReceiptNotes_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@GoodReceiptNoteIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.GoodReceiptNotesReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["GoodReceiptNoteID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
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
		dtPSInvoices = PSInvoices.FillCombo(GlobalVariables.BranchIDs);
		dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtPSInvoices);
			dataView.RowFilter = " Closed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboInvoiceNo, dataView.ToTable(), "PSInvoiceID", "PSInvoiceNo");
			DataView dataView2 = new DataView(dtPSOrders);
			dataView2.RowFilter = "Closed=0 And  HasPSInvoice =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboOrderNo, dataView2.ToTable(), "PSOrderID", "PSOrderNo");
		}
		DataView dataView3 = new DataView(dtStores);
		dataView3.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView3.ToTable();
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dataTable.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[m]["StoreID"], dataTable.Rows[m]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dataView3.ToTable(), "StoreID", "StoreName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
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
		if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1 && bool.Parse(dtPSOrders.Select(" PSOrderID = " + ((TextEditorControlBase)cboOrderNo).Value.ToString())[0]["HasPSInvoice"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة مشتريات على إذن الاضافة برجاء حذف الفاتورة اولا", "Cannot Delete This Transaction Because purchase Invoice Was Made on Good Receipt note Please Delete Purchase Invoice First   ");
			return;
		}
		if (rbIsDirect.Checked && drMaster != null && drMaster["PSInvoiceID"] != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة مشتريات على إذن الاضافة برجاء حذف الفاتورة اولا", "Cannot Delete This Transaction Because purchase Invoice Was Made on Good Receipt note Please Delete Purchase Invoice First   ");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ReturnedQty"].Value.ToString()) > 0m)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود مرتجع مورد على هذه الحركة", "Cannot Delete This Transaction Because Supplier Return Was Made on Good Receipt note Please Delete Supplier Return First   ");
				return;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n Cannot Delete This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return;
			}
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
		if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1 && bool.Parse(dtPSOrders.Select(" PSOrderID = " + ((TextEditorControlBase)cboOrderNo).Value.ToString())[0]["HasPSInvoice"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة مشتريات على إذن الاضافة برجاء حذف الفاتورة اولا", "Cannot Update This Transaction Because purchase Invoice Was Made on Good Receipt note Please Delete Purchase Invoice First   ");
			return;
		}
		if (rbIsDirect.Checked && drMaster != null && drMaster["PSInvoiceID"] != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة مشتريات على إذن الاضافة برجاء حذف الفاتورة اولا", "Cannot Update This Transaction Because purchase Invoice Was Made on Good Receipt note Please Delete Purchase Invoice First   ");
			return;
		}
		Updating = true;
		SetControls(NavMode: false);
	}

	private void getVoucherItemsValueList(DataTable dtVoucher)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtVoucher.Rows.Count; i++)
		{
			val.ValueListItems.Add((object)dtVoucher.Rows[i][rbIsPsInvoice.Checked ? "PSInvoiceDetailID" : "PSOrderDetailID"].ToString(), dtVoucher.Rows[i]["ItemName"].ToString());
			val2.ValueListItems.Add((object)dtVoucher.Rows[i][rbIsPsInvoice.Checked ? "PSInvoiceDetailID" : "PSOrderDetailID"].ToString(), dtVoucher.Rows[i]["BarCode"].ToString());
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[rbIsPsInvoice.Checked ? "PSInvoiceDetailID" : "PSOrderDetailID"].ValueList = (IValueList)(object)val;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].ValueList = (IValueList)(object)val2;
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PSInvoiceDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DetailBarCode") && !rbIsDirect.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID" && ((UltraToggleEditorBase)chkStore).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (cboInvoiceNo.SelectedIndex == -1 && cboOrderNo.SelectedIndex == -1 && !rbIsDirect.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private decimal ValidatePSInvoiceDetailAllowedQty(int RowID)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["PSInvoiceDetailID"].Value.ToString()) == RowID)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		num2 = decimal.Parse(dtPSInvoiceDetails.Select(" PSInvoiceDetailID= " + RowID)[0]["AllowedQty"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية المضافة " + Math.Round(num2, 3)) : ("Maximum Added Qty " + num2));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2 - num + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			return decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		return num2 - num;
	}

	private decimal ValidatePSOrderDetailAllowedQty(int RowID)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		((UltraGridBase)ULGData).UpdateData();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["PSOrderDetailID"].Value.ToString()) == RowID)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		num2 = decimal.Parse(dtPSOrderDetails.Select(" PSOrderDetailID= " + RowID)[0]["AllowedQty"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية المضافة " + Math.Round(num2, 3)) : ("Maximum Added Qty " + num2));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2 - num + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			return decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		return num2 - num;
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (e.Cell != null && e.Cell.Value != DBNull.Value && (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" || ((KeyedSubObjectBase)e.Cell.Column).Key == "PSInvoiceDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "PSOrderDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "DetailBarCode"))
		{
			if (cboInvoiceNo.SelectedIndex > -1 && rbIsPsInvoice.Checked)
			{
				ValidatePSInvoiceDetailAllowedQty(int.Parse(e.Cell.Row.Cells["PSInvoiceDetailID"].Value.ToString()));
			}
			else if (cboOrderNo.SelectedIndex > -1 && rbIsPsOrder.Checked)
			{
				ValidatePSOrderDetailAllowedQty(int.Parse(e.Cell.Row.Cells["PSOrderDetailID"].Value.ToString()));
			}
			if (decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString()) < decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  لايمكن تقليل الكمية عن الكمية المرتجعة و قدرها " + Math.Round(decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString()), 3)) : ("Issued Qty Cannot Decrease from Returned Qty " + decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString())));
				e.Cell.Row.Cells["Qty"].Value = e.Cell.Row.Cells["ReturnedQty"].Value;
			}
		}
		CalcTotalQty();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdd: Expected O, but got Unknown
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "PSInvoiceDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "PSOrderDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "DetailBarCode") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).AcceptChanges();
			int num = 0;
			if (rbIsPsInvoice.Checked && cboInvoiceNo.SelectedIndex > -1)
			{
				num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtPSInvoiceDetails.Select(" PSInvoiceDetailID =" + e.Cell.Value.ToString())[0]["ItemID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitID"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtPSInvoiceDetails.Select(" PSInvoiceDetailID =" + e.Cell.Value.ToString())[0]["UnitID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitPrice"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? decimal.Parse(dtPSInvoiceDetails.Select(" PSInvoiceDetailID =" + e.Cell.Value.ToString())[0]["UnitPrice"].ToString()) : 0m);
				UltraGridCell obj = e.Cell.Row.Cells["PSInvoiceDetailID"];
				object value = (e.Cell.Row.Cells["DetailBarCode"].Value = e.Cell.Value);
				obj.Value = value;
				e.Cell.Row.Cells["Qty"].Value = 0;
				e.Cell.Row.Cells["Qty"].Value = ValidatePSInvoiceDetailAllowedQty(int.Parse(e.Cell.Value.ToString()));
			}
			else if (rbIsPsOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtPSOrderDetails.Select(" PSOrderDetailID =" + e.Cell.Value.ToString())[0]["ItemID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitID"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtPSOrderDetails.Select(" PSOrderDetailID =" + e.Cell.Value.ToString())[0]["UnitID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitPrice"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? decimal.Parse(dtPSOrderDetails.Select(" PSOrderDetailID =" + e.Cell.Value.ToString())[0]["UnitPrice"].ToString()) : 0m);
				UltraGridCell obj2 = e.Cell.Row.Cells["PSOrderDetailID"];
				object value = (e.Cell.Row.Cells["DetailBarCode"].Value = e.Cell.Value);
				obj2.Value = value;
				e.Cell.Row.Cells["Qty"].Value = 0;
				e.Cell.Row.Cells["Qty"].Value = ValidatePSOrderDetailAllowedQty(int.Parse(e.Cell.Value.ToString()));
			}
			if (num != 0)
			{
				e.Cell.Row.Cells["ItemID"].Value = num;
				e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				if (UsingBatchNoAndValidityPeriod)
				{
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
			}
			else
			{
				e.Cell.Row.Cells["PSInvoiceDetailID"].Value = DBNull.Value;
				e.Cell.Row.Cells["PSOrderDetailID"].Value = DBNull.Value;
				e.Cell.Row.Cells["DetailBarCode"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemID"].Value = DBNull.Value;
				e.Cell.Row.Cells["BatchID"].ValueList = null;
				e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
			}
		}
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && rbIsDirect.Checked)
		{
			UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj3.Value = value;
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (UsingBatchNoAndValidityPeriod)
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
			int num3 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num3 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num3);
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
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			int num4 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num4 != 0)
			{
				UltraGridCell obj4 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num4);
				obj4.Value = value;
				DataRow dataRow2 = dtItems.Select(" ItemID= " + num4)[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				int num5 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num5 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num5);
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && rbIsDirect.Checked)
		{
			if (ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"];
				UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"];
				object obj4 = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = DBNull.Value);
				object obj5 = (obj3.Value = obj4);
				object value2 = (obj2.Value = obj5);
				obj.Value = value2;
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			}
			else if (dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell activeCell = ULGData.ActiveCell;
				UltraGridCell obj8 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj8.Value = obj5);
				activeCell.Value = value2;
			}
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DetailBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PSInvoiceDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PSOrderDetailID") && (rbIsPsOrder.Checked || rbIsPsInvoice.Checked))
		{
			if (ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj10 = ((UltraGridBase)ULGData).ActiveRow.Cells["PSOrderDetailID"];
				UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["PSInvoiceDetailID"];
				UltraGridCell obj12 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"];
				UltraGridCell obj13 = ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"];
				object obj14 = (((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"].Value = DBNull.Value);
				object obj4 = (obj13.Value = obj14);
				object obj5 = (obj12.Value = obj4);
				object value2 = (obj11.Value = obj5);
				obj10.Value = value2;
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			}
			else if (rbIsPsInvoice.Checked && dtPSInvoiceDetails.Select(" PSInvoiceDetailID =" + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell obj18 = ((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"];
				UltraGridCell obj19 = ((UltraGridBase)ULGData).ActiveRow.Cells["PSInvoiceDetailID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj19.Value = obj5);
				obj18.Value = value2;
			}
			else if (rbIsPsOrder.Checked && dtPSOrderDetails.Select(" PSOrderDetailID =" + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell obj21 = ((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"];
				UltraGridCell obj22 = ((UltraGridBase)ULGData).ActiveRow.Cells["PSOrderDetailID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj22.Value = obj5);
				obj21.Value = value2;
			}
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if (((rbIsPsOrder.Checked && !ShowSerial) || rbIsDirect.Checked) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
				{
					frmAddItemsSerial frmAddItemsSerial2 = new frmAddItemsSerial(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSerial2.WindowState = FormWindowState.Normal;
					if (frmAddItemsSerial2.ShowDialog() == DialogResult.OK)
					{
						dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						vlBatchs.ValueListItems.Clear();
						for (int i = 0; i < dtBatchs.Rows.Count; i++)
						{
							vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmAddItemsSerial2.BatchID;
					}
				}
				else if (rbIsDirect.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList == null)
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
				else if (rbIsDirect.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList == null)
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
			if ((Adding || Updating) && rbIsDirect.Checked && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "0", "-1", "1", "0", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "-1", IsFromServer: false));
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
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

	private void frmGoodReceiptNotes_Load(object sender, EventArgs e)
	{
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

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalcTotalQty();
		rowIndex = -1;
	}

	private void btnSelectLenses_Click(object sender, EventArgs e)
	{
		if (Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItems'")[0]["OptionValue"].ToString()))
		{
			frmColorSizeSelection frmColorSizeSelection2 = new frmColorSizeSelection(_ViewPrice: false);
			frmColorSizeSelection2.Tag = base.Tag;
			frmColorSizeSelection2.Width = base.Parent.Width;
			frmColorSizeSelection2.Height = base.Parent.Height;
			frmColorSizeSelection2.ShowDialog();
			if (frmColorSizeSelection2.dtColorSizeCrossStructure != null)
			{
				for (int i = 0; i < frmColorSizeSelection2.dtColorSizeCrossStructure.Rows.Count; i++)
				{
					for (int j = 1; j < frmColorSizeSelection2.dtColorSizeCrossStructure.Columns.Count; j++)
					{
						if (frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString() != "" && decimal.Parse(frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString()) > 0m && ((DataTable)((UltraGridBase)ULGData).DataSource).Select(" ItemID= " + frmColorSizeSelection2.ItemID + " And ColorID= " + frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i]["SPH/CYL"].ToString() + " And ItemSizeID= " + frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j].Caption).Length == 0)
						{
							((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
							((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
							UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
							object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmColorSizeSelection2.ItemID);
							obj.Value = value;
							DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
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
							((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i]["SPH/CYL"];
							((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j].Caption;
							((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
							((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(frmColorSizeSelection2.dtColorSizeCrossStructure.Rows[i][frmColorSizeSelection2.dtColorSizeCrossStructure.Columns[j]].ToString());
							if (UsingBatchNoAndValidityPeriod)
							{
								ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
								((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
								((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
							}
							int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
							ValueList unitsValueList = getUnitsValueList(unitTypeID);
							((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
							((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
						}
					}
				}
			}
			CalcTotalQty();
			return;
		}
		frmXYSelection frmXYSelection2 = new frmXYSelection(_ViewPrice: false);
		frmXYSelection2.Tag = base.Tag;
		frmXYSelection2.Width = base.Parent.Width;
		frmXYSelection2.Height = base.Parent.Height;
		frmXYSelection2.ShowDialog();
		if (frmXYSelection2.dtXYItems != null && frmXYSelection2.dtXYItems.Rows.Count > 0)
		{
			for (int k = 0; k < frmXYSelection2.dtXYItems.Rows.Count; k++)
			{
				for (int l = 1; l < frmXYSelection2.dtXYItems.Columns.Count; l++)
				{
					if (frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString() != "" && decimal.Parse(frmXYSelection2.dtXYItems.Rows[k]["Qty"].ToString()) > 0m && ((DataTable)((UltraGridBase)ULGData).DataSource).Select(" ItemID= " + frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString()).Length == 0)
					{
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
						object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = frmXYSelection2.dtXYItems.Rows[k]["ItemID"].ToString());
						obj2.Value = value;
						DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
						if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
							((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
						}
						if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
							((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(frmXYSelection2.dtXYItems.Rows[k]["Qty"].ToString());
						if (UsingBatchNoAndValidityPeriod)
						{
							ValueList batchsValueList2 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
						int unitTypeID2 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
						ValueList unitsValueList2 = getUnitsValueList(unitTypeID2);
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList2;
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					}
				}
			}
		}
		CalcTotalQty();
	}

	private void rbIsPsOrder_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblOrderNo;
		bool visible = (((Control)(object)cboOrderNo).Visible = rbIsPsOrder.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblInvoiceNo;
		visible = (((Control)(object)cboInvoiceNo).Visible = rbIsPsInvoice.Checked);
		((Control)(object)obj2).Visible = visible;
		((Control)(object)cboSupplier).Enabled = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)btnSupplierSearch).Visible = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		if (rbIsPsOrder.Checked && Adding)
		{
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			dtPSOrders = PSOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
			DataView dataView = new DataView(dtPSOrders);
			dataView.RowFilter = "Closed=0 And HasPSInvoice =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboOrderNo, dataView.ToTable(), "PSOrderID", "PSOrderNo");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboOrderNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboSupplier.SelectedIndex = -1;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
		}
	}

	private void rbIsPsInvoice_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblInvoiceNo;
		bool visible = (((Control)(object)cboInvoiceNo).Visible = rbIsPsInvoice.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblOrderNo;
		visible = (((Control)(object)cboOrderNo).Visible = rbIsPsOrder.Checked);
		((Control)(object)obj2).Visible = visible;
		((Control)(object)cboSupplier).Enabled = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)btnSupplierSearch).Visible = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		if (rbIsPsInvoice.Checked && Adding)
		{
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			dtPSInvoices = PSInvoices.FillCombo(GlobalVariables.BranchIDs);
			DataView dataView = new DataView(dtPSInvoices);
			dataView.RowFilter = " Closed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboInvoiceNo, dataView.ToTable(), "PSInvoiceID", "PSInvoiceNo");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboOrderNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboSupplier.SelectedIndex = -1;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
		}
	}

	public override void ImportGridData()
	{
		if (!Adding || !rbIsDirect.Checked)
		{
			return;
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "*.xls|*.xlsx";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		empty = openFileDialog.FileName;
		empty2 = Path.GetExtension(empty);
		DataTable dataTable = GlobalFunctions.ReadExcel(empty, empty2);
		StringBuilder stringBuilder = new StringBuilder();
		DataTable dataTable2 = (DataTable)cboStore.DataSource;
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		dtDetails.Rows.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString().Length == 0 || !(decimal.Parse(dataTable.Rows[i][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString()) > 0m))
			{
				continue;
			}
			if (dtItems.Select("ItemBarCode = '" + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? ("هذا الباركود " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود") : "This BarCode Does not Exists");
				stringBuilder.Append("\n");
				continue;
			}
			if (UsingColors && dtColors.Select("ColorName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" لون هذا الصنف  " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
			if (UsingSizes && dtSizes.Select("ItemSizeName = '" + dataTable.Rows[i][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'").Length == 0)
			{
				stringBuilder.Append(GlobalVariables.IsArabic ? (" مقاس هذا الصنف  " + dataTable.Rows[i][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "غير موجود ") : "This Color Does not Exists");
				stringBuilder.Append("\n");
			}
		}
		if (stringBuilder.ToString() != "")
		{
			GlobalVariables.InformationMB.Show(stringBuilder.ToString(), stringBuilder.ToString());
			return;
		}
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			if (dataTable.Rows[j][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString().Length != 0 && decimal.Parse(dataTable.Rows[j][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString()) > 0m)
			{
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				DataRow dataRow = null;
				DataRow dataRow2 = null;
				if (UsingColors)
				{
					dataRow = dtColors.Select("ColorName = '" + dataTable.Rows[j][GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors")].ToString() + "'")[0];
				}
				if (UsingSizes)
				{
					dataRow2 = dtSizes.Select("ItemSizeName = '" + dataTable.Rows[j][GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes")].ToString() + "'")[0];
				}
				DataRow dataRow3 = dtItems.Select("ItemBarCode = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الباركود" : "BarCode"].ToString() + "'")[0];
				UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow3["ItemID"]);
				obj.Value = value;
				if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "الوحدة" : "Unit") && dtUnits.Select("UnitName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'").Length != 0)
				{
					DataRow dataRow4 = dtUnits.Select("UnitName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "الوحدة" : "Unit"].ToString() + "'")[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow4["UnitID"];
				}
				else
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				}
				if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "المخزن" : "Store") && dataTable2 != null && dataTable2.Select("StoreName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "المخزن" : "Store"].ToString() + "'").Length != 0)
				{
					DataRow dataRow5 = dtStores.Select("StoreName = '" + dataTable.Rows[j][GlobalVariables.IsArabic ? "المخزن" : "Store"].ToString() + "'")[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow5["StoreID"];
				}
				if (dataTable.Columns.Contains(GlobalVariables.IsArabic ? "ملاحظات" : "Notes"))
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value = dataTable.Rows[j][GlobalVariables.IsArabic ? "ملاحظات" : "Notes"].ToString();
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataTable.Rows[j][GlobalVariables.IsArabic ? "الكمية" : "Qty"].ToString();
				rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
				}
				int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + dataRow3["UnitID"].ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList = getUnitsValueList(unitTypeID);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
				if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = ((!UsingColors || dataRow == null) ? ((object)1) : dataRow["ColorID"]);
				if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = ((!UsingSizes || dataRow2 == null) ? ((object)1) : dataRow2["ItemSizeID"]);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				CalcTotalQty();
			}
		}
		((UltraGridBase)ULGData).UpdateData();
	}

	private void rbIsDirect_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblInvoiceNo;
		bool visible = (((Control)(object)cboInvoiceNo).Visible = rbIsPsInvoice.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblOrderNo;
		visible = (((Control)(object)cboOrderNo).Visible = rbIsPsOrder.Checked);
		((Control)(object)obj2).Visible = visible;
		((Control)(object)btnVouchersSearch).Visible = !rbIsDirect.Checked;
		((Control)(object)cboSupplier).Enabled = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)btnSupplierSearch).Visible = (Adding || Updating) && rbIsDirect.Checked;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked;
		((Control)(object)btnImport).Visible = rbIsDirect.Checked && Adding;
		if (rbIsDirect.Checked && Adding)
		{
			((Control)(object)btnSelectLenses).Visible = Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItems'")[0]["OptionValue"].ToString()) || Convert.ToBoolean(GlobalVariables.dtSystemOptions.Select("OptionEnName = 'SelectLensesItemsXY'")[0]["OptionValue"].ToString());
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
		}
		else
		{
			((Control)(object)btnSelectLenses).Visible = false;
		}
	}

	private void cboInvoiceNo_ValueChanged(object sender, EventArgs e)
	{
		if (!rbIsPsInvoice.Checked || cboInvoiceNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboSupplier).Value = dtPSInvoices.Select(" PSInvoiceID= " + ((TextEditorControlBase)cboInvoiceNo).Value.ToString())[0]["SubAccountID"].ToString();
		dtPSInvoiceDetails = GoodReceiptNotesDetails.FillByPSInvoiceID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtPSInvoiceDetails.Copy();
		CalcTotalQty();
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
		InitGrid();
		getVoucherItemsValueList(dtPSInvoiceDetails);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		cboStore_ValueChanged(null, null);
	}

	private void cboOrderNo_ValueChanged(object sender, EventArgs e)
	{
		if (!rbIsPsOrder.Checked || cboOrderNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboSupplier).Value = dtPSOrders.Select(" PSOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString())[0]["SubAccountID"].ToString();
		dtPSOrderDetails = GoodReceiptNotesDetails.FillByPSOrderID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtPSOrderDetails.Copy();
		CalcTotalQty();
		CnsStoreID = "-1";
		if (CnsProjectsInstalled)
		{
			DataTable storeByPSOrder = Contracts.GetStoreByPSOrder(((TextEditorControlBase)cboOrderNo).Value.ToString());
			if (storeByPSOrder.Rows.Count > 0)
			{
				CnsStoreID = storeByPSOrder.Rows[0]["StoreID"].ToString();
				((UltraToggleEditorBase)chkStore).Checked = true;
				((TextEditorControlBase)cboStore).Value = CnsStoreID;
			}
		}
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
		InitGrid();
		getVoucherItemsValueList(dtPSOrderDetails);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderDetailID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		cboStore_ValueChanged(null, null);
	}

	private void btnVouchersSearch_Click(object sender, EventArgs e)
	{
		if (rbIsPsInvoice.Checked)
		{
			int num = SearchFunctions.PSInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num;
			}
		}
		else
		{
			int num2 = SearchFunctions.PSOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0, 0);
			if (num2 != 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num2;
			}
		}
	}

	private void cboOrderNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num;
			}
		}
	}

	private void cboInvoiceNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num;
			}
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

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = GoodReceiptNotes.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnPrintBarCode_Click(object sender, EventArgs e)
	{
		if (!(RowID == "") && dtDetails != null)
		{
			frmGenerateBarCode frmGenerateBarCode2 = new frmGenerateBarCode(dtDetails.Copy());
			frmGenerateBarCode2.WindowState = FormWindowState.Normal;
			((Control)(object)frmGenerateBarCode2.lblTitle).Text = (GlobalVariables.IsArabic ? "طباعة باركود" : "BarCode");
			frmGenerateBarCode2.ShowDialog();
		}
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Expected O, but got Unknown
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
				CalcTotalQty();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void AddItemInGid()
	{
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Expected O, but got Unknown
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
			if (text.StartsWith("000"))
			{
				text = "00" + text.TrimStart('0');
			}
			if (flag && flag2)
			{
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				GlobalVariables.InformationMB.Show("هذا الباركود غير موجود", "This BarCode Does not Exists");
				return;
			}
		}
		if (flag)
		{
			DataRow dataRow = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + text + "'")[0];
			text = dtItems.Select("ItemID = " + dataRow["ItemID"].ToString())[0]["ItemBarCode"].ToString();
			string text5 = dataRow["UnitID"].ToString();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3 && ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() == text5)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
					rowIndex = i;
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
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = text5;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
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
		else
		{
			DataRow dataRow2 = dtItems.Select(" ItemBarcode = '" + text + "'")[0];
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[j].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() == text3 && (!bool.Parse(dataRow2["IsUnitPrice"].ToString()) || ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString().Equals(dataRow2["UnitID"].ToString())))
				{
					((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
					rowIndex = j;
					((TextEditorControlBase)txtBarCode).Clear();
					((TextEditorControlBase)txtBarCode).Focus();
					return;
				}
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow2["ItemID"].ToString());
			obj3.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
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
		}
		CalcTotalQty();
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void btnStoreSearch_Click(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkStore).Checked && (Adding || Updating))
		{
			int num = SearchFunctions.Stores(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboStore).Value = num;
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
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Transactions.frmGoodReceiptNotes));
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
		this.rbIsDirect = new System.Windows.Forms.RadioButton();
		this.rbIsPsOrder = new System.Windows.Forms.RadioButton();
		this.rbIsPsInvoice = new System.Windows.Forms.RadioButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboSupplier = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblSupplier = new UltraLabel();
		this.lblInvoiceNo = new UltraLabel();
		this.cboInvoiceNo = new UltraComboEditor();
		this.lblOrderNo = new UltraLabel();
		this.cboOrderNo = new UltraComboEditor();
		this.btnVouchersSearch = new UltraButton();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.btnPrintBarCode = new UltraButton();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.btnStoreSearch = new UltraButton();
		this.btnSelectLenses = new UltraButton();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.btnSupplierSearch = new UltraButton();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOrderNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirect);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsPsOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsPsInvoice);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsDirect, "rbIsDirect");
		this.rbIsDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirect.Name = "rbIsDirect";
		this.rbIsDirect.TabStop = true;
		this.rbIsDirect.UseVisualStyleBackColor = false;
		this.rbIsDirect.CheckedChanged += new System.EventHandler(rbIsDirect_CheckedChanged);
		resources.ApplyResources(this.rbIsPsOrder, "rbIsPsOrder");
		this.rbIsPsOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbIsPsOrder.Name = "rbIsPsOrder";
		this.rbIsPsOrder.TabStop = true;
		this.rbIsPsOrder.UseVisualStyleBackColor = false;
		this.rbIsPsOrder.CheckedChanged += new System.EventHandler(rbIsPsOrder_CheckedChanged);
		resources.ApplyResources(this.rbIsPsInvoice, "rbIsPsInvoice");
		this.rbIsPsInvoice.BackColor = System.Drawing.Color.Transparent;
		this.rbIsPsInvoice.Name = "rbIsPsInvoice";
		this.rbIsPsInvoice.TabStop = true;
		this.rbIsPsInvoice.UseVisualStyleBackColor = false;
		this.rbIsPsInvoice.CheckedChanged += new System.EventHandler(rbIsPsInvoice_CheckedChanged);
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblSupplier.AutoEllipsis = false;
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		this.lblInvoiceNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblInvoiceNo, "lblInvoiceNo");
		((System.Windows.Forms.Control)(object)this.lblInvoiceNo).Name = "lblInvoiceNo";
		((ControlBase)this.lblInvoiceNo).WrapText = false;
		((TextEditorControlBase)this.cboInvoiceNo).AlwaysInEditMode = true;
		this.cboInvoiceNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboInvoiceNo, "cboInvoiceNo");
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).Name = "cboInvoiceNo";
		((TextEditorControlBase)this.cboInvoiceNo).ValueChanged += new System.EventHandler(cboInvoiceNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboInvoiceNo_KeyDown);
		this.lblOrderNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblOrderNo, "lblOrderNo");
		((System.Windows.Forms.Control)(object)this.lblOrderNo).Name = "lblOrderNo";
		((ControlBase)this.lblOrderNo).WrapText = false;
		((TextEditorControlBase)this.cboOrderNo).AlwaysInEditMode = true;
		this.cboOrderNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboOrderNo, "cboOrderNo");
		((System.Windows.Forms.Control)(object)this.cboOrderNo).Name = "cboOrderNo";
		((TextEditorControlBase)this.cboOrderNo).ValueChanged += new System.EventHandler(cboOrderNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboOrderNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboOrderNo_KeyDown);
		((UltraButtonBase)this.btnVouchersSearch).AcceptsFocus = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnVouchersSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnVouchersSearch, "btnVouchersSearch");
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Name = "btnVouchersSearch";
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Click += new System.EventHandler(btnVouchersSearch_Click);
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboStore, "cboStore");
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		((UltraButtonBase)this.btnPrintBarCode).AcceptsFocus = false;
		resources.ApplyResources(this.btnPrintBarCode, "btnPrintBarCode");
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Name = "btnPrintBarCode";
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Click += new System.EventHandler(btnPrintBarCode_Click);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		this.lblBarCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		((UltraButtonBase)this.btnStoreSearch).AcceptsFocus = false;
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Click += new System.EventHandler(btnStoreSearch_Click);
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		((ControlBase)this.btnSelectLenses).Appearance = (AppearanceBase)(object)val12;
		((UltraButtonBase)this.btnSelectLenses).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnSelectLenses).ImageSize = new System.Drawing.Size(1, 1);
		resources.ApplyResources(this.btnSelectLenses, "btnSelectLenses");
		((System.Windows.Forms.Control)(object)this.btnSelectLenses).Name = "btnSelectLenses";
		((System.Windows.Forms.Control)(object)this.btnSelectLenses).Click += new System.EventHandler(btnSelectLenses_Click);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSupplierSearch).Appearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(this.btnSupplierSearch, "btnSupplierSearch");
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Name = "btnSupplierSearch";
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Click += new System.EventHandler(btnSupplierSearch_Click);
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSupplierSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectLenses);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVouchersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmGoodReceiptNotes";
		base.Load += new System.EventHandler(frmGoodReceiptNotes_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVouchersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectLenses, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSupplierSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOrderNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
