using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Export;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Export.Transactions;

public class frmExpOperationsDeclarations : frmHeaderManyDetails
{
	private DataTable dtCarriers;

	private DataTable dtReports;

	private DataTable dtOperationItems;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtUnits;

	private DataTable dtPackingTypes;

	private DataTable dtSeaPorts;

	private DataTable dtOperations;

	private DataTable dtContainersSizes;

	private DataTable dtContainersTypes;

	private DataTable dtDeclarationDetails;

	private DataSet ds;

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlPackingTypes = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlReports = new ValueList();

	private ValueList vlOperationItems = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private int newID = -100000;

	private bool CanOpenLetter = false;

	private IContainer components = null;

	private UltraLabel lblContainersType;

	private UltraComboEditor cboContainersTypes;

	private UltraLabel lblCOTDate;

	private UltraDateTimeEditor dtpCOTDate;

	private UltraLabel lblOperation;

	private UltraComboEditor cboOperations;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnPODSearch;

	public UltraButton btnPOLSearch;

	private UltraLabel lblLoadingSeaPort;

	private UltraComboEditor cboLoadingSeaPort;

	private UltraComboEditor cboDischargeSeaPort;

	private UltraLabel lblDischargeSeaPort;

	private UltraComboEditor cboContainersSizes;

	private UltraLabel lblContainersSize;

	private UltraTextEditor txtVesselName;

	private UltraLabel lblVesselName;

	private UltraTextEditor txtVoyageNo;

	private UltraLabel lblVoyageNo;

	public UltraButton btnOperationsSearch;

	private UltraLabel lblETADate;

	private UltraDateTimeEditor dtpETADate;

	private UltraLabel lblETDDate;

	private UltraDateTimeEditor dtpETDDate;

	private UltraLabel lblETA2Date;

	private UltraDateTimeEditor dtpETA2Date;

	private UltraTextEditor txtCarrierBookingRefNo;

	private UltraTextEditor txtBillOfLadingNo;

	private UltraLabel lblCarrierBookingRefNo;

	private UltraLabel lblBillOfLadingNo;

	private UltraLabel lblContainersCount;

	private UltraTextEditor txtContainersCount;

	private UltraComboEditor cboCarriers;

	private UltraLabel lblCarrier;

	private UltraCheckEditor chkIsFreightPaymentPP;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraTextEditor txtCustomsTotalPrice;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel lblCustomsTotalPrice;

	private UltraLabel lblTotalPrice;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraTabPageControl ultraTabPageControl4;

	protected internal UltraGrid ULGDataItems;

	private UltraTabPageControl ultraTabPageControl5;

	protected internal UltraGrid ULGReports;

	private UltraLabel lblPackingNotes;

	private UltraTextEditor txtPackingNotes;

	private UltraTextEditor txtNoOfOriginals;

	private UltraLabel lblNoOfOriginals;

	private UltraLabel lblNoOfCopies;

	private UltraTextEditor txtNoOfCopies;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpCertificateDate;

	private UltraTextEditor txtCertificateNo;

	private UltraLabel ultraLabel2;

	public frmExpOperationsDeclarations()
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
		InitializeComponent();
		TableName = "EXP_OperationsDeclarations";
		IDCol = "OperationDeclarationID";
		NoCol = "OperationDeclarationNo";
		DateCol = "OperationDeclarationDate";
	}

	public frmExpOperationsDeclarations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
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
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtPackingTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackingTypes.ValueListItems.Clear();
		for (int n = 0; n < dtPackingTypes.Rows.Count; n++)
		{
			vlPackingTypes.ValueListItems.Add(dtPackingTypes.Rows[n]["PackingTypeID"], dtPackingTypes.Rows[n]["PackingTypeName"].ToString());
		}
		dtCarriers = Carriers.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCarriers, dtCarriers, "CarrierID", "CarrierName");
		dtContainersSizes = ContainersSizes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersSizes, dtContainersSizes, "ContainerSizeID", "ContainerSizeName");
		dtContainersTypes = BusinessLayer.Export.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersTypes, dtContainersTypes, "ContainerTypeID", "ContainerTypeName");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtSeaPorts);
		dataView.RowFilter = "IsLoadingPort = 0";
		cboDischargeSeaPort.DataSource = dataView;
		cboDischargeSeaPort.DisplayMember = "SeaPortName";
		cboDischargeSeaPort.ValueMember = "SeaPortID";
		DataView dataView2 = new DataView(dtSeaPorts);
		dataView2.RowFilter = "IsLoadingPort = 1";
		cboLoadingSeaPort.DataSource = dataView2;
		cboLoadingSeaPort.DisplayMember = "SeaPortName";
		cboLoadingSeaPort.ValueMember = "SeaPortID";
		dtOperations = BusinessLayer.Export.Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperations, dtOperations, "OperationID", "OperationNo");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("Export", "Reports", "frmExpOperationsDeclarationsRep"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGReports).DataSource = dtReports;
		InitGridReports();
		dtDetails = OperationsDeclarationsContainers.SelectByOperationDeclarationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtDeclarationDetails = OperationsDeclarationsDetails.SelectByOperationDeclarationID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtDeclarationDetails);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtDeclarationDetails";
		ds.Relations.Add(ds.Tables[0].Columns["OperationDeclarationContainerID"], ds.Tables[1].Columns["OperationDeclarationContainerID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataItems);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDeclarationContainerID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السريل" : "Serial No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SerialNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوية" : "Container No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الطرود " : "Bags Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BagsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الصافي" : "Net Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الكلي " : "Gross Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضاعة" : "Goods Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OperationDeclarationDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف " : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlOperationItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "السريل " : "Batch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية " : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة " : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingTypeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع التغليف" : "Packing Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingTypeID"].ValueList = (IValueList)(object)vlPackingTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingTypeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingWeight"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن " : "Packing Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PackingWeight"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BagsCount"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BagsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الطرود " : "Bags Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BagsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BagsCount"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة " : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "إجمالي السعر" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsUnitPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "جمرك الوحدة " : "Customs Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsTotalPrice"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي الجمارك " : "Customs Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CustomsTotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["GoodsDescription"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضاعة" : "Goods Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.1);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsDeclarations.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["OperationDeclarationNo"].ToString();
			((Control)(object)txtBillOfLadingNo).Text = drMaster["BillOfLadingNo"].ToString();
			((Control)(object)txtCarrierBookingRefNo).Text = drMaster["CarrierBookingRefNo"].ToString();
			((Control)(object)txtContainersCount).Text = drMaster["ContainersCount"].ToString();
			((Control)(object)txtNoOfOriginals).Text = drMaster["NoOfOriginals"].ToString();
			((Control)(object)txtNoOfCopies).Text = drMaster["NoOfCopies"].ToString();
			((Control)(object)txtCertificateNo).Text = drMaster["CertificateNo"].ToString();
			((Control)(object)txtVesselName).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtVoyageNo).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtPackingNotes).Text = drMaster["PackingNotes"].ToString();
			((Control)(object)txtCustomsTotalPrice).Text = decimal.Parse(drMaster["CustomsTotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboCarriers).Value = drMaster["CarrierID"];
			((TextEditorControlBase)cboContainersSizes).Value = drMaster["ContainerSizeID"];
			((TextEditorControlBase)cboContainersTypes).Value = drMaster["ContainerTypeID"];
			((TextEditorControlBase)cboDischargeSeaPort).Value = drMaster["DischargeSeaPortID"];
			((TextEditorControlBase)cboLoadingSeaPort).Value = drMaster["LoadingSeaPortID"];
			((TextEditorControlBase)cboOperations).ValueChanged -= cboOperations_ValueChanged;
			((TextEditorControlBase)cboOperations).Value = drMaster["OperationID"];
			dtOperationItems = OperationsDetails.FillCombo(((TextEditorControlBase)cboOperations).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			vlOperationItems.ValueListItems.Clear();
			for (int i = 0; i < dtOperationItems.Rows.Count; i++)
			{
				vlOperationItems.ValueListItems.Add(dtOperationItems.Rows[i]["ItemID"], dtOperationItems.Rows[i]["ItemName"].ToString());
			}
			((TextEditorControlBase)cboOperations).ValueChanged += cboOperations_ValueChanged;
			dtpDate.Value = drMaster["OperationDeclarationDate"];
			dtpCOTDate.Value = drMaster["COTDate"];
			dtpETA2Date.Value = drMaster["ETA2Date"];
			dtpETADate.Value = drMaster["ETADate"];
			dtpETDDate.Value = drMaster["ETDDate"];
			dtpCertificateDate.Value = drMaster["CertificateDate"];
			((UltraToggleEditorBase)chkIsFreightPaymentPP).Checked = bool.Parse(drMaster["IsFreightPaymentPP"].ToString());
			dtDeclarationDetails = OperationsDeclarationsDetails.SelectByOperationDeclarationID(drMaster["OperationDeclarationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtDetails = OperationsDeclarationsContainers.SelectByOperationDeclarationID(drMaster["OperationDeclarationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtDeclarationDetails);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtDeclarationDetails";
			ds.Relations.Add(ds.Tables[0].Columns["OperationDeclarationContainerID"], ds.Tables[1].Columns["OperationDeclarationContainerID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
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
		((EditorButtonControlBase)txtBillOfLadingNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCarrierBookingRefNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtContainersCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNoOfOriginals).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNoOfCopies).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCertificateNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVesselName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVoyageNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPackingNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCarriers).ReadOnly = NavMode;
		((EditorButtonControlBase)cboContainersSizes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboContainersTypes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDischargeSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLoadingSeaPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperations).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCOTDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpETA2Date).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpETADate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpETDDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCertificateDate).ReadOnly = NavMode;
		((Control)(object)chkIsFreightPaymentPP).Enabled = !NavMode;
		((Control)(object)btnOperationsSearch).Visible = !NavMode;
		((Control)(object)btnPOLSearch).Visible = !NavMode;
		((Control)(object)btnPODSearch).Visible = !NavMode;
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? OperationsDeclarations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboCarriers.SelectedIndex = -1;
		cboContainersSizes.SelectedIndex = -1;
		cboContainersTypes.SelectedIndex = -1;
		cboDischargeSeaPort.SelectedIndex = -1;
		cboLoadingSeaPort.SelectedIndex = -1;
		cboOperations.SelectedIndex = -1;
		((TextEditorControlBase)txtBillOfLadingNo).Clear();
		((TextEditorControlBase)txtCarrierBookingRefNo).Clear();
		((TextEditorControlBase)txtContainersCount).Clear();
		((TextEditorControlBase)txtNoOfOriginals).Clear();
		((TextEditorControlBase)txtNoOfCopies).Clear();
		((TextEditorControlBase)txtCertificateNo).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtPackingNotes).Clear();
		((TextEditorControlBase)txtVesselName).Clear();
		((TextEditorControlBase)txtVoyageNo).Clear();
		((TextEditorControlBase)txtTotalPrice).Clear();
		((TextEditorControlBase)txtCustomsTotalPrice).Clear();
		dtpCOTDate.Value = DBNull.Value;
		dtpETA2Date.Value = DBNull.Value;
		dtpETADate.Value = DBNull.Value;
		dtpETDDate.Value = DBNull.Value;
		dtpCertificateDate.Value = DBNull.Value;
		((UltraToggleEditorBase)chkIsFreightPaymentPP).Checked = false;
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value || dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال التاريخ " : "Please Enter The Date");
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الرقم " : "Please Enter No.");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboOperations.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العملية " : "Please Select Operation");
			((TextEditorControlBase)cboOperations).Focus();
			cboOperations.DropDown();
			return false;
		}
		if (cboCarriers.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار اسم الخط الملاحي" : "Please Select The Carrier");
			((TextEditorControlBase)cboCarriers).Focus();
			cboCarriers.DropDown();
			return false;
		}
		if (cboLoadingSeaPort.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار ميناء التحميل" : "Please Select POL");
			((TextEditorControlBase)cboLoadingSeaPort).Focus();
			cboLoadingSeaPort.DropDown();
			return false;
		}
		if (cboDischargeSeaPort.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار ميناء التفريغ" : "Please Select POD");
			((TextEditorControlBase)cboDischargeSeaPort).Focus();
			cboDischargeSeaPort.DropDown();
			return false;
		}
		if (cboContainersTypes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار أنواع الحاويات " : "Please Select The Containers Types");
			((TextEditorControlBase)cboContainersTypes).Focus();
			cboContainersTypes.DropDown();
			return false;
		}
		if (cboContainersSizes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مقاسات الحاويات " : "Please Select The Containers Sizes");
			((TextEditorControlBase)cboContainersSizes).Focus();
			cboContainersSizes.DropDown();
			return false;
		}
		if (((Control)(object)txtContainersCount).Text.Trim() == "" || int.Parse(((Control)(object)txtContainersCount).Text) <= 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الحاويات" : "Please Enter The Containers Count");
			((TextEditorControlBase)txtContainersCount).Focus();
			return false;
		}
		if (Main.CheckForValue("EXP_OperationsDeclarations", "OperationDeclarationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationDeclarationNo"].ToString(), IsFromServer: true) > 0)
		{
			string codeByBranchID = OperationsDeclarations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("هذا الرقم متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "This Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count != int.Parse(((Control)(object)txtContainersCount).Text))
		{
			GlobalVariables.InformationMB.Show("تفاصيل الحاويات المدخلة غير متوافق مع عدد الحاويات", "The Inserted Containers does not Equals to the Containers Count");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SerialNo"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "الرقم المسلسل متواجد من قبل" : "Serial No Already Exists");
					return false;
				}
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value.ToString()) <= 0)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الرقم المسلسل", "Please Enter Serial No. ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value.ToString()) <= 0)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال عدد الطرود ", "Please Enter Bags Count");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["NetWeight"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["NetWeight"].Value.ToString()), 8) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Net Weight");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["NetWeight"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["GrossWeight"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GrossWeight"].Value.ToString()), 8) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الوزن الكلي   ", "Please Enter Gross Weight");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["GrossWeight"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e26: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = OperationsDeclarations.Insert_Update("-1", ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), (cboCarriers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCarriers).Value.ToString(), (((Control)(object)txtContainersCount).Text == "") ? "0" : ((Control)(object)txtContainersCount).Text, (cboContainersTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersTypes).Value.ToString(), (cboContainersSizes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersSizes).Value.ToString(), (((Control)(object)txtNoOfOriginals).Text == "") ? "0" : ((Control)(object)txtNoOfOriginals).Text, (((Control)(object)txtNoOfCopies).Text == "") ? "0" : ((Control)(object)txtNoOfCopies).Text, (cboLoadingSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingSeaPort).Value.ToString(), (cboDischargeSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDischargeSeaPort).Value.ToString(), (dtpCOTDate.Value == null) ? "Null" : dtpCOTDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETADate.Value == null) ? "Null" : dtpETADate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETDDate.Value == null) ? "Null" : dtpETDDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETA2Date.Value == null) ? "Null" : dtpETA2Date.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtVesselName).Text == "") ? "Null" : ((Control)(object)txtVesselName).Text, (((Control)(object)txtVoyageNo).Text == "") ? "Null" : ((Control)(object)txtVoyageNo).Text, ((UltraToggleEditorBase)chkIsFreightPaymentPP).Checked ? "1" : "0", (((Control)(object)txtCarrierBookingRefNo).Text == "") ? "Null" : ((Control)(object)txtCarrierBookingRefNo).Text, (((Control)(object)txtBillOfLadingNo).Text == "") ? "Null" : ((Control)(object)txtBillOfLadingNo).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtCustomsTotalPrice).Text == "") ? "0" : ((Control)(object)txtCustomsTotalPrice).Text, (((Control)(object)txtPackingNotes).Text == "") ? "Null" : ((Control)(object)txtPackingNotes).Text, "Null", "Null", (((Control)(object)txtCertificateNo).Text == "") ? "Null" : ((Control)(object)txtCertificateNo).Text, (dtpCertificateDate.Value == null) ? "Null" : dtpCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = OperationsDeclarationsContainers.Insert_Update("-1", num.ToString(), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["SerialNo"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ContainerNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["BagsCount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["GoodsDescription"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["NetWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["NetWeight"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["GrossWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["GrossWeight"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					OperationsDeclarationsDetails.Insert_Update("-1", num2.ToString(), num.ToString(), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PackingTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PackingTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PackingWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PackingWeight"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BagsCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BagsCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CustomsUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CustomsUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CustomsTotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CustomsTotalPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["GoodsDescription"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = OperationsDeclarations.Insert_Update(drMaster["OperationDeclarationID"].ToString(), ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), (cboCarriers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCarriers).Value.ToString(), (((Control)(object)txtContainersCount).Text == "") ? "0" : ((Control)(object)txtContainersCount).Text, (cboContainersTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersTypes).Value.ToString(), (cboContainersSizes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainersSizes).Value.ToString(), (((Control)(object)txtNoOfOriginals).Text == "") ? "0" : ((Control)(object)txtNoOfOriginals).Text, (((Control)(object)txtNoOfCopies).Text == "") ? "0" : ((Control)(object)txtNoOfCopies).Text, (cboLoadingSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingSeaPort).Value.ToString(), (cboDischargeSeaPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDischargeSeaPort).Value.ToString(), (dtpCOTDate.Value == null) ? "Null" : dtpCOTDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETADate.Value == null) ? "Null" : dtpETADate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETDDate.Value == null) ? "Null" : dtpETDDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpETA2Date.Value == null) ? "Null" : dtpETA2Date.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtVesselName).Text == "") ? "Null" : ((Control)(object)txtVesselName).Text, (((Control)(object)txtVoyageNo).Text == "") ? "Null" : ((Control)(object)txtVoyageNo).Text, ((UltraToggleEditorBase)chkIsFreightPaymentPP).Checked ? "1" : "0", (((Control)(object)txtCarrierBookingRefNo).Text == "") ? "Null" : ((Control)(object)txtCarrierBookingRefNo).Text, (((Control)(object)txtBillOfLadingNo).Text == "") ? "Null" : ((Control)(object)txtBillOfLadingNo).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtCustomsTotalPrice).Text == "") ? "0" : ((Control)(object)txtCustomsTotalPrice).Text, (((Control)(object)txtPackingNotes).Text == "") ? "Null" : ((Control)(object)txtPackingNotes).Text, "Null", "Null", (((Control)(object)txtCertificateNo).Text == "") ? "Null" : ((Control)(object)txtCertificateNo).Text, (dtpCertificateDate.Value == null) ? "Null" : dtpCertificateDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			string text2 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationContainerID"].Value.ToString() + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					text2 = text2 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["OperationDeclarationDetailID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("EXP_OperationsDeclarationsDetails", "OperationDeclarationID", drMaster["OperationDeclarationID"].ToString(), "OperationDeclarationDetailID", text2);
			Main.DeleteForUpdate("EXP_OperationsDeclarationsContainers", "OperationDeclarationID", drMaster["OperationDeclarationID"].ToString(), "OperationDeclarationContainerID", text);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = OperationsDeclarationsContainers.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["OperationDeclarationContainerID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["OperationDeclarationContainerID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["OperationDeclarationContainerID"].Value.ToString(), num.ToString(), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["SerialNo"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["ContainerNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["BagsCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["BagsCount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["GoodsDescription"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["NetWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["NetWeight"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["GrossWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].Cells["GrossWeight"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					OperationsDeclarationsDetails.Insert_Update(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["OperationDeclarationDetailID"].Value.ToString(), num2.ToString(), num.ToString(), (cboOperations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperations).Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PackingTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PackingTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PackingWeight"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PackingWeight"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BagsCount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BagsCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["CustomsUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["CustomsUnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["CustomsTotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["CustomsTotalPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["GoodsDescription"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
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

	public override void DeleteData()
	{
		base.DeleteData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JVDetails.DeleteVirtualByJVID(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			OperationsDeclarationsDetails.DeleteVirtualByOperationDeclarationID(drMaster["OperationDeclarationID"].ToString(), GlobalVariables.UserID);
			OperationsDeclarationsContainers.DeleteVirtualByOperationDeclarationID(drMaster["OperationDeclarationID"].ToString(), GlobalVariables.UserID);
			OperationsDeclarations.DeleteVirtual(drMaster["OperationDeclarationID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
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
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtPackingTypes = PackingTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPackingTypes.ValueListItems.Clear();
		for (int n = 0; n < dtPackingTypes.Rows.Count; n++)
		{
			vlPackingTypes.ValueListItems.Add(dtPackingTypes.Rows[n]["PackingTypeID"], dtPackingTypes.Rows[n]["PackingTypeName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, GlobalFunctions.GetFormID("Export", "Reports", "frmExpOperationsDeclarationsRep"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGReports).DataSource = dtReports;
		dtCarriers = Carriers.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCarriers, dtCarriers, "CarrierID", "CarrierName");
		dtContainersSizes = ContainersSizes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersSizes, dtContainersSizes, "ContainerSizeID", "ContainerSizeName");
		dtContainersTypes = BusinessLayer.Export.ContainersTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboContainersTypes, dtContainersTypes, "ContainerTypeID", "ContainerTypeName");
		dtSeaPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtSeaPorts);
		dataView.RowFilter = "IsLoadingPort = 1";
		cboLoadingSeaPort.DataSource = dataView;
		cboLoadingSeaPort.DisplayMember = "SeaPortName";
		cboLoadingSeaPort.ValueMember = "SeaPortID";
		DataView dataView2 = new DataView(dtSeaPorts);
		dataView2.RowFilter = "IsLoadingPort = 0";
		cboDischargeSeaPort.DataSource = dataView2;
		cboDischargeSeaPort.DisplayMember = "SeaPortName";
		cboDischargeSeaPort.ValueMember = "SeaPortID";
		dtOperations = BusinessLayer.Export.Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperations, dtOperations, "OperationID", "OperationNo");
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ExpOperationsDeclarationsReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationDeclarationID"].ToString();
			FillData();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = OperationsDeclarations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetWeight" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GrossWeight"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SerialNo" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount"))
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["OperationDeclarationContainerID"].Value = ++newID;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 1)
			{
				e.Row.Cells["SerialNo"].Value = Convert.ToInt32(((UltraGridBase)ULGData).Rows[e.Row.Index - 1].Cells["SerialNo"].Value) + 1;
			}
			else
			{
				e.Row.Cells["SerialNo"].Value = 1;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)ULGData.ActiveCell).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ItemID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "BagsCount")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetWeight" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GrossWeight")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void cboOperations_ValueChanged(object sender, EventArgs e)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		((TextEditorControlBase)cboOperations).ValueChanged -= cboOperations_ValueChanged;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (cboOperations.SelectedIndex > -1)
		{
			DataRow dataRow = dtOperations.Select(" OperationID = " + ((TextEditorControlBase)cboOperations).Value.ToString())[0];
			((TextEditorControlBase)cboLoadingSeaPort).Value = dataRow["LoadingSeaPortID"];
			((TextEditorControlBase)cboDischargeSeaPort).Value = dataRow["DischargeSeaPortID"];
			((TextEditorControlBase)cboContainersSizes).Value = dataRow["ContainerSizeID"];
			((TextEditorControlBase)cboContainersTypes).Value = dataRow["ContainerTypeID"];
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
			dtOperationItems = OperationsDetails.FillCombo(((TextEditorControlBase)cboOperations).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			vlOperationItems.ValueListItems.Clear();
			for (int i = 0; i < dtOperationItems.Rows.Count; i++)
			{
				vlOperationItems.ValueListItems.Add(dtOperationItems.Rows[i]["ItemID"], dtOperationItems.Rows[i]["ItemName"].ToString());
			}
			CalculateTotals();
			CalculateCustomsTotals();
		}
		else
		{
			dtDeclarationDetails.Rows.Clear();
		}
		((TextEditorControlBase)cboOperations).ValueChanged += cboOperations_ValueChanged;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value.ToString());
			}
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateCustomsTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CustomsTotalPrice"].Value.ToString());
			}
		}
		((Control)(object)txtCustomsTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateTotalBagsCountAndWeights(int RowIndex)
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["PackingTypeID"].Value != null && ((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["PackingTypeID"].Value != DBNull.Value)
			{
				num3 += decimal.Parse(dtPackingTypes.Select("PackingTypeID = " + ((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["PackingTypeID"].Value.ToString())[0]["CoverWeightKG"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["BagsCount"].Value.ToString());
			}
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["PackingWeight"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["BagsCount"].Value.ToString());
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[RowIndex].ChildBands[0].Rows[i].Cells["BagsCount"].Value.ToString());
		}
		((UltraGridBase)ULGData).Rows[RowIndex].Cells["GrossWeight"].Value = (num2 + num3) / 1000m;
		((UltraGridBase)ULGData).Rows[RowIndex].Cells["NetWeight"].Value = num2 / 1000m;
		((UltraGridBase)ULGData).Rows[RowIndex].Cells["BagsCount"].Value = num;
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

	private void btnPOLSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(1, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLoadingSeaPort).Value = num;
		}
	}

	private void btnPODSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SeaPortsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboDischargeSeaPort).Value = num;
		}
	}

	private void btnOperationsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ExpOperationsSearch(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboOperations).Value = num;
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && TransportationsOrdersDetails.SelectByOperationDeclarationContainerID(((UltraGridBase)ULGData).ActiveRow.Cells["OperationDeclarationContainerID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الحاويه لانه تم عمل طلب نقل عليها" : "Cannot Delete This Container Because There's A Transportation Order On It");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_EXP_OperationsDeclarations_E.rpt" : "Rep_EXP_OperationsDeclarations_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationDeclarationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
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

	private void ULGReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster != null && e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGReports).ActiveRow != null && !CanOpenLetter)
		{
			CanOpenLetter = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationDeclarationIDs", string.Concat(",", drMaster["OperationDeclarationID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenLetter = false;
		}
	}

	private void ULGReports_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGReports).ActiveRow).Selected = true;
	}

	public void InitGridReports()
	{
		GlobalFunctions.PrepareGrid(ULGReports);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.1);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).Rows).Count; i++)
		{
			((UltraGridBase)ULGReports).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((GridItemBase)e.Cell).Band.Index == 1 && ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtOperationItems.Select(" ItemID = " + e.Cell.Value.ToString())[0];
			e.Cell.Row.Cells["UnitID"].Value = dataRow["UnitID"];
			e.Cell.Row.Cells["Qty"].Value = dataRow["Qty"];
			e.Cell.Row.Cells["ItemSizeID"].Value = dataRow["ItemSizeID"];
			e.Cell.Row.Cells["ColorID"].Value = dataRow["ColorID"];
			e.Cell.Row.Cells["BatchID"].Value = dataRow["BatchID"];
			e.Cell.Row.Cells["BagsCount"].Value = dataRow["BagsCount"];
			e.Cell.Row.Cells["PackingTypeID"].Value = dataRow["PackingTypeID"];
			e.Cell.Row.Cells["PackingWeight"].Value = dataRow["PackingWeight"];
			e.Cell.Row.Cells["UnitPrice"].Value = dataRow["UnitPrice"];
			e.Cell.Row.Cells["TotalPrice"].Value = dataRow["TotalPrice"];
			e.Cell.Row.Cells["CustomsUnitPrice"].Value = dataRow["CustomsUnitPrice"];
			e.Cell.Row.Cells["CustomsTotalPrice"].Value = dataRow["CustomsTotalPrice"];
			e.Cell.Row.Cells["GoodsDescription"].Value = dataRow["GoodsDescription"];
			CalculateTotalBagsCountAndWeights(e.Cell.Row.ParentRow.Index);
			CalculateTotals();
			CalculateCustomsTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		if (((GridItemBase)e.Cell.Row).Band.Index != 1)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsUnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CustomsTotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["CustomsTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CustomsUnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BagsCount")
			{
				CalculateTotalBagsCountAndWeights(((UltraGridBase)ULGData).ActiveRow.ParentRow.Index);
			}
			CalculateTotals();
			CalculateCustomsTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		CalculateCustomsTotals();
		CalculateTotals();
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateTotalBagsCountAndWeights(i);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		if (OperationsDeclarationsInvoices.SelectByOperationDeclarationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فواتير عليها", "Cannot Delete This Transaction Because There Are Invoices For It");
			return;
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
			if (OperationsDeclarationsInvoices.SelectByOperationDeclarationID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فواتير عليها", "Cannot Update This Transaction Because There Are Invoices For It");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Expected O, but got Unknown
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Expected O, but got Unknown
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Expected O, but got Unknown
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Expected O, but got Unknown
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Export.Transactions.frmExpOperationsDeclarations));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
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
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGDataItems = new UltraGrid();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.ULGReports = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.lblContainersType = new UltraLabel();
		this.cboContainersTypes = new UltraComboEditor();
		this.lblCOTDate = new UltraLabel();
		this.dtpCOTDate = new UltraDateTimeEditor();
		this.lblOperation = new UltraLabel();
		this.cboOperations = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnPODSearch = new UltraButton();
		this.btnPOLSearch = new UltraButton();
		this.lblLoadingSeaPort = new UltraLabel();
		this.cboLoadingSeaPort = new UltraComboEditor();
		this.cboDischargeSeaPort = new UltraComboEditor();
		this.lblDischargeSeaPort = new UltraLabel();
		this.cboContainersSizes = new UltraComboEditor();
		this.lblContainersSize = new UltraLabel();
		this.txtVesselName = new UltraTextEditor();
		this.lblVesselName = new UltraLabel();
		this.txtVoyageNo = new UltraTextEditor();
		this.lblVoyageNo = new UltraLabel();
		this.btnOperationsSearch = new UltraButton();
		this.lblETADate = new UltraLabel();
		this.dtpETADate = new UltraDateTimeEditor();
		this.lblETDDate = new UltraLabel();
		this.dtpETDDate = new UltraDateTimeEditor();
		this.lblETA2Date = new UltraLabel();
		this.dtpETA2Date = new UltraDateTimeEditor();
		this.txtCarrierBookingRefNo = new UltraTextEditor();
		this.txtBillOfLadingNo = new UltraTextEditor();
		this.lblCarrierBookingRefNo = new UltraLabel();
		this.lblBillOfLadingNo = new UltraLabel();
		this.lblContainersCount = new UltraLabel();
		this.txtContainersCount = new UltraTextEditor();
		this.cboCarriers = new UltraComboEditor();
		this.lblCarrier = new UltraLabel();
		this.chkIsFreightPaymentPP = new UltraCheckEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtCustomsTotalPrice = new UltraTextEditor();
		this.txtTotalPrice = new UltraTextEditor();
		this.lblCustomsTotalPrice = new UltraLabel();
		this.lblTotalPrice = new UltraLabel();
		this.lblPackingNotes = new UltraLabel();
		this.txtPackingNotes = new UltraTextEditor();
		this.txtNoOfOriginals = new UltraTextEditor();
		this.lblNoOfOriginals = new UltraLabel();
		this.lblNoOfCopies = new UltraLabel();
		this.txtNoOfCopies = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.dtpCertificateDate = new UltraDateTimeEditor();
		this.txtCertificateNo = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCOTDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDischargeSeaPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyageNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETADate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETDDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETA2Date).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCarrierBookingRefNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillOfLadingNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainersCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCarriers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFreightPaymentPP).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPackingNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNoOfOriginals).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNoOfCopies).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCertificateDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Items";
		val.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val, "ultraTab1");
		val.Visible = false;
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Reports";
		val2.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl5, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val3, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val4, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance13");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance14");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance15");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance16");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val9, "appearance17");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
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
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGDataItems, "ULGDataItems");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance1");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val12, "appearance2");
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val13, "appearance3");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance4");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance5");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataItems).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataItems).Name = "ULGDataItems";
		((UltraControlBase)this.ULGDataItems).UseFlatMode = (DefaultableBoolean)1;
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGReports);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.ULGReports, "ULGReports");
		((UltraGridBase)this.ULGReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance6");
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val17, "appearance7");
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val18, "appearance8");
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance9");
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance10");
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGReports).Name = "ULGReports";
		((UltraControlBase)this.ULGReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGReports.AfterEnterEditMode += new System.EventHandler(ULGReports_AfterEnterEditMode);
		this.ULGReports.ClickCellButton += new CellEventHandler(ULGReports_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.lblContainersType, "lblContainersType");
		this.lblContainersType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainersType).Name = "lblContainersType";
		((ControlBase)this.lblContainersType).WrapText = false;
		resources.ApplyResources(this.cboContainersTypes, "cboContainersTypes");
		this.cboContainersTypes.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboContainersTypes).Name = "cboContainersTypes";
		resources.ApplyResources(this.lblCOTDate, "lblCOTDate");
		this.lblCOTDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCOTDate).Name = "lblCOTDate";
		((ControlBase)this.lblCOTDate).WrapText = false;
		resources.ApplyResources(this.dtpCOTDate, "dtpCOTDate");
		((UltraWinEditorMaskedControlBase)this.dtpCOTDate).AlwaysInEditMode = true;
		this.dtpCOTDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCOTDate).Name = "dtpCOTDate";
		resources.ApplyResources(this.lblOperation, "lblOperation");
		this.lblOperation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperation).Name = "lblOperation";
		((ControlBase)this.lblOperation).WrapText = false;
		resources.ApplyResources(this.cboOperations, "cboOperations");
		this.cboOperations.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOperations).Name = "cboOperations";
		((TextEditorControlBase)this.cboOperations).ValueChanged += new System.EventHandler(cboOperations_ValueChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.btnPODSearch, "btnPODSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance22");
		((ControlBase)this.btnPODSearch).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.btnPODSearch).Name = "btnPODSearch";
		((System.Windows.Forms.Control)(object)this.btnPODSearch).Click += new System.EventHandler(btnPODSearch_Click);
		resources.ApplyResources(this.btnPOLSearch, "btnPOLSearch");
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val22, "appearance23");
		((ControlBase)this.btnPOLSearch).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.btnPOLSearch).Name = "btnPOLSearch";
		((System.Windows.Forms.Control)(object)this.btnPOLSearch).Click += new System.EventHandler(btnPOLSearch_Click);
		resources.ApplyResources(this.lblLoadingSeaPort, "lblLoadingSeaPort");
		this.lblLoadingSeaPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort).Name = "lblLoadingSeaPort";
		((ControlBase)this.lblLoadingSeaPort).WrapText = false;
		resources.ApplyResources(this.cboLoadingSeaPort, "cboLoadingSeaPort");
		this.cboLoadingSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort).Name = "cboLoadingSeaPort";
		resources.ApplyResources(this.cboDischargeSeaPort, "cboDischargeSeaPort");
		this.cboDischargeSeaPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort).Name = "cboDischargeSeaPort";
		resources.ApplyResources(this.lblDischargeSeaPort, "lblDischargeSeaPort");
		this.lblDischargeSeaPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort).Name = "lblDischargeSeaPort";
		((ControlBase)this.lblDischargeSeaPort).WrapText = false;
		resources.ApplyResources(this.cboContainersSizes, "cboContainersSizes");
		this.cboContainersSizes.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboContainersSizes).Name = "cboContainersSizes";
		resources.ApplyResources(this.lblContainersSize, "lblContainersSize");
		this.lblContainersSize.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainersSize).Name = "lblContainersSize";
		((ControlBase)this.lblContainersSize).WrapText = false;
		resources.ApplyResources(this.txtVesselName, "txtVesselName");
		((System.Windows.Forms.Control)(object)this.txtVesselName).Name = "txtVesselName";
		resources.ApplyResources(this.lblVesselName, "lblVesselName");
		this.lblVesselName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselName).Name = "lblVesselName";
		((ControlBase)this.lblVesselName).WrapText = false;
		resources.ApplyResources(this.txtVoyageNo, "txtVoyageNo");
		((System.Windows.Forms.Control)(object)this.txtVoyageNo).Name = "txtVoyageNo";
		resources.ApplyResources(this.lblVoyageNo, "lblVoyageNo");
		this.lblVoyageNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyageNo).Name = "lblVoyageNo";
		((ControlBase)this.lblVoyageNo).WrapText = false;
		resources.ApplyResources(this.btnOperationsSearch, "btnOperationsSearch");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val23, "appearance24");
		((ControlBase)this.btnOperationsSearch).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.btnOperationsSearch).Name = "btnOperationsSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationsSearch).Click += new System.EventHandler(btnOperationsSearch_Click);
		resources.ApplyResources(this.lblETADate, "lblETADate");
		this.lblETADate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblETADate).Name = "lblETADate";
		((ControlBase)this.lblETADate).WrapText = false;
		resources.ApplyResources(this.dtpETADate, "dtpETADate");
		((UltraWinEditorMaskedControlBase)this.dtpETADate).AlwaysInEditMode = true;
		this.dtpETADate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpETADate).Name = "dtpETADate";
		resources.ApplyResources(this.lblETDDate, "lblETDDate");
		this.lblETDDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblETDDate).Name = "lblETDDate";
		((ControlBase)this.lblETDDate).WrapText = false;
		resources.ApplyResources(this.dtpETDDate, "dtpETDDate");
		((UltraWinEditorMaskedControlBase)this.dtpETDDate).AlwaysInEditMode = true;
		this.dtpETDDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpETDDate).Name = "dtpETDDate";
		resources.ApplyResources(this.lblETA2Date, "lblETA2Date");
		this.lblETA2Date.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblETA2Date).Name = "lblETA2Date";
		((ControlBase)this.lblETA2Date).WrapText = false;
		resources.ApplyResources(this.dtpETA2Date, "dtpETA2Date");
		((UltraWinEditorMaskedControlBase)this.dtpETA2Date).AlwaysInEditMode = true;
		this.dtpETA2Date.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpETA2Date).Name = "dtpETA2Date";
		resources.ApplyResources(this.txtCarrierBookingRefNo, "txtCarrierBookingRefNo");
		((System.Windows.Forms.Control)(object)this.txtCarrierBookingRefNo).Name = "txtCarrierBookingRefNo";
		resources.ApplyResources(this.txtBillOfLadingNo, "txtBillOfLadingNo");
		((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo).Name = "txtBillOfLadingNo";
		resources.ApplyResources(this.lblCarrierBookingRefNo, "lblCarrierBookingRefNo");
		this.lblCarrierBookingRefNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCarrierBookingRefNo).Name = "lblCarrierBookingRefNo";
		((ControlBase)this.lblCarrierBookingRefNo).WrapText = false;
		resources.ApplyResources(this.lblBillOfLadingNo, "lblBillOfLadingNo");
		this.lblBillOfLadingNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo).Name = "lblBillOfLadingNo";
		((ControlBase)this.lblBillOfLadingNo).WrapText = false;
		resources.ApplyResources(this.lblContainersCount, "lblContainersCount");
		this.lblContainersCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainersCount).Name = "lblContainersCount";
		((ControlBase)this.lblContainersCount).WrapText = false;
		resources.ApplyResources(this.txtContainersCount, "txtContainersCount");
		((System.Windows.Forms.Control)(object)this.txtContainersCount).Name = "txtContainersCount";
		((System.Windows.Forms.Control)(object)this.txtContainersCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.cboCarriers, "cboCarriers");
		this.cboCarriers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCarriers).Name = "cboCarriers";
		resources.ApplyResources(this.lblCarrier, "lblCarrier");
		this.lblCarrier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCarrier).Name = "lblCarrier";
		((ControlBase)this.lblCarrier).WrapText = false;
		resources.ApplyResources(this.chkIsFreightPaymentPP, "chkIsFreightPaymentPP");
		((System.Windows.Forms.Control)(object)this.chkIsFreightPaymentPP).Name = "chkIsFreightPaymentPP";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtCustomsTotalPrice, "txtCustomsTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice).Name = "txtCustomsTotalPrice";
		((EditorButtonControlBase)this.txtCustomsTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.lblCustomsTotalPrice, "lblCustomsTotalPrice");
		this.lblCustomsTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice).Name = "lblCustomsTotalPrice";
		((ControlBase)this.lblCustomsTotalPrice).WrapText = false;
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.lblPackingNotes, "lblPackingNotes");
		this.lblPackingNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPackingNotes).Name = "lblPackingNotes";
		((ControlBase)this.lblPackingNotes).WrapText = false;
		resources.ApplyResources(this.txtPackingNotes, "txtPackingNotes");
		((System.Windows.Forms.Control)(object)this.txtPackingNotes).Name = "txtPackingNotes";
		resources.ApplyResources(this.txtNoOfOriginals, "txtNoOfOriginals");
		((System.Windows.Forms.Control)(object)this.txtNoOfOriginals).Name = "txtNoOfOriginals";
		((System.Windows.Forms.Control)(object)this.txtNoOfOriginals).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblNoOfOriginals, "lblNoOfOriginals");
		this.lblNoOfOriginals.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNoOfOriginals).Name = "lblNoOfOriginals";
		((ControlBase)this.lblNoOfOriginals).WrapText = false;
		resources.ApplyResources(this.lblNoOfCopies, "lblNoOfCopies");
		this.lblNoOfCopies.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNoOfCopies).Name = "lblNoOfCopies";
		((ControlBase)this.lblNoOfCopies).WrapText = false;
		resources.ApplyResources(this.txtNoOfCopies, "txtNoOfCopies");
		((System.Windows.Forms.Control)(object)this.txtNoOfCopies).Name = "txtNoOfCopies";
		((System.Windows.Forms.Control)(object)this.txtNoOfCopies).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpCertificateDate, "dtpCertificateDate");
		((UltraWinEditorMaskedControlBase)this.dtpCertificateDate).AlwaysInEditMode = true;
		this.dtpCertificateDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCertificateDate).Name = "dtpCertificateDate";
		resources.ApplyResources(this.txtCertificateNo, "txtCertificateNo");
		((System.Windows.Forms.Control)(object)this.txtCertificateNo).Name = "txtCertificateNo";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNoOfCopies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNoOfCopies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNoOfOriginals);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNoOfOriginals);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPackingNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPackingNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsFreightPaymentPP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPOLSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpETDDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCOTDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblETDDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCOTDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCarrier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCarriers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCarrierBookingRefNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCarrierBookingRefNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVesselName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContainersTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyageNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyageNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainersCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblETADate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblETA2Date);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpETADate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpETA2Date);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainersSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContainersSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPODSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCertificateDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Name = "frmExpOperationsDeclarations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCertificateDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPODSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDischargeSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDischargeSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContainersSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpETA2Date, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpETADate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblETA2Date, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblETADate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainersCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyageNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyageNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperations, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContainersTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainersType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCarrierBookingRefNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCarrierBookingRefNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCarriers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCarrier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCOTDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblETDDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCOTDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpETDDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLoadingSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadingSeaPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPOLSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsFreightPaymentPP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomsTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomsTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPackingNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPackingNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNoOfOriginals, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNoOfOriginals, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNoOfCopies, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNoOfCopies, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCOTDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDischargeSeaPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainersSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyageNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETADate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETDDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpETA2Date).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCarrierBookingRefNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillOfLadingNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainersCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCarriers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFreightPaymentPP).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPackingNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNoOfOriginals).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNoOfCopies).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCertificateDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
