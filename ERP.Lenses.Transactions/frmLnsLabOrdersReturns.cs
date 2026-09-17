using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
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

namespace ERP.Lenses.Transactions;

public class frmLnsLabOrdersReturns : frmHeaderDetails
{
	private DataTable dtVisaType;

	private DataTable dtStores;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItems;

	private DataTable dtClients;

	private DataTable dtLnsLabOrders;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtPOSDefaultData;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlServices = new ValueList();

	private ValueList vlUsers = new ValueList();

	private DataView dvItems;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

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

	public UltraButton btnLabOrdersSearch;

	private UltraLabel lblLnsInvoiceNo;

	private UltraComboEditor cboLabOrderNo;

	private UltraLabel lblAddedValue;

	private UltraTextEditor txtAddedValue;

	private UltraLabel lblSubtractedValue;

	private UltraTextEditor txtSubtractedValue;

	private UltraLabel lblNet;

	private UltraTextEditor txtNet;

	private UltraTextEditor txtVisaAmount;

	private UltraLabel lblVisaAmount;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraLabel lblVisaType;

	private UltraComboEditor cboVisaType;

	private UltraTextEditor txtInvoicePaidAmount;

	private UltraLabel ultraLabel1;

	protected internal UltraGrid ULGDataServices;

	private UltraLabel lblClientLoadAmount;

	private UltraTextEditor txtClientLoadAmount;

	private UltraLabel lblTotalContractAmount;

	private UltraTextEditor txtTotalContractAmount;

	public frmLnsLabOrdersReturns()
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
		TableName = "Lns_LabOrdersReturns";
		IDCol = "ReturnID";
		NoCol = "ReturnNo";
		DateCol = "ReturnDate";
	}

	public frmLnsLabOrdersReturns(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = Clients.FillComboWithGlassesHistory(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlColors.ValueListItems.Clear();
		for (int i = 0; i < dtColors.Rows.Count; i++)
		{
			vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSizes.ValueListItems.Clear();
		for (int j = 0; j < dtSizes.Rows.Count; j++)
		{
			vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
		}
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "IsService=1";
		dvItems.ToTable();
		vlServices.ValueListItems.Clear();
		for (int l = 0; l < dvItems.Count; l++)
		{
			vlServices.ValueListItems.Add(dvItems[l]["ItemID"], dvItems[l]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		dtLnsLabOrders = LabOrders.FillCombo(GlobalVariables.BranchIDs, "1", "-1", (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "0" : GlobalFunctions.GetDefault("ClientReturnPeriod"));
		dtDetails = LabOrdersReturnsDetails.SelectByReturnID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataServices).DataSource = LabOrdersReturnsServices.SelectByReturnID("0", GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
	}

	public override void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataServices);
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "GeneralData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ReturnServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["FromLab"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["FromLab"].Header).Caption = (GlobalVariables.IsArabic ? "من المعمل" : "From Lab");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر المعمل" : "Lab Price");
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceItemID"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["FromLab"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceItemID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["FromLab"].DefaultCellValue = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGDataServices).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGDataServices).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = LabOrdersReturns.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			((TextEditorControlBase)cboLabOrderNo).ValueChanged -= cboLabOrderNo_ValueChanged;
			((TextEditorControlBase)cboLabOrderNo).Value = drMaster["LabOrderID"];
			((TextEditorControlBase)cboLabOrderNo).ValueChanged += cboLabOrderNo_ValueChanged;
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalContractAmount).Text = decimal.Parse(drMaster["TotalContractAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtClientLoadAmount).Text = decimal.Parse(drMaster["ClientLoadAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
			((Control)(object)txtAddedValue).Text = decimal.Parse(drMaster["AddedValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
			((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
			((Control)(object)txtSubtractedValue).Text = drMaster["SubtractedValue"].ToString();
			((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
			((TextEditorControlBase)cboVisaType).Value = drMaster["VisaTypeID"];
			((Control)(object)txtVisaNo).Text = drMaster["VisaNo"].ToString();
			((TextEditorControlBase)txtVisaAmount).ValueChanged -= txtVisaAmount_ValueChanged;
			((Control)(object)txtVisaAmount).Text = decimal.Parse((drMaster["VisaAmount"] == DBNull.Value) ? "0" : drMaster["VisaAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtVisaAmount).ValueChanged += txtVisaAmount_ValueChanged;
			((Control)(object)txtNet).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtInvoicePaidAmount).Text = decimal.Parse((drMaster["InvicePaidAmount"] == DBNull.Value) ? "0" : drMaster["InvicePaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = LabOrdersReturnsDetails.SelectByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataServices).DataSource = LabOrdersReturnsServices.SelectByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((EditorButtonControlBase)cboLabOrderNo).ReadOnly = NavMode;
		((Control)(object)btnLabOrdersSearch).Visible = false;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddedValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSubtractedValue).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVisaType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVisaNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVisaAmount).ReadOnly = NavMode;
		if (Adding || Updating)
		{
			if (Adding)
			{
				DataView dataView = new DataView(dtLnsLabOrders);
				dataView.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
				GlobalFunctions.FillCombo(cboLabOrderNo, dataView.ToTable(), "LabOrderID", "LabOrderNo");
			}
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
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
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
		if (Adding)
		{
			DataView dataView4 = new DataView(dtVisaType);
			dataView4.RowFilter = " IsActive =1 ";
			DataTable dt = dataView4.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)(Adding ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? LabOrdersReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((Control)(object)txtTotal).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		((Control)(object)txtClientLoadAmount).Text = "0";
		((Control)(object)txtTotalContractAmount).Text = "0";
		((Control)(object)txtNet).Text = "0";
		((Control)(object)txtInvoicePaidAmount).Text = "0";
		cboVisaType.SelectedIndex = -1;
		((TextEditorControlBase)txtVisaNo).Clear();
		((TextEditorControlBase)txtVisaAmount).ValueChanged -= txtVisaAmount_ValueChanged;
		((Control)(object)txtVisaAmount).Text = "0";
		((TextEditorControlBase)txtVisaAmount).ValueChanged += txtVisaAmount_ValueChanged;
		((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
		((Control)(object)txtAddedValue).Text = "0";
		((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
		((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
		((Control)(object)txtSubtractedValue).Text = "0";
		((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboLabOrderNo).ValueChanged -= cboLabOrderNo_ValueChanged;
		cboLabOrderNo.SelectedIndex = -1;
		((TextEditorControlBase)cboLabOrderNo).ValueChanged += cboLabOrderNo_ValueChanged;
		((DataTable)((UltraGridBase)ULGDataServices).DataSource).Rows.Clear();
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
		if (cboLabOrderNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم فاتورة البيع المباشر" : "Please Select Point of Sales Invoice No");
			cboLabOrderNo.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_LabOrdersReturns", "ReturnNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReturnNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = LabOrdersReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
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
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='LabDiscountReturnsAccount' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtSubtractedValue).Text != "" && decimal.Parse(((Control)(object)txtSubtractedValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب خصم مردودات المعامل من حسابات النظام  ", "Please Select Lab Discount Return Amount From SystemAccounts ");
			return false;
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
		if (dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client ID From Sales Settings ");
			return false;
		}
		if (int.Parse(dataTable.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		return true;
	}

	public bool ValidateForShift()
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
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return false;
		}
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		DataTable dataTable3 = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dataTable3.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dataTable3.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
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
		DataRow dataRow = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0];
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = 0;
			num = LabOrdersReturns.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboLabOrderNo).Value.ToString(), ShiftDetailID.ToString(), ShiftDetailUserID.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), dtLnsLabOrders.Select("LabOrderID = " + ((TextEditorControlBase)cboLabOrderNo).Value.ToString())[0]["CurrencyID"].ToString(), dtLnsLabOrders.Select("LabOrderID = " + ((TextEditorControlBase)cboLabOrderNo).Value.ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (((Control)(object)txtTotalContractAmount).Text == "") ? "0" : ((Control)(object)txtTotalContractAmount).Text, (((Control)(object)txtClientLoadAmount).Text == "") ? "0" : ((Control)(object)txtClientLoadAmount).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, (((Control)(object)txtNet).Text == "") ? "0" : ((Control)(object)txtNet).Text, (((Control)(object)txtInvoicePaidAmount).Text == "") ? "0" : ((Control)(object)txtInvoicePaidAmount).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, (dataRow["SubAccountID"] == DBNull.Value) ? GlobalVariables.CurrentBranchID : dataRow["SubAccountBranchID"].ToString(), "Null", "Null", "1", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataServices).Rows[j].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGDataServices).Rows[j].Cells["ReturnServiceID"].Value = -1;
				((UltraGridBase)ULGDataServices).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				LabOrdersReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count > 0)
			{
				LabOrdersReturnsServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataServices).DataSource, GlobalVariables.UserID);
			}
			ShiftsDetails.LabOrdersReturnJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch (Exception)
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
	}

	public override void UpdateData()
	{
		DataRow dataRow = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0];
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = LabOrdersReturns.Insert_Update(drMaster["ReturnID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboLabOrderNo).Value.ToString(), ShiftDetailID.ToString(), ShiftDetailUserID.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), dtLnsLabOrders.Select("LabOrderID = " + ((TextEditorControlBase)cboLabOrderNo).Value.ToString())[0]["CurrencyID"].ToString(), dtLnsLabOrders.Select("LabOrderID = " + ((TextEditorControlBase)cboLabOrderNo).Value.ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (((Control)(object)txtTotalContractAmount).Text == "") ? "0" : ((Control)(object)txtTotalContractAmount).Text, (((Control)(object)txtClientLoadAmount).Text == "") ? "0" : ((Control)(object)txtClientLoadAmount).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, (((Control)(object)txtNet).Text == "") ? "0" : ((Control)(object)txtNet).Text, (((Control)(object)txtInvoicePaidAmount).Text == "") ? "0" : ((Control)(object)txtInvoicePaidAmount).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, (dataRow["SubAccountID"] == DBNull.Value) ? GlobalVariables.CurrentBranchID : dataRow["SubAccountBranchID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLabMIV", "RLabMIV");
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Lns_LabOrdersReturnsDetails", "ReturnID", drMaster["ReturnID"].ToString(), "ReturnDetailID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				LabOrdersReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataServices).Rows[j].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGDataServices).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text2 = text2 + ((UltraGridBase)ULGDataServices).Rows[j].Cells["ReturnServiceID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Lns_LabOrdersReturnsServices", "ReturnID", drMaster["ReturnID"].ToString(), "ReturnServiceID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count > 0)
			{
				LabOrdersReturnsServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataServices).DataSource, GlobalVariables.UserID);
			}
			ShiftsDetails.ReturnJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text3 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "RLabMIV", "RLabMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text3 != "")
			{
				GlobalVariables.InformationMB.Show(text3);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RLabMIV", "RLabMIV");
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
			MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLabMIV", "RLabMIV");
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			LabOrdersReturns.DeleteVirtual(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			LabOrdersReturnsDetails.DeleteVirtualByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			LabOrdersReturnsServices.DeleteVirtualByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			ShiftsDetails.ReturnJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.ExpenseJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLabMIV", "RLabMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RLabMIV", "RLabMIV");
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
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsLabordersReturnsReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ReturnID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = Clients.FillComboWithGlassesHistory(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlColors.ValueListItems.Clear();
		for (int i = 0; i < dtColors.Rows.Count; i++)
		{
			vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSizes.ValueListItems.Clear();
		for (int j = 0; j < dtSizes.Rows.Count; j++)
		{
			vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
		}
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "IsService=1";
		dvItems.ToTable();
		vlServices.ValueListItems.Clear();
		for (int l = 0; l < dvItems.Count; l++)
		{
			vlServices.ValueListItems.Add(dvItems[l]["ItemID"], dvItems[l]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
		dtLnsLabOrders = LabOrders.FillCombo(GlobalVariables.BranchIDs, "1", "-1", (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "0" : GlobalFunctions.GetDefault("ClientReturnPeriod"));
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
				GlobalVariables.InformationMB.Show(" لايمكن تعديل هذه الحركة لوجود المخزن \n" + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " مغلق ", "Can not Update This Transaction Because Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " Locked ");
				return;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n Cannot Update This Transaction Because Store Taking Date ", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), " \n  On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void CalculateTotals()
	{
		((UltraGridBase)ULGData).UpdateData();
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			num3 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Discount"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
		{
			num += decimal.Parse(((UltraGridBase)ULGDataServices).Rows[j].Cells["TotalPrice"].Value.ToString());
			num3 += decimal.Parse(((UltraGridBase)ULGDataServices).Rows[j].Cells["Discount"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGDataServices).Rows[j].Cells["TaxValue"].Value.ToString());
		}
		((Control)(object)txtTotal).Text = decimal.Parse((num - num3 + num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNet();
	}

	private void cboLabOrderNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboLabOrderNo.SelectedIndex > -1)
		{
			DataRow dataRow = dtLnsLabOrders.Select("LabOrderID = " + ((TextEditorControlBase)cboLabOrderNo).Value.ToString())[0];
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = dataRow["ClientID"];
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((Control)(object)txtInvoicePaidAmount).Text = dataRow["PaidAmount"].ToString();
			((Control)(object)txtTotalContractAmount).Text = dataRow["TotalContractAmount"].ToString();
			((Control)(object)txtClientLoadAmount).Text = dataRow["ClientLoadAmount"].ToString();
			((UltraGridBase)ULGData).DataSource = LabOrdersReturnsDetails.SelectByLabOrderID(((TextEditorControlBase)cboLabOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataServices).DataSource = LabOrdersReturnsServices.SelectByLabOrderID(((TextEditorControlBase)cboLabOrderNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
			CalculateTotals();
		}
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		CalculateRest();
	}

	private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGDataServices_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataServices).ActiveRow).Selected = true;
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex > -1)
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			DataView dataView = new DataView(dtLnsLabOrders);
			dataView.RowFilter = "ClientID =" + ((TextEditorControlBase)cboClient).Value.ToString() + "  And BranchID =" + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboLabOrderNo, dataView.ToTable(), "LabOrderID", "LabOrderNo");
		}
		else
		{
			if (((UltraGridBase)ULGData).DataSource != null)
			{
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			}
			GlobalFunctions.FillCombo(cboLabOrderNo, dtLnsLabOrders, "LabOrderID", "LabOrderNo");
		}
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
			((Control)(object)txtCode).Text = LabOrdersReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	public void CalculateNet()
	{
		((Control)(object)txtNet).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text) + decimal.Parse((((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "") ? "0" : ((Control)(object)txtTotalContractAmount).Text) - decimal.Parse((((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateRest();
	}

	public void CalculateRest()
	{
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNet).Text) - (decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text) + decimal.Parse((((Control)(object)txtVisaAmount).Text == "" || ((Control)(object)txtVisaAmount).Text == ".") ? "0" : ((Control)(object)txtVisaAmount).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void txtAddedValue_ValueChanged(object sender, EventArgs e)
	{
		CalculateNet();
	}

	private void txtSubtractedValue_ValueChanged(object sender, EventArgs e)
	{
		CalculateNet();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtVisaAmount_ValueChanged(object sender, EventArgs e)
	{
		CalculateRest();
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
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsLabOrdersReturns));
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
		this.btnLabOrdersSearch = new UltraButton();
		this.lblLnsInvoiceNo = new UltraLabel();
		this.cboLabOrderNo = new UltraComboEditor();
		this.lblAddedValue = new UltraLabel();
		this.txtAddedValue = new UltraTextEditor();
		this.lblSubtractedValue = new UltraLabel();
		this.txtSubtractedValue = new UltraTextEditor();
		this.lblNet = new UltraLabel();
		this.txtNet = new UltraTextEditor();
		this.txtVisaAmount = new UltraTextEditor();
		this.lblVisaAmount = new UltraLabel();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.lblVisaType = new UltraLabel();
		this.cboVisaType = new UltraComboEditor();
		this.txtInvoicePaidAmount = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ULGDataServices = new UltraGrid();
		this.lblClientLoadAmount = new UltraLabel();
		this.txtClientLoadAmount = new UltraTextEditor();
		this.lblTotalContractAmount = new UltraLabel();
		this.txtTotalContractAmount = new UltraTextEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboLabOrderNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoicePaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientLoadAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalContractAmount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataServices);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataServices, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance9");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance13");
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
		this.dtpDate.DateTime = new System.DateTime(2021, 2, 28, 0, 0, 0, 0);
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.Value = new System.DateTime(2021, 2, 28, 0, 0, 0, 0);
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
		((UltraButtonBase)this.btnLabOrdersSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnLabOrdersSearch, "btnLabOrdersSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance14");
		((ControlBase)this.btnLabOrdersSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnLabOrdersSearch).Name = "btnLabOrdersSearch";
		resources.ApplyResources(this.lblLnsInvoiceNo, "lblLnsInvoiceNo");
		this.lblLnsInvoiceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo).Name = "lblLnsInvoiceNo";
		((ControlBase)this.lblLnsInvoiceNo).WrapText = false;
		resources.ApplyResources(this.cboLabOrderNo, "cboLabOrderNo");
		((TextEditorControlBase)this.cboLabOrderNo).AlwaysInEditMode = true;
		this.cboLabOrderNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLabOrderNo).Name = "cboLabOrderNo";
		((TextEditorControlBase)this.cboLabOrderNo).ValueChanged += new System.EventHandler(cboLabOrderNo_ValueChanged);
		resources.ApplyResources(this.lblAddedValue, "lblAddedValue");
		this.lblAddedValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedValue).Name = "lblAddedValue";
		((ControlBase)this.lblAddedValue).WrapText = false;
		resources.ApplyResources(this.txtAddedValue, "txtAddedValue");
		((System.Windows.Forms.Control)(object)this.txtAddedValue).Name = "txtAddedValue";
		((TextEditorControlBase)this.txtAddedValue).ValueChanged += new System.EventHandler(txtAddedValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAddedValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSubtractedValue, "lblSubtractedValue");
		this.lblSubtractedValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubtractedValue).Name = "lblSubtractedValue";
		((ControlBase)this.lblSubtractedValue).WrapText = false;
		resources.ApplyResources(this.txtSubtractedValue, "txtSubtractedValue");
		((System.Windows.Forms.Control)(object)this.txtSubtractedValue).Name = "txtSubtractedValue";
		((TextEditorControlBase)this.txtSubtractedValue).ValueChanged += new System.EventHandler(txtSubtractedValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSubtractedValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblNet, "lblNet");
		this.lblNet.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNet).Name = "lblNet";
		((ControlBase)this.lblNet).WrapText = false;
		resources.ApplyResources(this.txtNet, "txtNet");
		((System.Windows.Forms.Control)(object)this.txtNet).Name = "txtNet";
		((EditorButtonControlBase)this.txtNet).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtNet).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVisaAmount, "txtVisaAmount");
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).Name = "txtVisaAmount";
		((TextEditorControlBase)this.txtVisaAmount).ValueChanged += new System.EventHandler(txtVisaAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblVisaAmount, "lblVisaAmount");
		this.lblVisaAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaAmount).Name = "lblVisaAmount";
		((ControlBase)this.lblVisaAmount).WrapText = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.lblVisaType, "lblVisaType");
		this.lblVisaType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaType).Name = "lblVisaType";
		((ControlBase)this.lblVisaType).WrapText = false;
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((TextEditorControlBase)this.cboVisaType).AlwaysInEditMode = true;
		this.cboVisaType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		((System.Windows.Forms.Control)(object)this.cboVisaType).TabStop = false;
		resources.ApplyResources(this.txtInvoicePaidAmount, "txtInvoicePaidAmount");
		((System.Windows.Forms.Control)(object)this.txtInvoicePaidAmount).Name = "txtInvoicePaidAmount";
		((EditorButtonControlBase)this.txtInvoicePaidAmount).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtInvoicePaidAmount).TabStop = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ULGDataServices, "ULGDataServices");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataServices).Name = "ULGDataServices";
		((UltraControlBase)this.ULGDataServices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataServices.AfterEnterEditMode += new System.EventHandler(ULGDataServices_AfterEnterEditMode);
		resources.ApplyResources(this.lblClientLoadAmount, "lblClientLoadAmount");
		this.lblClientLoadAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientLoadAmount).Name = "lblClientLoadAmount";
		((ControlBase)this.lblClientLoadAmount).WrapText = false;
		resources.ApplyResources(this.txtClientLoadAmount, "txtClientLoadAmount");
		((System.Windows.Forms.Control)(object)this.txtClientLoadAmount).Name = "txtClientLoadAmount";
		((EditorButtonControlBase)this.txtClientLoadAmount).ReadOnly = true;
		resources.ApplyResources(this.lblTotalContractAmount, "lblTotalContractAmount");
		this.lblTotalContractAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalContractAmount).Name = "lblTotalContractAmount";
		((ControlBase)this.lblTotalContractAmount).WrapText = false;
		resources.ApplyResources(this.txtTotalContractAmount, "txtTotalContractAmount");
		((System.Windows.Forms.Control)(object)this.txtTotalContractAmount).Name = "txtTotalContractAmount";
		((EditorButtonControlBase)this.txtTotalContractAmount).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientLoadAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientLoadAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalContractAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalContractAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubtractedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubtractedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLabOrdersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLabOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoicePaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmLnsLabOrdersReturns";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoicePaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLabOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLnsInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLabOrdersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalContractAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalContractAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientLoadAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientLoadAmount, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboLabOrderNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoicePaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientLoadAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalContractAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
