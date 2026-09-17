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
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.Sales;
using BusinessLayer.StockControl;
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

namespace ERP.StockControl.Transactions;

public class frmMaterialIssueVouchers : frmHeaderDetails
{
	private DataTable dtBranches;

	private DataTable dtReports;

	private DataTable dtStores;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtBatchs;

	private DataTable dtClients;

	private DataTable dtDepartments;

	private DataTable dtSLInvoices;

	private DataTable dtSLInvoiceDetails;

	private DataTable dtSLOrders;

	private DataTable dtSLOrderDetails;

	private DataTable dtMaterialIssueRequest;

	private DataTable dtMaterialIssueRequestDetails;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtItemBalance = new DataTable();

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private int rowIndex = -1;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool IsMS_MIV = false;

	private string OperationID;

	private string OnerID;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsRequest;

	private RadioButton rbIsSLInvoice;

	private UltraLabel lblClient;

	private UltraLabel lblInvoiceNo;

	private UltraComboEditor cboInvoiceNo;

	private UltraLabel lblMIVRequestNo;

	private UltraComboEditor cboMaterialIssueVoucherRequestNo;

	public UltraButton btnVouchersSearch;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private RadioButton rbIsDirect;

	private RadioButton rbIsDepartment;

	private UltraLabel lblDepartment;

	private UltraComboEditor cboDepartment;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	public UltraButton btnClientSearch;

	private UltraCheckEditor chkAllStok;

	public UltraButton btnStoreSearch;

	public UltraButton btnDepartmentSearch;

	public UltraButton btnStockBalance;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	private RadioButton rbIsSLOrder;

	private UltraLabel lblOrderNo;

	private UltraComboEditor cboOrderNo;

	public frmMaterialIssueVouchers()
	{
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
		TableName = "SC_MaterialIssueVouchers";
		IDCol = "MaterialIssueVoucherID";
		NoCol = "MaterialIssueVoucherNo";
		DateCol = "MaterialIssueVoucherDate";
	}

	public frmMaterialIssueVouchers(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmMaterialIssueVouchers(string operationID, string onerID)
		: this()
	{
		IsMS_MIV = true;
		OperationID = operationID;
		OnerID = onerID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
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
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.AgentSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.CaptainSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.SeaManSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.ClientSubAccountTypeIDs.Remove(0, 1), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtSLInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "1", "1", "1");
		GlobalFunctions.FillCombo(cboInvoiceNo, dtSLInvoices, "SLInvoiceID", "SLInvoiceNo");
		dtSLOrders = SLOrders.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboOrderNo, dtSLOrders, "SLOrderID", "SLOrderNo");
		dtMaterialIssueRequest = MaterialIssueRequest.FillCombo(GlobalVariables.BranchIDs, "1");
		GlobalFunctions.FillCombo(cboMaterialIssueVoucherRequestNo, dtMaterialIssueRequest, "MaterialIssueRequestID", "MaterialIssueRequestNo");
		dtDepartments = Departments.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDepartment, dtDepartments, "DepartmentID", "DepartmentName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtDetails = MaterialIssueVouchersDetails.SelectByMaterialIssueVoucherID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		if (IsMS_MIV)
		{
			btnAddClick();
			((Control)(object)pnlCheckType).Visible = false;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDetailID"].DefaultCellValue = -1;
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Header).Caption = (GlobalVariables.IsArabic ? "الرصيد" : "Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StockBalance"].Hidden = false;
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
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		if (IsMS_MIV)
		{
			btnAddClick();
			((TextEditorControlBase)cboClient).Value = OnerID;
			dtDetails = OperationsItems.SelectToMIV(OperationID, GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Column.ValueList.ItemCount > 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Column.ValueList.GetValue(0);
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = MaterialIssueVouchers.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			GlobalFunctions.FillCombo(cboInvoiceNo, dtSLInvoices, "SLInvoiceID", "SLInvoiceNo");
			GlobalFunctions.FillCombo(cboOrderNo, dtSLOrders, "SLOrderID", "SLOrderNo");
			GlobalFunctions.FillCombo(cboMaterialIssueVoucherRequestNo, dtMaterialIssueRequest, "MaterialIssueRequestID", "MaterialIssueRequestNo");
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["MaterialIssueVoucherNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["MaterialIssueVoucherDate"];
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			rbIsDirect.Checked = bool.Parse(drMaster["IsDirect"].ToString());
			rbIsSLInvoice.Checked = bool.Parse(drMaster["IsSLInvoice"].ToString());
			rbIsRequest.Checked = bool.Parse(drMaster["IsRequest"].ToString());
			rbIsSLOrder.Checked = bool.Parse(drMaster["IsSLOrder"].ToString());
			rbIsDepartment.Checked = bool.Parse(drMaster["IsDepartment"].ToString());
			((TextEditorControlBase)cboInvoiceNo).Value = drMaster["SLInvoiceID"];
			((TextEditorControlBase)cboOrderNo).Value = drMaster["SLOrderID"];
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value = drMaster["MaterialIssueRequestID"];
			((TextEditorControlBase)cboDepartment).Value = drMaster["DepartmentID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = MaterialIssueVouchersDetails.SelectByMaterialIssueVoucherID(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
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
		rbIsDirect.Enabled = !NavMode && !Updating;
		rbIsSLInvoice.Enabled = !NavMode && !Updating;
		rbIsRequest.Enabled = !NavMode && !Updating;
		rbIsSLOrder.Enabled = !NavMode && !Updating;
		rbIsDepartment.Enabled = !NavMode && !Updating;
		((EditorButtonControlBase)cboInvoiceNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboOrderNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboMaterialIssueVoucherRequestNo).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboDepartment).ReadOnly = rbIsRequest.Checked || NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = rbIsSLInvoice.Checked || rbIsSLOrder.Checked || NavMode;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = rbIsDirect.Checked && !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked || rbIsDepartment.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked || rbIsDepartment.Checked;
		((Control)(object)btnVouchersSearch).Visible = Adding && (rbIsSLInvoice.Checked || rbIsRequest.Checked || rbIsSLOrder.Checked);
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)btnClientSearch).Visible = rbIsDirect.Checked && !NavMode;
		((Control)(object)btnDepartmentSearch).Visible = rbIsDepartment.Checked && !NavMode;
		((Control)(object)btnStoreSearch).Visible = !NavMode;
		((Control)(object)chkAllStok).Visible = !NavMode;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
		((Control)(object)btnStockBalance).Visible = Adding || Updating;
		((Control)(object)btnImport).Visible = Adding;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
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
			DataView dataView2 = new DataView(dtSLOrders);
			dataView2.RowFilter = "Closed=0 And  HasSLInvoice =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboOrderNo, dataView2.ToTable(), "SLOrderID", "SLOrderNo");
		}
		else
		{
			int num = 0;
			if (cboOrderNo.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			GlobalFunctions.FillCombo(cboOrderNo, dtSLOrders, "SLOrderID", "SLOrderNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num;
			}
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
		}
		if (Adding)
		{
			DataView dataView3 = new DataView(dtItems);
			dataView3.RowFilter = " IsActive =1  " + (rbIsDirect.Checked ? " And IsSalesItem=1" : "");
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
			int num2 = 0;
			int num3 = 0;
			if (cboInvoiceNo.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboInvoiceNo).Value.ToString());
			}
			if (cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
			{
				num3 = int.Parse(((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString());
			}
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			GlobalFunctions.FillCombo(cboInvoiceNo, dtSLInvoices, "SLInvoiceID", "SLInvoiceNo");
			GlobalFunctions.FillCombo(cboMaterialIssueVoucherRequestNo, dtMaterialIssueRequest, "MaterialIssueRequestID", "MaterialIssueRequestNo");
			if (num2 > 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num2;
			}
			if (num3 > 0)
			{
				((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value = num3;
			}
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
			if ((rbIsDirect.Checked || rbIsDepartment.Checked) && Updating)
			{
				DataView dataView4 = new DataView(dtItems);
				dataView4.RowFilter = (rbIsDirect.Checked ? " IsSalesItem=1" : "");
				DataTable dataTable3 = dataView4.ToTable();
				vlItems.ValueListItems.Clear();
				vlBarCode.ValueListItems.Clear();
				for (int l = 0; l < dataTable3.Rows.Count; l++)
				{
					vlItems.ValueListItems.Add(dataTable3.Rows[l]["ItemID"], dataTable3.Rows[l]["Name"].ToString());
					vlBarCode.ValueListItems.Add(dataTable3.Rows[l]["ItemID"], dataTable3.Rows[l]["ItemBarCode"].ToString());
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
			}
		}
		if (!Updating)
		{
			return;
		}
		for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; n++)
		{
			if (((UltraGridBase)ULGData).Rows[n].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[n].Cells["ItemID"].Value.ToString())[0];
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
				((UltraGridBase)ULGData).Rows[n].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[n].Cells["BatchID"].Value = DBNull.Value;
				}
			}
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].Value = DBNull.Value;
				}
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].Value = DBNull.Value;
				}
			}
		}
		if (rbIsSLInvoice.Checked && cboInvoiceNo.SelectedIndex > -1)
		{
			dtSLInvoiceDetails = MaterialIssueVouchersDetails.FillBySLInvoiceID(RowID, ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			getVoucherItemsValueList(dtSLInvoiceDetails);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		}
		else if (rbIsSLOrder.Checked && cboOrderNo.SelectedIndex > -1)
		{
			dtSLOrderDetails = MaterialIssueVouchersDetails.FillBySLOrderID(RowID, ((TextEditorControlBase)cboOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			getVoucherItemsValueList(dtSLOrderDetails);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		}
		else if (rbIsRequest.Checked && cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
		{
			dtMaterialIssueRequestDetails = MaterialIssueVouchersDetails.FillByMaterialIssueRequestID(RowID, ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			getVoucherItemsValueList(dtMaterialIssueRequestDetails);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
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
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		dtDetails.Rows.Clear();
		DataTable dataTable2 = (DataTable)cboStore.DataSource;
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
			}
		}
		((UltraGridBase)ULGData).UpdateData();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? MaterialIssueVouchers.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboInvoiceNo.SelectedIndex = -1;
		cboOrderNo.SelectedIndex = -1;
		cboMaterialIssueVoucherRequestNo.SelectedIndex = -1;
		cboDepartment.SelectedIndex = -1;
		cboClient.SelectedIndex = -1;
		rbIsDirect.Checked = true;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkAllStok).Checked = false;
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
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
		if (rbIsSLInvoice.Checked && cboInvoiceNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم  فاتورة المبيعات" : "Please Select The Sales Invoice Number");
			((TextEditorControlBase)cboInvoiceNo).Focus();
			cboInvoiceNo.DropDown();
			return false;
		}
		if (rbIsSLOrder.Checked && cboOrderNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم  أمر البيع " : "Please Select The Sales Order");
			((TextEditorControlBase)cboOrderNo).Focus();
			cboOrderNo.DropDown();
			return false;
		}
		if (rbIsRequest.Checked && cboMaterialIssueVoucherRequestNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم  طلب الصرف" : "Please Select The Sales Order Number");
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Focus();
			cboMaterialIssueVoucherRequestNo.DropDown();
			return false;
		}
		if (cboClient.SelectedIndex == -1 && (rbIsDirect.Checked || rbIsSLInvoice.Checked || rbIsSLOrder.Checked))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboDepartment.SelectedIndex == -1 && (rbIsDepartment.Checked || rbIsRequest.Checked))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار القسم" : "Please Select Department");
			((TextEditorControlBase)cboDepartment).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SC_MaterialIssueVouchers", "MaterialIssueVoucherNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["MaterialIssueVoucherNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = MaterialIssueVouchers.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
				ValidateSLInvoiceDetailAllowedQty(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceDetailID"].Value.ToString()));
			}
			if (cboOrderNo.SelectedIndex > -1)
			{
				ValidateSLOrderDetailAllowedQty(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SLOrderDetailID"].Value.ToString()));
			}
			else if (cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
			{
				ValidateMaterialIssueVoucherRequestDetailAllowedQty(int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["MaterialIssueRequestDetailID"].Value.ToString()));
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (rbIsSLInvoice.Checked || rbIsSLOrder.Checked)
			{
				continue;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = MaterialIssueVouchers.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsRequest.Checked ? ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString() : "Null", rbIsSLInvoice.Checked ? ((TextEditorControlBase)cboInvoiceNo).Value.ToString() : "Null", (rbIsDepartment.Checked || rbIsRequest.Checked) ? ((TextEditorControlBase)cboDepartment).Value.ToString() : "Null", (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), rbIsSLOrder.Checked ? ((TextEditorControlBase)cboOrderNo).Value.ToString() : "Null", rbIsSLInvoice.Checked ? "1" : "0", rbIsRequest.Checked ? "1" : "0", rbIsDirect.Checked ? "1" : "0", rbIsDepartment.Checked ? "1" : "0", rbIsSLOrder.Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "MIV", "MIV");
			DataTable dataTable = ((DataTable)((UltraGridBase)ULGData).DataSource).Copy();
			dataTable.AcceptChanges();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				dataTable.Rows[i].SetAdded();
			}
			MaterialIssueVouchersDetails.Insert_UpdateByTableXML(dataTable, "MaterialIssueVoucherDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (rbIsSLOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				Main.ExecuteNonQuery(" Update SL_SLOrders Set IsMIVFirst=1 where SLOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
			if (IsMS_MIV)
			{
				OperationsMaterialIssueVouchers.Insert_Update("-1", OperationID, num.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				OperationsItems.UpdateDeliverdQty(OperationID);
			}
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "MIV", "MIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "MIV", "MIV");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				RowID = num.ToString();
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = MaterialIssueVouchers.Insert_Update(drMaster["MaterialIssueVoucherID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsRequest.Checked ? ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString() : "Null", rbIsSLInvoice.Checked ? ((TextEditorControlBase)cboInvoiceNo).Value.ToString() : "Null", (rbIsDepartment.Checked || rbIsRequest.Checked) ? ((TextEditorControlBase)cboDepartment).Value.ToString() : "Null", (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), rbIsSLOrder.Checked ? ((TextEditorControlBase)cboOrderNo).Value.ToString() : "Null", rbIsSLInvoice.Checked ? "1" : "0", rbIsRequest.Checked ? "1" : "0", rbIsDirect.Checked ? "1" : "0", rbIsDepartment.Checked ? "1" : "0", rbIsSLOrder.Checked ? "1" : "0", ((Control)(object)txtNotes).Text, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "MIV", "MIV");
			((UltraGridBase)ULGData).UpdateData();
			MaterialIssueVouchersDetails.Insert_UpdateByTableXML((DataTable)((UltraGridBase)ULGData).DataSource, "MaterialIssueVoucherDetailID", num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='MarineService'")[0]["Installed"]))
			{
				DataTable dataTable = OperationsMaterialIssueVouchers.SelectByMaterialIssueVoucherID(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.UserID);
				if (dataTable.Rows.Count > 0)
				{
					OperationsItems.UpdateDeliverdQty(dataTable.Rows[0]["OperationID"].ToString());
				}
			}
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "MIV", "MIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "MIV", "MIV");
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
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			MaterialIssueVouchers.DeleteVirtual(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.UserID);
			MaterialIssueVouchersDetails.DeleteVirtualByMaterialIssueVoucherID(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.UserID);
			if (rbIsRequest.Checked && cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
			{
				Main.ExecuteNonQuery(" Update PS_PSOrders Set IsGRNFirst=0 where PSOrderID= " + ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString());
			}
			if (rbIsSLOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				Main.ExecuteNonQuery(" Update SL_SLOrders Set IsMIVFirst=0 where SLOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString());
			}
			if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='MarineService'")[0]["Installed"]))
			{
				DataTable dataTable = OperationsMaterialIssueVouchers.SelectByMaterialIssueVoucherID(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.UserID);
				OperationsMaterialIssueVouchers.DeleteVirtualByMaterialIssueVoucherID(drMaster["MaterialIssueVoucherID"].ToString(), GlobalVariables.UserID);
				if (dataTable.Rows.Count > 0)
				{
					OperationsItems.UpdateDeliverdQty(dataTable.Rows[0]["OperationID"].ToString());
				}
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SC_MaterialIssueVouchers_A.rpt" : "Rep_SC_MaterialIssueVouchers_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@MaterialIssueVoucherIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.MaterialIssueVouchersReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["MaterialIssueVoucherID"].ToString();
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
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSLInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "1", "1", "1");
		DataView dataView = new DataView(dtSLInvoices);
		dataView.RowFilter = " Closed=0 And WithoutMIV = 0 And BranchID= " + GlobalVariables.CurrentBranchID;
		GlobalFunctions.FillCombo(cboInvoiceNo, dataView.ToTable(), "SLInvoiceID", "SLInvoiceNo");
		dtMaterialIssueRequest = MaterialIssueRequest.FillCombo(GlobalVariables.BranchIDs, "1");
		DataView dataView2 = new DataView(dtMaterialIssueRequest);
		dataView2.RowFilter = "Completed=0 And BranchID= " + GlobalVariables.CurrentBranchID;
		GlobalFunctions.FillCombo(cboMaterialIssueVoucherRequestNo, dataView2.ToTable(), "MaterialIssueRequestID", "MaterialIssueRequestNo");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView3 = new DataView(dtStores);
		dataView3.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView3.ToTable();
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dataTable.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[l]["StoreID"], dataTable.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dataTable, "StoreID", "StoreName");
		DataView dataView4 = new DataView(dtItems);
		dataView4.RowFilter = " IsActive =" + (Adding ? "1" : "0") + "  And IsSalesItem=" + ((rbIsDirect.Checked || rbIsDepartment.Checked) ? "1" : "0");
		DataTable dataTable2 = dataView4.ToTable();
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int m = 0; m < dataTable2.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.AgentSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.CaptainSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.SeaManSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.ClientSubAccountTypeIDs.Remove(0, 1), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView5 = new DataView(dtClients);
			dataView5.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView5.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		}
		dtDepartments = Departments.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDepartment, dtDepartments, "DepartmentID", "DepartmentName");
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
		if (rbIsDirect.Checked && drMaster["SLInvoiceID"] != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة مبيعات على إذن الصرف برجاء حذف الفاتورة اولا", "Cannot Delete This Transaction Because Sales Invoice Was Made on Material issue Voucher Please Delete Sales Invoice First   ");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ReturnedQty"].Value.ToString()) > 0m)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود مرتجع  على هذه الحركة", "Cannot Delete This Transaction Because Return Was Made on Good Receipt note Please Delete Supplier Return First   ");
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
		if (rbIsDirect.Checked && drMaster["SLInvoiceID"] != DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة مبيعات على إذن الصرف برجاء حذف الفاتورة اولا", "Cannot Update This Transaction Because Sales Invoice Was Made on Material issue Voucher Please Delete Sales Invoice First   ");
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
			val.ValueListItems.Add((object)dtVoucher.Rows[i][rbIsSLInvoice.Checked ? "SLInvoiceDetailID" : (rbIsSLOrder.Checked ? "SLorderDetailID" : "MaterialIssueRequestDetailID")].ToString(), dtVoucher.Rows[i]["ItemName"].ToString());
			val2.ValueListItems.Add((object)dtVoucher.Rows[i][rbIsSLInvoice.Checked ? "SLInvoiceDetailID" : (rbIsSLOrder.Checked ? "SLorderDetailID" : "MaterialIssueRequestDetailID")].ToString(), dtVoucher.Rows[i]["BarCode"].ToString());
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[rbIsSLInvoice.Checked ? "SLInvoiceDetailID" : (rbIsSLOrder.Checked ? "SLorderDetailID" : "MaterialIssueRequestDetailID")].ValueList = (IValueList)(object)val;
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
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SLInvoiceDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SLOrderDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DetailBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MaterialIssueRequestDetailID") && (rbIsSLInvoice.Checked || rbIsSLOrder.Checked || rbIsRequest.Checked))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID" && ((UltraToggleEditorBase)chkStore).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (cboInvoiceNo.SelectedIndex == -1 && cboOrderNo.SelectedIndex == -1 && cboMaterialIssueVoucherRequestNo.SelectedIndex == -1 && (rbIsSLInvoice.Checked || rbIsSLOrder.Checked || rbIsRequest.Checked))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StockBalance")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private decimal ValidateSLOrderDetailAllowedQty(int RowID)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SLOrderDetailID"].Value.ToString()) == RowID)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		num2 = decimal.Parse(dtSLOrderDetails.Select(" SLOrderDetailID= " + RowID)[0]["AllowedQty"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية المنصرفة " + Math.Round(num2, 3)) : ("Maximum Issued Qty " + num2));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2 - num + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			return decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		return num2 - num;
	}

	private decimal ValidateSLInvoiceDetailAllowedQty(int RowID)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SLInvoiceDetailID"].Value.ToString()) == RowID)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		num2 = decimal.Parse(dtSLInvoiceDetails.Select(" SLInvoiceDetailID= " + RowID)[0]["AllowedQty"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية المنصرفة " + Math.Round(num2, 3)) : ("Maximum Issued Qty " + num2));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2 - num + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			return decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		return num2 - num;
	}

	private decimal ValidateMaterialIssueVoucherRequestDetailAllowedQty(int RowID)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["MaterialIssueRequestDetailID"].Value.ToString()) == RowID)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
		}
		num2 = decimal.Parse(dtMaterialIssueRequestDetails.Select(" MaterialIssueRequestDetailID= " + RowID)[0]["AllowedQty"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  الحد الاقصى للكمية المنصرفة " + Math.Round(num2, 3)) : ("Maximum Issued Qty " + num2));
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = num2 - num + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			return decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		return num2 - num;
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (e.Cell != null && e.Cell.Value != DBNull.Value && (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" || ((KeyedSubObjectBase)e.Cell.Column).Key == "SLInvoiceDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "SLOrderDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "MaterialIssueRequestDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "DetailBarCode"))
		{
			if (cboInvoiceNo.SelectedIndex > -1)
			{
				ValidateSLInvoiceDetailAllowedQty(int.Parse(e.Cell.Row.Cells["SLInvoiceDetailID"].Value.ToString()));
			}
			if (cboOrderNo.SelectedIndex > -1)
			{
				ValidateSLOrderDetailAllowedQty(int.Parse(e.Cell.Row.Cells["SLOrderDetailID"].Value.ToString()));
			}
			else if (cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
			{
				ValidateMaterialIssueVoucherRequestDetailAllowedQty(int.Parse(e.Cell.Row.Cells["MaterialIssueRequestDetailID"].Value.ToString()));
			}
			if (decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString()) < decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ("  لايمكن تقليل الكمية عن الكمية المرتجعة و قدرها " + Math.Round(decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString()), 3)) : ("Issued Qty Cannot Decrease from Returned Qty " + decimal.Parse(e.Cell.Row.Cells["ReturnedQty"].Value.ToString())));
				e.Cell.Row.Cells["Qty"].Value = e.Cell.Row.Cells["ReturnedQty"].Value;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_125f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1269: Expected O, but got Unknown
		//IL_1277: Unknown result type (might be due to invalid IL or missing references)
		//IL_1281: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "SLInvoiceDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "SLOrderDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "MaterialIssueRequestDetailID" || ((KeyedSubObjectBase)e.Cell.Column).Key == "DetailBarCode") && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && (rbIsRequest.Checked || rbIsSLInvoice.Checked || rbIsSLOrder.Checked))
		{
			int num = 0;
			if (rbIsSLInvoice.Checked && cboInvoiceNo.SelectedIndex > -1)
			{
				num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtSLInvoiceDetails.Select(" SLInvoiceDetailID =" + e.Cell.Value.ToString())[0]["ItemID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitID"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtSLInvoiceDetails.Select(" SLInvoiceDetailID =" + e.Cell.Value.ToString())[0]["UnitID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitPrice"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? decimal.Parse(dtSLInvoiceDetails.Select(" SLInvoiceDetailID =" + e.Cell.Value.ToString())[0]["UnitPrice"].ToString()) : 0m);
				UltraGridCell obj = e.Cell.Row.Cells["SLInvoiceDetailID"];
				object value = (e.Cell.Row.Cells["DetailBarCode"].Value = e.Cell.Value);
				obj.Value = value;
				e.Cell.Row.Cells["Qty"].Value = 0;
				e.Cell.Row.Cells["Qty"].Value = ValidateSLInvoiceDetailAllowedQty(int.Parse(e.Cell.Value.ToString()));
			}
			else if (rbIsSLOrder.Checked && cboOrderNo.SelectedIndex > -1)
			{
				num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtSLInvoiceDetails.Select(" SLOrderDetailID =" + e.Cell.Value.ToString())[0]["ItemID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitID"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtSLInvoiceDetails.Select(" SLOrderDetailID =" + e.Cell.Value.ToString())[0]["UnitID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitPrice"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? decimal.Parse(dtSLInvoiceDetails.Select(" SLOrderDetailID =" + e.Cell.Value.ToString())[0]["UnitPrice"].ToString()) : 0m);
				UltraGridCell obj2 = e.Cell.Row.Cells["SLOrderDetailID"];
				object value = (e.Cell.Row.Cells["DetailBarCode"].Value = e.Cell.Value);
				obj2.Value = value;
				e.Cell.Row.Cells["Qty"].Value = 0;
				e.Cell.Row.Cells["Qty"].Value = ValidateSLOrderDetailAllowedQty(int.Parse(e.Cell.Value.ToString()));
			}
			else if (rbIsRequest.Checked && cboMaterialIssueVoucherRequestNo.SelectedIndex > -1)
			{
				num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtMaterialIssueRequestDetails.Select(" MaterialIssueRequestDetailID =" + e.Cell.Value.ToString())[0]["ItemID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitID"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtMaterialIssueRequestDetails.Select(" MaterialIssueRequestDetailID =" + e.Cell.Value.ToString())[0]["UnitID"].ToString()) : 0);
				e.Cell.Row.Cells["UnitPrice"].Value = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? decimal.Parse(dtMaterialIssueRequestDetails.Select(" MaterialIssueRequestDetailID =" + e.Cell.Value.ToString())[0]["UnitPrice"].ToString()) : 0m);
				UltraGridCell obj3 = e.Cell.Row.Cells["MaterialIssueRequestDetailID"];
				object value = (e.Cell.Row.Cells["DetailBarCode"].Value = e.Cell.Value);
				obj3.Value = value;
				e.Cell.Row.Cells["Qty"].Value = 0;
				e.Cell.Row.Cells["Qty"].Value = ValidateMaterialIssueVoucherRequestDetailAllowedQty(int.Parse(e.Cell.Value.ToString()));
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
				e.Cell.Row.Cells["SLInvoiceDetailID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SLOrderDetailID"].Value = DBNull.Value;
				e.Cell.Row.Cells["MaterialIssueRequestDetailID"].Value = DBNull.Value;
				e.Cell.Row.Cells["DetailBarCode"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemID"].Value = DBNull.Value;
				e.Cell.Row.Cells["BatchID"].ValueList = null;
				e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
			}
		}
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && (rbIsDepartment.Checked || rbIsDirect.Checked))
		{
			UltraGridCell obj4 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj4.Value = value;
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
				UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num4);
				obj5.Value = value;
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
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && (rbIsDirect.Checked || rbIsDepartment.Checked))
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
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DetailBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SLInvoiceDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SLOrderDetailID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MaterialIssueRequestDetailID") && (rbIsRequest.Checked || rbIsSLInvoice.Checked || rbIsSLOrder.Checked))
		{
			if (ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj10 = ((UltraGridBase)ULGData).ActiveRow.Cells["MaterialIssueRequestDetailID"];
				UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["SLInvoiceDetailID"];
				UltraGridCell obj12 = ((UltraGridBase)ULGData).ActiveRow.Cells["SLOrderDetailID"];
				UltraGridCell obj13 = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"];
				UltraGridCell obj14 = ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"];
				object obj15 = (((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"].Value = DBNull.Value);
				object obj16 = (obj14.Value = obj15);
				object obj4 = (obj13.Value = obj16);
				object obj5 = (obj12.Value = obj4);
				object value2 = (obj11.Value = obj5);
				obj10.Value = value2;
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			}
			else if (rbIsSLInvoice.Checked && dtSLInvoiceDetails.Select(" SLInvoiceDetailID =" + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell obj21 = ((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"];
				UltraGridCell obj22 = ((UltraGridBase)ULGData).ActiveRow.Cells["SLInvoiceDetailID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj22.Value = obj5);
				obj21.Value = value2;
			}
			else if (rbIsSLOrder.Checked && dtSLInvoiceDetails.Select(" SLOrderDetailID =" + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell obj24 = ((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"];
				UltraGridCell obj25 = ((UltraGridBase)ULGData).ActiveRow.Cells["SLOrderDetailID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj25.Value = obj5);
				obj24.Value = value2;
			}
			else if (rbIsRequest.Checked && dtMaterialIssueRequestDetails.Select(" MaterialIssueRequestDetailID =" + ULGData.ActiveCell.Value.ToString()).Length == 0)
			{
				UltraGridCell obj27 = ((UltraGridBase)ULGData).ActiveRow.Cells["DetailBarCode"];
				UltraGridCell obj28 = ((UltraGridBase)ULGData).ActiveRow.Cells["MaterialIssueRequestDetailID"];
				object obj5 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
				object value2 = (obj28.Value = obj5);
				obj27.Value = value2;
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
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (rbIsDepartment.Checked || rbIsDirect.Checked))
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
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if ((Adding || Updating) && (rbIsDepartment.Checked || rbIsDirect.Checked) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
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

	private void btnStockBalance_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاءإختيار المخزن  ", "Please Select Store ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value)
			{
				if (UsingColors)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = "1";
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value)
			{
				if (UsingSizes)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = "1";
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return;
			}
		}
		StringWriter stringWriter = new StringWriter();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
		dataView.ToTable("Table", false, "ItemID", "ColorID", "ItemSizeID", "BatchID", "UnitID", "StoreID").WriteXml((TextWriter)stringWriter);
		DataTable itemAllowedQtyByItemIDs = Items.GetItemAllowedQtyByItemIDs(stringWriter.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			DataRow[] array = itemAllowedQtyByItemIDs.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() + " And StoreID= " + ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString() + " And ColorID =" + ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() + " And ItemSizeID= " + ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString() + " And BatchID" + ((((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value == DBNull.Value) ? " is null " : ("=" + ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString())) + " And UnitID=" + ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString());
			if (array.Length != 0)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["StockBalance"].Value = array[0]["Balance"].ToString();
			}
		}
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void cboOrderNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.SLOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num;
			}
		}
	}

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		}
		dtDetails.Rows.Clear();
	}

	private void cboOrderNo_ValueChanged(object sender, EventArgs e)
	{
		if (!rbIsSLOrder.Checked || cboOrderNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboClient).Value = dtSLOrders.Select(" SLOrderID= " + ((TextEditorControlBase)cboOrderNo).Value.ToString())[0]["SubAccountID"].ToString();
		dtSLOrderDetails = MaterialIssueVouchersDetails.FillBySLOrderID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtSLOrderDetails.Copy();
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
		getVoucherItemsValueList(dtSLOrderDetails);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		cboStore_ValueChanged(null, null);
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + " ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblMIVRequestNo;
		bool visible = (((Control)(object)cboMaterialIssueVoucherRequestNo).Visible = rbIsRequest.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblInvoiceNo;
		visible = (((Control)(object)cboInvoiceNo).Visible = rbIsSLInvoice.Checked);
		((Control)(object)obj2).Visible = visible;
		UltraLabel obj3 = lblOrderNo;
		visible = (((Control)(object)cboOrderNo).Visible = rbIsSLOrder.Checked);
		((Control)(object)obj3).Visible = visible;
		UltraLabel obj4 = lblDepartment;
		visible = (((Control)(object)cboDepartment).Visible = rbIsDepartment.Checked || rbIsRequest.Checked);
		((Control)(object)obj4).Visible = visible;
		UltraLabel obj5 = lblClient;
		visible = (((Control)(object)cboClient).Visible = rbIsSLInvoice.Checked || rbIsSLOrder.Checked || rbIsDirect.Checked);
		((Control)(object)obj5).Visible = visible;
		UltraCheckEditor obj6 = chkBranches;
		visible = (((Control)(object)cboBranches).Visible = rbIsSLInvoice.Checked || rbIsSLOrder.Checked || rbIsDirect.Checked);
		((Control)(object)obj6).Visible = visible;
		((EditorButtonControlBase)cboClient).ReadOnly = (!Adding && !Updating) || rbIsSLInvoice.Checked || rbIsSLOrder.Checked;
		((Control)(object)btnImport).Visible = rbIsDirect.Checked && Adding;
		((EditorButtonControlBase)cboDepartment).ReadOnly = rbIsRequest.Checked;
		((Control)(object)txtBarCode).Visible = rbIsDirect.Checked || rbIsDepartment.Checked;
		((Control)(object)lblBarCode).Visible = rbIsDirect.Checked || rbIsDepartment.Checked;
		((Control)(object)btnVouchersSearch).Visible = Adding && (rbIsSLInvoice.Checked || rbIsRequest.Checked || rbIsSLOrder.Checked);
		((Control)(object)btnClientSearch).Visible = rbIsDirect.Checked && (Adding || Updating);
		((Control)(object)btnBranchesSearch).Visible = rbIsDirect.Checked && (Adding || Updating);
		((Control)(object)btnDepartmentSearch).Visible = rbIsDepartment.Checked && (Adding || Updating);
		if (rbIsRequest.Checked && Adding)
		{
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			dtMaterialIssueRequest = MaterialIssueRequest.FillCombo(GlobalVariables.BranchIDs, "1");
			DataView dataView = new DataView(dtMaterialIssueRequest);
			dataView.RowFilter = "Completed=0  And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboMaterialIssueVoucherRequestNo, dataView.ToTable(), "MaterialIssueRequestID", "MaterialIssueRequestNo");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboMaterialIssueVoucherRequestNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboOrderNo.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
		}
		else if (rbIsSLInvoice.Checked && Adding)
		{
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			dtSLInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "1", "1", "1");
			DataView dataView2 = new DataView(dtSLInvoices);
			dataView2.RowFilter = " Closed=0 And WithoutMIV = 0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboInvoiceNo, dataView2.ToTable(), "SLInvoiceID", "SLInvoiceNo");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboMaterialIssueVoucherRequestNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboOrderNo.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
		}
		else if (rbIsSLOrder.Checked && Adding)
		{
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboOrderNo).ValueChanged -= cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			dtSLOrders = SLOrders.FillCombo(GlobalVariables.BranchIDs, "1");
			DataView dataView3 = new DataView(dtSLOrders);
			dataView3.RowFilter = " Closed=0 And HasSLInvoice = 0 and  BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboOrderNo, dataView3.ToTable(), "SLOrderID", "SLOrderNo");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboMaterialIssueVoucherRequestNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboOrderNo.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboOrderNo).ValueChanged += cboOrderNo_ValueChanged;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
		}
		else
		{
			if (!Adding && !Updating)
			{
				return;
			}
			((TextEditorControlBase)cboInvoiceNo).ValueChanged -= cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged -= cboMaterialIssueVoucherRequestNo_ValueChanged;
			if ((rbIsDirect.Checked || rbIsDepartment.Checked) && Adding)
			{
				DataView dataView4 = new DataView(dtItems);
				dataView4.RowFilter = " IsActive =1  " + (rbIsDirect.Checked ? " And IsSalesItem=1" : "");
				vlItems.ValueListItems.Clear();
				vlBarCode.ValueListItems.Clear();
				DataTable dataTable = dataView4.ToTable();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
					vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
				}
			}
			else if ((rbIsDirect.Checked || rbIsDepartment.Checked) && Updating)
			{
				DataView dataView5 = new DataView(dtItems);
				dataView5.RowFilter = (rbIsDirect.Checked ? " And IsSalesItem=1" : "");
				vlItems.ValueListItems.Clear();
				vlBarCode.ValueListItems.Clear();
				for (int j = 0; j < dataView5.ToTable().Rows.Count; j++)
				{
					vlItems.ValueListItems.Add(dataView5.ToTable().Rows[j]["ItemID"], dataView5.ToTable().Rows[j]["Name"].ToString());
					vlBarCode.ValueListItems.Add(dataView5.ToTable().Rows[j]["ItemID"], dataView5.ToTable().Rows[j]["ItemBarCode"].ToString());
				}
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = true;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			cboMaterialIssueVoucherRequestNo.SelectedIndex = -1;
			cboInvoiceNo.SelectedIndex = -1;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboInvoiceNo).ValueChanged += cboInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).ValueChanged += cboMaterialIssueVoucherRequestNo_ValueChanged;
		}
	}

	private void cboInvoiceNo_ValueChanged(object sender, EventArgs e)
	{
		if (!rbIsSLInvoice.Checked || cboInvoiceNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboClient).Value = dtSLInvoices.Select(" SLInvoiceID= " + ((TextEditorControlBase)cboInvoiceNo).Value.ToString())[0]["SubAccountID"].ToString();
		dtSLInvoiceDetails = MaterialIssueVouchersDetails.FillBySLInvoiceID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtSLInvoiceDetails.Copy();
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
		getVoucherItemsValueList(dtSLInvoiceDetails);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		cboStore_ValueChanged(null, null);
	}

	private void cboMaterialIssueVoucherRequestNo_ValueChanged(object sender, EventArgs e)
	{
		if (!rbIsRequest.Checked || cboMaterialIssueVoucherRequestNo.SelectedIndex <= -1 || !Adding)
		{
			return;
		}
		((TextEditorControlBase)cboDepartment).Value = dtMaterialIssueRequest.Select(" MaterialIssueRequestID= " + ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString())[0]["DepartmentID"].ToString();
		dtMaterialIssueRequestDetails = MaterialIssueVouchersDetails.FillByMaterialIssueRequestID(Adding ? "-1" : RowID, ((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtMaterialIssueRequestDetails.Copy();
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
		getVoucherItemsValueList(dtMaterialIssueRequestDetails);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestDetailID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DetailBarCode"].Hidden = false;
		cboStore_ValueChanged(null, null);
	}

	private void btnVouchersSearch_Click(object sender, EventArgs e)
	{
		if (rbIsSLOrder.Checked)
		{
			int num = SearchFunctions.SLOrdersSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboOrderNo).Value = num;
			}
		}
		else if (rbIsSLInvoice.Checked)
		{
			int num2 = SearchFunctions.SLInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0, -1, 0);
			if (num2 != 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num2;
			}
		}
		else
		{
			int num3 = SearchFunctions.MaterialIssueRequestSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0);
			if (num3 != 0)
			{
				((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value = num3;
			}
		}
	}

	private void cboMaterialIssueVoucherRequestNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.MaterialIssueRequestSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboMaterialIssueVoucherRequestNo).Value = num;
			}
		}
	}

	private void cboInvoiceNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.SLInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, 0, -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboInvoiceNo).Value = num;
			}
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		if (rbIsDirect.Checked)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && rbIsDirect.Checked)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
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
		if ((Adding || Updating) && cboStore.SelectedIndex > -1 && ((UltraToggleEditorBase)chkAllStok).Checked)
		{
			GlobalVariables.QuestionMB.Show("هل تريد صرف كل أصناف المخزن ؟", "Are You Sure You want to Issue All Store Items?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				dtItemBalance.Rows.Clear();
				dtItemBalance = MaterialIssueVouchersDetails.FillByStoreID(((TextEditorControlBase)cboStore).Value.ToString(), GlobalVariables.CurrentBranchID, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtItemBalance;
				InitGrid();
			}
		}
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
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
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void AddItemInGid()
	{
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Expected O, but got Unknown
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Expected O, but got Unknown
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
		object value;
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
			value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
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
			return;
		}
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
		value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow2["ItemID"].ToString());
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

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = MaterialIssueVouchers.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
		if ((Adding || Updating) && cboStore.SelectedIndex > -1 && ((UltraToggleEditorBase)chkAllStok).Checked)
		{
			GlobalVariables.QuestionMB.Show("هل تريد صرف كل أصناف المخزن ؟", "Are You Sure You want to Issue All Store Items?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				dtItemBalance.Rows.Clear();
				dtItemBalance = MaterialIssueVouchersDetails.FillByStoreID(((TextEditorControlBase)cboStore).Value.ToString(), GlobalVariables.CurrentBranchID, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dtItemBalance;
				InitGrid();
			}
		}
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void chkAllStok_CheckedChanged(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || cboStore.SelectedIndex <= -1 || !((UltraToggleEditorBase)chkAllStok).Checked)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد صرف كل أصناف المخزن ؟", "Are You Sure You want to Issue All Store Items?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		dtItemBalance.Rows.Clear();
		dtItemBalance = MaterialIssueVouchersDetails.FillByStoreID(((TextEditorControlBase)cboStore).Value.ToString(), GlobalVariables.CurrentBranchID, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtItemBalance;
		InitGrid();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value = DBNull.Value;
			}
		}
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

	private void btnDepartmentSearch_Click(object sender, EventArgs e)
	{
		if (rbIsDepartment.Checked && (Adding || Updating))
		{
			int num = SearchFunctions.Departments(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboDepartment).Value = num;
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
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Expected O, but got Unknown
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Transactions.frmMaterialIssueVouchers));
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
		this.rbIsSLOrder = new System.Windows.Forms.RadioButton();
		this.rbIsDepartment = new System.Windows.Forms.RadioButton();
		this.rbIsDirect = new System.Windows.Forms.RadioButton();
		this.rbIsRequest = new System.Windows.Forms.RadioButton();
		this.rbIsSLInvoice = new System.Windows.Forms.RadioButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblInvoiceNo = new UltraLabel();
		this.cboInvoiceNo = new UltraComboEditor();
		this.lblMIVRequestNo = new UltraLabel();
		this.cboMaterialIssueVoucherRequestNo = new UltraComboEditor();
		this.btnVouchersSearch = new UltraButton();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.lblDepartment = new UltraLabel();
		this.cboDepartment = new UltraComboEditor();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.btnClientSearch = new UltraButton();
		this.chkAllStok = new UltraCheckEditor();
		this.btnStoreSearch = new UltraButton();
		this.btnDepartmentSearch = new UltraButton();
		this.btnStockBalance = new UltraButton();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.lblOrderNo = new UltraLabel();
		this.cboOrderNo = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherRequestNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllStok).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOrderNo).BeginInit();
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
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSLOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDepartment);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirect);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsRequest);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSLInvoice);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsSLOrder, "rbIsSLOrder");
		this.rbIsSLOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbIsSLOrder.Name = "rbIsSLOrder";
		this.rbIsSLOrder.TabStop = true;
		this.rbIsSLOrder.UseVisualStyleBackColor = false;
		this.rbIsSLOrder.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDepartment, "rbIsDepartment");
		this.rbIsDepartment.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDepartment.Name = "rbIsDepartment";
		this.rbIsDepartment.TabStop = true;
		this.rbIsDepartment.UseVisualStyleBackColor = false;
		this.rbIsDepartment.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDirect, "rbIsDirect");
		this.rbIsDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirect.Name = "rbIsDirect";
		this.rbIsDirect.TabStop = true;
		this.rbIsDirect.UseVisualStyleBackColor = false;
		this.rbIsDirect.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsRequest, "rbIsRequest");
		this.rbIsRequest.BackColor = System.Drawing.Color.Transparent;
		this.rbIsRequest.Name = "rbIsRequest";
		this.rbIsRequest.TabStop = true;
		this.rbIsRequest.UseVisualStyleBackColor = false;
		this.rbIsRequest.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsSLInvoice, "rbIsSLInvoice");
		this.rbIsSLInvoice.BackColor = System.Drawing.Color.Transparent;
		this.rbIsSLInvoice.Name = "rbIsSLInvoice";
		this.rbIsSLInvoice.TabStop = true;
		this.rbIsSLInvoice.UseVisualStyleBackColor = false;
		this.rbIsSLInvoice.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		this.lblInvoiceNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblInvoiceNo, "lblInvoiceNo");
		((System.Windows.Forms.Control)(object)this.lblInvoiceNo).Name = "lblInvoiceNo";
		((TextEditorControlBase)this.cboInvoiceNo).AlwaysInEditMode = true;
		this.cboInvoiceNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboInvoiceNo, "cboInvoiceNo");
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).Name = "cboInvoiceNo";
		((TextEditorControlBase)this.cboInvoiceNo).ValueChanged += new System.EventHandler(cboInvoiceNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboInvoiceNo_KeyDown);
		this.lblMIVRequestNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblMIVRequestNo, "lblMIVRequestNo");
		((System.Windows.Forms.Control)(object)this.lblMIVRequestNo).Name = "lblMIVRequestNo";
		((ControlBase)this.lblMIVRequestNo).WrapText = false;
		((TextEditorControlBase)this.cboMaterialIssueVoucherRequestNo).AlwaysInEditMode = true;
		this.cboMaterialIssueVoucherRequestNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboMaterialIssueVoucherRequestNo, "cboMaterialIssueVoucherRequestNo");
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherRequestNo).Name = "cboMaterialIssueVoucherRequestNo";
		((TextEditorControlBase)this.cboMaterialIssueVoucherRequestNo).ValueChanged += new System.EventHandler(cboMaterialIssueVoucherRequestNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherRequestNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboMaterialIssueVoucherRequestNo_KeyDown);
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
		this.lblDepartment.AutoEllipsis = false;
		resources.ApplyResources(this.lblDepartment, "lblDepartment");
		((System.Windows.Forms.Control)(object)this.lblDepartment).Name = "lblDepartment";
		((ControlBase)this.lblDepartment).WrapText = false;
		((TextEditorControlBase)this.cboDepartment).AlwaysInEditMode = true;
		this.cboDepartment.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboDepartment, "cboDepartment");
		((System.Windows.Forms.Control)(object)this.cboDepartment).Name = "cboDepartment";
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).Enter += new System.EventHandler(txtBarCode_Enter);
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		this.lblBarCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		((UltraButtonBase)this.btnClientSearch).AcceptsFocus = false;
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.chkAllStok, "chkAllStok");
		((System.Windows.Forms.Control)(object)this.chkAllStok).Name = "chkAllStok";
		((UltraToggleEditorBase)this.chkAllStok).CheckedChanged += new System.EventHandler(chkAllStok_CheckedChanged);
		((UltraButtonBase)this.btnStoreSearch).AcceptsFocus = false;
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Click += new System.EventHandler(btnStoreSearch_Click);
		((UltraButtonBase)this.btnDepartmentSearch).AcceptsFocus = false;
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnDepartmentSearch).Appearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(this.btnDepartmentSearch, "btnDepartmentSearch");
		((System.Windows.Forms.Control)(object)this.btnDepartmentSearch).Name = "btnDepartmentSearch";
		((System.Windows.Forms.Control)(object)this.btnDepartmentSearch).Click += new System.EventHandler(btnDepartmentSearch_Click);
		resources.ApplyResources(this.btnStockBalance, "btnStockBalance");
		((System.Windows.Forms.Control)(object)this.btnStockBalance).Name = "btnStockBalance";
		((System.Windows.Forms.Control)(object)this.btnStockBalance).Click += new System.EventHandler(btnStockBalance_Click);
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
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStockBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDepartmentSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDepartment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllStok);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVouchersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMIVRequestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherRequestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmMaterialIssueVouchers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherRequestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMIVRequestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVouchersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllStok, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDepartment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartment, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDepartmentSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStockBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOrderNo, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherRequestNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllStok).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOrderNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
