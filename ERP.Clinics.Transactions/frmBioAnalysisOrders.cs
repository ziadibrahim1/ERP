using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Clinics.MasterData;
using ERP.Properties;
using ERP.StockControl.MasterData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.Clinics.Transactions;

public class frmBioAnalysisOrders : frmHeaderManyDetails
{
	private DataTable dtTaxs;

	private DataTable dtBioAnalysisOrdersGroupsDetails;

	private DataTable dtBioAnalysisOrdersGroupsDetailsMaterials;

	private DataTable dtBioAnalysisOrderPayments;

	private DataTable dtUsers;

	private DataTable dtReports;

	private DataTable dtUnits;

	private DataTable dtPatients;

	private DataTable dtPriceType;

	private DataTable dtLabs;

	private DataTable dtVisaType;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private DataTable dtBioAnalysisPrices;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBioAnalysis;

	private DataTable dtBioAnalysisRanges;

	private ValueList vlPrintedUsers = new ValueList();

	private ValueList vlProcessUsers = new ValueList();

	private ValueList vlPaymentUsers = new ValueList();

	private ValueList vlBioAnalysisLabs = new ValueList();

	private ValueList vlBioAnalysis = new ValueList();

	private ValueList vlGroupBioAnalysis = new ValueList();

	private ValueList vlDetailsUnits = new ValueList();

	private ValueList vlMaterialsUnits = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlVisaType = new ValueList();

	private DataSet ds;

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private int NewPriceUserID = 0;

	private int newID = -100000;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

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

	private UltraCheckEditor chkIsDeliverd;

	private UltraDateTimeEditor dtpActualDeliveryDate;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraButton btnOrderPayments;

	public UltraButton btnPatientAdd;

	public UltraButton btnPatientSearch;

	private UltraLabel lblPatientName;

	private UltraComboEditor cboPatients;

	public UltraButton btnPriceTypeSearch;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	private UltraTextEditor txtFromDoctor;

	private UltraLabel lblFromDoctor;

	private UltraComboEditor cboSamplesLabs;

	private UltraLabel lblSamplesLab;

	public UltraButton btnSamplesLabSearch;

	private UltraCheckEditor chkIsConfirmed;

	private UltraComboEditor cboSampleLabUser;

	private UltraLabel lblSampleLabUser;

	public UltraButton btnReceivingLabSearch;

	private UltraLabel lblReceivingLab;

	private UltraComboEditor cboReceivingLab;

	private UltraDateTimeEditor dtpExpectedDeliveryDate;

	private UltraLabel lblExpectedDeliveryDate;

	private UltraLabel lblDeliveryUser;

	private UltraComboEditor cboDeliveryUser;

	private UltraCheckEditor chkVisa;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraComboEditor cboVisaType;

	public frmBioAnalysisOrders()
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
		InitializeComponent();
		TableName = "CL_BioAnalysisOrders";
		IDCol = "BioAnalysisOrderID";
		NoCol = "BioAnalysisOrderNo";
		DateCol = "BioAnalysisOrderDate";
	}

	public frmBioAnalysisOrders(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		base.PrepareData();
		dtBioAnalysis = BioAnalysis.FillCombo("-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlGroupBioAnalysis.ValueListItems.Clear();
		vlBioAnalysis.ValueListItems.Clear();
		for (int i = 0; i < dtBioAnalysis.Rows.Count; i++)
		{
			vlGroupBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
			vlBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
		}
		dtBioAnalysisRanges = BioAnalysisRanges.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int j = 0; j < dtColors.Rows.Count; j++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int k = 0; k < dtSizes.Rows.Count; k++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int l = 0; l < dtBatchs.Rows.Count; l++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int n = 0; n < dtItems.Rows.Count; n++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["ItemBarCode"].ToString());
		}
		dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSampleLabUser, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboDeliveryUser, dtUsers, "UserID", "UserName");
		vlProcessUsers.ValueListItems.Clear();
		vlPrintedUsers.ValueListItems.Clear();
		vlPaymentUsers.ValueListItems.Clear();
		for (int num = 0; num < dtUsers.Rows.Count; num++)
		{
			vlPrintedUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
			vlProcessUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
			vlPaymentUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
		}
		dtLabs = Labs.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtLabs);
		dataView.RowFilter = " IsSampleLab =1 And BranchID = " + GlobalVariables.CurrentBranchID;
		DataView dataView2 = new DataView(dtLabs);
		dataView2.RowFilter = " IsBioAnalysisLab =1 ";
		GlobalFunctions.FillCombo(cboSamplesLabs, dataView.ToTable(), "LabID", "LabName");
		GlobalFunctions.FillCombo(cboReceivingLab, dtLabs, "LabID", "LabName");
		vlBioAnalysisLabs.ValueListItems.Clear();
		DataTable dataTable = dataView2.ToTable();
		for (int num2 = 0; num2 < dataTable.Rows.Count; num2++)
		{
			vlBioAnalysisLabs.ValueListItems.Add(dataTable.Rows[num2]["LabID"], dataTable.Rows[num2]["LabName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		vlVisaType.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtVisaType.Rows.Count; num3++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[num3]["VisaTypeID"], dtVisaType.Rows[num3]["VisaTypeName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsUnits.ValueListItems.Clear();
		vlMaterialsUnits.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtUnits.Rows.Count; num4++)
		{
			vlDetailsUnits.ValueListItems.Add(dtUnits.Rows[num4]["UnitID"], dtUnits.Rows[num4]["UnitName"].ToString());
			vlMaterialsUnits.ValueListItems.Add(dtUnits.Rows[num4]["UnitID"], dtUnits.Rows[num4]["UnitName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtDetails = BioAnalysisOrdersGroups.SelectByBioAnalysisOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBioAnalysisOrdersGroupsDetails = BioAnalysisOrdersGroupsDetails.SelectByBioAnalysisOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBioAnalysisOrdersGroupsDetailsMaterials = BioAnalysisOrdersGroupsDetailsMaterials.SelectByBioAnalysisOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBioAnalysisOrderPayments = BioAnalysisOrdersPayments.SelectByBioAnalysisOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtBioAnalysisOrdersGroupsDetails);
		ds.Tables.Add(dtBioAnalysisOrdersGroupsDetailsMaterials);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtBioAnalysisOrdersGroupsDetails";
		ds.Tables[2].TableName = "dtBioAnalysisOrdersGroupsDetailsMaterials";
		ds.Relations.Add(ds.Tables[0].Columns["BioAnalysisOrderGroupID"], ds.Tables[1].Columns["BioAnalysisOrderGroupID"]);
		ds.Relations.Add(ds.Tables[1].Columns["BioAnalysisOrderGroupDetailID"], ds.Tables[2].Columns["BioAnalysisOrderGroupDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		((UltraGridBase)ULGDataPayments).DataSource = dtBioAnalysisOrderPayments;
		InitGrid();
		InitGridPayment();
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

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderGroupID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupBioAnalysisID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrinted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintUserID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupBioAnalysisID"].Header).Caption = (GlobalVariables.IsArabic ? "مجموعه التحليل" : "Group BioAnalysis");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Header).Caption = (GlobalVariables.IsArabic ? "التعليقات" : "Comments");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrinted"].Header).Caption = (GlobalVariables.IsArabic ? "تم طباعته" : "Is Printed");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintUserID"].Header).Caption = (GlobalVariables.IsArabic ? "طبع بواسطة" : "Print User");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupBioAnalysisID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrinted"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintUserID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupBioAnalysisID"].ValueList = (IValueList)(object)vlGroupBioAnalysis;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintUserID"].ValueList = (IValueList)(object)vlPrintedUsers;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPrinted"].DefaultCellValue = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعه" : "Print");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعه" : "Print");
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Layout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Layout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["SampleNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisLabID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Result"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ResultDescription"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["InProcess"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessStartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessUserID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FiniShedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisID"].Header).Caption = (GlobalVariables.IsArabic ? "التحليل" : "BioAnalysis");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["SampleNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العينة" : "SampleNo");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisLabID"].Header).Caption = (GlobalVariables.IsArabic ? "معمل التحاليل" : "BioAnalysisLab");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Result"].Header).Caption = (GlobalVariables.IsArabic ? "النتجه" : "Result");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ResultDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف النتيجة" : "Result Description");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["InProcess"].Header).Caption = (GlobalVariables.IsArabic ? "قيد التشغيل" : "In Process");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التشغيل" : "ProcessStartDate");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessUserID"].Header).Caption = (GlobalVariables.IsArabic ? "تشغيل بواسطة" : "ProcessUser");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].Header).Caption = (GlobalVariables.IsArabic ? "منتهي" : "Finished");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FiniShedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الانتهاء" : "Finished Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["SampleNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisLabID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Result"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ResultDescription"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["InProcess"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessStartDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessUserID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FiniShedDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisID"].ValueList = (IValueList)(object)vlBioAnalysis;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlDetailsUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessUserID"].ValueList = (IValueList)(object)vlProcessUsers;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BioAnalysisLabID"].ValueList = (IValueList)(object)vlBioAnalysisLabs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["InProcess"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ProcessStartDate"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FiniShedDate"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Layout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Layout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "Barcode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].ValueList = (IValueList)(object)vlMaterialsUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].DefaultCellValue = 0;
	}

	public void InitGridPayment()
	{
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentDate"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "TotalAmount");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "معتمد" : "Approved");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BioAnalysisOrderPaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlPaymentUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].DefaultCellValue = 0;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعه" : "Print");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.05);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPayments).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataPayments).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعه" : "Print");
		}
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns).Count - 1));
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BioAnalysisOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BioAnalysisOrderNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["BioAnalysisOrderDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboPatients).ValueChanged -= cboPatients_ValueChanged;
			((TextEditorControlBase)cboPatients).Value = drMaster["PatientID"];
			((TextEditorControlBase)cboPatients).ValueChanged += cboPatients_ValueChanged;
			((TextEditorControlBase)cboDeliveryUser).Value = drMaster["DeliveryUserID"];
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboReceivingLab).Value = drMaster["ReceivingLabID"];
			((TextEditorControlBase)cboSampleLabUser).Value = drMaster["SampleLabUserID"];
			((TextEditorControlBase)cboSamplesLabs).ValueChanged -= cboSamplesLabs_ValueChanged;
			((TextEditorControlBase)cboSamplesLabs).Value = drMaster["SampleLabID"];
			((TextEditorControlBase)cboSamplesLabs).ValueChanged += cboSamplesLabs_ValueChanged;
			dtpExpectedDeliveryDate.Value = ((drMaster["ExpectedDeliveryDate"] == DBNull.Value) ? DBNull.Value : drMaster["ExpectedDeliveryDate"]);
			((UltraToggleEditorBase)chkIsDeliverd).CheckedChanged -= chkIsDeliverd_CheckedChanged;
			((UltraToggleEditorBase)chkIsDeliverd).Checked = bool.Parse(drMaster["IsDeliverd"].ToString());
			((UltraToggleEditorBase)chkIsDeliverd).CheckedChanged += chkIsDeliverd_CheckedChanged;
			dtpActualDeliveryDate.Value = ((drMaster["ActualDeliveryDate"] == DBNull.Value) ? DBNull.Value : drMaster["ActualDeliveryDate"]);
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
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtFromDoctor).Text = drMaster["FromDoctor"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = BioAnalysisOrdersGroups.SelectByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysisOrdersGroupsDetails = BioAnalysisOrdersGroupsDetails.SelectByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysisOrdersGroupsDetailsMaterials = BioAnalysisOrdersGroupsDetailsMaterials.SelectByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysisOrderPayments = BioAnalysisOrdersPayments.SelectByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataTable dataTable = dtBioAnalysisOrderPayments;
			object obj = dtBioAnalysisOrderPayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtBioAnalysisOrdersGroupsDetails);
			ds.Tables.Add(dtBioAnalysisOrdersGroupsDetailsMaterials);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtBioAnalysisOrdersGroupsDetails";
			ds.Tables[2].TableName = "dtBioAnalysisOrdersGroupsDetailsMaterials";
			ds.Relations.Add(ds.Tables[0].Columns["BioAnalysisOrderGroupID"], ds.Tables[1].Columns["BioAnalysisOrderGroupID"]);
			ds.Relations.Add(ds.Tables[1].Columns["BioAnalysisOrderGroupDetailID"], ds.Tables[2].Columns["BioAnalysisOrderGroupDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			((UltraGridBase)ULGDataPayments).DataSource = dtBioAnalysisOrderPayments;
			InitGrid();
			InitGridPayment();
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
		((EditorButtonControlBase)dtpActualDeliveryDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPatients).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboReceivingLab).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSampleLabUser).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSamplesLabs).ReadOnly = NavMode;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		((Control)(object)chkVisa).Visible = Adding;
		((Control)(object)cboVisaType).Visible = Adding;
		((Control)(object)lblVisaNo).Visible = Adding;
		((Control)(object)txtVisaNo).Visible = Adding;
		((Control)(object)chkIsDeliverd).Enabled = !NavMode;
		((Control)(object)chkIsConfirmed).Enabled = !NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFromDoctor).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = !Adding;
		((Control)(object)btnPatientSearch).Visible = !NavMode;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode;
		((Control)(object)btnSamplesLabSearch).Visible = !NavMode;
		((Control)(object)btnReceivingLabSearch).Visible = !NavMode;
		((Control)(object)btnPatientAdd).Visible = !NavMode;
		((Control)(object)btnOrderPayments).Visible = Updating;
		((UltraTabControlBase)UTCDetails).Tabs["Payments"].Visible = !Adding;
		NewPriceUserID = 0;
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[i]["VisaTypeID"], dataTable.Rows[i]["VisaTypeName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int j = 0; j < dtVisaType.Rows.Count; j++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[j]["VisaTypeID"], dtVisaType.Rows[j]["VisaTypeName"].ToString());
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(((UltraToggleEditorBase)chkIsConfirmed).Checked ? 2 : 6);
	}

	private void MaxDeliveryDate()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BioAnalysisID"].Value != DBNull.Value)
				{
					num2 = int.Parse(dtBioAnalysis.Select("BioAnalysisID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BioAnalysisID"].Value.ToString())[0]["MinDeliveryDays"].ToString());
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
		}
		dtpExpectedDeliveryDate.DateTime = dtpDate.DateTime.AddDays(num);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? BioAnalysisOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboPatients).ValueChanged -= cboPatients_ValueChanged;
		cboPatients.SelectedIndex = -1;
		((TextEditorControlBase)cboPatients).ValueChanged += cboPatients_ValueChanged;
		cboDeliveryUser.SelectedIndex = -1;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboReceivingLab).Value = ((TextEditorControlBase)cboSamplesLabs).Value;
		((TextEditorControlBase)cboSampleLabUser).Value = GlobalVariables.UserID;
		cboTax.SelectedIndex = -1;
		((UltraToggleEditorBase)chkIsDeliverd).CheckedChanged -= chkIsDeliverd_CheckedChanged;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		((UltraToggleEditorBase)chkIsDeliverd).CheckedChanged += chkIsDeliverd_CheckedChanged;
		((UltraToggleEditorBase)chkIsConfirmed).Checked = false;
		((UltraToggleEditorBase)chkVisa).CheckedChanged -= chkVisa_CheckedChanged;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		((UltraToggleEditorBase)chkVisa).CheckedChanged += chkVisa_CheckedChanged;
		dtpExpectedDeliveryDate.Value = DBNull.Value;
		dtpActualDeliveryDate.Value = DBNull.Value;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtFromDoctor).Clear();
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
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
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
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
		{
			GlobalVariables.InformationMB.Show("التاريخ أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "The Date is Less Than Shift End Date Check Your pc ");
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
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(ShiftDetailID, GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
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

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (cboSamplesLabs.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار معمل العينات", "Please Select Sample Lab");
			((TextEditorControlBase)cboSamplesLabs).Focus();
			cboSamplesLabs.DropDown();
			return false;
		}
		if (cboSampleLabUser.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مستخدم معمل العينات", "Please Select Sample Lab User");
			((TextEditorControlBase)cboSampleLabUser).Focus();
			cboSampleLabUser.DropDown();
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
		if (cboPatients.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المريض" : "Please Select Patient");
			((TextEditorControlBase)cboPatients).Focus();
			cboPatients.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CL_BioAnalysisOrders", "BioAnalysisOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BioAnalysisOrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BioAnalysisOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Order Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم المجموعه  ", "Please Enter Group BioAnalysis");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"];
				((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
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
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		DataRow dataRow = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value.ToString())[0];
		string text = ((Control)(object)txtCode).Text;
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
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = BioAnalysisOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPatients.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPatients).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((Control)(object)txtFromDoctor).Text, ((UltraToggleEditorBase)chkIsConfirmed).Checked ? "1" : "0", (cboSamplesLabs.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSamplesLabs).Value.ToString(), (cboSampleLabUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSampleLabUser).Value.ToString(), (cboReceivingLab.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReceivingLab).Value.ToString(), (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpActualDeliveryDate.Value == null) ? "Null" : dtpActualDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboDeliveryUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryUser).Value.ToString(), ((Control)(object)txtNotes).Text, ((Control)(object)txtGrossValue).Text, ((Control)(object)txtDiscBeforeTaxValue).Text, ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), ((Control)(object)txtTaxTotalValue).Text, "0", "0", ((Control)(object)txtNetprice).Text, ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtRestAmount).Text, GlobalVariables.UserID, ShiftDetailID, ShiftDetailUserID, "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = BioAnalysisOrdersGroups.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["GroupBioAnalysisID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["RootBioAnalysisID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Comments"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["Comments"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["IsPrinted"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].Cells["PrintUserID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["PrintUserID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					int num3 = BioAnalysisOrdersGroupsDetails.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BioAnalysisID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["SampleNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BioAnalysisLabID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BioAnalysisLabID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Result"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Result"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ResultDescription"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Notes"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["InProcess"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ProcessStartDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ProcessStartDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ProcessUserID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ProcessUserID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["IsFinished"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["FiniShedDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["FiniShedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows).Count; l++)
					{
						BioAnalysisOrdersGroupsDetailsMaterials.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			if (((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
			{
				ShiftsDetails.ClinicBioAnalysisOrdersPaymentsJVAdding(BioAnalysisOrdersPayments.Insert_Update("-1", ReservationsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), num.ToString(), ((UltraToggleEditorBase)chkVisa).Checked ? ((TextEditorControlBase)cboVisaType).Value.ToString() : "Null", ((UltraToggleEditorBase)chkVisa).Checked ? ((Control)(object)txtVisaNo).Text.ToString() : "Null", ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			ShiftsDetails.SalesClinicBioAnalysisOrdersJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.UserID);
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

	public override void UpdateData()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		DataRow dataRow = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value.ToString())[0];
		string text = ((Control)(object)txtCode).Text;
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = BioAnalysisOrders.Insert_Update(drMaster["BioAnalysisOrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPatients.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPatients).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((Control)(object)txtFromDoctor).Text, ((UltraToggleEditorBase)chkIsConfirmed).Checked ? "1" : "0", (cboSamplesLabs.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSamplesLabs).Value.ToString(), (cboSampleLabUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSampleLabUser).Value.ToString(), (cboReceivingLab.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReceivingLab).Value.ToString(), (dtpExpectedDeliveryDate.Value == null) ? "Null" : dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpActualDeliveryDate.Value == null) ? "Null" : dtpActualDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboDeliveryUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryUser).Value.ToString(), ((Control)(object)txtNotes).Text, ((Control)(object)txtGrossValue).Text, ((Control)(object)txtDiscBeforeTaxValue).Text, ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), ((Control)(object)txtTaxTotalValue).Text, "0", "0", ((Control)(object)txtNetprice).Text, ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtRestAmount).Text, (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), ShiftDetailID, ShiftDetailUserID, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text2 = ",";
			string text3 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				text2 = text2 + ((UltraGridBase)ULGData).Rows[j].Cells["BioAnalysisOrderGroupID"].Value.ToString() + ",";
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					text3 = text3 + ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BioAnalysisOrderGroupDetailID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("dbo.CL_BioAnalysisOrdersGroupsDetailsMaterials", "BioAnalysisOrderID", drMaster["BioAnalysisOrderID"].ToString(), "BioAnalysisOrderGroupDetailID", text3);
			Main.DeleteForUpdate("dbo.CL_BioAnalysisOrdersGroupsDetails", "BioAnalysisOrderID", drMaster["BioAnalysisOrderID"].ToString(), "BioAnalysisOrderGroupDetailID", text3);
			Main.DeleteForUpdate("dbo.CL_BioAnalysisOrdersGroups", "BioAnalysisOrderID", drMaster["BioAnalysisOrderID"].ToString(), "BioAnalysisOrderGroupID", text2);
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				int num2 = BioAnalysisOrdersGroups.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["BioAnalysisOrderGroupID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["BioAnalysisOrderGroupID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[l].Cells["BioAnalysisOrderGroupID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["GroupBioAnalysisID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["Discount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["ActualUnitSalesPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["RootBioAnalysisID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["Comments"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["Comments"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[l].Cells["IsPrinted"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[l].Cells["PrintUserID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["PrintUserID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows).Count; m++)
				{
					int num3 = BioAnalysisOrdersGroupsDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisOrderGroupDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisOrderGroupDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisOrderGroupDetailID"].Value.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["SampleNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisLabID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["BioAnalysisLabID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["Result"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["Result"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["ResultDescription"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["Notes"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["InProcess"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["ProcessStartDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["ProcessStartDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["ProcessUserID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["ProcessUserID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["IsFinished"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["FiniShedDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].Cells["FiniShedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows).Count; n++)
					{
						BioAnalysisOrdersGroupsDetailsMaterials.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["BioAnalysisOrderGroupDetailMaterialID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["BioAnalysisOrderGroupDetailMaterialID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["BioAnalysisOrderGroupDetailMaterialID"].Value.ToString(), num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].ChildBands[0].Rows[m].ChildBands[0].Rows[n].Cells["Notes"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			ShiftsDetails.SalesClinicJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			BioAnalysisOrders.DeleteVirtual(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.UserID);
			BioAnalysisOrdersGroups.DeleteVirtualByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.UserID);
			BioAnalysisOrdersGroupsDetails.DeleteVirtualByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.UserID);
			BioAnalysisOrdersGroupsDetailsMaterials.DeleteVirtualByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.UserID);
			ShiftsDetails.SalesClinicJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_BioAnalysisOrders_A.rpt" : "Rep_CL_BioAnalysisOrders_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BioAnalysisOrderIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@BioAnalysisOrderGroupID", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BioAnalysisOrdersReport(-1, 0, -1, GlobalVariables.BranchIDs);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BioAnalysisOrderID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtBioAnalysis = BioAnalysis.FillCombo("-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlGroupBioAnalysis.ValueListItems.Clear();
		vlBioAnalysis.ValueListItems.Clear();
		for (int i = 0; i < dtBioAnalysis.Rows.Count; i++)
		{
			vlGroupBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
			vlBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
		}
		dtBioAnalysisRanges = BioAnalysisRanges.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int j = 0; j < dtColors.Rows.Count; j++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int k = 0; k < dtSizes.Rows.Count; k++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[k]["ItemSizeID"], dtSizes.Rows[k]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int l = 0; l < dtBatchs.Rows.Count; l++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int n = 0; n < dtItems.Rows.Count; n++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["ItemBarCode"].ToString());
		}
		dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSampleLabUser, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboDeliveryUser, dtUsers, "UserID", "UserName");
		vlProcessUsers.ValueListItems.Clear();
		vlPrintedUsers.ValueListItems.Clear();
		vlPaymentUsers.ValueListItems.Clear();
		for (int num = 0; num < dtUsers.Rows.Count; num++)
		{
			vlPrintedUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
			vlProcessUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
			vlPaymentUsers.ValueListItems.Add(dtUsers.Rows[num]["User_ID"], dtUsers.Rows[num]["UserName"].ToString());
		}
		dtLabs = Labs.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtLabs);
		dataView.RowFilter = " IsSampleLab =1 And BranchID = " + GlobalVariables.CurrentBranchID;
		DataView dataView2 = new DataView(dtLabs);
		dataView2.RowFilter = " IsBioAnalysisLab =1 ";
		GlobalFunctions.FillCombo(cboSamplesLabs, dataView.ToTable(), "LabID", "LabName");
		GlobalFunctions.FillCombo(cboReceivingLab, dtLabs, "LabID", "LabName");
		vlBioAnalysisLabs.ValueListItems.Clear();
		DataTable dataTable = dataView2.ToTable();
		for (int num2 = 0; num2 < dataTable.Rows.Count; num2++)
		{
			vlBioAnalysisLabs.ValueListItems.Add(dataTable.Rows[num2]["LabID"], dataTable.Rows[num2]["LabName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView3 = new DataView(dtVisaType);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView3.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable2, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int num3 = 0; num3 < dataTable2.Rows.Count; num3++)
			{
				vlVisaType.ValueListItems.Add(dataTable2.Rows[num3]["VisaTypeID"], dataTable2.Rows[num3]["VisaTypeName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int num4 = 0; num4 < dtVisaType.Rows.Count; num4++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[num4]["VisaTypeID"], dtVisaType.Rows[num4]["VisaTypeName"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsUnits.ValueListItems.Clear();
		vlMaterialsUnits.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtUnits.Rows.Count; num5++)
		{
			vlDetailsUnits.ValueListItems.Add(dtUnits.Rows[num5]["UnitID"], dtUnits.Rows[num5]["UnitName"].ToString());
			vlMaterialsUnits.ValueListItems.Add(dtUnits.Rows[num5]["UnitID"], dtUnits.Rows[num5]["UnitName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["BioAnalysisOrderGroupID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["BioAnalysisOrderGroupDetailID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 2)
		{
			e.Row.Cells["BioAnalysisOrderGroupDetailMaterialID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)ULGData).Enter += ULGData_Enter;
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null)
		{
			if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
			{
				((UltraGridBase)ULGData).UpdateData();
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price" && ULGData.ActiveCell.Value == DBNull.Value)
				{
					ULGData.ActiveCell.Value = 0;
				}
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price")
				{
					CalculateGoss();
					CalculateRow(e.Cell.Row);
					CalculateRowActualUnitSalesPrice(e.Cell.Row);
				}
			}
			else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ULGData.ActiveCell.Value != DBNull.Value)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "InProcess")
				{
					if (Convert.ToBoolean(ULGData.ActiveCell.Value))
					{
						ULGData.ActiveCell.Row.Cells["ProcessStartDate"].Value = GlobalFunctions.GetServerDateTimeNow();
						ULGData.ActiveCell.Row.Cells["ProcessUserID"].Value = GlobalVariables.UserID;
					}
					else
					{
						ULGData.ActiveCell.Row.Cells["ProcessStartDate"].Value = DBNull.Value;
						ULGData.ActiveCell.Row.Cells["ProcessUserID"].Value = DBNull.Value;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsFinished")
				{
					if (Convert.ToBoolean(ULGData.ActiveCell.Value))
					{
						ULGData.ActiveCell.Row.Cells["FinishedDate"].Value = GlobalFunctions.GetServerDateTimeNow();
					}
					else
					{
						ULGData.ActiveCell.Row.Cells["FinishedDate"].Value = DBNull.Value;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Result")
				{
					string text = "";
					if (cboPatients.SelectedIndex > -1)
					{
						DataRow dataRow = dtPatients.Select("PatientID = " + ((TextEditorControlBase)cboPatients).Value.ToString())[0];
						text += ((dataRow["GenderID"] == DBNull.Value) ? "" : ("And  (GenderID Is Null Or  GenderID = " + dataRow["GenderID"].ToString() + ") "));
						if (dataRow["BirthDate"] != null)
						{
							int num = GlobalFunctions.GetServerDateTimeNow().Year - Convert.ToDateTime(dataRow["BirthDate"]).Year;
							text = text + " And ( FromAge <=" + num + ") And (ToAge = 0 Or ToAge >= " + num + ")";
						}
					}
					DataRow[] array = dtBioAnalysisRanges.Select("BioAnalysisID = " + ULGData.ActiveCell.Row.Cells["BioAnalysisID"].Value.ToString() + " And FromRange <= " + ULGData.ActiveCell.Value.ToString() + " And ToRange >=" + ULGData.ActiveCell.Value.ToString() + " " + text);
					if (array.Length != 0)
					{
						ULGData.ActiveCell.Row.Cells["ResultDescription"].Value = array[0]["RangeDescription"];
					}
				}
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GroupBioAnalysisID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price") && ((UltraToggleEditorBase)chkIsConfirmed).Checked)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsPrinted" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PrintUserID")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BioAnalysisID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ProcessStartDate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FiniShedDate")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Result" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGoss();
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraToggleEditorBase)chkIsConfirmed).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف اي من هذه التحاليل لأنه تم التاكيد" : "Cannot Delete This Analysis Because It Is Confirmed");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Price"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = BioAnalysisOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
		}
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "0") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			CalculateGoss();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void btnInvoicePayments_Click(object sender, EventArgs e)
	{
		if (Updating)
		{
			frmBioAnalysisOrdersPayments frmBioAnalysisOrdersPayments2 = new frmBioAnalysisOrdersPayments(int.Parse(drMaster["BioAnalysisOrderID"].ToString()), decimal.Parse(((Control)(object)txtNetprice).Text.ToString()) - decimal.Parse(((Control)(object)txtPaidAmount).Text.ToString()));
			frmBioAnalysisOrdersPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBioAnalysisOrdersPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد" : "Payments");
			frmBioAnalysisOrdersPayments2.CanAdd = true;
			frmBioAnalysisOrdersPayments2.ShowDialog();
			dtBioAnalysisOrderPayments = BioAnalysisOrdersPayments.SelectByBioAnalysisOrderID(drMaster["BioAnalysisOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataTable dataTable = dtBioAnalysisOrderPayments;
			object obj = dtBioAnalysisOrderPayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((UltraGridBase)ULGDataPayments).DataSource = dtBioAnalysisOrderPayments;
			InitGridPayment();
		}
	}

	private void ULGDataPayments_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Selected = true;
	}

	private void btnPatientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PatientsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboPatients).Value = num;
		}
	}

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		frmPricesTypesChange frmPricesTypesChange2 = new frmPricesTypesChange(base.Name);
		frmPricesTypesChange2.WindowState = FormWindowState.Normal;
		frmPricesTypesChange2.ShowDialog();
		((TextEditorControlBase)cboPriceType).Value = ((frmPricesTypesChange2.PriceTypeID > 0) ? ((object)frmPricesTypesChange2.PriceTypeID) : ((TextEditorControlBase)cboPriceType).Value);
		NewPriceUserID = frmPricesTypesChange2.UserID;
	}

	private void btnSamplesLabSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LabsSearch("1", "0", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSamplesLabs).Value = num;
		}
	}

	private void btnPatientAdd_Click(object sender, EventArgs e)
	{
		bool flag = cboPatients.SelectedIndex == -1;
		frmPatients frmPatients2 = new frmPatients((cboPatients.SelectedIndex == -1) ? (-1) : int.Parse(((TextEditorControlBase)cboPatients).Value.ToString()));
		frmPatients2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmPatients2.lblTitle).Text = (GlobalVariables.IsArabic ? "ملفات المرضى" : "Patients");
		frmPatients2.Tag = GlobalVariables.dtForms.Select("Form = 'frmPatients'")[0];
		frmPatients2.ShowDialog();
		if (frmPatients2.PatientID != 0)
		{
			dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
			((TextEditorControlBase)cboPatients).Value = frmPatients2.PatientID;
		}
	}

	private void btnReceivingLabSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LabsSearch("-1", "-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboReceivingLab).Value = num;
		}
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtBioAnalysisPrices = BioAnalysisPrices.GetPrice(((TextEditorControlBase)cboPriceType).Value.ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Price"].Value = decimal.Parse(dtBioAnalysisPrices.Select(" BioAnalysisID= " + ((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"].Value.ToString())[0]["Price"].ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtBioAnalysisPrices = null;
		}
	}

	private void cboPatients_ValueChanged(object sender, EventArgs e)
	{
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Expected O, but got Unknown
		if (cboPatients.SelectedIndex <= -1)
		{
			return;
		}
		if (dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["Notes"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["Notes"].ToString());
		}
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		if (dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).Value = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["PriceTypeID"];
		}
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtBioAnalysisPrices = BioAnalysisPrices.GetPrice(((TextEditorControlBase)cboPriceType).Value.ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Price"].Value = decimal.Parse(dtBioAnalysisPrices.Select(" BioAnalysisID= " + ((UltraGridBase)ULGData).Rows[i].Cells["GroupBioAnalysisID"].Value.ToString())[0]["Price"].ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
					CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
				}
			}
			CalculateGoss();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtBioAnalysisPrices = null;
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			cboPriceType.SelectedIndex = -1;
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		}
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["Price"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
		}
		if (cboTax.SelectedIndex > -1)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["Price"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["Price"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		if (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			Row.Cells["ActualUnitSalesPrice"].Value = decimal.Parse(Row.Cells["Price"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["Price"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text);
		}
	}

	private void chkIsDeliverd_CheckedChanged(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkIsDeliverd).Checked)
		{
			((TextEditorControlBase)cboDeliveryUser).Value = GlobalVariables.UserID;
			dtpActualDeliveryDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		}
		else
		{
			((TextEditorControlBase)cboDeliveryUser).Value = null;
		}
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboVisaType;
		bool enabled = (((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked);
		((Control)(object)obj).Enabled = enabled;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Expected O, but got Unknown
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Expected O, but got Unknown
		object obj = null;
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Cell).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "GroupBioAnalysisID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				((UltraGridBase)ULGData).UpdateData();
				DataRow dataRow = dtBioAnalysis.Select(" BioAnalysisID = " + e.Cell.Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["RootBioAnalysisID"].Value = ((dataRow["ParentID"] == DBNull.Value) ? e.Cell.Value.ToString() : dataRow["ParentID"]);
				if (dtBioAnalysisPrices != null && dtBioAnalysisPrices.Rows.Count > 0 && cboPatients.SelectedIndex > -1)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["Price"].Value = decimal.Parse(dtBioAnalysisPrices.Select(" BioAnalysisID= " + e.Cell.Value.ToString())[0]["Price"].ToString());
					CalculateGoss();
					CalculateRow(e.Cell.Row);
					CalculateRowActualUnitSalesPrice(e.Cell.Row);
				}
				DataTable dataTable = (Convert.ToBoolean(dataRow["IsMain"]) ? BioAnalysis.SelectByParentID(e.Cell.Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false) : ((!Convert.ToBoolean(dataRow["IsRecipe"])) ? BioAnalysis.Select(e.Cell.Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false) : BioAnalysisRecipes.FillByBioAnalysisID(e.Cell.Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false)));
				DataRow[] array = dtBioAnalysisOrdersGroupsDetails.Select("BioAnalysisOrderGroupID = " + e.Cell.Row.Cells["BioAnalysisOrderGroupID"].Value.ToString());
				DataRow[] array2 = array;
				foreach (DataRow row in array2)
				{
					dtBioAnalysisOrdersGroupsDetails.Rows.Remove(row);
				}
				DataRow[] array3 = dtBioAnalysisOrdersGroupsDetailsMaterials.Select("BioAnalysisOrderGroupID = " + e.Cell.Row.Cells["BioAnalysisOrderGroupID"].Value.ToString());
				DataRow[] array4 = array3;
				foreach (DataRow row2 in array4)
				{
					dtBioAnalysisOrdersGroupsDetailsMaterials.Rows.Remove(row2);
				}
				foreach (DataRow row3 in dataTable.Rows)
				{
					DataRow dataRow3 = dtBioAnalysisOrdersGroupsDetails.NewRow();
					dataRow3["BioAnalysisOrderGroupDetailID"] = ++newID;
					dataRow3["BioAnalysisOrderGroupID"] = e.Cell.Row.Cells["BioAnalysisOrderGroupID"].Value;
					if (Convert.ToBoolean(dataRow["IsMain"]))
					{
						dataRow3["BioAnalysisID"] = row3["BioAnalysisID"];
					}
					else if (Convert.ToBoolean(dataRow["IsRecipe"]))
					{
						dataRow3["BioAnalysisID"] = row3["RecipeBioAnalysisID"];
					}
					else
					{
						dataRow3["BioAnalysisID"] = row3["BioAnalysisID"];
					}
					dataRow3["UnitID"] = row3["BioAnalysisUnitID"];
					dataRow3["BioAnalysisLabID"] = row3["DefaultLabID"];
					dataRow3["InProcess"] = false;
					dataRow3["IsFinished"] = false;
					dtBioAnalysisOrdersGroupsDetails.Rows.Add(dataRow3);
					if (row3["DefaultLabID"] != DBNull.Value)
					{
						obj = dtLabs.Select("LabID = " + row3["DefaultLabID"].ToString())[0]["StoreID"];
					}
					DataTable dataTable2 = BioAnalysisMaterials.SelectByBioAnalysisID(row3["BioAnalysisID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					foreach (DataRow row4 in dataTable2.Rows)
					{
						DataRow dataRow5 = dtBioAnalysisOrdersGroupsDetailsMaterials.NewRow();
						dataRow5["BioAnalysisOrderGroupDetailMaterialID"] = ++newID;
						dataRow5["BioAnalysisOrderGroupID"] = e.Cell.Row.Cells["BioAnalysisOrderGroupID"].Value;
						dataRow5["BioAnalysisOrderGroupDetailID"] = dataRow3["BioAnalysisOrderGroupDetailID"];
						dataRow5["ItemID"] = row4["ItemID"];
						dataRow5["ItemBarcode"] = row4["ItemID"];
						dataRow5["ColorID"] = row4["ColorID"];
						dataRow5["ItemSizeID"] = row4["ItemSizeID"];
						dataRow5["Qty"] = row4["Qty"];
						dataRow5["UnitID"] = row4["UnitID"];
						dataRow5["StoreID"] = ((obj == null) ? DBNull.Value : obj);
						dataRow5["Notes"] = row4["Notes"];
						dtBioAnalysisOrdersGroupsDetailsMaterials.Rows.Add(dataRow5);
					}
				}
				MaxDeliveryDate();
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "BioAnalysisLabID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			obj = dtLabs.Select("LabID = " + e.Cell.Value.ToString())[0]["StoreID"];
			for (int k = 0; k < ((DisposableObjectCollectionBase)ULGData.ActiveCell.Row.ChildBands[0].Rows).Count; k++)
			{
				ULGData.ActiveCell.Row.ChildBands[0].Rows[k].Cells["StoreID"].Value = obj;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		if (!Adding && !Updating && ((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print")
		{
			ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).ActiveRow.Cells["Isprinted"].Value = true;
			((UltraGridBase)ULGData).ActiveRow.Cells["PrintUserID"].Value = GlobalVariables.UserID;
			ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			BioAnalysisOrdersGroups.UpdatPrintData(((UltraGridBase)ULGData).ActiveRow.Cells["BioAnalysisOrderGroupID"].Value.ToString(), GlobalVariables.UserID);
			GlobalVariables.ReportDocument = new ReportDocument();
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_BioAnalysisOrders_A.rpt" : "Rep_CL_BioAnalysisOrders_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BioAnalysisOrderIDs", drMaster["BioAnalysisOrderID"].ToString());
			GlobalVariables.ReportDocument.SetParameterValue("@BioAnalysisOrderGroupID", ((UltraGridBase)ULGData).ActiveRow.Cells["BioAnalysisOrderGroupID"].Value.ToString());
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void ULGDataPayments_ClickCellButton(object sender, CellEventArgs e)
	{
		if (!Adding && !Updating && ((UltraGridBase)ULGDataPayments).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Band.Index == 0 && ((UltraGridBase)ULGDataPayments).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print")
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_BioAnalysisOrdersPayments_A.rpt" : "Rep_CL_BioAnalysisOrdersPayments_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@BioAnalysisOrderPaymentID", ((UltraGridBase)ULGDataPayments).ActiveRow.Cells["BioAnalysisOrderPaymentID"].Value);
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
			GC.Collect();
		}
	}

	private void cboSamplesLabs_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((TextEditorControlBase)cboReceivingLab).Value = ((TextEditorControlBase)cboSamplesLabs).Value;
		}
	}

	private void chkIsConfirmed_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboPatients;
		UltraComboEditor obj2 = cboPriceType;
		UltraButton obj3 = btnPatientSearch;
		UltraButton obj4 = btnPriceTypeSearch;
		bool flag = (((Control)(object)chkIsConfirmed).Enabled = (!Adding && !Updating) || !((UltraToggleEditorBase)chkIsConfirmed).Checked);
		bool flag3 = (((Control)(object)obj4).Enabled = flag);
		bool flag5 = (((Control)(object)obj3).Enabled = flag3);
		bool enabled = (((Control)(object)obj2).Enabled = flag5);
		((Control)(object)obj).Enabled = enabled;
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
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Expected O, but got Unknown
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Expected O, but got Unknown
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Expected O, but got Unknown
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_087a: Expected O, but got Unknown
		//IL_0e6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmBioAnalysisOrders));
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
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.dtpActualDeliveryDate = new UltraDateTimeEditor();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.btnOrderPayments = new UltraButton();
		this.btnPatientAdd = new UltraButton();
		this.btnPatientSearch = new UltraButton();
		this.lblPatientName = new UltraLabel();
		this.cboPatients = new UltraComboEditor();
		this.btnPriceTypeSearch = new UltraButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.txtFromDoctor = new UltraTextEditor();
		this.lblFromDoctor = new UltraLabel();
		this.cboSamplesLabs = new UltraComboEditor();
		this.lblSamplesLab = new UltraLabel();
		this.btnSamplesLabSearch = new UltraButton();
		this.chkIsConfirmed = new UltraCheckEditor();
		this.cboSampleLabUser = new UltraComboEditor();
		this.lblSampleLabUser = new UltraLabel();
		this.btnReceivingLabSearch = new UltraButton();
		this.lblReceivingLab = new UltraLabel();
		this.cboReceivingLab = new UltraComboEditor();
		this.dtpExpectedDeliveryDate = new UltraDateTimeEditor();
		this.lblExpectedDeliveryDate = new UltraLabel();
		this.lblDeliveryUser = new UltraLabel();
		this.cboDeliveryUser = new UltraComboEditor();
		this.chkVisa = new UltraCheckEditor();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.cboVisaType = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFromDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSamplesLabs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsConfirmed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSampleLabUser).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReceivingLab).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryUser).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Payments";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
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
		this.ULGDataPayments.ClickCellButton += new CellEventHandler(ULGDataPayments_ClickCellButton);
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
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
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
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		((UltraToggleEditorBase)this.chkIsDeliverd).CheckedChanged += new System.EventHandler(chkIsDeliverd_CheckedChanged);
		resources.ApplyResources(this.dtpActualDeliveryDate, "dtpActualDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpActualDeliveryDate).AlwaysInEditMode = true;
		this.dtpActualDeliveryDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpActualDeliveryDate).Name = "dtpActualDeliveryDate";
		resources.ApplyResources(this.lblTax, "lblTax");
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
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
		resources.ApplyResources(this.btnOrderPayments, "btnOrderPayments");
		((System.Windows.Forms.Control)(object)this.btnOrderPayments).Name = "btnOrderPayments";
		((System.Windows.Forms.Control)(object)this.btnOrderPayments).Click += new System.EventHandler(btnInvoicePayments_Click);
		resources.ApplyResources(this.btnPatientAdd, "btnPatientAdd");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val15, "appearance14");
		((ControlBase)this.btnPatientAdd).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnPatientAdd).Name = "btnPatientAdd";
		((System.Windows.Forms.Control)(object)this.btnPatientAdd).Click += new System.EventHandler(btnPatientAdd_Click);
		resources.ApplyResources(this.btnPatientSearch, "btnPatientSearch");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance15");
		((ControlBase)this.btnPatientSearch).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Name = "btnPatientSearch";
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Click += new System.EventHandler(btnPatientSearch_Click);
		resources.ApplyResources(this.lblPatientName, "lblPatientName");
		this.lblPatientName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatientName).Name = "lblPatientName";
		((ControlBase)this.lblPatientName).WrapText = false;
		resources.ApplyResources(this.cboPatients, "cboPatients");
		((TextEditorControlBase)this.cboPatients).AlwaysInEditMode = true;
		this.cboPatients.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPatients).Name = "cboPatients";
		((TextEditorControlBase)this.cboPatients).ValueChanged += new System.EventHandler(cboPatients_ValueChanged);
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance16");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((System.Windows.Forms.Control)(object)this.cboPriceType).TabStop = false;
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		resources.ApplyResources(this.txtFromDoctor, "txtFromDoctor");
		((System.Windows.Forms.Control)(object)this.txtFromDoctor).Name = "txtFromDoctor";
		resources.ApplyResources(this.lblFromDoctor, "lblFromDoctor");
		this.lblFromDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDoctor).Name = "lblFromDoctor";
		((ControlBase)this.lblFromDoctor).WrapText = false;
		resources.ApplyResources(this.cboSamplesLabs, "cboSamplesLabs");
		((TextEditorControlBase)this.cboSamplesLabs).AlwaysInEditMode = true;
		this.cboSamplesLabs.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSamplesLabs).Name = "cboSamplesLabs";
		((TextEditorControlBase)this.cboSamplesLabs).ValueChanged += new System.EventHandler(cboSamplesLabs_ValueChanged);
		resources.ApplyResources(this.lblSamplesLab, "lblSamplesLab");
		this.lblSamplesLab.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSamplesLab).Name = "lblSamplesLab";
		((ControlBase)this.lblSamplesLab).WrapText = false;
		resources.ApplyResources(this.btnSamplesLabSearch, "btnSamplesLabSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance17");
		((ControlBase)this.btnSamplesLabSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnSamplesLabSearch).Name = "btnSamplesLabSearch";
		((System.Windows.Forms.Control)(object)this.btnSamplesLabSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSamplesLabSearch).Click += new System.EventHandler(btnSamplesLabSearch_Click);
		resources.ApplyResources(this.chkIsConfirmed, "chkIsConfirmed");
		((System.Windows.Forms.Control)(object)this.chkIsConfirmed).Name = "chkIsConfirmed";
		((UltraToggleEditorBase)this.chkIsConfirmed).CheckedChanged += new System.EventHandler(chkIsConfirmed_CheckedChanged);
		resources.ApplyResources(this.cboSampleLabUser, "cboSampleLabUser");
		((TextEditorControlBase)this.cboSampleLabUser).AlwaysInEditMode = true;
		this.cboSampleLabUser.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSampleLabUser).Name = "cboSampleLabUser";
		resources.ApplyResources(this.lblSampleLabUser, "lblSampleLabUser");
		this.lblSampleLabUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSampleLabUser).Name = "lblSampleLabUser";
		((ControlBase)this.lblSampleLabUser).WrapText = false;
		resources.ApplyResources(this.btnReceivingLabSearch, "btnReceivingLabSearch");
		((AppearanceBase)val19).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val19, "appearance18");
		((ControlBase)this.btnReceivingLabSearch).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.btnReceivingLabSearch).Name = "btnReceivingLabSearch";
		((System.Windows.Forms.Control)(object)this.btnReceivingLabSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnReceivingLabSearch).Click += new System.EventHandler(btnReceivingLabSearch_Click);
		resources.ApplyResources(this.lblReceivingLab, "lblReceivingLab");
		this.lblReceivingLab.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingLab).Name = "lblReceivingLab";
		((ControlBase)this.lblReceivingLab).WrapText = false;
		resources.ApplyResources(this.cboReceivingLab, "cboReceivingLab");
		((TextEditorControlBase)this.cboReceivingLab).AlwaysInEditMode = true;
		this.cboReceivingLab.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboReceivingLab).Name = "cboReceivingLab";
		resources.ApplyResources(this.dtpExpectedDeliveryDate, "dtpExpectedDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpExpectedDeliveryDate).AlwaysInEditMode = true;
		this.dtpExpectedDeliveryDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate).Name = "dtpExpectedDeliveryDate";
		((EditorButtonControlBase)this.dtpExpectedDeliveryDate).ReadOnly = true;
		resources.ApplyResources(this.lblExpectedDeliveryDate, "lblExpectedDeliveryDate");
		this.lblExpectedDeliveryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate).Name = "lblExpectedDeliveryDate";
		((ControlBase)this.lblExpectedDeliveryDate).WrapText = false;
		resources.ApplyResources(this.lblDeliveryUser, "lblDeliveryUser");
		this.lblDeliveryUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryUser).Name = "lblDeliveryUser";
		((ControlBase)this.lblDeliveryUser).WrapText = false;
		resources.ApplyResources(this.cboDeliveryUser, "cboDeliveryUser");
		((TextEditorControlBase)this.cboDeliveryUser).AlwaysInEditMode = true;
		this.cboDeliveryUser.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDeliveryUser).Name = "cboDeliveryUser";
		((EditorButtonControlBase)this.cboDeliveryUser).ReadOnly = true;
		resources.ApplyResources(this.chkVisa, "chkVisa");
		((System.Windows.Forms.Control)(object)this.chkVisa).Name = "chkVisa";
		((UltraToggleEditorBase)this.chkVisa).CheckedChanged += new System.EventHandler(chkVisa_CheckedChanged);
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((TextEditorControlBase)this.cboVisaType).AlwaysInEditMode = true;
		this.cboVisaType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		((System.Windows.Forms.Control)(object)this.cboVisaType).TabStop = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDeliveryUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSampleLabUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSampleLabUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReceivingLab);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingLab);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSamplesLabs);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSamplesLab);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFromDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReceivingLabSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSamplesLabSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPatients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOrderPayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsConfirmed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmBioAnalysisOrders";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsConfirmed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpActualDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOrderPayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPatients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSamplesLabSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReceivingLabSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFromDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSamplesLab, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSamplesLabs, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceivingLab, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReceivingLab, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSampleLabUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSampleLabUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDeliveryUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFromDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSamplesLabs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsConfirmed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSampleLabUser).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReceivingLab).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryUser).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
