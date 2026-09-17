using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.POS.MasterData;

public class frmRooms : frmHeaderManyDetails
{
	private DataTable dtRoomsSetting;

	private DataTable dtClients;

	private DataTable dtCities;

	private DataTable dtClassification;

	private DataTable dtSubAccounts;

	private DataTable dtDineinReports;

	private DataTable dtDeliveryReports;

	private DataTable dtTakeAwayReports;

	private DataTable dtSpecialOrderReports;

	private DataTable dtStores;

	private DataTable dtPriceType;

	private DataTable dtRoomsItems;

	private DataTable dtItems;

	private DataTable dtBranchs;

	private DataTable dtMinCharge;

	private DataTable dtMaxCharge;

	private DataTable dtServices;

	private DataTable dtOffers;

	private DataTable dtRoomsOffers;

	private ValueList vlOffers = new ValueList();

	private string[,] arWeekDays = new string[7, 2]
	{
		{ "السبت", "Saturday" },
		{ "الاحد", "Sunday" },
		{ "الاثنين", "Monday" },
		{ "الثلاثاء", "Tuesday" },
		{ "الاربعاء", "Wednesday" },
		{ "الخميس", "Thursday" },
		{ "الجمعة", "Friday" }
	};

	private bool IsRoomImageChanged = false;

	private bool IsRoomLogoChanged = false;

	private IContainer components = null;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboStore;

	private UltraLabel lblStoreName;

	private UltraButton btnDesignHall;

	private UltraButton btnPic;

	private UltraLabel lblPic;

	private UltraPictureBox picRoom;

	private UltraTextEditor txtTableCount;

	private UltraLabel lblTableCount;

	private UltraTextEditor txtRoomEnglishName;

	private UltraLabel lblRoomEnglishName;

	private UltraTextEditor txtRoomArabicName;

	private UltraLabel lblRoomArabicName;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItems;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAll;

	private OpenFileDialog ofdPicture;

	private UltraPanel pnlCheckType;

	private RadioButton rbDineIn;

	private RadioButton rbTakeAway;

	private RadioButton rbDelivery;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGMinCharge;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranches;

	private UltraCheckEditor chkMinCharge;

	private UltraCheckEditor chkDynamicMinCharge;

	private UltraTextEditor txtMinChargeAmount;

	private UltraLabel lblMinChargeAmount;

	private UltraTextEditor txtServiceChargePercent;

	private UltraLabel lblServiceCharge;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboMinChargeDiff;

	private UltraLabel lblMinChargeDiff;

	private UltraLabel lblRoomLogo;

	private UltraPictureBox picbRoomLogo;

	private UltraButton btnRoomLogoPath;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraLabel lblCompanyMessage;

	private UltraTextEditor txtCompanyMessage;

	private UltraComboEditor cboTakeAwayReport;

	private UltraLabel lblTakeAwayReport;

	private UltraComboEditor cboDeliveryReport;

	private UltraLabel lblDeliveryReport;

	private UltraComboEditor cboDineInReport;

	private UltraLabel lblDineInReport;

	private UltraComboEditor cboDefaultSubAccount;

	private UltraLabel lblDefaultSubAccount;

	private UltraCheckEditor chkSentItemsMessage;

	private UltraLabel lblPrintAlert2;

	private UltraLabel lblPrintAlert1;

	private UltraLabel lblCheckAlert2;

	private UltraTextEditor txtPrintAlert;

	private UltraLabel lblCheckAlert1;

	private UltraTextEditor txtCheckAlert;

	private UltraTextEditor txtRoundingValue;

	private UltraLabel lblRoundingValue;

	private UltraComboEditor cboDefaultClient;

	private UltraLabel lblDefaultClient;

	private UltraCheckEditor chkPrintCheck;

	private UltraCheckEditor chkPrintTaxInvoices;

	private UltraLabel lblPrintClosingCount;

	private UltraTextEditor txtPrintClosingCount;

	private UltraLabel lblPrintCheckCount;

	private UltraTextEditor txtPrintCheckCount;

	private UltraCheckEditor chkEnforceCaptainOrderSelection;

	private UltraCheckEditor chkNetPriceIsCashDefaultAmount;

	private UltraComboEditor cboSpecialOrderReport;

	private UltraLabel lblSpecialOrderReport;

	private RadioButton rbSpecialOrder;

	private UltraCheckEditor chkAdditionalDiscountWithoutTax;

	private UltraCheckEditor chkMaxCharge;

	private UltraLabel lblMaxChargeAmount;

	private UltraTextEditor txtMaxChargeAmount;

	private UltraCheckEditor chkDynamicMaxCharge;

	private UltraTabPageControl ultraTabPageControl5;

	public UltraGrid ULGMaxCharge;

	private UltraTabPageControl ultraTabPageControl6;

	public UltraGrid ULGDataRoomsOffers;

	private UltraLabel lblMaxChargeDiff;

	private UltraComboEditor cboMaxChargeDiff;

	private UltraCheckEditor chkPrintWithoutCheckNo;

	private UltraComboEditor cboPackingClassification;

	private UltraLabel lblClassification;

	private UltraComboEditor cboPickupPrinter;

	private UltraLabel lblPickupPrinter;

	private UltraCheckEditor chkShowFirstDiscount;

	private UltraCheckEditor chkAdditionalDiscountIncludeTax;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraCheckEditor chkWithoutSalesJV;

	public frmRooms()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_Rooms";
		IDCol = "RoomID";
		NoCol = "RoomCode";
		DateCol = "GetDate()";
	}

	public frmRooms(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			cboPickupPrinter.Items.Add((object)installedPrinter);
		}
		dtOffers = Offers.FillCombo("Null", "," + GlobalVariables.CurrentBranchID + ",", "-1", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlOffers.ValueListItems.Clear();
		for (int i = 0; i < dtOffers.Rows.Count; i++)
		{
			vlOffers.ValueListItems.Add(dtOffers.Rows[i]["OfferID"], dtOffers.Rows[i]["OfferName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtItems = Items.FillTreeWithItemType("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtServices = Items.FillCombo("-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMinChargeDiff, dtServices, "ItemID", "Name");
		GlobalFunctions.FillCombo(cboMaxChargeDiff, dtServices, "ItemID", "Name");
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "ItemID", "Name", "", "IsMain");
		}
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranches, dtBranchs, "BranchID", "BranchName");
		dtClients = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultClient, dtClients, "ClientID", "ClientName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtDineinReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmDineIn", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDineInReport, dtDineinReports, "ReportID", "ReportName");
		dtDeliveryReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmDelivery", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDeliveryReport, dtDeliveryReports, "ReportID", "ReportName");
		dtTakeAwayReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmTakeAway", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTakeAwayReport, dtTakeAwayReports, "ReportID", "ReportName");
		dtSpecialOrderReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmSpecialOrder", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSpecialOrderReport, dtSpecialOrderReports, "ReportID", "ReportName");
		dtClassification = ItemsFirstClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPackingClassification, dtClassification, "ItemFirstClassificationID", GlobalVariables.IsArabic ? "ItemFirstClassificationNameAr" : "ItemFirstClassificationNameEn");
		dtDetails = Tables.SelectByRoomID("0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtRoomsItems = RoomsItems.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtMinCharge = RoomsMinCharge.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtMaxCharge = RoomsMaxCharge.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtRoomsSetting = RoomsSettings.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtRoomsOffers = RoomsOffers.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGMinCharge).DataSource = dtMinCharge;
		((UltraGridBase)ULGMaxCharge).DataSource = dtMaxCharge;
		((UltraGridBase)ULGDataRoomsOffers).DataSource = dtRoomsOffers;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckCounter"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الطاولة" : "Table Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "فعالة" : "Active");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TableCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		GlobalFunctions.PrepareGrid(ULGDataRoomsOffers);
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Bands[0].Columns["RoomOfferID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Bands[0].Columns["OfferID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Bands[0].Columns["OfferID"].Header).Caption = (GlobalVariables.IsArabic ? "العرض" : "Offer");
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Bands[0].Columns["OfferID"].Width = ((Control)(object)ULGDataRoomsOffers).Width - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Bands[0].Columns["OfferID"].ValueList = (IValueList)(object)vlOffers;
		GlobalFunctions.PrepareGrid(ULGMinCharge);
		((UltraGridBase)ULGMinCharge).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["RoomMinChargeID"].DefaultCellValue = -1;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["MinChargeValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Hidden = !GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Hidden = GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].Header).Caption = (GlobalVariables.IsArabic ? "إيقاف من" : "Stop From");
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].Style = (ColumnStyle)30;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].MinValue = 0;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].MaxValue = 24;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].Header).Caption = (GlobalVariables.IsArabic ? "ايقاف الى" : "Stop To");
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].Style = (ColumnStyle)30;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].MinValue = 0;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].MaxValue = 24;
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["MinChargeValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["MinChargeValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["StopMinChargeTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGMinCharge).DisplayLayout.Bands[0].Columns["MinChargeValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		GlobalFunctions.PrepareGrid(ULGMaxCharge);
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["RoomMaxChargeID"].DefaultCellValue = -1;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["MaxChargeValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Hidden = !GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Hidden = GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].Header).Caption = (GlobalVariables.IsArabic ? "إيقاف من" : "Stop From");
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].Style = (ColumnStyle)30;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].MinValue = 0;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].MaxValue = 24;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].Header).Caption = (GlobalVariables.IsArabic ? "ايقاف الى" : "Stop To");
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].Style = (ColumnStyle)30;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].MinValue = 0;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].MaxValue = 24;
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["MaxChargeValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["MaxChargeValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["DayNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["StopMaxChargeTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGMaxCharge).DisplayLayout.Bands[0].Columns["MaxChargeValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Rooms.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6b: Expected O, but got Unknown
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Expected O, but got Unknown
		//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab5: Expected O, but got Unknown
		//IL_0ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acd: Expected O, but got Unknown
		if (drMaster != null)
		{
			((TextEditorControlBase)txtTableCount).ValueChanged -= txtTableCount_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["RoomCode"].ToString();
			((Control)(object)txtRoomArabicName).Text = drMaster["RoomNameAr"].ToString();
			((Control)(object)txtRoomEnglishName).Text = drMaster["RoomNameEn"].ToString();
			rbDineIn.Checked = bool.Parse(drMaster["IsDineIn"].ToString());
			rbDelivery.Checked = bool.Parse(drMaster["IsDelivery"].ToString());
			rbTakeAway.Checked = bool.Parse(drMaster["IsTakeAway"].ToString());
			rbSpecialOrder.Checked = bool.Parse(drMaster["IsSpecialOrder"].ToString());
			((Control)(object)txtTableCount).Text = drMaster["TablesCount"].ToString();
			((TextEditorControlBase)cboStore).Value = drMaster["StoreID"];
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboBranches).Value = drMaster["BranchID"];
			((UltraToggleEditorBase)chkMinCharge).Checked = bool.Parse(drMaster["HasMinCharge"].ToString());
			((UltraToggleEditorBase)chkWithoutSalesJV).CheckedChanged -= chkWithoutSalesJV_CheckedChanged;
			((UltraToggleEditorBase)chkWithoutSalesJV).Checked = bool.Parse(drMaster["WithoutSalesJV"].ToString());
			((UltraToggleEditorBase)chkWithoutSalesJV).CheckedChanged += chkWithoutSalesJV_CheckedChanged;
			((UltraToggleEditorBase)chkDynamicMinCharge).CheckedChanged -= chkDynamicMinCharge_CheckedChanged;
			((UltraToggleEditorBase)chkDynamicMinCharge).Checked = bool.Parse(drMaster["IsDynamicMinCharge"].ToString());
			((UltraTabControlBase)UTCDetails).Tabs["MinCharge"].Visible = ((UltraToggleEditorBase)chkDynamicMinCharge).Checked;
			((UltraToggleEditorBase)chkDynamicMinCharge).CheckedChanged += chkDynamicMinCharge_CheckedChanged;
			((TextEditorControlBase)txtMinChargeAmount).ValueChanged -= txtMinChargeAmount_ValueChanged;
			((Control)(object)txtMinChargeAmount).Text = drMaster["MinChargeValue"].ToString();
			((TextEditorControlBase)txtMinChargeAmount).ValueChanged += txtMinChargeAmount_ValueChanged;
			((TextEditorControlBase)cboMinChargeDiff).Value = drMaster["MinChargeItemID"];
			((TextEditorControlBase)cboMaxChargeDiff).Value = drMaster["MaxChargeItemID"];
			((UltraToggleEditorBase)chkMaxCharge).Checked = bool.Parse(drMaster["HasMaxCharge"].ToString());
			((UltraToggleEditorBase)chkDynamicMaxCharge).CheckedChanged -= chkDynamicMaxCharge_CheckedChanged;
			((UltraToggleEditorBase)chkDynamicMaxCharge).Checked = bool.Parse(drMaster["IsDynamicMaxCharge"].ToString());
			((UltraTabControlBase)UTCDetails).Tabs["MaxCharge"].Visible = ((UltraToggleEditorBase)chkDynamicMaxCharge).Checked;
			((UltraToggleEditorBase)chkDynamicMaxCharge).CheckedChanged += chkDynamicMaxCharge_CheckedChanged;
			((TextEditorControlBase)txtMaxChargeAmount).ValueChanged -= txtMaxChargeAmount_ValueChanged;
			((Control)(object)txtMaxChargeAmount).Text = drMaster["MaxChargeValue"].ToString();
			((TextEditorControlBase)txtMaxChargeAmount).ValueChanged += txtMaxChargeAmount_ValueChanged;
			((Control)(object)txtServiceChargePercent).Text = drMaster["ServiceChargePercent"].ToString();
			((Control)(object)txtRoundingValue).Text = drMaster["RoundingValue"].ToString();
			picRoom.Image = ((drMaster["RoomImage"] == DBNull.Value) ? null : GlobalFunctions.BinaryToImage((byte[])drMaster["RoomImage"]));
			dtRoomsSetting = RoomsSettings.SelectByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dtRoomsSetting.Rows.Count > 0)
			{
				((TextEditorControlBase)cboDefaultClient).Value = dtRoomsSetting.Rows[0]["DefaultClientID"];
				((TextEditorControlBase)cboDefaultSubAccount).Value = dtRoomsSetting.Rows[0]["DefaultSubAccountID"];
				((TextEditorControlBase)cboDineInReport).Value = dtRoomsSetting.Rows[0]["DineInReportID"];
				((TextEditorControlBase)cboDeliveryReport).Value = dtRoomsSetting.Rows[0]["DeliveryReportID"];
				((TextEditorControlBase)cboPickupPrinter).Value = dtRoomsSetting.Rows[0]["PickupPrinter"];
				((TextEditorControlBase)cboTakeAwayReport).Value = dtRoomsSetting.Rows[0]["TakeAwayReportID"];
				((TextEditorControlBase)cboCity).Value = dtRoomsSetting.Rows[0]["CityID"];
				((TextEditorControlBase)cboPackingClassification).Value = dtRoomsSetting.Rows[0]["PackingItemFirstClassificationID"];
				((TextEditorControlBase)cboSpecialOrderReport).Value = dtRoomsSetting.Rows[0]["SpecialOrderReportID"];
				((Control)(object)txtCheckAlert).Text = dtRoomsSetting.Rows[0]["CheckDelayAlert"].ToString();
				((Control)(object)txtPrintAlert).Text = dtRoomsSetting.Rows[0]["PrintDelayAlert"].ToString();
				((UltraToggleEditorBase)chkSentItemsMessage).Checked = bool.Parse(dtRoomsSetting.Rows[0]["SentItemsMessage"].ToString());
				((UltraToggleEditorBase)chkPrintWithoutCheckNo).Checked = bool.Parse(dtRoomsSetting.Rows[0]["PrintWithoutCheckNo"].ToString());
				((UltraToggleEditorBase)chkPrintCheck).Checked = bool.Parse(dtRoomsSetting.Rows[0]["PrintCheck"].ToString());
				((UltraToggleEditorBase)chkPrintTaxInvoices).Checked = bool.Parse(dtRoomsSetting.Rows[0]["PrintTaxInvoices"].ToString());
				((Control)(object)txtCompanyMessage).Text = dtRoomsSetting.Rows[0]["Message"].ToString();
				((Control)(object)txtPrintCheckCount).Text = dtRoomsSetting.Rows[0]["PrintCheckCount"].ToString();
				((Control)(object)txtPrintClosingCount).Text = dtRoomsSetting.Rows[0]["PrintClosingCheckCount"].ToString();
				((UltraToggleEditorBase)chkEnforceCaptainOrderSelection).Checked = bool.Parse(dtRoomsSetting.Rows[0]["EnforceCaptainOrderSelection"].ToString());
				((UltraToggleEditorBase)chkNetPriceIsCashDefaultAmount).Checked = bool.Parse(dtRoomsSetting.Rows[0]["NetPriceIsCashDefault"].ToString());
				((UltraToggleEditorBase)chkShowFirstDiscount).Checked = bool.Parse(dtRoomsSetting.Rows[0]["ShowFirstDiscount"].ToString());
				((UltraToggleEditorBase)chkAdditionalDiscountWithoutTax).Checked = bool.Parse(dtRoomsSetting.Rows[0]["AdditionalDiscountWithoutTax"].ToString());
				((UltraToggleEditorBase)chkAdditionalDiscountIncludeTax).Checked = bool.Parse(dtRoomsSetting.Rows[0]["AdditionalDiscountIncludeTax"].ToString());
				picbRoomLogo.Image = ((dtRoomsSetting.Rows[0]["Logo"] == DBNull.Value) ? null : GlobalFunctions.BinaryToImage((byte[])dtRoomsSetting.Rows[0]["Logo"]));
				picbRoomLogo.ScaleImage = (ScaleImage)1;
			}
			IsRoomLogoChanged = false;
			dtDetails = Tables.SelectByRoomID(drMaster["RoomID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtRoomsItems = RoomsItems.SelectByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtMinCharge = RoomsMinCharge.SelectByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtMaxCharge = RoomsMaxCharge.SelectByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtRoomsOffers = RoomsOffers.SelectByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
			SetCheckedItems(dtRoomsItems);
			TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGMinCharge).DataSource = dtMinCharge;
			((UltraGridBase)ULGMaxCharge).DataSource = dtMaxCharge;
			((UltraGridBase)ULGDataRoomsOffers).DataSource = dtRoomsOffers;
			InitGrid();
			((TextEditorControlBase)txtTableCount).ValueChanged += txtTableCount_ValueChanged;
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
		((Control)(object)btnPrint).Visible = false;
		((EditorButtonControlBase)txtRoomArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRoomEnglishName).ReadOnly = NavMode;
		rbDineIn.Enabled = !NavMode;
		rbDelivery.Enabled = !NavMode;
		rbTakeAway.Enabled = !NavMode;
		rbSpecialOrder.Enabled = !NavMode;
		((EditorButtonControlBase)txtTableCount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboStore).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPackingClassification).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranches).ReadOnly = NavMode || Updating;
		((Control)(object)chkMinCharge).Enabled = !NavMode;
		((Control)(object)chkWithoutSalesJV).Enabled = !NavMode && Adding;
		((EditorButtonControlBase)cboPickupPrinter).ReadOnly = NavMode;
		((Control)(object)chkDynamicMinCharge).Enabled = !NavMode && ((UltraToggleEditorBase)chkMinCharge).Checked;
		((Control)(object)txtMinChargeAmount).Enabled = !NavMode && ((UltraToggleEditorBase)chkMinCharge).Checked;
		((Control)(object)chkMaxCharge).Enabled = !NavMode;
		((Control)(object)chkDynamicMaxCharge).Enabled = !NavMode && ((UltraToggleEditorBase)chkMaxCharge).Checked;
		((Control)(object)txtMaxChargeAmount).Enabled = !NavMode && ((UltraToggleEditorBase)chkMaxCharge).Checked;
		((Control)(object)cboMinChargeDiff).Enabled = !NavMode && ((UltraToggleEditorBase)chkMinCharge).Checked;
		((Control)(object)cboMaxChargeDiff).Enabled = !NavMode && ((UltraToggleEditorBase)chkMaxCharge).Checked;
		((EditorButtonControlBase)txtServiceChargePercent).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRoundingValue).ReadOnly = NavMode;
		((Control)(object)btnPic).Visible = !NavMode;
		((Control)(object)btnDesignHall).Visible = NavMode;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
		((EditorButtonControlBase)cboDefaultClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultSubAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDineInReport).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDeliveryReport).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTakeAwayReport).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSpecialOrderReport).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCheckAlert).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPrintAlert).ReadOnly = NavMode;
		((Control)(object)chkSentItemsMessage).Enabled = !NavMode;
		((Control)(object)chkPrintWithoutCheckNo).Enabled = !NavMode;
		((Control)(object)chkPrintCheck).Enabled = !NavMode;
		((Control)(object)chkPrintTaxInvoices).Enabled = !NavMode;
		((Control)(object)txtPrintCheckCount).Enabled = !NavMode;
		((Control)(object)txtPrintClosingCount).Enabled = !NavMode;
		((Control)(object)chkEnforceCaptainOrderSelection).Enabled = !NavMode;
		((Control)(object)chkNetPriceIsCashDefaultAmount).Enabled = !NavMode;
		((Control)(object)chkNetPriceIsCashDefaultAmount).Visible = !Updating || drMaster == null || !Convert.ToBoolean(drMaster["WithoutSalesJV"]);
		((Control)(object)chkShowFirstDiscount).Enabled = !NavMode;
		((Control)(object)chkAdditionalDiscountWithoutTax).Enabled = !NavMode;
		((Control)(object)chkAdditionalDiscountIncludeTax).Enabled = !NavMode;
		((EditorButtonControlBase)txtCompanyMessage).ReadOnly = NavMode;
		((Control)(object)btnRoomLogoPath).Visible = !NavMode;
		string text = "";
		string text2 = "";
		string text3 = GlobalVariables.BranchIDs.Remove(GlobalVariables.BranchIDs.Length - 1).Remove(0, 1);
		if (((TextEditorControlBase)cboStore).Value != null)
		{
			text = ((TextEditorControlBase)cboStore).Value.ToString();
		}
		if (((TextEditorControlBase)cboBranches).Value != null)
		{
			text2 = ((TextEditorControlBase)cboBranches).Value.ToString();
		}
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " BranchID in ( " + text3 + ")";
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboStore, dt, "StoreID", "StoreName");
			DataView dataView2 = new DataView(dtBranchs);
			dataView2.RowFilter = " BranchID in ( " + text3 + ")";
			DataTable dt2 = dataView2.ToTable();
			GlobalFunctions.FillCombo(cboBranches, dt2, "BranchID", "BranchName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
			GlobalFunctions.FillCombo(cboBranches, dtBranchs, "BranchID", "BranchName");
		}
		if (text2 != "")
		{
			((TextEditorControlBase)cboBranches).Value = text2;
		}
		if (text != "")
		{
			((TextEditorControlBase)cboStore).Value = text;
		}
		((TextEditorControlBase)txtRoomArabicName).Focus();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataRoomsOffers).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtRoomArabicName).Clear();
		((TextEditorControlBase)txtRoomEnglishName).Clear();
		rbDineIn.Checked = true;
		rbDelivery.Checked = false;
		rbTakeAway.Checked = false;
		rbSpecialOrder.Checked = false;
		cboStore.SelectedIndex = -1;
		cboPriceType.SelectedIndex = -1;
		cboBranches.SelectedIndex = -1;
		cboPackingClassification.SelectedIndex = -1;
		cboPickupPrinter.SelectedIndex = -1;
		((UltraToggleEditorBase)chkMinCharge).Checked = false;
		((UltraToggleEditorBase)chkWithoutSalesJV).Checked = false;
		((UltraToggleEditorBase)chkDynamicMinCharge).CheckedChanged -= chkDynamicMinCharge_CheckedChanged;
		((UltraToggleEditorBase)chkDynamicMinCharge).Checked = false;
		((UltraTabControlBase)UTCDetails).Tabs["MinCharge"].Visible = false;
		((UltraToggleEditorBase)chkDynamicMinCharge).CheckedChanged += chkDynamicMinCharge_CheckedChanged;
		((TextEditorControlBase)txtMinChargeAmount).ValueChanged -= txtMinChargeAmount_ValueChanged;
		((Control)(object)txtMinChargeAmount).Text = "0";
		((TextEditorControlBase)txtMinChargeAmount).ValueChanged += txtMinChargeAmount_ValueChanged;
		cboMinChargeDiff.SelectedIndex = -1;
		cboMaxChargeDiff.SelectedIndex = -1;
		((UltraToggleEditorBase)chkMaxCharge).Checked = false;
		cboCity.SelectedIndex = -1;
		((UltraToggleEditorBase)chkDynamicMaxCharge).CheckedChanged -= chkDynamicMaxCharge_CheckedChanged;
		((UltraToggleEditorBase)chkDynamicMaxCharge).Checked = false;
		((UltraTabControlBase)UTCDetails).Tabs["MaxCharge"].Visible = false;
		((UltraToggleEditorBase)chkDynamicMaxCharge).CheckedChanged += chkDynamicMaxCharge_CheckedChanged;
		((TextEditorControlBase)txtMaxChargeAmount).ValueChanged -= txtMaxChargeAmount_ValueChanged;
		((Control)(object)txtMaxChargeAmount).Text = "0";
		((TextEditorControlBase)txtMaxChargeAmount).ValueChanged += txtMaxChargeAmount_ValueChanged;
		((Control)(object)txtServiceChargePercent).Text = "0";
		((Control)(object)txtRoundingValue).Text = "0";
		((Control)(object)txtTableCount).Text = "0";
		((Control)(object)txtCode).Text = (Adding ? Rooms.GetCode(IsFromServer: true) : "");
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
		((Control)(object)txtItems).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
		picRoom.Image = null;
		picbRoomLogo.Image = null;
		cboDefaultClient.SelectedIndex = -1;
		cboDefaultSubAccount.SelectedIndex = -1;
		cboDineInReport.SelectedIndex = -1;
		cboDeliveryReport.SelectedIndex = -1;
		cboTakeAwayReport.SelectedIndex = -1;
		cboSpecialOrderReport.SelectedIndex = -1;
		((Control)(object)txtCheckAlert).Text = "0";
		((Control)(object)txtPrintAlert).Text = "0";
		((UltraToggleEditorBase)chkSentItemsMessage).Checked = false;
		((UltraToggleEditorBase)chkPrintWithoutCheckNo).Checked = false;
		((UltraToggleEditorBase)chkPrintCheck).Checked = true;
		((UltraToggleEditorBase)chkPrintTaxInvoices).Checked = false;
		((Control)(object)txtPrintCheckCount).Text = "1";
		((Control)(object)txtPrintClosingCount).Text = "1";
		((UltraToggleEditorBase)chkEnforceCaptainOrderSelection).Checked = false;
		((UltraToggleEditorBase)chkNetPriceIsCashDefaultAmount).Checked = false;
		((UltraToggleEditorBase)chkShowFirstDiscount).Checked = false;
		((UltraToggleEditorBase)chkAdditionalDiscountWithoutTax).Checked = false;
		((UltraToggleEditorBase)chkAdditionalDiscountIncludeTax).Checked = false;
		((TextEditorControlBase)txtCompanyMessage).Clear();
		((DataTable)((UltraGridBase)ULGDataRoomsOffers).DataSource).Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الصالة", "Please Enter Room Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtRoomArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم الصالة بالعربية", "Please Enter Room Arabic Name");
			((TextEditorControlBase)txtRoomArabicName).Focus();
			return false;
		}
		if ((((Control)(object)txtTableCount).Text == "" || int.Parse(((Control)(object)txtTableCount).Text) <= 0) && rbDineIn.Checked)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الطاولات فى الصالة ", "Please Enter Tables Count In the Room");
			((TextEditorControlBase)txtTableCount).Focus();
			return false;
		}
		if (cboStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم المخزن ", "Please Select Store Name");
			((TextEditorControlBase)cboStore).Focus();
			cboStore.DropDown();
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار نوع السعر ", "Please Select Price type");
			((TextEditorControlBase)cboPriceType).Focus();
			cboPriceType.DropDown();
			return false;
		}
		if (cboBranches.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم الفرع" : "Please Select The Branch Name");
			((TextEditorControlBase)cboBranches).Focus();
			cboBranches.DropDown();
			return false;
		}
		if (int.Parse(((TextEditorControlBase)cboBranches).Value.ToString()) != int.Parse(dtStores.Select("StoreID = " + ((TextEditorControlBase)cboStore).Value.ToString())[0]["BranchID"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "فرع الصالة و فرع المخزن غير متطابقين برجاء التطابق" : "Store Branch and Room Branch are Not Equivalent.");
			((TextEditorControlBase)cboBranches).Focus();
			cboBranches.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkMinCharge).Checked && cboMinChargeDiff.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الخدمة للحد الادنى " : "Please Select Service Min Charge");
			((TextEditorControlBase)cboMinChargeDiff).Focus();
			cboMinChargeDiff.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkMaxCharge).Checked && cboMaxChargeDiff.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الخدمة للحد الثابت " : "Please Select Service Fixed Charge");
			((TextEditorControlBase)cboMaxChargeDiff).Focus();
			cboMaxChargeDiff.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkMinCharge).Checked && !((UltraToggleEditorBase)chkDynamicMinCharge).Checked && ((Control)(object)txtMinChargeAmount).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال المبلغ " : "Please Insert Amount");
			((TextEditorControlBase)txtMinChargeAmount).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkMaxCharge).Checked && !((UltraToggleEditorBase)chkDynamicMaxCharge).Checked && ((Control)(object)txtMaxChargeAmount).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال المبلغ " : "Please Insert Amount");
			((TextEditorControlBase)txtMaxChargeAmount).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkAdditionalDiscountIncludeTax).Checked && ((UltraToggleEditorBase)chkAdditionalDiscountWithoutTax).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء تحديد احد الاختيارين فقط خصم شامل الضريبة او غير شامل " : "Please Select whether Discount Include or without Tax ");
			((Control)(object)chkAdditionalDiscountIncludeTax).Focus();
			return false;
		}
		if (rbDelivery.Checked && cboCity.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المحافظه ", "Please Select City");
			((TextEditorControlBase)cboCity).Focus();
			cboCity.DropDown();
			return false;
		}
		if (cboDefaultSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب التحليلى الافتراضى" : "Please Select The Default SubAccount");
			((UltraTabControlBase)UTCDetails).Tabs[4].Selected = true;
			((TextEditorControlBase)cboDefaultSubAccount).Focus();
			cboDefaultSubAccount.DropDown();
			return false;
		}
		if (Main.CheckForValue("POS_Rooms", "RoomCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["RoomCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Rooms.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		DataTable dataTable = (DataTable)((UltraGridBase)ULGDataRoomsOffers).DataSource;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRoomsOffers).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["OfferID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم العرض  ", "Please Select Offer Name ");
				ULGDataRoomsOffers.ActiveCell = ((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["OfferID"];
				return false;
			}
			if (dataTable.Select("OfferID = " + ((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["OfferID"].Value.ToString()).Length > 1)
			{
				GlobalVariables.InformationMB.Show(" لا يمكن تكرار العرض  ", "Cannot Duplicate The Offer ");
				ULGDataRoomsOffers.ActiveCell = ((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["OfferID"];
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGData).Rows[j].Cells["TableCode"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال كود الطاولة" : "Please Enter Table Code");
				return false;
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Rooms.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtRoomArabicName).Text, (((Control)(object)txtRoomEnglishName).Text == "") ? "Null" : ((Control)(object)txtRoomEnglishName).Text, rbDineIn.Checked ? "1" : "0", rbDelivery.Checked ? "1" : "0", rbTakeAway.Checked ? "1" : "0", rbSpecialOrder.Checked ? "1" : "0", ((Control)(object)txtTableCount).Text, (cboStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboStore).Value.ToString() : "Null", (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((UltraToggleEditorBase)chkMinCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDynamicMinCharge).Checked ? "1" : "0", (((UltraToggleEditorBase)chkMinCharge).Checked && ((Control)(object)txtMinChargeAmount).Text != "") ? ((Control)(object)txtMinChargeAmount).Text : "0", (cboMinChargeDiff.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMinChargeDiff).Value.ToString(), ((UltraToggleEditorBase)chkMaxCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDynamicMaxCharge).Checked ? "1" : "0", (((UltraToggleEditorBase)chkMaxCharge).Checked && ((Control)(object)txtMaxChargeAmount).Text != "") ? ((Control)(object)txtMaxChargeAmount).Text : "0", (cboMaxChargeDiff.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaxChargeDiff).Value.ToString(), (((Control)(object)txtServiceChargePercent).Text == "") ? "0" : ((Control)(object)txtServiceChargePercent).Text, (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((UltraToggleEditorBase)chkWithoutSalesJV).Checked ? "1" : "0", "0", ((TextEditorControlBase)cboBranches).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsSettings.Insert_Update("-1", num.ToString(), ((Control)(object)txtCompanyMessage).Text, (((Control)(object)txtCheckAlert).Text != "") ? ((Control)(object)txtCheckAlert).Text : "0", (((Control)(object)txtPrintAlert).Text != "") ? ((Control)(object)txtPrintAlert).Text : "0", (cboDefaultClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultClient).Value.ToString() : "Null", (cboDefaultSubAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultSubAccount).Value.ToString() : "Null", (cboDineInReport.SelectedIndex > -1 && rbDineIn.Checked) ? ((TextEditorControlBase)cboDineInReport).Value.ToString() : "Null", (cboDeliveryReport.SelectedIndex > -1 && rbDelivery.Checked) ? ((TextEditorControlBase)cboDeliveryReport).Value.ToString() : "Null", (cboTakeAwayReport.SelectedIndex > -1 && rbTakeAway.Checked) ? ((TextEditorControlBase)cboTakeAwayReport).Value.ToString() : "Null", (cboSpecialOrderReport.SelectedIndex > -1 && rbSpecialOrder.Checked) ? ((TextEditorControlBase)cboSpecialOrderReport).Value.ToString() : "Null", ((UltraToggleEditorBase)chkSentItemsMessage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintWithoutCheckNo).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintCheck).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintTaxInvoices).Checked ? "1" : "0", (((Control)(object)txtPrintCheckCount).Text == "") ? "1" : ((Control)(object)txtPrintCheckCount).Text, (((Control)(object)txtPrintClosingCount).Text == "") ? "1" : ((Control)(object)txtPrintClosingCount).Text, ((UltraToggleEditorBase)chkEnforceCaptainOrderSelection).Checked ? "1" : "0", ((UltraToggleEditorBase)chkNetPriceIsCashDefaultAmount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkShowFirstDiscount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAdditionalDiscountWithoutTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAdditionalDiscountIncludeTax).Checked ? "1" : "0", (cboPackingClassification.SelectedIndex > -1) ? ((TextEditorControlBase)cboPackingClassification).Value.ToString() : "Null", (cboPickupPrinter.SelectedIndex > -1) ? ((TextEditorControlBase)cboPickupPrinter).Value.ToString() : "Null", (cboCity.SelectedIndex > -1) ? ((TextEditorControlBase)cboCity).Value.ToString() : "Null", "0", ((TextEditorControlBase)cboBranches).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRoomsOffers).Rows).Count; i++)
			{
				((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["RoomOfferID"].Value = -1;
				((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["RoomID"].Value = num;
				((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRoomsOffers).Rows).Count > 0)
			{
				RoomsOffers.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataRoomsOffers).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkDynamicMinCharge).Checked)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMinCharge).Rows).Count; j++)
				{
					((UltraGridBase)ULGMinCharge).Rows[j].Cells["RoomMinChargeID"].Value = -1;
					((UltraGridBase)ULGMinCharge).Rows[j].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGMinCharge).Rows[j].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
				}
				RoomsMinCharge.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGMinCharge).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkDynamicMaxCharge).Checked)
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMaxCharge).Rows).Count; k++)
				{
					((UltraGridBase)ULGMaxCharge).Rows[k].Cells["RoomMaxChargeID"].Value = -1;
					((UltraGridBase)ULGMaxCharge).Rows[k].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGMaxCharge).Rows[k].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
				}
				RoomsMaxCharge.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGMaxCharge).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (IsRoomImageChanged)
			{
				if (picRoom.Image != null)
				{
					Rooms.Image_Update(num, (Image)picRoom.Image, IsFromServer: true);
				}
				else
				{
					Main.SyncExecuteNonQuery("Update POS_Rooms Set RoomImage = null Where RoomID=" + num);
				}
			}
			if (IsRoomLogoChanged)
			{
				if (picbRoomLogo.Image != null)
				{
					RoomsSettings.RoomLogo_Update(num, (Image)picbRoomLogo.Image, IsFromServer: true);
				}
				else
				{
					Main.SyncExecuteNonQuery(" Update POS_RoomsSettings Set Logo = null Where RoomID=" + num);
				}
			}
			if (rbDineIn.Checked)
			{
				DesignHall();
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["TableID"].Value = -1;
					((UltraGridBase)ULGData).Rows[l].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				Tables.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			string text = InsertItems(num.ToString());
			if (text != "")
			{
				Main.SyncExecuteNonQuery(text);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Rooms.Insert_Update(drMaster["RoomID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtRoomArabicName).Text, (((Control)(object)txtRoomEnglishName).Text == "") ? "Null" : ((Control)(object)txtRoomEnglishName).Text, rbDineIn.Checked ? "1" : "0", rbDelivery.Checked ? "1" : "0", rbTakeAway.Checked ? "1" : "0", rbSpecialOrder.Checked ? "1" : "0", ((Control)(object)txtTableCount).Text, (cboStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboStore).Value.ToString() : "Null", (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((UltraToggleEditorBase)chkMinCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDynamicMinCharge).Checked ? "1" : "0", (((UltraToggleEditorBase)chkMinCharge).Checked && ((Control)(object)txtMinChargeAmount).Text != "") ? ((Control)(object)txtMinChargeAmount).Text : "0", (cboMinChargeDiff.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMinChargeDiff).Value.ToString(), ((UltraToggleEditorBase)chkMaxCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDynamicMaxCharge).Checked ? "1" : "0", (((UltraToggleEditorBase)chkMaxCharge).Checked && ((Control)(object)txtMaxChargeAmount).Text != "") ? ((Control)(object)txtMaxChargeAmount).Text : "0", (cboMaxChargeDiff.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaxChargeDiff).Value.ToString(), (((Control)(object)txtServiceChargePercent).Text == "") ? "0" : ((Control)(object)txtServiceChargePercent).Text, (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, Convert.ToBoolean(drMaster["WithoutSalesJV"]) ? "1" : "0", "0", ((TextEditorControlBase)cboBranches).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsSettings.Insert_Update("0", num.ToString(), ((Control)(object)txtCompanyMessage).Text, (((Control)(object)txtCheckAlert).Text != "") ? ((Control)(object)txtCheckAlert).Text : "0", (((Control)(object)txtPrintAlert).Text != "") ? ((Control)(object)txtPrintAlert).Text : "0", (cboDefaultClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultClient).Value.ToString() : "Null", (cboDefaultSubAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultSubAccount).Value.ToString() : "Null", (cboDineInReport.SelectedIndex > -1 && rbDineIn.Checked) ? ((TextEditorControlBase)cboDineInReport).Value.ToString() : "Null", (cboDeliveryReport.SelectedIndex > -1 && rbDelivery.Checked) ? ((TextEditorControlBase)cboDeliveryReport).Value.ToString() : "Null", (cboTakeAwayReport.SelectedIndex > -1 && rbTakeAway.Checked) ? ((TextEditorControlBase)cboTakeAwayReport).Value.ToString() : "Null", (cboSpecialOrderReport.SelectedIndex > -1 && rbSpecialOrder.Checked) ? ((TextEditorControlBase)cboSpecialOrderReport).Value.ToString() : "Null", ((UltraToggleEditorBase)chkSentItemsMessage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintWithoutCheckNo).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintCheck).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPrintTaxInvoices).Checked ? "1" : "0", (((Control)(object)txtPrintCheckCount).Text == "") ? "1" : ((Control)(object)txtPrintCheckCount).Text, (((Control)(object)txtPrintClosingCount).Text == "") ? "1" : ((Control)(object)txtPrintClosingCount).Text, ((UltraToggleEditorBase)chkEnforceCaptainOrderSelection).Checked ? "1" : "0", ((UltraToggleEditorBase)chkNetPriceIsCashDefaultAmount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkShowFirstDiscount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAdditionalDiscountWithoutTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAdditionalDiscountIncludeTax).Checked ? "1" : "0", (cboPackingClassification.SelectedIndex > -1) ? ((TextEditorControlBase)cboPackingClassification).Value.ToString() : "Null", (cboPickupPrinter.SelectedIndex > -1) ? ((TextEditorControlBase)cboPickupPrinter).Value.ToString() : "Null", (cboCity.SelectedIndex > -1) ? ((TextEditorControlBase)cboCity).Value.ToString() : "Null", "0", ((TextEditorControlBase)cboBranches).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRoomsOffers).Rows).Count; i++)
			{
				((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["RoomID"].Value = num;
				((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
				text = text + ((UltraGridBase)ULGDataRoomsOffers).Rows[i].Cells["RoomOfferID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("POS_RoomsOffers", "RoomID", drMaster["RoomID"].ToString(), "RoomOfferID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataRoomsOffers).Rows).Count > 0)
			{
				RoomsOffers.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataRoomsOffers).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkDynamicMinCharge).Checked)
			{
				string text2 = ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMinCharge).Rows).Count; j++)
				{
					((UltraGridBase)ULGMinCharge).Rows[j].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGMinCharge).Rows[j].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
					text2 = text2 + ((UltraGridBase)ULGMinCharge).Rows[j].Cells["RoomMinChargeID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("POS_RoomsMinCharge", "RoomID", drMaster["RoomID"].ToString(), "RoomMinChargeID", text2, IsFromServer: true);
				RoomsMinCharge.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGMinCharge).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkDynamicMaxCharge).Checked)
			{
				string text3 = ",";
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMaxCharge).Rows).Count; k++)
				{
					((UltraGridBase)ULGMaxCharge).Rows[k].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGMaxCharge).Rows[k].Cells["BranchID"].Value = ((TextEditorControlBase)cboBranches).Value.ToString();
					text3 = text3 + ((UltraGridBase)ULGMaxCharge).Rows[k].Cells["RoomMaxChargeID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("POS_RoomsMaxCharge", "RoomID", drMaster["RoomID"].ToString(), "RoomMaxChargeID", text3, IsFromServer: true);
				RoomsMaxCharge.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGMaxCharge).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (IsRoomImageChanged)
			{
				if (picRoom.Image != null)
				{
					Rooms.Image_Update(num, (Image)picRoom.Image, IsFromServer: true);
				}
				else
				{
					Main.SyncExecuteNonQuery("Update POS_Rooms Set RoomImage = null Where RoomID=" + num);
				}
			}
			if (IsRoomLogoChanged)
			{
				if (picbRoomLogo.Image != null)
				{
					RoomsSettings.RoomLogo_Update(num, (Image)picbRoomLogo.Image, IsFromServer: true);
				}
				else
				{
					Main.SyncExecuteNonQuery("Update POS_RoomsSettings Set Logo = null Where RoomID=" + num);
				}
			}
			if (rbDineIn.Checked)
			{
				if (int.Parse(((Control)(object)txtTableCount).Text) > int.Parse(drMaster["TablesCount"].ToString()))
				{
					DesignHall();
				}
				string text4 = ",";
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["RoomID"].Value = num;
					((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text4 = text4 + ((UltraGridBase)ULGData).Rows[l].Cells["TableID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("POS_Tables", "RoomID", drMaster["RoomID"].ToString(), "TableID", text4, IsFromServer: true);
				Tables.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			RoomsItems.DeleteByRoomID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			string text5 = InsertItems(num.ToString());
			if (text5 != "")
			{
				Main.SyncExecuteQuery_DataTable_Trans(text5);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public string InsertItems(string RoomID)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeItems.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)TreeItems.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)TreeItems.Nodes[i]).Tag) || TreeItems.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(TreeItems.Nodes[i], RoomID)) : (text + " EXEC POS_RoomsItems_Insert_Update -1," + RoomID + "," + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["TaxID"] == DBNull.Value) ? "null" : dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["TaxID"].ToString()) : ((dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["TaxID"] == DBNull.Value) ? "null" : dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["TaxID"].ToString())) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["StoreID"] == DBNull.Value) ? "null" : dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["StoreID"].ToString()) : ((cboStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboStore).Value.ToString() : "null")) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["POSPrinter"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["POSPrinter"].ToString() + "'")) : ((dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["POSPrinter"] == DBNull.Value) ? "Null" : ("'" + dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["POSPrinter"].ToString() + "'"))) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["PickupPrinter"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["PickupPrinter"].ToString() + "'")) : ((cboPickupPrinter.SelectedIndex == -1) ? "Null" : string.Concat("'", ((TextEditorControlBase)cboPickupPrinter).Value, "'"))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length == 0) ? "-1" : ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["CanModifyPrice"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["CanModifyPrice"].ToString() + "'"))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length == 0) ? "0" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["EnforceAccessories"].ToString() + "'")) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length == 0) ? 0m : ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["DiscountPercentage"] == DBNull.Value) ? 0m : decimal.Parse(dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["DiscountPercentage"].ToString()))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key).Length == 0) ? "0" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)TreeItems.Nodes[i]).Key)[0]["ExcludeCheckDiscount"].ToString() + "'")) + ",0," + GlobalVariables.CurrentBranchID + "," + GlobalVariables.UserID + "; "));
		}
		return text;
	}

	public string GetNodeCheckedChildsIDs(UltraTreeNode Node, string RoomID)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)Node.Nodes[i]).Tag) || Node.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(Node.Nodes[i], RoomID)) : (text + " EXEC POS_RoomsItems_Insert_Update -1," + RoomID + "," + ((KeyedSubObjectBase)Node.Nodes[i]).Key + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["TaxID"] == DBNull.Value) ? "null" : dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["TaxID"].ToString()) : ((dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["TaxID"] == DBNull.Value) ? "null" : dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["TaxID"].ToString())) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["StoreID"] == DBNull.Value) ? "null" : dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["StoreID"].ToString()) : ((cboStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboStore).Value.ToString() : "null")) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["POSPrinter"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["POSPrinter"].ToString() + "'")) : ((dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["POSPrinter"] == DBNull.Value) ? "Null" : ("'" + dtItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["POSPrinter"].ToString() + "'"))) + "," + ((!Adding && dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length != 0) ? ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["PickupPrinter"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["PickupPrinter"].ToString() + "'")) : ((cboPickupPrinter.SelectedIndex == -1) ? "Null" : string.Concat("'", ((TextEditorControlBase)cboPickupPrinter).Value, "'"))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length == 0) ? "-1" : ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["CanModifyPrice"] == DBNull.Value) ? "Null" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["CanModifyPrice"].ToString() + "'"))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length == 0) ? "0" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["EnforceAccessories"].ToString() + "'")) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length == 0) ? 0m : ((dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["DiscountPercentage"] == DBNull.Value) ? 0m : decimal.Parse(dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["DiscountPercentage"].ToString()))) + "," + ((Adding || dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key).Length == 0) ? "0" : ("'" + dtRoomsItems.Select(" ItemID= " + ((KeyedSubObjectBase)Node.Nodes[i]).Key)[0]["ExcludeCheckDiscount"].ToString() + "'")) + ",0," + GlobalVariables.CurrentBranchID + "," + GlobalVariables.UserID + "; "));
		}
		return text;
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (int.Parse(e.Rows[i].Cells["CheckCounter"].Value.ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لايمكن حذف هذه الطاولة لوجود شيكات مربوطة على هذه الطاولة", "You Cannot Delete This Table Because There Is Point Of Sales Check on this Table");
				return;
			}
			if (int.Parse(e.Rows[i].Cells["ReservationCounter"].Value.ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لايمكن حذف هذه الطاولة لوجود حجز على هذه الطاولة", "You Cannot Delete This Table Because There Is Reservations on this Table");
				return;
			}
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
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
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CheckCounter"].Value.ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لايمكن حذف هذه الصالة لوجود شيكات مربوطة على هذه الصالة", "You Cannot Delete This Room Because There Is Point Of Sales Check on this Room");
				return;
			}
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ReservationCounter"].Value.ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لايمكن حذف هذه الصالة لوجود حجز على هذه الصالة", "You Cannot Delete This Room Because There Is Reservations on this Room");
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			RoomsOffers.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsSettings.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsMaxCharge.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsMinCharge.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			RoomsItems.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Tables.DeleteByRoomID(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Rooms.Delete(drMaster["RoomID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحذف  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.RoomsReport(GlobalVariables.BranchIDs, IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["RoomID"].ToString();
			FillData();
		}
	}

	private void btnPic_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picRoom.Image = Image.FromFile(ofdPicture.FileName);
			IsRoomImageChanged = true;
		}
	}

	private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image == null)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه الصورة ؟", "Do you want to Clear this Image?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image = null;
			if (sender.Equals(picRoom))
			{
				IsRoomImageChanged = true;
			}
		}
	}

	private void btnRoomLogoPath_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picbRoomLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsRoomLogoChanged = true;
		}
	}

	private void picbRoomLogo_DoubleClick(object sender, EventArgs e)
	{
		if (((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image == null)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه الصورة ؟", "Do you want to Clear this Image?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image = null;
			if (sender.Equals(picbRoomLogo))
			{
				IsRoomLogoChanged = true;
			}
		}
	}

	private void txtTableCount_ValueChanged(object sender, EventArgs e)
	{
		if (!(((Control)(object)txtTableCount).Text != "") || int.Parse(((Control)(object)txtTableCount).Text) <= ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			int.TryParse(((UltraGridBase)ULGData).Rows[i].Cells["TableCode"].Value.ToString(), out result);
			if (result > num)
			{
				num = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TableCode"].Value.ToString());
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		int num2 = int.Parse(((Control)(object)txtTableCount).Text) - ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		for (int j = 0; j < num2; j++)
		{
			num++;
			DataRow dataRow = dtDetails.NewRow();
			dataRow["TableCode"] = num;
			dataRow["IsActive"] = true;
			dataRow["TableID"] = -1;
			if (Updating)
			{
				dataRow["x"] = 0;
				dataRow["Y"] = 0;
				dataRow["CheckCounter"] = 0;
				dataRow["ReservationCounter"] = 0;
			}
			dtDetails.Rows.Add(dataRow);
		}
	}

	private void txtTableCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void btnDesignHall_Click(object sender, EventArgs e)
	{
		if (drMaster != null)
		{
			frmRoomDesign frmRoomDesign2 = new frmRoomDesign(int.Parse(drMaster["RoomID"].ToString()));
			frmRoomDesign2.Tag = base.Tag;
			frmRoomDesign2.MdiParent = base.MdiParent;
			frmRoomDesign2.TopLevel = false;
			frmRoomDesign2.Parent = base.Parent;
			frmRoomDesign2.Width = base.Parent.Width;
			frmRoomDesign2.Height = base.Parent.Height;
			frmRoomDesign2.fRoom = this;
			frmRoomDesign2.Show();
			frmRoomDesign2.BringToFront();
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)txtTableCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
	}

	private void txtTableCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtTableCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
	}

	private void DesignHall()
	{
		double num = 1.0 / (Math.Round(3.0 * Math.Sqrt(double.Parse(((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString()) / 6.0)) + 1.0);
		double num2 = 1.0 / (Math.Round(2.0 * Math.Sqrt(double.Parse(((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString()) / 6.0)) + 1.0);
		double num3 = 0.0;
		double num4 = 0.0;
		int num5 = 1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if ((double)num5 < 3.0 * Math.Sqrt(double.Parse(((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString()) / 6.0))
			{
				((UltraGridBase)ULGData).Rows[i].Cells["x"].Value = num3;
				((UltraGridBase)ULGData).Rows[i].Cells["y"].Value = num4;
				num5++;
				num3 += num;
			}
			else
			{
				num4 += num2;
				num3 = 0.0;
				num5 = 1;
				((UltraGridBase)ULGData).Rows[i].Cells["x"].Value = num3;
				((UltraGridBase)ULGData).Rows[i].Cells["y"].Value = num4;
				num5++;
				num3 += num;
			}
		}
	}

	private void TreeItems_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
	}

	private void TreeItems_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		if (!Adding && !Updating)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void chkMaxChargeValue_CheckedChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((Control)(object)chkDynamicMaxCharge).Enabled = ((UltraToggleEditorBase)chkMaxCharge).Checked;
			((Control)(object)txtMaxChargeAmount).Enabled = ((UltraToggleEditorBase)chkMaxCharge).Checked;
			((Control)(object)cboMaxChargeDiff).Enabled = ((UltraToggleEditorBase)chkMaxCharge).Checked;
			if (!((UltraToggleEditorBase)chkMaxCharge).Checked)
			{
				((UltraToggleEditorBase)chkDynamicMaxCharge).Checked = false;
			}
		}
	}

	private void chkWithoutSalesJV_CheckedChanged(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkWithoutSalesJV).Checked)
		{
			((UltraToggleEditorBase)chkNetPriceIsCashDefaultAmount).Checked = false;
		}
		((Control)(object)chkNetPriceIsCashDefaultAmount).Visible = !((UltraToggleEditorBase)chkWithoutSalesJV).Checked;
	}

	private void chkDynamicMaxCharge_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)UTCDetails).Tabs["MaxCharge"].Visible = ((UltraToggleEditorBase)chkDynamicMaxCharge).Checked;
		if (((UltraToggleEditorBase)chkDynamicMaxCharge).Checked)
		{
			CreateMaxChargeWeek();
		}
	}

	private void txtMaxChargeAmount_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtMaxChargeAmount).Text != "")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMaxCharge).Rows).Count; i++)
			{
				((UltraGridBase)ULGMaxCharge).Rows[i].Cells["MaxChargeValue"].Value = ((Control)(object)txtMaxChargeAmount).Text;
			}
		}
	}

	private void txtMaxChargeAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeItems);
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
	}

	public void SetCheckBoxAllState(UltraTree tree, UltraCheckEditor CheckBox)
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			if (tree.Nodes[i].CheckedState == CheckState.Unchecked || tree.Nodes[i].CheckedState == CheckState.Indeterminate)
			{
				((UltraToggleEditorBase)CheckBox).Checked = false;
			}
			else
			{
				num++;
			}
		}
		if (num == ((DisposableObjectCollectionBase)tree.Nodes).Count)
		{
			((UltraToggleEditorBase)CheckBox).Checked = true;
		}
	}

	public void SetCheckedItems(DataTable dtRoomItem)
	{
		for (int i = 0; i < dtRoomItem.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeItems.GetNodeByKey(dtRoomsItems.Rows[i]["ItemID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			for (int j = 0; j < ((DisposableObjectCollectionBase)nodeByKey.Nodes).Count; j++)
			{
				TreeFunctions.SetAllNodeChildsCheckState(nodeByKey.CheckedState, nodeByKey);
			}
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = "Name Like '%" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0]["ItemID"].ToString());
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i]["ItemID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblTableCount).Visible = rbDineIn.Checked;
		((Control)(object)txtTableCount).Visible = rbDineIn.Checked;
		((Control)(object)btnDesignHall).Visible = rbDineIn.Checked;
		((UltraTabControlBase)UTCDetails).Tabs[0].Visible = rbDineIn.Checked;
		UltraLabel obj = lblDineInReport;
		bool visible = (((Control)(object)cboDineInReport).Visible = rbDineIn.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblDeliveryReport;
		visible = (((Control)(object)cboDeliveryReport).Visible = rbDelivery.Checked);
		((Control)(object)obj2).Visible = visible;
		UltraLabel obj3 = lblTakeAwayReport;
		visible = (((Control)(object)cboTakeAwayReport).Visible = rbTakeAway.Checked);
		((Control)(object)obj3).Visible = visible;
		UltraLabel obj4 = lblSpecialOrderReport;
		visible = (((Control)(object)cboSpecialOrderReport).Visible = rbSpecialOrder.Checked);
		((Control)(object)obj4).Visible = visible;
		UltraLabel obj5 = lblPrintAlert1;
		UltraLabel obj6 = lblPrintAlert2;
		UltraLabel obj7 = lblCheckAlert1;
		UltraLabel obj8 = lblCheckAlert2;
		UltraTextEditor obj9 = txtCheckAlert;
		bool flag5 = (((Control)(object)txtPrintAlert).Visible = rbDineIn.Checked);
		bool flag7 = (((Control)(object)obj9).Visible = flag5);
		bool flag9 = (((Control)(object)obj8).Visible = flag7);
		bool flag11 = (((Control)(object)obj7).Visible = flag9);
		visible = (((Control)(object)obj6).Visible = flag11);
		((Control)(object)obj5).Visible = visible;
		((Control)(object)chkEnforceCaptainOrderSelection).Visible = rbDineIn.Checked || rbTakeAway.Checked;
		UltraComboEditor obj10 = cboCity;
		visible = (((Control)(object)lblCity).Visible = rbDelivery.Checked);
		((Control)(object)obj10).Visible = visible;
	}

	private void chkMinCharge_CheckedChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((Control)(object)chkDynamicMinCharge).Enabled = ((UltraToggleEditorBase)chkMinCharge).Checked;
			((Control)(object)txtMinChargeAmount).Enabled = ((UltraToggleEditorBase)chkMinCharge).Checked;
			((Control)(object)cboMinChargeDiff).Enabled = ((UltraToggleEditorBase)chkMinCharge).Checked;
			if (!((UltraToggleEditorBase)chkMinCharge).Checked)
			{
				((UltraToggleEditorBase)chkDynamicMinCharge).Checked = false;
			}
		}
	}

	private void chkDynamicMinCharge_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)UTCDetails).Tabs["MinCharge"].Visible = ((UltraToggleEditorBase)chkDynamicMinCharge).Checked;
		if (((UltraToggleEditorBase)chkDynamicMinCharge).Checked)
		{
			CreateMinChargeWeek();
		}
	}

	public void CreateMinChargeWeek()
	{
		dtMinCharge.Rows.Clear();
		for (int i = 0; i < arWeekDays.GetLength(0); i++)
		{
			DataRow dataRow = dtMinCharge.NewRow();
			dataRow["RoomMinChargeID"] = -1;
			dataRow["DayNameEn"] = arWeekDays[i, 1];
			dataRow["DayNameAr"] = arWeekDays[i, 0];
			dataRow["StopMinChargeFrom"] = 0;
			dataRow["StopMinChargeTo"] = 0;
			dataRow["MinChargeValue"] = ((((Control)(object)txtMinChargeAmount).Text == "") ? "0" : ((Control)(object)txtMinChargeAmount).Text);
			dtMinCharge.Rows.Add(dataRow);
		}
		((UltraGridBase)ULGMinCharge).DataSource = dtMinCharge;
		InitGrid();
	}

	public void CreateMaxChargeWeek()
	{
		dtMaxCharge.Rows.Clear();
		for (int i = 0; i < arWeekDays.GetLength(0); i++)
		{
			DataRow dataRow = dtMaxCharge.NewRow();
			dataRow["RoomMaxChargeID"] = -1;
			dataRow["DayNameEn"] = arWeekDays[i, 1];
			dataRow["DayNameAr"] = arWeekDays[i, 0];
			dataRow["StopMaxChargeFrom"] = 0;
			dataRow["StopMaxChargeTo"] = 0;
			dataRow["MaxChargeValue"] = ((((Control)(object)txtMaxChargeAmount).Text == "") ? "0" : ((Control)(object)txtMaxChargeAmount).Text);
			dtMaxCharge.Rows.Add(dataRow);
		}
		((UltraGridBase)ULGMaxCharge).DataSource = dtMaxCharge;
		InitGrid();
	}

	private void txtMinChargeAmount_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtMinChargeAmount).Text != "")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMinCharge).Rows).Count; i++)
			{
				((UltraGridBase)ULGMinCharge).Rows[i].Cells["MinChargeValue"].Value = ((Control)(object)txtMinChargeAmount).Text;
			}
		}
	}

	private void txtMinChargeAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtCheckAlert_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtPrintAlert_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	public void ULGDataRoomsOffers_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		((UltraToggleEditorBase)chkWithoutSalesJV).Checked = false;
		SetControls(NavMode: false);
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
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Expected O, but got Unknown
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Expected O, but got Unknown
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Expected O, but got Unknown
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Expected O, but got Unknown
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Expected O, but got Unknown
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Expected O, but got Unknown
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Expected O, but got Unknown
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Expected O, but got Unknown
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Expected O, but got Unknown
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Expected O, but got Unknown
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Expected O, but got Unknown
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Expected O, but got Unknown
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected O, but got Unknown
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Expected O, but got Unknown
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Expected O, but got Unknown
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected O, but got Unknown
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Expected O, but got Unknown
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Expected O, but got Unknown
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected O, but got Unknown
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Expected O, but got Unknown
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Expected O, but got Unknown
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Expected O, but got Unknown
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Expected O, but got Unknown
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Expected O, but got Unknown
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected O, but got Unknown
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Expected O, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Expected O, but got Unknown
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Expected O, but got Unknown
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Expected O, but got Unknown
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Expected O, but got Unknown
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Expected O, but got Unknown
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Expected O, but got Unknown
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Expected O, but got Unknown
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Expected O, but got Unknown
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Expected O, but got Unknown
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Expected O, but got Unknown
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Expected O, but got Unknown
		//IL_1255: Unknown result type (might be due to invalid IL or missing references)
		//IL_125f: Expected O, but got Unknown
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1277: Expected O, but got Unknown
		//IL_2d6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d79: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmRooms));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
		UltraTab val4 = new UltraTab();
		UltraTab val5 = new UltraTab();
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
		Override val16 = new Override();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		Appearance val57 = new Appearance();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.TreeItems = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGMinCharge = new UltraGrid();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.ULGMaxCharge = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.cboPickupPrinter = new UltraComboEditor();
		this.lblPickupPrinter = new UltraLabel();
		this.cboPackingClassification = new UltraComboEditor();
		this.lblClassification = new UltraLabel();
		this.cboSpecialOrderReport = new UltraComboEditor();
		this.lblSpecialOrderReport = new UltraLabel();
		this.chkShowFirstDiscount = new UltraCheckEditor();
		this.chkAdditionalDiscountIncludeTax = new UltraCheckEditor();
		this.chkAdditionalDiscountWithoutTax = new UltraCheckEditor();
		this.chkNetPriceIsCashDefaultAmount = new UltraCheckEditor();
		this.chkEnforceCaptainOrderSelection = new UltraCheckEditor();
		this.lblPrintClosingCount = new UltraLabel();
		this.txtPrintClosingCount = new UltraTextEditor();
		this.lblPrintCheckCount = new UltraLabel();
		this.txtPrintCheckCount = new UltraTextEditor();
		this.chkPrintCheck = new UltraCheckEditor();
		this.chkPrintTaxInvoices = new UltraCheckEditor();
		this.cboDefaultClient = new UltraComboEditor();
		this.lblDefaultClient = new UltraLabel();
		this.chkPrintWithoutCheckNo = new UltraCheckEditor();
		this.chkSentItemsMessage = new UltraCheckEditor();
		this.lblPrintAlert2 = new UltraLabel();
		this.lblPrintAlert1 = new UltraLabel();
		this.lblCheckAlert2 = new UltraLabel();
		this.txtPrintAlert = new UltraTextEditor();
		this.lblCheckAlert1 = new UltraLabel();
		this.txtCheckAlert = new UltraTextEditor();
		this.cboTakeAwayReport = new UltraComboEditor();
		this.lblTakeAwayReport = new UltraLabel();
		this.cboDeliveryReport = new UltraComboEditor();
		this.lblDeliveryReport = new UltraLabel();
		this.cboDineInReport = new UltraComboEditor();
		this.lblDineInReport = new UltraLabel();
		this.cboDefaultSubAccount = new UltraComboEditor();
		this.lblDefaultSubAccount = new UltraLabel();
		this.lblCompanyMessage = new UltraLabel();
		this.txtCompanyMessage = new UltraTextEditor();
		this.lblRoomLogo = new UltraLabel();
		this.picbRoomLogo = new UltraPictureBox();
		this.btnRoomLogoPath = new UltraButton();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.ULGDataRoomsOffers = new UltraGrid();
		this.pnlCheckType = new UltraPanel();
		this.rbSpecialOrder = new System.Windows.Forms.RadioButton();
		this.rbDineIn = new System.Windows.Forms.RadioButton();
		this.rbTakeAway = new System.Windows.Forms.RadioButton();
		this.rbDelivery = new System.Windows.Forms.RadioButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.cboStore = new UltraComboEditor();
		this.lblStoreName = new UltraLabel();
		this.btnDesignHall = new UltraButton();
		this.btnPic = new UltraButton();
		this.lblPic = new UltraLabel();
		this.picRoom = new UltraPictureBox();
		this.txtTableCount = new UltraTextEditor();
		this.lblTableCount = new UltraLabel();
		this.txtRoomEnglishName = new UltraTextEditor();
		this.lblRoomEnglishName = new UltraLabel();
		this.txtRoomArabicName = new UltraTextEditor();
		this.lblRoomArabicName = new UltraLabel();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.lblBranch = new UltraLabel();
		this.cboBranches = new UltraComboEditor();
		this.chkMinCharge = new UltraCheckEditor();
		this.chkDynamicMinCharge = new UltraCheckEditor();
		this.txtMinChargeAmount = new UltraTextEditor();
		this.lblMinChargeAmount = new UltraLabel();
		this.txtServiceChargePercent = new UltraTextEditor();
		this.lblServiceCharge = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboMinChargeDiff = new UltraComboEditor();
		this.lblMinChargeDiff = new UltraLabel();
		this.txtRoundingValue = new UltraTextEditor();
		this.lblRoundingValue = new UltraLabel();
		this.chkMaxCharge = new UltraCheckEditor();
		this.lblMaxChargeAmount = new UltraLabel();
		this.txtMaxChargeAmount = new UltraTextEditor();
		this.chkDynamicMaxCharge = new UltraCheckEditor();
		this.lblMaxChargeDiff = new UltraLabel();
		this.cboMaxChargeDiff = new UltraComboEditor();
		this.chkWithoutSalesJV = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMinCharge).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMaxCharge).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPickupPrinter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPackingClassification).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecialOrderReport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkShowFirstDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAdditionalDiscountIncludeTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAdditionalDiscountWithoutTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkNetPriceIsCashDefaultAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceCaptainOrderSelection).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintClosingCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintCheckCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintCheck).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintTaxInvoices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintWithoutCheckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentItemsMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintAlert).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckAlert).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTakeAwayReport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryReport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDineInReport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataRoomsOffers).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMinCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDynamicMinCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinChargeAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargePercent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMinChargeDiff).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaxCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxChargeAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDynamicMaxCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaxChargeDiff).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutSalesJV).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "MinCharge";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "MaxCharge";
		val3.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val3, "ultraTab4");
		((SubObjectBase)val3).ForceApplyResources = "";
		((KeyedSubObjectBase)val4).Key = "Settings";
		val4.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val4, "ultraTab3");
		((SubObjectBase)val4).ForceApplyResources = "";
		((KeyedSubObjectBase)val5).Key = "RoomsOffers";
		val5.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val5, "ultraTab5");
		((SubObjectBase)val5).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[5] { val, val2, val3, val4, val5 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl6, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl5, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val6, "appearance45");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val7, "appearance46");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance47");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance48");
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance49");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance50");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val12, "appearance51");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val13).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val13).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val13).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val13).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val13).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance52");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val13;
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
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance55");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance2");
		this.TreeItems.Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val16.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val16;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		this.TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance56");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGMinCharge);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGMinCharge, "ULGMinCharge");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val18).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val18).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance4");
		((SpecialBoxBase)((UltraGridBase)this.ULGMinCharge).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val19, "appearance5");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val19;
		((SpecialBoxBase)((UltraGridBase)this.ULGMinCharge).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val20).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val20).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val20, "appearance6");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val21).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val21, "appearance7");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val21;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val22, "appearance8");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val22;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val23, "appearance9");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val23;
		((AppearanceBase)val24).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val24, "appearance10");
		((AppearanceBase)val24).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val25).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val25).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val25).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val25).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val25, "appearance11");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val26).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val26, "appearance12");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val27, "appearance13");
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGMinCharge).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGMinCharge).Name = "ULGMinCharge";
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGMaxCharge);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.ULGMaxCharge, "ULGMaxCharge");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance25");
		((SpecialBoxBase)((UltraGridBase)this.ULGMaxCharge).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val28;
		((AppearanceBase)val29).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val29, "appearance26");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val29;
		((SpecialBoxBase)((UltraGridBase)this.ULGMaxCharge).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val30).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val30).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val30).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val30, "appearance27");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val31).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val31, "appearance28");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val32, "appearance29");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val33, "appearance30");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val33;
		((AppearanceBase)val34).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val34, "appearance31");
		((AppearanceBase)val34).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val35).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val35).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val35).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val35).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val35, "appearance32");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val36).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val36, "appearance33");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val37, "appearance34");
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGMaxCharge).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGMaxCharge).Name = "ULGMaxCharge";
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboPickupPrinter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPickupPrinter);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboPackingClassification);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboSpecialOrderReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblSpecialOrderReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkShowFirstDiscount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkAdditionalDiscountIncludeTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkAdditionalDiscountWithoutTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkNetPriceIsCashDefaultAmount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceCaptainOrderSelection);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintClosingCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintClosingCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintCheckCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintCheckCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkPrintCheck);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkPrintTaxInvoices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultClient);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultClient);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkPrintWithoutCheckNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkSentItemsMessage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintAlert2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintAlert1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckAlert2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintAlert);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckAlert1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtCheckAlert);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboTakeAwayReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblTakeAwayReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboDeliveryReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboDineInReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDineInReport);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultSubAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultSubAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyMessage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyMessage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomLogo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.picbRoomLogo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.btnRoomLogoPath);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance57");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val38;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.cboPickupPrinter, "cboPickupPrinter");
		this.cboPickupPrinter.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPickupPrinter).Name = "cboPickupPrinter";
		resources.ApplyResources(this.lblPickupPrinter, "lblPickupPrinter");
		this.lblPickupPrinter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPickupPrinter).Name = "lblPickupPrinter";
		((ControlBase)this.lblPickupPrinter).WrapText = false;
		resources.ApplyResources(this.cboPackingClassification, "cboPackingClassification");
		this.cboPackingClassification.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPackingClassification).Name = "cboPackingClassification";
		resources.ApplyResources(this.lblClassification, "lblClassification");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance58");
		((ControlBase)this.lblClassification).Appearance = (AppearanceBase)(object)val39;
		this.lblClassification.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification).Name = "lblClassification";
		((ControlBase)this.lblClassification).WrapText = false;
		resources.ApplyResources(this.cboSpecialOrderReport, "cboSpecialOrderReport");
		this.cboSpecialOrderReport.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSpecialOrderReport).Name = "cboSpecialOrderReport";
		resources.ApplyResources(this.lblSpecialOrderReport, "lblSpecialOrderReport");
		this.lblSpecialOrderReport.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSpecialOrderReport).Name = "lblSpecialOrderReport";
		((ControlBase)this.lblSpecialOrderReport).WrapText = false;
		resources.ApplyResources(this.chkShowFirstDiscount, "chkShowFirstDiscount");
		((System.Windows.Forms.Control)(object)this.chkShowFirstDiscount).Name = "chkShowFirstDiscount";
		resources.ApplyResources(this.chkAdditionalDiscountIncludeTax, "chkAdditionalDiscountIncludeTax");
		((System.Windows.Forms.Control)(object)this.chkAdditionalDiscountIncludeTax).Name = "chkAdditionalDiscountIncludeTax";
		resources.ApplyResources(this.chkAdditionalDiscountWithoutTax, "chkAdditionalDiscountWithoutTax");
		((System.Windows.Forms.Control)(object)this.chkAdditionalDiscountWithoutTax).Name = "chkAdditionalDiscountWithoutTax";
		resources.ApplyResources(this.chkNetPriceIsCashDefaultAmount, "chkNetPriceIsCashDefaultAmount");
		((System.Windows.Forms.Control)(object)this.chkNetPriceIsCashDefaultAmount).Name = "chkNetPriceIsCashDefaultAmount";
		resources.ApplyResources(this.chkEnforceCaptainOrderSelection, "chkEnforceCaptainOrderSelection");
		((System.Windows.Forms.Control)(object)this.chkEnforceCaptainOrderSelection).Name = "chkEnforceCaptainOrderSelection";
		resources.ApplyResources(this.lblPrintClosingCount, "lblPrintClosingCount");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val40, "appearance59");
		((ControlBase)this.lblPrintClosingCount).Appearance = (AppearanceBase)(object)val40;
		this.lblPrintClosingCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintClosingCount).Name = "lblPrintClosingCount";
		((ControlBase)this.lblPrintClosingCount).WrapText = false;
		resources.ApplyResources(this.txtPrintClosingCount, "txtPrintClosingCount");
		((System.Windows.Forms.Control)(object)this.txtPrintClosingCount).Name = "txtPrintClosingCount";
		resources.ApplyResources(this.lblPrintCheckCount, "lblPrintCheckCount");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val41, "appearance60");
		((ControlBase)this.lblPrintCheckCount).Appearance = (AppearanceBase)(object)val41;
		this.lblPrintCheckCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintCheckCount).Name = "lblPrintCheckCount";
		((ControlBase)this.lblPrintCheckCount).WrapText = false;
		resources.ApplyResources(this.txtPrintCheckCount, "txtPrintCheckCount");
		((System.Windows.Forms.Control)(object)this.txtPrintCheckCount).Name = "txtPrintCheckCount";
		resources.ApplyResources(this.chkPrintCheck, "chkPrintCheck");
		((System.Windows.Forms.Control)(object)this.chkPrintCheck).Name = "chkPrintCheck";
		resources.ApplyResources(this.chkPrintTaxInvoices, "chkPrintTaxInvoices");
		((System.Windows.Forms.Control)(object)this.chkPrintTaxInvoices).Name = "chkPrintTaxInvoices";
		resources.ApplyResources(this.cboDefaultClient, "cboDefaultClient");
		this.cboDefaultClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultClient).Name = "cboDefaultClient";
		resources.ApplyResources(this.lblDefaultClient, "lblDefaultClient");
		this.lblDefaultClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultClient).Name = "lblDefaultClient";
		((ControlBase)this.lblDefaultClient).WrapText = false;
		resources.ApplyResources(this.chkPrintWithoutCheckNo, "chkPrintWithoutCheckNo");
		((System.Windows.Forms.Control)(object)this.chkPrintWithoutCheckNo).Name = "chkPrintWithoutCheckNo";
		resources.ApplyResources(this.chkSentItemsMessage, "chkSentItemsMessage");
		((System.Windows.Forms.Control)(object)this.chkSentItemsMessage).Name = "chkSentItemsMessage";
		resources.ApplyResources(this.lblPrintAlert2, "lblPrintAlert2");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val42, "appearance61");
		((ControlBase)this.lblPrintAlert2).Appearance = (AppearanceBase)(object)val42;
		this.lblPrintAlert2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintAlert2).Name = "lblPrintAlert2";
		((ControlBase)this.lblPrintAlert2).WrapText = false;
		resources.ApplyResources(this.lblPrintAlert1, "lblPrintAlert1");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val43, "appearance62");
		((ControlBase)this.lblPrintAlert1).Appearance = (AppearanceBase)(object)val43;
		this.lblPrintAlert1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintAlert1).Name = "lblPrintAlert1";
		((ControlBase)this.lblPrintAlert1).WrapText = false;
		resources.ApplyResources(this.lblCheckAlert2, "lblCheckAlert2");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val44, "appearance63");
		((ControlBase)this.lblCheckAlert2).Appearance = (AppearanceBase)(object)val44;
		this.lblCheckAlert2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckAlert2).Name = "lblCheckAlert2";
		((ControlBase)this.lblCheckAlert2).WrapText = false;
		resources.ApplyResources(this.txtPrintAlert, "txtPrintAlert");
		((System.Windows.Forms.Control)(object)this.txtPrintAlert).Name = "txtPrintAlert";
		((System.Windows.Forms.Control)(object)this.txtPrintAlert).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrintAlert_KeyPress);
		resources.ApplyResources(this.lblCheckAlert1, "lblCheckAlert1");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val45, "appearance64");
		((ControlBase)this.lblCheckAlert1).Appearance = (AppearanceBase)(object)val45;
		this.lblCheckAlert1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckAlert1).Name = "lblCheckAlert1";
		((ControlBase)this.lblCheckAlert1).WrapText = false;
		resources.ApplyResources(this.txtCheckAlert, "txtCheckAlert");
		((System.Windows.Forms.Control)(object)this.txtCheckAlert).Name = "txtCheckAlert";
		((System.Windows.Forms.Control)(object)this.txtCheckAlert).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCheckAlert_KeyPress);
		resources.ApplyResources(this.cboTakeAwayReport, "cboTakeAwayReport");
		this.cboTakeAwayReport.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTakeAwayReport).Name = "cboTakeAwayReport";
		resources.ApplyResources(this.lblTakeAwayReport, "lblTakeAwayReport");
		((System.Windows.Forms.Control)(object)this.lblTakeAwayReport).Name = "lblTakeAwayReport";
		resources.ApplyResources(this.cboDeliveryReport, "cboDeliveryReport");
		this.cboDeliveryReport.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDeliveryReport).Name = "cboDeliveryReport";
		resources.ApplyResources(this.lblDeliveryReport, "lblDeliveryReport");
		((System.Windows.Forms.Control)(object)this.lblDeliveryReport).Name = "lblDeliveryReport";
		resources.ApplyResources(this.cboDineInReport, "cboDineInReport");
		this.cboDineInReport.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDineInReport).Name = "cboDineInReport";
		resources.ApplyResources(this.lblDineInReport, "lblDineInReport");
		((System.Windows.Forms.Control)(object)this.lblDineInReport).Name = "lblDineInReport";
		resources.ApplyResources(this.cboDefaultSubAccount, "cboDefaultSubAccount");
		this.cboDefaultSubAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultSubAccount).Name = "cboDefaultSubAccount";
		resources.ApplyResources(this.lblDefaultSubAccount, "lblDefaultSubAccount");
		this.lblDefaultSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultSubAccount).Name = "lblDefaultSubAccount";
		((ControlBase)this.lblDefaultSubAccount).WrapText = false;
		resources.ApplyResources(this.lblCompanyMessage, "lblCompanyMessage");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val46, "appearance65");
		((ControlBase)this.lblCompanyMessage).Appearance = (AppearanceBase)(object)val46;
		this.lblCompanyMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyMessage).Name = "lblCompanyMessage";
		((ControlBase)this.lblCompanyMessage).WrapText = false;
		resources.ApplyResources(this.txtCompanyMessage, "txtCompanyMessage");
		((System.Windows.Forms.Control)(object)this.txtCompanyMessage).Name = "txtCompanyMessage";
		resources.ApplyResources(this.lblRoomLogo, "lblRoomLogo");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val47, "appearance66");
		((ControlBase)this.lblRoomLogo).Appearance = (AppearanceBase)(object)val47;
		this.lblRoomLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomLogo).Name = "lblRoomLogo";
		((ControlBase)this.lblRoomLogo).WrapText = false;
		resources.ApplyResources(this.picbRoomLogo, "picbRoomLogo");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val48, "appearance67");
		this.picbRoomLogo.Appearance = (AppearanceBase)(object)val48;
		this.picbRoomLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.picbRoomLogo.BorderStyle = (UIElementBorderStyle)2;
		this.picbRoomLogo.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.picbRoomLogo).Name = "picbRoomLogo";
		((System.Windows.Forms.Control)(object)this.picbRoomLogo).DoubleClick += new System.EventHandler(picbRoomLogo_DoubleClick);
		resources.ApplyResources(this.btnRoomLogoPath, "btnRoomLogoPath");
		((System.Windows.Forms.Control)(object)this.btnRoomLogoPath).Name = "btnRoomLogoPath";
		((System.Windows.Forms.Control)(object)this.btnRoomLogoPath).Click += new System.EventHandler(btnRoomLogoPath_Click);
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataRoomsOffers);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.ULGDataRoomsOffers, "ULGDataRoomsOffers");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val49).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val49).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val49).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val49, "appearance35");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val49;
		((AppearanceBase)val50).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val50, "appearance36");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val50;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val51).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val51).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val51).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val51, "appearance37");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val51;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val52).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val52).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val52, "appearance38");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val52;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val53).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val53, "appearance39");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val54, "appearance40");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val54;
		((AppearanceBase)val55).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val55, "appearance41");
		((AppearanceBase)val55).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val56).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val56).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val56).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val56).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val56, "appearance42");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val56;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val57).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val57).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val57, "appearance43");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val57;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val58, "appearance44");
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataRoomsOffers).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataRoomsOffers).Name = "ULGDataRoomsOffers";
		this.ULGDataRoomsOffers.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataRoomsOffers_BeforeRowsDeleted);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val59).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val59, "appearance68");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val59;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbSpecialOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbDineIn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbTakeAway);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbDelivery);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbSpecialOrder, "rbSpecialOrder");
		this.rbSpecialOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbSpecialOrder.Name = "rbSpecialOrder";
		this.rbSpecialOrder.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbDineIn, "rbDineIn");
		this.rbDineIn.BackColor = System.Drawing.Color.Transparent;
		this.rbDineIn.Checked = true;
		this.rbDineIn.Name = "rbDineIn";
		this.rbDineIn.TabStop = true;
		this.rbDineIn.UseVisualStyleBackColor = false;
		this.rbDineIn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbTakeAway, "rbTakeAway");
		this.rbTakeAway.BackColor = System.Drawing.Color.Transparent;
		this.rbTakeAway.Name = "rbTakeAway";
		this.rbTakeAway.UseVisualStyleBackColor = false;
		this.rbTakeAway.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbDelivery, "rbDelivery");
		this.rbDelivery.BackColor = System.Drawing.Color.Transparent;
		this.rbDelivery.Name = "rbDelivery";
		this.rbDelivery.UseVisualStyleBackColor = false;
		this.rbDelivery.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.cboStore, "cboStore");
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		resources.ApplyResources(this.lblStoreName, "lblStoreName");
		this.lblStoreName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoreName).Name = "lblStoreName";
		((ControlBase)this.lblStoreName).WrapText = false;
		resources.ApplyResources(this.btnDesignHall, "btnDesignHall");
		((System.Windows.Forms.Control)(object)this.btnDesignHall).Name = "btnDesignHall";
		((System.Windows.Forms.Control)(object)this.btnDesignHall).Click += new System.EventHandler(btnDesignHall_Click);
		resources.ApplyResources(this.btnPic, "btnPic");
		((System.Windows.Forms.Control)(object)this.btnPic).Name = "btnPic";
		((System.Windows.Forms.Control)(object)this.btnPic).Click += new System.EventHandler(btnPic_Click);
		resources.ApplyResources(this.lblPic, "lblPic");
		((AppearanceBase)val60).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val60).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val60, "appearance53");
		((ControlBase)this.lblPic).Appearance = (AppearanceBase)(object)val60;
		this.lblPic.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPic).Name = "lblPic";
		((ControlBase)this.lblPic).WrapText = false;
		resources.ApplyResources(this.picRoom, "picRoom");
		this.picRoom.BorderShadowColor = System.Drawing.Color.Empty;
		this.picRoom.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.picRoom).Name = "picRoom";
		((System.Windows.Forms.Control)(object)this.picRoom).MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(PictureBox_MouseDoubleClick);
		resources.ApplyResources(this.txtTableCount, "txtTableCount");
		((System.Windows.Forms.Control)(object)this.txtTableCount).Name = "txtTableCount";
		((TextEditorControlBase)this.txtTableCount).ValueChanged += new System.EventHandler(txtTableCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTableCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTableCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtTableCount).Leave += new System.EventHandler(txtTableCount_Leave);
		resources.ApplyResources(this.lblTableCount, "lblTableCount");
		this.lblTableCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTableCount).Name = "lblTableCount";
		((ControlBase)this.lblTableCount).WrapText = false;
		resources.ApplyResources(this.txtRoomEnglishName, "txtRoomEnglishName");
		((System.Windows.Forms.Control)(object)this.txtRoomEnglishName).Name = "txtRoomEnglishName";
		resources.ApplyResources(this.lblRoomEnglishName, "lblRoomEnglishName");
		this.lblRoomEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomEnglishName).Name = "lblRoomEnglishName";
		((ControlBase)this.lblRoomEnglishName).WrapText = false;
		resources.ApplyResources(this.txtRoomArabicName, "txtRoomArabicName");
		((System.Windows.Forms.Control)(object)this.txtRoomArabicName).Name = "txtRoomArabicName";
		resources.ApplyResources(this.lblRoomArabicName, "lblRoomArabicName");
		this.lblRoomArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomArabicName).Name = "lblRoomArabicName";
		((ControlBase)this.lblRoomArabicName).WrapText = false;
		this.ofdPicture.FileName = "ofdPicture";
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.lblBranch, "lblBranch");
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		resources.ApplyResources(this.chkMinCharge, "chkMinCharge");
		((System.Windows.Forms.Control)(object)this.chkMinCharge).Name = "chkMinCharge";
		((UltraToggleEditorBase)this.chkMinCharge).CheckedChanged += new System.EventHandler(chkMinCharge_CheckedChanged);
		resources.ApplyResources(this.chkDynamicMinCharge, "chkDynamicMinCharge");
		((System.Windows.Forms.Control)(object)this.chkDynamicMinCharge).Name = "chkDynamicMinCharge";
		((UltraToggleEditorBase)this.chkDynamicMinCharge).CheckedChanged += new System.EventHandler(chkDynamicMinCharge_CheckedChanged);
		resources.ApplyResources(this.txtMinChargeAmount, "txtMinChargeAmount");
		((System.Windows.Forms.Control)(object)this.txtMinChargeAmount).Name = "txtMinChargeAmount";
		((TextEditorControlBase)this.txtMinChargeAmount).ValueChanged += new System.EventHandler(txtMinChargeAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtMinChargeAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMinChargeAmount_KeyPress);
		resources.ApplyResources(this.lblMinChargeAmount, "lblMinChargeAmount");
		this.lblMinChargeAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMinChargeAmount).Name = "lblMinChargeAmount";
		((ControlBase)this.lblMinChargeAmount).WrapText = false;
		resources.ApplyResources(this.txtServiceChargePercent, "txtServiceChargePercent");
		((System.Windows.Forms.Control)(object)this.txtServiceChargePercent).Name = "txtServiceChargePercent";
		((System.Windows.Forms.Control)(object)this.txtServiceChargePercent).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMinChargeAmount_KeyPress);
		resources.ApplyResources(this.lblServiceCharge, "lblServiceCharge");
		this.lblServiceCharge.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceCharge).Name = "lblServiceCharge";
		((ControlBase)this.lblServiceCharge).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboMinChargeDiff, "cboMinChargeDiff");
		((TextEditorControlBase)this.cboMinChargeDiff).AlwaysInEditMode = true;
		this.cboMinChargeDiff.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMinChargeDiff).Name = "cboMinChargeDiff";
		resources.ApplyResources(this.lblMinChargeDiff, "lblMinChargeDiff");
		this.lblMinChargeDiff.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMinChargeDiff).Name = "lblMinChargeDiff";
		((ControlBase)this.lblMinChargeDiff).WrapText = false;
		resources.ApplyResources(this.txtRoundingValue, "txtRoundingValue");
		((System.Windows.Forms.Control)(object)this.txtRoundingValue).Name = "txtRoundingValue";
		((System.Windows.Forms.Control)(object)this.txtRoundingValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMinChargeAmount_KeyPress);
		resources.ApplyResources(this.lblRoundingValue, "lblRoundingValue");
		this.lblRoundingValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoundingValue).Name = "lblRoundingValue";
		((ControlBase)this.lblRoundingValue).WrapText = false;
		resources.ApplyResources(this.chkMaxCharge, "chkMaxCharge");
		((System.Windows.Forms.Control)(object)this.chkMaxCharge).Name = "chkMaxCharge";
		((UltraToggleEditorBase)this.chkMaxCharge).CheckedChanged += new System.EventHandler(chkMaxChargeValue_CheckedChanged);
		resources.ApplyResources(this.lblMaxChargeAmount, "lblMaxChargeAmount");
		this.lblMaxChargeAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaxChargeAmount).Name = "lblMaxChargeAmount";
		((ControlBase)this.lblMaxChargeAmount).WrapText = false;
		resources.ApplyResources(this.txtMaxChargeAmount, "txtMaxChargeAmount");
		((System.Windows.Forms.Control)(object)this.txtMaxChargeAmount).Name = "txtMaxChargeAmount";
		((TextEditorControlBase)this.txtMaxChargeAmount).ValueChanged += new System.EventHandler(txtMaxChargeAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtMaxChargeAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMaxChargeAmount_KeyPress);
		resources.ApplyResources(this.chkDynamicMaxCharge, "chkDynamicMaxCharge");
		((System.Windows.Forms.Control)(object)this.chkDynamicMaxCharge).Name = "chkDynamicMaxCharge";
		((UltraToggleEditorBase)this.chkDynamicMaxCharge).CheckedChanged += new System.EventHandler(chkDynamicMaxCharge_CheckedChanged);
		resources.ApplyResources(this.lblMaxChargeDiff, "lblMaxChargeDiff");
		this.lblMaxChargeDiff.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaxChargeDiff).Name = "lblMaxChargeDiff";
		((ControlBase)this.lblMaxChargeDiff).WrapText = false;
		resources.ApplyResources(this.cboMaxChargeDiff, "cboMaxChargeDiff");
		((TextEditorControlBase)this.cboMaxChargeDiff).AlwaysInEditMode = true;
		this.cboMaxChargeDiff.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaxChargeDiff).Name = "cboMaxChargeDiff";
		resources.ApplyResources(this.chkWithoutSalesJV, "chkWithoutSalesJV");
		((System.Windows.Forms.Control)(object)this.chkWithoutSalesJV).Name = "chkWithoutSalesJV";
		((UltraToggleEditorBase)this.chkWithoutSalesJV).CheckedChanged += new System.EventHandler(chkWithoutSalesJV_CheckedChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaxChargeDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMinChargeDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaxChargeDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMinChargeDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceChargePercent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMaxChargeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaxChargeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMinChargeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkDynamicMaxCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMinChargeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMaxCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkDynamicMinCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithoutSalesJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMinCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoreName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDesignHall);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picRoom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTableCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTableCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoomEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoomArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomArabicName);
		base.Name = "frmRooms";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoomArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoomArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoomEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoomEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTableCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTableCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDesignHall, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoreName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMinCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithoutSalesJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkDynamicMinCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMaxCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMinChargeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkDynamicMaxCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMinChargeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaxChargeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMaxChargeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceChargePercent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMinChargeDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaxChargeDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMinChargeDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaxChargeDiff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoundingValue, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGMinCharge).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGMaxCharge).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPickupPrinter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPackingClassification).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecialOrderReport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkShowFirstDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAdditionalDiscountIncludeTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAdditionalDiscountWithoutTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkNetPriceIsCashDefaultAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceCaptainOrderSelection).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintClosingCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintCheckCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintCheck).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintTaxInvoices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPrintWithoutCheckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentItemsMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintAlert).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckAlert).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTakeAwayReport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryReport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDineInReport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataRoomsOffers).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMinCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDynamicMinCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinChargeAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargePercent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMinChargeDiff).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaxCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxChargeAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDynamicMaxCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaxChargeDiff).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithoutSalesJV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
