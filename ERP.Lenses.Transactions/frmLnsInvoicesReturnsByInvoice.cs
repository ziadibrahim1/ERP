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
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmLnsInvoicesReturnsByInvoice : frmHeaderDetails
{
	private DataTable dtReturnReasons;

	private DataTable dtReports;

	private DataTable dtStores;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtLnsInvoices;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtShiftDetails;

	private DataTable dtPOSDefaultData;

	private DataTable dtTaxs;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes;

	private bool UseReturnReasons = false;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private ArrayList ArOfferIDs = new ArrayList();

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblRestAmount;

	private UltraComboEditor cboClientCode;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBrabnchBalance;

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblShiftDate;

	private UltraDateTimeEditor dtpShiftDate;

	public UltraButton btnLnsInvoiceSearch;

	private UltraLabel lblLnsInvoiceNo;

	private UltraComboEditor cboLnsInvoiceNo;

	private UltraLabel lblReturnReason;

	private UltraComboEditor cboReturnReasons;

	public frmLnsInvoicesReturnsByInvoice()
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
		TableName = "Lns_InvoicesReturns";
		IDCol = "ReturnID";
		NoCol = "ReturnNo";
		DateCol = "ReturnDate";
	}

	public frmLnsInvoicesReturnsByInvoice(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseReturnReasons = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From SC_ReturnReasons Where Deleted=0").Rows[0][0].ToString()) > 0;
		AutoPrint = false;
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int l = 0; l < dtTaxs.Rows.Count; l++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[l]["TaxID"], dtTaxs.Rows[l]["TaxName"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int n = 0; n < dtItems.Rows.Count; n++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		if (UseReturnReasons)
		{
			dtReturnReasons = ReturnReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReturnReasons, dtReturnReasons, "ReturnReasonID", "ReturnReasonName");
		}
		dtLnsInvoices = Invoices.FillCombo(GlobalVariables.BranchIDs, (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "0" : GlobalFunctions.GetDefault("ClientReturnPeriod"));
		GlobalFunctions.FillCombo(cboLnsInvoiceNo, dtLnsInvoices, "InvoiceID", "InvoiceNo");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		dtDetails = InvoicesReturnsDetails.SelectByReturnID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
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
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصى" : "Max limit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = InvoicesReturns.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ReturnNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ReturnDate"];
			((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged -= cboLnsInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboLnsInvoiceNo).Value = drMaster["InvoiceID"];
			((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged += cboLnsInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			UltraComboEditor obj = cboClientCode;
			object value = (((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"]);
			((TextEditorControlBase)obj).Value = value;
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboReturnReasons).Value = drMaster["ReturnReasonID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = InvoicesReturnsDetails.SelectByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLnsInvoiceNo).ReadOnly = NavMode;
		((Control)(object)btnLnsInvoiceSearch).Visible = false;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode;
		((Control)(object)cboReturnReasons).Visible = UseReturnReasons;
		((Control)(object)lblReturnReason).Visible = UseReturnReasons;
		((EditorButtonControlBase)cboReturnReasons).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = NavMode || !CanEditValue;
		((Control)(object)dtpShiftDate).Visible = !Adding;
		((Control)(object)txtShiftNo).Visible = !Adding;
		((Control)(object)lblShiftDate).Visible = !Adding;
		((Control)(object)lblShiftNo).Visible = !Adding;
		int num = 0;
		if (cboLnsInvoiceNo.SelectedIndex > -1)
		{
			num = int.Parse(((TextEditorControlBase)cboLnsInvoiceNo).Value.ToString());
		}
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtLnsInvoices);
			dataView.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboLnsInvoiceNo, dataView.ToTable(), "InvoiceID", "InvoiceNo");
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
			GlobalFunctions.FillCombo(cboLnsInvoiceNo, dtLnsInvoices, "InvoiceID", "InvoiceNo");
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged -= cboLnsInvoiceNo_ValueChanged;
		if (num != 0)
		{
			((TextEditorControlBase)cboLnsInvoiceNo).Value = num;
		}
		((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged += cboLnsInvoiceNo_ValueChanged;
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
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? InvoicesReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((Control)(object)txtTotal).Text = "0";
		((Control)(object)txtPaidAmount).Text = "0";
		((Control)(object)txtRestAmount).Text = "0";
		cboReturnReasons.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		UltraComboEditor obj = cboClientCode;
		int selectedIndex = (cboClient.SelectedIndex = -1);
		obj.SelectedIndex = selectedIndex;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged -= cboLnsInvoiceNo_ValueChanged;
		cboLnsInvoiceNo.SelectedIndex = -1;
		((TextEditorControlBase)cboLnsInvoiceNo).ValueChanged += cboLnsInvoiceNo_ValueChanged;
	}

	public override bool ValidateData()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
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
		if (cboLnsInvoiceNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم فاتورة البيع المباشر" : "Please Select Point of Sales Invoice No");
			cboLnsInvoiceNo.DropDown();
			return false;
		}
		if (UseReturnReasons && cboReturnReasons.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار سبب الارتجاع" : "Please Select Return Reason");
			((TextEditorControlBase)cboReturnReasons).Focus();
			cboReturnReasons.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_InvoicesReturns", "ReturnNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReturnNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = InvoicesReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (decimal.Parse(((Control)(object)txtRestAmount).Text) < 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "القيمة المدفوعة اكبر من إجمالى الفاتورة" : "Paid Amount Greater than Invoice Total Amount");
			((TextEditorControlBase)txtPaidAmount).Focus();
			return false;
		}
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) == 0m)
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
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].DroppedDown = true;
				return false;
			}
			if ((((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value || dtStores.Select("Locked=0 And  StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString()).Length == 0) && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
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
		DataTable dataTable3 = ShiftsDetailsUsers.SelectNotClosed(dataTable2.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable2.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		DataTable dataTable4 = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dataTable4.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (dataTable3.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dataTable4.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable3.Rows[0]["ShiftDetailUserID"].ToString();
		}
		return true;
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

	public override void AddData()
	{
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = InvoicesReturns.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboLnsInvoiceNo).Value.ToString(), ShiftDetailID.ToString(), ShiftDetailUserID.ToString(), GlobalVariables.UserID, (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboReturnReasons.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReturnReasons).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "Null", "1", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			InvoicesReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ShiftsDetails.ReturnJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
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
			RowID = num.ToString();
		}
		if (!DataSaved || !(GlobalVariables.POSPrinter != ""))
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة فاتورة المرتجع ؟", "Are You Sure You want to Print This Return Invoice?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_InvoicesReturnsFastPrint")
				{
					RowID = num.ToString();
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_InvoicesReturns_A.rpt" : "Rep_Lns_InvoicesReturns_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ReturnID", num);
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
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
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Expected O, but got Unknown
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = InvoicesReturns.Insert_Update(drMaster["ReturnID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboLnsInvoiceNo).Value.ToString(), ShiftDetailID.ToString(), ShiftDetailUserID.ToString(), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboReturnReasons.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReturnReasons).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), (drMaster["EInvoiceInternalCode"] == DBNull.Value) ? "Null" : drMaster["EInvoiceInternalCode"].ToString(), (drMaster["EInvoiceUUID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceUUID"].ToString(), (drMaster["EInvoiceSenDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceSenDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceSendUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceSendUserID"].ToString(), bool.Parse(drMaster["EInvoiceIsCanceled"].ToString()) ? "1" : "0", (drMaster["EInvoiceCanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceCanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceCanceledUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceCanceledUserID"].ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "Null", "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV");
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("Lns_InvoicesReturnsDetails", "ReturnID", drMaster["ReturnID"].ToString(), "ReturnDetailID", text);
			InvoicesReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ShiftsDetails.ReturnJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.ExpenseJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLnsMIV", "RLnsMIV");
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
			MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLnsMIV", "RLnsMIV");
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			InvoicesReturns.DeleteVirtual(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			InvoicesReturnsDetails.DeleteVirtualByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			ShiftsDetails.ReturnJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.ExpenseJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLnsMIV", "RLnsMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLnsMIV", "RLnsMIV");
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

	public void FastPrint()
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_Lns_InvoicesReturns " + RowID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable2 = Main.ExecuteQuery_DataTable(" Rep_POS_Settings_SelectByBranchID " + GlobalVariables.CurrentBranchID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		Color black = Color.Black;
		Color darkBlue = Color.DarkBlue;
		Color black2 = Color.Black;
		float lineWidth = 0.03f;
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 6f, FontStyle.Regular);
		Font font3 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font5 = new Font("Times New Roman", 8f, FontStyle.Bold);
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
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["ReturnNo"].ToString(), font5, 0.7f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("مرتجع برقم", font5, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(((DateTime)dataRow["ReturnDate"]).ToString("dd/MM/yyyy hh:mm:ss tt"), font, 0.7f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("بتاريخ", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["BranchName"].ToString(), font, 0.7f, 4f, StringAlignment.Near, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("فرع", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["UserName"].ToString(), font, 0.7f, 4f, StringAlignment.Near, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اسم البائع", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["ClientName"].ToString(), font, 0.5f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell(dataRow["ClientSupplierNo"].ToString(), font, 0.2f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("العميل", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font, 18f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("سعر الوحده", font, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الوحده", font, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الكميه", font, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الصنف", font, 28f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		foreach (DataRow row in dataTable.Rows)
		{
			instance.AddTextCell(decimal.Parse(row["TotalUnitPriceDetail"].ToString(), NumberStyles.Currency).ToString("0.00"), font2, 18f / instance.OverallWidth, 3f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(decimal.Parse(row["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font2, 8f / instance.OverallWidth, 3f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["UnitName"].ToString(), font2, 8f / instance.OverallWidth, 3f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(decimal.Parse(row["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font2, 8f / instance.OverallWidth, 3f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["ItemName"].ToString(), font2, 28f / instance.OverallWidth, 3f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(decimal.Parse(dataRow["TotalPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 18f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الإجمالي", font4, 52f / instance.OverallWidth, 4f, StringAlignment.Far, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell(decimal.Parse(dataRow["CurrentBalance"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 42f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("الرصيد الحالى بالفرع", font4, 28f / instance.OverallWidth, 4f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataTable2.Rows[0]["TaxNo"].ToString(), font2, 0.7f, 6f, StringAlignment.Far, black, DrawRectangle: false, black2, lineWidth);
		instance.AddTextCell("الرقم الضريبي : ", font3, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: false, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font2, 1f, 4f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dataTable2.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataTable2.Rows[0]["Message"].ToString().Trim(), font2, 1f, float.Parse((Math.Ceiling((double)dataTable2.Rows[0]["Message"].ToString().Trim().Length / 85.0) * 3.0).ToString()), StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		instance.PrinterSettings.Copies = 1;
		instance.Print();
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_InvoicesReturnsFastPrint")
				{
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_InvoicesReturns_A.rpt" : "Rep_Lns_InvoicesReturns_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ReturnID", RowID);
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsInvoicesReturnsReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ReturnID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		UseReturnReasons = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From SC_ReturnReasons Where Deleted=0").Rows[0][0].ToString()) > 0;
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
		dtLnsInvoices = Invoices.FillCombo(GlobalVariables.BranchIDs, (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "0" : GlobalFunctions.GetDefault("ClientReturnPeriod"));
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtReturnReasons = ReturnReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReturnReasons, dtReturnReasons, "ReturnReasonID", "ReturnReasonName");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
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
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n Cannot Update This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), " \n  On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return;
			}
		}
		Updating = true;
		SetControls(NavMode: false);
	}

	public override void btnDeleteClick()
	{
		if (!ValidateForShift() || drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		dtUsersTransactions = UsersTransactions.CheckTransaction("0", "0");
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
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["HaveSingleDiscount"].Value.ToString()) == 0)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes")
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
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(" الحد الاقصى للكمية المرتجعة " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString(), " Max Allowed Returned Quantity " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value;
			}
			if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
			{
				CalculateTotals();
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
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
		CalculateTotals();
		ULGData.AfterRowsDeleted += ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		ArOfferIDs.Clear();
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (e.Rows[i].Cells["OfferID"].Value != DBNull.Value && int.Parse(e.Rows[i].Cells["HaveSingleDiscount"].Value.ToString()) == 0)
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

	private void CalculateTotals()
	{
		((UltraGridBase)ULGData).UpdateData();
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalDiscount"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString()));
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(num.ToString()) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void cboLnsInvoiceNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboLnsInvoiceNo.SelectedIndex > -1)
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			UltraComboEditor obj = cboClientCode;
			object value = (((TextEditorControlBase)cboClient).Value = dtLnsInvoices.Select("InvoiceID = " + ((TextEditorControlBase)cboLnsInvoiceNo).Value.ToString())[0]["SubAccountID"]);
			((TextEditorControlBase)obj).Value = value;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			((UltraGridBase)ULGData).DataSource = InvoicesReturnsDetails.SelectByInvoiceIDs("," + ((TextEditorControlBase)cboLnsInvoiceNo).Value.ToString() + ",", GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
			CalculateTotals();
		}
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtTotal).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboClient).Value;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		if (cboClient.SelectedIndex > -1)
		{
			DataRow dataRow = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0];
			string text = "," + ((dataRow["AccountID"] != DBNull.Value) ? string.Concat(dataRow["AccountID"], ",") : "");
			if (dataRow["SupplierAccountID"] != DBNull.Value)
			{
				text = text + dataRow["SupplierAccountID"].ToString() + ",";
			}
			((Control)(object)txtBrabnchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : drMaster["BranchID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			DataView dataView = new DataView(dtLnsInvoices);
			dataView.RowFilter = "SubAccountID=" + ((TextEditorControlBase)cboClient).Value.ToString() + "  And BranchID =" + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboLnsInvoiceNo, dataView.ToTable(), "InvoiceID", "InvoiceNo");
		}
		else
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			GlobalFunctions.FillCombo(cboLnsInvoiceNo, dtLnsInvoices, "InvoiceID", "InvoiceNo");
		}
	}

	private void cboClientCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboClientCode.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboClientCode).Value;
			return;
		}
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
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
			((Control)(object)txtCode).Text = InvoicesReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsInvoicesReturnsByInvoice));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.txtRestAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.cboClientCode = new UltraComboEditor();
		this.lblBalance = new UltraLabel();
		this.txtBrabnchBalance = new UltraTextEditor();
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblShiftDate = new UltraLabel();
		this.dtpShiftDate = new UltraDateTimeEditor();
		this.btnLnsInvoiceSearch = new UltraButton();
		this.lblLnsInvoiceNo = new UltraLabel();
		this.cboLnsInvoiceNo = new UltraComboEditor();
		this.lblReturnReason = new UltraLabel();
		this.cboReturnReasons = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLnsInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReturnReasons).BeginInit();
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
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
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		((EditorButtonControlBase)this.txtTotal).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotal).TabStop = false;
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtPaidAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPaidAmount_KeyPress);
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtRestAmount).TabStop = false;
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.cboClientCode, "cboClientCode");
		((TextEditorControlBase)this.cboClientCode).AlwaysInEditMode = true;
		this.cboClientCode.AutoCompleteMode = (AutoCompleteMode)3;
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
		((UltraButtonBase)this.btnLnsInvoiceSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnLnsInvoiceSearch, "btnLnsInvoiceSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnLnsInvoiceSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnLnsInvoiceSearch).Name = "btnLnsInvoiceSearch";
		resources.ApplyResources(this.lblLnsInvoiceNo, "lblLnsInvoiceNo");
		this.lblLnsInvoiceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo).Name = "lblLnsInvoiceNo";
		((ControlBase)this.lblLnsInvoiceNo).WrapText = false;
		resources.ApplyResources(this.cboLnsInvoiceNo, "cboLnsInvoiceNo");
		((TextEditorControlBase)this.cboLnsInvoiceNo).AlwaysInEditMode = true;
		this.cboLnsInvoiceNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLnsInvoiceNo).Name = "cboLnsInvoiceNo";
		((TextEditorControlBase)this.cboLnsInvoiceNo).ValueChanged += new System.EventHandler(cboLnsInvoiceNo_ValueChanged);
		resources.ApplyResources(this.lblReturnReason, "lblReturnReason");
		this.lblReturnReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReturnReason).Name = "lblReturnReason";
		((ControlBase)this.lblReturnReason).WrapText = false;
		resources.ApplyResources(this.cboReturnReasons, "cboReturnReasons");
		((TextEditorControlBase)this.cboReturnReasons).AlwaysInEditMode = true;
		this.cboReturnReasons.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboReturnReasons).Name = "cboReturnReasons";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReturnReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReturnReasons);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLnsInvoiceSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLnsInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBrabnchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmLnsInvoicesReturnsByInvoice";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBrabnchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLnsInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLnsInvoiceSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReturnReasons, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReturnReason, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLnsInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReturnReasons).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
