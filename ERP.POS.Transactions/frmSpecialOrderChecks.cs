using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.POS.Transactions;

public class frmSpecialOrderChecks : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtItemsAndGroups;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtItemsAndGroupsCopy;

	private DataTable dtClients;

	private DataTable dtUnits;

	private DataTable dtTaxs;

	private DataTable dtRoomData;

	private DataTable dtSpecialOrdersDetailsAccessories;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private DataTable dtSpecialOrderPayments;

	private DataTable dtVisaType;

	private DataTable dtUsers;

	private DataTable dtDeletedItems = new DataTable();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlVisaType = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private string ShiftDetailID = "";

	private string ShiftDetailUserID = "";

	private int RoomID;

	private int CheckID = 0;

	private DataView dvItems;

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private int newID = -100000;

	private IContainer components = null;

	public UltraButton btnClientSearch;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblClient;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraGroupBox UGBItemData;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDeliveryChargeValue;

	private UltraTextEditor txtDeliveryChargeValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraButton btnPlus;

	private UltraButton btnSubtract;

	private UltraPanel pnlItems;

	private UltraLabel lblDiscountValue;

	private UltraTextEditor txtDiscountValue;

	private UltraButton btnClear;

	private UltraLabel lblDiscountRatio;

	private UltraTextEditor txtDiscountRatio;

	public UltraButton btnDiscountRatio;

	public UltraButton btnDiscountValue;

	public UltraButton btnDeliveryChargeValue;

	private UltraLabel lblRoundingValue;

	private UltraTextEditor txtRoundingValue;

	private UltraButton btnAccessories;

	private UltraCheckEditor chkIsManufactured;

	private UltraDateTimeEditor dtpManuFacturedDate;

	private UltraDateTimeEditor dtpDeliverdDate;

	private UltraCheckEditor chkIsDeliverd;

	private UltraButton btnSpecialOrdersPayments;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataPayments;

	public UltraButton btnDiscountValue2;

	public UltraButton btnDiscountRatio2;

	private UltraLabel lblDiscountRatio2;

	private UltraTextEditor txtDiscountRatio2;

	private UltraLabel lblDiscountValue2;

	private UltraTextEditor txtDiscountValue2;

	private UltraTabControl UTCItemsGroups;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	public frmSpecialOrderChecks()
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
		InitializeComponent();
		TableName = "POS_SpecialOrders";
		IDCol = "SpecialOrderID";
		NoCol = "SpecialOrderNo";
		DateCol = "SpecialOrderDate";
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "شيكات الطلبات الخاصة" : "Special Orders Checks");
	}

	public frmSpecialOrderChecks(int ID, int Room)
		: this()
	{
		CheckID = ID;
		RoomID = Room;
	}

	public frmSpecialOrderChecks(int Room)
		: this()
	{
		RoomID = Room;
	}

	public override void PrepareData()
	{
		dtReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmSpecialOrder", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		dtDeletedItems.Columns.Add("SpecialOrderDetailID");
		dtDeletedItems.Columns.Add("Notes");
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
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UltraDateTimeEditor obj = dtpDate;
		UltraDateTimeEditor obj2 = dtpManuFacturedDate;
		string text = (dtpDeliverdDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt");
		string maskInput = (obj2.MaskInput = text);
		obj.MaskInput = maskInput;
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
		dtItemsAndGroupsCopy = dtItemsAndGroups.Copy();
		FillItemsGroups();
		dvItems = new DataView(dtItemsAndGroups);
		dvItems.RowFilter = " IsMain=0 ";
		dvItems.ToTable();
		for (int k = 0; k < dvItems.Count; k++)
		{
			vlItems.ValueListItems.Add(dvItems[k]["ItemID"], dvItems[k]["ItemName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtClients = Clients.FillCombo("1", GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int m = 0; m < dtUsers.Rows.Count; m++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[m]["User_ID"], dtUsers.Rows[m]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlVisaType.ValueListItems.Clear();
		for (int n = 0; n < dtVisaType.Rows.Count; n++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[n]["VisaTypeID"], dtVisaType.Rows[n]["VisaTypeName"].ToString());
		}
		dtDetails = SpecialOrdersDetails.SelectBySpecialOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtSpecialOrderPayments = SpecialOrdersPayments.SelectBySpecialOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtSpecialOrdersDetailsAccessories = SpecialOrdersDetailsAccessories.SelectBySpecialOrderIDs(",0,", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataPayments).DataSource = dtSpecialOrderPayments;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SpecialOrderDetailID"].DefaultCellValue = -1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Comments"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Comments");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderPaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderPaymentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderPaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].Header).Caption = (GlobalVariables.IsArabic ? "نقدى" : "Cash");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Header).Caption = (GlobalVariables.IsArabic ? "نقدى" : "Cash");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].Header).Caption = (GlobalVariables.IsArabic ? "فيزا" : "Visa");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Header).Caption = (GlobalVariables.IsArabic ? "فيزا" : "Visa");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderPaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["SpecialOrderDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsCash"].DefaultCellValue = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["IsVisa"].DefaultCellValue = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["CashAmount"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = SpecialOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["SpecialOrderNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SpecialOrderDate"];
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			if (cboClient.SelectedIndex > -1)
			{
				((Control)(object)txtDiscountRatio).Text = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString();
				if (dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == DBNull.Value || dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == dtRoomData.Rows[0]["PriceTypeID"])
				{
					dtItemsAndGroups = dtItemsAndGroupsCopy;
				}
				else
				{
					dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
				}
			}
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((UltraToggleEditorBase)chkIsManufactured).Checked = bool.Parse(drMaster["IsManufactured"].ToString());
			dtpManuFacturedDate.Value = drMaster["ManuFacturedDate"];
			((UltraToggleEditorBase)chkIsDeliverd).Checked = bool.Parse(drMaster["IsDeliverd"].ToString());
			dtpDeliverdDate.Value = drMaster["DeliverdDate"];
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((Control)(object)txtDiscountValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(drMaster["DiscountBeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			((Control)(object)txtDiscountValue2).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountRatio2).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
			((Control)(object)txtDeliveryChargeValue).Text = decimal.Parse(drMaster["DeliveryChargeValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRoundingValue).Text = decimal.Parse(drMaster["RoundingValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = SpecialOrdersDetails.SelectBySpecialOrderID(drMaster["SpecialOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtSpecialOrderPayments = SpecialOrdersPayments.SelectBySpecialOrderID(drMaster["SpecialOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtSpecialOrdersDetailsAccessories = SpecialOrdersDetailsAccessories.SelectBySpecialOrderIDs("," + drMaster["SpecialOrderID"].ToString() + ",", GlobalVariables.IsArabic ? "1" : "0");
			object obj = dtSpecialOrderPayments.Compute(" Sum(CashAmount) ", "");
			object obj2 = dtSpecialOrderPayments.Compute(" Sum(VisaAmount) ", "");
			((Control)(object)txtPaidAmount).Text = decimal.Parse((decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()) + decimal.Parse((obj2 == DBNull.Value) ? "0" : obj2.ToString())).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataPayments).DataSource = dtSpecialOrderPayments;
			InitGrid();
			dtDeletedItems.Rows.Clear();
			if (drMaster["IsDeliverd"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
				((Control)(object)btnPrint).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
				((Control)(object)btnPrint).Enabled = true;
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
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((Control)(object)chkIsManufactured).Enabled = !NavMode;
		((EditorButtonControlBase)dtpManuFacturedDate).ReadOnly = NavMode;
		((Control)(object)chkIsDeliverd).Enabled = !NavMode;
		((EditorButtonControlBase)dtpDeliverdDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountValue).ReadOnly = NavMode;
		UltraTextEditor obj = txtDiscountRatio2;
		UltraTextEditor obj2 = txtDiscountValue2;
		UltraButton obj3 = btnDiscountRatio2;
		UltraButton obj4 = btnDiscountValue2;
		UltraLabel obj5 = lblDiscountRatio2;
		bool flag = (((Control)(object)lblDiscountValue2).Visible = bool.Parse(dtRoomData.Rows[0]["AdditionalDiscountWithoutTax"].ToString()));
		bool flag3 = (((Control)(object)obj5).Visible = flag);
		bool flag5 = (((Control)(object)obj4).Visible = flag3);
		bool flag7 = (((Control)(object)obj3).Visible = flag5);
		bool visible = (((Control)(object)obj2).Visible = flag7);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)txtDiscountRatio2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountValue2).ReadOnly = NavMode;
		((Control)(object)btnRefreshData).Visible = false;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((EditorButtonControlBase)txtDeliveryChargeValue).ReadOnly = NavMode;
		((Control)(object)btnDeliveryChargeValue).Enabled = !NavMode;
		((Control)(object)btnPrint).Visible = true;
		((Control)(object)btnCopyTo).Visible = false;
		UltraButton obj6 = btnPlus;
		UltraButton obj7 = btnSubtract;
		UltraButton obj8 = btnClear;
		UltraButton obj9 = btnDiscountRatio;
		UltraButton obj10 = btnDiscountValue;
		UltraButton obj11 = btnDiscountRatio2;
		bool flag10 = (((Control)(object)btnDiscountValue2).Enabled = !NavMode);
		flag = (((Control)(object)obj11).Enabled = flag10);
		flag3 = (((Control)(object)obj10).Enabled = flag);
		flag5 = (((Control)(object)obj9).Enabled = flag3);
		flag7 = (((Control)(object)obj8).Enabled = flag5);
		visible = (((Control)(object)obj7).Enabled = flag7);
		((Control)(object)obj6).Enabled = visible;
		((Control)(object)btnSpecialOrdersPayments).Visible = !NavMode;
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlVisaType.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[i]["VisaTypeID"], dataTable.Rows[i]["VisaTypeName"].ToString());
			}
		}
		else
		{
			vlVisaType.ValueListItems.Clear();
			for (int j = 0; j < dtVisaType.Rows.Count; j++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[j]["VisaTypeID"], dtVisaType.Rows[j]["VisaTypeName"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItemsAndGroups.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
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
		((TextEditorControlBase)txtCode).Clear();
		UltraDateTimeEditor obj = dtpDate;
		DateTime dateTime = (dtpManuFacturedDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		obj.DateTime = dateTime;
		((Control)(object)txtCode).Text = (Adding ? SpecialOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		cboClient.SelectedIndex = -1;
		((UltraToggleEditorBase)chkIsManufactured).Checked = false;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.Value = null;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscountValue).Text = "0";
		((Control)(object)txtDeliveryChargeValue).Text = "0";
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue_ValueChanged;
		((Control)(object)txtDiscountValue2).Text = "0";
		((Control)(object)txtDiscountRatio2).Text = "0";
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtRoundingValue).Text = "0";
		((Control)(object)txtPaidAmount).Text = "0";
		((Control)(object)txtRestAmount).Text = "0";
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		dtSpecialOrdersDetailsAccessories.Rows.Clear();
		if (dtRoomData.Rows.Count > 0 && dtRoomData.Rows[0]["DefaultClientID"] != DBNull.Value && Adding)
		{
			((TextEditorControlBase)cboClient).Value = dtRoomData.Rows[0]["DefaultClientID"];
		}
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
		dtDeletedItems.Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الشيك" : "Please Enter The Check Date");
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			return false;
		}
		if (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب التحليلى للعميل" : "Please Select Client SubAccount");
			((TextEditorControlBase)cboClient).Focus();
			return false;
		}
		if (decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) > 0m && decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إعطاء نوعين خصم فى نفس الشيك" : "Cannot Put Different Discount In The Same Check");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_SpecialOrders", "SpecialOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["SpecialOrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "  ") > 0)
		{
			string codeByBranchID = SpecialOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			if (((Control)(object)txtCode).Text != codeByBranchID)
			{
				GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					((TextEditorControlBase)txtCode).Focus();
					return false;
				}
				((Control)(object)txtCode).Text = codeByBranchID;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		if (decimal.Parse(((Control)(object)txtRestAmount).Text) > 0m && ((UltraToggleEditorBase)chkIsDeliverd).Checked)
		{
			GlobalVariables.QuestionMB.Show(" القيمة المدفوعة اقل من قيمة الفاتورة هل تريد الحفظ", " Paid Amount Less Than Invoice Value Are You Sure To Save Invoice  ");
			if (GlobalVariables.MessageBoxResult == 'N')
			{
				return false;
			}
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال المخزن  ", "Please Enter Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].DroppedDown = true;
				return false;
			}
		}
		if (GlobalFunctions.GetOption("POSSalesAutoGenerateJVs"))
		{
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='DeliveryChargeAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب رسم التوصيل من حسابات النظام  ", "Please Select Delivery Charge Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && decimal.Parse(((Control)(object)txtDiscountValue).Text) > 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='POSRoundingValuesAccount' ")[0]["AccountID"] == DBNull.Value && dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب فروق تقريب بيع مباشر من حسابات النظام  ", "Please Select Rounding Values Account From SystemAccounts ");
				return false;
			}
		}
		return true;
	}

	public override void btnOKClick()
	{
		DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
		if (GlobalFunctions.GetServerDateTimeNow() < dateTime)
		{
			GlobalVariables.InformationMB.Show(dateTime.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime.ToShortTimeString());
			return;
		}
		if (GlobalFunctions.GetServerDateTimeNow() > dateTime2.AddHours(2.0))
		{
			GlobalVariables.InformationMB.Show(dateTime2.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime2.ToShortTimeString());
			return;
		}
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return;
		}
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return;
		}
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
		}
		base.btnOKClick();
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
						DataTable dataTable3 = SpecialOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
				if (Updating && RowID != "" && TableName != "")
				{
					if (UsersTransactions.CheckNotKicked(GlobalVariables.UserLoginID, TableName, RowID).Rows.Count == 0)
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لقد تم إخراجك من قبل مستخدم اخر ", "You Have Been Kicked By Another User");
						return false;
					}
					if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
						return false;
					}
				}
				UpdateData();
				if (!Close)
				{
					DataTable dataTable4 = SpecialOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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

	public override void btnSaveClose_Click(object sender, EventArgs e)
	{
		SaveClose(Close: true);
	}

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Expected O, but got Unknown
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c37: Expected O, but got Unknown
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
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = SpecialOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", ((UltraToggleEditorBase)chkIsManufactured).Checked ? "1" : "0", (dtpManuFacturedDate.Value == DBNull.Value) ? "Null" : dtpManuFacturedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpDeliverdDate.Value == DBNull.Value || dtpDeliverdDate.Value == null) ? "Null" : dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), ((Control)(object)txtDeliveryChargeValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, dtRoomData.Rows[0]["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", "Null", "0", "Null", "Null", "0", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SpecialOrderDetailID"].Value.ToString());
					int num3 = SpecialOrdersDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString(), "0", (((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int k = 0; k < dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
					{
						if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailID"].ToString()) == num2)
						{
							dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailID"] = num3;
						}
					}
				}
				for (int l = 0; l < dtSpecialOrdersDetailsAccessories.Rows.Count; l++)
				{
					dtSpecialOrdersDetailsAccessories.Rows[l]["SpecialOrderDetailAccessoryID"] = -1;
					dtSpecialOrdersDetailsAccessories.Rows[l]["VoucherDate"] = dtpDate.DateTime;
					dtSpecialOrdersDetailsAccessories.Rows[l]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtSpecialOrdersDetailsAccessories.Rows[l]["SpecialOrderID"] = num.ToString();
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				if (dtSpecialOrdersDetailsAccessories.Rows.Count > 0)
				{
					SpecialOrdersDetailsAccessories.Insert_UpdateByTable(dtSpecialOrdersDetailsAccessories, GlobalVariables.UserID);
				}
			}
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV");
			}
			else
			{
				RowID = num.ToString();
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Expected O, but got Unknown
		//IL_0fc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Expected O, but got Unknown
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
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = SpecialOrders.Insert_Update(drMaster["SpecialOrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", ((UltraToggleEditorBase)chkIsManufactured).Checked ? "1" : "0", (dtpManuFacturedDate.Value == DBNull.Value) ? "Null" : dtpManuFacturedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpDeliverdDate.Value == DBNull.Value || dtpDeliverdDate.Value == null) ? "Null" : dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), (((Control)(object)txtDeliveryChargeValue).Text == "") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, dtRoomData.Rows[0]["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), bool.Parse(drMaster["IsPrinted"].ToString()) ? "1" : "0", (drMaster["PrintUserID"] == DBNull.Value) ? "Null" : drMaster["PrintUserID"].ToString(), (drMaster["PrintDate"] == DBNull.Value) ? "Null" : drMaster["PrintDate"].ToString(), bool.Parse(drMaster["IsCancelled"].ToString()) ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["HasChanges"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				string text2 = ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					text = text + ((UltraGridBase)ULGData).Rows[j].Cells["SpecialOrderDetailID"].Value.ToString() + ",";
				}
				for (int k = 0; k < dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
				{
					text2 = text2 + dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailAccessoryID"].ToString() + ",";
				}
				Main.DeleteForUpdate("POS_SpecialOrdersDetailsAccessories", "SpecialOrderID", drMaster["SpecialOrderID"].ToString(), "SpecialOrderDetailAccessoryID", text2);
				for (int l = 0; l < dtDeletedItems.Rows.Count; l++)
				{
					SpecialOrdersDetails.DeleteVirtualWithNotes(dtDeletedItems.Rows[l]["SpecialOrderDetailID"].ToString(), dtDeletedItems.Rows[l]["Notes"].ToString(), GlobalVariables.UserID);
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[m].Cells["SpecialOrderDetailID"].Value.ToString());
					int num3 = SpecialOrdersDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[m].Cells["SpecialOrderDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[m].Cells["SpecialOrderDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[m].Cells["SpecialOrderDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[m].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[m].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[m].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[m].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["ReturnedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["ReturnedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[m].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[m].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[m].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[m].Cells["ActualUnitSalesPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[m].Cells["Notes"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int n = 0; n < dtSpecialOrdersDetailsAccessories.Rows.Count; n++)
					{
						if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[n]["SpecialOrderDetailID"].ToString()) == num2)
						{
							dtSpecialOrdersDetailsAccessories.Rows[n]["SpecialOrderDetailID"] = num3;
						}
					}
				}
				for (int num4 = 0; num4 < dtSpecialOrdersDetailsAccessories.Rows.Count; num4++)
				{
					dtSpecialOrdersDetailsAccessories.Rows[num4]["VoucherDate"] = dtpDate.DateTime;
					dtSpecialOrdersDetailsAccessories.Rows[num4]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtSpecialOrdersDetailsAccessories.Rows[num4]["SpecialOrderID"] = num.ToString();
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				if (dtSpecialOrdersDetailsAccessories.Rows.Count > 0)
				{
					SpecialOrdersDetailsAccessories.Insert_UpdateByTable(dtSpecialOrdersDetailsAccessories, GlobalVariables.UserID);
				}
			}
			string text3 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text3 != "")
			{
				GlobalVariables.InformationMB.Show(text3);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "SOMIV", "SOMIV");
			}
			else
			{
				RowID = num.ToString();
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
			SpecialOrders.DeleteVirtual(drMaster["SpecialOrderID"].ToString(), GlobalVariables.UserID);
			SpecialOrdersDetails.DeleteVirtualBySpecialOrderID(drMaster["SpecialOrderID"].ToString(), GlobalVariables.UserID);
			SpecialOrdersDetailsAccessories.DeleteVirtualBySpecialOrderID(drMaster["SpecialOrderID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SpecialOrdersReport("," + dtRoomData.Rows[0]["RoomID"].ToString() + ",", GlobalVariables.BranchIDs);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SpecialOrderID"].ToString();
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
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlVisaType.ValueListItems.Clear();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[k]["VisaTypeID"], dataTable.Rows[k]["VisaTypeName"].ToString());
			}
		}
		else
		{
			vlVisaType.ValueListItems.Clear();
			for (int l = 0; l < dtVisaType.Rows.Count; l++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[l]["VisaTypeID"], dtVisaType.Rows[l]["VisaTypeName"].ToString());
			}
		}
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
		dtItemsAndGroupsCopy = dtItemsAndGroups.Copy();
		FillItemsGroups();
		dvItems = new DataView(dtItemsAndGroups);
		dvItems.RowFilter = " IsMain=0 ";
		dvItems.ToTable();
		for (int m = 0; m < dvItems.Count; m++)
		{
			vlItems.ValueListItems.Add(dvItems[m]["ItemID"], dvItems[m]["ItemName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtClients = Clients.FillCombo("1", GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
	}

	public void FillItemsGroups()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		int num = 8;
		int num2 = 6;
		DataTable dataTable = dtItemsAndGroups.DefaultView.ToTable(true, "ItemFirstClassificationName");
		DataView dataView = new DataView(dtItemsAndGroups);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			UltraPanel val = new UltraPanel();
			dataView.RowFilter = ((dataTable.Rows[i]["ItemFirstClassificationName"] == DBNull.Value) ? "IsMain=1 And ItemFirstClassificationName is null" : ("IsMain=1 And ItemFirstClassificationName='" + dataTable.Rows[i]["ItemFirstClassificationName"].ToString() + "'"));
			dataView.ToTable();
			if (dataView.Count > 0)
			{
				((UltraTabControlBase)UTCItemsGroups).Tabs.Add(i.ToString(), (dataTable.Rows[i]["ItemFirstClassificationName"] != DBNull.Value) ? dataTable.Rows[i]["ItemFirstClassificationName"].ToString() : (GlobalVariables.IsArabic ? "مجموعات الاصناف" : "Item Groups"));
				((UltraControlBase)UTCItemsGroups).UseAppStyling = false;
				((UltraTabControlBase)UTCItemsGroups).TabSize = new Size(15, 40);
				((UltraTabControlBase)UTCItemsGroups).Tabs[i.ToString()].FixedWidth = 120;
				((UltraTabControlBase)UTCItemsGroups).TabHeaderAreaAppearance.FontData.SizeInPoints = 10f;
				((Control)(object)val).Dock = DockStyle.Fill;
				val.AutoScroll = true;
				((Control)(object)((UltraTabControlBase)UTCItemsGroups).Tabs[i.ToString()].TabPage).Controls.Add((Control)(object)val);
			}
			num = 8;
			num2 = 6;
			for (int j = 0; j < dataView.Count; j++)
			{
				UltraButton val2 = new UltraButton();
				((Control)(object)val2).Click += btnGroub_Click;
				((Control)(object)val2).Tag = dataView[j]["ItemID"].ToString() + "," + dataView[j]["IsOffer"].ToString() + "," + dataView[j]["IsQtyDiscount"].ToString();
				((Control)(object)val2).Text = dataView[j]["ItemName"].ToString();
				((Control)(object)val2).Height += 25;
				((Control)(object)val.ClientArea).Controls.Add((Control)(object)val2);
				((UltraControlBase)val2).Update();
				if (num + ((Control)(object)val2).Width > ((Control)(object)val).Width)
				{
					num2 += ((Control)(object)val2).Height;
					num = 8;
				}
				((Control)(object)val2).Left = num;
				((Control)(object)val2).Top = num2;
				((Control)(object)val2).Width = (((Control)(object)val).Width - GlobalVariables.ScrollWidth) / 7;
				num += ((Control)(object)val2).Width;
			}
		}
	}

	public void btnGroub_Click(object sender, EventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		((Control)(object)pnlItems).Visible = false;
		((Control)(object)pnlItems.ClientArea).Controls.Clear();
		string[] array = ((Control)(UltraButton)sender).Tag.ToString().Split(',');
		string text = array[0].ToString();
		bool flag = Convert.ToBoolean(array[1].ToString());
		bool flag2 = bool.Parse(array[2].ToString());
		if (!flag)
		{
			DataView dataView = new DataView(dtItemsAndGroups);
			dataView.RowFilter = "IsMain=0 And ParentID=" + text;
			dataView.ToTable();
			int num = 8;
			int num2 = 6;
			for (int i = 0; i < dataView.Count; i++)
			{
				UltraButton val = new UltraButton();
				((Control)(object)val).Click += btnItems_Click;
				((Control)(object)val).Tag = dataView[i]["ItemID"].ToString();
				((Control)(object)val).Text = dataView[i]["ItemName"].ToString();
				((Control)(object)val).Height += 40;
				((Control)(object)pnlItems.ClientArea).Controls.Add((Control)(object)val);
				((UltraControlBase)val).Update();
				if (num + ((Control)(object)val).Width > ((Control)(object)pnlItems).Width)
				{
					num2 += ((Control)(object)val).Height;
					num = 8;
				}
				((Control)(object)val).Left = num;
				((Control)(object)val).Top = num2;
				((Control)(object)val).Width = (((Control)(object)pnlItems).Width - GlobalVariables.ScrollWidth) / 6;
				num += ((Control)(object)val).Width;
			}
		}
		((Control)(object)pnlItems).Visible = true;
	}

	public void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0e: Expected O, but got Unknown
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			DataRow dataRow = dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value)[0];
			if (int.Parse(((Control)(UltraButton)sender).Tag.ToString()) == int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) && (dataRow["AccessoriesCount"] == DBNull.Value || int.Parse(dataRow["AccessoriesCount"].ToString()) == 0))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				if (bool.Parse(dataRow["IsWeight"].ToString()) || bool.Parse(dataRow["UsePOSNumPad"].ToString()))
				{
					frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGData).Rows[i].Cells["Qty"], ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
					frmDecimal2.ShowDialog();
				}
				else
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode -= SelectFullRow;
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((!Adding && !Updating) ? 2 : 6);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value = ++newID;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = ((Control)(UltraButton)sender).Tag;
		DataRow dataRow2 = dtItemsAndGroups.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow2["Price"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow2["TaxID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow2["StoreID"];
		if (bool.Parse(dataRow2["IsWeight"].ToString()) || bool.Parse(dataRow2["UsePOSNumPad"].ToString()))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			frmDecimal frmDecimal3 = new frmDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"], ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			frmDecimal3.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal3.ShowDialog();
		}
		else
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		}
		if (dataRow2["AccessoriesCount"] != DBNull.Value && int.Parse(dataRow2["AccessoriesCount"].ToString()) > 0)
		{
			int accessoriescount = int.Parse(dataRow2["AccessoriesCount"].ToString());
			bool enforceAccessories = bool.Parse(dtItemsAndGroups.Select(" IsOffer = 0 and ItemID = " + dataRow2["ItemID"].ToString())[0]["EnforceAccessories"].ToString());
			frmSpecialOrdersDetailsAccessories frmSpecialOrdersDetailsAccessories2 = new frmSpecialOrdersDetailsAccessories(accessoriescount, int.Parse(dataRow2["ItemID"].ToString()), int.Parse(dataRow2["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()), canedit: true, enforceAccessories);
			frmSpecialOrdersDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmSpecialOrdersDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
			frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = null;
			frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = dtSpecialOrdersDetailsAccessories.Clone();
			DataView dataView = new DataView(dtSpecialOrdersDetailsAccessories);
			dataView.RowFilter = " SpecialOrderDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString();
			DataTable dataTable = dataView.ToTable();
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.ImportRow(dataTable.Rows[j]);
			}
			frmSpecialOrdersDetailsAccessories2.ShowDialog();
			if (!frmSpecialOrdersDetailsAccessories2.Cancel)
			{
				for (int k = 0; k < dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
				{
					if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()))
					{
						dtSpecialOrdersDetailsAccessories.Rows[k].Delete();
						dtSpecialOrdersDetailsAccessories.AcceptChanges();
						k--;
					}
				}
				for (int l = 0; l < frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows.Count; l++)
				{
					dtSpecialOrdersDetailsAccessories.ImportRow(frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows[l]);
				}
			}
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		CalculateGoss();
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateTotalsTax();
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
		int index = ((UltraGridBase)ULGData).ActiveRow.Index;
		((UltraGridBase)ULGData).ActiveRow.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).Rows[index].Cells["Qty"].Activate();
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode += SelectFullRow;
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()) && CheckID != 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إضافة صنف تمت طباعة الشيك" : "Cannot Add this Item Because Check is Printed");
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItemsAndGroups.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["SpecialOrderDetailID"].Value = ++newID;
			e.Row.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
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
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (CheckID != 0 && drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لايمكن حذف هذا الصنف لانه تمت طباعة  الشيك", "Cannot Delete this Item Because it is Printed");
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		bool flag = false;
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (int.Parse(e.Rows[i].Cells["SpecialOrderDetailID"].Value.ToString()) <= -100000 || int.Parse(e.Rows[i].Cells["SpecialOrderDetailID"].Value.ToString()) > -1)
			{
				flag = true;
				break;
			}
		}
		frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Notes", _IsInt: false, _IsNumeric: false);
		if (flag)
		{
			frmPOSComments2.WindowState = FormWindowState.Normal;
			frmPOSComments2.ShowDialog();
			if (frmPOSComments2.Value == "")
			{
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
		}
		for (int j = 0; j < e.Rows.Length; j++)
		{
			if ((int.Parse(e.Rows[j].Cells["SpecialOrderDetailID"].Value.ToString()) <= -100000 || int.Parse(e.Rows[j].Cells["SpecialOrderDetailID"].Value.ToString()) > -1) && frmPOSComments2 != null)
			{
				dtDeletedItems.Rows.Add(e.Rows[j].Cells["SpecialOrderDetailID"].Value.ToString(), frmPOSComments2.Value);
			}
			for (int k = 0; k < dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
			{
				if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailID"].ToString()) == int.Parse(e.Rows[j].Cells["SpecialOrderDetailID"].Value.ToString()))
				{
					dtSpecialOrdersDetailsAccessories.Rows[k].Delete();
					dtSpecialOrdersDetailsAccessories.AcceptChanges();
					k--;
				}
			}
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow["Price"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["StoreID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateGoss();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
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
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		if ((Adding || Updating) && ((!(((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice") && !(((KeyedSubObjectBase)e.Cell.Column).Key == "TotalPrice")) || e.Cell.Row.Cells["ItemID"].Value == DBNull.Value || bool.Parse(dtItemsAndGroups.Select(" ItemID =" + e.Cell.Row.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString())) && ((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" && ((drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString()) && e.Cell.Row.Cells["OfferID"].Value == DBNull.Value) || CheckID == 0))
		{
			frmDecimal frmDecimal2 = new frmDecimal(e.Cell, e.Cell.Value.ToString());
			frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal2.ShowDialog();
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Comments")
		{
			frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Comments", _IsInt: false, _IsNumeric: false, e.Cell.Row.Cells["Notes"].Value.ToString());
			frmPOSComments2.WindowState = FormWindowState.Normal;
			if (frmPOSComments2.ShowDialog() == DialogResult.OK)
			{
				e.Cell.Row.Cells["Notes"].Value = frmPOSComments2.Value;
			}
		}
	}

	private void ULGDataPayments_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Selected = true;
	}

	private void ULGDataPayments_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		for (int i = 0; i < e.Rows.Length; i++)
		{
			DataTable dataTable = ShiftsDetailsUsers.Select(e.Rows[i].Cells["ShiftDetailUserID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows[0]["IsClosed"].Equals(true))
			{
				GlobalVariables.QuestionMB.Show("لا يمكن حذف السداد لوجود خزينة المستخدم مغلقة؟", " Data Cannot be Deleted User Safe Is Closed");
				((CancelEventArgs)(object)e).Cancel = true;
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		CalculateNetTotals();
		ShiftsDetails.RevenueJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemsAndGroups != null && dtItemsAndGroups.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && (decimal.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["RoomDiscountValue"].ToString()) > 0m || bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString())))
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

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		decimal num2 = CalculateGrossWithoutItemUnderDiscount();
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		((Control)(object)txtDiscountValue).Text = ((num2 == 0m) ? "0" : decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num2).ToString()).ToString(GlobalVariables.txtDecimalFormate));
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
		((Control)(object)txtDiscountValue2).Text = ((num2 == 0m) ? "0" : decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num2).ToString()).ToString(GlobalVariables.txtDecimalFormate));
		((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtDiscountRatio).Text != "" && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscountRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "0" && Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItemsAndGroups.Select("ItemID=" + Row.Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString()))
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m;
		}
		else
		{
			Row.Cells["DisCount"].Value = 0;
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

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * (-1m * decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text)) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
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

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
		{
			double num = double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString());
			double num2 = Math.Round(double.Parse(((Control)(object)txtNetprice).Text) % num, 3);
			double num3 = num / 2.0;
			double num4 = num - num2;
			if (num2 < num3)
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((double.Parse(((Control)(object)txtNetprice).Text) - num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRoundingValue).Text = decimal.Parse((num2 * -1.0).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((double.Parse(((Control)(object)txtNetprice).Text) + num4).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRoundingValue).Text = decimal.Parse(num4.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Expected O, but got Unknown
		//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Expected O, but got Unknown
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string value = arrayList[1].ToString();
		string value2 = arrayList[2].ToString();
		string text2 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtItemsAndGroups.Select("IsMain=0 And ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		int num = int.Parse(dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
		if (GlobalFunctions.GetOption("UseScaleBarCode") && dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["IsWeight"] != DBNull.Value && bool.Parse(dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["IsWeight"].ToString()))
		{
			s = (decimal.Parse(s) / 1000m).ToString();
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) == num && (dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"] == DBNull.Value || int.Parse(dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"].ToString()) == 0))
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
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode -= SelectFullRow;
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value = ++newID;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtItemsAndGroups.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["StoreID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (dataRow["AccessoriesCount"] != DBNull.Value && int.Parse(dataRow["AccessoriesCount"].ToString()) > 0)
		{
			int accessoriescount = int.Parse(dataRow["AccessoriesCount"].ToString());
			bool enforceAccessories = bool.Parse(dtItemsAndGroups.Select(" IsOffer = 0 and ItemID = " + dataRow["ItemID"].ToString())[0]["EnforceAccessories"].ToString());
			frmSpecialOrdersDetailsAccessories frmSpecialOrdersDetailsAccessories2 = new frmSpecialOrdersDetailsAccessories(accessoriescount, int.Parse(dataRow["ItemID"].ToString()), int.Parse(dataRow["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()), canedit: true, enforceAccessories);
			frmSpecialOrdersDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmSpecialOrdersDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
			frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = null;
			frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = dtSpecialOrdersDetailsAccessories.Clone();
			DataView dataView = new DataView(dtSpecialOrdersDetailsAccessories);
			dataView.RowFilter = " SpecialOrderDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString();
			DataTable dataTable = dataView.ToTable();
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.ImportRow(dataTable.Rows[j]);
			}
			frmSpecialOrdersDetailsAccessories2.ShowDialog();
			if (!frmSpecialOrdersDetailsAccessories2.Cancel)
			{
				for (int k = 0; k < dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
				{
					if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[k]["SpecialOrderDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()))
					{
						dtSpecialOrdersDetailsAccessories.Rows[k].Delete();
						dtSpecialOrdersDetailsAccessories.AcceptChanges();
						k--;
					}
				}
				for (int l = 0; l < frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows.Count; l++)
				{
					dtSpecialOrdersDetailsAccessories.ImportRow(frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows[l]);
				}
			}
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow["Price"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		CalculateGoss();
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateTotalsTax();
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = value;
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = value2;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode += SelectFullRow;
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
	}

	private void btnPlus_Click(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraGridBase)ULGData).ActiveRow != null && CheckID != 0 && drMaster != null && drMaster["IsPrinted"].Equals(false))
		{
			ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + 1.0;
		}
	}

	private void btnSubtract_Click(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraGridBase)ULGData).ActiveRow != null && CheckID != 0 && drMaster != null && drMaster["IsPrinted"].Equals(false) && double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 1.0)
		{
			ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) - 1.0;
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.POSClientsSearch(GlobalVariables.CurrentBranchID, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	public void PrintCheck()
	{
		if (!SaveClose(Close: false) || !(RowID != ""))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		DataRow[] array = dtReports.Select("ReportID=" + (dtRoomData.Rows[0]["SpecialOrderReportID"].Equals(DBNull.Value) ? "0" : dtRoomData.Rows[0]["SpecialOrderReportID"]));
		if (array.Length != 0)
		{
			reportDocument.Load(GlobalVariables.ReportsPath + array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
		}
		else
		{
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_SpecialOrders_A.rpt" : "Rep_POS_SpecialOrders_E.rpt"));
		}
		GlobalFunctions.ConfigureReport(reportDocument);
		reportDocument.SetParameterValue("@SpecialOrderIDs", "," + RowID + ",");
		reportDocument.SetParameterValue("@RoomID", RoomID.ToString());
		reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
		try
		{
			if (((UltraToggleEditorBase)chkIsDeliverd).Checked)
			{
				int num = int.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
				if (num > 0)
				{
					reportDocument.PrintToPrinter(int.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString()), collated: true, 0, 10000);
				}
			}
			else
			{
				reportDocument.PrintToPrinter(int.Parse(dtRoomData.Rows[0]["PrintCheckCount"].ToString()), collated: true, 0, 10000);
			}
			drMaster["IsPrinted"] = true;
			drMaster["PrintUserID"] = GlobalVariables.UserID;
			drMaster["PrintDate"] = DateTime.Now;
			SaveClose(Close: false);
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		if (dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintCheck"].ToString()))
		{
			PrintCheck();
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Delete(false);
		}
	}

	private void btnDiscountValue_Click(object sender, EventArgs e)
	{
		OpenChangeDiscountForm();
	}

	private void btnDiscountRatio_Click(object sender, EventArgs e)
	{
		OpenChangeDiscountForm();
	}

	public void OpenChangeDiscountForm()
	{
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		decimal totalamount = CalculateGrossWithoutItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(totalamount, decimal.Parse(((Control)(object)txtDiscountValue).Text), decimal.Parse(((Control)(object)txtDiscountRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountRatio).Text = ((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString("0.##########") : decimal.Parse(UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate));
			((Control)(object)txtDiscountValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			return;
		}
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)txtDiscountRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscountValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[j]);
		}
		CalculateTotalsTax();
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscountValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "0" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) / num * 100m, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
		}
		else
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = "0";
			((Control)(object)txtDiscountValue).Text = "0";
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void txtDiscountRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = "0";
			((Control)(object)txtDiscountValue).Text = "0";
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void btnDiscountRatio2_Click(object sender, EventArgs e)
	{
		OpenChangeDiscount2Form();
	}

	private void btnDiscountValue2_Click(object sender, EventArgs e)
	{
		OpenChangeDiscount2Form();
	}

	public void OpenChangeDiscount2Form()
	{
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(num, decimal.Parse(((Control)(object)txtDiscountValue2).Text), decimal.Parse(((Control)(object)txtDiscountRatio2).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscountRatio2).Text = decimal.Parse(UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscountRatio2).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscountValue2).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscountValue2_ValueChanged(object sender, EventArgs e)
	{
	}

	private void txtDiscountRatio2_ValueChanged(object sender, EventArgs e)
	{
	}

	public override void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 ");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	private void btnDeliveryChargeValue_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtDeliveryChargeValue, ((Control)(object)txtDeliveryChargeValue).Text);
		frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
		frmDecimal2.Show();
	}

	private void txtDeliveryChargeValue_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtDeliveryChargeValue).Text != "" && decimal.Parse(((Control)(object)txtDeliveryChargeValue).Text) > 0m)
		{
			CalculateNetTotals();
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Expected O, but got Unknown
		if (base.Disposing)
		{
			return;
		}
		if (cboClient.SelectedIndex > -1)
		{
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == DBNull.Value || dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == dtRoomData.Rows[0]["PriceTypeID"])
			{
				dtItemsAndGroups = dtItemsAndGroupsCopy;
			}
			else
			{
				dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
			}
		}
		else
		{
			((Control)(object)txtDiscountRatio).Text = "0";
			dtItemsAndGroups = dtItemsAndGroupsCopy;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString());
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
		}
		CalculateGoss();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = SpecialOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnAccessories_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			return;
		}
		DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		if (dataRow["AccessoriesCount"] == DBNull.Value || int.Parse(dataRow["AccessoriesCount"].ToString()) <= 0)
		{
			return;
		}
		bool canedit = true;
		int accessoriescount = int.Parse(dataRow["AccessoriesCount"].ToString());
		bool enforceAccessories = bool.Parse(dtItemsAndGroups.Select(" IsOffer = 0 and ItemID = " + dataRow["ItemID"].ToString())[0]["EnforceAccessories"].ToString());
		frmSpecialOrdersDetailsAccessories frmSpecialOrdersDetailsAccessories2 = new frmSpecialOrdersDetailsAccessories(accessoriescount, int.Parse(dataRow["ItemID"].ToString()), int.Parse(dataRow["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()), canedit, enforceAccessories);
		frmSpecialOrdersDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmSpecialOrdersDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
		frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = null;
		frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories = dtSpecialOrdersDetailsAccessories.Clone();
		DataView dataView = new DataView(dtSpecialOrdersDetailsAccessories);
		dataView.RowFilter = " SpecialOrderDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString();
		DataTable dataTable = dataView.ToTable();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.ImportRow(dataTable.Rows[i]);
		}
		frmSpecialOrdersDetailsAccessories2.ShowDialog();
		if (frmSpecialOrdersDetailsAccessories2.Cancel)
		{
			return;
		}
		for (int j = 0; j < dtSpecialOrdersDetailsAccessories.Rows.Count; j++)
		{
			if (int.Parse(dtSpecialOrdersDetailsAccessories.Rows[j]["SpecialOrderDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SpecialOrderDetailID"].Value.ToString()))
			{
				dtSpecialOrdersDetailsAccessories.Rows[j].Delete();
				dtSpecialOrdersDetailsAccessories.AcceptChanges();
				j--;
			}
		}
		for (int k = 0; k < frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows.Count; k++)
		{
			dtSpecialOrdersDetailsAccessories.ImportRow(frmSpecialOrdersDetailsAccessories2.dtSpecialOrdersDetailsAccessories.Rows[k]);
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void btnSpecialOrdersPayments_Click(object sender, EventArgs e)
	{
		SaveClose(Close: false);
		if (Updating)
		{
			frmSpecialOrderPayments frmSpecialOrderPayments2 = new frmSpecialOrderPayments(int.Parse(drMaster["SpecialOrderID"].ToString()), decimal.Parse(((Control)(object)txtRestAmount).Text.ToString()));
			frmSpecialOrderPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmSpecialOrderPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد الفواتير" : "Invoices Payments");
			frmSpecialOrderPayments2.CanAdd = CanAdd;
			frmSpecialOrderPayments2.CanUpdate = CanUpdate;
			frmSpecialOrderPayments2.CanDelete = CanDelete;
			frmSpecialOrderPayments2.CanDiscount = CanDiscount;
			frmSpecialOrderPayments2.CanSearching = CanSearching;
			frmSpecialOrderPayments2.CanExport = CanExport;
			frmSpecialOrderPayments2.CanPrint = CanPrint;
			frmSpecialOrderPayments2.CanPrintReport = CanPrintReport;
			frmSpecialOrderPayments2.CanViewReport = CanViewReport;
			frmSpecialOrderPayments2.ShowDialog();
			dtSpecialOrderPayments = SpecialOrdersPayments.SelectBySpecialOrderID(drMaster["SpecialOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			object obj = dtSpecialOrderPayments.Compute(" Sum(CashAmount) ", "");
			object obj2 = dtSpecialOrderPayments.Compute(" Sum(VisaAmount) ", "");
			((Control)(object)txtPaidAmount).Text = decimal.Parse((decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()) + decimal.Parse((obj2 == DBNull.Value) ? "0" : obj2.ToString())).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraGridBase)ULGDataPayments).DataSource = dtSpecialOrderPayments;
			InitGrid();
			btnOKClick();
		}
	}

	private void chkIsDeliverd_CheckedChanged(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkIsDeliverd).Checked)
		{
			((UltraToggleEditorBase)chkIsManufactured).Checked = true;
			dtpDeliverdDate.DateTime = DateTime.Now;
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
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Expected O, but got Unknown
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Expected O, but got Unknown
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Expected O, but got Unknown
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Expected O, but got Unknown
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Expected O, but got Unknown
		//IL_0e70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmSpecialOrderChecks));
		UltraTab val = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.pnlItems = new UltraPanel();
		this.btnClientSearch = new UltraButton();
		this.txtBarCode = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.UGBItemData = new UltraGroupBox();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDeliveryChargeValue = new UltraLabel();
		this.txtDeliveryChargeValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.btnPlus = new UltraButton();
		this.btnSubtract = new UltraButton();
		this.lblDiscountValue = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.btnClear = new UltraButton();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.btnDiscountRatio = new UltraButton();
		this.btnDiscountValue = new UltraButton();
		this.btnDeliveryChargeValue = new UltraButton();
		this.lblRoundingValue = new UltraLabel();
		this.txtRoundingValue = new UltraTextEditor();
		this.btnAccessories = new UltraButton();
		this.chkIsManufactured = new UltraCheckEditor();
		this.dtpManuFacturedDate = new UltraDateTimeEditor();
		this.dtpDeliverdDate = new UltraDateTimeEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.btnSpecialOrdersPayments = new UltraButton();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.btnDiscountValue2 = new UltraButton();
		this.btnDiscountRatio2 = new UltraButton();
		this.lblDiscountRatio2 = new UltraLabel();
		this.txtDiscountRatio2 = new UltraTextEditor();
		this.lblDiscountValue2 = new UltraLabel();
		this.txtDiscountValue2 = new UltraTextEditor();
		this.UTCItemsGroups = new UltraTabControl();
		this.ultraTabSharedControlsPage2 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsManufactured).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpManuFacturedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Payments";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		base.ULGData.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
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
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPayments.AfterEnterEditMode += new System.EventHandler(ULGDataPayments_AfterEnterEditMode);
		this.ULGDataPayments.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataPayments_BeforeRowsDeleted);
		resources.ApplyResources(this.pnlItems, "pnlItems");
		this.pnlItems.AutoScroll = true;
		resources.ApplyResources(this.pnlItems.ClientArea, "pnlItems.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlItems).Name = "pnlItems";
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance16");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.UGBItemData, "UGBItemData");
		this.UGBItemData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItemData).Controls.Add((System.Windows.Forms.Control)(object)this.pnlItems);
		((System.Windows.Forms.Control)(object)this.UGBItemData).Name = "UGBItemData";
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDeliveryChargeValue, "lblDeliveryChargeValue");
		this.lblDeliveryChargeValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue).Name = "lblDeliveryChargeValue";
		((ControlBase)this.lblDeliveryChargeValue).WrapText = false;
		resources.ApplyResources(this.txtDeliveryChargeValue, "txtDeliveryChargeValue");
		((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue).Name = "txtDeliveryChargeValue";
		((EditorButtonControlBase)this.txtDeliveryChargeValue).ReadOnly = true;
		((TextEditorControlBase)this.txtDeliveryChargeValue).ValueChanged += new System.EventHandler(txtDeliveryChargeValue_ValueChanged);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.btnPlus, "btnPlus");
		((System.Windows.Forms.Control)(object)this.btnPlus).Name = "btnPlus";
		((System.Windows.Forms.Control)(object)this.btnPlus).Click += new System.EventHandler(btnPlus_Click);
		resources.ApplyResources(this.btnSubtract, "btnSubtract");
		((System.Windows.Forms.Control)(object)this.btnSubtract).Name = "btnSubtract";
		((System.Windows.Forms.Control)(object)this.btnSubtract).Click += new System.EventHandler(btnSubtract_Click);
		resources.ApplyResources(this.lblDiscountValue, "lblDiscountValue");
		this.lblDiscountValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue).Name = "lblDiscountValue";
		((ControlBase)this.lblDiscountValue).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue, "txtDiscountValue");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).Name = "txtDiscountValue";
		((EditorButtonControlBase)this.txtDiscountValue).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountValue).ValueChanged += new System.EventHandler(txtDiscountValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.lblDiscountRatio, "lblDiscountRatio");
		this.lblDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio).Name = "lblDiscountRatio";
		((ControlBase)this.lblDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio, "txtDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).Name = "txtDiscountRatio";
		((EditorButtonControlBase)this.txtDiscountRatio).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountRatio).ValueChanged += new System.EventHandler(txtDiscountRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.btnDiscountRatio, "btnDiscountRatio");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Name = "btnDiscountRatio";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Click += new System.EventHandler(btnDiscountRatio_Click);
		resources.ApplyResources(this.btnDiscountValue, "btnDiscountValue");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Name = "btnDiscountValue";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Click += new System.EventHandler(btnDiscountValue_Click);
		resources.ApplyResources(this.btnDeliveryChargeValue, "btnDeliveryChargeValue");
		((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue).Name = "btnDeliveryChargeValue";
		((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue).Click += new System.EventHandler(btnDeliveryChargeValue_Click);
		resources.ApplyResources(this.lblRoundingValue, "lblRoundingValue");
		this.lblRoundingValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoundingValue).Name = "lblRoundingValue";
		((ControlBase)this.lblRoundingValue).WrapText = false;
		resources.ApplyResources(this.txtRoundingValue, "txtRoundingValue");
		((System.Windows.Forms.Control)(object)this.txtRoundingValue).Name = "txtRoundingValue";
		((EditorButtonControlBase)this.txtRoundingValue).ReadOnly = true;
		resources.ApplyResources(this.btnAccessories, "btnAccessories");
		((System.Windows.Forms.Control)(object)this.btnAccessories).Name = "btnAccessories";
		((System.Windows.Forms.Control)(object)this.btnAccessories).Click += new System.EventHandler(btnAccessories_Click);
		resources.ApplyResources(this.chkIsManufactured, "chkIsManufactured");
		((System.Windows.Forms.Control)(object)this.chkIsManufactured).Name = "chkIsManufactured";
		resources.ApplyResources(this.dtpManuFacturedDate, "dtpManuFacturedDate");
		((UltraWinEditorMaskedControlBase)this.dtpManuFacturedDate).AlwaysInEditMode = true;
		this.dtpManuFacturedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpManuFacturedDate).Name = "dtpManuFacturedDate";
		resources.ApplyResources(this.dtpDeliverdDate, "dtpDeliverdDate");
		((UltraWinEditorMaskedControlBase)this.dtpDeliverdDate).AlwaysInEditMode = true;
		this.dtpDeliverdDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDeliverdDate).Name = "dtpDeliverdDate";
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		((UltraToggleEditorBase)this.chkIsDeliverd).CheckedChanged += new System.EventHandler(chkIsDeliverd_CheckedChanged);
		resources.ApplyResources(this.btnSpecialOrdersPayments, "btnSpecialOrdersPayments");
		((System.Windows.Forms.Control)(object)this.btnSpecialOrdersPayments).Name = "btnSpecialOrdersPayments";
		((System.Windows.Forms.Control)(object)this.btnSpecialOrdersPayments).Click += new System.EventHandler(btnSpecialOrdersPayments_Click);
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((EditorButtonControlBase)this.txtPaidAmount).ReadOnly = true;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.btnDiscountValue2, "btnDiscountValue2");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Name = "btnDiscountValue2";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Click += new System.EventHandler(btnDiscountValue2_Click);
		resources.ApplyResources(this.btnDiscountRatio2, "btnDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Name = "btnDiscountRatio2";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Click += new System.EventHandler(btnDiscountRatio2_Click);
		resources.ApplyResources(this.lblDiscountRatio2, "lblDiscountRatio2");
		this.lblDiscountRatio2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio2).Name = "lblDiscountRatio2";
		((ControlBase)this.lblDiscountRatio2).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio2, "txtDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio2).Name = "txtDiscountRatio2";
		((EditorButtonControlBase)this.txtDiscountRatio2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountRatio2).ValueChanged += new System.EventHandler(txtDiscountRatio2_ValueChanged);
		resources.ApplyResources(this.lblDiscountValue2, "lblDiscountValue2");
		this.lblDiscountValue2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue2).Name = "lblDiscountValue2";
		((ControlBase)this.lblDiscountValue2).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue2, "txtDiscountValue2");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue2).Name = "txtDiscountValue2";
		((EditorButtonControlBase)this.txtDiscountValue2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountValue2).ValueChanged += new System.EventHandler(txtDiscountValue2_ValueChanged);
		resources.ApplyResources(this.UTCItemsGroups, "UTCItemsGroups");
		resources.ApplyResources(val16, "appearance15");
		((AppearanceBase)val16).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCItemsGroups).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage2);
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Name = "UTCItemsGroups";
		((UltraTabControlBase)this.UTCItemsGroups).SharedControlsPage = this.ultraTabSharedControlsPage2;
		resources.ApplyResources(this.ultraTabSharedControlsPage2, "ultraTabSharedControlsPage2");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage2).Name = "ultraTabSharedControlsPage2";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCItemsGroups);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSpecialOrdersPayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliverdDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpManuFacturedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccessories);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtract);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPlus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsManufactured);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmSpecialOrderChecks";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsManufactured, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPlus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtract, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccessories, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpManuFacturedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliverdDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSpecialOrdersPayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCItemsGroups, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsManufactured).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpManuFacturedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
