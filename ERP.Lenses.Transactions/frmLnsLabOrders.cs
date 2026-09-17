using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.POS.MasterData;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Lenses.Transactions;

public class frmLnsLabOrders : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtSponsorsSubAccounts;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private DataTable dtClients;

	private DataTable dtSalesMan;

	private DataTable dtLabs;

	private DataTable dtTaxs;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItems;

	private DataTable dtVisaType;

	private DataTable dtDetailsLenses;

	private DataTable dtLabOrdersPayments;

	private DataTable dtUsers;

	private DataTable dtLabOrdersServices;

	private DataTable dtLabOrdersContractsDetails;

	private DataTable dtLabOrdersDestroyedItems;

	private DataTable dtItemPrices;

	private DataTable dtLabsItemsPrices;

	private DataTable dtDoctors;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtLensesDiameters;

	private DataTable dtSponsorContract;

	private DataTable dtFamilyRelatives;

	private ValueList vlFrames = new ValueList();

	private ValueList vlRightLense = new ValueList();

	private ValueList vlLeftLense = new ValueList();

	private ValueList vlServices = new ValueList();

	private ValueList vlDestroyedItems = new ValueList();

	private ValueList vlFramesColors = new ValueList();

	private ValueList vlRightColors = new ValueList();

	private ValueList vlLeftColors = new ValueList();

	private ValueList vlDestroyedColors = new ValueList();

	private ValueList vlFramesSizes = new ValueList();

	private ValueList vlRightSizes = new ValueList();

	private ValueList vlLeftSizes = new ValueList();

	private ValueList vlDestroyedSizes = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlVisaType = new ValueList();

	private ValueList vlLensesDiameters = new ValueList();

	private ValueList vlGlassesType = new ValueList();

	private ValueList vlOnCostOfID = new ValueList();

	private DataView dvItems;

	private DataSet ds;

	private string LabContractID = "";

	private int newID = -100000;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private decimal ClientsGlassesHistoryID = default(decimal);

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private bool UserFourthClassification = false;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnClientSearch;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataPayments;

	private UltraLabel lblLabs;

	private UltraComboEditor cboLabs;

	private UltraCheckEditor chkIsDeliverd;

	private UltraDateTimeEditor dtpDeliverdDate;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	public UltraButton btnClientAdd;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraButton btnInvoicePayments;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDataServices;

	private UltraComboEditor cboMobile;

	private UltraLabel lblMobile;

	public UltraButton btnSalesManSearch;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMan;

	private UltraLabel lblmmDistance;

	private UltraLabel lblmmReading;

	private UltraTextEditor txtIPDReading;

	private UltraTextEditor txtIPDDistance;

	private UltraLabel lblIPDReading;

	private UltraLabel lblIPDDistance;

	private UltraLabel lblIPD;

	private UltraLabel lblLAdd;

	private UltraComboEditor cboLSizeReading;

	private UltraTextEditor txtLAxisReading;

	private UltraComboEditor cboLColorReading;

	private UltraComboEditor cboLSizeDistance;

	private UltraTextEditor txtLAxisDistance;

	private UltraLabel lblLCyl;

	private UltraLabel lblLAxis;

	private UltraTextEditor txtLAdd;

	private UltraLabel lblLSph;

	private UltraComboEditor cboLColorDistance;

	private UltraLabel lblL;

	private UltraLabel lblRAdd;

	private UltraComboEditor cboRSizeReading;

	private UltraTextEditor txtRAxisReading;

	private UltraComboEditor cboRColorReading;

	private UltraComboEditor cboRSizeDistance;

	private UltraTextEditor txtRAxisDistance;

	private UltraLabel lblRCyl;

	private UltraLabel lblRAxis;

	private UltraTextEditor txtRAdd;

	private UltraLabel lblDistance;

	private UltraLabel lblRSph;

	private UltraComboEditor cboRColorDistance;

	private UltraLabel lblR;

	private UltraLabel lblReading;

	private UltraGroupBox UGBGlassesHistory;

	public UltraButton btnAddHistory;

	private UltraDateTimeEditor dtpGlassesHistoryDate;

	private UltraLabel lblDoctor;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboDoctor;

	private UltraTabPageControl ultraTabPageControl4;

	protected internal UltraGrid ULGDataDestroyed;

	private UltraLabel lblDestroyed;

	private UltraTextEditor txtDestroyed;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	private UltraButton btnCanceled;

	private UltraDateTimeEditor dtpCancelDate;

	private UltraCheckEditor chkIsCanceled;

	private UltraComboEditor cboVisaType;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraCheckEditor chkVisa;

	public UltraButton btnSponsorSearch;

	private UltraLabel lblSponsor;

	private UltraComboEditor cboSponsor;

	private UltraLabel lblCompanyDiscount;

	private UltraTextEditor txtCompanyDiscount;

	private UltraLabel lblClientDiscount;

	private UltraTextEditor txtClientDiscount;

	private UltraTabPageControl ultraTabPageControl5;

	protected internal UltraGrid ULGDataContractsDetails;

	private UltraButton btnSelectDetails;

	private UltraTextEditor txtTotalContractAmount;

	private UltraLabel lblTotalContractAmount;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	public UltraButton btnRTranspose;

	public UltraButton btnLTranspose;

	private UltraLabel lblSponsorCompanyName;

	private UltraTextEditor txtSponsorCompanyName;

	private UltraComboEditor cboFamilyRelatives;

	private UltraLabel lblFamilyRelative;

	private UltraTextEditor txtClientLoadAmount;

	private UltraLabel lblClientLoadAmount;

	private UltraLabel lblSponsorApprovalNo;

	private UltraTextEditor txtSponsorApprovalNo;

	public frmLnsLabOrders()
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
		InitializeComponent();
		TableName = "Lns_LabOrders";
		IDCol = "LabOrderID";
		NoCol = "LabOrderNo";
		DateCol = "LabOrderDate";
	}

	public frmLnsLabOrders(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpDeliverdDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UserFourthClassification = ItemsFourthClassifications.Select("-1", "-1", "1", IsFromServer: false).Rows.Count > 0;
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtClients = Clients.FillComboWithGlassesHistory(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
		dtSponsorsSubAccounts = LabContractsClients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSponsor, dtSponsorsSubAccounts, "SubAccountID", "SubAccountName");
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtLabs = BusinessLayer.Lenses.Labs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLabs, dtLabs, "LabID", "LabName");
		dtLensesDiameters = LensesDiameters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlLensesDiameters.ValueListItems.Clear();
		for (int i = 0; i < dtLensesDiameters.Rows.Count; i++)
		{
			vlLensesDiameters.ValueListItems.Add(dtLensesDiameters.Rows[i]["LenseDiameterID"], dtLensesDiameters.Rows[i]["Diameter"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtFamilyRelatives = FamilyRelatives.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFamilyRelatives, dtFamilyRelatives, "FamilyRelativeID", "FamilyRelativeName");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		vlFramesColors.ValueListItems.Clear();
		vlRightColors.ValueListItems.Clear();
		vlLeftColors.ValueListItems.Clear();
		vlDestroyedColors.ValueListItems.Clear();
		for (int j = 0; j < dtColors.Rows.Count; j++)
		{
			vlFramesColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			vlRightColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			vlLeftColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			vlDestroyedColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		vlFramesSizes.ValueListItems.Clear();
		vlRightSizes.ValueListItems.Clear();
		vlLeftSizes.ValueListItems.Clear();
		vlDestroyedSizes.ValueListItems.Clear();
		for (int k = 0; k < dtSizes.Rows.Count; k++)
		{
			vlFramesSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			vlRightSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			vlLeftSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			vlDestroyedSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
		}
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDestroyedItems.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlDestroyedItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "ItemTypeID =1";
		dvItems.ToTable();
		vlFrames.ValueListItems.Clear();
		for (int m = 0; m < dvItems.Count; m++)
		{
			vlFrames.ValueListItems.Add(dvItems[m]["ItemID"], dvItems[m]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "ItemTypeID =2";
		dvItems.ToTable();
		vlRightLense.ValueListItems.Clear();
		vlLeftLense.ValueListItems.Clear();
		for (int n = 0; n < dvItems.Count; n++)
		{
			vlRightLense.ValueListItems.Add(dvItems[n]["ItemID"], dvItems[n]["Name"].ToString());
			vlLeftLense.ValueListItems.Add(dvItems[n]["ItemID"], dvItems[n]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "IsService=1";
		dvItems.ToTable();
		vlServices.ValueListItems.Clear();
		for (int num = 0; num < dvItems.Count; num++)
		{
			vlServices.ValueListItems.Add(dvItems[num]["ItemID"], dvItems[num]["Name"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtUsers.Rows.Count; num2++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num2]["User_ID"], dtUsers.Rows[num2]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		vlVisaType.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtVisaType.Rows.Count; num3++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[num3]["VisaTypeID"], dtVisaType.Rows[num3]["VisaTypeName"].ToString());
		}
		vlGlassesType.ValueListItems.Clear();
		vlGlassesType.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "قراءة" : "Reading");
		vlGlassesType.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "مسافات" : "Distance");
		vlGlassesType.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "شمس" : "Sun");
		vlGlassesType.ValueListItems.Add((object)4, GlobalVariables.IsArabic ? "مالتى فوكل" : "MultiFocal");
		vlGlassesType.ValueListItems.Add((object)5, GlobalVariables.IsArabic ? "باى فوكل" : "BiFocal");
		vlOnCostOfID.ValueListItems.Clear();
		vlOnCostOfID.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "معمل" : "Lab");
		vlOnCostOfID.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "فرع" : "Store");
		vlOnCostOfID.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "عميل" : "Client");
		dtDetails = LabOrdersDetails.SelectFramesByLabOrderID("0");
		dtDetailsLenses = LabOrdersDetails.SelectLensesByLabOrderID("0");
		dtLabOrdersPayments = LabOrdersPayments.SelectByLabOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtLabOrdersServices = LabOrdersServices.SelectByLabOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtLabOrdersDestroyedItems = LabOrdersDetails.SelectDestroyedItemsByLabOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtLabOrdersContractsDetails = LabOrdersContractsDetails.SelectByLabOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtDetailsLenses);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtDetailsLenses";
		ds.Relations.Add(ds.Tables[0].Columns["LabOrderDetailID"], ds.Tables[1].Columns["ParentID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		((UltraGridBase)ULGDataPayments).DataSource = dtLabOrdersPayments;
		((UltraGridBase)ULGDataServices).DataSource = dtLabOrdersServices;
		((UltraGridBase)ULGDataContractsDetails).DataSource = dtLabOrdersContractsDetails;
		((UltraGridBase)ULGDataDestroyed).DataSource = dtLabOrdersDestroyedItems;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		GlobalFunctions.PrepareGrid(ULGDataServices);
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		GlobalFunctions.PrepareGrid(ULGDataContractsDetails);
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		GlobalFunctions.PrepareGrid(ULGDataDestroyed);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDetailID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrameItemID"].Header).Caption = (GlobalVariables.IsArabic ? "النظارة" : "Frame");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = (GlobalVariables.IsArabic ? "اللون" : "Color");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = (GlobalVariables.IsArabic ? "المقاس" : "Size");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LenseDiameterID"].Header).Caption = (GlobalVariables.IsArabic ? "قطر العدسه" : "Lense Diameter");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Header).Caption = (GlobalVariables.IsArabic ? "مستلم" : "Deliverd");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrameItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LenseDiameterID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrameItemID"].ValueList = (IValueList)(object)vlFrames;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlFramesColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlFramesSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].ValueList = (IValueList)(object)vlGlassesType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LenseDiameterID"].ValueList = (IValueList)(object)vlLensesDiameters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsReturned"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDestroyed"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLenseLabOrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLenseLabOrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLenseItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.23) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsManufactured"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLenseItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.23);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsManufactured"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLenseItemID"].Header).Caption = (GlobalVariables.IsArabic ? "R" : "R");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsManufactured"].Header).Caption = (GlobalVariables.IsArabic ? "مصنعه" : "Is Manufactured");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLenseItemID"].Header).Caption = (GlobalVariables.IsArabic ? "L" : "L");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsManufactured"].Header).Caption = (GlobalVariables.IsArabic ? "مصنعه" : "Is Manufactured");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLenseItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsManufactured"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLenseItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsManufactured"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLenseItemID"].ValueList = (IValueList)(object)vlRightLense;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorID"].ValueList = (IValueList)(object)vlRightColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeID"].ValueList = (IValueList)(object)vlRightSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLenseItemID"].ValueList = (IValueList)(object)vlLeftLense;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LColorID"].ValueList = (IValueList)(object)vlLeftColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LItemSizeID"].ValueList = (IValueList)(object)vlLeftSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsReturned"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsManufactured"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RDiscount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RLabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsDeliverd"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsDestroyed"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RIsManufactured"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsReturned"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LDiscount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LTaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LLabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsDeliverd"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsDestroyed"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LIsManufactured"].DefaultCellValue = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["LabOrderPaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabOrderServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["FromLab"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromBranchStore"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromLabStore"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["OnCostOfID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromBranchStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المحل" : "From Store");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر المعمل" : " Lab Price");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromLabStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المعمل" : "From Lab");
		((HeaderBase)((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["OnCostOfID"].Header).Caption = (GlobalVariables.IsArabic ? "تحميل" : "Cost");
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromLabStore"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromBranchStore"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["LabUnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["OnCostOfID"].Hidden = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlDestroyedItems;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlDestroyedSizes;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlDestroyedColors;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["OnCostOfID"].ValueList = (IValueList)(object)vlOnCostOfID;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromLabStore"].DefaultCellValue = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["FromBranchStore"].DefaultCellValue = false;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["LabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataDestroyed).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["LabOrderContractDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["LabContractDetailDescription"].Width = (int)((double)((Control)(object)ULGDataContractsDetails).Width * 0.8) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGDataContractsDetails).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["LabContractDetailDescription"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((HeaderBase)((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["LabContractDetailDescription"].Hidden = false;
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = LabOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["LabOrderNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["LabOrderDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboLabs).Value = drMaster["LabID"];
			((TextEditorControlBase)cboSalesMan).Value = drMaster["EmployeeID"];
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			((TextEditorControlBase)cboSponsor).ValueChanged -= cboSponsor_ValueChanged;
			((TextEditorControlBase)cboSponsor).Value = drMaster["SponsorSubAccountID"];
			LabContractID = ((drMaster["LabContractID"] == null) ? "" : drMaster["LabContractID"].ToString());
			((TextEditorControlBase)cboSponsor).ValueChanged += cboSponsor_ValueChanged;
			((TextEditorControlBase)cboFamilyRelatives).Value = drMaster["FamilyRelativeID"];
			((Control)(object)txtSponsorCompanyName).Text = drMaster["SponsorCompanyName"].ToString();
			((Control)(object)txtSponsorApprovalNo).Text = drMaster["SponsorApprovalNo"].ToString();
			((Control)(object)txtCompanyDiscount).Text = decimal.Parse(drMaster["CompanyDiscount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtClientDiscount).Text = decimal.Parse(drMaster["ClientDiscount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClientsGlassesHistoryID = ((drMaster["ClientsGlassesHistoryID"] != DBNull.Value) ? int.Parse(drMaster["ClientsGlassesHistoryID"].ToString()) : 0);
			if (cboClient.SelectedIndex == -1)
			{
				ClearClientGlassesHistory();
			}
			else
			{
				FillClientGlassesHistory(ClientsGlassesHistoryID);
			}
			((UltraToggleEditorBase)chkIsDeliverd).Checked = bool.Parse(drMaster["IsDeliverd"].ToString());
			dtpDeliverdDate.Value = ((drMaster["DeliverdDate"] == DBNull.Value) ? DBNull.Value : drMaster["DeliverdDate"]);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
			((TextEditorControlBase)cboTax).Value = drMaster["TaxID"];
			((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalContractAmount).Text = decimal.Parse(drMaster["TotalContractAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtClientLoadAmount).ValueChanged -= txtClientLoadAmount_ValueChanged;
			((Control)(object)txtClientLoadAmount).Text = decimal.Parse(drMaster["ClientLoadAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtClientLoadAmount).ValueChanged += txtClientLoadAmount_ValueChanged;
			((UltraToggleEditorBase)chkIsCanceled).Checked = bool.Parse(drMaster["IsCanceled"].ToString());
			dtpCancelDate.Value = ((drMaster["CanceledDate"] == DBNull.Value) ? DBNull.Value : drMaster["CanceledDate"]);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = LabOrdersDetails.SelectFramesByLabOrderID(drMaster["LabOrderID"].ToString());
			dtDetailsLenses = LabOrdersDetails.SelectLensesByLabOrderID(drMaster["LabOrderID"].ToString());
			dtLabOrdersPayments = LabOrdersPayments.SelectByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtLabOrdersServices = LabOrdersServices.SelectByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtLabOrdersContractsDetails = LabOrdersContractsDetails.SelectByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtLabOrdersDestroyedItems = LabOrdersDetails.SelectDestroyedItemsByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			object obj = dtLabOrdersDestroyedItems.Compute(" Sum(UnitPrice) ", "OnCostOfID=3");
			((Control)(object)txtDestroyed).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) + decimal.Parse((((Control)(object)txtDestroyed).Text == "" || ((Control)(object)txtDestroyed).Text == ".") ? "0" : ((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			object obj2 = dtLabOrdersPayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj2 == DBNull.Value) ? "0" : obj2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) + decimal.Parse((((Control)(object)txtDestroyed).Text == "" || ((Control)(object)txtDestroyed).Text == ".") ? "0" : ((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtDetailsLenses);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtDetailsLenses";
			ds.Relations.Add(ds.Tables[0].Columns["LabOrderDetailID"], ds.Tables[1].Columns["ParentID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			((UltraGridBase)ULGDataPayments).DataSource = dtLabOrdersPayments;
			((UltraGridBase)ULGDataServices).DataSource = dtLabOrdersServices;
			((UltraGridBase)ULGDataContractsDetails).DataSource = dtLabOrdersContractsDetails;
			((UltraGridBase)ULGDataDestroyed).DataSource = dtLabOrdersDestroyedItems;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || drMaster["IsCanceled"].Equals(true))
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
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLabs).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMobile).ReadOnly = NavMode;
		((Control)(object)chkIsDeliverd).Enabled = !NavMode;
		((EditorButtonControlBase)dtpDeliverdDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSponsor).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFamilyRelatives).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSponsorCompanyName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSponsorApprovalNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtClientLoadAmount).ReadOnly = NavMode;
		DiscountUserID = 0;
		((Control)(object)btnRTranspose).Visible = !NavMode;
		((Control)(object)btnLTranspose).Visible = !NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = !Adding;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)btnSponsorSearch).Visible = !NavMode;
		((Control)(object)btnClientAdd).Visible = !NavMode;
		((Control)(object)btnAddHistory).Visible = !NavMode;
		((Control)(object)btnInvoicePayments).Visible = Updating;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		((Control)(object)chkVisa).Visible = Adding;
		((Control)(object)cboVisaType).Visible = Adding;
		((Control)(object)lblVisaNo).Visible = Adding;
		((Control)(object)txtVisaNo).Visible = Adding;
		((UltraTabControlBase)UTCDetails).Tabs["Payments"].Visible = !Adding;
		((UltraTabControlBase)UTCDetails).Tabs["Destroyed"].Visible = !Adding;
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataContractsDetails).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		FilterLeftRightLenses(ClearLensesData: false);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value != DBNull.Value)
				{
					DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value.ToString())[0];
					if (dataRow["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = DBNull.Value;
						}
					}
					if (dataRow["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = DBNull.Value;
						}
					}
				}
				if (j != 0)
				{
					continue;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value != DBNull.Value)
				{
					DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value.ToString())[0];
					if (dataRow2["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].Value = DBNull.Value;
						}
					}
					if (dataRow2["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].Value = DBNull.Value;
						}
					}
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow3 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value.ToString())[0];
				if (dataRow3["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].Value = DBNull.Value;
					}
				}
				if (dataRow3["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[k]["VisaTypeID"], dataTable.Rows[k]["VisaTypeName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int l = 0; l < dtVisaType.Rows.Count; l++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[l]["VisaTypeID"], dtVisaType.Rows[l]["VisaTypeName"].ToString());
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		DiscountUserID = 0;
		((Control)(object)txtCode).Text = (Adding ? LabOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboSalesMan.SelectedIndex = -1;
		((TextEditorControlBase)cboLabs).ValueChanged -= cboLabs_ValueChanged;
		cboLabs.SelectedIndex = -1;
		if (Adding)
		{
			object obj = dtUsers.Select("User_ID = " + GlobalVariables.UserID)[0]["SubAccountID"];
			if (obj != DBNull.Value)
			{
				((TextEditorControlBase)cboSalesMan).Value = obj;
			}
			DataRow[] array = dtLabs.Select("LabBranchID = " + GlobalVariables.CurrentBranchID);
			if (array.Length != 0)
			{
				((TextEditorControlBase)cboLabs).Value = array[0]["LabID"];
			}
		}
		((TextEditorControlBase)cboLabs).ValueChanged += cboLabs_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
		UltraComboEditor obj2 = cboClient;
		int selectedIndex = (cboMobile.SelectedIndex = -1);
		obj2.SelectedIndex = selectedIndex;
		((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		ClearClientGlassesHistory();
		((TextEditorControlBase)cboSponsor).ValueChanged -= cboSponsor_ValueChanged;
		cboSponsor.SelectedIndex = -1;
		((TextEditorControlBase)cboSponsor).ValueChanged += cboSponsor_ValueChanged;
		((Control)(object)txtCompanyDiscount).Text = "0";
		((Control)(object)txtClientDiscount).Text = "0";
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.DateTime = dtpDate.DateTime;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtSponsorCompanyName).Clear();
		((TextEditorControlBase)txtSponsorApprovalNo).Clear();
		cboFamilyRelatives.SelectedIndex = -1;
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalContractAmount).Text = "0";
		((TextEditorControlBase)txtClientLoadAmount).ValueChanged -= txtClientLoadAmount_ValueChanged;
		((Control)(object)txtClientLoadAmount).Text = "0";
		((TextEditorControlBase)txtClientLoadAmount).ValueChanged += txtClientLoadAmount_ValueChanged;
		((Control)(object)txtDestroyed).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		ClientsGlassesHistoryID = default(decimal);
		((TextEditorControlBase)txtBarCode).Clear();
		((UltraToggleEditorBase)chkIsCanceled).Checked = false;
		dtpCancelDate.Value = null;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		cboVisaType.SelectedIndex = -1;
		((Control)(object)txtVisaNo).Text = "";
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataServices).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataContractsDetails).DataSource).Rows.Clear();
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboLabs.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المعمل" : "Please Select Lab");
			((TextEditorControlBase)cboLabs).Focus();
			cboLabs.DropDown();
			return false;
		}
		if (decimal.Parse(((Control)(object)txtRestAmount).Text) < 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? ".القيمة المدفوعة اكبر من إجمالى الفاتورة" : "Paid Amount Greater than Invoice Total Amount.");
			((TextEditorControlBase)txtPaidAmount).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkVisa).Checked && cboVisaType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع الفيزا" : "Please Select Visa Type");
			((TextEditorControlBase)cboVisaType).Focus();
			cboVisaType.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkVisa).Checked && ((Control)(object)txtVisaNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الفيزا" : "Please Select Visa No");
			((TextEditorControlBase)txtVisaNo).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsDeliverd).Checked && decimal.Parse((((Control)(object)txtRestAmount).Text == "" || ((Control)(object)txtRestAmount).Text == ".") ? "0" : ((Control)(object)txtRestAmount).Text) > 0m && dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء سداد كامل القيمة قبل التسليم" : "Please Pay The Total Amount Before Delivery");
			return false;
		}
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_LabOrders", "LabOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["LabOrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = LabOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار الوحدة  ", "Please Select Unit");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار النوع  ", "Please Select Glasses Type");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"];
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RUnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار الوحدة للعدسة اليمنى ", "Please Select Unit For Right Lense");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RUnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value != DBNull.Value)
				{
					DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value.ToString())[0];
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].Value.ToString()).Length == 0 || (dataRow["ItemColorCategoryID"] != DBNull.Value && dtItemsColorCategorysDetails.Select(" ItemColorCategoryID = " + int.Parse(dataRow["ItemColorCategoryID"].ToString()) + " and ColorID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].Value.ToString()).Length == 0))
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"];
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].DroppedDown = true;
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].Value.ToString()).Length == 0 || (dataRow["ItemSizeCategoryID"] != DBNull.Value && dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID = " + int.Parse(dataRow["ItemSizeCategoryID"].ToString()) + " and ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].Value.ToString()).Length == 0))
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"];
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].DroppedDown = true;
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
				else if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LUnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار الوحدة للعدسة اليسرى ", "Please Select Unit For Left Lense");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LUnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value != DBNull.Value)
				{
					DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value.ToString())[0];
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].Value.ToString()).Length == 0 || (dataRow2["ItemColorCategoryID"] != DBNull.Value && dtItemsColorCategorysDetails.Select(" ItemColorCategoryID = " + int.Parse(dataRow2["ItemColorCategoryID"].ToString()) + " and ColorID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].Value.ToString()).Length == 0))
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"];
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LColorID"].DroppedDown = true;
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].Value.ToString()).Length == 0 || (dataRow2["ItemSizeCategoryID"] != DBNull.Value && dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID = " + int.Parse(dataRow2["ItemSizeCategoryID"].ToString()) + " and ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].Value.ToString()).Length == 0))
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"];
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LItemSizeID"].DroppedDown = true;
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الخدمة  ", "Please Select Service Name ");
				ULGDataServices.ActiveCell = ((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceID"];
				((UltraTabControlBase)UTCDetails).Tabs["Services"].Selected = true;
				ULGDataServices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataServices).Rows[k].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataServices).Rows[k].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة الخدمة  ", "Please Enter Service Amount ");
				ULGDataServices.ActiveCell = ((UltraGridBase)ULGDataServices).Rows[k].Cells["UnitPrice"];
				((UltraTabControlBase)UTCDetails).Tabs["Services"].Selected = true;
				ULGDataServices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataServices).Rows[k].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataServices).Rows[k].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة الخدمة  ", "Please Enter Service Amount ");
				ULGDataServices.ActiveCell = ((UltraGridBase)ULGDataServices).Rows[k].Cells["Qty"];
				((UltraTabControlBase)UTCDetails).Tabs["Services"].Selected = true;
				ULGDataServices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
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
		if (int.Parse(dataTable.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
			return false;
		}
		if (dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client ID From Sales Settings ");
			return false;
		}
		if (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الافتراضى من اعدادات البيع  ", "Please Select Default Store From Sales Settings ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public bool ValidateForShift()
	{
		if (Adding)
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
			if (!Updating)
			{
				DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(ShiftDetailID, GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
				if (dataTable2.Rows.Count == 0)
				{
					ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
				}
				else
				{
					ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
				}
			}
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
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		DataRow dataRow = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0];
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			CalculateGoss();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = LabOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboLabs.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLabs).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), "Null", (ClientsGlassesHistoryID == 0m) ? "Null" : ClientsGlassesHistoryID.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (dataRow["PriceTypeID"] == DBNull.Value) ? "Null" : dataRow["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), "Null", (cboSponsor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSponsor).Value.ToString(), ((Control)(object)txtSponsorCompanyName).Text, ((Control)(object)txtSponsorApprovalNo).Text, (cboFamilyRelatives.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFamilyRelatives).Value.ToString(), (LabContractID == "") ? "Null" : LabContractID, (((Control)(object)txtCompanyDiscount).Text == "") ? "0" : ((Control)(object)txtCompanyDiscount).Text, (((Control)(object)txtClientDiscount).Text == "") ? "0" : ((Control)(object)txtClientDiscount).Text, "0", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", "0", (((Control)(object)txtTotalContractAmount).Text == "") ? "0" : ((Control)(object)txtTotalContractAmount).Text, (((Control)(object)txtClientLoadAmount).Text == "") ? "0" : ((Control)(object)txtClientLoadAmount).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", (dataRow["SubAccountID"] == DBNull.Value) ? GlobalVariables.CurrentBranchID : dataRow["SubAccountBranchID"].ToString(), "Null", "Null", "1", (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["LabBranchID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["LabBranchID"].ToString(), "Null", "Null", "Null", "Null", "Null", "0", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = LabOrdersDetails.Insert_Update("-1", num.ToString(), "Null", (((UltraGridBase)ULGData).Rows[j].Cells["FrameItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["FrameItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString(), "0", (((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "1", "0", bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["IsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].Cells["OnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["OnCostOfID"].Value.ToString(), "0", (((UltraGridBase)ULGData).Rows[j].Cells["LabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["LabUnitPrice"].Value.ToString(), "1", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					if (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["PrimaryKey"].Value != DBNull.Value)
					{
						LabOrdersDetails.Insert_Update("-1", num.ToString(), num2.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RLenseItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RLenseItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RUnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RUnitID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RIsManufactured"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RDiscount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RDiscount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "1", bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RIsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ROnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ROnCostOfID"].Value.ToString(), "1", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RLabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["RLabUnitPrice"].Value.ToString(), "0", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
						LabOrdersDetails.Insert_Update("-1", num.ToString(), num2.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LLenseItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LLenseItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LUnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LUnitID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LIsManufactured"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LDiscount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LDiscount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "0", bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LIsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LOnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LOnCostOfID"].Value.ToString(), "1", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LLabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["LLabUnitPrice"].Value.ToString(), "0", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			if (((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
			{
				ShiftsDetails.LabOrdersPaymentsJVAdding(LabOrdersPayments.Insert_Update("-1", LabOrdersPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), num.ToString(), ((UltraToggleEditorBase)chkVisa).Checked ? ((TextEditorControlBase)cboVisaType).Value.ToString() : "Null", ((UltraToggleEditorBase)chkVisa).Checked ? ((Control)(object)txtVisaNo).Text.ToString() : "Null", ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; l++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[l]);
				CalculateServiceRowActualUnitSalesPrice(((UltraGridBase)ULGDataServices).Rows[l]);
				LabOrdersServices.Insert_Update("-1", num.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["ServiceItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["ServiceItemID"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["TotalPrice"].Value.ToString(), ((UltraGridBase)ULGDataServices).Rows[l].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGDataServices).Rows[l].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGDataServices).Rows[l].Cells["ActualUnitSalesPrice"].Value.ToString(), "0", ((UltraGridBase)ULGDataServices).Rows[l].Cells["LabUnitPrice"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataContractsDetails).Rows).Count; m++)
			{
				LabOrdersContractsDetails.Insert_Update("-1", num.ToString(), (((UltraGridBase)ULGDataContractsDetails).Rows[m].Cells["LabContractDetailID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataContractsDetails).Rows[m].Cells["LabContractDetailID"].Value.ToString(), ((UltraGridBase)ULGDataContractsDetails).Rows[m].Cells["Price"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			LabOrders.GenerateSalesJvs("," + num + ",", GlobalVariables.UserID);
			LabOrders.GenerateBranchPurchaseJvs("," + num + ",", GlobalVariables.UserID);
			ItemsTransactions.ManagementInsertUpdateDelete();
			ItemsTransactions.RecalculateCurrentQtyOnly();
			string text = LabOrders.AllowedQty_Message(num.ToString(), "0", "1", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LabMIV", "LabMIV");
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
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_29ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_29f8: Expected O, but got Unknown
		//IL_2af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b01: Expected O, but got Unknown
		DataRow dataRow = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0];
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			text = text + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderDetailID"].Value.ToString() + ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseLabOrderDetailID"].Value != DBNull.Value)
				{
					text = text + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseLabOrderDetailID"].Value.ToString() + ",";
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseLabOrderDetailID"].Value != DBNull.Value)
				{
					text = text + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseLabOrderDetailID"].Value.ToString() + ",";
				}
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			CalculateGoss();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = LabOrders.Insert_Update(drMaster["LabOrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboLabs.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLabs).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), "Null", (ClientsGlassesHistoryID == 0m) ? "Null" : ClientsGlassesHistoryID.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (dataRow["PriceTypeID"] == DBNull.Value) ? "Null" : dataRow["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), "Null", (cboSponsor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSponsor).Value.ToString(), ((Control)(object)txtSponsorCompanyName).Text, ((Control)(object)txtSponsorApprovalNo).Text, (cboFamilyRelatives.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFamilyRelatives).Value.ToString(), (LabContractID == "") ? "Null" : LabContractID, (((Control)(object)txtCompanyDiscount).Text == "") ? "0" : ((Control)(object)txtCompanyDiscount).Text, (((Control)(object)txtClientDiscount).Text == "") ? "0" : ((Control)(object)txtClientDiscount).Text, bool.Parse(drMaster["IsSent"].ToString()) ? "1" : "0", DateTime.Parse(drMaster["SentDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsFinished"].ToString()) ? "1" : "0", DateTime.Parse(drMaster["FinishedDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", "0", (((Control)(object)txtTotalContractAmount).Text == "") ? "0" : ((Control)(object)txtTotalContractAmount).Text, (((Control)(object)txtClientLoadAmount).Text == "") ? "0" : ((Control)(object)txtClientLoadAmount).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, (drMaster["ShiftDetailID"] == DBNull.Value) ? "Null" : drMaster["ShiftDetailID"].ToString(), (drMaster["ShiftDetailUserID"] == DBNull.Value) ? "Null" : drMaster["ShiftDetailUserID"].ToString(), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", (dataRow["SubAccountID"] == DBNull.Value) ? GlobalVariables.CurrentBranchID : dataRow["SubAccountBranchID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "1", (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["LabBranchID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["LabBranchID"].ToString(), (drMaster["LabSalesJVID"] == DBNull.Value) ? "Null" : drMaster["LabSalesJVID"].ToString(), (drMaster["BranchPurchasesJVID"] == DBNull.Value) ? "Null" : drMaster["BranchPurchasesJVID"].ToString(), (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["StockControlJVID2"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID2"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), (drMaster["IsCanceled"] == DBNull.Value) ? "Null" : (bool.Parse(drMaster["IsCanceled"].ToString()) ? "1" : "0"), (drMaster["CanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["CanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.DeleteForUpdate("Lns_LabOrdersDetails", "LabOrderID", drMaster["LabOrderID"].ToString(), "LabOrderDetailID", text);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = LabOrdersDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["LabOrderDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["LabOrderDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["LabOrderDetailID"].Value.ToString(), num.ToString(), "Null", (((UltraGridBase)ULGData).Rows[k].Cells["FrameItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["FrameItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value.ToString(), "0", (((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["IsReturned"].Value.ToString()) ? "1" : "0", "1", "0", bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["IsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].Cells["OnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["OnCostOfID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["FromLabStore"].Value == DBNull.Value) ? "0" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromLabStore"].Value.ToString()) ? "1" : "0"), (((UltraGridBase)ULGData).Rows[k].Cells["LabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["LabUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["FromBranchStore"].Value == DBNull.Value) ? "1" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromBranchStore"].Value.ToString()) ? "1" : "0"), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					if (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PrimaryKey"].Value != DBNull.Value)
					{
						LabOrdersDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLenseLabOrderDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLenseLabOrderDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLenseLabOrderDetailID"].Value.ToString(), num.ToString(), num2.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLenseItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLenseItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RUnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RUnitID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RIsManufactured"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RDiscount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RDiscount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "1", bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RIsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ROnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ROnCostOfID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RFromLabStore"].Value == DBNull.Value) ? "1" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RFromLabStore"].Value.ToString()) ? "1" : "0"), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RLabUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RFromBranchStore"].Value == DBNull.Value) ? "0" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["RFromBranchStore"].Value.ToString()) ? "1" : "0"), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
						LabOrdersDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLenseLabOrderDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLenseLabOrderDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLenseLabOrderDetailID"].Value.ToString(), num.ToString(), num2.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLenseItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLenseItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LUnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LUnitID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LIsManufactured"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LNotes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LNotes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LDiscount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LDiscount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LTaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LTaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["LenseDiameterID"].Value.ToString(), (dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "0", bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LIsDestroyed"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LOnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LOnCostOfID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LFromLabStore"].Value == DBNull.Value) ? "1" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LFromLabStore"].Value.ToString()) ? "1" : "0"), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LLabUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LFromBranchStore"].Value == DBNull.Value) ? "0" : (bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["LFromBranchStore"].Value.ToString()) ? "1" : "0"), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count > 0)
			{
				string text2 = ",";
				ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; m++)
				{
					((UltraGridBase)ULGDataServices).Rows[m].Cells["LabOrderID"].Value = num;
					((UltraGridBase)ULGDataServices).Rows[m].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text2 = text2 + ((UltraGridBase)ULGDataServices).Rows[m].Cells["LabOrderServiceID"].Value.ToString() + ",";
					CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[m]);
					CalculateServiceRowActualUnitSalesPrice(((UltraGridBase)ULGDataServices).Rows[m]);
				}
				ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
				Main.DeleteForUpdate("Lns_LabOrdersServices", "LabOrderID", drMaster["LabOrderID"].ToString(), "LabOrderServiceID", text2);
				LabOrdersServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataServices).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataContractsDetails).Rows).Count > 0)
			{
				string text3 = ",";
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataContractsDetails).Rows).Count; n++)
				{
					((UltraGridBase)ULGDataContractsDetails).Rows[n].Cells["LabOrderID"].Value = num;
					((UltraGridBase)ULGDataContractsDetails).Rows[n].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text3 = text3 + ((UltraGridBase)ULGDataContractsDetails).Rows[n].Cells["LabOrderContractDetailID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("Lns_LabOrdersContractsDetails", "LabOrderID", drMaster["LabOrderID"].ToString(), "LabOrderContractDetailID", text3);
				LabOrdersContractsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataContractsDetails).DataSource, GlobalVariables.UserID);
			}
			LabOrders.GenerateSalesJvs("," + num + ",", GlobalVariables.UserID);
			LabOrders.GenerateBranchPurchaseJvs("," + num + ",", GlobalVariables.UserID);
			string text4 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "LabMIV", "LabMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text4 != "")
			{
				GlobalVariables.InformationMB.Show(text4);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "LabMIV", "LabMIV");
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

	public override void btnUpdateClick()
	{
		if (Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && LabOrders.SyncCanUpdate(RowID) == 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان تم تعديله في المركز الرئيسي برجاء الانتظار حتي الانتهاء من تحديث البيانات من الخادم  ", "Can not Update this Data. This data is Modified in The Central Point Please wait for Syncronization ");
		}
		else if (drMaster != null)
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
			if (bool.Parse(drMaster["IsSent"].ToString()) && !bool.Parse(drMaster["IsFinished"].ToString()) && !bool.Parse(drMaster["IsDeliverd"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تم إرسالها الى المعمل", "Cannot Update This Transaction Because It IS Sent To Lab ");
				return;
			}
			if (bool.Parse(drMaster["IsDeliverd"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تم الاستلام من العميل", "Cannot Update This Transaction Because The Client Deliverd ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
		}
	}

	public override void btnDeleteClick()
	{
		if (Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && LabOrders.SyncCanUpdate(RowID) == 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان تم تعديله في المركز الرئيسي برجاء الانتظار حتي الانتهاء من تحديث البيانات من الخادم  ", "Can not Update this Data. This data is Modified in The Central Point Please wait for Syncronization ");
		}
		else
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
			if (int.Parse(LabOrdersPayments.CheckShiftDetailsClosedByLabOrderID(drMaster["LabOrderID"].ToString()).Rows[0]["Counter"].ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لوردية مغلقة", "Cannot Delete This Transaction Because It IS Related To Closed Shift ");
				return;
			}
			if (bool.Parse(drMaster["IsSent"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تم إرسالها الى المعمل", "Cannot Delete This Transaction Because It IS Sent To Lab ");
				return;
			}
			GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
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
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			LabOrders.DeleteVirtual(drMaster["LabOrderID"].ToString(), GlobalVariables.UserID);
			LabOrdersDetails.DeleteVirtualByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.UserID);
			LabOrdersServices.DeleteVirtualByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.UserID);
			LabOrdersPayments.DeleteVirtualByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["BranchPurchasesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["BranchPurchasesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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

	private void PrintParts(int partNo)
	{
		ReportDocument reportDocument = new ReportDocument();
		switch (partNo)
		{
		case 1:
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_LabOrders_Part1_A.rpt" : "Rep_Lns_LabOrders_Part1_E.rpt"));
			break;
		case 2:
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_LabOrders_PartA5_1_A.rpt" : "Rep_Lns_LabOrders_PartA5_1_E.rpt"));
			break;
		}
		GlobalFunctions.ConfigureReport(reportDocument);
		reportDocument.SetParameterValue("@LabOrderID", RowID);
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
		reportDocument.Dispose();
		if (dtDetails.Select("GlassesTypeID <> 3").Length != 0 || partNo == 2)
		{
			ReportDocument reportDocument2 = new ReportDocument();
			switch (partNo)
			{
			case 1:
				reportDocument2.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_LabOrders_Part2_A.rpt" : "Rep_Lns_LabOrders_Part2_E.rpt"));
				break;
			case 2:
				reportDocument2.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_LabOrders_PartA5_2_A.rpt" : "Rep_Lns_LabOrders_PartA5_2_E.rpt"));
				break;
			}
			GlobalFunctions.ConfigureReport(reportDocument2);
			reportDocument2.SetParameterValue("@LabOrderID", RowID);
			reportDocument2.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument2.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument2.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument2.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex2)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex2.Message, "Check Printer Cable\n" + ex2.Message);
			}
			reportDocument2.Dispose();
		}
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		if (dtReports.Rows.Count > 0)
		{
			if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_LabOrders_Part")
			{
				PrintParts(1);
				return;
			}
			if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_LabOrders_Part2")
			{
				PrintParts(2);
				return;
			}
			reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@LabOrderID", RowID);
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
			reportDocument.Dispose();
		}
		else
		{
			PrintParts(1);
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsLabOrdersReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["LabOrderID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtClients = Clients.FillComboWithGlassesHistory(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtSponsorsSubAccounts = LabContractsClients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSponsor, dtSponsorsSubAccounts, "SubAccountID", "SubAccountName");
		dtLabs = BusinessLayer.Lenses.Labs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLabs, dtLabs, "LabID", "LabName");
		dtLensesDiameters = LensesDiameters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlLensesDiameters.ValueListItems.Clear();
		for (int i = 0; i < dtLensesDiameters.Rows.Count; i++)
		{
			vlLensesDiameters.ValueListItems.Add(dtLensesDiameters.Rows[i]["LenseDiameterID"], dtLensesDiameters.Rows[i]["Diameter"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtFamilyRelatives = FamilyRelatives.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFamilyRelatives, dtFamilyRelatives, "FamilyRelativeID", "FamilyRelativeName");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		vlRightColors.ValueListItems.Clear();
		vlLeftColors.ValueListItems.Clear();
		vlDestroyedColors.ValueListItems.Clear();
		for (int j = 0; j < dtColors.Rows.Count; j++)
		{
			vlRightColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			vlLeftColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			vlDestroyedColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		vlRightSizes.ValueListItems.Clear();
		vlLeftSizes.ValueListItems.Clear();
		vlDestroyedSizes.ValueListItems.Clear();
		for (int k = 0; k < dtSizes.Rows.Count; k++)
		{
			vlRightSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			vlLeftSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			vlDestroyedSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
		}
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "ItemTypeID =1";
		dvItems.ToTable();
		vlFrames.ValueListItems.Clear();
		for (int l = 0; l < dvItems.Count; l++)
		{
			vlFrames.ValueListItems.Add(dvItems[l]["ItemID"], dvItems[l]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "ItemTypeID =2";
		dvItems.ToTable();
		vlRightLense.ValueListItems.Clear();
		vlLeftLense.ValueListItems.Clear();
		for (int m = 0; m < dvItems.Count; m++)
		{
			vlRightLense.ValueListItems.Add(dvItems[m]["ItemID"], dvItems[m]["Name"].ToString());
			vlLeftLense.ValueListItems.Add(dvItems[m]["ItemID"], dvItems[m]["Name"].ToString());
		}
		dvItems = new DataView(dtItems);
		dvItems.RowFilter = "IsService=1";
		dvItems.ToTable();
		vlServices.ValueListItems.Clear();
		for (int n = 0; n < dvItems.Count; n++)
		{
			vlServices.ValueListItems.Add(dvItems[n]["ItemID"], dvItems[n]["Name"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int num = 0; num < dtUsers.Rows.Count; num++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int num2 = 0; num2 < dataTable.Rows.Count; num2++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[num2]["VisaTypeID"], dataTable.Rows[num2]["VisaTypeName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int num3 = 0; num3 < dtVisaType.Rows.Count; num3++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[num3]["VisaTypeID"], dtVisaType.Rows[num3]["VisaTypeName"].ToString());
			}
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		vlGlassesType.ValueListItems.Clear();
		vlGlassesType.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "قراءة" : "Reading");
		vlGlassesType.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "مسافات" : "Distance");
		vlGlassesType.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "شمس" : "Sun");
		vlOnCostOfID.ValueListItems.Clear();
		vlOnCostOfID.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "معمل" : "Lab");
		vlOnCostOfID.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "فرع" : "Store");
		vlOnCostOfID.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "عميل" : "Client");
	}

	public override void ULGData_Enter(object sender, EventArgs e)
	{
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (e.Row != null && ((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["LabOrderDetailID"].Value = ++newID;
			UltraGridRow val = ((UltraGridBase)ULGData).DisplayLayout.Bands[1].AddNew();
			val.Cells["Primarykey"].Value = ++newID;
			val.Cells["RNotes"].Value = "-";
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].AddNew();
		}
		else if (e.Row != null && ((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["Primarykey"].Value = ++newID;
		}
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		((UltraGridBase)ULGData).DataBind();
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_13eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f5: Expected O, but got Unknown
		//IL_1403: Unknown result type (might be due to invalid IL or missing references)
		//IL_140d: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "FrameItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow dataRow2 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
					CalculateGoss();
					CalculateRow(e.Cell.Row);
				}
				if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
				{
					DataRow dataRow3 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["LabUnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
				}
				if (dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (dataRow["ItemSizeCategoryID"] != DBNull.Value)
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
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "GlassesTypeID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				FilterLeftRightLenses(ClearLensesData: false);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows).Count; i++)
				{
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["LLenseItemID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["LColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["LItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["LUnitPrice"].Value = 0;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["RLenseItemID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["RColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["RItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["RUnitPrice"].Value = 0;
				}
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "RLenseItemID" && (e.Cell.Column.ValueList.SelectedItemIndex >= 0 || e.Cell.ValueList.SelectedItemIndex >= 0))
			{
				DataRow dataRow4 = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["RUnitID"].Value = dataRow4["UnitID"];
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow dataRow5 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["RUnitPrice"].Value = decimal.Parse(dataRow5["Price"].ToString()) - decimal.Parse(dataRow5["Price"].ToString()) * decimal.Parse(dataRow5["DiscountPercentage"].ToString()) / 100m;
					CalculateGoss();
					CalculateRow(e.Cell.Row.ParentRow);
				}
				if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
				{
					DataRow dataRow6 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["RLabUnitPrice"].Value = decimal.Parse(dataRow6["Price"].ToString()) - decimal.Parse(dataRow6["Price"].ToString()) * decimal.Parse(dataRow6["DiscountPercentage"].ToString()) / 100m;
				}
				if (dataRow4["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["RColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["RColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow4["ItemColorCategoryID"].ToString()));
					if (e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value != DBNull.Value)
					{
						e.Cell.Row.Cells["RColorID"].Value = ((int.Parse(e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value.ToString()) != 1) ? ((((TextEditorControlBase)cboRColorDistance).Value == null || ((TextEditorControlBase)cboRColorDistance).Value == DBNull.Value) ? DBNull.Value : ((TextEditorControlBase)cboRColorDistance).Value) : ((((TextEditorControlBase)cboRColorReading).Value == null || ((TextEditorControlBase)cboRColorReading).Value == DBNull.Value) ? DBNull.Value : ((TextEditorControlBase)cboRColorReading).Value));
					}
				}
				else
				{
					e.Cell.Row.Cells["RColorID"].Value = 1;
					e.Cell.Row.Cells["RColorID"].ValueList = null;
				}
				if (dataRow4["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["RItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["RItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow4["ItemSizeCategoryID"].ToString()));
					if (e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value != DBNull.Value)
					{
						e.Cell.Row.Cells["RItemSizeID"].Value = ((int.Parse(e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value.ToString()) != 1) ? ((((TextEditorControlBase)cboRSizeDistance).Value == null || ((TextEditorControlBase)cboRSizeDistance).Value == DBNull.Value) ? DBNull.Value : ((TextEditorControlBase)cboRSizeDistance).Value) : ((((TextEditorControlBase)cboRSizeReading).Value == null || ((TextEditorControlBase)cboRSizeReading).Value == DBNull.Value) ? DBNull.Value : ((TextEditorControlBase)cboRSizeReading).Value));
					}
				}
				else
				{
					e.Cell.Row.Cells["RItemSizeID"].Value = 1;
					e.Cell.Row.Cells["RItemSizeID"].ValueList = null;
				}
				e.Cell.Row.Cells["LLenseItemID"].Value = DBNull.Value;
				e.Cell.Row.Cells["LLenseItemID"].ValueList = (IValueList)(object)getLeftLenseValueList(e.Cell.Row.ParentRow);
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "LLenseItemID" && (e.Cell.Column.ValueList.SelectedItemIndex >= 0 || e.Cell.ValueList.SelectedItemIndex >= 0))
			{
				DataRow dataRow7 = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["LUnitID"].Value = dataRow7["UnitID"];
				if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
				{
					DataRow dataRow8 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["LUnitPrice"].Value = decimal.Parse(dataRow8["Price"].ToString()) - decimal.Parse(dataRow8["Price"].ToString()) * decimal.Parse(dataRow8["DiscountPercentage"].ToString()) / 100m;
					CalculateGoss();
					CalculateRow(e.Cell.Row.ParentRow);
				}
				if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
				{
					DataRow dataRow9 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["LLabUnitPrice"].Value = decimal.Parse(dataRow9["Price"].ToString()) - decimal.Parse(dataRow9["Price"].ToString()) * decimal.Parse(dataRow9["DiscountPercentage"].ToString()) / 100m;
				}
				if (dataRow7["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["LColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["LColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow7["ItemColorCategoryID"].ToString()));
					if (e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value != DBNull.Value)
					{
						e.Cell.Row.Cells["LColorID"].Value = ((int.Parse(e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value.ToString()) != 1) ? ((((TextEditorControlBase)cboLColorDistance).Value == null || ((TextEditorControlBase)cboLColorDistance).Value == DBNull.Value) ? DBNull.Value : ((TextEditorControlBase)cboLColorDistance).Value) : ((((TextEditorControlBase)cboLColorReading).Value == DBNull.Value || ((TextEditorControlBase)cboLColorReading).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboLColorReading).Value));
					}
				}
				else
				{
					e.Cell.Row.Cells["LColorID"].Value = 1;
					e.Cell.Row.Cells["LColorID"].ValueList = null;
				}
				if (dataRow7["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["LItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["LItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow7["ItemSizeCategoryID"].ToString()));
					if (e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value != DBNull.Value)
					{
						e.Cell.Row.Cells["LItemSizeID"].Value = ((int.Parse(e.Cell.Row.ParentRow.Cells["GlassesTypeID"].Value.ToString()) != 1) ? ((((TextEditorControlBase)cboLSizeDistance).Value == DBNull.Value || ((TextEditorControlBase)cboLSizeDistance).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboLSizeDistance).Value) : ((((TextEditorControlBase)cboLSizeReading).Value == DBNull.Value || ((TextEditorControlBase)cboLSizeReading).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboLSizeReading).Value));
					}
				}
				else
				{
					e.Cell.Row.Cells["LItemSizeID"].Value = 1;
					e.Cell.Row.Cells["LItemSizeID"].ValueList = null;
				}
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FrameItemID" && ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["LabUnitPrice"].Value = 0);
				obj.Value = value;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateGoss();
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				ULGData.ActiveCell.Value = ((ULGData.ActiveCell.Value == DBNull.Value) ? ((object)0) : ULGData.ActiveCell.Value);
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateGoss();
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RLenseItemID" && ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["RUnitPrice"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["RLabUnitPrice"].Value = 0);
				obj3.Value = value;
				UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["RColorID"];
				UltraGridCell obj6 = ((UltraGridBase)ULGData).ActiveRow.Cells["RItemSizeID"];
				object obj7 = (((UltraGridBase)ULGData).ActiveRow.Cells["RUnitID"].Value = DBNull.Value);
				value = (obj6.Value = obj7);
				obj5.Value = value;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
				CalculateGoss();
				((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].ValueList = (IValueList)(object)getLeftLenseValueList(((UltraGridBase)ULGData).ActiveRow.ParentRow);
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LLenseItemID" && ULGData.ActiveCell.Value == DBNull.Value)
			{
				UltraGridCell obj9 = ((UltraGridBase)ULGData).ActiveRow.Cells["LUnitPrice"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["LLabUnitPrice"].Value = 0);
				obj9.Value = value;
				UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["LColorID"];
				UltraGridCell obj12 = ((UltraGridBase)ULGData).ActiveRow.Cells["LItemSizeID"];
				object obj7 = (((UltraGridBase)ULGData).ActiveRow.Cells["LUnitID"].Value = DBNull.Value);
				value = (obj12.Value = obj7);
				obj11.Value = value;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LUnitPrice")
			{
				ULGData.ActiveCell.Value = ((ULGData.ActiveCell.Value == DBNull.Value) ? ((object)0) : ULGData.ActiveCell.Value);
				CalculateRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
				CalculateGoss();
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (drMaster != null && Updating && bool.Parse(drMaster["IsSent"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID") && ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value == DBNull.Value)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((UltraGridBase)ULGData).ActiveRow.Index > 0)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value == DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LItemSizeID"))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value == DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RItemSizeID"))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LUnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RUnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGoss();
	}

	private ValueList getLeftLenseValueList(UltraGridRow Row)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		string text = "";
		string text2 = "";
		string text3 = "-1";
		string text4 = "-1";
		int num = -1;
		ValueList val = new ValueList();
		if (Row.Cells["GlassesTypeID"].Value != DBNull.Value)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; i++)
			{
				if (int.Parse(Row.Cells["GlassesTypeID"].Value.ToString()) == 1 && cboLColorReading.SelectedIndex > -1 && cboLSizeReading.SelectedIndex > -1)
				{
					text = ((TextEditorControlBase)cboLColorReading).Value.ToString();
					text2 = ((TextEditorControlBase)cboLSizeReading).Value.ToString();
				}
				else if (int.Parse(Row.Cells["GlassesTypeID"].Value.ToString()) == 2 && cboLColorDistance.SelectedIndex > -1 && cboLSizeDistance.SelectedIndex > -1)
				{
					text = ((TextEditorControlBase)cboLColorDistance).Value.ToString();
					text2 = ((TextEditorControlBase)cboLSizeDistance).Value.ToString();
				}
				else if ((int.Parse(Row.Cells["GlassesTypeID"].Value.ToString()) == 4 || int.Parse(Row.Cells["GlassesTypeID"].Value.ToString()) == 5) && cboLColorDistance.SelectedIndex > -1 && cboLColorReading.SelectedIndex > -1 && cboLSizeDistance.SelectedIndex > -1)
				{
					DataTable dataTable = dtItemsColorCategorysDetails.DefaultView.ToTable(true, "ItemColorCategoryID");
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if (dtItemsColorCategorysDetails.Select(" ItemColorCategoryID= " + dataTable.Rows[j]["ItemColorCategoryID"].ToString() + " And ColorID= " + ((TextEditorControlBase)cboLColorDistance).Value.ToString()).Length != 0 && dtItemsColorCategorysDetails.Select(" ItemColorCategoryID= " + dataTable.Rows[j]["ItemColorCategoryID"].ToString() + " And ColorID= " + ((((TextEditorControlBase)cboLColorReading).Value == DBNull.Value) ? "" : ((TextEditorControlBase)cboLColorReading).Value.ToString())).Length != 0)
						{
							text3 = ((!(text3 == "-1")) ? (text3 + "," + dataTable.Rows[j]["ItemColorCategoryID"].ToString()) : dataTable.Rows[j]["ItemColorCategoryID"].ToString());
						}
					}
					text2 = ((TextEditorControlBase)cboLSizeDistance).Value.ToString();
				}
				if (Row.ChildBands[0].Rows[i].Cells["RLenseItemID"].Value != DBNull.Value)
				{
					num = int.Parse(dtItems.Select(" ItemID= " + Row.ChildBands[0].Rows[i].Cells["RLenseItemID"].Value.ToString())[0]["ParentID"].ToString());
				}
			}
			if (text3 == "-1" && text != "")
			{
				DataRow[] array = dtItemsColorCategorysDetails.Select(" ColorID= " + text);
				for (int k = 0; k < array.Length; k++)
				{
					text3 = ((k != 0 || array.Length == 0) ? (text3 + "," + array[k]["ItemColorCategoryID"].ToString()) : array[k]["ItemColorCategoryID"].ToString());
				}
			}
			if (text2 != "")
			{
				DataRow[] array2 = dtItemsSizeCategorysDetails.Select(" ItemSizeID= " + text2);
				for (int l = 0; l < array2.Length; l++)
				{
					text4 = ((l != 0 || array2.Length == 0) ? (text4 + "," + array2[l]["ItemSizeCategoryID"].ToString()) : array2[l]["ItemSizeCategoryID"].ToString());
				}
			}
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = "ItemTypeID =2 " + ((text3 != "-1") ? ("  And ItemColorCategoryID in ( " + text3 + ")") : "") + ((text4 != "-1") ? (" And  ItemSizeCategoryID in (" + text4 + ")") : "") + ((num != -1) ? (" And ParentID= " + num) : "") + ((Row.Cells["GlassesTypeID"].Value != DBNull.Value && int.Parse(Row.Cells["GlassesTypeID"].Value.ToString()) == 3 && UserFourthClassification) ? " And ItemFourthClassificationID=3 " : "");
			DataTable dataTable2 = dataView.ToTable();
			for (int m = 0; m < dataTable2.Rows.Count; m++)
			{
				val.ValueListItems.Add(dataTable2.Rows[m]["ItemID"], dataTable2.Rows[m]["Name"].ToString());
			}
		}
		return val;
	}

	public void FilterLeftRightLenses(bool ClearLensesData)
	{
		//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e55: Expected O, but got Unknown
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d22: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d3: Expected O, but got Unknown
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Expected O, but got Unknown
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a0: Expected O, but got Unknown
		//IL_0a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Expected O, but got Unknown
		if ((cboRColorDistance.SelectedIndex > -1 && cboRSizeDistance.SelectedIndex > -1) || (cboRColorReading.SelectedIndex > -1 && cboRSizeReading.SelectedIndex > -1))
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value == DBNull.Value)
				{
					continue;
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString()) == 1 && cboRColorReading.SelectedIndex > -1 && cboRSizeReading.SelectedIndex > -1)
					{
						string text = "-1";
						string text2 = "-1";
						DataRow[] array = dtItemsColorCategorysDetails.Select(" ColorID= " + ((TextEditorControlBase)cboRColorReading).Value.ToString());
						DataRow[] array2 = dtItemsSizeCategorysDetails.Select(" ItemSizeID= " + ((TextEditorControlBase)cboRSizeReading).Value.ToString());
						for (int k = 0; k < array.Length; k++)
						{
							text = ((k != 0 || array.Length == 0) ? (text + "," + array[k]["ItemColorCategoryID"].ToString()) : array[k]["ItemColorCategoryID"].ToString());
						}
						for (int l = 0; l < array2.Length; l++)
						{
							text2 = ((l != 0 || array2.Length == 0) ? (text2 + "," + array2[l]["ItemSizeCategoryID"].ToString()) : array2[l]["ItemSizeCategoryID"].ToString());
						}
						DataView dataView = new DataView(dtItems);
						dataView.RowFilter = "ItemTypeID =2 And ItemColorCategoryID in ( " + text + ")  And  ItemSizeCategoryID in (" + text2 + ")";
						DataTable dataTable = dataView.ToTable();
						ValueList val = new ValueList();
						val.ValueListItems.Clear();
						for (int m = 0; m < dataTable.Rows.Count; m++)
						{
							val.ValueListItems.Add(dataTable.Rows[m]["ItemID"], dataTable.Rows[m]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].ValueList = (IValueList)(object)val;
					}
					else if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString()) == 2 && cboRColorDistance.SelectedIndex > -1 && cboRSizeDistance.SelectedIndex > -1)
					{
						string text3 = "-1";
						string text4 = "-1";
						DataRow[] array3 = dtItemsColorCategorysDetails.Select(" ColorID= " + ((TextEditorControlBase)cboRColorDistance).Value.ToString());
						DataRow[] array4 = dtItemsSizeCategorysDetails.Select(" ItemSizeID= " + ((TextEditorControlBase)cboRSizeDistance).Value.ToString());
						for (int n = 0; n < array3.Length; n++)
						{
							text3 = ((n != 0 || array3.Length == 0) ? (text3 + "," + array3[n]["ItemColorCategoryID"].ToString()) : array3[n]["ItemColorCategoryID"].ToString());
						}
						for (int num = 0; num < array4.Length; num++)
						{
							text4 = ((num != 0 || array4.Length == 0) ? (text4 + "," + array4[num]["ItemSizeCategoryID"].ToString()) : array4[num]["ItemSizeCategoryID"].ToString());
						}
						DataView dataView2 = new DataView(dtItems);
						dataView2.RowFilter = "ItemTypeID =2 And ItemColorCategoryID in ( " + text3 + ")  And  ItemSizeCategoryID in (" + text4 + ")";
						DataTable dataTable2 = dataView2.ToTable();
						ValueList val2 = new ValueList();
						val2.ValueListItems.Clear();
						for (int num2 = 0; num2 < dataTable2.Rows.Count; num2++)
						{
							val2.ValueListItems.Add(dataTable2.Rows[num2]["ItemID"], dataTable2.Rows[num2]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].ValueList = (IValueList)(object)val2;
					}
					else if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString()) == 3)
					{
						DataView dataView3 = new DataView(dtItems);
						dataView3.RowFilter = "ItemTypeID =2 " + (UserFourthClassification ? " And ItemFourthClassificationID=3 " : "");
						DataTable dataTable3 = dataView3.ToTable();
						ValueList val3 = new ValueList();
						val3.ValueListItems.Clear();
						for (int num3 = 0; num3 < dataTable3.Rows.Count; num3++)
						{
							val3.ValueListItems.Add(dataTable3.Rows[num3]["ItemID"], dataTable3.Rows[num3]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].ValueList = (IValueList)(object)val3;
					}
					else if ((int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString()) == 4 || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString()) == 5) && cboRColorDistance.SelectedIndex > -1 && cboRColorReading.SelectedIndex > -1 && cboRSizeDistance.SelectedIndex > -1)
					{
						string text5 = "-1";
						string text6 = "-1";
						DataTable dataTable4 = dtItemsColorCategorysDetails.DefaultView.ToTable(true, "ItemColorCategoryID");
						for (int num4 = 0; num4 < dataTable4.Rows.Count; num4++)
						{
							if (dtItemsColorCategorysDetails.Select(" ItemColorCategoryID= " + dataTable4.Rows[num4]["ItemColorCategoryID"].ToString() + " And ColorID= " + ((TextEditorControlBase)cboRColorDistance).Value.ToString()).Length != 0 && dtItemsColorCategorysDetails.Select(" ItemColorCategoryID= " + dataTable4.Rows[num4]["ItemColorCategoryID"].ToString() + " And ColorID= " + ((((TextEditorControlBase)cboRColorReading).Value == DBNull.Value) ? "" : ((TextEditorControlBase)cboRColorReading).Value.ToString())).Length != 0)
							{
								text5 = ((!(text5 == "-1")) ? (text5 + "," + dataTable4.Rows[num4]["ItemColorCategoryID"].ToString()) : dataTable4.Rows[num4]["ItemColorCategoryID"].ToString());
							}
						}
						DataRow[] array5 = dtItemsSizeCategorysDetails.Select(" ItemSizeID= " + ((TextEditorControlBase)cboRSizeDistance).Value.ToString());
						for (int num5 = 0; num5 < array5.Length; num5++)
						{
							text6 = ((num5 != 0 || array5.Length == 0) ? (text6 + "," + array5[num5]["ItemSizeCategoryID"].ToString()) : array5[num5]["ItemSizeCategoryID"].ToString());
						}
						DataView dataView4 = new DataView(dtItems);
						dataView4.RowFilter = "ItemTypeID =2  " + (UserFourthClassification ? (" And ItemFourthClassificationID= " + ((((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value.ToString() == "4") ? "1" : "2")) : "") + " And  ItemColorCategoryID in ( " + text5 + ")  And  ItemSizeCategoryID in (" + text6 + ")";
						DataTable dataTable5 = dataView4.ToTable();
						ValueList val4 = new ValueList();
						val4.ValueListItems.Clear();
						for (int num6 = 0; num6 < dataTable5.Rows.Count; num6++)
						{
							val4.ValueListItems.Add(dataTable5.Rows[num6]["ItemID"], dataTable5.Rows[num6]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].ValueList = (IValueList)(object)val4;
					}
					if (ClearLensesData)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RUnitPrice"].Value = 0;
					}
				}
			}
		}
		else
		{
			for (int num7 = 0; num7 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num7++)
			{
				if (((UltraGridBase)ULGData).Rows[num7].Cells["GlassesTypeID"].Value != DBNull.Value && int.Parse(((UltraGridBase)ULGData).Rows[num7].Cells["GlassesTypeID"].Value.ToString()) == 3)
				{
					for (int num8 = 0; num8 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[num7].ChildBands[0].Rows).Count; num8++)
					{
						dvItems = new DataView(dtItems);
						dvItems.RowFilter = "ItemTypeID =2 " + (UserFourthClassification ? " And ItemFourthClassificationID=3 " : "");
						DataTable dataTable6 = dvItems.ToTable();
						ValueList val5 = new ValueList();
						val5.ValueListItems.Clear();
						for (int num9 = 0; num9 < dataTable6.Rows.Count; num9++)
						{
							val5.ValueListItems.Add(dataTable6.Rows[num9]["ItemID"], dataTable6.Rows[num9]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[num7].ChildBands[0].Rows[num8].Cells["RLenseItemID"].ValueList = (IValueList)(object)val5;
					}
					continue;
				}
				for (int num10 = 0; num10 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[num7].ChildBands[0].Rows).Count; num10++)
				{
					dvItems = new DataView(dtItems);
					dvItems.RowFilter = "ItemTypeID =2 ";
					DataTable dataTable7 = dvItems.ToTable();
					ValueList val6 = new ValueList();
					val6.ValueListItems.Clear();
					for (int num11 = 0; num11 < dataTable7.Rows.Count; num11++)
					{
						val6.ValueListItems.Add(dataTable7.Rows[num11]["ItemID"], dataTable7.Rows[num11]["Name"].ToString());
					}
					((UltraGridBase)ULGData).Rows[num7].ChildBands[0].Rows[num10].Cells["RLenseItemID"].ValueList = (IValueList)(object)val6;
				}
			}
		}
		if ((cboLColorDistance.SelectedIndex > -1 && cboLSizeDistance.SelectedIndex > -1) || (cboLColorReading.SelectedIndex > -1 && cboLSizeReading.SelectedIndex > -1))
		{
			for (int num12 = 0; num12 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num12++)
			{
				if (((UltraGridBase)ULGData).Rows[num12].Cells["GlassesTypeID"].Value == DBNull.Value)
				{
					continue;
				}
				for (int num13 = 0; num13 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows).Count; num13++)
				{
					((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows[num13].Cells["LLenseItemID"].ValueList = (IValueList)(object)getLeftLenseValueList(((UltraGridBase)ULGData).Rows[num12]);
					if (ClearLensesData)
					{
						((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows[num13].Cells["LLenseItemID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows[num13].Cells["LColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows[num13].Cells["LItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).Rows[num12].ChildBands[0].Rows[num13].Cells["LUnitPrice"].Value = 0;
					}
				}
			}
		}
		else
		{
			for (int num14 = 0; num14 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num14++)
			{
				if (((UltraGridBase)ULGData).Rows[num14].Cells["GlassesTypeID"].Value != DBNull.Value && int.Parse(((UltraGridBase)ULGData).Rows[num14].Cells["GlassesTypeID"].Value.ToString()) == 3)
				{
					for (int num15 = 0; num15 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[num14].ChildBands[0].Rows).Count; num15++)
					{
						dvItems = new DataView(dtItems);
						dvItems.RowFilter = "ItemTypeID =2 " + (UserFourthClassification ? " And ItemFourthClassificationID=3 " : "");
						DataTable dataTable8 = dvItems.ToTable();
						ValueList val7 = new ValueList();
						val7.ValueListItems.Clear();
						for (int num16 = 0; num16 < dataTable8.Rows.Count; num16++)
						{
							val7.ValueListItems.Add(dataTable8.Rows[num16]["ItemID"], dataTable8.Rows[num16]["Name"].ToString());
						}
						((UltraGridBase)ULGData).Rows[num14].ChildBands[0].Rows[num15].Cells["LLenseItemID"].ValueList = (IValueList)(object)val7;
					}
					continue;
				}
				for (int num17 = 0; num17 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[num14].ChildBands[0].Rows).Count; num17++)
				{
					dvItems = new DataView(dtItems);
					dvItems.RowFilter = "ItemTypeID =2 ";
					DataTable dataTable9 = dvItems.ToTable();
					ValueList val8 = new ValueList();
					val8.ValueListItems.Clear();
					for (int num18 = 0; num18 < dataTable9.Rows.Count; num18++)
					{
						val8.ValueListItems.Add(dataTable9.Rows[num18]["ItemID"], dataTable9.Rows[num18]["Name"].ToString());
					}
					((UltraGridBase)ULGData).Rows[num14].ChildBands[0].Rows[num17].Cells["LLenseItemID"].ValueList = (IValueList)(object)val8;
				}
			}
		}
		if (ClearLensesData)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateGoss();
		}
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

	private void CalculateServiceRow(UltraGridRow Row)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.Cells["ServiceItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.Cells["ServiceItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
		}
		if (cboTax.SelectedIndex > -1)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
	}

	private void CalculateServiceRowActualUnitSalesPrice(UltraGridRow Row)
	{
		if (decimal.Parse(Row.Cells["Qty"].Value.ToString()) > 0m)
		{
			Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString())) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
		}
		else
		{
			Row.Cells["ActualUnitSalesPrice"].Value = 0;
		}
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.Cells["FrameItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.Cells["FrameItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; i++)
			{
				if (i == 0)
				{
					if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.ChildBands[0].Rows[i].Cells["RLenseItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.ChildBands[0].Rows[i].Cells["RLenseItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
					{
						Row.ChildBands[0].Rows[i].Cells["RDisCount"].Value = 0;
					}
					else
					{
						Row.ChildBands[0].Rows[i].Cells["RDisCount"].Value = decimal.Parse(Row.ChildBands[0].Rows[i].Cells["RUnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
					}
					if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.ChildBands[0].Rows[i].Cells["LLenseItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.ChildBands[0].Rows[i].Cells["LLenseItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
					{
						Row.ChildBands[0].Rows[i].Cells["LDisCount"].Value = 0;
					}
					else
					{
						Row.ChildBands[0].Rows[i].Cells["LDisCount"].Value = decimal.Parse(Row.ChildBands[0].Rows[i].Cells["LUnitPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
					}
				}
			}
		}
		if (cboTax.SelectedIndex > -1)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["UnitPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
			for (int j = 0; j < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; j++)
			{
				if (j == 0)
				{
					Row.ChildBands[0].Rows[j].Cells["RTaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.ChildBands[0].Rows[j].Cells["RUnitPrice"].Value.ToString()) - decimal.Parse(Row.ChildBands[0].Rows[j].Cells["RDiscount"].Value.ToString()));
					Row.ChildBands[0].Rows[j].Cells["LTaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.ChildBands[0].Rows[j].Cells["LUnitPrice"].Value.ToString()) - decimal.Parse(Row.ChildBands[0].Rows[j].Cells["LDiscount"].Value.ToString()));
				}
			}
			return;
		}
		Row.Cells["TaxValue"].Value = 0;
		for (int k = 0; k < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; k++)
		{
			if (k == 0)
			{
				Row.ChildBands[0].Rows[k].Cells["RTaxValue"].Value = 0;
				Row.ChildBands[0].Rows[k].Cells["LTaxValue"].Value = 0;
			}
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			num += CalculateLensesPrices(((UltraGridBase)ULGData).Rows[i]);
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse((num + CalculateOrderServices()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public decimal CalculateLensesPrices(UltraGridRow Row)
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; i++)
		{
			if (i == 0)
			{
				if (Row.ChildBands[0].Rows[i].Cells["RUnitPrice"].Value != DBNull.Value)
				{
					result += decimal.Parse(Row.ChildBands[0].Rows[i].Cells["RUnitPrice"].Value.ToString());
				}
				if (Row.ChildBands[0].Rows[i].Cells["LUnitPrice"].Value != DBNull.Value)
				{
					result += decimal.Parse(Row.ChildBands[0].Rows[i].Cells["LUnitPrice"].Value.ToString());
				}
			}
		}
		return result;
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = decimal.Parse(Row.Cells["UnitPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
		for (int i = 0; i < ((DisposableObjectCollectionBase)Row.ChildBands[0].Rows).Count; i++)
		{
			if (i == 0)
			{
				Row.ChildBands[0].Rows[i].Cells["RActualUnitSalesPrice"].Value = decimal.Parse(Row.ChildBands[0].Rows[i].Cells["RUnitPrice"].Value.ToString()) + decimal.Parse(Row.ChildBands[0].Rows[i].Cells["RTaxValue"].Value.ToString()) - decimal.Parse(Row.ChildBands[0].Rows[i].Cells["RDiscount"].Value.ToString());
				Row.ChildBands[0].Rows[i].Cells["LActualUnitSalesPrice"].Value = decimal.Parse(Row.ChildBands[0].Rows[i].Cells["LUnitPrice"].Value.ToString()) + decimal.Parse(Row.ChildBands[0].Rows[i].Cells["LTaxValue"].Value.ToString()) - decimal.Parse(Row.ChildBands[0].Rows[i].Cells["LDiscount"].Value.ToString());
			}
		}
	}

	public decimal CalculateOrderServices()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; i++)
		{
			result += decimal.Parse(((UltraGridBase)ULGDataServices).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		return result;
	}

	private void CalculateTotalContractAmount()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataContractsDetails).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGDataContractsDetails).Rows[i].Cells["Price"].Value.ToString());
		}
		((Control)(object)txtTotalContractAmount).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = LabOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.POSClientsSearch("-1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void cboMobile_ValueChanged(object sender, EventArgs e)
	{
		if (cboMobile.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboMobile).Value;
			return;
		}
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Expected O, but got Unknown
		((Control)(object)txtClientLoadAmount).Text = "0";
		if (cboClient.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = ((TextEditorControlBase)cboClient).Value;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtClientDiscount).Text == "" || ((Control)(object)txtClientDiscount).Text == ".") ? "0" : ((Control)(object)txtClientDiscount).Text) > 0m) ? ((Control)(object)txtClientDiscount).Text : dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			if (Updating || Adding)
			{
				ClientsGlassesHistoryID = ((dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["ClientsGlassesHistoryID"] != DBNull.Value) ? int.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["ClientsGlassesHistoryID"].ToString()) : 0);
				FillClientGlassesHistory(ClientsGlassesHistoryID);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				FilterLeftRightLenses(ClearLensesData: true);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			if (dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					if (((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value != DBNull.Value)
					{
						DataRow dataRow = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value.ToString())[0];
						((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(dataRow["DiscountPercentage"].ToString()) / 100m;
					}
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
					{
						if (j == 0)
						{
							if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value != DBNull.Value)
							{
								DataRow dataRow2 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value.ToString())[0];
								((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RUnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
							}
							if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value != DBNull.Value)
							{
								DataRow dataRow3 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value.ToString())[0];
								((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LUnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
							}
						}
					}
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; k++)
				{
					DataRow dataRow4 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceItemID"].Value.ToString())[0];
					((UltraGridBase)ULGDataServices).Rows[k].Cells["UnitPrice"].Value = decimal.Parse(dataRow4["Price"].ToString()) - decimal.Parse(dataRow4["Price"].ToString()) * decimal.Parse(dataRow4["DiscountPercentage"].ToString()) / 100m;
					((UltraGridBase)ULGDataServices).Rows[k].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataServices).Rows[k].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataServices).Rows[k].Cells["Qty"].Value.ToString());
					CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[k]);
				}
				CalculateGoss();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			else
			{
				dtItemPrices = null;
			}
		}
		else
		{
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			cboMobile.SelectedIndex = -1;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			ClearClientGlassesHistory();
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.POSClientsSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		bool flag = cboClient.SelectedIndex == -1;
		frmPOSClients frmPOSClients2 = new frmPOSClients((cboClient.SelectedIndex == -1) ? (-1m) : decimal.Parse(((TextEditorControlBase)cboClient).Value.ToString()));
		frmPOSClients2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmPOSClients2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Clients");
		frmPOSClients2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.POS.MasterData.frmPOSClients'")[0];
		frmPOSClients2.ShowDialog();
		if (flag && frmPOSClients2.ClientID != 0m)
		{
			frmPOSClientsGlassesHistory frmPOSClientsGlassesHistory2 = new frmPOSClientsGlassesHistory(islabmodule: true, frmPOSClients2.ClientID, frmPOSClients2.ClientName, -1m);
			frmPOSClientsGlassesHistory2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmPOSClientsGlassesHistory2.lblTitle).Text = (GlobalVariables.IsArabic ? "كشف النظر" : "Glasses History");
			frmPOSClientsGlassesHistory2.Tag = base.Tag;
			frmPOSClientsGlassesHistory2.CanAdd = CanAdd;
			frmPOSClientsGlassesHistory2.CanUpdate = CanUpdate;
			frmPOSClientsGlassesHistory2.CanDelete = CanDelete;
			frmPOSClientsGlassesHistory2.CanDiscount = CanDiscount;
			frmPOSClientsGlassesHistory2.CanSearching = CanSearching;
			frmPOSClientsGlassesHistory2.CanExport = CanExport;
			frmPOSClientsGlassesHistory2.CanPrint = CanPrint;
			frmPOSClientsGlassesHistory2.CanPrintReport = CanPrintReport;
			frmPOSClientsGlassesHistory2.CanViewReport = CanViewReport;
			frmPOSClientsGlassesHistory2.CanMinimunCharge = CanMinimunCharge;
			frmPOSClientsGlassesHistory2.ShowDialog();
			if (frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID > 0m || frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID < -1m)
			{
				ClientsGlassesHistoryID = frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID;
				FillClientGlassesHistory(frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID);
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			FilterLeftRightLenses(ClearLensesData: true);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		if (frmPOSClients2.ClientID != 0m)
		{
			dtClients = Clients.FillComboWithGlassesHistory(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
			GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
			((TextEditorControlBase)cboClient).Value = frmPOSClients2.ClientID;
		}
	}

	private void btnAddHistory_Click(object sender, EventArgs e)
	{
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		if (cboClient.SelectedIndex > -1)
		{
			int num = int.Parse(((TextEditorControlBase)cboClient).Value.ToString());
			frmPOSClientsGlassesHistory frmPOSClientsGlassesHistory2 = new frmPOSClientsGlassesHistory(islabmodule: true, num, ((Control)(object)cboClient).Text, -1m);
			frmPOSClientsGlassesHistory2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmPOSClientsGlassesHistory2.lblTitle).Text = (GlobalVariables.IsArabic ? "كشف النظر" : "Glasses History");
			frmPOSClientsGlassesHistory2.Tag = base.Tag;
			frmPOSClientsGlassesHistory2.CanAdd = CanAdd;
			frmPOSClientsGlassesHistory2.CanUpdate = CanUpdate;
			frmPOSClientsGlassesHistory2.CanDelete = CanDelete;
			frmPOSClientsGlassesHistory2.CanDiscount = CanDiscount;
			frmPOSClientsGlassesHistory2.CanSearching = CanSearching;
			frmPOSClientsGlassesHistory2.CanExport = CanExport;
			frmPOSClientsGlassesHistory2.CanPrint = CanPrint;
			frmPOSClientsGlassesHistory2.CanPrintReport = CanPrintReport;
			frmPOSClientsGlassesHistory2.CanViewReport = CanViewReport;
			frmPOSClientsGlassesHistory2.CanMinimunCharge = CanMinimunCharge;
			frmPOSClientsGlassesHistory2.ShowDialog();
			if (frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID > 0m || frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID < -1m)
			{
				ClientsGlassesHistoryID = frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID;
				FillClientGlassesHistory(frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID);
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			FilterLeftRightLenses(ClearLensesData: true);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			object value = ((TextEditorControlBase)cboDoctor).Value;
			dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
			((TextEditorControlBase)cboDoctor).Value = value;
		}
	}

	private void FillClientGlassesHistory(decimal ClientsGlassesHistoryID)
	{
		DataTable dataTable = ClientsGlassesHistory.Select(ClientsGlassesHistoryID.ToString(), "-1", "0", IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			((TextEditorControlBase)cboRColorDistance).Value = dataRow["RColorIDDistance"];
			((TextEditorControlBase)cboRSizeDistance).Value = dataRow["RItemSizeIDDistance"];
			((Control)(object)txtRAxisDistance).Text = dataRow["RAxDistance"].ToString();
			((TextEditorControlBase)cboRColorReading).Value = dataRow["RColorIDReading"];
			((TextEditorControlBase)cboRSizeReading).Value = dataRow["RItemSizeIDReading"];
			((Control)(object)txtRAxisReading).Text = dataRow["RAxReading"].ToString();
			((Control)(object)txtRAdd).Text = dataRow["RAdd"].ToString();
			((TextEditorControlBase)cboLColorDistance).Value = dataRow["LColorIDDistance"];
			((TextEditorControlBase)cboLSizeDistance).Value = dataRow["LItemSizeIDDistance"];
			((Control)(object)txtLAxisDistance).Text = dataRow["LAxDistance"].ToString();
			((TextEditorControlBase)cboLColorReading).Value = dataRow["LColorIDReading"];
			((TextEditorControlBase)cboLSizeReading).Value = dataRow["LItemSizeIDReading"];
			((Control)(object)txtLAxisReading).Text = dataRow["LAxReading"].ToString();
			((Control)(object)txtLAdd).Text = dataRow["LAdd"].ToString();
			((Control)(object)txtIPDDistance).Text = dataRow["IPDDistance"].ToString();
			((Control)(object)txtIPDReading).Text = dataRow["IPDReading"].ToString();
			((TextEditorControlBase)cboDoctor).Value = dataRow["DoctorID"];
			dtpGlassesHistoryDate.Value = (DateTime)dataRow["Date"];
		}
		else
		{
			ClearClientGlassesHistory();
		}
	}

	private void ClearClientGlassesHistory()
	{
		cboRColorDistance.SelectedIndex = -1;
		cboRSizeDistance.SelectedIndex = -1;
		((TextEditorControlBase)txtRAxisDistance).Clear();
		cboRColorReading.SelectedIndex = -1;
		cboRSizeReading.SelectedIndex = -1;
		((TextEditorControlBase)txtRAxisReading).Clear();
		((TextEditorControlBase)txtRAdd).Clear();
		cboLColorDistance.SelectedIndex = -1;
		cboLSizeDistance.SelectedIndex = -1;
		((TextEditorControlBase)txtLAxisDistance).Clear();
		cboLColorReading.SelectedIndex = -1;
		cboLSizeReading.SelectedIndex = -1;
		((TextEditorControlBase)txtLAxisReading).Clear();
		((TextEditorControlBase)txtLAdd).Clear();
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		if (cboTax.SelectedIndex > -1)
		{
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[j]);
			}
		}
	}

	public void OpenChangeDiscountForm()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse(((Control)(object)txtGrossValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscBeforeTaxValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "0") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[j]);
			}
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = (decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString();
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[j]);
			}
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtClientLoadAmount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtClientLoadAmount).ValueChanged -= txtClientLoadAmount_ValueChanged;
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtClientLoadAmount).ValueChanged += txtClientLoadAmount_ValueChanged;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
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
		}
		else
		{
			if (e.KeyCode != Keys.F9 || ULGData.ActiveCell == null || ((UltraGridBase)ULGData).ActiveRow == null)
			{
				return;
			}
			if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
			{
				if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FrameItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID") && ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value != DBNull.Value)
				{
					DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString(), (cboLabs.SelectedIndex <= -1) ? ((dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "-1" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString()) : ((dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString()), "-1", (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
					frmImageViewer2.WindowState = FormWindowState.Normal;
					frmImageViewer2.ShowDialog();
					if (Adding && frmImageViewer2.Saved)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = (frmImageViewer2.ColorID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value : frmImageViewer2.ColorID);
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = (frmImageViewer2.ItemSizeID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value : frmImageViewer2.ItemSizeID);
						((UltraGridBase)ULGData).ActiveRow.Cells["LabStoreID"].Value = (frmImageViewer2.StoreID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["LabStoreID"].Value : frmImageViewer2.StoreID);
					}
				}
			}
			else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
			{
				if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RLenseItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RColorID") && ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value != DBNull.Value)
				{
					DataTable dataTable2 = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					frmImageViewer frmImageViewer3 = new frmImageViewer((dataTable2.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable2.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable2.Rows[0]["ItemNameAr"].ToString() : dataTable2.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["RLenseItemID"].Value.ToString(), (cboLabs.SelectedIndex <= -1) ? ((dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "-1" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString()) : ((dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString()), "-1", (((UltraGridBase)ULGData).ActiveRow.Cells["RColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["RColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["RItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["RItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
					frmImageViewer3.WindowState = FormWindowState.Normal;
					frmImageViewer3.ShowDialog();
					if (Adding && frmImageViewer3.Saved)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["RColorID"].Value = (frmImageViewer3.ColorID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["RColorID"].Value : frmImageViewer3.ColorID);
						((UltraGridBase)ULGData).ActiveRow.Cells["RItemSizeID"].Value = (frmImageViewer3.ItemSizeID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["RItemSizeID"].Value : frmImageViewer3.ItemSizeID);
						((UltraGridBase)ULGData).ActiveRow.Cells["RLabStoreID"].Value = (frmImageViewer3.StoreID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["RLabStoreID"].Value : frmImageViewer3.StoreID);
					}
				}
				else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LLenseItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LColorID") && ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value != DBNull.Value)
				{
					DataTable dataTable3 = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					frmImageViewer frmImageViewer4 = new frmImageViewer((dataTable3.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable3.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable3.Rows[0]["ItemNameAr"].ToString() : dataTable3.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["LLenseItemID"].Value.ToString(), (cboLabs.SelectedIndex <= -1) ? ((dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "-1" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString()) : ((dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"] == DBNull.Value) ? "Null" : dtLabs.Select(" LabID= " + ((TextEditorControlBase)cboLabs).Value.ToString())[0]["StoreID"].ToString()), "-1", (((UltraGridBase)ULGData).ActiveRow.Cells["LColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["LColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["LItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["LItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
					frmImageViewer4.WindowState = FormWindowState.Normal;
					frmImageViewer4.ShowDialog();
					if (Adding && frmImageViewer4.Saved)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["LColorID"].Value = (frmImageViewer4.ColorID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["LColorID"].Value : frmImageViewer4.ColorID);
						((UltraGridBase)ULGData).ActiveRow.Cells["LItemSizeID"].Value = (frmImageViewer4.ItemSizeID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["LItemSizeID"].Value : frmImageViewer4.ItemSizeID);
						((UltraGridBase)ULGData).ActiveRow.Cells["LLabStoreID"].Value = (frmImageViewer4.StoreID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["LLabStoreID"].Value : frmImageViewer4.StoreID);
					}
				}
			}
			e.Handled = true;
		}
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".")
		{
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = "0";
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		}
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void btnOrderPayments_Click(object sender, EventArgs e)
	{
		if (Updating)
		{
			frmLnsLabOrdersPayments frmLnsLabOrdersPayments2 = new frmLnsLabOrdersPayments(int.Parse(drMaster["LabOrderID"].ToString()), decimal.Parse(((Control)(object)txtRestAmount).Text.ToString()));
			frmLnsLabOrdersPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmLnsLabOrdersPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد الفواتير" : "Invoices Payments");
			frmLnsLabOrdersPayments2.ShowDialog();
			dtLabOrdersPayments = LabOrdersPayments.SelectByLabOrderID(drMaster["LabOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataTable dataTable = dtLabOrdersPayments;
			object obj = dtLabOrdersPayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((UltraGridBase)ULGDataPayments).DataSource = dtLabOrdersPayments;
			InitGrid();
		}
	}

	private void ULGDataPayments_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Selected = true;
	}

	private void ULGDataServices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataServices_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
		if (ULGDataServices.ActiveCell != null && ULGDataServices.ActiveCell.Value == DBNull.Value && (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" || ((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice"))
		{
			ULGDataServices.ActiveCell.Value = 0;
			CalculateServiceRow(((UltraGridBase)ULGDataServices).ActiveRow);
		}
		if (ULGDataServices.ActiveCell != null && (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" || ((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice"))
		{
			((UltraGridBase)ULGDataServices).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataServices).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataServices).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateGoss();
			CalculateServiceRow(((UltraGridBase)ULGDataServices).ActiveRow);
		}
		ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
	}

	private void ULGDataServices_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGoss();
	}

	private void ULGDataServices_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataServices_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		ULGDataServices.CellListSelect -= new CellEventHandler(ULGDataServices_CellListSelect);
		ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataServices).UpdateData();
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow dataRow = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataServices).ActiveRow.Cells["ServiceItemID"].Value.ToString())[0];
				((UltraGridBase)ULGDataServices).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(dataRow["DiscountPercentage"].ToString()) / 100m;
				((UltraGridBase)ULGDataServices).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataServices).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataServices).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateServiceRow(e.Cell.Row);
			}
			if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
			{
				DataRow dataRow2 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataServices).ActiveRow.Cells["ServiceItemID"].Value.ToString())[0];
				((UltraGridBase)ULGDataServices).ActiveRow.Cells["LabUnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
			}
		}
		ULGDataServices.CellListSelect += new CellEventHandler(ULGDataServices_CellListSelect);
		ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
	}

	private void ULGDataServices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (ULGDataServices.ActiveCell != null && (((KeyedSubObjectBase)ULGDataServices.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGDataServices.ActiveCell.Column).Key == "LabUnitPrice"))
		{
			((GridItemBase)((UltraGridBase)ULGDataServices).ActiveRow).Selected = true;
		}
		else if (ULGDataServices.ActiveCell != null && ((UltraGridBase)ULGDataServices).ActiveRow != null && ((KeyedSubObjectBase)ULGDataServices.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGDataServices).ActiveRow.Cells["ServiceItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGDataServices).ActiveRow.Cells["ServiceItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataServices).ActiveRow).Selected = true;
		}
	}

	private void cboLabs_ValueChanged(object sender, EventArgs e)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Expected O, but got Unknown
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Expected O, but got Unknown
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Expected O, but got Unknown
		if (cboLabs.SelectedIndex > -1)
		{
			DataTable dataTable = LabsBranchesPriceTypes.SelectByLabBranchID(GlobalVariables.CurrentBranchID, ((TextEditorControlBase)cboLabs).Value.ToString(), IsFromServer: false);
			if (dataTable.Rows.Count <= 0 || dataTable.Rows[0]["PriceTypeID"] == DBNull.Value)
			{
				return;
			}
			dtLabsItemsPrices = ItemsPrices.GetPriceWithItemDiscount(dataTable.Rows[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value != DBNull.Value)
				{
					DataRow dataRow = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["FrameItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).Rows[i].Cells["LabUnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(dataRow["DiscountPercentage"].ToString()) / 100m;
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					if (j == 0)
					{
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value != DBNull.Value)
						{
							DataRow dataRow2 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLenseItemID"].Value.ToString())[0];
							((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["RLabUnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
						}
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value != DBNull.Value)
						{
							DataRow dataRow3 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLenseItemID"].Value.ToString())[0];
							((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LLabUnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
						}
					}
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceItemID"].Value != DBNull.Value)
				{
					DataRow dataRow4 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceItemID"].Value.ToString())[0];
					((UltraGridBase)ULGDataServices).Rows[k].Cells["LabUnitPrice"].Value = decimal.Parse(dataRow4["Price"].ToString()) - decimal.Parse(dataRow4["Price"].ToString()) * decimal.Parse(dataRow4["DiscountPercentage"].ToString()) / 100m;
				}
			}
			ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
		}
		else
		{
			dtLabsItemsPrices = null;
		}
	}

	private void ULGDataDestroyed_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (ULGDataDestroyed.ActiveCell != null && ((KeyedSubObjectBase)ULGDataDestroyed.ActiveCell.Column).Key != "LabUnitPrice" && ((KeyedSubObjectBase)ULGDataDestroyed.ActiveCell.Column).Key != "UnitPrice")
		{
			((GridItemBase)((UltraGridBase)ULGDataDestroyed).ActiveRow).Selected = true;
		}
	}

	private void ULGDataDestroyed_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		ULGDataDestroyed.AfterCellUpdate -= new CellEventHandler(ULGDataDestroyed_AfterCellUpdate);
		if (ULGDataDestroyed.ActiveCell != null && ULGDataDestroyed.ActiveCell.Value == DBNull.Value && (((KeyedSubObjectBase)e.Cell.Column).Key == "LabUnitPrice" || ((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice"))
		{
			ULGDataDestroyed.ActiveCell.Value = 0;
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice")
		{
			((UltraGridBase)ULGDataDestroyed).UpdateData();
			object obj = dtLabOrdersDestroyedItems.Compute(" Sum(UnitPrice) ", "OnCostOfID=3");
			((Control)(object)txtDestroyed).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) + decimal.Parse((((Control)(object)txtDestroyed).Text == "" || ((Control)(object)txtDestroyed).Text == ".") ? "0" : ((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		ULGDataDestroyed.AfterCellUpdate += new CellEventHandler(ULGDataDestroyed_AfterCellUpdate);
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
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Expected O, but got Unknown
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string value = arrayList[1].ToString();
		string value2 = arrayList[2].ToString();
		string text2 = arrayList[3].ToString();
		string text3 = arrayList[4].ToString();
		if (dtItems.Select(" ItemTypeID =1 And ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["LabOrderDetailID"].Value = ++newID;
			UltraGridRow val = ((UltraGridBase)ULGData).DisplayLayout.Bands[1].AddNew();
			val.Cells["Primarykey"].Value = ++newID;
			val.Cells["RNotes"].Value = "-";
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Primarykey"].Value = ++newID;
		}
		((UltraGridBase)ULGData).UpdateData();
		((UltraGridBase)ULGData).DataBind();
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = value2;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow dataRow2 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			}
			if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
			{
				DataRow dataRow3 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["FrameItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["LabUnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
			}
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["FrameItemID"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
			DataRow dataRow4 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["FrameItemID"].Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["ColorID"].Value = value;
			((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["ItemSizeID"].Value = value2;
			((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["UnitID"].Value = dataRow4["UnitID"];
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				DataRow dataRow5 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["FrameItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow5["Price"].ToString()) - decimal.Parse(dataRow5["Price"].ToString()) * decimal.Parse(dataRow5["DiscountPercentage"].ToString()) / 100m;
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
			}
			if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && cboLabs.SelectedIndex > -1)
			{
				DataRow dataRow6 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["FrameItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabUnitPrice"].Value = decimal.Parse(dataRow6["Price"].ToString()) - decimal.Parse(dataRow6["Price"].ToString()) * decimal.Parse(dataRow6["DiscountPercentage"].ToString()) / 100m;
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnCanceled_Click(object sender, EventArgs e)
	{
		if (drMaster != null)
		{
			frmSelectDate frmSelectDate2 = new frmSelectDate(GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Canceletion Date");
			frmSelectDate2.WindowState = FormWindowState.Normal;
			frmSelectDate2.ShowDialog();
			if (frmSelectDate2.DateTimeValue.HasValue)
			{
				dtpCancelDate.DateTime = frmSelectDate2.DateTimeValue.Value;
				((UltraToggleEditorBase)chkIsCanceled).Checked = true;
				Main.ExecuteNonQuery("Update Lns_LabOrders Set IsCanceled=1,CanceledDate='" + dtpCancelDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "' Where LabOrderID=" + drMaster["LabOrderID"].ToString());
				ItemsTransactions.ManageInThread();
			}
		}
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboVisaType;
		bool enabled = (((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked);
		((Control)(object)obj).Enabled = enabled;
	}

	private void cboSponsor_ValueChanged(object sender, EventArgs e)
	{
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Expected O, but got Unknown
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Expected O, but got Unknown
		((DataTable)((UltraGridBase)ULGDataContractsDetails).DataSource).Rows.Clear();
		((Control)(object)txtTotalContractAmount).Text = "0";
		if (cboSponsor.SelectedIndex > -1)
		{
			dtSponsorContract = LabContractsClients.GetLastContract(((TextEditorControlBase)cboSponsor).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
			string text;
			if (dtSponsorContract.Rows.Count == 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "هذا الكفيل ليسه لديه عقود مفتوحه في هذه الفتره" : "This Sponsor Has No Valid Contract For This Period");
				((UltraTabControlBase)UTCDetails).Tabs["ContractsDetails"].Visible = false;
				UltraTextEditor obj = txtClientDiscount;
				text = (((Control)(object)txtCompanyDiscount).Text = "0");
				((Control)(object)obj).Text = text;
				CalculateTotalContractAmount();
				((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
				return;
			}
			((UltraTabControlBase)UTCDetails).Tabs["ContractsDetails"].Visible = dtSponsorContract.Rows[0]["HasDetails"].ToString() == "1";
			LabContractID = dtSponsorContract.Rows[0]["LabContractID"].ToString();
			((Control)(object)txtCompanyDiscount).Text = decimal.Parse(dtSponsorContract.Rows[0]["CompanyDiscount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			UltraTextEditor obj2 = txtClientDiscount;
			text = (((Control)(object)txtDiscBeforeTaxRatio).Text = dtSponsorContract.Rows[0]["ClientDiscount"].ToString());
			((Control)(object)obj2).Text = decimal.Parse(text).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; j++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[j]);
			}
			((Control)(object)txtTaxTotalValue).Text = ((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0");
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			UltraTextEditor obj3 = txtClientDiscount;
			UltraTextEditor obj4 = txtCompanyDiscount;
			UltraTextEditor obj5 = txtTotalContractAmount;
			string text4 = (((Control)(object)txtDiscBeforeTaxRatio).Text = "0");
			string text6 = (((Control)(object)obj5).Text = text4);
			string text = (((Control)(object)obj4).Text = text6);
			((Control)(object)obj3).Text = text;
			((DataTable)((UltraGridBase)ULGDataContractsDetails).DataSource).Rows.Clear();
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[k]);
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; l++)
			{
				CalculateServiceRow(((UltraGridBase)ULGDataServices).Rows[l]);
			}
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtTotalContractAmount).Text == "" || ((Control)(object)txtTotalContractAmount).Text == ".") ? "0" : ((Control)(object)txtTotalContractAmount).Text) + decimal.Parse((((Control)(object)txtClientLoadAmount).Text == "" || ((Control)(object)txtClientLoadAmount).Text == ".") ? "0" : ((Control)(object)txtClientLoadAmount).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDestroyed).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void ULGDataContractsDetails_AfterCellUpdate(object sender, CellEventArgs e)
	{
		CalculateTotalContractAmount();
	}

	private void ULGDataContractsDetails_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (ULGDataContractsDetails.ActiveCell != null && ((((KeyedSubObjectBase)ULGDataContractsDetails.ActiveCell.Column).Key == "Price" && !bool.Parse(ULGDataContractsDetails.ActiveCell.Row.Cells["CanEditPrice"].Value.ToString())) || ((KeyedSubObjectBase)ULGDataContractsDetails.ActiveCell.Column).Key == "LabContractDetailDescription"))
			{
				((GridItemBase)((UltraGridBase)ULGDataContractsDetails).ActiveRow).Selected = true;
			}
		}
		else if (((UltraGridBase)ULGDataContractsDetails).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataContractsDetails).ActiveRow).Selected = true;
		}
	}

	private void ULGDataContractsDetails_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotalContractAmount();
	}

	private void btnSelectDetails_Click(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || !(LabContractID != ""))
		{
			return;
		}
		DataTable dataTable = SearchFunctions.LabContractDetailsReport("," + LabContractID + ",", 0);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dtLabOrdersContractsDetails.Select("LabContractDetailID = " + dataTable.Rows[i]["LabContractDetailID"].ToString()).Length == 0)
			{
				DataRow dataRow = dtLabOrdersContractsDetails.NewRow();
				dataRow["LabOrderContractDetailID"] = "-1";
				dataRow["LabContractDetailDescription"] = dataTable.Rows[i]["LabContractDetailDescription"];
				dataRow["LabContractDetailID"] = dataTable.Rows[i]["LabContractDetailID"];
				dataRow["Price"] = dataTable.Rows[i]["Price"];
				dataRow["CanEditPrice"] = dataTable.Rows[i]["CanEditPrice"];
				dataRow["Deleted"] = false;
				dtLabOrdersContractsDetails.Rows.Add(dataRow);
			}
		}
		((UltraGridBase)ULGDataContractsDetails).UpdateData();
		CalculateTotalContractAmount();
	}

	private void btnSponsorSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LabContractClientsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSponsor).Value = num;
		}
	}

	private void btnRTranspose_Click(object sender, EventArgs e)
	{
		if (!(ClientsGlassesHistoryID != 0m))
		{
			return;
		}
		bool flag = Transpose(cboRColorDistance, cboRSizeDistance, txtRAxisDistance, IsReading: false);
		bool flag2 = Transpose(cboRColorReading, cboRSizeReading, txtRAxisReading, IsReading: true);
		if (flag || flag2)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				ClientsGlassesHistory.Update(ClientsGlassesHistoryID.ToString(), (cboRColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorReading).Value.ToString(), (cboRSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeReading).Value.ToString(), (((Control)(object)txtRAxisReading).Text == "") ? "Null" : ((Control)(object)txtRAxisReading).Text, (cboRColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorDistance).Value.ToString(), (cboRSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeDistance).Value.ToString(), (((Control)(object)txtRAxisDistance).Text == "") ? "Null" : ((Control)(object)txtRAxisDistance).Text, "1", GlobalVariables.UserID, IsFromServer: false);
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			}
		}
	}

	private void btnLTranspose_Click(object sender, EventArgs e)
	{
		if (!(ClientsGlassesHistoryID != 0m))
		{
			return;
		}
		bool flag = Transpose(cboLColorDistance, cboLSizeDistance, txtLAxisDistance, IsReading: false);
		bool flag2 = Transpose(cboLColorReading, cboLSizeReading, txtLAxisReading, IsReading: true);
		if (flag || flag2)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				ClientsGlassesHistory.Update(ClientsGlassesHistoryID.ToString(), (cboLColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorReading).Value.ToString(), (cboLSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeReading).Value.ToString(), (((Control)(object)txtLAxisReading).Text == "") ? "Null" : ((Control)(object)txtLAxisReading).Text, (cboLColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorDistance).Value.ToString(), (cboLSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeDistance).Value.ToString(), (((Control)(object)txtLAxisDistance).Text == "") ? "Null" : ((Control)(object)txtLAxisDistance).Text, "0", GlobalVariables.UserID, IsFromServer: false);
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			}
		}
	}

	private bool Transpose(UltraComboEditor cboColor, UltraComboEditor cboSize, UltraTextEditor txtAxis, bool IsReading)
	{
		string text = "";
		text = ((!IsReading) ? (GlobalVariables.IsArabic ? "المسافات" : "Distance") : (GlobalVariables.IsArabic ? "القراءه" : "Reading"));
		decimal result;
		bool flag = decimal.TryParse(((Control)(object)cboColor).Text.ToString(), out result);
		decimal result2;
		bool flag2 = decimal.TryParse(((Control)(object)cboSize).Text.ToString(), out result2);
		decimal result3;
		bool flag3 = decimal.TryParse(((Control)(object)txtAxis).Text.ToString(), out result3);
		if (flag && flag2 && flag3)
		{
			decimal num = result + result2;
			decimal num2 = result2 * -1m;
			DataRow[] array = dtColors.Select("ColorName = '" + num + "' or   ColorName = '+" + num + "'");
			DataRow[] array2 = dtSizes.Select("ItemSizeName = '" + num2 + "' or   ItemSizeName = '+" + num2 + "'");
			if (array.Length != 0 && array2.Length != 0)
			{
				((TextEditorControlBase)cboColor).Value = array[0]["ColorID"];
				((TextEditorControlBase)cboSize).Value = array2[0]["ItemSizeID"];
				if (result3 <= 90m)
				{
					((Control)(object)txtAxis).Text = (result3 + 90m).ToString();
				}
				else
				{
					((Control)(object)txtAxis).Text = (result3 - 90m).ToString();
				}
				return true;
			}
			GlobalVariables.InformationMB.Show(" لا يمكن تحوير عدسات " + text + " لعدم وجود عدسات مطابقه للتحوير", "Cannot Transpose " + text + " Lenses Because There Is No Valid Lenses ");
			return false;
		}
		GlobalVariables.InformationMB.Show(" لا يمكن تحوير عدسات " + text, "Cannot Transpose " + text + " Lenses");
		return false;
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		UltraCheckEditor obj = chkIsCanceled;
		bool flag = (((UltraToggleEditorBase)chkIsDeliverd).Checked = false);
		((UltraToggleEditorBase)obj).Checked = flag;
		UltraDateTimeEditor obj2 = dtpCancelDate;
		object value = (dtpDeliverdDate.Value = null);
		obj2.Value = value;
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		dtLabOrdersPayments.Clear();
		dtLabOrdersContractsDetails.Clear();
		dtLabOrdersDestroyedItems.Clear();
		((Control)(object)txtPaidAmount).Text = "0";
		drMaster = null;
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
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected O, but got Unknown
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Expected O, but got Unknown
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Expected O, but got Unknown
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Expected O, but got Unknown
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Expected O, but got Unknown
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Expected O, but got Unknown
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Expected O, but got Unknown
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Expected O, but got Unknown
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Expected O, but got Unknown
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Expected O, but got Unknown
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Expected O, but got Unknown
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Expected O, but got Unknown
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Expected O, but got Unknown
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Expected O, but got Unknown
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Expected O, but got Unknown
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Expected O, but got Unknown
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Expected O, but got Unknown
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Expected O, but got Unknown
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Expected O, but got Unknown
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Expected O, but got Unknown
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected O, but got Unknown
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Expected O, but got Unknown
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Expected O, but got Unknown
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Expected O, but got Unknown
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Expected O, but got Unknown
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Expected O, but got Unknown
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Expected O, but got Unknown
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Expected O, but got Unknown
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Expected O, but got Unknown
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Expected O, but got Unknown
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Expected O, but got Unknown
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Expected O, but got Unknown
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Expected O, but got Unknown
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Expected O, but got Unknown
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Expected O, but got Unknown
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Expected O, but got Unknown
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Expected O, but got Unknown
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Expected O, but got Unknown
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Expected O, but got Unknown
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Expected O, but got Unknown
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Expected O, but got Unknown
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Expected O, but got Unknown
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Expected O, but got Unknown
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Expected O, but got Unknown
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Expected O, but got Unknown
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Expected O, but got Unknown
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Expected O, but got Unknown
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Expected O, but got Unknown
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Expected O, but got Unknown
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Expected O, but got Unknown
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Expected O, but got Unknown
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Expected O, but got Unknown
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Expected O, but got Unknown
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Expected O, but got Unknown
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Expected O, but got Unknown
		//IL_0e83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8d: Expected O, but got Unknown
		//IL_0eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebd: Expected O, but got Unknown
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Expected O, but got Unknown
		//IL_14b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bc: Expected O, but got Unknown
		//IL_14fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Expected O, but got Unknown
		//IL_1512: Unknown result type (might be due to invalid IL or missing references)
		//IL_151c: Expected O, but got Unknown
		//IL_1b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b94: Expected O, but got Unknown
		//IL_1ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsLabOrders));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
		UltraTab val4 = new UltraTab();
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
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataServices = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGDataDestroyed = new UltraGrid();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.ULGDataContractsDetails = new UltraGrid();
		this.btnSelectDetails = new UltraButton();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnClientSearch = new UltraButton();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblLabs = new UltraLabel();
		this.cboLabs = new UltraComboEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.dtpDeliverdDate = new UltraDateTimeEditor();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.btnClientAdd = new UltraButton();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.btnInvoicePayments = new UltraButton();
		this.cboMobile = new UltraComboEditor();
		this.lblMobile = new UltraLabel();
		this.btnSalesManSearch = new UltraButton();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMan = new UltraComboEditor();
		this.lblmmDistance = new UltraLabel();
		this.lblmmReading = new UltraLabel();
		this.txtIPDReading = new UltraTextEditor();
		this.txtIPDDistance = new UltraTextEditor();
		this.lblIPDReading = new UltraLabel();
		this.lblIPDDistance = new UltraLabel();
		this.lblIPD = new UltraLabel();
		this.lblLAdd = new UltraLabel();
		this.cboLSizeReading = new UltraComboEditor();
		this.txtLAxisReading = new UltraTextEditor();
		this.cboLColorReading = new UltraComboEditor();
		this.cboLSizeDistance = new UltraComboEditor();
		this.txtLAxisDistance = new UltraTextEditor();
		this.lblLCyl = new UltraLabel();
		this.lblLAxis = new UltraLabel();
		this.txtLAdd = new UltraTextEditor();
		this.lblLSph = new UltraLabel();
		this.cboLColorDistance = new UltraComboEditor();
		this.lblL = new UltraLabel();
		this.lblRAdd = new UltraLabel();
		this.cboRSizeReading = new UltraComboEditor();
		this.txtRAxisReading = new UltraTextEditor();
		this.cboRColorReading = new UltraComboEditor();
		this.cboRSizeDistance = new UltraComboEditor();
		this.txtRAxisDistance = new UltraTextEditor();
		this.lblRCyl = new UltraLabel();
		this.lblRAxis = new UltraLabel();
		this.txtRAdd = new UltraTextEditor();
		this.lblDistance = new UltraLabel();
		this.lblRSph = new UltraLabel();
		this.cboRColorDistance = new UltraComboEditor();
		this.lblR = new UltraLabel();
		this.lblReading = new UltraLabel();
		this.UGBGlassesHistory = new UltraGroupBox();
		this.btnLTranspose = new UltraButton();
		this.btnRTranspose = new UltraButton();
		this.dtpGlassesHistoryDate = new UltraDateTimeEditor();
		this.lblDoctor = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.btnAddHistory = new UltraButton();
		this.lblDestroyed = new UltraLabel();
		this.txtDestroyed = new UltraTextEditor();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.btnCanceled = new UltraButton();
		this.dtpCancelDate = new UltraDateTimeEditor();
		this.chkIsCanceled = new UltraCheckEditor();
		this.cboVisaType = new UltraComboEditor();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.chkVisa = new UltraCheckEditor();
		this.btnSponsorSearch = new UltraButton();
		this.lblSponsor = new UltraLabel();
		this.cboSponsor = new UltraComboEditor();
		this.lblCompanyDiscount = new UltraLabel();
		this.txtCompanyDiscount = new UltraTextEditor();
		this.lblClientDiscount = new UltraLabel();
		this.txtClientDiscount = new UltraTextEditor();
		this.txtTotalContractAmount = new UltraTextEditor();
		this.lblTotalContractAmount = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.lblSponsorCompanyName = new UltraLabel();
		this.txtSponsorCompanyName = new UltraTextEditor();
		this.cboFamilyRelatives = new UltraComboEditor();
		this.lblFamilyRelative = new UltraLabel();
		this.txtClientLoadAmount = new UltraTextEditor();
		this.lblClientLoadAmount = new UltraLabel();
		this.lblSponsorApprovalNo = new UltraLabel();
		this.txtSponsorApprovalNo = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataDestroyed).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataContractsDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLabs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpGlassesHistoryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDestroyed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSponsor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalContractAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSponsorCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFamilyRelatives).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientLoadAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSponsorApprovalNo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Services";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab2");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Payments";
		val2.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val2, "ultraTab1");
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "Destroyed";
		val3.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val3, "ultraTab3");
		((SubObjectBase)val3).ForceApplyResources = "";
		((KeyedSubObjectBase)val4).Key = "ContractsDetails";
		val4.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val4, "ultraTab4");
		((SubObjectBase)val4).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[4] { val, val2, val3, val4 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl5, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val5, "appearance21");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val6, "appearance22");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance23");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance24");
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance25");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance26");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val11, "appearance27");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val12).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val12).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val12).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val12).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val12).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance28");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val12;
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
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataServices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGDataServices, "ULGDataServices");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance6");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance7");
		((AppearanceBase)val14).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val15, "appearance8");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val16, "appearance9");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val17, "appearance10");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataServices).Name = "ULGDataServices";
		((UltraControlBase)this.ULGDataServices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
		this.ULGDataServices.AfterEnterEditMode += new System.EventHandler(ULGDataServices_AfterEnterEditMode);
		this.ULGDataServices.AfterRowsDeleted += new System.EventHandler(ULGDataServices_AfterRowsDeleted);
		this.ULGDataServices.CellListSelect += new CellEventHandler(ULGDataServices_CellListSelect);
		this.ULGDataServices.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataServices_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataServices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataServices_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataPayments);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataPayments, "ULGDataPayments");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val18, "appearance1");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance2");
		((AppearanceBase)val19).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val20, "appearance3");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val21, "appearance4");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val22).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val22).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val22, "appearance5");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val22;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPayments.AfterEnterEditMode += new System.EventHandler(ULGDataPayments_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataDestroyed);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGDataDestroyed, "ULGDataDestroyed");
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance11");
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val24).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val24).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val24, "appearance12");
		((AppearanceBase)val24).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val25).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val25, "appearance13");
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val25;
		((AppearanceBase)val26).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val26).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val26, "appearance14");
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val27).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val27).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val27).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val27, "appearance15");
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataDestroyed).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataDestroyed).Name = "ULGDataDestroyed";
		((UltraControlBase)this.ULGDataDestroyed).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataDestroyed.AfterCellUpdate += new CellEventHandler(ULGDataDestroyed_AfterCellUpdate);
		this.ULGDataDestroyed.AfterEnterEditMode += new System.EventHandler(ULGDataDestroyed_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataContractsDetails);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectDetails);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.ULGDataContractsDetails, "ULGDataContractsDetails");
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val28, "appearance16");
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val29).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val29).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val29, "appearance17");
		((AppearanceBase)val29).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val30, "appearance18");
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val30;
		((AppearanceBase)val31).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val31).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val31, "appearance19");
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val32).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val32).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val32).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val32, "appearance20");
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataContractsDetails).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataContractsDetails).Name = "ULGDataContractsDetails";
		((UltraControlBase)this.ULGDataContractsDetails).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataContractsDetails.AfterCellUpdate += new CellEventHandler(ULGDataContractsDetails_AfterCellUpdate);
		this.ULGDataContractsDetails.AfterEnterEditMode += new System.EventHandler(ULGDataContractsDetails_AfterEnterEditMode);
		this.ULGDataContractsDetails.AfterRowsDeleted += new System.EventHandler(ULGDataContractsDetails_AfterRowsDeleted);
		resources.ApplyResources(this.btnSelectDetails, "btnSelectDetails");
		((System.Windows.Forms.Control)(object)this.btnSelectDetails).Name = "btnSelectDetails";
		((System.Windows.Forms.Control)(object)this.btnSelectDetails).Click += new System.EventHandler(btnSelectDetails_Click);
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
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val33).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val33, "appearance29");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.lblLabs, "lblLabs");
		this.lblLabs.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLabs).Name = "lblLabs";
		((ControlBase)this.lblLabs).WrapText = false;
		resources.ApplyResources(this.cboLabs, "cboLabs");
		((TextEditorControlBase)this.cboLabs).AlwaysInEditMode = true;
		this.cboLabs.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLabs).Name = "cboLabs";
		((TextEditorControlBase)this.cboLabs).ValueChanged += new System.EventHandler(cboLabs_ValueChanged);
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		resources.ApplyResources(this.dtpDeliverdDate, "dtpDeliverdDate");
		((UltraWinEditorMaskedControlBase)this.dtpDeliverdDate).AlwaysInEditMode = true;
		this.dtpDeliverdDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDeliverdDate).Name = "dtpDeliverdDate";
		resources.ApplyResources(this.lblTax, "lblTax");
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((AppearanceBase)val34).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val34, "appearance30");
		((ControlBase)this.btnClientAdd).Appearance = (AppearanceBase)(object)val34;
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtPaidAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		resources.ApplyResources(this.btnInvoicePayments, "btnInvoicePayments");
		((System.Windows.Forms.Control)(object)this.btnInvoicePayments).Name = "btnInvoicePayments";
		((System.Windows.Forms.Control)(object)this.btnInvoicePayments).Click += new System.EventHandler(btnOrderPayments_Click);
		resources.ApplyResources(this.cboMobile, "cboMobile");
		((TextEditorControlBase)this.cboMobile).AlwaysInEditMode = true;
		this.cboMobile.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMobile).Name = "cboMobile";
		((TextEditorControlBase)this.cboMobile).ValueChanged += new System.EventHandler(cboMobile_ValueChanged);
		resources.ApplyResources(this.lblMobile, "lblMobile");
		this.lblMobile.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobile).Name = "lblMobile";
		((ControlBase)this.lblMobile).WrapText = false;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((AppearanceBase)val35).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val35, "appearance31");
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		this.lblSalesMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((System.Windows.Forms.Control)(object)this.cboSalesMan).TabStop = false;
		resources.ApplyResources(this.lblmmDistance, "lblmmDistance");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val36, "appearance32");
		((ControlBase)this.lblmmDistance).Appearance = (AppearanceBase)(object)val36;
		this.lblmmDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblmmDistance).Name = "lblmmDistance";
		((ControlBase)this.lblmmDistance).WrapText = false;
		resources.ApplyResources(this.lblmmReading, "lblmmReading");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val37, "appearance33");
		((ControlBase)this.lblmmReading).Appearance = (AppearanceBase)(object)val37;
		this.lblmmReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblmmReading).Name = "lblmmReading";
		((ControlBase)this.lblmmReading).WrapText = false;
		resources.ApplyResources(this.txtIPDReading, "txtIPDReading");
		((System.Windows.Forms.Control)(object)this.txtIPDReading).Name = "txtIPDReading";
		((EditorButtonControlBase)this.txtIPDReading).ReadOnly = true;
		resources.ApplyResources(this.txtIPDDistance, "txtIPDDistance");
		((System.Windows.Forms.Control)(object)this.txtIPDDistance).Name = "txtIPDDistance";
		((EditorButtonControlBase)this.txtIPDDistance).ReadOnly = true;
		resources.ApplyResources(this.lblIPDReading, "lblIPDReading");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val38, "appearance34");
		((ControlBase)this.lblIPDReading).Appearance = (AppearanceBase)(object)val38;
		this.lblIPDReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPDReading).Name = "lblIPDReading";
		((ControlBase)this.lblIPDReading).WrapText = false;
		resources.ApplyResources(this.lblIPDDistance, "lblIPDDistance");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val39, "appearance35");
		((ControlBase)this.lblIPDDistance).Appearance = (AppearanceBase)(object)val39;
		this.lblIPDDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPDDistance).Name = "lblIPDDistance";
		((ControlBase)this.lblIPDDistance).WrapText = false;
		resources.ApplyResources(this.lblIPD, "lblIPD");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val40, "appearance36");
		((ControlBase)this.lblIPD).Appearance = (AppearanceBase)(object)val40;
		this.lblIPD.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPD).Name = "lblIPD";
		((ControlBase)this.lblIPD).WrapText = false;
		resources.ApplyResources(this.lblLAdd, "lblLAdd");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val41, "appearance37");
		((ControlBase)this.lblLAdd).Appearance = (AppearanceBase)(object)val41;
		this.lblLAdd.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLAdd).Name = "lblLAdd";
		((ControlBase)this.lblLAdd).WrapText = false;
		resources.ApplyResources(this.cboLSizeReading, "cboLSizeReading");
		this.cboLSizeReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboLSizeReading).Name = "cboLSizeReading";
		((EditorButtonControlBase)this.cboLSizeReading).ReadOnly = true;
		resources.ApplyResources(this.txtLAxisReading, "txtLAxisReading");
		((System.Windows.Forms.Control)(object)this.txtLAxisReading).Name = "txtLAxisReading";
		((EditorButtonControlBase)this.txtLAxisReading).ReadOnly = true;
		resources.ApplyResources(this.cboLColorReading, "cboLColorReading");
		this.cboLColorReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboLColorReading).Name = "cboLColorReading";
		((EditorButtonControlBase)this.cboLColorReading).ReadOnly = true;
		resources.ApplyResources(this.cboLSizeDistance, "cboLSizeDistance");
		this.cboLSizeDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboLSizeDistance).Name = "cboLSizeDistance";
		((EditorButtonControlBase)this.cboLSizeDistance).ReadOnly = true;
		resources.ApplyResources(this.txtLAxisDistance, "txtLAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtLAxisDistance).Name = "txtLAxisDistance";
		((EditorButtonControlBase)this.txtLAxisDistance).ReadOnly = true;
		resources.ApplyResources(this.lblLCyl, "lblLCyl");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val42, "appearance38");
		((ControlBase)this.lblLCyl).Appearance = (AppearanceBase)(object)val42;
		this.lblLCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLCyl).Name = "lblLCyl";
		((ControlBase)this.lblLCyl).WrapText = false;
		resources.ApplyResources(this.lblLAxis, "lblLAxis");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val43).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val43, "appearance39");
		((ControlBase)this.lblLAxis).Appearance = (AppearanceBase)(object)val43;
		this.lblLAxis.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLAxis).Name = "lblLAxis";
		((ControlBase)this.lblLAxis).WrapText = false;
		resources.ApplyResources(this.txtLAdd, "txtLAdd");
		((System.Windows.Forms.Control)(object)this.txtLAdd).Name = "txtLAdd";
		((EditorButtonControlBase)this.txtLAdd).ReadOnly = true;
		resources.ApplyResources(this.lblLSph, "lblLSph");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val44, "appearance40");
		((ControlBase)this.lblLSph).Appearance = (AppearanceBase)(object)val44;
		this.lblLSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLSph).Name = "lblLSph";
		((ControlBase)this.lblLSph).WrapText = false;
		resources.ApplyResources(this.cboLColorDistance, "cboLColorDistance");
		this.cboLColorDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboLColorDistance).Name = "cboLColorDistance";
		((EditorButtonControlBase)this.cboLColorDistance).ReadOnly = true;
		resources.ApplyResources(this.lblL, "lblL");
		this.lblL.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblL).Name = "lblL";
		((ControlBase)this.lblL).WrapText = false;
		resources.ApplyResources(this.lblRAdd, "lblRAdd");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val45, "appearance41");
		((ControlBase)this.lblRAdd).Appearance = (AppearanceBase)(object)val45;
		this.lblRAdd.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRAdd).Name = "lblRAdd";
		((ControlBase)this.lblRAdd).WrapText = false;
		resources.ApplyResources(this.cboRSizeReading, "cboRSizeReading");
		this.cboRSizeReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboRSizeReading).Name = "cboRSizeReading";
		((EditorButtonControlBase)this.cboRSizeReading).ReadOnly = true;
		resources.ApplyResources(this.txtRAxisReading, "txtRAxisReading");
		((System.Windows.Forms.Control)(object)this.txtRAxisReading).Name = "txtRAxisReading";
		((EditorButtonControlBase)this.txtRAxisReading).ReadOnly = true;
		resources.ApplyResources(this.cboRColorReading, "cboRColorReading");
		this.cboRColorReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboRColorReading).Name = "cboRColorReading";
		((EditorButtonControlBase)this.cboRColorReading).ReadOnly = true;
		resources.ApplyResources(this.cboRSizeDistance, "cboRSizeDistance");
		this.cboRSizeDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboRSizeDistance).Name = "cboRSizeDistance";
		((EditorButtonControlBase)this.cboRSizeDistance).ReadOnly = true;
		resources.ApplyResources(this.txtRAxisDistance, "txtRAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtRAxisDistance).Name = "txtRAxisDistance";
		((EditorButtonControlBase)this.txtRAxisDistance).ReadOnly = true;
		resources.ApplyResources(this.lblRCyl, "lblRCyl");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val46, "appearance42");
		((ControlBase)this.lblRCyl).Appearance = (AppearanceBase)(object)val46;
		this.lblRCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRCyl).Name = "lblRCyl";
		((ControlBase)this.lblRCyl).WrapText = false;
		resources.ApplyResources(this.lblRAxis, "lblRAxis");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val47).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val47, "appearance43");
		((ControlBase)this.lblRAxis).Appearance = (AppearanceBase)(object)val47;
		this.lblRAxis.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRAxis).Name = "lblRAxis";
		((ControlBase)this.lblRAxis).WrapText = false;
		resources.ApplyResources(this.txtRAdd, "txtRAdd");
		((System.Windows.Forms.Control)(object)this.txtRAdd).Name = "txtRAdd";
		((EditorButtonControlBase)this.txtRAdd).ReadOnly = true;
		resources.ApplyResources(this.lblDistance, "lblDistance");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val48).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val48, "appearance44");
		((ControlBase)this.lblDistance).Appearance = (AppearanceBase)(object)val48;
		this.lblDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDistance).Name = "lblDistance";
		((ControlBase)this.lblDistance).WrapText = false;
		resources.ApplyResources(this.lblRSph, "lblRSph");
		((AppearanceBase)val49).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val49).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val49, "appearance45");
		((ControlBase)this.lblRSph).Appearance = (AppearanceBase)(object)val49;
		this.lblRSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRSph).Name = "lblRSph";
		((ControlBase)this.lblRSph).WrapText = false;
		resources.ApplyResources(this.cboRColorDistance, "cboRColorDistance");
		this.cboRColorDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboRColorDistance).Name = "cboRColorDistance";
		((EditorButtonControlBase)this.cboRColorDistance).ReadOnly = true;
		resources.ApplyResources(this.lblR, "lblR");
		this.lblR.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblR).Name = "lblR";
		((ControlBase)this.lblR).WrapText = false;
		resources.ApplyResources(this.lblReading, "lblReading");
		((AppearanceBase)val50).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val50).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val50, "appearance46");
		((ControlBase)this.lblReading).Appearance = (AppearanceBase)(object)val50;
		this.lblReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReading).Name = "lblReading";
		((ControlBase)this.lblReading).WrapText = false;
		resources.ApplyResources(this.UGBGlassesHistory, "UGBGlassesHistory");
		this.UGBGlassesHistory.BorderStyle = (GroupBoxBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnLTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnRTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.dtpGlassesHistoryDate);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnAddHistory);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblR);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRColorDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRSph);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRAxis);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRCyl);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAxisDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRSizeDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRColorReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAxisReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRSizeReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblL);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLColorDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLSph);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLAxis);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLCyl);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAxisDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLSizeDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLColorReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAxisReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLSizeReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPD);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblmmDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPDDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblmmReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPDReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtIPDReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtIPDDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Name = "UGBGlassesHistory";
		((UltraControlBase)this.UGBGlassesHistory).UseAppStyling = false;
		resources.ApplyResources(this.btnLTranspose, "btnLTranspose");
		((AppearanceBase)val51).Image = ERP.Properties.Resources.Productions;
		resources.ApplyResources(val51, "appearance47");
		((ControlBase)this.btnLTranspose).Appearance = (AppearanceBase)(object)val51;
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Name = "btnLTranspose";
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Click += new System.EventHandler(btnLTranspose_Click);
		resources.ApplyResources(this.btnRTranspose, "btnRTranspose");
		((AppearanceBase)val52).Image = ERP.Properties.Resources.Productions;
		resources.ApplyResources(val52, "appearance48");
		((ControlBase)this.btnRTranspose).Appearance = (AppearanceBase)(object)val52;
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Name = "btnRTranspose";
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Click += new System.EventHandler(btnRTranspose_Click);
		resources.ApplyResources(this.dtpGlassesHistoryDate, "dtpGlassesHistoryDate");
		((UltraWinEditorMaskedControlBase)this.dtpGlassesHistoryDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpGlassesHistoryDate).Name = "dtpGlassesHistoryDate";
		((EditorButtonControlBase)this.dtpGlassesHistoryDate).ReadOnly = true;
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		((AppearanceBase)val53).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val53).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val53, "appearance49");
		((ControlBase)this.lblDoctor).Appearance = (AppearanceBase)(object)val53;
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val54).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val54).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val54, "appearance50");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val54;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		((EditorButtonControlBase)this.cboDoctor).ReadOnly = true;
		resources.ApplyResources(this.btnAddHistory, "btnAddHistory");
		((AppearanceBase)val55).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val55, "appearance51");
		((ControlBase)this.btnAddHistory).Appearance = (AppearanceBase)(object)val55;
		((System.Windows.Forms.Control)(object)this.btnAddHistory).Name = "btnAddHistory";
		((System.Windows.Forms.Control)(object)this.btnAddHistory).Click += new System.EventHandler(btnAddHistory_Click);
		resources.ApplyResources(this.lblDestroyed, "lblDestroyed");
		this.lblDestroyed.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDestroyed).Name = "lblDestroyed";
		((ControlBase)this.lblDestroyed).WrapText = false;
		resources.ApplyResources(this.txtDestroyed, "txtDestroyed");
		((System.Windows.Forms.Control)(object)this.txtDestroyed).Name = "txtDestroyed";
		((EditorButtonControlBase)this.txtDestroyed).ReadOnly = true;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.btnCanceled, "btnCanceled");
		((System.Windows.Forms.Control)(object)this.btnCanceled).Name = "btnCanceled";
		((System.Windows.Forms.Control)(object)this.btnCanceled).Click += new System.EventHandler(btnCanceled_Click);
		resources.ApplyResources(this.dtpCancelDate, "dtpCancelDate");
		((UltraWinEditorMaskedControlBase)this.dtpCancelDate).AlwaysInEditMode = true;
		this.dtpCancelDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCancelDate).Name = "dtpCancelDate";
		resources.ApplyResources(this.chkIsCanceled, "chkIsCanceled");
		((System.Windows.Forms.Control)(object)this.chkIsCanceled).Name = "chkIsCanceled";
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((TextEditorControlBase)this.cboVisaType).AlwaysInEditMode = true;
		this.cboVisaType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		((System.Windows.Forms.Control)(object)this.cboVisaType).TabStop = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.chkVisa, "chkVisa");
		((System.Windows.Forms.Control)(object)this.chkVisa).Name = "chkVisa";
		((UltraToggleEditorBase)this.chkVisa).CheckedChanged += new System.EventHandler(chkVisa_CheckedChanged);
		resources.ApplyResources(this.btnSponsorSearch, "btnSponsorSearch");
		((AppearanceBase)val56).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val56, "appearance52");
		((ControlBase)this.btnSponsorSearch).Appearance = (AppearanceBase)(object)val56;
		((System.Windows.Forms.Control)(object)this.btnSponsorSearch).Name = "btnSponsorSearch";
		((System.Windows.Forms.Control)(object)this.btnSponsorSearch).Click += new System.EventHandler(btnSponsorSearch_Click);
		resources.ApplyResources(this.lblSponsor, "lblSponsor");
		this.lblSponsor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSponsor).Name = "lblSponsor";
		((ControlBase)this.lblSponsor).WrapText = false;
		resources.ApplyResources(this.cboSponsor, "cboSponsor");
		((TextEditorControlBase)this.cboSponsor).AlwaysInEditMode = true;
		this.cboSponsor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSponsor).Name = "cboSponsor";
		((TextEditorControlBase)this.cboSponsor).ValueChanged += new System.EventHandler(cboSponsor_ValueChanged);
		resources.ApplyResources(this.lblCompanyDiscount, "lblCompanyDiscount");
		this.lblCompanyDiscount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyDiscount).Name = "lblCompanyDiscount";
		((ControlBase)this.lblCompanyDiscount).WrapText = false;
		resources.ApplyResources(this.txtCompanyDiscount, "txtCompanyDiscount");
		((System.Windows.Forms.Control)(object)this.txtCompanyDiscount).Name = "txtCompanyDiscount";
		((EditorButtonControlBase)this.txtCompanyDiscount).ReadOnly = true;
		resources.ApplyResources(this.lblClientDiscount, "lblClientDiscount");
		this.lblClientDiscount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientDiscount).Name = "lblClientDiscount";
		((ControlBase)this.lblClientDiscount).WrapText = false;
		resources.ApplyResources(this.txtClientDiscount, "txtClientDiscount");
		((System.Windows.Forms.Control)(object)this.txtClientDiscount).Name = "txtClientDiscount";
		((EditorButtonControlBase)this.txtClientDiscount).ReadOnly = true;
		resources.ApplyResources(this.txtTotalContractAmount, "txtTotalContractAmount");
		((System.Windows.Forms.Control)(object)this.txtTotalContractAmount).Name = "txtTotalContractAmount";
		((EditorButtonControlBase)this.txtTotalContractAmount).ReadOnly = true;
		resources.ApplyResources(this.lblTotalContractAmount, "lblTotalContractAmount");
		this.lblTotalContractAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalContractAmount).Name = "lblTotalContractAmount";
		((ControlBase)this.lblTotalContractAmount).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.lblSponsorCompanyName, "lblSponsorCompanyName");
		this.lblSponsorCompanyName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSponsorCompanyName).Name = "lblSponsorCompanyName";
		((ControlBase)this.lblSponsorCompanyName).WrapText = false;
		resources.ApplyResources(this.txtSponsorCompanyName, "txtSponsorCompanyName");
		((System.Windows.Forms.Control)(object)this.txtSponsorCompanyName).Name = "txtSponsorCompanyName";
		resources.ApplyResources(this.cboFamilyRelatives, "cboFamilyRelatives");
		((TextEditorControlBase)this.cboFamilyRelatives).AlwaysInEditMode = true;
		this.cboFamilyRelatives.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFamilyRelatives).Name = "cboFamilyRelatives";
		resources.ApplyResources(this.lblFamilyRelative, "lblFamilyRelative");
		this.lblFamilyRelative.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFamilyRelative).Name = "lblFamilyRelative";
		((ControlBase)this.lblFamilyRelative).WrapText = false;
		resources.ApplyResources(this.txtClientLoadAmount, "txtClientLoadAmount");
		((System.Windows.Forms.Control)(object)this.txtClientLoadAmount).Name = "txtClientLoadAmount";
		((EditorButtonControlBase)this.txtClientLoadAmount).ReadOnly = true;
		((TextEditorControlBase)this.txtClientLoadAmount).ValueChanged += new System.EventHandler(txtClientLoadAmount_ValueChanged);
		resources.ApplyResources(this.lblClientLoadAmount, "lblClientLoadAmount");
		this.lblClientLoadAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientLoadAmount).Name = "lblClientLoadAmount";
		((ControlBase)this.lblClientLoadAmount).WrapText = false;
		resources.ApplyResources(this.lblSponsorApprovalNo, "lblSponsorApprovalNo");
		this.lblSponsorApprovalNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSponsorApprovalNo).Name = "lblSponsorApprovalNo";
		((ControlBase)this.lblSponsorApprovalNo).WrapText = false;
		resources.ApplyResources(this.txtSponsorApprovalNo, "txtSponsorApprovalNo");
		((System.Windows.Forms.Control)(object)this.txtSponsorApprovalNo).Name = "txtSponsorApprovalNo";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSponsorSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFamilyRelative);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSponsor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFamilyRelatives);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSponsor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCancelDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSponsorApprovalNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSponsorCompanyName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSponsorApprovalNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSponsorCompanyName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDestroyed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDestroyed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnInvoicePayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliverdDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLabs);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLabs);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientLoadAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientLoadAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalContractAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalContractAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBGlassesHistory);
		base.Name = "frmLnsLabOrders";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBGlassesHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalContractAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalContractAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientLoadAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientLoadAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLabs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLabs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliverdDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnInvoicePayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDestroyed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDestroyed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSponsorCompanyName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSponsorApprovalNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSponsorCompanyName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSponsorApprovalNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanyDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCancelDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSponsor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFamilyRelatives, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSponsor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFamilyRelative, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSponsorSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataDestroyed).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataContractsDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLabs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpGlassesHistoryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDestroyed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancelDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSponsor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalContractAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSponsorCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFamilyRelatives).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientLoadAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSponsorApprovalNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
